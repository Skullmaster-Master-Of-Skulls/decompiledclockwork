using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x02000939 RID: 2361
	public sealed class AuthorizationRuleCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600551C RID: 21788 RVA: 0x00134844 File Offset: 0x00133844
		internal AuthorizationRuleCollection()
		{
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0013484C File Offset: 0x0013384C
		internal void AddRule(AuthorizationRule rule)
		{
			base.InnerList.Add(rule);
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0013485B File Offset: 0x0013385B
		public void CopyTo(AuthorizationRule[] rules, int index)
		{
			((ICollection)this).CopyTo(rules, index);
		}

		// Token: 0x17000EB2 RID: 3762
		public AuthorizationRule this[int index]
		{
			get
			{
				return base.InnerList[index] as AuthorizationRule;
			}
		}
	}
}
