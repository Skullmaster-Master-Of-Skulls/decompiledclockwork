using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Controls;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x0200001E RID: 30
	public class CustomLabelSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomLabelDTO>
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00004B0C File Offset: 0x00002D0C
		public XElement Serialize(Guid formId, CustomLabelDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementFromCustomControlBase(dataObj.GetCustomControlTagForXml<CustomLabelDTO>(), new object[]
			{
				new XAttribute("case", ((int)dataObj.CharacterCasing).ToString())
			}) : null;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004B58 File Offset: 0x00002D58
		public CustomLabelDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomLabelDTO customLabelDTO = (serializedData != null) ? serializedData.CreateCustomControlBaseFromXElement(formId) : null;
			bool flag = customLabelDTO == null;
			CustomLabelDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomLabelDTO customLabelDTO2 = customLabelDTO;
				XAttribute xattribute = serializedData.Attribute("case");
				customLabelDTO2.CharacterCasing = ((xattribute != null) ? xattribute.GetEnumFromAttributeInt(eCustomControlCharacterCasing.Default) : eCustomControlCharacterCasing.Default);
				result = customLabelDTO;
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004BAC File Offset: 0x00002DAC
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomLabelDTO);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004BCC File Offset: 0x00002DCC
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
