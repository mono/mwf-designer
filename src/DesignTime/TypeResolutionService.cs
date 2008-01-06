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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

#if WITH_MONO_DESIGN
using Mono.Design;
#endif

namespace mwf_designer
{
	internal class TypeResolutionService : ITypeResolutionService
	{
		private References _references;

		public TypeResolutionService (References references)
		{
			if (references == null)
				throw new ArgumentNullException ("references");
			_references = references;
		}

		public References References {
			get { return _references; }
			set { _references = value; }
		}

		public Assembly GetAssembly (AssemblyName name)
		{
			return this.GetAssembly (name, false);
		}

		public Assembly GetAssembly (AssemblyName name, bool throwOnError)
		{
			Assembly result = null;

			foreach (Assembly assembly in _references.Assemblies)
				if (assembly.GetName().Name == name.Name)
					result = assembly;

			if (result == null && throwOnError)
				throw new ArgumentException ("Assembly not found: " + name.Name);
			return result;
		}

		public string GetPathOfAssembly (AssemblyName name)
		{
			throw new NotImplementedException ();
		}

		public Type GetType (string name)
		{
			return this.GetType (name, false);
		}

		public Type GetType (string name, bool throwOnError)
		{
			return this.GetType (name, throwOnError, false);
		}

		public Type GetType (string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
				throw new ArgumentNullException ("name");

			Type result = null;

			foreach (Assembly assembly in _references.Assemblies) {
				if (name.IndexOf (".") != -1) { // a fully qualified name, e.g System.Windows.Forms.Button 
					result = assembly.GetType (name, false, ignoreCase);
					if (result != null)
						break;
				} else {
					Type[] types = assembly.GetTypes ();
					foreach (Type type in types) {
						if (String.Compare (type.Name, name, ignoreCase) == 0) {
							result = type;
							break;
						}
					}
				}
			}

			if (result == null)
				result = Type.GetType (name, false, ignoreCase);

			if (result == null)
				result = Assembly.GetExecutingAssembly ().GetType (name, false, ignoreCase);

			return result;
		}

		public void ReferenceAssembly (AssemblyName name)
		{
			_references.AddReference (name.FullName);
		}
	}

}
