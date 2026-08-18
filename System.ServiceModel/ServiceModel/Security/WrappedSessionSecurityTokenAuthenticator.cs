using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000363 RID: 867
	internal class WrappedSessionSecurityTokenAuthenticator : SecurityTokenAuthenticator, IIssuanceSecurityTokenAuthenticator, ICommunicationObject
	{
		// Token: 0x06001FC0 RID: 8128 RVA: 0x000770D0 File Offset: 0x000752D0
		public WrappedSessionSecurityTokenAuthenticator(SessionSecurityTokenHandler sessionTokenHandler, SecurityTokenAuthenticator wcfSessionAuthenticator, SctClaimsHandler sctClaimsHandler, ExceptionMapper exceptionMapper)
		{
			if (sessionTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sessionTokenHandler");
			}
			if (wcfSessionAuthenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wcfSessionAuthenticator");
			}
			if (sctClaimsHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sctClaimsHandler");
			}
			if (exceptionMapper == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exceptionMapper");
			}
			this._issuanceSecurityTokenAuthenticator = (wcfSessionAuthenticator as IIssuanceSecurityTokenAuthenticator);
			if (this._issuanceSecurityTokenAuthenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4244"));
			}
			this._communicationObject = (wcfSessionAuthenticator as ICommunicationObject);
			if (this._communicationObject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4245"));
			}
			this._sessionTokenHandler = sessionTokenHandler;
			this._sctClaimsHandler = sctClaimsHandler;
			this._exceptionMapper = exceptionMapper;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00077198 File Offset: 0x00075398
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			SecurityContextSecurityToken sct = token as SecurityContextSecurityToken;
			SessionSecurityToken token2 = SecurityContextSecurityTokenHelper.ConvertSctToSessionToken(sct);
			IEnumerable<ClaimsIdentity> identityCollection = null;
			try
			{
				identityCollection = this._sessionTokenHandler.ValidateToken(token2, this._sctClaimsHandler.EndpointId);
			}
			catch (Exception ex)
			{
				if (!this._exceptionMapper.HandleSecurityTokenProcessingException(ex))
				{
					throw;
				}
			}
			return new List<IAuthorizationPolicy>(new AuthorizationPolicy[]
			{
				new AuthorizationPolicy(identityCollection)
			}).AsReadOnly();
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0007720C File Offset: 0x0007540C
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is SecurityContextSecurityToken;
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x00077217 File Offset: 0x00075417
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x00077224 File Offset: 0x00075424
		public IssuedSecurityTokenHandler IssuedSecurityTokenHandler
		{
			get
			{
				return this._issuanceSecurityTokenAuthenticator.IssuedSecurityTokenHandler;
			}
			set
			{
				this._issuanceSecurityTokenAuthenticator.IssuedSecurityTokenHandler = value;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x00077232 File Offset: 0x00075432
		// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x0007723F File Offset: 0x0007543F
		public RenewedSecurityTokenHandler RenewedSecurityTokenHandler
		{
			get
			{
				return this._issuanceSecurityTokenAuthenticator.RenewedSecurityTokenHandler;
			}
			set
			{
				this._issuanceSecurityTokenAuthenticator.RenewedSecurityTokenHandler = value;
			}
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0007724D File Offset: 0x0007544D
		public void Abort()
		{
			this._communicationObject.Abort();
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0007725A File Offset: 0x0007545A
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._communicationObject.BeginClose(timeout, callback, state);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0007726A File Offset: 0x0007546A
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this._communicationObject.BeginClose(callback, state);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00077279 File Offset: 0x00075479
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._communicationObject.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x00077289 File Offset: 0x00075489
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this._communicationObject.BeginOpen(callback, state);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00077298 File Offset: 0x00075498
		public void Close(TimeSpan timeout)
		{
			this._communicationObject.Close(timeout);
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000772A6 File Offset: 0x000754A6
		public void Close()
		{
			this._communicationObject.Close();
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001FCE RID: 8142 RVA: 0x000772B3 File Offset: 0x000754B3
		// (remove) Token: 0x06001FCF RID: 8143 RVA: 0x000772C1 File Offset: 0x000754C1
		public event EventHandler Closed
		{
			add
			{
				this._communicationObject.Closed += value;
			}
			remove
			{
				this._communicationObject.Closed -= value;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001FD0 RID: 8144 RVA: 0x000772CF File Offset: 0x000754CF
		// (remove) Token: 0x06001FD1 RID: 8145 RVA: 0x000772DD File Offset: 0x000754DD
		public event EventHandler Closing
		{
			add
			{
				this._communicationObject.Closing += value;
			}
			remove
			{
				this._communicationObject.Closing -= value;
			}
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000772EB File Offset: 0x000754EB
		public void EndClose(IAsyncResult result)
		{
			this._communicationObject.EndClose(result);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000772F9 File Offset: 0x000754F9
		public void EndOpen(IAsyncResult result)
		{
			this._communicationObject.EndOpen(result);
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001FD4 RID: 8148 RVA: 0x00077307 File Offset: 0x00075507
		// (remove) Token: 0x06001FD5 RID: 8149 RVA: 0x00077315 File Offset: 0x00075515
		public event EventHandler Faulted
		{
			add
			{
				this._communicationObject.Faulted += value;
			}
			remove
			{
				this._communicationObject.Faulted -= value;
			}
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x00077323 File Offset: 0x00075523
		public void Open(TimeSpan timeout)
		{
			this._communicationObject.Open(timeout);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00077331 File Offset: 0x00075531
		public void Open()
		{
			this._communicationObject.Open();
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001FD8 RID: 8152 RVA: 0x0007733E File Offset: 0x0007553E
		// (remove) Token: 0x06001FD9 RID: 8153 RVA: 0x0007734C File Offset: 0x0007554C
		public event EventHandler Opened
		{
			add
			{
				this._communicationObject.Opened += value;
			}
			remove
			{
				this._communicationObject.Opened -= value;
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001FDA RID: 8154 RVA: 0x0007735A File Offset: 0x0007555A
		// (remove) Token: 0x06001FDB RID: 8155 RVA: 0x00077368 File Offset: 0x00075568
		public event EventHandler Opening
		{
			add
			{
				this._communicationObject.Opening += value;
			}
			remove
			{
				this._communicationObject.Opening -= value;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x00077376 File Offset: 0x00075576
		public CommunicationState State
		{
			get
			{
				return this._communicationObject.State;
			}
		}

		// Token: 0x04001EFA RID: 7930
		private SessionSecurityTokenHandler _sessionTokenHandler;

		// Token: 0x04001EFB RID: 7931
		private IIssuanceSecurityTokenAuthenticator _issuanceSecurityTokenAuthenticator;

		// Token: 0x04001EFC RID: 7932
		private ICommunicationObject _communicationObject;

		// Token: 0x04001EFD RID: 7933
		private SctClaimsHandler _sctClaimsHandler;

		// Token: 0x04001EFE RID: 7934
		private ExceptionMapper _exceptionMapper;
	}
}
