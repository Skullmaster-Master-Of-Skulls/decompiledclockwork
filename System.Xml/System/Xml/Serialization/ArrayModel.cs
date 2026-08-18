using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002D7 RID: 727
	internal class ArrayModel : TypeModel
	{
		// Token: 0x0600224D RID: 8781 RVA: 0x000A09BC File Offset: 0x0009F9BC
		internal ArrayModel(Type type, TypeDesc typeDesc, ModelScope scope) : base(type, typeDesc, scope)
		{
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000A09C7 File Offset: 0x0009F9C7
		internal TypeModel Element
		{
			get
			{
				return base.ModelScope.GetTypeModel(TypeScope.GetArrayElementType(base.Type, null));
			}
		}
	}
}
