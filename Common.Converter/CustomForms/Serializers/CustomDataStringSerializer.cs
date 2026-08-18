using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Serializers
{
	// Token: 0x0200000C RID: 12
	public class CustomDataStringSerializer : ICustomDataSerializer<CustomDataString>
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000034DC File Offset: 0x000016DC
		public CustomDataSerialized Serialize(CustomDataString dataObj)
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
					new XElement(eCustomDataPrimitiveType.String.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag, new object[]
					{
						new XAttribute("id", dataObj.DataInstanceId.ToString()),
						new XAttribute("val", dataObj.Value ?? ""),
						new XAttribute("type", ((int)dataObj.TextType).ToString())
					})
				}).ToString();
				result = new CustomDataSerialized(dataObj.DataType, dataObj.DataInstanceId, xml, null, null);
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000035BC File Offset: 0x000017BC
		public CustomDataString DeSerialize(CustomDataSerialized serializedData)
		{
			XDocument xdocument = XDocument.Parse(serializedData.DataValueXml);
			XElement xelement = xdocument.Descendants(eCustomDataPrimitiveType.String.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag).FirstOrDefault<XElement>();
			string text;
			if (xelement == null)
			{
				text = null;
			}
			else
			{
				XAttribute xattribute = xelement.Attributes("type").FirstOrDefault<XAttribute>();
				text = ((xattribute != null) ? xattribute.Value : null);
			}
			string s = (text ?? "").Trim();
			int num;
			eCustomDataStringTextType textType = (eCustomDataStringTextType)((int.TryParse(s, out num) && Enum.IsDefined(typeof(eCustomDataStringTextType), num)) ? num : 0);
			CustomDataString customDataString = new CustomDataString();
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
			customDataString.DataInstanceId = new Guid(text2 ?? "");
			customDataString.DataType = eCustomDataPrimitiveType.String;
			string text3;
			if (xelement == null)
			{
				text3 = null;
			}
			else
			{
				XAttribute xattribute3 = xelement.Attributes("val").First<XAttribute>();
				text3 = ((xattribute3 != null) ? xattribute3.Value : null);
			}
			customDataString.Value = (text3 ?? "").Trim();
			customDataString.TextType = textType;
			return customDataString;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000036E8 File Offset: 0x000018E8
		public bool IsValueEmptyForStorage(CustomDataString dataObj)
		{
			bool flag = (((dataObj != null) ? dataObj.Value : null) ?? "").Length < 1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				eCustomDataStringTextType textType = dataObj.TextType;
				eCustomDataStringTextType eCustomDataStringTextType = textType;
				if (eCustomDataStringTextType != eCustomDataStringTextType.Rtf)
				{
				}
				result = false;
			}
			return result;
		}
	}
}
