using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http.Properties;

namespace System.Web.Http.Filters
{
	// Token: 0x020000EA RID: 234
	public class HttpFilterCollection : IEnumerable<FilterInfo>, IEnumerable
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001315D File Offset: 0x0001135D
		public int Count
		{
			get
			{
				return this._filters.Count;
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001316A File Offset: 0x0001136A
		public void Add(IFilter filter)
		{
			if (filter == null)
			{
				throw Error.ArgumentNull("filter");
			}
			this._filters.Add(HttpFilterCollection.CreateFilterInfo(filter));
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001318C File Offset: 0x0001138C
		public void AddRange(IEnumerable<IFilter> filters)
		{
			if (filters == null)
			{
				throw Error.ArgumentNull("filters");
			}
			IFilter[] array = filters.ToArray<IFilter>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SRResources.CollectionParameterContainsNullElement, new object[]
					{
						"filters"
					}), "filters");
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				this._filters.Add(HttpFilterCollection.CreateFilterInfo(array[j]));
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001320C File Offset: 0x0001140C
		private static FilterInfo CreateFilterInfo(IFilter filter)
		{
			return new FilterInfo(filter, FilterScope.Global);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00013215 File Offset: 0x00011415
		public void Clear()
		{
			this._filters.Clear();
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001323C File Offset: 0x0001143C
		public bool Contains(IFilter filter)
		{
			return this._filters.Any((FilterInfo f) => f.Instance == filter);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001326D File Offset: 0x0001146D
		public IEnumerator<FilterInfo> GetEnumerator()
		{
			return this._filters.GetEnumerator();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001327F File Offset: 0x0001147F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000132A0 File Offset: 0x000114A0
		public void Remove(IFilter filter)
		{
			this._filters.RemoveAll((FilterInfo f) => f.Instance == filter);
		}

		// Token: 0x040001A2 RID: 418
		private readonly List<FilterInfo> _filters = new List<FilterInfo>();
	}
}
