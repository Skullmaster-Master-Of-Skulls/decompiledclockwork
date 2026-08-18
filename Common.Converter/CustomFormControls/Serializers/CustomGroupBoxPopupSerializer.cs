using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x0200001C RID: 28
	public class CustomGroupBoxPopupSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomGroupBoxPopupDTO>
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000048F4 File Offset: 0x00002AF4
		public XElement Serialize(Guid formId, CustomGroupBoxPopupDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementFromCustomControlBase(dataObj.GetCustomControlTagForXml<CustomGroupBoxPopupDTO>(), new object[]
			{
				new XAttribute("backcolor", (dataObj.BackgroundColorArgb != null) ? dataObj.BackgroundColorArgb.Value.ToString() : "")
			}) : null;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004960 File Offset: 0x00002B60
		public CustomGroupBoxPopupDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomGroupBoxPopupDTO customGroupBoxPopupDTO = (serializedData != null) ? serializedData.CreateCustomControlContainerFromXElement(formId) : null;
			bool flag = customGroupBoxPopupDTO == null;
			CustomGroupBoxPopupDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomGroupBoxPopupDTO customGroupBoxPopupDTO2 = customGroupBoxPopupDTO;
				XAttribute xattribute = serializedData.Attribute("backcolor");
				customGroupBoxPopupDTO2.BackgroundColorArgb = (((xattribute != null) ? xattribute.Value : null) ?? "").Trim().ConvertStringToInt();
				result = customGroupBoxPopupDTO;
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000049C4 File Offset: 0x00002BC4
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomGroupBoxPopupDTO);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000049E4 File Offset: 0x00002BE4
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
