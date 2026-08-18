using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FF RID: 1279
	internal class SafeLink<TParent> where TParent : class
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000E48D6 File Offset: 0x000E2AD6
		public TParent Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000E48E0 File Offset: 0x000E2AE0
		internal static IEnumerable<TChild> BindChildren<TChild>(TParent parent, Func<TChild, SafeLink<TParent>> getLink, IEnumerable<TChild> children)
		{
			foreach (TChild child in children)
			{
				SafeLink<TParent>.BindChild<TChild>(parent, getLink, child);
			}
			return children;
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000E492C File Offset: 0x000E2B2C
		internal static TChild BindChild<TChild>(TParent parent, Func<TChild, SafeLink<TParent>> getLink, TChild child)
		{
			SafeLink<TParent> safeLink = getLink(child);
			safeLink._value = parent;
			return child;
		}

		// Token: 0x0400122C RID: 4652
		private TParent _value;
	}
}
