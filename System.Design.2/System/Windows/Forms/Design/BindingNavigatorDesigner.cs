using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200029C RID: 668
	internal class BindingNavigatorDesigner : ToolStripDesigner
	{
		// Token: 0x060019CE RID: 6606 RVA: 0x000937FC File Offset: 0x000919FC
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRemoved += this.ComponentChangeSvc_ComponentRemoved;
				componentChangeService.ComponentChanged += this.ComponentChangeSvc_ComponentChanged;
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00093850 File Offset: 0x00091A50
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRemoved -= this.ComponentChangeSvc_ComponentRemoved;
					componentChangeService.ComponentChanged -= this.ComponentChangeSvc_ComponentChanged;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000938A4 File Offset: 0x00091AA4
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			BindingNavigator bindingNavigator = (BindingNavigator)base.Component;
			IDesignerHost host = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			try
			{
				ToolStripDesigner._autoAddNewItems = false;
				bindingNavigator.SuspendLayout();
				bindingNavigator.AddStandardItems();
				this.SiteItems(host, bindingNavigator.Items);
				this.RaiseItemsChanged();
				bindingNavigator.ResumeLayout();
				bindingNavigator.ShowItemToolTips = true;
			}
			finally
			{
				ToolStripDesigner._autoAddNewItems = true;
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00093930 File Offset: 0x00091B30
		private void RaiseItemsChanged()
		{
			BindingNavigator component = (BindingNavigator)base.Component;
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				MemberDescriptor member = TypeDescriptor.GetProperties(component)["Items"];
				componentChangeService.OnComponentChanging(component, member);
				componentChangeService.OnComponentChanged(component, member, null, null);
				foreach (string name in BindingNavigatorDesigner.itemNames)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[name];
					if (propertyDescriptor != null)
					{
						componentChangeService.OnComponentChanging(component, propertyDescriptor);
						componentChangeService.OnComponentChanged(component, propertyDescriptor, null, null);
					}
				}
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000939CC File Offset: 0x00091BCC
		private void SiteItem(IDesignerHost host, ToolStripItem item)
		{
			if (item is DesignerToolStripControlHost)
			{
				return;
			}
			host.Container.Add(item, DesignerUtils.GetUniqueSiteName(host, item.Name));
			item.Name = item.Site.Name;
			ToolStripDropDownItem toolStripDropDownItem = item as ToolStripDropDownItem;
			if (toolStripDropDownItem != null && toolStripDropDownItem.HasDropDownItems)
			{
				this.SiteItems(host, toolStripDropDownItem.DropDownItems);
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00093A2C File Offset: 0x00091C2C
		private void SiteItems(IDesignerHost host, ToolStripItemCollection items)
		{
			foreach (object obj in items)
			{
				ToolStripItem item = (ToolStripItem)obj;
				this.SiteItem(host, item);
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00093A84 File Offset: 0x00091C84
		private void ComponentChangeSvc_ComponentRemoved(object sender, ComponentEventArgs e)
		{
			ToolStripItem toolStripItem = e.Component as ToolStripItem;
			if (toolStripItem != null)
			{
				BindingNavigator bindingNavigator = (BindingNavigator)base.Component;
				if (toolStripItem == bindingNavigator.MoveFirstItem)
				{
					bindingNavigator.MoveFirstItem = null;
					return;
				}
				if (toolStripItem == bindingNavigator.MovePreviousItem)
				{
					bindingNavigator.MovePreviousItem = null;
					return;
				}
				if (toolStripItem == bindingNavigator.MoveNextItem)
				{
					bindingNavigator.MoveNextItem = null;
					return;
				}
				if (toolStripItem == bindingNavigator.MoveLastItem)
				{
					bindingNavigator.MoveLastItem = null;
					return;
				}
				if (toolStripItem == bindingNavigator.PositionItem)
				{
					bindingNavigator.PositionItem = null;
					return;
				}
				if (toolStripItem == bindingNavigator.CountItem)
				{
					bindingNavigator.CountItem = null;
					return;
				}
				if (toolStripItem == bindingNavigator.AddNewItem)
				{
					bindingNavigator.AddNewItem = null;
					return;
				}
				if (toolStripItem == bindingNavigator.DeleteItem)
				{
					bindingNavigator.DeleteItem = null;
				}
			}
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00093B38 File Offset: 0x00091D38
		private void ComponentChangeSvc_ComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			BindingNavigator bindingNavigator = (BindingNavigator)base.Component;
			if (e.Component != null && e.Component == bindingNavigator.CountItem && e.Member != null && e.Member.Name == "Text")
			{
				bindingNavigator.CountItemFormat = bindingNavigator.CountItem.Text;
			}
		}

		// Token: 0x040015BE RID: 5566
		private static string[] itemNames = new string[]
		{
			"MovePreviousItem",
			"MoveFirstItem",
			"MoveNextItem",
			"MoveLastItem",
			"AddNewItem",
			"DeleteItem",
			"PositionItem",
			"CountItem"
		};
	}
}
