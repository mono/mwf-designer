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
using System.Reflection;
using System.Drawing.Design;

#if WITH_MONO_DESIGN
using Mono.Design;
#endif

namespace mwf_designer
{
	internal class Workspace
	{
		private Dictionary<object, Document> _documents;
		private Document _activeDocument;
		private References _references;
		private ServiceContainer _serviceContainer;

		public Workspace ()
		{
			_documents = new Dictionary<object, Document>();
			_activeDocument = null;
			_references = new References ();
			_serviceContainer = new ServiceContainer ();
		}

		public void Load ()
		{
			LoadDefaultReferences (_references);
			InitializeWorkspaceServiceContainer (_serviceContainer);
		}

		public void Dispose ()
		{
			_activeDocument = null;
			_documents = null;
			_references.Dispose ();
			_references = null;
			_serviceContainer.Dispose ();
			_serviceContainer = null;
		}

		private void InitializeWorkspaceServiceContainer (IServiceContainer container)
		{
			container.AddService (typeof (ITypeResolutionService), new TypeResolutionService (this.References));
		}

		private void LoadDefaultReferences (References references)
		{
			references.AddReference (Assembly.Load ("System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			references.AddReference (Assembly.Load ("System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
			references.AddReference (Assembly.Load ("System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
		}

		public References References {
			get { return _references; }
		}

		public Document CreateDocument (string file)
		{
			return CreateDocument (file, file);
		}

		public Document CreateDocument (string file, object identifierKey)
		{
			Document doc = new Document (file, this);
			_documents[identifierKey] = doc;
			return doc;
		}

		public Document GetDocument (object identifierKey)
		{
			return _documents[identifierKey];
		}

		public void CloseDocument (Document doc)
		{
			object key = null;
			foreach (KeyValuePair<object, Document> kvp in _documents)
				if (doc == kvp.Value)
					key = kvp.Key;
			if (key != null)
				CloseDocument (key);
		}

		public void CloseDocument (object identifierKey)
		{
			Document doc = _documents[identifierKey];
			if (doc != null) {
				if (doc.IsModified && doc.LoadSuccessful)
					doc.Save ();
				_documents[identifierKey] = null;
				doc.Dispose ();
			}
		}

		public ServiceContainer Services {
			get { return _serviceContainer; }
		}

		public Document ActiveDocument {
			get { return _activeDocument; }
			set { 
				Document old = _activeDocument;
				_activeDocument = value;
				OnActiveDocumentChanged (value, old);
			}
		}

		protected virtual void OnActiveDocumentChanged (Document newDocument, Document oldDocument)
		{
			if (ActiveDocumentChanged != null)
				ActiveDocumentChanged (this, new ActiveDocumentChangedEventArgs (newDocument, oldDocument));
		}

		public event ActiveDocumentChangedEventHandler ActiveDocumentChanged;
	}


	internal class ActiveDocumentChangedEventArgs : EventArgs
	{
		private Document _oldDocument;
		private Document _newDocument;

		public ActiveDocumentChangedEventArgs (Document newDocument, Document oldDocument)
		{
			_newDocument = newDocument;
			_oldDocument = oldDocument;
		}

		public Document OldDocument {
			get { return _oldDocument; }
			set { _oldDocument = value; }
		}

		public Document NewDocument {
			get { return _newDocument; }
			set { _newDocument = value; }
		}
	}

	internal delegate void ActiveDocumentChangedEventHandler (object sender, ActiveDocumentChangedEventArgs args);

}
