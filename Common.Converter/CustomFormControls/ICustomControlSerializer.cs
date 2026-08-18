using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;

namespace TechnoPro.Common.Converter.CustomFormControls
{
	// Token: 0x02000017 RID: 23
	public interface ICustomControlSerializer
	{
		// Token: 0x06000078 RID: 120
		XElement SerializeItem(Guid formId, CustomControlBaseDTO dataObj);

		// Token: 0x06000079 RID: 121
		CustomControlBaseDTO DeSerializeItem(Guid formId, XElement serializedData);
	}
}
