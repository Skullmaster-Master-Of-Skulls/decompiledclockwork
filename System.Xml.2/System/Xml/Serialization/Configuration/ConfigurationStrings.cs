using System;
using System.Globalization;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x020001CC RID: 460
	internal static class ConfigurationStrings
	{
		// Token: 0x06001F4B RID: 8011 RVA: 0x000AA1EC File Offset: 0x000A83EC
		private static string GetSectionPath(string sectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				"system.xml.serialization",
				sectionName
			});
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000AA20F File Offset: 0x000A840F
		internal static string SchemaImporterExtensionsSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("schemaImporterExtensions");
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000AA21B File Offset: 0x000A841B
		internal static string DateTimeSerializationSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("dateTimeSerialization");
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x000AA227 File Offset: 0x000A8427
		internal static string XmlSerializerSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("xmlSerializer");
			}
		}

		// Token: 0x04000D19 RID: 3353
		internal const string Name = "name";

		// Token: 0x04000D1A RID: 3354
		internal const string SchemaImporterExtensionsSectionName = "schemaImporterExtensions";

		// Token: 0x04000D1B RID: 3355
		internal const string DateTimeSerializationSectionName = "dateTimeSerialization";

		// Token: 0x04000D1C RID: 3356
		internal const string XmlSerializerSectionName = "xmlSerializer";

		// Token: 0x04000D1D RID: 3357
		internal const string SectionGroupName = "system.xml.serialization";

		// Token: 0x04000D1E RID: 3358
		internal const string SqlTypesSchemaImporterChar = "SqlTypesSchemaImporterChar";

		// Token: 0x04000D1F RID: 3359
		internal const string SqlTypesSchemaImporterNChar = "SqlTypesSchemaImporterNChar";

		// Token: 0x04000D20 RID: 3360
		internal const string SqlTypesSchemaImporterVarChar = "SqlTypesSchemaImporterVarChar";

		// Token: 0x04000D21 RID: 3361
		internal const string SqlTypesSchemaImporterNVarChar = "SqlTypesSchemaImporterNVarChar";

		// Token: 0x04000D22 RID: 3362
		internal const string SqlTypesSchemaImporterText = "SqlTypesSchemaImporterText";

		// Token: 0x04000D23 RID: 3363
		internal const string SqlTypesSchemaImporterNText = "SqlTypesSchemaImporterNText";

		// Token: 0x04000D24 RID: 3364
		internal const string SqlTypesSchemaImporterVarBinary = "SqlTypesSchemaImporterVarBinary";

		// Token: 0x04000D25 RID: 3365
		internal const string SqlTypesSchemaImporterBinary = "SqlTypesSchemaImporterBinary";

		// Token: 0x04000D26 RID: 3366
		internal const string SqlTypesSchemaImporterImage = "SqlTypesSchemaImporterImage";

		// Token: 0x04000D27 RID: 3367
		internal const string SqlTypesSchemaImporterDecimal = "SqlTypesSchemaImporterDecimal";

		// Token: 0x04000D28 RID: 3368
		internal const string SqlTypesSchemaImporterNumeric = "SqlTypesSchemaImporterNumeric";

		// Token: 0x04000D29 RID: 3369
		internal const string SqlTypesSchemaImporterBigInt = "SqlTypesSchemaImporterBigInt";

		// Token: 0x04000D2A RID: 3370
		internal const string SqlTypesSchemaImporterInt = "SqlTypesSchemaImporterInt";

		// Token: 0x04000D2B RID: 3371
		internal const string SqlTypesSchemaImporterSmallInt = "SqlTypesSchemaImporterSmallInt";

		// Token: 0x04000D2C RID: 3372
		internal const string SqlTypesSchemaImporterTinyInt = "SqlTypesSchemaImporterTinyInt";

		// Token: 0x04000D2D RID: 3373
		internal const string SqlTypesSchemaImporterBit = "SqlTypesSchemaImporterBit";

		// Token: 0x04000D2E RID: 3374
		internal const string SqlTypesSchemaImporterFloat = "SqlTypesSchemaImporterFloat";

		// Token: 0x04000D2F RID: 3375
		internal const string SqlTypesSchemaImporterReal = "SqlTypesSchemaImporterReal";

		// Token: 0x04000D30 RID: 3376
		internal const string SqlTypesSchemaImporterDateTime = "SqlTypesSchemaImporterDateTime";

		// Token: 0x04000D31 RID: 3377
		internal const string SqlTypesSchemaImporterSmallDateTime = "SqlTypesSchemaImporterSmallDateTime";

		// Token: 0x04000D32 RID: 3378
		internal const string SqlTypesSchemaImporterMoney = "SqlTypesSchemaImporterMoney";

		// Token: 0x04000D33 RID: 3379
		internal const string SqlTypesSchemaImporterSmallMoney = "SqlTypesSchemaImporterSmallMoney";

		// Token: 0x04000D34 RID: 3380
		internal const string SqlTypesSchemaImporterUniqueIdentifier = "SqlTypesSchemaImporterUniqueIdentifier";

		// Token: 0x04000D35 RID: 3381
		internal const string Type = "type";

		// Token: 0x04000D36 RID: 3382
		internal const string Mode = "mode";

		// Token: 0x04000D37 RID: 3383
		internal const string CheckDeserializeAdvances = "checkDeserializeAdvances";

		// Token: 0x04000D38 RID: 3384
		internal const string TempFilesLocation = "tempFilesLocation";

		// Token: 0x04000D39 RID: 3385
		internal const string UseLegacySerializerGeneration = "useLegacySerializerGeneration";
	}
}
