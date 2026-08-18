using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x0200001B RID: 27
	public class CustomDropListSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomDropListDTO>
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000047F0 File Offset: 0x000029F0
		public XElement Serialize(Guid formId, CustomDropListDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomDropListDTO>(), new object[]
			{
				new XAttribute("listgroupid", dataObj.CustomListGroupId.ToString())
			}) : null;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004840 File Offset: 0x00002A40
		public CustomDropListDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomDropListDTO customDropListDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customDropListDTO == null;
			CustomDropListDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XAttribute xattribute = serializedData.Attribute("listgroupid");
				string text = (((xattribute != null) ? xattribute.Value : null) ?? "").Trim();
				customDropListDTO.CustomListGroupId = ((text.Length > 0) ? new Guid(text) : Guid.Empty);
				result = customDropListDTO;
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000048B8 File Offset: 0x00002AB8
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomDropListDTO);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000048D8 File Offset: 0x00002AD8
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
