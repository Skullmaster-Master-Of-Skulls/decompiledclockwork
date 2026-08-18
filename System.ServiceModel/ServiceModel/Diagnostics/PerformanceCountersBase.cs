using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A8B RID: 2699
	internal abstract class PerformanceCountersBase : IDisposable
	{
		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06006AAC RID: 27308
		internal abstract string InstanceName { get; }

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06006AAD RID: 27309
		internal abstract string[] CounterNames { get; }

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06006AAE RID: 27310
		internal abstract int PerfCounterStart { get; }

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x06006AAF RID: 27311
		internal abstract int PerfCounterEnd { get; }

		// Token: 0x06006AB0 RID: 27312 RVA: 0x0018DDD4 File Offset: 0x0018BFD4
		private static string GetInstanceNameWithHash(string instanceName, string fullInstanceName)
		{
			return string.Format("{0}{1}", instanceName, StringUtil.GetNonRandomizedHashCode(fullInstanceName).ToString("X", CultureInfo.InvariantCulture));
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x0018DE04 File Offset: 0x0018C004
		protected static string EnsureUniqueInstanceName(string categoryName, string instanceName, string fullInstanceName)
		{
			if (string.IsNullOrEmpty(categoryName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("categoryName");
			}
			if (string.IsNullOrEmpty(instanceName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("instanceName");
			}
			if (string.IsNullOrEmpty(fullInstanceName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("fullInstanceName");
			}
			try
			{
				if (PerformanceCounterCategory.InstanceExists(instanceName, categoryName))
				{
					return PerformanceCountersBase.GetInstanceNameWithHash(instanceName, fullInstanceName);
				}
			}
			catch
			{
			}
			return instanceName;
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x0018DE88 File Offset: 0x0018C088
		protected static string GetUniqueInstanceName(string categoryName, string instanceName, string fullInstanceName)
		{
			if (string.IsNullOrEmpty(categoryName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("categoryName");
			}
			if (string.IsNullOrEmpty(instanceName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("instanceName");
			}
			if (string.IsNullOrEmpty(fullInstanceName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("fullInstanceName");
			}
			try
			{
				string instanceNameWithHash = PerformanceCountersBase.GetInstanceNameWithHash(instanceName, fullInstanceName);
				if (PerformanceCounterCategory.InstanceExists(instanceNameWithHash, categoryName))
				{
					return instanceNameWithHash;
				}
			}
			catch
			{
			}
			return instanceName;
		}

		// Token: 0x06006AB3 RID: 27315 RVA: 0x0018DF0C File Offset: 0x0018C10C
		protected static string GetHashedString(string str, int startIndex, int count, bool hashAtEnd)
		{
			string text = str.Remove(startIndex, count);
			string text2 = ((uint)(StringUtil.GetNonRandomizedHashCode(str) % 99)).ToString("00", CultureInfo.InvariantCulture);
			if (!hashAtEnd)
			{
				return text2 + text;
			}
			return text + text2;
		}

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x06006AB4 RID: 27316
		internal abstract bool Initialized { get; }

		// Token: 0x06006AB5 RID: 27317 RVA: 0x0018DF50 File Offset: 0x0018C150
		public void Dispose()
		{
			if (Interlocked.Exchange(ref this.disposed, 1) == 0)
			{
				this.Dispose(true);
			}
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x0018DF67 File Offset: 0x0018C167
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x04003CBF RID: 15551
		protected int disposed;

		// Token: 0x02000EAA RID: 3754
		protected class CounterSetInstanceCache
		{
			// Token: 0x06008435 RID: 33845 RVA: 0x001E8898 File Offset: 0x001E6A98
			internal CounterSetInstance Get(string instanceName)
			{
				Dictionary<string, WeakReference> obj = this.cache;
				CounterSetInstance result;
				lock (obj)
				{
					WeakReference weakReference;
					if (this.cache.TryGetValue(instanceName, out weakReference))
					{
						this.cache.Remove(instanceName);
						result = (CounterSetInstance)weakReference.Target;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}

			// Token: 0x06008436 RID: 33846 RVA: 0x001E8900 File Offset: 0x001E6B00
			internal void Add(string instanceName, CounterSetInstance instance)
			{
				Dictionary<string, WeakReference> obj = this.cache;
				lock (obj)
				{
					this.cache[instanceName] = new WeakReference(instance);
				}
			}

			// Token: 0x06008437 RID: 33847 RVA: 0x001E894C File Offset: 0x001E6B4C
			internal void Cleanup()
			{
				Dictionary<string, WeakReference> obj = this.cache;
				lock (obj)
				{
					foreach (KeyValuePair<string, WeakReference> keyValuePair in (from pair in this.cache
					where !pair.Value.IsAlive
					select pair).ToList<KeyValuePair<string, WeakReference>>())
					{
						this.cache.Remove(keyValuePair.Key);
					}
				}
			}

			// Token: 0x04004C3B RID: 19515
			private readonly Dictionary<string, WeakReference> cache = new Dictionary<string, WeakReference>();
		}
	}
}
