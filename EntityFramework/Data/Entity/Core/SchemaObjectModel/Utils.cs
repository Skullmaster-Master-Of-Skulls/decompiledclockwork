using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200039A RID: 922
	internal static class Utils
	{
		// Token: 0x06002163 RID: 8547 RVA: 0x0009D2FA File Offset: 0x0009B4FA
		internal static void ExtractNamespaceAndName(string qualifiedTypeName, out string namespaceName, out string name)
		{
			Utils.GetBeforeAndAfterLastPeriod(qualifiedTypeName, out namespaceName, out name);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0009D304 File Offset: 0x0009B504
		internal static string ExtractTypeName(string qualifiedTypeName)
		{
			return Utils.GetEverythingAfterLastPeriod(qualifiedTypeName);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0009D30C File Offset: 0x0009B50C
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

		// Token: 0x06002166 RID: 8550 RVA: 0x0009D344 File Offset: 0x0009B544
		internal static string GetEverythingBeforeLastPeriod(string qualifiedTypeName)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				return null;
			}
			return qualifiedTypeName.Substring(0, num);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0009D368 File Offset: 0x0009B568
		private static string GetEverythingAfterLastPeriod(string qualifiedTypeName)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				return qualifiedTypeName;
			}
			return qualifiedTypeName.Substring(num + 1);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0009D38D File Offset: 0x0009B58D
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

		// Token: 0x06002169 RID: 8553 RVA: 0x0009D3CD File Offset: 0x0009B5CD
		public static bool GetDottedName(Schema schema, XmlReader reader, out string name)
		{
			return Utils.GetString(schema, reader, out name) && Utils.ValidateDottedName(schema, reader, name);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0009D3E4 File Offset: 0x0009B5E4
		internal static bool ValidateDottedName(Schema schema, XmlReader reader, string name)
		{
			if (schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				foreach (string name2 in name.Split(new char[]
				{
					'.'
				}))
				{
					if (!name2.IsValidUndottedName())
					{
						schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(name, reader.Name));
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0009D44C File Offset: 0x0009B64C
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
			if (schema.DataModel == SchemaDataModelOption.EntityDataModel && !name.IsValidUndottedName())
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(name, reader.Name));
				return false;
			}
			return true;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0009D4C0 File Offset: 0x0009B6C0
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

		// Token: 0x0600216D RID: 8557 RVA: 0x0009D524 File Offset: 0x0009B724
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

		// Token: 0x0600216E RID: 8558 RVA: 0x0009D580 File Offset: 0x0009B780
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

		// Token: 0x0600216F RID: 8559 RVA: 0x0009D5D7 File Offset: 0x0009B7D7
		public static int CompareNames(string lhsName, string rhsName)
		{
			return string.Compare(lhsName, rhsName, StringComparison.Ordinal);
		}
	}
}
