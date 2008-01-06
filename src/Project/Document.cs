//
// Authors:	 
//	  Ivan N. Zlatev (contact i-nZ.net)
//
// (C) 2007-2008 Ivan N. Zlatev

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;

#if WITH_MONO_DESIGN
using Mono.Design;
using DesignSurface = Mono.Design.DesignSurface;
using UndoEngine = Mono.Design.UndoEngine;
#endif

namespace mwf_designer
{
	internal class Document : IDisposable
	{
		private bool _loaded;

		private CodeProvider _codeProvider;
		private Workspace _workspace;
		private string _fileName;
		private DesignSurface _surface;
		private CodeProviderDesignerLoader _loader;
		private bool _modified;

		public Document (string fileName, Workspace workspace)
		{
			if (workspace == null)
				throw new ArgumentNullException ("workspace");
			if (fileName == null)
				throw new ArgumentNullException ("fileName");
			_fileName = fileName;
			_workspace = workspace;
			_loaded = false;
			_surface = new DesignSurface (_workspace.Services);
		}

		// Note that this ServiceContainer is not the workspace one.
		// The workspace one is the parent of the surface one. So when services
		// are added to this container they won't be added to the parent one 
		// (unless promoted). 
		// 
		// Basically added services will be available only to the document
		//
		public IServiceContainer Services {
			get { return _surface.GetService (typeof (IServiceContainer)) as IServiceContainer; }
		}

		public bool Load ()
		{
			if (_loaded)
				return true;
			_loaded = false;
			_modified = false;

			// Initialize code provider, loader and surface
			//

			ITypeResolutionService resolutionSvc = _surface.GetService (typeof (ITypeResolutionService)) as ITypeResolutionService;
			_codeProvider = new CodeProvider (this.FileName, resolutionSvc);
			_loader = new CodeProviderDesignerLoader (_codeProvider);

			// Initialize and add the services
			//
			IServiceContainer container = (IServiceContainer) _surface.GetService (typeof (IServiceContainer));
			container.AddService (typeof (IEventBindingService),
					      new CodeProviderEventBindingService (_codeProvider, (IServiceProvider) container));
			_surface.BeginLoad (_loader);
			if (_surface.IsLoaded) {
				_loaded = true;
				// Mark as Modified on ComponentChanged
				//
				IComponentChangeService changeService = (IComponentChangeService)_surface.GetService (typeof (IComponentChangeService));
				changeService.ComponentChanged += delegate {
					_modified = true;
					if (Modified != null)
						Modified (this, EventArgs.Empty);
				};
				if (Loaded != null)
					Loaded (this, EventArgs.Empty);
			}

			return _loaded;
		}

		public bool LoadSuccessful {
			get { return _loaded; }
			set { _loaded = value; }
		}

		public void Save ()
		{
			if (_loaded && _modified) {
				_surface.Flush ();
				_modified = false;
			}
		}

		public bool IsModified {
			get { return _modified; }
		}

		public string FileName {
			get { return _fileName; }
		}

		public string CodeBehindFileName {
			get { return CodeProvider.GetCodeBehindFileName (_fileName); }
		}


		public CodeProvider CodeProvider {
			get { return _codeProvider; }
		}

		public Workspace Workspace {
			get { return _workspace; }
		}

		public DesignSurface DesignSurface {
			get { return _surface; }
		}

		public void Dispose ()
		{
			_surface.Dispose ();
			_codeProvider = null;
		}

		public event EventHandler Loaded;
		public event EventHandler Modified;
	}
}
