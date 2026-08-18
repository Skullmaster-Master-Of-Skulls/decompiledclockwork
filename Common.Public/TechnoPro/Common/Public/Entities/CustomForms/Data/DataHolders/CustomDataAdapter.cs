using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x0200042C RID: 1068
	public static class CustomDataAdapter
	{
		// Token: 0x06002064 RID: 8292 RVA: 0x00024978 File Offset: 0x00022B78
		public static eCustomDataPrimitiveType GetDataPrimitiveTypeFromXmlTag(this string tag)
		{
			string nm = tag.ToLower().Trim();
			return ((eCustomDataPrimitiveType[])Enum.GetValues(typeof(eCustomDataPrimitiveType))).FirstOrDefault((eCustomDataPrimitiveType g) => nm == (g.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag ?? ""));
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000249C8 File Offset: 0x00022BC8
		public static string GetXml(this string elementName, params XAttribute[] attributes)
		{
			return new XDocument(new object[]
			{
				new XElement(elementName),
				attributes
			}).ToString();
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000249FC File Offset: 0x00022BFC
		public static string GetXml(this string elementName, string attributeName, string val)
		{
			return new XDocument(new object[]
			{
				new XElement(elementName),
				new XAttribute(attributeName, val ?? "")
			}).ToString();
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00024A44 File Offset: 0x00022C44
		public static IDictionary<string, string> GetStringVals(this string xml, string elementName, params string[] attributeNames)
		{
			XDocument xdocument = XDocument.Parse(xml);
			XElement element = xdocument.Descendants(elementName).FirstOrDefault<XElement>();
			return attributeNames.ToDictionary((string g) => g, delegate(string g)
			{
				XAttribute xattribute = element.Attribute(g);
				return ((xattribute != null) ? xattribute.Value : null) ?? "";
			});
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00024AAC File Offset: 0x00022CAC
		public static string GetStringVal(this string xml, string elementName, string attributeName)
		{
			XDocument xdocument = XDocument.Parse(xml);
			XElement xelement = xdocument.Descendants(elementName).FirstOrDefault<XElement>();
			XAttribute xattribute = (xelement != null) ? xelement.Attribute(attributeName) : null;
			return (xattribute != null) ? xattribute.Value : null;
		}
	}
}
