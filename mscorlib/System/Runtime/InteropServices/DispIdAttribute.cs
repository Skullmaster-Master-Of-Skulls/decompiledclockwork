using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004DC RID: 1244
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	[ComVisible(true)]
	public sealed class DispIdAttribute : Attribute
	{
		// Token: 0x06003141 RID: 12609 RVA: 0x000A9025 File Offset: 0x000A8025
		public DispIdAttribute(int dispId)
		{
			this._val = dispId;
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000A9034 File Offset: 0x000A8034
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040018F1 RID: 6385
		internal int _val;
	}
}
