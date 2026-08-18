using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D30 RID: 3376
	internal static class SchemaValidatorFactory
	{
		// Token: 0x06007DA1 RID: 32161 RVA: 0x001CBD74 File Offset: 0x001C9F74
		static SchemaValidatorFactory()
		{
			SchemaValidatorFactory.validatorMappings[typeof(HierarchySchemaElement)] = new HierarchySchemaElementValidator();
			SchemaValidatorFactory.validatorMappings[typeof(KpiSchemaElement)] = new KpiSchemaElementValidator();
			SchemaValidatorFactory.validatorMappings[typeof(LevelSchemaElement)] = new LevelSchemaElementValidator();
			SchemaValidatorFactory.validatorMappings[typeof(NamedSetSchemaElement)] = new NamedSetSchemaElementValidator();
			SchemaValidatorFactory.validatorMappings[typeof(DimensionSchemaElement)] = new UniqueSchemaElementValidator();
			SchemaValidatorFactory.validatorMappings[typeof(MeasureSchemaElement)] = new UniqueSchemaElementValidator();
			SchemaValidatorFactory.defaultValidator = new SchemaElementValidator();
		}

		// Token: 0x06007DA2 RID: 32162 RVA: 0x001CBE2C File Offset: 0x001CA02C
		public static SchemaElementValidator GetValidatorForType(Type schemaElementType)
		{
			SchemaElementValidator validatorForNonNullType = SchemaValidatorFactory.defaultValidator;
			if (schemaElementType != null)
			{
				validatorForNonNullType = SchemaValidatorFactory.GetValidatorForNonNullType(schemaElementType);
			}
			return validatorForNonNullType;
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x001CBE50 File Offset: 0x001CA050
		private static SchemaElementValidator GetValidatorForNonNullType(Type schemaElementType)
		{
			if (SchemaValidatorFactory.validatorMappings.ContainsKey(schemaElementType))
			{
				return SchemaValidatorFactory.validatorMappings[schemaElementType];
			}
			return SchemaValidatorFactory.defaultValidator;
		}

		// Token: 0x04002285 RID: 8837
		private static Dictionary<Type, SchemaElementValidator> validatorMappings = new Dictionary<Type, SchemaElementValidator>();

		// Token: 0x04002286 RID: 8838
		private static SchemaElementValidator defaultValidator;
	}
}
