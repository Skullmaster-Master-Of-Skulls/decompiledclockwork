using System;
using System.Collections.Specialized;
using System.Web.Caching;

namespace System.Web.UI
{
	// Token: 0x02000289 RID: 649
	internal sealed class FileDataSourceCache : DataSourceCache
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x00062165 File Offset: 0x00060365
		public StringCollection FileDependencies
		{
			get
			{
				if (this._fileDependencies == null)
				{
					this._fileDependencies = new StringCollection();
				}
				return this._fileDependencies;
			}
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00062180 File Offset: 0x00060380
		protected override void SaveDataToCacheInternal(string key, object data, CacheDependency dependency)
		{
			int count = this.FileDependencies.Count;
			string[] array = new string[count];
			this.FileDependencies.CopyTo(array, 0);
			CacheDependency cacheDependency = new CacheDependency(0, array);
			if (dependency != null)
			{
				AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
				aggregateCacheDependency.Add(new CacheDependency[]
				{
					cacheDependency,
					dependency
				});
				dependency = aggregateCacheDependency;
			}
			else
			{
				dependency = cacheDependency;
			}
			base.SaveDataToCacheInternal(key, data, dependency);
		}

		// Token: 0x040019A1 RID: 6561
		private StringCollection _fileDependencies;
	}
}
