using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200012F RID: 303
	internal sealed class TypeDescriptorContext : ITypeDescriptorContext, IServiceProvider
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00047640 File Offset: 0x00045840
		public TypeDescriptorContext(IDesignerHost designerHost, PropertyDescriptor propDesc, object instance)
		{
			this._designerHost = designerHost;
			this._propDesc = propDesc;
			this._instance = instance;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0004765D File Offset: 0x0004585D
		private IComponentChangeService ComponentChangeService
		{
			get
			{
				return (IComponentChangeService)this._designerHost.GetService(typeof(IComponentChangeService));
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00047679 File Offset: 0x00045879
		public IContainer Container
		{
			get
			{
				return (IContainer)this._designerHost.GetService(typeof(IContainer));
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00047695 File Offset: 0x00045895
		public object Instance
		{
			get
			{
				return this._instance;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0004769D File Offset: 0x0004589D
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this._propDesc;
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000476A5 File Offset: 0x000458A5
		public object GetService(Type serviceType)
		{
			return this._designerHost.GetService(serviceType);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000476B4 File Offset: 0x000458B4
		public bool OnComponentChanging()
		{
			if (this.ComponentChangeService != null)
			{
				try
				{
					this.ComponentChangeService.OnComponentChanging(this._instance, this._propDesc);
				}
				catch (CheckoutException ex)
				{
					if (ex == CheckoutException.Canceled)
					{
						return false;
					}
					throw ex;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00047704 File Offset: 0x00045904
		public void OnComponentChanged()
		{
			if (this.ComponentChangeService != null)
			{
				this.ComponentChangeService.OnComponentChanged(this._instance, this._propDesc, null, null);
			}
		}

		// Token: 0x04000696 RID: 1686
		private IDesignerHost _designerHost;

		// Token: 0x04000697 RID: 1687
		private PropertyDescriptor _propDesc;

		// Token: 0x04000698 RID: 1688
		private object _instance;
	}
}
