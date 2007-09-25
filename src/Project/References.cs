
//
// Authors:	 
//	  Ivan N. Zlatev (contact i-nZ.net)
//
// (C) 2007 Ivan N. Zlatev

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
using System.Text;
using System.Reflection;
using System.IO;

namespace mwf_designer
{
	internal class References
	{
		private List<Assembly> _assemblies;

		public References ()
		{
			_assemblies = new List<Assembly> ();
		}

		public void Dispose ()
		{
			_assemblies.Clear ();
			_assemblies = null;
		}

		public void AddReference (Assembly assembly)
		{
			if (assembly == null)
				throw new ArgumentNullException ("assembly");

			_assemblies.Add (assembly);
			OnReferencesChanged ();
		}

		public bool AddReference (string fileName)
		{
			Assembly assembly = null;
			try {
				assembly = Assembly.LoadFile (fileName);
			} catch { }
			if (assembly != null)
				this.AddReference (assembly);
			return assembly != null;
		}

		public void RemoveReference (string fileName)
		{
			int toRemove = -1;
			for (int i = 0; i < _assemblies.Count; i++) {
				if (String.Equals (fileName, Path.GetFileName (_assemblies[i].Location))) {
					toRemove = i;
					break;
				}
			}
			if (toRemove != -1) {
				_assemblies.RemoveAt (toRemove);
				OnReferencesChanged ();
			}
		}

		public string[] FileNames {
			get {
				string[] files = new string[_assemblies.Count];
				for (int i = 0; i < _assemblies.Count; i++)
					files[i] = Path.GetFileName (_assemblies[i].Location);
				return files;
			}
		}

		public List<Assembly> Assemblies {
			get{ return _assemblies; }
		}

		private void OnReferencesChanged ()
		{
			if (ReferencesChanged != null)
				ReferencesChanged (this, EventArgs.Empty);
		}

		public event EventHandler ReferencesChanged;
	}
}
