using System;
using System.Collections;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000B2 RID: 178
	internal struct ResponseDependencyList
	{
		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001F2C4 File Offset: 0x0001D4C4
		internal void AddDependency(string item, string argname)
		{
			if (item == null)
			{
				throw new ArgumentNullException(argname);
			}
			this._dependencyArray = null;
			if (this._dependencies == null)
			{
				this._dependencies = new ArrayList(1);
			}
			DateTime utcNow = DateTime.UtcNow;
			this._dependencies.Add(new ResponseDependencyInfo(new string[]
			{
				item
			}, utcNow));
			if (this._oldestDependency == DateTime.MinValue || utcNow < this._oldestDependency)
			{
				this._oldestDependency = utcNow;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001F340 File Offset: 0x0001D540
		internal void AddDependencies(ArrayList items, string argname)
		{
			if (items == null)
			{
				throw new ArgumentNullException(argname);
			}
			string[] items2 = (string[])items.ToArray(typeof(string));
			this.AddDependencies(items2, argname, false);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001F376 File Offset: 0x0001D576
		internal void AddDependencies(string[] items, string argname)
		{
			this.AddDependencies(items, argname, true);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001F381 File Offset: 0x0001D581
		internal void AddDependencies(string[] items, string argname, bool cloneArray)
		{
			this.AddDependencies(items, argname, cloneArray, DateTime.UtcNow);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0001F391 File Offset: 0x0001D591
		internal void AddDependencies(string[] items, string argname, bool cloneArray, string requestVirtualPath)
		{
			if (requestVirtualPath == null)
			{
				throw new ArgumentNullException("requestVirtualPath");
			}
			this._requestVirtualPath = requestVirtualPath;
			this.AddDependencies(items, argname, cloneArray, DateTime.UtcNow);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
		internal void AddDependencies(string[] items, string argname, bool cloneArray, DateTime utcDepTime)
		{
			if (items == null)
			{
				throw new ArgumentNullException(argname);
			}
			string[] array;
			if (cloneArray)
			{
				array = (string[])items.Clone();
			}
			else
			{
				array = items;
			}
			foreach (string value in array)
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException(argname);
				}
			}
			this._dependencyArray = null;
			if (this._dependencies == null)
			{
				this._dependencies = new ArrayList(1);
			}
			this._dependencies.Add(new ResponseDependencyInfo(array, utcDepTime));
			if (this._oldestDependency == DateTime.MinValue || utcDepTime < this._oldestDependency)
			{
				this._oldestDependency = utcDepTime;
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0001F45D File Offset: 0x0001D65D
		internal bool HasDependencies()
		{
			return this._dependencyArray != null || this._dependencies != null;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001F474 File Offset: 0x0001D674
		internal string[] GetDependencies()
		{
			if (this._dependencyArray == null && this._dependencies != null)
			{
				int num = 0;
				foreach (object obj in this._dependencies)
				{
					ResponseDependencyInfo responseDependencyInfo = (ResponseDependencyInfo)obj;
					num += responseDependencyInfo.items.Length;
				}
				this._dependencyArray = new string[num];
				int num2 = 0;
				foreach (object obj2 in this._dependencies)
				{
					ResponseDependencyInfo responseDependencyInfo2 = (ResponseDependencyInfo)obj2;
					int num3 = responseDependencyInfo2.items.Length;
					Array.Copy(responseDependencyInfo2.items, 0, this._dependencyArray, num2, num3);
					num2 += num3;
				}
			}
			return this._dependencyArray;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001F570 File Offset: 0x0001D770
		internal CacheDependency CreateCacheDependency(CacheDependencyType dependencyType, CacheDependency dependency)
		{
			if (this._dependencies != null)
			{
				if (dependencyType == CacheDependencyType.Files || dependencyType == CacheDependencyType.CacheItems)
				{
					using (IEnumerator enumerator = this._dependencies.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							ResponseDependencyInfo responseDependencyInfo = (ResponseDependencyInfo)obj;
							using (CacheDependency cacheDependency = dependency)
							{
								if (dependencyType == CacheDependencyType.Files)
								{
									dependency = new CacheDependency(0, responseDependencyInfo.items, null, cacheDependency, responseDependencyInfo.utcDate);
								}
								else
								{
									dependency = new CacheDependency(null, responseDependencyInfo.items, cacheDependency, DateTimeUtil.ConvertToLocalTime(responseDependencyInfo.utcDate));
								}
							}
						}
						return dependency;
					}
				}
				CacheDependency cacheDependency2 = null;
				VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
				if (virtualPathProvider != null && this._requestVirtualPath != null)
				{
					cacheDependency2 = virtualPathProvider.GetCacheDependency(this._requestVirtualPath, this.GetDependencies(), this._oldestDependency);
				}
				if (cacheDependency2 != null)
				{
					AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
					aggregateCacheDependency.Add(new CacheDependency[]
					{
						cacheDependency2
					});
					if (dependency != null)
					{
						aggregateCacheDependency.Add(new CacheDependency[]
						{
							dependency
						});
					}
					dependency = aggregateCacheDependency;
				}
			}
			return dependency;
		}

		// Token: 0x0400046B RID: 1131
		private ArrayList _dependencies;

		// Token: 0x0400046C RID: 1132
		private string[] _dependencyArray;

		// Token: 0x0400046D RID: 1133
		private DateTime _oldestDependency;

		// Token: 0x0400046E RID: 1134
		private string _requestVirtualPath;
	}
}
