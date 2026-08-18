using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Filters;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020000C8 RID: 200
	public sealed class GlobalFilterCollection : IEnumerable<Filter>, IEnumerable, IFilterProvider
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public int Count
		{
			get
			{
				return this._filters.Count;
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000EA2C File Offset: 0x0000CC2C
		public void Add(object filter)
		{
			this.AddInternal(filter, null);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000EA49 File Offset: 0x0000CC49
		public void Add(object filter, int order)
		{
			this.AddInternal(filter, new int?(order));
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000EA58 File Offset: 0x0000CC58
		private void AddInternal(object filter, int? order)
		{
			GlobalFilterCollection.ValidateFilterInstance(filter);
			this._filters.Add(new Filter(filter, FilterScope.Global, order));
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000EA74 File Offset: 0x0000CC74
		public void Clear()
		{
			this._filters.Clear();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		public bool Contains(object filter)
		{
			return this._filters.Any((Filter f) => f.Instance == filter);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000EACD File Offset: 0x0000CCCD
		public IEnumerator<Filter> GetEnumerator()
		{
			return this._filters.GetEnumerator();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000EADF File Offset: 0x0000CCDF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._filters.GetEnumerator();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000EAF1 File Offset: 0x0000CCF1
		IEnumerable<Filter> IFilterProvider.GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return this;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		public void Remove(object filter)
		{
			this._filters.RemoveAll((Filter f) => f.Instance == filter);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000EB40 File Offset: 0x0000CD40
		private static void ValidateFilterInstance(object instance)
		{
			if (instance != null && !(instance is IActionFilter) && !(instance is IAuthorizationFilter) && !(instance is IExceptionFilter) && !(instance is IResultFilter) && !(instance is IAuthenticationFilter))
			{
				throw Error.InvalidOperation(MvcResources.GlobalFilterCollection_UnsupportedFilterInstance, new object[]
				{
					typeof(IAuthorizationFilter).FullName,
					typeof(IActionFilter).FullName,
					typeof(IResultFilter).FullName,
					typeof(IExceptionFilter).FullName,
					typeof(IAuthenticationFilter).FullName
				});
			}
		}

		// Token: 0x0400016F RID: 367
		private List<Filter> _filters = new List<Filter>();
	}
}
