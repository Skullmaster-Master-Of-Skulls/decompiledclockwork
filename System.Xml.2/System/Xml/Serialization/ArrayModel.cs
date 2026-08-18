using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200015C RID: 348
	internal class ArrayModel : TypeModel
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x00068D28 File Offset: 0x00066F28
		internal ArrayModel(Type type, TypeDesc typeDesc, ModelScope scope) : base(type, typeDesc, scope)
		{
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00068D33 File Offset: 0x00066F33
		internal TypeModel Element
		{
			get
			{
				return base.ModelScope.GetTypeModel(TypeScope.GetArrayElementType(base.Type, null));
			}
		}
	}
}
