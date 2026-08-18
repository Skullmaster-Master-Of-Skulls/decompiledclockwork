using System;
using System.Web.Mvc;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests;
using TechnoPro.ClockWorkWeb.Infrastructure;

namespace TechnoPro.ClockWorkWeb.Binders
{
	// Token: 0x0200015A RID: 346
	public class AlternateFormatPendingRequestsModelBinder : IModelBinder
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x00048A28 File Offset: 0x00046C28
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			PendingRequestsCart pendingRequestsCart = (PendingRequestsCart)controllerContext.HttpContext.Session["PendingRequestsCart"];
			bool flag = pendingRequestsCart == null;
			if (flag)
			{
				pendingRequestsCart = new PendingRequestsCart
				{
					Student = LogonPerson.Instance.GetLogonStudent(controllerContext.HttpContext.Session)
				};
				controllerContext.HttpContext.Session["PendingRequestsCart"] = pendingRequestsCart;
			}
			return pendingRequestsCart;
		}

		// Token: 0x0400080F RID: 2063
		private const string sessionKey = "PendingRequestsCart";
	}
}
