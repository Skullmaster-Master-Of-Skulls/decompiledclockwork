using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004FF RID: 1279
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		// Token: 0x06003193 RID: 12691 RVA: 0x000A9876 File Offset: 0x000A8876
		public ComAliasNameAttribute(string alias)
		{
			this._val = alias;
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000A9885 File Offset: 0x000A8885
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040019A1 RID: 6561
		internal string _val;
	}
}
