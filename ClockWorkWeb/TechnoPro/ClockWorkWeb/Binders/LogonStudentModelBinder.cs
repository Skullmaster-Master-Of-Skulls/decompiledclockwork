using System;
using System.Web.Mvc;
using TechnoPro.ClockWorkWeb.Infrastructure;

namespace TechnoPro.ClockWorkWeb.Binders
{
	// Token: 0x0200015B RID: 347
	public class LogonStudentModelBinder : IModelBinder
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x00048A9C File Offset: 0x00046C9C
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return LogonPerson.Instance.GetLogonStudent(controllerContext.HttpContext.Session);
		}
	}
}
