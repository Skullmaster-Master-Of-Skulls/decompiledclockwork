using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000782 RID: 1922
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.Captcha)]
	public class CustomRecaptchaDTO : CustomControlStaticDTO
	{
	}
}
