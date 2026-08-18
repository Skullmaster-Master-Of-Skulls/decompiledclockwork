using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000141 RID: 321
	internal class XMLSchema
	{
		// Token: 0x060012FA RID: 4858 RVA: 0x000946D0 File Offset: 0x00093AD0
		internal static TypeConverter GetConverter(Type type)
		{
			CodeAccessPermission codeAccessPermission = (CodeAccessPermission)new HostProtectionAttribute
			{
				SharedState = true
			}.CreatePermission();
			codeAccessPermission.Assert();
			TypeConverter converter;
			try
			{
				converter = TypeDescriptor.GetConverter(type);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return converter;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00094728 File Offset: 0x00093B28
		internal static void SetProperties(object instance, XmlAttributeCollection attrs)
		{
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
				{
					string localName = attrs[i].LocalName;
					string value = attrs[i].Value;
					if (!(localName == "DefaultValue") && !(localName == "RemotingFormat") && (!(localName == "Expression") || !(instance is DataColumn)))
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(instance)[localName];
						if (propertyDescriptor != null)
						{
							Type propertyType = propertyDescriptor.PropertyType;
							TypeConverter converter = XMLSchema.GetConverter(propertyType);
							object value2;
							if (converter.CanConvertFrom(typeof(string)))
							{
								value2 = converter.ConvertFromString(value);
							}
							else if (propertyType == typeof(Type))
							{
								value2 = DataStorage.GetType(value);
							}
							else
							{
								if (!(propertyType == typeof(CultureInfo)))
								{
									throw ExceptionBuilder.CannotConvert(value, propertyType.FullName);
								}
								value2 = new CultureInfo(value);
							}
							propertyDescriptor.SetValue(instance, value2);
						}
					}
				}
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0009484C File Offset: 0x00093C4C
		internal static bool FEqualIdentity(XmlNode node, string name, string ns)
		{
			return node != null && node.LocalName == name && node.NamespaceURI == ns;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0009487C File Offset: 0x00093C7C
		internal static bool GetBooleanAttribute(XmlElement element, string attrName, string attrNS, bool defVal)
		{
			string attribute = element.GetAttribute(attrName, attrNS);
			if (attribute == null || attribute.Length == 0)
			{
				return defVal;
			}
			if (attribute == "true" || attribute == "1")
			{
				return true;
			}
			if (attribute == "false" || attribute == "0")
			{
				return false;
			}
			throw ExceptionBuilder.InvalidAttributeValue(attrName, attribute);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000948E0 File Offset: 0x00093CE0
		internal static string GenUniqueColumnName(string proposedName, DataTable table)
		{
			if (table.Columns.IndexOf(proposedName) >= 0)
			{
				for (int i = 0; i <= table.Columns.Count; i++)
				{
					string text = proposedName + "_" + i.ToString(CultureInfo.InvariantCulture);
					if (table.Columns.IndexOf(text) < 0)
					{
						return text;
					}
				}
			}
			return proposedName;
		}
	}
}
