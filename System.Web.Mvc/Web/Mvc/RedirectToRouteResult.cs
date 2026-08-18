using System;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x020001E1 RID: 481
	public class RedirectToRouteResult : ActionResult
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x00026219 File Offset: 0x00024419
		public RedirectToRouteResult(RouteValueDictionary routeValues) : this(null, routeValues)
		{
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00026223 File Offset: 0x00024423
		public RedirectToRouteResult(string routeName, RouteValueDictionary routeValues) : this(routeName, routeValues, false)
		{
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0002622E File Offset: 0x0002442E
		public RedirectToRouteResult(string routeName, RouteValueDictionary routeValues, bool permanent)
		{
			this.Permanent = permanent;
			this.RouteName = (routeName ?? string.Empty);
			this.RouteValues = (routeValues ?? new RouteValueDictionary());
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0002625D File Offset: 0x0002445D
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x00026265 File Offset: 0x00024465
		public bool Permanent { get; private set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0002626E File Offset: 0x0002446E
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x00026276 File Offset: 0x00024476
		public string RouteName { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0002627F File Offset: 0x0002447F
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x00026287 File Offset: 0x00024487
		public RouteValueDictionary RouteValues { get; private set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00026290 File Offset: 0x00024490
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x000262AB File Offset: 0x000244AB
		internal RouteCollection Routes
		{
			get
			{
				if (this._routes == null)
				{
					this._routes = RouteTable.Routes;
				}
				return this._routes;
			}
			set
			{
				this._routes = value;
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000262B4 File Offset: 0x000244B4
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.IsChildAction)
			{
				throw new InvalidOperationException(MvcResources.RedirectAction_CannotRedirectInChildAction);
			}
			string text = UrlHelper.GenerateUrl(this.RouteName, null, null, this.RouteValues, this.Routes, context.RequestContext, false);
			if (string.IsNullOrEmpty(text))
			{
				throw new InvalidOperationException(MvcResources.Common_NoRouteMatched);
			}
			context.Controller.TempData.Keep();
			if (this.Permanent)
			{
				context.HttpContext.Response.RedirectPermanent(text, false);
				return;
			}
			context.HttpContext.Response.Redirect(text, false);
		}

		// Token: 0x040003CC RID: 972
		private RouteCollection _routes;
	}
}
