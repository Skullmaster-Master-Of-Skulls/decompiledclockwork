using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200072A RID: 1834
	internal static class EnumTypeExtensions
	{
		// Token: 0x06004B66 RID: 19302 RVA: 0x00161AA1 File Offset: 0x0015FCA1
		public static Type GetClrType(this EnumType enumType)
		{
			return enumType.Annotations.GetClrType();
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x00161AAE File Offset: 0x0015FCAE
		public static void SetClrType(this EnumType enumType, Type type)
		{
			enumType.GetMetadataProperties().SetClrType(type);
		}
	}
}
