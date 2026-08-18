using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200013E RID: 318
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	[__DynamicallyInvokable]
	public static class CallSiteOps
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x00025890 File Offset: 0x00023A90
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static CallSite<T> CreateMatchmaker<T>(CallSite<T> site) where T : class
		{
			CallSite<T> callSite = site.CreateMatchMaker();
			CallSiteOps.ClearMatch(callSite);
			return callSite;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000258AC File Offset: 0x00023AAC
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static bool SetNotMatched(CallSite site)
		{
			bool match = site._match;
			site._match = false;
			return match;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000258C8 File Offset: 0x00023AC8
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static bool GetMatch(CallSite site)
		{
			return site._match;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000258D0 File Offset: 0x00023AD0
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static void ClearMatch(CallSite site)
		{
			site._match = true;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000258D9 File Offset: 0x00023AD9
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static void AddRule<T>(CallSite<T> site, T rule) where T : class
		{
			site.AddRule(rule);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000258E2 File Offset: 0x00023AE2
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static void UpdateRules<T>(CallSite<T> @this, int matched) where T : class
		{
			if (matched > 1)
			{
				@this.MoveRule(matched);
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000258EF File Offset: 0x00023AEF
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static T[] GetRules<T>(CallSite<T> site) where T : class
		{
			return site.Rules;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000258F7 File Offset: 0x00023AF7
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static RuleCache<T> GetRuleCache<T>(CallSite<T> site) where T : class
		{
			return site.Binder.GetRuleCache<T>();
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00025904 File Offset: 0x00023B04
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static void MoveRule<T>(RuleCache<T> cache, T rule, int i) where T : class
		{
			if (i > 1)
			{
				cache.MoveRule(rule, i);
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00025912 File Offset: 0x00023B12
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static T[] GetCachedRules<T>(RuleCache<T> cache) where T : class
		{
			return cache.GetRules();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002591A File Offset: 0x00023B1A
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public static T Bind<T>(CallSiteBinder binder, CallSite<T> site, object[] args) where T : class
		{
			return binder.BindCore<T>(site, args);
		}
	}
}
