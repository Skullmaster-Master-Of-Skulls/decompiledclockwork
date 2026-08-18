using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004E2 RID: 1250
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	public sealed class ComVisibleAttribute : Attribute
	{
		// Token: 0x0600314B RID: 12619 RVA: 0x000A909F File Offset: 0x000A809F
		public ComVisibleAttribute(bool visibility)
		{
			this._val = visibility;
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000A90AE File Offset: 0x000A80AE
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040018FD RID: 6397
		internal bool _val;
	}
}
