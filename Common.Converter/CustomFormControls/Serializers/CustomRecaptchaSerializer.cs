using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x02000020 RID: 32
	public class CustomRecaptchaSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomRecaptchaDTO>
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004D30 File Offset: 0x00002F30
		public XElement Serialize(Guid formId, CustomRecaptchaDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementFromCustomControlBase(dataObj.GetCustomControlTagForXml<CustomRecaptchaDTO>(), Array.Empty<object>()) : null;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004D5C File Offset: 0x00002F5C
		public CustomRecaptchaDTO DeSerialize(Guid formId, XElement serializedData)
		{
			return (serializedData != null) ? serializedData.CreateCustomControlBaseFromXElement(formId) : null;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004D7C File Offset: 0x00002F7C
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomRecaptchaDTO);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004D9C File Offset: 0x00002F9C
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
