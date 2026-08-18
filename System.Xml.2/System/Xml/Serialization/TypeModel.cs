using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200015B RID: 347
	internal abstract class TypeModel
	{
		// Token: 0x060017FB RID: 6139 RVA: 0x00068CF3 File Offset: 0x00066EF3
		protected TypeModel(Type type, TypeDesc typeDesc, ModelScope scope)
		{
			this.scope = scope;
			this.type = type;
			this.typeDesc = typeDesc;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00068D10 File Offset: 0x00066F10
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x00068D18 File Offset: 0x00066F18
		internal ModelScope ModelScope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00068D20 File Offset: 0x00066F20
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
		}

		// Token: 0x04000B19 RID: 2841
		private TypeDesc typeDesc;

		// Token: 0x04000B1A RID: 2842
		private Type type;

		// Token: 0x04000B1B RID: 2843
		private ModelScope scope;
	}
}
