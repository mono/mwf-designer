//
// Authors:	 
//	  Jonathan Pobst (monkey@jpobst.com>)
//
// (C) 2007 Jonathan Pobst

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
using System.Drawing;
using System.Windows.Forms;

namespace mwf_designer
{
       public class ToolBoxGroupHeader : UserControl
       {
               #region Fields
               private bool expanded = true;
               #endregion

               #region Public Constructor
               public ToolBoxGroupHeader ()
               {
                       Size = new Size (200, 20);
                       SetStyle (ControlStyles.ResizeRedraw, true);
					   BackColor = SystemColors.ControlDark;
					   Font = new Font (this.Font, FontStyle.Bold);
               }
               #endregion

               #region Public Properties
               public bool Expanded {
                       get { return expanded; }
                       set {
                               if (expanded != value) {
                                       expanded = value;
                                       Invalidate ();
                               }
                       }
               }
               #endregion

               #region Protected Methods
               protected override void OnClick (EventArgs e)
               {
                       base.OnClick (e);

                       Expanded = !expanded;
                       OnExpandedChanged (EventArgs.Empty);
               }

               protected virtual void OnExpandedChanged (EventArgs e)
               {
                       EventHandler eh = (EventHandler)(Events[ExpandedChangedEvent]);
                       if (eh != null)
                               eh (this, e);
               }

               protected override void OnPaint (PaintEventArgs e)
               {
                       base.OnPaint (e);

                       Graphics g = e.Graphics;

                       g.Clear (BackColor);

                       if (expanded)
                               DrawDownChevron (g, new Point (7, 8), Pens.LightGray);
                       else
                               DrawRightChevron (g, new Point (10, 5), Pens.LightGray);

                       TextRenderer.DrawText (g, Text, Font, new Point (18, 2), Color.White);
               }
               #endregion

               #region Public Events
               static object ExpandedChangedEvent = new object ();

               public event EventHandler ExpandedChanged {
                       add { Events.AddHandler (ExpandedChangedEvent, value); }
                       remove { Events.RemoveHandler (ExpandedChangedEvent, value); }
               }
               #endregion

               #region Private Methods
               private void DrawDownChevron (Graphics g, Point location, Pen p)
               {
                       g.DrawLine (p, location.X, location.Y, location.X + 8, location.Y);
                       g.DrawLine (p, location.X + 1, location.Y + 1, location.X + 7, location.Y + 1);
                       g.DrawLine (p, location.X + 2, location.Y + 2, location.X + 6, location.Y + 2);
                       g.DrawLine (p, location.X + 3, location.Y + 3, location.X + 5, location.Y + 3);
               }

               private void DrawRightChevron (Graphics g, Point location, Pen p)
               {
                       g.DrawLine (p, location.X, location.Y, location.X, location.Y + 8);
                       g.DrawLine (p, location.X + 1, location.Y + 1, location.X + 1, location.Y + 7);
                       g.DrawLine (p, location.X + 2, location.Y + 2, location.X + 2, location.Y + 6);
                       g.DrawLine (p, location.X + 3, location.Y + 3, location.X + 3, location.Y + 5);
               }
               #endregion
       }
}
