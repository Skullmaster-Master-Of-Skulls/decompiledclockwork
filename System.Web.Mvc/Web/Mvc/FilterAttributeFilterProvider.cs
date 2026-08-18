using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020000C3 RID: 195
	public class FilterAttributeFilterProvider : IFilterProvider
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x0000E489 File Offset: 0x0000C689
		public FilterAttributeFilterProvider() : this(true)
		{
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000E492 File Offset: 0x0000C692
		public FilterAttributeFilterProvider(bool cacheAttributeInstances)
		{
			this._cacheAttributeInstances = cacheAttributeInstances;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000E4A1 File Offset: 0x0000C6A1
		protected virtual IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return actionDescriptor.GetFilterAttributes(this._cacheAttributeInstances);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000E4AF File Offset: 0x0000C6AF
		protected virtual IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return actionDescriptor.ControllerDescriptor.GetFilterAttributes(this._cacheAttributeInstances);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000E770 File Offset: 0x0000C970
		public virtual IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			if (controllerContext.Controller != null)
			{
				foreach (FilterAttribute attr in this.GetControllerAttributes(controllerContext, actionDescriptor))
				{
					yield return new Filter(attr, FilterScope.Controller, null);
				}
				foreach (FilterAttribute attr2 in this.GetActionAttributes(controllerContext, actionDescriptor))
				{
					yield return new Filter(attr2, FilterScope.Action, null);
				}
			}
			yield break;
		}

		// Token: 0x04000164 RID: 356
		private readonly bool _cacheAttributeInstances;
	}
}
