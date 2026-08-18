using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x020000E6 RID: 230
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AuthorizeAttribute : AuthorizationFilterAttribute
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00012282 File Offset: 0x00010482
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00012293 File Offset: 0x00010493
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

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000122A8 File Offset: 0x000104A8
		public override object TypeId
		{
			get
			{
				return this._typeId;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000122B0 File Offset: 0x000104B0
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x000122C1 File Offset: 0x000104C1
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

		// Token: 0x06000597 RID: 1431 RVA: 0x000122D8 File Offset: 0x000104D8
		protected virtual bool IsAuthorized(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			IPrincipal principal = actionContext.ControllerContext.RequestContext.Principal;
			return principal != null && principal.Identity != null && principal.Identity.IsAuthenticated && (this._usersSplit.Length <= 0 || this._usersSplit.Contains(principal.Identity.Name, StringComparer.OrdinalIgnoreCase)) && (this._rolesSplit.Length <= 0 || this._rolesSplit.Any(new Func<string, bool>(principal.IsInRole)));
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00012370 File Offset: 0x00010570
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (AuthorizeAttribute.SkipAuthorization(actionContext))
			{
				return;
			}
			if (!this.IsAuthorized(actionContext))
			{
				this.HandleUnauthorizedRequest(actionContext);
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00012399 File Offset: 0x00010599
		protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, SRResources.RequestNotAuthorized);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000123C9 File Offset: 0x000105C9
		private static bool SkipAuthorization(HttpActionContext actionContext)
		{
			return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>();
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00012534 File Offset: 0x00010734
		internal static string[] SplitString(string original)
		{
			if (string.IsNullOrEmpty(original))
			{
				return AuthorizeAttribute._emptyArray;
			}
			IEnumerable<string> source = from piece in original.Split(new char[]
			{
				','
			})
			let trimmed = piece.Trim()
			where !string.IsNullOrEmpty(trimmed)
			select trimmed;
			return source.ToArray<string>();
		}

		// Token: 0x04000197 RID: 407
		private static readonly string[] _emptyArray = new string[0];

		// Token: 0x04000198 RID: 408
		private readonly object _typeId = new object();

		// Token: 0x04000199 RID: 409
		private string _roles;

		// Token: 0x0400019A RID: 410
		private string[] _rolesSplit = AuthorizeAttribute._emptyArray;

		// Token: 0x0400019B RID: 411
		private string _users;

		// Token: 0x0400019C RID: 412
		private string[] _usersSplit = AuthorizeAttribute._emptyArray;
	}
}
