using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Converter.CustomFormControls
{
	// Token: 0x02000019 RID: 25
	public static class CustomControlSerializerAdapter
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00004360 File Offset: 0x00002560
		public static XElement CreateXElementFromCustomControlBase(this CustomControlBaseDTO dataObj, string customControlTag, params object[] content)
		{
			XElement xelement = new XElement(customControlTag, new object[]
			{
				new XAttribute("id", dataObj.ControlId ?? ""),
				new XAttribute("caption", dataObj.Caption ?? "")
			});
			bool flag = content != null && content.Length != 0;
			if (flag)
			{
				xelement.Add(content);
			}
			return xelement;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000043E0 File Offset: 0x000025E0
		public static XElement CreateXElementCromCustomControlDataHolding(this CustomControlDataHolderDTO dataObj, string customControlTag, params object[] content)
		{
			XElement xelement = new XElement(customControlTag, new object[]
			{
				new XAttribute("id", dataObj.ControlId ?? ""),
				new XAttribute("datainstanceid", dataObj.DataInstanceId.ToString()),
				new XAttribute("caption", dataObj.Caption ?? ""),
				new XAttribute("valstaff", ((int)dataObj.ControlValidationStaffType).ToString()),
				new XAttribute("valstudent", ((int)dataObj.ControlValidationStudentType).ToString())
			});
			bool flag = content != null && content.Length != 0;
			if (flag)
			{
				xelement.Add(content);
			}
			return xelement;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000044C8 File Offset: 0x000026C8
		public static XElement CreateXElementCromCustomControlContainer(this CustomControlContainerDTO dataObj, string customControlTag, params object[] content)
		{
			XElement xelement = new XElement(customControlTag, new object[]
			{
				new XAttribute("id", dataObj.ControlId ?? ""),
				new XAttribute("caption", dataObj.Caption ?? "")
			});
			bool flag = content != null && content.Length != 0;
			if (flag)
			{
				xelement.Add(content);
			}
			return xelement;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004548 File Offset: 0x00002748
		public static T CreateCustomControlDataHolderFromXElement<T>(this XElement serializedData, Guid formId) where T : CustomControlDataHolderDTO
		{
			XAttribute xattribute = serializedData.Attribute("datainstanceid");
			string text = (xattribute != null) ? xattribute.Value : null;
			T t = Activator.CreateInstance<T>();
			t.FormId = formId;
			CustomControlBaseDTO customControlBaseDTO = t;
			XAttribute xattribute2 = serializedData.Attribute("id");
			customControlBaseDTO.ControlId = ((xattribute2 != null) ? xattribute2.Value : null);
			CustomControlBaseDTO customControlBaseDTO2 = t;
			XAttribute xattribute3 = serializedData.Attribute("caption");
			customControlBaseDTO2.Caption = (((xattribute3 != null) ? xattribute3.Value : null) ?? "");
			t.DataInstanceId = (string.IsNullOrEmpty(text) ? Guid.Empty : new Guid(text));
			CustomControlDataHolderDTO customControlDataHolderDTO = t;
			XAttribute xattribute4 = serializedData.Attribute("valstaff");
			customControlDataHolderDTO.ControlValidationStaffType = ((xattribute4 != null) ? xattribute4.GetEnumFromAttributeInt(eCustomControlValidationType.NotSpecified) : eCustomControlValidationType.NotSpecified);
			CustomControlDataHolderDTO customControlDataHolderDTO2 = t;
			XAttribute xattribute5 = serializedData.Attribute("valstudent");
			customControlDataHolderDTO2.ControlValidationStudentType = ((xattribute5 != null) ? xattribute5.GetEnumFromAttributeInt(eCustomControlValidationType.NotSpecified) : eCustomControlValidationType.NotSpecified);
			return t;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004658 File Offset: 0x00002858
		public static T CreateCustomControlBaseFromXElement<T>(this XElement serializedData, Guid formId) where T : CustomControlBaseDTO
		{
			T t = Activator.CreateInstance<T>();
			t.FormId = formId;
			CustomControlBaseDTO customControlBaseDTO = t;
			XAttribute xattribute = serializedData.Attribute("id");
			customControlBaseDTO.ControlId = ((xattribute != null) ? xattribute.Value : null);
			CustomControlBaseDTO customControlBaseDTO2 = t;
			XAttribute xattribute2 = serializedData.Attribute("caption");
			customControlBaseDTO2.Caption = (((xattribute2 != null) ? xattribute2.Value : null) ?? "");
			return t;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000046D8 File Offset: 0x000028D8
		public static T CreateCustomControlContainerFromXElement<T>(this XElement serializedData, Guid formId) where T : CustomControlContainerDTO
		{
			T t = Activator.CreateInstance<T>();
			t.FormId = formId;
			CustomControlBaseDTO customControlBaseDTO = t;
			XAttribute xattribute = serializedData.Attribute("id");
			customControlBaseDTO.ControlId = ((xattribute != null) ? xattribute.Value : null);
			CustomControlBaseDTO customControlBaseDTO2 = t;
			XAttribute xattribute2 = serializedData.Attribute("caption");
			customControlBaseDTO2.Caption = (((xattribute2 != null) ? xattribute2.Value : null) ?? "");
			return t;
		}
	}
}
