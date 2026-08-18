using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x020000A6 RID: 166
	internal class AuthenticationFilterTracer : FilterTracer, IAuthenticationFilter, IFilter, IDecorator<IAuthenticationFilter>
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000C2A1 File Offset: 0x0000A4A1
		public AuthenticationFilterTracer(IAuthenticationFilter innerFilter, ITraceWriter traceWriter) : base(innerFilter, traceWriter)
		{
			this._innerFilter = innerFilter;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000C2B2 File Offset: 0x0000A4B2
		public new IAuthenticationFilter Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			IPrincipal originalPrincipal = null;
			return base.TraceWriter.TraceBeginEndAsync((context != null) ? context.Request : null, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, "AuthenticateAsync", delegate(TraceRecord tr)
			{
				if (context != null)
				{
					originalPrincipal = context.Principal;
				}
			}, () => this._innerFilter.AuthenticateAsync(context, cancellationToken), delegate(TraceRecord tr)
			{
				if (context != null)
				{
					if (context.ErrorResult != null)
					{
						tr.Message = string.Format(CultureInfo.CurrentCulture, SRResources.AuthenticationFilterErrorResult, new object[]
						{
							context.ErrorResult
						});
						return;
					}
					if (context.Principal != originalPrincipal)
					{
						if (context.Principal == null || context.Principal.Identity == null)
						{
							tr.Message = SRResources.AuthenticationFilterSetPrincipalToUnknownIdentity;
							return;
						}
						tr.Message = string.Format(CultureInfo.CurrentCulture, SRResources.AuthenticationFilterSetPrincipalToKnownIdentity, new object[]
						{
							context.Principal.Identity.Name,
							context.Principal.Identity.AuthenticationType
						});
						return;
					}
					else
					{
						tr.Message = SRResources.AuthenticationFilterDidNothing;
					}
				}
			}, null);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000C49C File Offset: 0x0000A69C
		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			return base.TraceWriter.TraceBeginEndAsync((context != null) ? context.Request : null, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, "ChallengeAsync", null, () => this._innerFilter.ChallengeAsync(context, cancellationToken), null, null);
		}

		// Token: 0x0400011E RID: 286
		private const string AuthenticateAsyncMethodName = "AuthenticateAsync";

		// Token: 0x0400011F RID: 287
		private const string ChallengeAsyncMethodName = "ChallengeAsync";

		// Token: 0x04000120 RID: 288
		private readonly IAuthenticationFilter _innerFilter;
	}
}
