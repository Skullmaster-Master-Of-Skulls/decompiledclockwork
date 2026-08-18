using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000306 RID: 774
	internal class ListBoxDesigner : ControlDesigner
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x000B77B4 File Offset: 0x000B59B4
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x000B77CC File Offset: 0x000B59CC
		public bool IntegralHeight
		{
			get
			{
				return (bool)base.ShadowProperties["IntegralHeight"];
			}
			set
			{
				base.ShadowProperties["IntegralHeight"] = value;
				ListBox listBox = (ListBox)base.Component;
				if (listBox.Dock != DockStyle.Fill && listBox.Dock != DockStyle.Left && listBox.Dock != DockStyle.Right)
				{
					listBox.IntegralHeight = value;
				}
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x000B781D File Offset: 0x000B5A1D
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x000B7830 File Offset: 0x000B5A30
		public DockStyle Dock
		{
			get
			{
				return ((ListBox)base.Component).Dock;
			}
			set
			{
				ListBox listBox = (ListBox)base.Component;
				if (value == DockStyle.Fill || value == DockStyle.Left || value == DockStyle.Right)
				{
					listBox.IntegralHeight = false;
					listBox.Dock = value;
					return;
				}
				listBox.Dock = value;
				listBox.IntegralHeight = (bool)base.ShadowProperties["IntegralHeight"];
			}
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000B7888 File Offset: 0x000B5A88
		protected override void PreFilterProperties(IDictionary properties)
		{
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["IntegralHeight"];
			if (propertyDescriptor != null)
			{
				properties["IntegralHeight"] = TypeDescriptor.CreateProperty(typeof(ListBoxDesigner), propertyDescriptor, new Attribute[0]);
			}
			PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)properties["Dock"];
			if (propertyDescriptor2 != null)
			{
				properties["Dock"] = TypeDescriptor.CreateProperty(typeof(ListBoxDesigner), propertyDescriptor2, new Attribute[0]);
			}
			base.PreFilterProperties(properties);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000B7908 File Offset: 0x000B5B08
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRename -= this.OnComponentRename;
					componentChangeService.ComponentChanged -= this.OnComponentChanged;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000B795C File Offset: 0x000B5B5C
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			ListBox listBox = component as ListBox;
			if (listBox != null)
			{
				this.IntegralHeight = listBox.IntegralHeight;
			}
			base.AutoResizeHandles = true;
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRename += this.OnComponentRename;
				componentChangeService.ComponentChanged += this.OnComponentChanged;
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000B79CC File Offset: 0x000B5BCC
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			((ListBox)base.Component).FormattingEnabled = true;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Name"];
			if (propertyDescriptor != null)
			{
				this.UpdateControlName(propertyDescriptor.GetValue(base.Component).ToString());
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000B7A21 File Offset: 0x000B5C21
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (e.Component == base.Component)
			{
				this.UpdateControlName(e.NewName);
			}
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000B7A40 File Offset: 0x000B5C40
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (e.Component == base.Component && e.Member != null && e.Member.Name == "Items")
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Name"];
				if (propertyDescriptor != null)
				{
					this.UpdateControlName(propertyDescriptor.GetValue(base.Component).ToString());
				}
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000B7AAC File Offset: 0x000B5CAC
		protected override void OnCreateHandle()
		{
			base.OnCreateHandle();
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Name"];
			if (propertyDescriptor != null)
			{
				this.UpdateControlName(propertyDescriptor.GetValue(base.Component).ToString());
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000B7AF0 File Offset: 0x000B5CF0
		private void UpdateControlName(string name)
		{
			ListBox listBox = (ListBox)this.Control;
			if (listBox.IsHandleCreated && listBox.Items.Count == 0)
			{
				NativeMethods.SendMessage(listBox.Handle, 388, 0, 0);
				NativeMethods.SendMessage(listBox.Handle, 384, 0, name);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x000B7B44 File Offset: 0x000B5D44
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					if (base.Component is CheckedListBox)
					{
						this._actionLists.Add(new ListControlUnboundActionList(this));
					}
					else
					{
						this._actionLists.Add(new ListControlBoundActionList(this));
					}
				}
				return this._actionLists;
			}
		}

		// Token: 0x040017D5 RID: 6101
		private DesignerActionListCollection _actionLists;
	}
}
