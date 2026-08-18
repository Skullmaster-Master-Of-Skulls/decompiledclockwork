using System;
using System.Web.Caching;

namespace System.Web.UI
{
	// Token: 0x02000465 RID: 1125
	internal sealed class SqlDataSourceCache : DataSourceCache
	{
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x000E6564 File Offset: 0x000E5564
		// (set) Token: 0x06003533 RID: 13619 RVA: 0x000E6591 File Offset: 0x000E5591
		public string SqlCacheDependency
		{
			get
			{
				object obj = base.ViewState["SqlCacheDependency"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["SqlCacheDependency"] = value;
			}
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000E65A4 File Offset: 0x000E55A4
		protected override void SaveDataToCacheInternal(string key, object data, CacheDependency dependency)
		{
			string sqlCacheDependency = this.SqlCacheDependency;
			if (sqlCacheDependency.Length > 0 && !string.Equals(sqlCacheDependency, "CommandNotification", StringComparison.OrdinalIgnoreCase))
			{
				CacheDependency cacheDependency = System.Web.Caching.SqlCacheDependency.CreateOutputCacheDependency(sqlCacheDependency);
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
			}
			base.SaveDataToCacheInternal(key, data, dependency);
		}

		// Token: 0x04002529 RID: 9513
		internal const string Sql9CacheDependencyDirective = "CommandNotification";
	}
}
