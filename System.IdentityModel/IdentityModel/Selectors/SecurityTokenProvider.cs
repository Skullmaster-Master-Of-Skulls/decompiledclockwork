using System;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Threading;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A7 RID: 423
	public abstract class SecurityTokenProvider
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool SupportsTokenRenewal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool SupportsTokenCancellation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003F5CC File Offset: 0x0003D7CC
		public SecurityToken GetToken(TimeSpan timeout)
		{
			SecurityToken tokenCore = this.GetTokenCore(timeout);
			if (tokenCore == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("TokenProviderUnableToGetToken", new object[]
				{
					this
				})));
			}
			return tokenCore;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003F609 File Offset: 0x0003D809
		public IAsyncResult BeginGetToken(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginGetTokenCore(timeout, callback, state);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003F614 File Offset: 0x0003D814
		public SecurityToken EndGetToken(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			SecurityToken securityToken = this.EndGetTokenCore(result);
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("TokenProviderUnableToGetToken", new object[]
				{
					this
				})));
			}
			return securityToken;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003F664 File Offset: 0x0003D864
		public SecurityToken RenewToken(TimeSpan timeout, SecurityToken tokenToBeRenewed)
		{
			if (tokenToBeRenewed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenToBeRenewed");
			}
			SecurityToken securityToken = this.RenewTokenCore(timeout, tokenToBeRenewed);
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("TokenProviderUnableToRenewToken", new object[]
				{
					this
				})));
			}
			return securityToken;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003F6B5 File Offset: 0x0003D8B5
		public IAsyncResult BeginRenewToken(TimeSpan timeout, SecurityToken tokenToBeRenewed, AsyncCallback callback, object state)
		{
			if (tokenToBeRenewed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenToBeRenewed");
			}
			return this.BeginRenewTokenCore(timeout, tokenToBeRenewed, callback, state);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
		public SecurityToken EndRenewToken(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			SecurityToken securityToken = this.EndRenewTokenCore(result);
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("TokenProviderUnableToRenewToken", new object[]
				{
					this
				})));
			}
			return securityToken;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003F728 File Offset: 0x0003D928
		public void CancelToken(TimeSpan timeout, SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			this.CancelTokenCore(timeout, token);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003F745 File Offset: 0x0003D945
		public IAsyncResult BeginCancelToken(TimeSpan timeout, SecurityToken token, AsyncCallback callback, object state)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			return this.BeginCancelTokenCore(timeout, token, callback, state);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003F765 File Offset: 0x0003D965
		public void EndCancelToken(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			this.EndCancelTokenCore(result);
		}

		// Token: 0x06000DBF RID: 3519
		protected abstract SecurityToken GetTokenCore(TimeSpan timeout);

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003F781 File Offset: 0x0003D981
		protected virtual SecurityToken RenewTokenCore(TimeSpan timeout, SecurityToken tokenToBeRenewed)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("TokenRenewalNotSupported", new object[]
			{
				this
			})));
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003F7A6 File Offset: 0x0003D9A6
		protected virtual void CancelTokenCore(TimeSpan timeout, SecurityToken token)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("TokenCancellationNotSupported", new object[]
			{
				this
			})));
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003F7CC File Offset: 0x0003D9CC
		protected virtual IAsyncResult BeginGetTokenCore(TimeSpan timeout, AsyncCallback callback, object state)
		{
			SecurityToken token = this.GetToken(timeout);
			return new SecurityTokenProvider.SecurityTokenAsyncResult(token, callback, state);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003F7E9 File Offset: 0x0003D9E9
		protected virtual SecurityToken EndGetTokenCore(IAsyncResult result)
		{
			return SecurityTokenProvider.SecurityTokenAsyncResult.End(result);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		protected virtual IAsyncResult BeginRenewTokenCore(TimeSpan timeout, SecurityToken tokenToBeRenewed, AsyncCallback callback, object state)
		{
			SecurityToken token = this.RenewTokenCore(timeout, tokenToBeRenewed);
			return new SecurityTokenProvider.SecurityTokenAsyncResult(token, callback, state);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003F7E9 File Offset: 0x0003D9E9
		protected virtual SecurityToken EndRenewTokenCore(IAsyncResult result)
		{
			return SecurityTokenProvider.SecurityTokenAsyncResult.End(result);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003F813 File Offset: 0x0003DA13
		protected virtual IAsyncResult BeginCancelTokenCore(TimeSpan timeout, SecurityToken token, AsyncCallback callback, object state)
		{
			this.CancelToken(timeout, token);
			return new SecurityTokenProvider.SecurityTokenAsyncResult(null, callback, state);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003F826 File Offset: 0x0003DA26
		protected virtual void EndCancelTokenCore(IAsyncResult result)
		{
			SecurityTokenProvider.SecurityTokenAsyncResult.End(result);
		}

		// Token: 0x02000290 RID: 656
		protected internal class SecurityTokenAsyncResult : IAsyncResult
		{
			// Token: 0x06001348 RID: 4936 RVA: 0x000526F0 File Offset: 0x000508F0
			public SecurityTokenAsyncResult(SecurityToken token, AsyncCallback callback, object state)
			{
				this.token = token;
				this.state = state;
				if (callback != null)
				{
					try
					{
						callback(this);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(SR.GetString("AsyncCallbackException"), ex);
					}
				}
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x06001349 RID: 4937 RVA: 0x0005275C File Offset: 0x0005095C
			public object AsyncState
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x0600134A RID: 4938 RVA: 0x00052764 File Offset: 0x00050964
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					if (this.manualResetEvent != null)
					{
						return this.manualResetEvent;
					}
					object obj = this.thisLock;
					lock (obj)
					{
						if (this.manualResetEvent == null)
						{
							this.manualResetEvent = new ManualResetEvent(true);
						}
					}
					return this.manualResetEvent;
				}
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x0600134B RID: 4939 RVA: 0x00002434 File Offset: 0x00000634
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x0600134C RID: 4940 RVA: 0x00002434 File Offset: 0x00000634
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600134D RID: 4941 RVA: 0x000527C8 File Offset: 0x000509C8
			public static SecurityToken End(IAsyncResult result)
			{
				if (result == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
				}
				SecurityTokenProvider.SecurityTokenAsyncResult securityTokenAsyncResult = result as SecurityTokenProvider.SecurityTokenAsyncResult;
				if (securityTokenAsyncResult == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidAsyncResult"), "result"));
				}
				return securityTokenAsyncResult.token;
			}

			// Token: 0x0400112E RID: 4398
			private SecurityToken token;

			// Token: 0x0400112F RID: 4399
			private object state;

			// Token: 0x04001130 RID: 4400
			private ManualResetEvent manualResetEvent;

			// Token: 0x04001131 RID: 4401
			private object thisLock = new object();
		}
	}
}
