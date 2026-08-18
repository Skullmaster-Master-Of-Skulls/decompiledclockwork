using System;
using System.Collections.Generic;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001AE RID: 430
	internal class SafeLink<TParent> where TParent : class
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0006C7E6 File Offset: 0x0006A9E6
		public TParent Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0006C7F0 File Offset: 0x0006A9F0
		internal static IEnumerable<TChild> BindChildren<TChild>(TParent parent, Func<TChild, SafeLink<TParent>> getLink, IEnumerable<TChild> children)
		{
			foreach (TChild child in children)
			{
				SafeLink<TParent>.BindChild<TChild>(parent, getLink, child);
			}
			return children;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0006C83C File Offset: 0x0006AA3C
		internal static TChild BindChild<TChild>(TParent parent, Func<TChild, SafeLink<TParent>> getLink, TChild child)
		{
			SafeLink<TParent> safeLink = getLink(child);
			safeLink._value = parent;
			return child;
		}

		// Token: 0x04000CE6 RID: 3302
		private TParent _value;
	}
}
