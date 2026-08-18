using System;
using System.Text;
using System.Web.Mvc;
using TechnoPro.ClockWorkWeb.Models.Exceptions;

namespace TechnoPro.ClockWorkWeb.Infrastructure.ErrorHandler
{
	// Token: 0x02000114 RID: 276
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public sealed class HandleModelStateExceptionAttribute : FilterAttribute, IExceptionFilter
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x0003AC58 File Offset: 0x00038E58
		public void OnException(ExceptionContext filterContext)
		{
			bool flag = filterContext == null;
			if (flag)
			{
				throw new ArgumentNullException("filterContext");
			}
			bool flag2 = filterContext.Exception != null && typeof(ModelStateException).IsInstanceOfType(filterContext.Exception) && !filterContext.ExceptionHandled;
			if (flag2)
			{
				filterContext.ExceptionHandled = true;
				filterContext.HttpContext.Response.Clear();
				filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
				filterContext.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
				filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
				filterContext.HttpContext.Response.StatusCode = 400;
				filterContext.Result = new ContentResult
				{
					Content = (filterContext.Exception as ModelStateException).Message,
					ContentEncoding = Encoding.UTF8
				};
			}
		}
	}
}
