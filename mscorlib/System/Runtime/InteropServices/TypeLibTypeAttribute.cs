using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004F0 RID: 1264
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
	public sealed class TypeLibTypeAttribute : Attribute
	{
		// Token: 0x06003161 RID: 12641 RVA: 0x000A925B File Offset: 0x000A825B
		public TypeLibTypeAttribute(TypeLibTypeFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000A926A File Offset: 0x000A826A
		public TypeLibTypeAttribute(short flags)
		{
			this._val = (TypeLibTypeFlags)flags;
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000A9279 File Offset: 0x000A8279
		public TypeLibTypeFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001933 RID: 6451
		internal TypeLibTypeFlags _val;
	}
}
