using System;
using System.Web.Caching;

namespace System.Web.UI
{
	// Token: 0x020002FF RID: 767
	internal sealed class SqlDataSourceCache : DataSourceCache
	{
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x00073830 File Offset: 0x00071A30
		// (set) Token: 0x06002365 RID: 9061 RVA: 0x0007385D File Offset: 0x00071A5D
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

		// Token: 0x06002366 RID: 9062 RVA: 0x00073870 File Offset: 0x00071A70
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

		// Token: 0x04001CBF RID: 7359
		internal const string Sql9CacheDependencyDirective = "CommandNotification";
	}
}
