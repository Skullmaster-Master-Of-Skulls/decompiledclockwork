using System;
using System.CodeDom.Compiler;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200031C RID: 796
	internal static class Utils
	{
		// Token: 0x06002F18 RID: 12056 RVA: 0x000B271E File Offset: 0x000B091E
		internal static void ExtractNamespaceAndName(SchemaDataModelOption dataModel, string qualifiedTypeName, out string namespaceName, out string name)
		{
			Utils.GetBeforeAndAfterLastPeriod(qualifiedTypeName, out namespaceName, out name);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000B2728 File Offset: 0x000B0928
		internal static string ExtractTypeName(SchemaDataModelOption dataModel, string qualifiedTypeName)
		{
			return Utils.GetEverythingAfterLastPeriod(qualifiedTypeName);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000B2730 File Offset: 0x000B0930
		private static void GetBeforeAndAfterLastPeriod(string qualifiedTypeName, out string before, out string after)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				before = null;
				after = qualifiedTypeName;
				return;
			}
			before = qualifiedTypeName.Substring(0, num);
			after = qualifiedTypeName.Substring(num + 1);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000B2768 File Offset: 0x000B0968
		internal static string GetEverythingBeforeLastPeriod(string qualifiedTypeName)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				return null;
			}
			return qualifiedTypeName.Substring(0, num);
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000B278C File Offset: 0x000B098C
		private static string GetEverythingAfterLastPeriod(string qualifiedTypeName)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				return qualifiedTypeName;
			}
			return qualifiedTypeName.Substring(num + 1);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000B27B1 File Offset: 0x000B09B1
		public static bool GetString(Schema schema, XmlReader reader, out string value)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				value = null;
				return false;
			}
			value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(value, reader.Name));
				return false;
			}
			return true;
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000B27F1 File Offset: 0x000B09F1
		public static bool GetDottedName(Schema schema, XmlReader reader, out string name)
		{
			return Utils.GetString(schema, reader, out name) && Utils.ValidateDottedName(schema, reader, name);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000B2808 File Offset: 0x000B0A08
		internal static bool ValidateDottedName(Schema schema, XmlReader reader, string name)
		{
			if (schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				foreach (string name2 in name.Split(new char[]
				{
					'.'
				}))
				{
					if (!Utils.ValidUndottedName(name2))
					{
						schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(name, reader.Name));
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000B2864 File Offset: 0x000B0A64
		public static bool GetUndottedName(Schema schema, XmlReader reader, out string name)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				name = null;
				return false;
			}
			name = reader.Value;
			if (string.IsNullOrEmpty(name))
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.EmptyName(reader.Name));
				return false;
			}
			if (schema.DataModel == SchemaDataModelOption.EntityDataModel && !Utils.ValidUndottedName(name))
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(name, reader.Name));
				return false;
			}
			return true;
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000B28D7 File Offset: 0x000B0AD7
		internal static bool ValidUndottedName(string name)
		{
			return !string.IsNullOrEmpty(name) && Utils.UndottedNameValidator.IsMatch(name) && Utils.IsValidLanguageIndependentIdentifier(name);
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000B28F6 File Offset: 0x000B0AF6
		[SecuritySafeCritical]
		private static bool IsValidLanguageIndependentIdentifier(string name)
		{
			return CodeGenerator.IsValidLanguageIndependentIdentifier(name);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000B2900 File Offset: 0x000B0B00
		public static bool GetBool(Schema schema, XmlReader reader, out bool value)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				value = true;
				return false;
			}
			try
			{
				value = reader.ReadContentAsBoolean();
				return true;
			}
			catch (XmlException)
			{
				schema.AddError(ErrorCode.BoolValueExpected, EdmSchemaErrorSeverity.Error, reader, Strings.ValueNotUnderstood(reader.Value, reader.Name));
			}
			value = true;
			return false;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000B2964 File Offset: 0x000B0B64
		public static bool GetInt(Schema schema, XmlReader reader, out int value)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				value = 0;
				return false;
			}
			string value2 = reader.Value;
			value = int.MinValue;
			if (int.TryParse(value2, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
			{
				return true;
			}
			schema.AddError(ErrorCode.IntegerExpected, EdmSchemaErrorSeverity.Error, reader, Strings.ValueNotUnderstood(reader.Value, reader.Name));
			return false;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000B29C0 File Offset: 0x000B0BC0
		public static bool GetByte(Schema schema, XmlReader reader, out byte value)
		{
			if (reader.SchemaInfo.Validity == XmlSchemaValidity.Invalid)
			{
				value = 0;
				return false;
			}
			string value2 = reader.Value;
			value = 0;
			if (byte.TryParse(value2, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
			{
				return true;
			}
			schema.AddError(ErrorCode.ByteValueExpected, EdmSchemaErrorSeverity.Error, reader, Strings.ValueNotUnderstood(reader.Value, reader.Name));
			return false;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000B2A17 File Offset: 0x000B0C17
		public static int CompareNames(string lhsName, string rhsName)
		{
			return string.Compare(lhsName, rhsName, StringComparison.Ordinal);
		}

		// Token: 0x04001452 RID: 5202
		private const string StartCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}]";

		// Token: 0x04001453 RID: 5203
		private const string OtherCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]";

		// Token: 0x04001454 RID: 5204
		private const string NameExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x04001455 RID: 5205
		private static Regex UndottedNameValidator = new Regex("^[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}$", RegexOptions.Compiled | RegexOptions.Singleline);
	}
}
