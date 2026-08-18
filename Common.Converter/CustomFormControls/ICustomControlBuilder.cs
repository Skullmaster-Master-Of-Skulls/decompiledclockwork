using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;

namespace TechnoPro.Common.Converter.CustomFormControls
{
	// Token: 0x02000015 RID: 21
	public interface ICustomControlBuilder<out TU> where TU : CustomControlBaseDTO
	{
		// Token: 0x06000076 RID: 118
		TU BuildControl(XElement xCtrl, Guid formId);
	}
}
