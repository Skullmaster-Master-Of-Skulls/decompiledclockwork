using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200002A RID: 42
	internal static class MetadataItemHelper
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00009FB0 File Offset: 0x000081B0
		public static bool IsInvalid(MetadataItem instance)
		{
			MetadataProperty metadataProperty;
			return instance.MetadataProperties.TryGetValue("EdmSchemaInvalid", false, out metadataProperty) && metadataProperty != null && (bool)metadataProperty.Value;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009FE2 File Offset: 0x000081E2
		public static bool HasSchemaErrors(MetadataItem instance)
		{
			return instance.MetadataProperties.Contains("EdmSchemaErrors");
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009FF4 File Offset: 0x000081F4
		public static IEnumerable<EdmSchemaError> GetSchemaErrors(MetadataItem instance)
		{
			MetadataProperty metadataProperty;
			if (!instance.MetadataProperties.TryGetValue("EdmSchemaErrors", false, out metadataProperty) || metadataProperty == null)
			{
				return Enumerable.Empty<EdmSchemaError>();
			}
			return (IEnumerable<EdmSchemaError>)metadataProperty.Value;
		}

		// Token: 0x040000D8 RID: 216
		internal const string SchemaErrorsMetadataPropertyName = "EdmSchemaErrors";

		// Token: 0x040000D9 RID: 217
		internal const string SchemaInvalidMetadataPropertyName = "EdmSchemaInvalid";
	}
}
