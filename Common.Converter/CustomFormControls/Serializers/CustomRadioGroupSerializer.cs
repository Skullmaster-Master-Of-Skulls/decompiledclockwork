using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x0200001F RID: 31
	public class CustomRadioGroupSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomRadioGroupDTO>
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public XElement Serialize(Guid formId, CustomRadioGroupDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomRadioGroupDTO>(), new object[]
			{
				new XAttribute("numx", dataObj.NumHorizontal.ToString()),
				new XAttribute("listgroupid", dataObj.CustomListGroupId.ToString())
			}) : null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C58 File Offset: 0x00002E58
		public CustomRadioGroupDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomRadioGroupDTO customRadioGroupDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customRadioGroupDTO == null;
			CustomRadioGroupDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomRadioGroupDTO customRadioGroupDTO2 = customRadioGroupDTO;
				XAttribute xattribute = serializedData.Attribute("numx");
				customRadioGroupDTO2.NumHorizontal = ((xattribute != null) ? xattribute.GetIntFromAttribute(0) : 0);
				XAttribute xattribute2 = serializedData.Attribute("listgroupid");
				string text = (((xattribute2 != null) ? xattribute2.Value : null) ?? "").Trim();
				customRadioGroupDTO.CustomListGroupId = ((text.Length > 0) ? new Guid(text) : Guid.Empty);
				result = customRadioGroupDTO;
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomRadioGroupDTO);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004D14 File Offset: 0x00002F14
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
