using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x02000021 RID: 33
	public class CustomRichTextBoxSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomRichTextBoxDTO>
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public XElement Serialize(Guid formId, CustomRichTextBoxDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomRichTextBoxDTO>(), new object[]
			{
				new XAttribute("maxchars", dataObj.MaxChars.ToString()),
				new XAttribute("rowcount", dataObj.RowCount.ToString())
			}) : null;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004E24 File Offset: 0x00003024
		public CustomRichTextBoxDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomRichTextBoxDTO customRichTextBoxDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customRichTextBoxDTO == null;
			CustomRichTextBoxDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomRichTextBoxDTO customRichTextBoxDTO2 = customRichTextBoxDTO;
				XAttribute xattribute = serializedData.Attribute("maxchars");
				customRichTextBoxDTO2.MaxChars = ((xattribute != null) ? xattribute.GetIntFromAttribute(0) : 0);
				CustomRichTextBoxDTO customRichTextBoxDTO3 = customRichTextBoxDTO;
				XAttribute xattribute2 = serializedData.Attribute("rowcount");
				customRichTextBoxDTO3.RowCount = ((xattribute2 != null) ? xattribute2.GetIntFromAttribute(0) : 0);
				result = customRichTextBoxDTO;
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004E9C File Offset: 0x0000309C
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomRichTextBoxDTO);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004EBC File Offset: 0x000030BC
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
