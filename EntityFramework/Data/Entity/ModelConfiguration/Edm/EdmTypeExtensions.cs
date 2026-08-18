using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x020002C8 RID: 712
	internal static class EdmTypeExtensions
	{
		// Token: 0x06001937 RID: 6455 RVA: 0x0007CD28 File Offset: 0x0007AF28
		public static Type GetClrType(this EdmType item)
		{
			EntityType entityType = item as EntityType;
			if (entityType != null)
			{
				return entityType.GetClrType();
			}
			EnumType enumType = item as EnumType;
			if (enumType != null)
			{
				return enumType.GetClrType();
			}
			ComplexType complexType = item as ComplexType;
			if (complexType != null)
			{
				return complexType.GetClrType();
			}
			return null;
		}
	}
}
