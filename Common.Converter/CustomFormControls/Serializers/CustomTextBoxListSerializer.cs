using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x02000023 RID: 35
	public class CustomTextBoxListSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomTextBoxListDTO>
	{
		// Token: 0x060000AF RID: 175 RVA: 0x0000502C File Offset: 0x0000322C
		public XElement Serialize(Guid formId, CustomTextBoxListDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomTextBoxListDTO>(), new object[]
			{
				new XAttribute("maxchars", dataObj.MaxChars.ToString()),
				new XAttribute("maxcount", dataObj.MaxTextBoxCount.ToString()),
				new XAttribute("startcount", dataObj.TextBoxCountStart.ToString())
			}) : null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000050B8 File Offset: 0x000032B8
		public CustomTextBoxListDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomTextBoxListDTO customTextBoxListDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customTextBoxListDTO == null;
			CustomTextBoxListDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomTextBoxListDTO customTextBoxListDTO2 = customTextBoxListDTO;
				XAttribute xattribute = serializedData.Attribute("maxchars");
				customTextBoxListDTO2.MaxChars = ((xattribute != null) ? xattribute.GetIntFromAttribute(0) : 0);
				CustomTextBoxListDTO customTextBoxListDTO3 = customTextBoxListDTO;
				XAttribute xattribute2 = serializedData.Attribute("maxcount");
				customTextBoxListDTO3.MaxTextBoxCount = ((xattribute2 != null) ? xattribute2.GetIntFromAttribute(0) : 0);
				CustomTextBoxListDTO customTextBoxListDTO4 = customTextBoxListDTO;
				XAttribute xattribute3 = serializedData.Attribute("startcount");
				customTextBoxListDTO4.TextBoxCountStart = ((xattribute3 != null) ? xattribute3.GetIntFromAttribute(0) : 0);
				result = customTextBoxListDTO;
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005154 File Offset: 0x00003354
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomTextBoxListDTO);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005174 File Offset: 0x00003374
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
