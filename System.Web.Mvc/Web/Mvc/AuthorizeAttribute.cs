using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000175 RID: 373
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AuthorizeAttribute : FilterAttribute, IAuthorizationFilter
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001AD86 File Offset: 0x00018F86
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0001AD97 File Offset: 0x00018F97
		public string Roles
		{
			get
			{
				return this._roles ?? string.Empty;
			}
			set
			{
				this._roles = value;
				this._rolesSplit = AuthorizeAttribute.SplitString(value);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0001ADAC File Offset: 0x00018FAC
		public override object TypeId
		{
			get
			{
				return this._typeId;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0001ADC5 File Offset: 0x00018FC5
		public string Users
		{
			get
			{
				return this._users ?? string.Empty;
			}
			set
			{
				this._users = value;
				this._usersSplit = AuthorizeAttribute.SplitString(value);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001ADDC File Offset: 0x00018FDC
		protected virtual bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			IPrincipal user = httpContext.User;
			return user.Identity.IsAuthenticated && (this._usersSplit.Length <= 0 || this._usersSplit.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase)) && (this._rolesSplit.Length <= 0 || this._rolesSplit.Any(new Func<string, bool>(user.IsInRole)));
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001AE5F File Offset: 0x0001905F
		private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
		{
			validationStatus = this.OnCacheAuthorization(new HttpContextWrapper(context));
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001AE70 File Offset: 0x00019070
		public virtual void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
			{
				throw new InvalidOperationException(MvcResources.AuthorizeAttribute_CannotUseWithinChildActionCache);
			}
			bool flag = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
			if (flag)
			{
				return;
			}
			if (this.AuthorizeCore(filterContext.HttpContext))
			{
				HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
				cache.SetProxyMaxAge(new TimeSpan(0L));
				cache.AddValidationCallback(new HttpCacheValidateHandler(this.CacheValidateHandler), null);
				return;
			}
			this.HandleUnauthorizedRequest(filterContext);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001AF20 File Offset: 0x00019120
		protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new HttpUnauthorizedResult();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001AF30 File Offset: 0x00019130
		protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (!this.AuthorizeCore(httpContext))
			{
				return HttpValidationStatus.IgnoreThisRequest;
			}
			return HttpValidationStatus.Valid;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001B098 File Offset: 0x00019298
		internal static string[] SplitString(string original)
		{
			if (string.IsNullOrEmpty(original))
			{
				return new string[0];
			}
			IEnumerable<string> source = from piece in original.Split(AuthorizeAttribute._splitParameter)
			let trimmed = piece.Trim()
			where !string.IsNullOrEmpty(trimmed)
			select trimmed;
			return source.ToArray<string>();
		}

		// Token: 0x04000299 RID: 665
		private static readonly char[] _splitParameter = new char[]
		{
			','
		};

		// Token: 0x0400029A RID: 666
		private readonly object _typeId = new object();

		// Token: 0x0400029B RID: 667
		private string _roles;

		// Token: 0x0400029C RID: 668
		private string[] _rolesSplit = new string[0];

		// Token: 0x0400029D RID: 669
		private string _users;

		// Token: 0x0400029E RID: 670
		private string[] _usersSplit = new string[0];
	}
}
