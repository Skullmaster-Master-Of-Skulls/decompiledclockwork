using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x02000024 RID: 36
	public class CustomTextBoxNumberSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomTextBoxNumberDTO>
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00005190 File Offset: 0x00003390
		public XElement Serialize(Guid formId, CustomTextBoxNumberDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomTextBoxNumberDTO>(), new object[]
			{
				new XAttribute("min", dataObj.MinValue.ToString()),
				new XAttribute("max", dataObj.MaxValue.ToString())
			}) : null;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000051FC File Offset: 0x000033FC
		public CustomTextBoxNumberDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomTextBoxNumberDTO customTextBoxNumberDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customTextBoxNumberDTO == null;
			CustomTextBoxNumberDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomTextBoxNumberDTO customTextBoxNumberDTO2 = customTextBoxNumberDTO;
				XAttribute xattribute = serializedData.Attribute("min");
				customTextBoxNumberDTO2.MinValue = ((xattribute != null) ? xattribute.GetIntFromAttribute(0) : 0);
				CustomTextBoxNumberDTO customTextBoxNumberDTO3 = customTextBoxNumberDTO;
				XAttribute xattribute2 = serializedData.Attribute("max");
				customTextBoxNumberDTO3.MaxValue = ((xattribute2 != null) ? xattribute2.GetIntFromAttribute(0) : 0);
				result = customTextBoxNumberDTO;
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005274 File Offset: 0x00003474
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomTextBoxNumberDTO);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005294 File Offset: 0x00003494
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
