using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004F7 RID: 1271
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class GuidAttribute : Attribute
	{
		// Token: 0x06003176 RID: 12662 RVA: 0x000A9485 File Offset: 0x000A8485
		public GuidAttribute(string guid)
		{
			this._val = guid;
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x000A9494 File Offset: 0x000A8494
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001991 RID: 6545
		internal string _val;
	}
}
