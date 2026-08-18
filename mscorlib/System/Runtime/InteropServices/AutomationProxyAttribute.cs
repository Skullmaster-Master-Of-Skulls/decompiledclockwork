using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000500 RID: 1280
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	public sealed class AutomationProxyAttribute : Attribute
	{
		// Token: 0x06003195 RID: 12693 RVA: 0x000A988D File Offset: 0x000A888D
		public AutomationProxyAttribute(bool val)
		{
			this._val = val;
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x000A989C File Offset: 0x000A889C
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040019A2 RID: 6562
		internal bool _val;
	}
}
