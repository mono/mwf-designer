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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

#if WITH_MONO_DESIGN
using Mono.Design;
using UndoEngine = Mono.Design.UndoEngine;
#endif

namespace mwf_designer
{
	internal class UndoRedoEngine : UndoEngine
	{
		private Stack<UndoUnit> _undoUnits;
		private Stack<UndoUnit> _redoUnits;

		public UndoRedoEngine (IServiceProvider provider) : base (provider)
		{
			_undoUnits = new Stack<UndoUnit> ();
			_redoUnits = new Stack<UndoUnit> ();
		}

		protected override void AddUndoUnit (UndoEngine.UndoUnit unit) 
		{
			_undoUnits.Push (unit);
		}

		protected override void DiscardUndoUnit (UndoEngine.UndoUnit unit) 
		{
			if (_undoUnits.Count > 0 && Object.ReferenceEquals (unit, _undoUnits.Peek ()))
				_undoUnits.Pop ();
		}

		public void Undo ()
		{
			this.Undo (1);
		}

		public void Undo (int actionsCount)
		{
			if (actionsCount <= 0 || actionsCount > _undoUnits.Count)
				throw new ArgumentOutOfRangeException ("actionsCount");

			for (; actionsCount != 0; actionsCount--) {
				UndoUnit unit = _undoUnits.Pop ();
				unit.Undo ();
				_redoUnits.Push (unit);
			}
		}

		public void Redo ()
		{
			this.Redo (1);
		}

		public void Redo (int actionsCount)
		{
			if (actionsCount <= 0 || actionsCount > _redoUnits.Count)
				throw new ArgumentOutOfRangeException ("actionsCount");

			for (; actionsCount != 0; actionsCount--) {
				UndoUnit unit = _redoUnits.Pop ();
				unit.Undo ();
				_undoUnits.Push (unit);
			}
		}

		public List<string> UndoUnitsNames {
			get {
				List<string> names = new List<string> ();
				foreach (UndoUnit unit in _undoUnits)
					names.Add (unit.Name);
				return names; 
			}
		}

		public List<string> RedoUnitsNames {
			get {
				List<string> names = new List<string> ();
				foreach (UndoUnit unit in _redoUnits)
					names.Add (unit.Name);
				return names; 
			}
		}

		protected override void Dispose (bool disposing)
		{
			if (disposing) {
				_undoUnits.Clear ();
				_redoUnits.Clear ();
			}
			base.Dispose ();
		}
	}
}
