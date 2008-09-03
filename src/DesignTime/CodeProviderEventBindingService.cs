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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

#if WITH_MONO_DESIGN
using Mono.Design;
using EventBindingService = Mono.Design.EventBindingService;
#endif

namespace mwf_designer
{
	internal class CodeProviderEventBindingService : EventBindingService
	{

		private CodeProvider _codeProvider;

		public CodeProviderEventBindingService (CodeProvider codeProvider, IServiceProvider provider) : base (provider)
		{
			if (codeProvider == null)
				throw new ArgumentNullException ("codeProvider");
			_codeProvider = codeProvider;
		}
					
		protected override string CreateUniqueMethodName (IComponent component, EventDescriptor eventDescriptor)
		{
			string methodName = component.Site.Name + "_" + eventDescriptor.Name;
			ICollection compatibleMethodNames = this.GetCompatibleMethods (eventDescriptor);
			if (compatibleMethodNames.Count == 0)
				return methodName;

			bool interrupt = false;
			int i = 0;
			while (!interrupt) {
				string tmpName = methodName;
				foreach (string existingName in compatibleMethodNames) {
					if (existingName == tmpName)
						tmpName += i.ToString ();
					else {
						methodName = tmpName;
						interrupt = true;
					}
				}
				i++;
			}

			return methodName;
		}

		protected override ICollection GetCompatibleMethods (EventDescriptor eventDescriptor)
		{
			return _codeProvider.GetCompatibleMethods (eventDescriptor.EventType.GetMethod ("Invoke").GetParameters ());
		}


		protected override bool ShowCode (IComponent component, EventDescriptor e, string methodName)
		{
			return this.ShowCode (0);
		}

		protected override bool ShowCode (int lineNumber)
		{
			// XXX: No text editor control
			return false;
		}

		protected override bool ShowCode ()
		{
			return ShowCode (0);
		}
	}
}
