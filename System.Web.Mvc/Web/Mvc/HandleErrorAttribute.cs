using System;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001C6 RID: 454
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class HandleErrorAttribute : FilterAttribute, IExceptionFilter
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00023700 File Offset: 0x00021900
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x00023708 File Offset: 0x00021908
		public Type ExceptionType
		{
			get
			{
				return this._exceptionType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!typeof(Exception).IsAssignableFrom(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.ExceptionViewAttribute_NonExceptionType, new object[]
					{
						value.FullName
					}));
				}
				this._exceptionType = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00023768 File Offset: 0x00021968
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x00023779 File Offset: 0x00021979
		public string Master
		{
			get
			{
				return this._master ?? string.Empty;
			}
			set
			{
				this._master = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00023782 File Offset: 0x00021982
		public override object TypeId
		{
			get
			{
				return this._typeId;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002378A File Offset: 0x0002198A
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x000237A5 File Offset: 0x000219A5
		public string View
		{
			get
			{
				if (string.IsNullOrEmpty(this._view))
				{
					return "Error";
				}
				return this._view;
			}
			set
			{
				this._view = value;
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000237B0 File Offset: 0x000219B0
		public virtual void OnException(ExceptionContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (filterContext.IsChildAction)
			{
				return;
			}
			if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
			{
				return;
			}
			Exception exception = filterContext.Exception;
			if (new HttpException(null, exception).GetHttpCode() != 500)
			{
				return;
			}
			if (!this.ExceptionType.IsInstanceOfType(exception))
			{
				return;
			}
			string controllerName = (string)filterContext.RouteData.Values["controller"];
			string actionName = (string)filterContext.RouteData.Values["action"];
			HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
			filterContext.Result = new ViewResult
			{
				ViewName = this.View,
				MasterName = this.Master,
				ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
				TempData = filterContext.Controller.TempData
			};
			filterContext.ExceptionHandled = true;
			filterContext.HttpContext.Response.Clear();
			filterContext.HttpContext.Response.StatusCode = 500;
			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
		}

		// Token: 0x04000374 RID: 884
		private const string DefaultView = "Error";

		// Token: 0x04000375 RID: 885
		private readonly object _typeId = new object();

		// Token: 0x04000376 RID: 886
		private Type _exceptionType = typeof(Exception);

		// Token: 0x04000377 RID: 887
		private string _master;

		// Token: 0x04000378 RID: 888
		private string _view;
	}
}
