using System;
using System.ComponentModel;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000ED RID: 237
	internal class XMLSchema
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x00217D38 File Offset: 0x00217138
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

		// Token: 0x06000DBF RID: 3519 RVA: 0x00217D98 File Offset: 0x00217198
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
								value2 = Type.GetType(value);
							}
							else
							{
								if (propertyType != typeof(CultureInfo))
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

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00217EB8 File Offset: 0x002172B8
		internal static bool FEqualIdentity(XmlNode node, string name, string ns)
		{
			return node != null && node.LocalName == name && node.NamespaceURI == ns;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00217EE8 File Offset: 0x002172E8
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

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00217F58 File Offset: 0x00217358
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
