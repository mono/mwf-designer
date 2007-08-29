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
