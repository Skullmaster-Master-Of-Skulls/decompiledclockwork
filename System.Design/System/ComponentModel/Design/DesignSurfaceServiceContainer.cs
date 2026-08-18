using System;
using System.Collections;
using System.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x0200055F RID: 1375
	internal sealed class DesignSurfaceServiceContainer : ServiceContainer
	{
		// Token: 0x0600309B RID: 12443 RVA: 0x00113591 File Offset: 0x00112591
		internal DesignSurfaceServiceContainer(IServiceProvider parentProvider) : base(parentProvider)
		{
			this.AddFixedService(typeof(DesignSurfaceServiceContainer), this);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x001135AB File Offset: 0x001125AB
		internal void AddFixedService(Type serviceType, object serviceInstance)
		{
			base.AddService(serviceType, serviceInstance);
			if (this._fixedServices == null)
			{
				this._fixedServices = new Hashtable();
			}
			this._fixedServices[serviceType] = serviceType;
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x001135D5 File Offset: 0x001125D5
		internal void RemoveFixedService(Type serviceType)
		{
			if (this._fixedServices != null)
			{
				this._fixedServices.Remove(serviceType);
			}
			base.RemoveService(serviceType);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x001135F4 File Offset: 0x001125F4
		public override void RemoveService(Type serviceType, bool promote)
		{
			if (serviceType != null && this._fixedServices != null && this._fixedServices.ContainsKey(serviceType))
			{
				throw new InvalidOperationException(SR.GetString("DesignSurfaceServiceIsFixed", new object[]
				{
					serviceType.Name
				}));
			}
			base.RemoveService(serviceType, promote);
		}

		// Token: 0x040020AB RID: 8363
		private Hashtable _fixedServices;
	}
}
