using System;
using System.Text;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200002A RID: 42
	public class OAuthValidateClientAuthenticationContext : BaseValidatingClientContext
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000071C6 File Offset: 0x000053C6
		public OAuthValidateClientAuthenticationContext(IOwinContext context, OAuthAuthorizationServerOptions options, IReadableStringCollection parameters) : base(context, options, null)
		{
			this.Parameters = parameters;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000071D8 File Offset: 0x000053D8
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000071E0 File Offset: 0x000053E0
		public IReadableStringCollection Parameters { get; private set; }

		// Token: 0x06000122 RID: 290 RVA: 0x000071E9 File Offset: 0x000053E9
		public bool Validated(string clientId)
		{
			base.ClientId = clientId;
			return this.Validated();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000071F8 File Offset: 0x000053F8
		public bool TryGetBasicCredentials(out string clientId, out string clientSecret)
		{
			string text = base.Request.Headers.Get("Authorization");
			if (!string.IsNullOrWhiteSpace(text) && text.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					byte[] bytes = Convert.FromBase64String(text.Substring("Basic ".Length).Trim());
					string @string = Encoding.UTF8.GetString(bytes);
					int num = @string.IndexOf(':');
					if (num >= 0)
					{
						clientId = @string.Substring(0, num);
						clientSecret = @string.Substring(num + 1);
						base.ClientId = clientId;
						return true;
					}
				}
				catch (FormatException)
				{
				}
				catch (ArgumentException)
				{
				}
			}
			clientId = null;
			clientSecret = null;
			return false;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000072B8 File Offset: 0x000054B8
		public bool TryGetFormCredentials(out string clientId, out string clientSecret)
		{
			clientId = this.Parameters.Get("client_id");
			if (!string.IsNullOrEmpty(clientId))
			{
				clientSecret = this.Parameters.Get("client_secret");
				base.ClientId = clientId;
				return true;
			}
			clientId = null;
			clientSecret = null;
			return false;
		}
	}
}
