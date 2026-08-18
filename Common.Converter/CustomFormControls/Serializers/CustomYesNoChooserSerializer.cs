using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x02000026 RID: 38
	public class CustomYesNoChooserSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomYesNoChooserDTO>
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000053D0 File Offset: 0x000035D0
		public XElement Serialize(Guid formId, CustomYesNoChooserDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomYesNoChooserDTO>(), new object[]
			{
				new XAttribute("popupyes", string.Join(",", dataObj.PopupYesControlIds ?? new string[0])),
				new XAttribute("popupno", string.Join(",", dataObj.PopupNoControlIds ?? new string[0]))
			}) : null;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005454 File Offset: 0x00003654
		public CustomYesNoChooserDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomYesNoChooserDTO customYesNoChooserDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customYesNoChooserDTO == null;
			CustomYesNoChooserDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomYesNoChooserDTO customYesNoChooserDTO2 = customYesNoChooserDTO;
				XAttribute xattribute = serializedData.Attribute("popupyes");
				customYesNoChooserDTO2.PopupYesControlIds = (from g in (((xattribute != null) ? xattribute.GetStringFromAttribute() : null) ?? "").Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).ToArray<string>();
				CustomYesNoChooserDTO customYesNoChooserDTO3 = customYesNoChooserDTO;
				XAttribute xattribute2 = serializedData.Attribute("popupno");
				customYesNoChooserDTO3.PopupNoControlIds = (from g in (((xattribute2 != null) ? xattribute2.GetStringFromAttribute() : null) ?? "").Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).ToArray<string>();
				result = customYesNoChooserDTO;
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005598 File Offset: 0x00003798
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomYesNoChooserDTO);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000055B8 File Offset: 0x000037B8
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
