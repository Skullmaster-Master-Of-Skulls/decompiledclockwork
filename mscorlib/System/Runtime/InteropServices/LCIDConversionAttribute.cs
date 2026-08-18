using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004E4 RID: 1252
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		// Token: 0x0600314F RID: 12623 RVA: 0x000A90D2 File Offset: 0x000A80D2
		public LCIDConversionAttribute(int lcid)
		{
			this._val = lcid;
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000A90E1 File Offset: 0x000A80E1
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040018FF RID: 6399
		internal int _val;
	}
}
