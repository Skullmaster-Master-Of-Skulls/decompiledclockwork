using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004F2 RID: 1266
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVarAttribute : Attribute
	{
		// Token: 0x06003167 RID: 12647 RVA: 0x000A92A7 File Offset: 0x000A82A7
		public TypeLibVarAttribute(TypeLibVarFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000A92B6 File Offset: 0x000A82B6
		public TypeLibVarAttribute(short flags)
		{
			this._val = (TypeLibVarFlags)flags;
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000A92C5 File Offset: 0x000A82C5
		public TypeLibVarFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001935 RID: 6453
		internal TypeLibVarFlags _val;
	}
}
