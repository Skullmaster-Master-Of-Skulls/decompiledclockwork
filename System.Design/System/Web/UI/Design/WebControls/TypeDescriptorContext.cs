using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004FC RID: 1276
	internal sealed class TypeDescriptorContext : ITypeDescriptorContext, IServiceProvider
	{
		// Token: 0x06002DA6 RID: 11686 RVA: 0x0010340E File Offset: 0x0010240E
		public TypeDescriptorContext(IDesignerHost designerHost, PropertyDescriptor propDesc, object instance)
		{
			this._designerHost = designerHost;
			this._propDesc = propDesc;
			this._instance = instance;
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x0010342B File Offset: 0x0010242B
		private IComponentChangeService ComponentChangeService
		{
			get
			{
				return (IComponentChangeService)this._designerHost.GetService(typeof(IComponentChangeService));
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x00103447 File Offset: 0x00102447
		public IContainer Container
		{
			get
			{
				return (IContainer)this._designerHost.GetService(typeof(IContainer));
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x00103463 File Offset: 0x00102463
		public object Instance
		{
			get
			{
				return this._instance;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x0010346B File Offset: 0x0010246B
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this._propDesc;
			}
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x00103473 File Offset: 0x00102473
		public object GetService(Type serviceType)
		{
			return this._designerHost.GetService(serviceType);
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x00103484 File Offset: 0x00102484
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

		// Token: 0x06002DAD RID: 11693 RVA: 0x001034D4 File Offset: 0x001024D4
		public void OnComponentChanged()
		{
			if (this.ComponentChangeService != null)
			{
				this.ComponentChangeService.OnComponentChanged(this._instance, this._propDesc, null, null);
			}
		}

		// Token: 0x04001F0E RID: 7950
		private IDesignerHost _designerHost;

		// Token: 0x04001F0F RID: 7951
		private PropertyDescriptor _propDesc;

		// Token: 0x04001F10 RID: 7952
		private object _instance;
	}
}
