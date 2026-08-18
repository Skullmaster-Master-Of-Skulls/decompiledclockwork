using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E7 RID: 2279
	internal abstract class MsmqChannelListenerBase<TChannel> : MsmqChannelListenerBase, IChannelListener<TChannel>, IChannelListener, ICommunicationObject where TChannel : class, IChannel
	{
		// Token: 0x060056E1 RID: 22241 RVA: 0x0013EE38 File Offset: 0x0013D038
		protected MsmqChannelListenerBase(MsmqBindingElementBase bindingElement, BindingContext context, MsmqReceiveParameters receiveParameters, MessageEncoderFactory messageEncoderFactory) : base(bindingElement, context, receiveParameters, messageEncoderFactory)
		{
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x0013EE45 File Offset: 0x0013D045
		public override string Scheme
		{
			get
			{
				return "net.msmq";
			}
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x060056E3 RID: 22243 RVA: 0x0013EE4C File Offset: 0x0013D04C
		internal override UriPrefixTable<ITransportManagerRegistration> TransportManagerTable
		{
			get
			{
				return Msmq.StaticTransportManagerTable;
			}
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x0013EE53 File Offset: 0x0013D053
		internal override ITransportManagerRegistration CreateTransportManagerRegistration(Uri listenUri)
		{
			return null;
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x0013EE56 File Offset: 0x0013D056
		protected virtual void OnCloseCore(bool isAborting)
		{
		}

		// Token: 0x060056E6 RID: 22246 RVA: 0x0013EE58 File Offset: 0x0013D058
		protected virtual void OnOpenCore(TimeSpan timeout)
		{
			if (MsmqAuthenticationMode.Certificate == base.ReceiveParameters.TransportSecurity.MsmqAuthenticationMode)
			{
				SecurityUtils.OpenTokenAuthenticatorIfRequired(this.x509SecurityTokenAuthenticator, timeout);
			}
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x0013EE79 File Offset: 0x0013D079
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnCloseCore(false);
			return base.OnBeginClose(timeout, callback, state);
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x0013EE8B File Offset: 0x0013D08B
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseCore(false);
			base.OnClose(timeout);
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x0013EE9B File Offset: 0x0013D09B
		protected override void OnAbort()
		{
			this.OnCloseCore(true);
			base.OnAbort();
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x0013EEAC File Offset: 0x0013D0AC
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IAsyncResult result = base.OnBeginOpen(timeoutHelper.RemainingTime(), callback, state);
			this.OnOpenCore(timeoutHelper.RemainingTime());
			return result;
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x0013EEE0 File Offset: 0x0013D0E0
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.OnOpenCore(timeoutHelper.RemainingTime());
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x0013EF10 File Offset: 0x0013D110
		internal override IList<TransportManager> SelectTransportManagers()
		{
			UriPrefixTable<ITransportManagerRegistration> transportManagerTable = this.TransportManagerTable;
			lock (transportManagerTable)
			{
				ITransportManagerRegistration transportManagerRegistration;
				if (this.TransportManagerTable.TryLookupUri(this.Uri, HostNameComparisonMode.Exact, out transportManagerRegistration))
				{
					IList<TransportManager> list = transportManagerRegistration.Select(this);
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							list[i].Open(this);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x0013EF90 File Offset: 0x0013D190
		protected void SetSecurityTokenAuthenticator(string scheme, BindingContext context)
		{
			if (base.ReceiveParameters.TransportSecurity.MsmqAuthenticationMode == MsmqAuthenticationMode.Certificate)
			{
				SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
				if (securityCredentialsManager == null)
				{
					securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
				}
				SecurityTokenManager securityTokenManager = securityCredentialsManager.CreateSecurityTokenManager();
				SecurityTokenResolver securityTokenResolver;
				this.x509SecurityTokenAuthenticator = securityTokenManager.CreateSecurityTokenAuthenticator(new RecipientServiceModelSecurityTokenRequirement
				{
					TokenType = SecurityTokenTypes.X509Certificate,
					TransportScheme = scheme,
					ListenUri = this.Uri,
					KeyUsage = SecurityKeyUsage.Signature
				}, out securityTokenResolver);
			}
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x0013F008 File Offset: 0x0013D208
		internal SecurityMessageProperty ValidateSecurity(MsmqInputMessage msmqMessage)
		{
			SecurityMessageProperty securityMessageProperty = null;
			X509Certificate2 certificate = null;
			WindowsSidIdentity windowsSidIdentity = null;
			try
			{
				if (MsmqAuthenticationMode.Certificate == base.ReceiveParameters.TransportSecurity.MsmqAuthenticationMode)
				{
					try
					{
						byte[] bufferCopy = msmqMessage.SenderCertificate.GetBufferCopy(msmqMessage.SenderCertificateLength.Value);
						X509Helper.VerifyNotPfx(bufferCopy);
						certificate = new X509Certificate2(bufferCopy);
						X509SecurityToken token = new X509SecurityToken(certificate, false);
						ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = this.x509SecurityTokenAuthenticator.ValidateToken(token);
						securityMessageProperty = new SecurityMessageProperty
						{
							TransportToken = new SecurityTokenSpecification(token, readOnlyCollection),
							ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection)
						};
						goto IL_1A4;
					}
					catch (SecurityTokenValidationException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MsmqBadCertificate"), innerException));
					}
					catch (CryptographicException innerException2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MsmqBadCertificate"), innerException2));
					}
				}
				if (MsmqAuthenticationMode.WindowsDomain == base.ReceiveParameters.TransportSecurity.MsmqAuthenticationMode)
				{
					byte[] bufferCopy2 = msmqMessage.SenderId.GetBufferCopy(msmqMessage.SenderIdLength.Value);
					if (bufferCopy2.Length == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MsmqNoSid")));
					}
					SecurityIdentifier securityIdentifier = new SecurityIdentifier(bufferCopy2, 0);
					List<Claim> list = new List<Claim>(2);
					list.Add(new Claim(ClaimTypes.Sid, securityIdentifier, Rights.Identity));
					list.Add(Claim.CreateWindowsSidClaim(securityIdentifier));
					ClaimSet issuance = new DefaultClaimSet(ClaimSet.System, list);
					List<IAuthorizationPolicy> list2 = new List<IAuthorizationPolicy>(1);
					windowsSidIdentity = new WindowsSidIdentity(securityIdentifier);
					list2.Add(new UnconditionalPolicy(windowsSidIdentity, issuance));
					ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection2 = list2.AsReadOnly();
					securityMessageProperty = new SecurityMessageProperty
					{
						TransportToken = new SecurityTokenSpecification(null, readOnlyCollection2),
						ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection2)
					};
				}
				IL_1A4:;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (AuditLevel.Failure == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Failure))
				{
					this.WriteAuditEvent(AuditLevel.Failure, certificate, windowsSidIdentity, null);
				}
				throw;
			}
			if (securityMessageProperty != null && AuditLevel.Success == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Success))
			{
				this.WriteAuditEvent(AuditLevel.Success, certificate, windowsSidIdentity, null);
			}
			return securityMessageProperty;
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x0013F250 File Offset: 0x0013D450
		private void WriteAuditEvent(AuditLevel auditLevel, X509Certificate2 certificate, WindowsSidIdentity wsid, Exception exception)
		{
			try
			{
				string clientIdentity = string.Empty;
				if (certificate != null)
				{
					clientIdentity = SecurityUtils.GetCertificateId(certificate);
				}
				else if (wsid != null)
				{
					clientIdentity = SecurityUtils.GetIdentityName(wsid);
				}
				if (auditLevel == AuditLevel.Success)
				{
					SecurityAuditHelper.WriteTransportAuthenticationSuccessEvent(base.AuditBehavior.AuditLogLocation, base.AuditBehavior.SuppressAuditFailure, null, this.Uri, clientIdentity);
				}
				else
				{
					SecurityAuditHelper.WriteTransportAuthenticationFailureEvent(base.AuditBehavior.AuditLogLocation, base.AuditBehavior.SuppressAuditFailure, null, this.Uri, clientIdentity, exception);
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2) || auditLevel == AuditLevel.Success)
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
			}
		}

		// Token: 0x060056F0 RID: 22256
		public abstract TChannel AcceptChannel();

		// Token: 0x060056F1 RID: 22257
		public abstract IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state);

		// Token: 0x060056F2 RID: 22258
		public abstract TChannel AcceptChannel(TimeSpan timeout);

		// Token: 0x060056F3 RID: 22259
		public abstract IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060056F4 RID: 22260
		public abstract TChannel EndAcceptChannel(IAsyncResult result);

		// Token: 0x04003590 RID: 13712
		private SecurityTokenAuthenticator x509SecurityTokenAuthenticator;
	}
}
