using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Serializers
{
	// Token: 0x02000008 RID: 8
	public class CustomDataDateTimeSerializer : ICustomDataSerializer<CustomDataDateTime>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public CustomDataSerialized Serialize(CustomDataDateTime dataObj)
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
					new XElement(eCustomDataPrimitiveType.DateTime.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag, new object[]
					{
						new XAttribute("id", dataObj.DataInstanceId.ToString()),
						new XAttribute("val", dataObj.Value.ToString("yyyy-MM-dd h:mm tt"))
					})
				}).ToString();
				result = new CustomDataSerialized(dataObj.DataType, dataObj.DataInstanceId, xml, null, null);
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public CustomDataDateTime DeSerialize(CustomDataSerialized serializedData)
		{
			XDocument xdocument = XDocument.Parse(serializedData.DataValueXml);
			XElement xelement = xdocument.Descendants(eCustomDataPrimitiveType.DateTime.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag).FirstOrDefault<XElement>();
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
			DateTime value;
			bool flag = !DateTime.TryParse(s, out value);
			CustomDataDateTime result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomDataDateTime customDataDateTime = new CustomDataDateTime();
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
				customDataDateTime.DataInstanceId = new Guid(text2 ?? "");
				customDataDateTime.DataType = eCustomDataPrimitiveType.DateTime;
				customDataDateTime.Value = value;
				result = customDataDateTime;
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002D84 File Offset: 0x00000F84
		public bool IsValueEmptyForStorage(CustomDataDateTime dataObj)
		{
			return dataObj == null || dataObj.Value == DateTime.MinValue;
		}
	}
}
