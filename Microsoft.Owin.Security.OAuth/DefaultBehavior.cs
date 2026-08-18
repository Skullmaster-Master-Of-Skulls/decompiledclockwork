using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200001F RID: 31
	internal static class DefaultBehavior
	{
		// Token: 0x04000068 RID: 104
		internal static readonly Func<OAuthValidateAuthorizeRequestContext, Task> ValidateAuthorizeRequest = delegate(OAuthValidateAuthorizeRequestContext context)
		{
			context.Validated();
			return Task.FromResult<object>(null);
		};

		// Token: 0x04000069 RID: 105
		internal static readonly Func<OAuthValidateTokenRequestContext, Task> ValidateTokenRequest = delegate(OAuthValidateTokenRequestContext context)
		{
			context.Validated();
			return Task.FromResult<object>(null);
		};

		// Token: 0x0400006A RID: 106
		internal static readonly Func<OAuthGrantAuthorizationCodeContext, Task> GrantAuthorizationCode = delegate(OAuthGrantAuthorizationCodeContext context)
		{
			if (context.Ticket != null && context.Ticket.Identity != null && context.Ticket.Identity.IsAuthenticated)
			{
				context.Validated();
			}
			return Task.FromResult<object>(null);
		};

		// Token: 0x0400006B RID: 107
		internal static readonly Func<OAuthGrantRefreshTokenContext, Task> GrantRefreshToken = delegate(OAuthGrantRefreshTokenContext context)
		{
			if (context.Ticket != null && context.Ticket.Identity != null && context.Ticket.Identity.IsAuthenticated)
			{
				context.Validated();
			}
			return Task.FromResult<object>(null);
		};
	}
}
