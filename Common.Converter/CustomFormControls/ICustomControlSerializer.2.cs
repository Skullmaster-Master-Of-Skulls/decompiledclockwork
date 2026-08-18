using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;

namespace TechnoPro.Common.Converter.CustomFormControls
{
	// Token: 0x02000018 RID: 24
	public interface ICustomControlSerializer<T> where T : CustomControlBaseDTO
	{
		// Token: 0x0600007A RID: 122
		XElement Serialize(Guid formId, T dataObj);

		// Token: 0x0600007B RID: 123
		T DeSerialize(Guid formId, XElement serializedData);
	}
}
