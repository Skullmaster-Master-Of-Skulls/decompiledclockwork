using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Converter.CustomForms.Serializers
{
	// Token: 0x0200000B RID: 11
	public class CustomDataListItemSerializer : ICustomDataSerializer<CustomDataListItem>
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00003254 File Offset: 0x00001454
		public CustomDataSerialized Serialize(CustomDataListItem dataObj)
		{
			bool flag = this.IsValueEmptyForStorage(dataObj);
			CustomDataSerialized result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object[] array = new object[1];
				int num = 0;
				XName name = eCustomDataPrimitiveType.ListItem.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag;
				object[] array2 = new object[2];
				array2[0] = new XAttribute("id", dataObj.DataInstanceId.ToString());
				int num2 = 1;
				XName name2 = "liid";
				CustomListItem listItem = dataObj.ListItem;
				array2[num2] = new XAttribute(name2, (listItem != null) ? listItem.ListItemId.ToString() : null);
				array[num] = new XElement(name, array2);
				string text = new XDocument(array).ToString();
				eCustomDataPrimitiveType dataType = dataObj.DataType;
				Guid dataInstanceId = dataObj.DataInstanceId;
				string xml = text;
				CustomListItem listItem2 = dataObj.ListItem;
				Guid? dataValueJoinId = (listItem2 != null) ? new Guid?(listItem2.ListItemId) : null;
				object extraValues;
				if (dataObj.ListItem == null)
				{
					extraValues = null;
				}
				else
				{
					Dictionary<string, object> dictionary = extraValues = new Dictionary<string, object>();
					string key = "itemcaption";
					CustomListItem listItem3 = dataObj.ListItem;
					dictionary.Add(key, (listItem3 != null) ? listItem3.ItemCaption : null);
				}
				result = new CustomDataSerialized(dataType, dataInstanceId, xml, dataValueJoinId, extraValues);
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003364 File Offset: 0x00001564
		public CustomDataListItem DeSerialize(CustomDataSerialized serializedData)
		{
			XDocument xdocument = XDocument.Parse(serializedData.DataValueXml);
			XElement xelement = xdocument.Descendants(eCustomDataPrimitiveType.ListItem.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag).FirstOrDefault<XElement>();
			string text;
			if (xelement == null)
			{
				text = null;
			}
			else
			{
				XAttribute xattribute = xelement.Attributes("liid").FirstOrDefault<XAttribute>();
				text = ((xattribute != null) ? xattribute.Value : null);
			}
			string text2 = (text ?? "").Trim();
			string itemCaption = (serializedData.ExtraValues != null && serializedData.ExtraValues.ContainsKey("itemcaption")) ? ((serializedData.ExtraValues["itemcaption"] as string) ?? "") : "";
			CustomDataListItem customDataListItem = new CustomDataListItem();
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
			customDataListItem.DataInstanceId = new Guid(text3 ?? "");
			customDataListItem.DataType = eCustomDataPrimitiveType.ListItem;
			CustomListItem listItem;
			if (!string.IsNullOrEmpty(text2))
			{
				CustomListItem customListItem = new CustomListItem();
				customListItem.ListItemId = new Guid(text2);
				listItem = customListItem;
				customListItem.ItemCaption = itemCaption;
			}
			else
			{
				listItem = null;
			}
			customDataListItem.ListItem = listItem;
			return customDataListItem;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003490 File Offset: 0x00001690
		public bool IsValueEmptyForStorage(CustomDataListItem dataObj)
		{
			Guid? guid;
			if (dataObj == null)
			{
				guid = null;
			}
			else
			{
				CustomListItem listItem = dataObj.ListItem;
				guid = ((listItem != null) ? new Guid?(listItem.ListItemId) : null);
			}
			Guid? guid2 = guid;
			return guid2 == null;
		}
	}
}
