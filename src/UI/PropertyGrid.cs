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
				if (this.ActiveComponents.Length > 0) {
					ISelectionService selectionService = ((IComponent)ActiveComponents[0]).Site.GetService (typeof (ISelectionService)) as ISelectionService;
					if (selectionService != null)
						return (IComponent) selectionService.PrimarySelection;
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
			_componentsCombo.Items.Remove (args.Component.Site.Name);
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
