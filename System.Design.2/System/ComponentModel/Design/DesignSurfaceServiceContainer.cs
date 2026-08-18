using System;
using System.Collections;
using System.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x020001CA RID: 458
	internal sealed class DesignSurfaceServiceContainer : ServiceContainer
	{
		// Token: 0x06001112 RID: 4370 RVA: 0x0005EC70 File Offset: 0x0005CE70
		internal DesignSurfaceServiceContainer(IServiceProvider parentProvider) : base(parentProvider)
		{
			this.AddFixedService(typeof(DesignSurfaceServiceContainer), this);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0005EC8A File Offset: 0x0005CE8A
		internal void AddFixedService(Type serviceType, object serviceInstance)
		{
			base.AddService(serviceType, serviceInstance);
			if (this._fixedServices == null)
			{
				this._fixedServices = new Hashtable();
			}
			this._fixedServices[serviceType] = serviceType;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0005ECB4 File Offset: 0x0005CEB4
		internal void RemoveFixedService(Type serviceType)
		{
			if (this._fixedServices != null)
			{
				this._fixedServices.Remove(serviceType);
			}
			base.RemoveService(serviceType);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0005ECD4 File Offset: 0x0005CED4
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

		// Token: 0x040009AA RID: 2474
		private Hashtable _fixedServices;
	}
}
