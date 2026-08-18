using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Serializers
{
	// Token: 0x0200000A RID: 10
	public class CustomDataIntSerializer : ICustomDataSerializer<CustomDataInt>
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00003080 File Offset: 0x00001280
		public CustomDataSerialized Serialize(CustomDataInt dataObj)
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
					new XElement(eCustomDataPrimitiveType.Int.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag, new object[]
					{
						new XAttribute("id", dataObj.DataInstanceId.ToString()),
						new XAttribute("val", dataObj.Value.ToString())
					})
				}).ToString();
				result = new CustomDataSerialized(dataObj.DataType, dataObj.DataInstanceId, xml, null, null);
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000313C File Offset: 0x0000133C
		public CustomDataInt DeSerialize(CustomDataSerialized serializedData)
		{
			XDocument xdocument = XDocument.Parse(serializedData.DataValueXml);
			XElement xelement = xdocument.Descendants(eCustomDataPrimitiveType.Int.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag).FirstOrDefault<XElement>();
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
			string s = (text ?? "").Trim();
			int value;
			bool flag = !int.TryParse(s, out value);
			CustomDataInt result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomDataInt customDataInt = new CustomDataInt();
				string text2;
				if (xelement == null)
				{
					text2 = null;
				}
				else
				{
					XAttribute xattribute2 = xelement.Attributes("id").First<XAttribute>();
					text2 = ((xattribute2 != null) ? xattribute2.Value : null);
				}
				customDataInt.DataInstanceId = new Guid(text2 ?? "");
				customDataInt.DataType = eCustomDataPrimitiveType.Int;
				customDataInt.Value = value;
				result = customDataInt;
			}
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000321C File Offset: 0x0000141C
		public bool IsValueEmptyForStorage(CustomDataInt dataObj)
		{
			int? num = (dataObj != null) ? new int?(dataObj.Value) : null;
			return num == null;
		}
	}
}
