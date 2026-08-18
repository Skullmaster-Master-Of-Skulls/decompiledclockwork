using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;

namespace TechnoPro.Common.Converter.CustomFormControls.Serializers
{
	// Token: 0x02000022 RID: 34
	public class CustomSingleFileSerializer : ICustomControlSerializer, ICustomControlSerializer<CustomSingleFileDTO>
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004ED8 File Offset: 0x000030D8
		public XElement Serialize(Guid formId, CustomSingleFileDTO dataObj)
		{
			return (dataObj != null) ? dataObj.CreateXElementCromCustomControlDataHolding(dataObj.GetCustomControlTagForXml<CustomSingleFileDTO>(), new object[]
			{
				new XAttribute("allowedfiletypes", string.Join(",", dataObj.AllowedFileTypes ?? new string[0]))
			}) : null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004F30 File Offset: 0x00003130
		public CustomSingleFileDTO DeSerialize(Guid formId, XElement serializedData)
		{
			CustomSingleFileDTO customSingleFileDTO = (serializedData != null) ? serializedData.CreateCustomControlDataHolderFromXElement(formId) : null;
			bool flag = customSingleFileDTO == null;
			CustomSingleFileDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CustomSingleFileDTO customSingleFileDTO2 = customSingleFileDTO;
				XAttribute xattribute = serializedData.Attribute("allowedfiletypes");
				customSingleFileDTO2.AllowedFileTypes = (from g in (((xattribute != null) ? xattribute.Value : null) ?? "").Trim().Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).ToArray<string>();
				result = customSingleFileDTO;
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004FF0 File Offset: 0x000031F0
		public XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj)
		{
			return this.Serialize(formId, dataObj as CustomSingleFileDTO);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005010 File Offset: 0x00003210
		public CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData)
		{
			return this.DeSerialize(formId, serializedData);
		}
	}
}
