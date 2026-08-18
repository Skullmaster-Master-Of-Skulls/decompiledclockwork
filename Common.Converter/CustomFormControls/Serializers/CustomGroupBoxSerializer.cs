using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x0200001D RID: 29
	public class CustomGroupBoxSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomGroupBoxDTO>
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00004A00 File Offset: 0x00002C00
		public XElement Serialize(Guid formId, CustomGroupBoxDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlContainer(dataObj.GetCustomControlTagForXml<CustomGroupBoxDTO>(), new object[]
			{
				new XAttribute("backcolor", (dataObj.BackgroundColorArgb != null) ? dataObj.BackgroundColorArgb.Value.ToString() : "")
			}) : null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004A6C File Offset: 0x00002C6C
		public CustomGroupBoxDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomGroupBoxDTO customGroupBoxDTO = (serializedData != null) ? serializedData.CreateCustomControlContainerFromXElement(formId) : null;
			bool flag = customGroupBoxDTO == null;
			CustomGroupBoxDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomGroupBoxDTO customGroupBoxDTO2 = customGroupBoxDTO;
				XAttribute xattribute = serializedData.Attribute("backcolor");
				customGroupBoxDTO2.BackgroundColorArgb = (((xattribute != null) ? xattribute.Value : null) ?? "").Trim().ConvertStringToInt();
				result = customGroupBoxDTO;
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomGroupBoxDTO);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
