using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x0200001B RID: 27
	public abstract class AuthenticationHandler
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F0D File Offset: 0x0000110D
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002F15 File Offset: 0x00001115
		private protected IOwinContext Context { protected get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002F1E File Offset: 0x0000111E
		protected IOwinRequest Request
		{
			get
			{
				return this.Context.Request;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002F2B File Offset: 0x0000112B
		protected IOwinResponse Response
		{
			get
			{
				return this.Context.Response;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002F38 File Offset: 0x00001138
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002F40 File Offset: 0x00001140
		private protected PathString RequestPathBase { protected get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002F49 File Offset: 0x00001149
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002F51 File Offset: 0x00001151
		private protected SecurityHelper Helper { protected get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002F5A File Offset: 0x0000115A
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002F62 File Offset: 0x00001162
		protected bool Faulted { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F6B File Offset: 0x0000116B
		internal AuthenticationOptions BaseOptions
		{
			get
			{
				return this._baseOptions;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000031B0 File Offset: 0x000013B0
		protected async Task BaseInitializeAsync(AuthenticationOptions options, IOwinContext context)
		{
			this._baseOptions = options;
			this.Context = context;
			this.Helper = new SecurityHelper(context);
			this.RequestPathBase = this.Request.PathBase;
			this._registration = this.Request.RegisterAuthenticationHandler(this);
			this.Response.OnSendingHeaders(new Action<object>(AuthenticationHandler.OnSendingHeaderCallback), this);
			await this.InitializeCoreAsync();
			if (this.BaseOptions.AuthenticationMode == AuthenticationMode.Active)
			{
				AuthenticationTicket ticket = await this.AuthenticateAsync();
				if (ticket != null && ticket.Identity != null)
				{
					this.Helper.AddUserIdentity(ticket.Identity);
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003208 File Offset: 0x00001408
		private static void OnSendingHeaderCallback(object state)
		{
			AuthenticationHandler authenticationHandler = (AuthenticationHandler)state;
			authenticationHandler.ApplyResponseAsync().Wait();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003227 File Offset: 0x00001427
		protected virtual Task InitializeCoreAsync()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000339C File Offset: 0x0000159C
		internal async Task TeardownAsync()
		{
			await this.ApplyResponseAsync();
			await this.TeardownCoreAsync();
			this.Request.UnregisterAuthenticationHandler(this._registration);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000033E2 File Offset: 0x000015E2
		protected virtual Task TeardownCoreAsync()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033EA File Offset: 0x000015EA
		public virtual Task<bool> InvokeAsync()
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033F2 File Offset: 0x000015F2
		public Task<AuthenticationTicket> AuthenticateAsync()
		{
			return LazyInitializer.EnsureInitialized<Task<AuthenticationTicket>>(ref this._authenticate, ref this._authenticateInitialized, ref this._authenticateSyncLock, new Func<Task<AuthenticationTicket>>(this.AuthenticateCoreAsync));
		}

		// Token: 0x0600006B RID: 107
		protected abstract Task<AuthenticationTicket> AuthenticateCoreAsync();

		// Token: 0x0600006C RID: 108 RVA: 0x00003550 File Offset: 0x00001750
		private async Task ApplyResponseAsync()
		{
			try
			{
				if (!this.Faulted)
				{
					await LazyInitializer.EnsureInitialized<Task>(ref this._applyResponse, ref this._applyResponseInitialized, ref this._applyResponseSyncLock, new Func<Task>(this.ApplyResponseCoreAsync));
				}
			}
			catch (Exception)
			{
				this.Faulted = true;
				throw;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000036DC File Offset: 0x000018DC
		protected virtual async Task ApplyResponseCoreAsync()
		{
			await this.ApplyResponseGrantAsync();
			await this.ApplyResponseChallengeAsync();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003722 File Offset: 0x00001922
		protected virtual Task ApplyResponseGrantAsync()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000372A File Offset: 0x0000192A
		protected virtual Task ApplyResponseChallengeAsync()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003734 File Offset: 0x00001934
		protected void GenerateCorrelationId(AuthenticationProperties properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			string key = ".AspNet.Correlation." + this.BaseOptions.AuthenticationType;
			byte[] data = new byte[32];
			AuthenticationHandler.Random.GetBytes(data);
			string value = TextEncodings.Base64Url.Encode(data);
			CookieOptions options = new CookieOptions
			{
				HttpOnly = true,
				Secure = this.Request.IsSecure
			};
			properties.Dictionary[key] = value;
			this.Response.Cookies.Append(key, value, options);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000037C8 File Offset: 0x000019C8
		protected void GenerateCorrelationId(ICookieManager cookieManager, AuthenticationProperties properties)
		{
			if (cookieManager == null)
			{
				throw new ArgumentNullException("cookieManager");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			string key = ".AspNet.Correlation." + this.BaseOptions.AuthenticationType;
			byte[] data = new byte[32];
			AuthenticationHandler.Random.GetBytes(data);
			string value = TextEncodings.Base64Url.Encode(data);
			CookieOptions options = new CookieOptions
			{
				HttpOnly = true,
				Secure = this.Request.IsSecure
			};
			properties.Dictionary[key] = value;
			cookieManager.AppendResponseCookie(this.Context, key, value, options);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003868 File Offset: 0x00001A68
		protected bool ValidateCorrelationId(AuthenticationProperties properties, ILogger logger)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			string text = ".AspNet.Correlation." + this.BaseOptions.AuthenticationType;
			string text2 = this.Request.Cookies[text];
			if (string.IsNullOrWhiteSpace(text2))
			{
				logger.WriteWarning("{0} cookie not found.", new string[]
				{
					text
				});
				return false;
			}
			CookieOptions options = new CookieOptions
			{
				HttpOnly = true,
				Secure = this.Request.IsSecure
			};
			this.Response.Cookies.Delete(text, options);
			string b;
			if (!properties.Dictionary.TryGetValue(text, out b))
			{
				logger.WriteWarning("{0} state property not found.", new string[]
				{
					text
				});
				return false;
			}
			properties.Dictionary.Remove(text);
			if (!string.Equals(text2, b, StringComparison.Ordinal))
			{
				logger.WriteWarning("{0} correlation cookie and state property mismatch.", new string[]
				{
					text
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003974 File Offset: 0x00001B74
		protected bool ValidateCorrelationId(ICookieManager cookieManager, AuthenticationProperties properties, ILogger logger)
		{
			if (cookieManager == null)
			{
				throw new ArgumentNullException("cookieManager");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			string text = ".AspNet.Correlation." + this.BaseOptions.AuthenticationType;
			string requestCookie = cookieManager.GetRequestCookie(this.Context, text);
			if (string.IsNullOrWhiteSpace(requestCookie))
			{
				logger.WriteWarning("{0} cookie not found.", new string[]
				{
					text
				});
				return false;
			}
			CookieOptions options = new CookieOptions
			{
				HttpOnly = true,
				Secure = this.Request.IsSecure
			};
			cookieManager.DeleteCookie(this.Context, text, options);
			string b;
			if (!properties.Dictionary.TryGetValue(text, out b))
			{
				logger.WriteWarning("{0} state property not found.", new string[]
				{
					text
				});
				return false;
			}
			properties.Dictionary.Remove(text);
			if (!string.Equals(requestCookie, b, StringComparison.Ordinal))
			{
				logger.WriteWarning("{0} correlation cookie and state property mismatch.", new string[]
				{
					text
				});
				return false;
			}
			return true;
		}

		// Token: 0x04000025 RID: 37
		private static readonly RNGCryptoServiceProvider Random = new RNGCryptoServiceProvider();

		// Token: 0x04000026 RID: 38
		private object _registration;

		// Token: 0x04000027 RID: 39
		private Task<AuthenticationTicket> _authenticate;

		// Token: 0x04000028 RID: 40
		private bool _authenticateInitialized;

		// Token: 0x04000029 RID: 41
		private object _authenticateSyncLock;

		// Token: 0x0400002A RID: 42
		private Task _applyResponse;

		// Token: 0x0400002B RID: 43
		private bool _applyResponseInitialized;

		// Token: 0x0400002C RID: 44
		private object _applyResponseSyncLock;

		// Token: 0x0400002D RID: 45
		private AuthenticationOptions _baseOptions;
	}
}
