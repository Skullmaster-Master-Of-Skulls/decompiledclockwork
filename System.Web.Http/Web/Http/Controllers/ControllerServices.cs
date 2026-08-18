using System;
using System.Collections.Generic;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200012B RID: 299
	public class ControllerServices : ServicesContainer
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0001898F File Offset: 0x00016B8F
		public ControllerServices(ServicesContainer parent)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			this._parent = parent;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000189AC File Offset: 0x00016BAC
		public override bool IsSingleService(Type serviceType)
		{
			return this._parent.IsSingleService(serviceType);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000189BC File Offset: 0x00016BBC
		public override object GetService(Type serviceType)
		{
			object result;
			if (this._overrideSingle != null && this._overrideSingle.TryGetValue(serviceType, out result))
			{
				return result;
			}
			return this._parent.GetService(serviceType);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000189F0 File Offset: 0x00016BF0
		public override IEnumerable<object> GetServices(Type serviceType)
		{
			List<object> result;
			if (this._overrideMulti != null && this._overrideMulti.TryGetValue(serviceType, out result))
			{
				return result;
			}
			return this._parent.GetServices(serviceType);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00018A23 File Offset: 0x00016C23
		protected override void ReplaceSingle(Type serviceType, object service)
		{
			if (this._overrideSingle == null)
			{
				this._overrideSingle = new Dictionary<Type, object>();
			}
			this._overrideSingle[serviceType] = service;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00018A45 File Offset: 0x00016C45
		protected override void ClearSingle(Type serviceType)
		{
			if (this._overrideSingle == null)
			{
				return;
			}
			this._overrideSingle.Remove(serviceType);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00018A60 File Offset: 0x00016C60
		protected override List<object> GetServiceInstances(Type serviceType)
		{
			if (this._overrideMulti == null)
			{
				this._overrideMulti = new Dictionary<Type, List<object>>();
			}
			List<object> list;
			if (!this._overrideMulti.TryGetValue(serviceType, out list))
			{
				list = new List<object>(this._parent.GetServices(serviceType));
				this._overrideMulti[serviceType] = list;
			}
			return list;
		}

		// Token: 0x0400021B RID: 539
		private Dictionary<Type, object> _overrideSingle;

		// Token: 0x0400021C RID: 540
		private Dictionary<Type, List<object>> _overrideMulti;

		// Token: 0x0400021D RID: 541
		private readonly ServicesContainer _parent;
	}
}
