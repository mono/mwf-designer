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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace mwf_designer
{
    internal partial class PropertyGrid : UserControl
    {

        public PropertyGrid()
        {
            InitializeComponent();
        }

		private void OnPrimarySelectionChanged (object sender, EventArgs args)
		{
			ISelectionService selectionService = this.GetService (typeof (ISelectionService)) as ISelectionService;
			if (selectionService != null) {
				ICollection componentsCollection = selectionService.GetSelectedComponents ();
				object[] components = new object[componentsCollection.Count];
				componentsCollection.CopyTo (components, 0);
				SetActiveComponents (components);
			}
		}

		private void OnComponentsCombo_SelectedIndexChanged (object sender, EventArgs args)
		{
			ISelectionService selectionService = this.GetService (typeof (ISelectionService)) as ISelectionService;
			if (selectionService != null && _componentsCombo.SelectedIndex != -1) {
				string selectedComponentName = (string) _componentsCombo.Items[_componentsCombo.SelectedIndex];
				IComponent selectedComponent = PrimarySelection.Site.Container.Components[selectedComponentName];
				selectionService.SetSelectedComponents (new IComponent[] { selectedComponent });
			}
		}

		public object[] ActiveComponents {
			get { return _propertyGrid.SelectedObjects; }
			set { SetActiveComponents (value); }
		}

		private IComponent PrimarySelection {
			get {
				if (this.ActiveComponents != null && ActiveComponents.Length > 0) {
					IComponent component = ActiveComponents[0] as IComponent;
					if (component != null && component.Site != null) {
						ISelectionService selectionService = component.Site.GetService (typeof (ISelectionService)) as ISelectionService;
						if (selectionService != null)
							return selectionService.PrimarySelection as IComponent;
					}
				}
				return null;
			}
		}

		private void SetActiveComponents (object[] components)
		{
			if (components.Length == 1) {
				IComponent component = (IComponent) components[0];
				if (_propertyGrid.SelectedObject != null) // detach events from the prev active component
					DisableNotification ((IComponent)_propertyGrid.SelectedObject);

				PopulateComponents (component.Site.Container.Components);
				_componentsCombo.SelectedIndex = _componentsCombo.Items.IndexOf (component.Site.Name);
				_propertyGrid.SelectedObject = component; 
				EnableNotification (component);
			} else if (components.Length == 0) {
				_propertyGrid.SelectedObjects = components;
				_componentsCombo.Items.Clear ();
			} else {
				_propertyGrid.SelectedObjects = components;
				_componentsCombo.SelectedIndex = -1;
			}
//          ShowEventsTab (); // MWF's PropertyGrid doesn't support EventsTab
		}

		// MWF's PropertyGrid doesn't support EventsTab
		//
		private void ShowEventsTab ()
		{
			_propertyGrid.PropertyTabs.AddTabType (typeof (System.Windows.Forms.Design.EventsTab));
			_propertyGrid.Site = PrimarySelection.Site;
			_propertyGrid.GetType ().InvokeMember ("ShowEventsButton", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
							   null, _propertyGrid, new object [] { true });
		}

		private void EnableNotification (IComponent component)
		{
			IComponentChangeService changeService = this.GetService (typeof (IComponentChangeService)) as IComponentChangeService;
			if (changeService != null) {
				changeService.ComponentAdded += new ComponentEventHandler (OnComponentAdded);
				changeService.ComponentRemoving += new ComponentEventHandler (OnComponentRemoving);
				changeService.ComponentRename += new ComponentRenameEventHandler (OnComponentRename);
			}
			ISelectionService selectionService = this.GetService (typeof (ISelectionService)) as ISelectionService;
			if (selectionService != null)
				selectionService.SelectionChanged += new EventHandler (OnPrimarySelectionChanged);
		}

		private void DisableNotification (IComponent component)
		{
			IComponentChangeService changeService = this.GetService (typeof (IComponentChangeService)) as IComponentChangeService;
			if (changeService != null) {
				changeService.ComponentAdded -= new ComponentEventHandler (OnComponentAdded);
				changeService.ComponentRemoving -= new ComponentEventHandler (OnComponentRemoving);
				changeService.ComponentRename -= new ComponentRenameEventHandler (OnComponentRename);
			}
			ISelectionService selectionService = this.GetService (typeof (ISelectionService)) as ISelectionService;
			if (selectionService != null)
				selectionService.SelectionChanged -= new EventHandler (OnPrimarySelectionChanged);
		}


		private void OnComponentAdded (object sender, ComponentEventArgs args)
		{
			_componentsCombo.Items.Add (args.Component.Site.Name);
		}

		private void OnComponentRemoving (object sender, ComponentEventArgs args)
		{
			int index = -1;
			for (int i=0; i < _componentsCombo.Items.Count; i++) {
				if (_componentsCombo.Items[i] == args.Component) {
					index = i;
					break;
				}
			}
			if (index != -1)
				_componentsCombo.Items.RemoveAt (index);
			// MWF bug: _componentsCombo.Items.Remove (args.Component.Site.Name);
		}

		private void OnComponentRename (object sender, ComponentRenameEventArgs args)
		{
			_componentsCombo.Items.Remove (args.OldName);
			_componentsCombo.SelectedIndex = _componentsCombo.Items.Add (args.NewName);
		}

		private void PopulateComponents (ComponentCollection components)
		{
			_componentsCombo.Items.Clear ();
			foreach (IComponent c in components) {
				_componentsCombo.Items.Add (c.Site.Name);
			}
		}

		protected override object GetService (Type type)
		{
			if (this.PrimarySelection != null && this.PrimarySelection.Site != null)
				return this.PrimarySelection.Site.GetService (type);
			else
				return base.GetService (type);
		}
    }
}
