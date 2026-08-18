using System;
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200006C RID: 108
	public class DependencyResolverTrackerPolicy : IDependencyResolverTrackerPolicy, IBuilderPolicy
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00006934 File Offset: 0x00004B34
		public void AddResolverKey(object key)
		{
			lock (this.keys)
			{
				this.keys.Add(key);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000697C File Offset: 0x00004B7C
		public void RemoveResolvers(IPolicyList policies)
		{
			List<object> list = new List<object>();
			lock (this.keys)
			{
				list.AddRange(this.keys);
				this.keys.Clear();
			}
			foreach (object buildKey in list)
			{
				policies.Clear(buildKey);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006A14 File Offset: 0x00004C14
		public static IDependencyResolverTrackerPolicy GetTracker(IPolicyList policies, object buildKey)
		{
			IDependencyResolverTrackerPolicy dependencyResolverTrackerPolicy = policies.Get(buildKey);
			if (dependencyResolverTrackerPolicy == null)
			{
				dependencyResolverTrackerPolicy = new DependencyResolverTrackerPolicy();
				policies.Set(dependencyResolverTrackerPolicy, buildKey);
			}
			return dependencyResolverTrackerPolicy;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006A3C File Offset: 0x00004C3C
		public static void TrackKey(IPolicyList policies, object buildKey, object resolverKey)
		{
			IDependencyResolverTrackerPolicy tracker = DependencyResolverTrackerPolicy.GetTracker(policies, buildKey);
			tracker.AddResolverKey(resolverKey);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006A58 File Offset: 0x00004C58
		public static void RemoveResolvers(IPolicyList policies, object buildKey)
		{
			IDependencyResolverTrackerPolicy dependencyResolverTrackerPolicy = policies.Get(buildKey);
			if (dependencyResolverTrackerPolicy != null)
			{
				dependencyResolverTrackerPolicy.RemoveResolvers(policies);
			}
		}

		// Token: 0x04000069 RID: 105
		private List<object> keys = new List<object>();
	}
}
