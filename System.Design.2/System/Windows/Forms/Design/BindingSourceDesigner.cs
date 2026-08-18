using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Data;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200029D RID: 669
	internal class BindingSourceDesigner : ComponentDesigner
	{
		// Token: 0x170005B8 RID: 1464
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x00093BF8 File Offset: 0x00091DF8
		public bool BindingUpdatedByUser
		{
			set
			{
				this.bindingUpdatedByUser = value;
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00093C04 File Offset: 0x00091E04
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanged += this.OnComponentChanged;
				componentChangeService.ComponentRemoving += this.OnComponentRemoving;
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00093C58 File Offset: 0x00091E58
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentChanged -= this.OnComponentChanged;
					componentChangeService.ComponentRemoving -= this.OnComponentRemoving;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00093CAC File Offset: 0x00091EAC
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (this.bindingUpdatedByUser && e.Component == base.Component && e.Member != null && (e.Member.Name == "DataSource" || e.Member.Name == "DataMember"))
			{
				this.bindingUpdatedByUser = false;
				DataSourceProviderService dataSourceProviderService = (DataSourceProviderService)this.GetService(typeof(DataSourceProviderService));
				if (dataSourceProviderService != null)
				{
					dataSourceProviderService.NotifyDataSourceComponentAdded(base.Component);
				}
			}
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00093D34 File Offset: 0x00091F34
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			BindingSource bindingSource = base.Component as BindingSource;
			if (bindingSource != null && bindingSource.DataSource == e.Component)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				string dataMember = bindingSource.DataMember;
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(bindingSource);
				PropertyDescriptor propertyDescriptor = (properties != null) ? properties["DataMember"] : null;
				if (componentChangeService != null && propertyDescriptor != null)
				{
					componentChangeService.OnComponentChanging(bindingSource, propertyDescriptor);
				}
				bindingSource.DataSource = null;
				if (componentChangeService != null && propertyDescriptor != null)
				{
					componentChangeService.OnComponentChanged(bindingSource, propertyDescriptor, dataMember, "");
				}
			}
		}

		// Token: 0x040015BF RID: 5567
		private bool bindingUpdatedByUser;
	}
}
