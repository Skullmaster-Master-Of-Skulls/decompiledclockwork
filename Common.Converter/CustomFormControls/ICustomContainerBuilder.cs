using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;

namespace TechnoPro.Common.Converter.CustomFormControls
{
	// Token: 0x02000016 RID: 22
	public interface ICustomContainerBuilder<out TU> where TU : CustomControlContainerDTO
	{
		// Token: 0x06000077 RID: 119
		TU BuildContainer(XElement xCtrl, Guid formId);
	}
}
