using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004F1 RID: 1265
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibFuncAttribute : Attribute
	{
		// Token: 0x06003164 RID: 12644 RVA: 0x000A9281 File Offset: 0x000A8281
		public TypeLibFuncAttribute(TypeLibFuncFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000A9290 File Offset: 0x000A8290
		public TypeLibFuncAttribute(short flags)
		{
			this._val = (TypeLibFuncFlags)flags;
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000A929F File Offset: 0x000A829F
		public TypeLibFuncFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001934 RID: 6452
		internal TypeLibFuncFlags _val;
	}
}
