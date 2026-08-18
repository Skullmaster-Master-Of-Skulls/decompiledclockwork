using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x0200001A RID: 26
	public class CustomCheckBoxSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomCheckBoxDTO>
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00004758 File Offset: 0x00002958
		public XElement Serialize(Guid formId, CustomCheckBoxDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomCheckBoxDTO>(), Array.Empty<object>()) : null;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004784 File Offset: 0x00002984
		public CustomCheckBoxDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomCheckBoxDTO customCheckBoxDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customCheckBoxDTO == null;
			CustomCheckBoxDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = customCheckBoxDTO;
			}
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000047B4 File Offset: 0x000029B4
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomCheckBoxDTO);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000047D4 File Offset: 0x000029D4
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
