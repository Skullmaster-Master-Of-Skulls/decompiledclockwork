using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002D6 RID: 726
	internal abstract class TypeModel
	{
		// Token: 0x06002249 RID: 8777 RVA: 0x000A0987 File Offset: 0x0009F987
		protected TypeModel(Type type, TypeDesc typeDesc, ModelScope scope)
		{
			this.scope = scope;
			this.type = type;
			this.typeDesc = typeDesc;
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x000A09A4 File Offset: 0x0009F9A4
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x000A09AC File Offset: 0x0009F9AC
		internal ModelScope ModelScope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x000A09B4 File Offset: 0x0009F9B4
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
		}

		// Token: 0x040014B0 RID: 5296
		private TypeDesc typeDesc;

		// Token: 0x040014B1 RID: 5297
		private Type type;

		// Token: 0x040014B2 RID: 5298
		private ModelScope scope;
	}
}
