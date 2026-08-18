using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Serializers
{
	// Token: 0x02000006 RID: 6
	public class CustomDataBooleanNullableSerializer : ICustomDataSerializer<CustomDataBooleanNullable>
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000281C File Offset: 0x00000A1C
		public CustomDataSerialized Serialize(CustomDataBooleanNullable dataObj)
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
						new XAttribute("val", (dataObj.Value != null) ? (dataObj.Value.Value ? "1" : "0") : "")
					})
				}).ToString();
				result = new CustomDataSerialized(dataObj.DataType, dataObj.DataInstanceId, xml, null, null);
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002900 File Offset: 0x00000B00
		public CustomDataBooleanNullable DeSerialize(CustomDataSerialized serializedData)
		{
			XDocument xdocument = XDocument.Parse(serializedData.DataValueXml);
			XElement xelement = xdocument.Descendants(eCustomDataPrimitiveType.Boolean.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag).FirstOrDefault<XElement>();
			string text;
			if (xelement == null)
			{
				text = null;
			}
			else
			{
				XAttribute xattribute = xelement.Attributes("val").First<XAttribute>();
				text = ((xattribute != null) ? xattribute.Value : null);
			}
			string text2 = (text ?? "").Trim();
			CustomDataBooleanNullable customDataBooleanNullable = new CustomDataBooleanNullable();
			string text3;
			if (xelement == null)
			{
				text3 = null;
			}
			else
			{
				XAttribute xattribute2 = xelement.Attributes("id").First<XAttribute>();
				text3 = ((xattribute2 != null) ? xattribute2.Value : null);
			}
			customDataBooleanNullable.DataInstanceId = new Guid(text3 ?? "");
			customDataBooleanNullable.DataType = eCustomDataPrimitiveType.Boolean;
			customDataBooleanNullable.Value = ((text2.Length < 1) ? null : new bool?(text2 == "1"));
			return customDataBooleanNullable;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000029EC File Offset: 0x00000BEC
		public bool IsValueEmptyForStorage(CustomDataBooleanNullable dataObj)
		{
			bool? flag = (dataObj != null) ? dataObj.Value : null;
			return flag == null;
		}
	}
}
