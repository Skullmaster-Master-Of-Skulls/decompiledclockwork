using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x02000025 RID: 37
	public class CustomTextBoxSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomTextBoxDTO>
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000052B0 File Offset: 0x000034B0
		public XElement Serialize(Guid formId, CustomTextBoxDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomTextBoxDTO>(), new object[]
			{
				new XAttribute("maxchars", dataObj.MaxChars.ToString()),
				new XAttribute("rowcount", dataObj.RowCount.ToString())
			}) : null;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000531C File Offset: 0x0000351C
		public CustomTextBoxDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomTextBoxDTO customTextBoxDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customTextBoxDTO == null;
			CustomTextBoxDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomTextBoxDTO customTextBoxDTO2 = customTextBoxDTO;
				XAttribute xattribute = serializedData.Attribute("maxchars");
				customTextBoxDTO2.MaxChars = ((xattribute != null) ? xattribute.GetIntFromAttribute(0) : 0);
				CustomTextBoxDTO customTextBoxDTO3 = customTextBoxDTO;
				XAttribute xattribute2 = serializedData.Attribute("rowcount");
				customTextBoxDTO3.RowCount = ((xattribute2 != null) ? xattribute2.GetIntFromAttribute(0) : 0);
				result = customTextBoxDTO;
			}
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005394 File Offset: 0x00003594
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomTextBoxDTO);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000053B4 File Offset: 0x000035B4
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
