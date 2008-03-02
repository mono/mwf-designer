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
using System.Collections;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Windows.Forms.Design;


#if WITH_MONO_DESIGN
using Mono.Design;
using CodeDomDesignerLoader = Mono.Design.CodeDomDesignerLoader;
#endif

namespace mwf_designer
{
	internal class CodeProviderDesignerLoader : CodeDomDesignerLoader
	{

		private CodeProvider _provider;

		public CodeProviderDesignerLoader (CodeProvider provider)
		{
			if (provider == null)
				throw new ArgumentNullException ("provider");
			_provider = provider;
		}

		protected override CodeDomProvider CodeDomProvider {
			get { return _provider.CodeDomProvider; }
		}

		protected override ITypeResolutionService TypeResolutionService { 
			get { return base.GetService (typeof (ITypeResolutionService)) as ITypeResolutionService; }
		}

		protected override CodeCompileUnit Parse ()
		{
			return _provider.Parse ();
		}

		protected override void Write (CodeCompileUnit unit)
		{
			_provider.Write (unit);
		}

		protected override void OnEndLoad (bool successful, ICollection errors)
		{
			ReportErrors (errors);
			base.OnEndLoad (successful, errors);
		}

		protected override void ReportFlushErrors (ICollection errors)
		{
			ReportErrors (errors);
		}

		private void ReportErrors (ICollection errors)
		{
			IUIService service = base.GetService (typeof (IUIService)) as IUIService;
			if (service != null) {
				foreach (object error in errors) {
					if (error is Exception)
						service.ShowError ((Exception) error);
					else if (error is string)
						service.ShowError ((string) error);
					else
						service.ShowError (error.ToString ());
				}
			}
		}
	}
}
