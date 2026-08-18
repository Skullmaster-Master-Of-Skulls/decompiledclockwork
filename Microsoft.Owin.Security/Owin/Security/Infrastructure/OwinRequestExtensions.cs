using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x0200001D RID: 29
	internal static class OwinRequestExtensions
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public static object RegisterAuthenticationHandler(this IOwinRequest request, AuthenticationHandler handler)
		{
			Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> chained = request.Get<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>(Constants.SecurityAuthenticate);
			OwinRequestExtensions.Hook hook = new OwinRequestExtensions.Hook(handler, chained);
			request.Set<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>(Constants.SecurityAuthenticate, new Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>(hook.AuthenticateAsync));
			return hook;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B04 File Offset: 0x00001D04
		public static void UnregisterAuthenticationHandler(this IOwinRequest request, object registration)
		{
			OwinRequestExtensions.Hook hook = registration as OwinRequestExtensions.Hook;
			if (hook == null)
			{
				throw new InvalidOperationException(Resources.Exception_UnhookAuthenticationStateType);
			}
			request.Set<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>(Constants.SecurityAuthenticate, hook.Chained);
		}

		// Token: 0x0200001E RID: 30
		private class Hook
		{
			// Token: 0x0600007C RID: 124 RVA: 0x00003B38 File Offset: 0x00001D38
			public Hook(AuthenticationHandler handler, Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> chained)
			{
				this._handler = handler;
				this.Chained = chained;
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600007D RID: 125 RVA: 0x00003B4E File Offset: 0x00001D4E
			// (set) Token: 0x0600007E RID: 126 RVA: 0x00003B56 File Offset: 0x00001D56
			public Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> Chained { get; private set; }

			// Token: 0x0600007F RID: 127 RVA: 0x00003DA8 File Offset: 0x00001FA8
			public async Task AuthenticateAsync(string[] authenticationTypes, Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object> callback, object state)
			{
				if (authenticationTypes == null)
				{
					callback(null, null, this._handler.BaseOptions.Description.Properties, state);
				}
				else if (authenticationTypes.Contains(this._handler.BaseOptions.AuthenticationType, StringComparer.Ordinal))
				{
					AuthenticationTicket ticket = await this._handler.AuthenticateAsync();
					if (ticket != null && ticket.Identity != null)
					{
						callback(ticket.Identity, ticket.Properties.Dictionary, this._handler.BaseOptions.Description.Properties, state);
					}
				}
				if (this.Chained != null)
				{
					await this.Chained(authenticationTypes, callback, state);
				}
			}

			// Token: 0x04000033 RID: 51
			private readonly AuthenticationHandler _handler;
		}
	}
}
