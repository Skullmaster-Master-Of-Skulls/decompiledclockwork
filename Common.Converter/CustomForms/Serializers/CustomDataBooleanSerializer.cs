using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Serializers
{
	// Token: 0x02000007 RID: 7
	public class CustomDataBooleanSerializer : ICustomDataSerializer<CustomDataBoolean>
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002A28 File Offset: 0x00000C28
		public CustomDataSerialized Serialize(CustomDataBoolean dataObj)
		{
			bool flag = this.IsValueEmptyForStorage(dataObj);
			CustomDataSerialized result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string xml = new XDocument(new object[]
				{
					new XElement(eCustomDataPrimitiveType.Boolean.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag, new object[]
					{
						new XAttribute("id", dataObj.DataInstanceId.ToString()),
						new XAttribute("val", dataObj.Value ? "1" : "0")
					})
				}).ToString();
				result = new CustomDataSerialized(dataObj.DataType, dataObj.DataInstanceId, xml, null, null);
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002AEC File Offset: 0x00000CEC
		public CustomDataBoolean DeSerialize(CustomDataSerialized serializedData)
		{
			XDocument xdocument = XDocument.Parse(serializedData.DataValueXml);
			XElement xelement = xdocument.Descendants(eCustomDataPrimitiveType.Boolean.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag).FirstOrDefault<XElement>();
			CustomDataBoolean customDataBoolean = new CustomDataBoolean();
			string text;
			if (xelement == null)
			{
				text = null;
			}
			else
			{
				XAttribute xattribute = xelement.Attributes("id").First<XAttribute>();
				text = ((xattribute != null) ? xattribute.Value : null);
			}
			customDataBoolean.DataInstanceId = new Guid(text ?? "");
			customDataBoolean.DataType = eCustomDataPrimitiveType.Boolean;
			string text2;
			if (xelement == null)
			{
				text2 = null;
			}
			else
			{
				XAttribute xattribute2 = xelement.Attributes("val").First<XAttribute>();
				text2 = ((xattribute2 != null) ? xattribute2.Value : null);
			}
			customDataBoolean.Value = ((text2 ?? "").Trim() == "1");
			return customDataBoolean;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002BBC File Offset: 0x00000DBC
		public bool IsValueEmptyForStorage(CustomDataBoolean dataObj)
		{
			return dataObj == null || !dataObj.Value;
		}
	}
}
