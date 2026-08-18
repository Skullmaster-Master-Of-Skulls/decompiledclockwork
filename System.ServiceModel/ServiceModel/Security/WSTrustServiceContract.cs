using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime;
using System.Security.Claims;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Security
{
	// Token: 0x02000377 RID: 887
	[ServiceBehavior(Name = "SecurityTokenService", Namespace = "http://schemas.microsoft.com/ws/2008/06/identity/securitytokenservice", InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class WSTrustServiceContract : IWSTrustFeb2005SyncContract, IWSTrust13SyncContract, IWSTrustFeb2005AsyncContract, IWSTrust13AsyncContract, IWsdlExportExtension, IContractBehavior
	{
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060020BE RID: 8382 RVA: 0x00078D4C File Offset: 0x00076F4C
		// (remove) Token: 0x060020BF RID: 8383 RVA: 0x00078D84 File Offset: 0x00076F84
		private event EventHandler<WSTrustRequestProcessingErrorEventArgs> _requestFailed;

		// Token: 0x060020C0 RID: 8384 RVA: 0x00078DB9 File Offset: 0x00076FB9
		public WSTrustServiceContract(SecurityTokenServiceConfiguration securityTokenServiceConfiguration)
		{
			if (securityTokenServiceConfiguration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenServiceConfiguration");
			}
			this._securityTokenServiceConfiguration = securityTokenServiceConfiguration;
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060020C1 RID: 8385 RVA: 0x00078DDB File Offset: 0x00076FDB
		// (remove) Token: 0x060020C2 RID: 8386 RVA: 0x00078DE4 File Offset: 0x00076FE4
		public event EventHandler<WSTrustRequestProcessingErrorEventArgs> RequestFailed
		{
			add
			{
				this._requestFailed += value;
			}
			remove
			{
				this._requestFailed -= value;
			}
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00078DF0 File Offset: 0x00076FF0
		protected virtual SecurityTokenResolver GetSecurityHeaderTokenResolver(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestContext");
			}
			List<SecurityToken> list = new List<SecurityToken>();
			if (requestContext.RequestMessage != null && requestContext.RequestMessage.Properties != null && requestContext.RequestMessage.Properties.Security != null)
			{
				SecurityMessageProperty security = requestContext.RequestMessage.Properties.Security;
				if (security.ProtectionToken != null)
				{
					list.Add(security.ProtectionToken.SecurityToken);
				}
				if (security.HasIncomingSupportingTokens)
				{
					foreach (SupportingTokenSpecification supportingTokenSpecification in security.IncomingSupportingTokens)
					{
						if (supportingTokenSpecification != null && (supportingTokenSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || supportingTokenSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing))
						{
							list.Add(supportingTokenSpecification.SecurityToken);
						}
					}
				}
				if (security.InitiatorToken != null)
				{
					list.Add(security.InitiatorToken.SecurityToken);
				}
			}
			if (list.Count > 0)
			{
				return SecurityTokenResolver.CreateDefaultSecurityTokenResolver(list.AsReadOnly(), true);
			}
			return EmptySecurityTokenResolver.Instance;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00078F08 File Offset: 0x00077108
		protected virtual SecurityTokenResolver GetRstSecurityTokenResolver()
		{
			if (this._securityTokenServiceConfiguration != null)
			{
				SecurityTokenResolver serviceTokenResolver = this._securityTokenServiceConfiguration.SecurityTokenHandlers.Configuration.ServiceTokenResolver;
				if (serviceTokenResolver != null && serviceTokenResolver != EmptySecurityTokenResolver.Instance)
				{
					return serviceTokenResolver;
				}
			}
			if (OperationContext.Current != null && OperationContext.Current.Host != null && OperationContext.Current.Host.Description != null)
			{
				ServiceCredentials serviceCredentials = OperationContext.Current.Host.Description.Behaviors.Find<ServiceCredentials>();
				if (serviceCredentials != null && serviceCredentials.ServiceCertificate != null && serviceCredentials.ServiceCertificate.Certificate != null)
				{
					return SecurityTokenResolver.CreateDefaultSecurityTokenResolver(new List<SecurityToken>(1)
					{
						new X509SecurityToken(serviceCredentials.ServiceCertificate.Certificate)
					}.AsReadOnly(), false);
				}
			}
			return EmptySecurityTokenResolver.Instance;
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x00078FC5 File Offset: 0x000771C5
		protected virtual WSTrustSerializationContext CreateSerializationContext()
		{
			return new WSTrustSerializationContext(this._securityTokenServiceConfiguration.SecurityTokenHandlerCollectionManager, this.GetRstSecurityTokenResolver(), this.GetSecurityHeaderTokenResolver(OperationContext.Current.RequestContext));
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00078FED File Offset: 0x000771ED
		protected virtual IAsyncResult BeginDispatchRequest(DispatchContext dispatchContext, AsyncCallback asyncCallback, object asyncState)
		{
			return new WSTrustServiceContract.DispatchRequestAsyncResult(dispatchContext, asyncCallback, asyncState);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00078FF7 File Offset: 0x000771F7
		protected virtual DispatchContext EndDispatchRequest(IAsyncResult ar)
		{
			return WSTrustServiceContract.DispatchRequestAsyncResult.End(ar);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00079000 File Offset: 0x00077200
		protected virtual void DispatchRequest(DispatchContext dispatchContext)
		{
			RequestSecurityToken requestSecurityToken = dispatchContext.RequestMessage as RequestSecurityToken;
			SecurityTokenService securityTokenService = dispatchContext.SecurityTokenService;
			ClaimsPrincipal principal = dispatchContext.Principal;
			if (requestSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3022")));
			}
			string requestType = requestSecurityToken.RequestType;
			if (requestType == "http://schemas.microsoft.com/idfx/requesttype/cancel")
			{
				dispatchContext.ResponseMessage = securityTokenService.Cancel(principal, requestSecurityToken);
				return;
			}
			if (requestType == "http://schemas.microsoft.com/idfx/requesttype/issue")
			{
				dispatchContext.ResponseMessage = securityTokenService.Issue(principal, requestSecurityToken);
				return;
			}
			if (requestType == "http://schemas.microsoft.com/idfx/requesttype/renew")
			{
				dispatchContext.ResponseMessage = securityTokenService.Renew(principal, requestSecurityToken);
				return;
			}
			if (!(requestType == "http://schemas.microsoft.com/idfx/requesttype/validate"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3112", new object[]
				{
					requestSecurityToken.RequestType
				})));
			}
			dispatchContext.ResponseMessage = securityTokenService.Validate(principal, requestSecurityToken);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000790E8 File Offset: 0x000772E8
		protected virtual System.ServiceModel.Channels.Message ProcessCore(System.ServiceModel.Channels.Message requestMessage, WSTrustRequestSerializer requestSerializer, WSTrustResponseSerializer responseSerializer, string requestAction, string responseAction, string trustNamespace)
		{
			if (requestMessage == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestMessage");
			}
			if (requestSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestSerializer");
			}
			if (responseSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseSerializer");
			}
			if (string.IsNullOrEmpty(requestAction))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestAction");
			}
			if (string.IsNullOrEmpty(responseAction))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseAction");
			}
			if (string.IsNullOrEmpty(trustNamespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustNamespace");
			}
			System.ServiceModel.Channels.Message result = null;
			try
			{
				WSTrustSerializationContext wstrustSerializationContext = this.CreateSerializationContext();
				DispatchContext dispatchContext = this.CreateDispatchContext(requestMessage, requestAction, responseAction, trustNamespace, requestSerializer, responseSerializer, wstrustSerializationContext);
				this.ValidateDispatchContext(dispatchContext);
				this.DispatchRequest(dispatchContext);
				result = System.ServiceModel.Channels.Message.CreateMessage(OperationContext.Current.RequestContext.RequestMessage.Version, dispatchContext.ResponseAction, new WSTrustResponseBodyWriter(dispatchContext.ResponseMessage, responseSerializer, wstrustSerializationContext));
			}
			catch (Exception ex)
			{
				if (!this.HandleException(ex, trustNamespace, requestAction, requestMessage.Version.Envelope))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00079200 File Offset: 0x00077400
		protected virtual DispatchContext CreateDispatchContext(System.ServiceModel.Channels.Message requestMessage, string requestAction, string responseAction, string trustNamespace, WSTrustRequestSerializer requestSerializer, WSTrustResponseSerializer responseSerializer, WSTrustSerializationContext serializationContext)
		{
			DispatchContext dispatchContext = new DispatchContext
			{
				Principal = OperationContext.Current.ClaimsPrincipal,
				RequestAction = requestAction,
				ResponseAction = responseAction,
				TrustNamespace = trustNamespace
			};
			XmlReader readerAtBodyContents = requestMessage.GetReaderAtBodyContents();
			if (requestSerializer.CanRead(readerAtBodyContents))
			{
				dispatchContext.RequestMessage = requestSerializer.ReadXml(readerAtBodyContents, serializationContext);
			}
			else
			{
				if (!responseSerializer.CanRead(readerAtBodyContents))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3114")));
				}
				dispatchContext.RequestMessage = responseSerializer.ReadXml(readerAtBodyContents, serializationContext);
			}
			dispatchContext.SecurityTokenService = this.CreateSTS();
			return dispatchContext;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000792A0 File Offset: 0x000774A0
		protected virtual void ValidateDispatchContext(DispatchContext dispatchContext)
		{
			if (dispatchContext.RequestMessage is RequestSecurityToken && !WSTrustServiceContract.IsValidRSTAction(dispatchContext))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3113", new object[]
				{
					"RequestSecurityToken",
					dispatchContext.RequestAction
				})));
			}
			if (dispatchContext.RequestMessage is RequestSecurityTokenResponse && !WSTrustServiceContract.IsValidRSTRAction(dispatchContext))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3113", new object[]
				{
					"RequestSecurityTokenResponse",
					dispatchContext.RequestAction
				})));
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0007933C File Offset: 0x0007753C
		private static bool IsValidRSTAction(DispatchContext dispatchContext)
		{
			bool result = false;
			string requestAction = dispatchContext.RequestAction;
			if (dispatchContext.TrustNamespace == "http://docs.oasis-open.org/ws-sx/ws-trust/200512" && (requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel" || requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue" || requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew" || requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate"))
			{
				result = true;
			}
			if (dispatchContext.TrustNamespace == "http://schemas.xmlsoap.org/ws/2005/02/trust" && (requestAction == "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel" || requestAction == "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue" || requestAction == "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew" || requestAction == "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate"))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000793E4 File Offset: 0x000775E4
		private static bool IsValidRSTRAction(DispatchContext dispatchContext)
		{
			bool result = false;
			string requestAction = dispatchContext.RequestAction;
			if (dispatchContext.TrustNamespace == "http://docs.oasis-open.org/ws-sx/ws-trust/200512")
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(requestAction);
				if (num <= 1294491626U)
				{
					if (num <= 749560815U)
					{
						if (num != 291947293U)
						{
							if (num != 749560815U)
							{
								goto IL_109;
							}
							if (!(requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal"))
							{
								goto IL_109;
							}
						}
						else if (!(requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal"))
						{
							goto IL_109;
						}
					}
					else if (num != 826051820U)
					{
						if (num != 1294491626U)
						{
							goto IL_109;
						}
						if (!(requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew"))
						{
							goto IL_109;
						}
					}
					else if (!(requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue"))
					{
						goto IL_109;
					}
				}
				else if (num <= 1946976173U)
				{
					if (num != 1653741127U)
					{
						if (num != 1946976173U)
						{
							goto IL_109;
						}
						if (!(requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal"))
						{
							goto IL_109;
						}
					}
					else if (!(requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel"))
					{
						goto IL_109;
					}
				}
				else if (num != 2152189335U)
				{
					if (num != 2227689702U)
					{
						goto IL_109;
					}
					if (!(requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal"))
					{
						goto IL_109;
					}
				}
				else if (!(requestAction == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Validate"))
				{
					goto IL_109;
				}
				result = true;
			}
			IL_109:
			if (dispatchContext.TrustNamespace == "http://schemas.xmlsoap.org/ws/2005/02/trust" && (requestAction == "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel" || requestAction == "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue" || requestAction == "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew" || requestAction == "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate"))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x00079544 File Offset: 0x00077744
		private SecurityTokenService CreateSTS()
		{
			SecurityTokenService securityTokenService = this._securityTokenServiceConfiguration.CreateSecurityTokenService();
			if (securityTokenService == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3002")));
			}
			return securityTokenService;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0007957C File Offset: 0x0007777C
		protected virtual IAsyncResult BeginProcessCore(System.ServiceModel.Channels.Message requestMessage, WSTrustRequestSerializer requestSerializer, WSTrustResponseSerializer responseSerializer, string requestAction, string responseAction, string trustNamespace, AsyncCallback callback, object state)
		{
			if (requestMessage == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			if (requestSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestSerializer");
			}
			if (responseSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseSerializer");
			}
			if (string.IsNullOrEmpty(requestAction))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestAction");
			}
			if (string.IsNullOrEmpty(responseAction))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseAction");
			}
			if (string.IsNullOrEmpty(trustNamespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustNamespace");
			}
			IAsyncResult result = null;
			try
			{
				WSTrustSerializationContext serializationContext = this.CreateSerializationContext();
				DispatchContext dispatchContext = this.CreateDispatchContext(requestMessage, requestAction, responseAction, trustNamespace, requestSerializer, responseSerializer, serializationContext);
				this.ValidateDispatchContext(dispatchContext);
				result = new WSTrustServiceContract.ProcessCoreAsyncResult(this, dispatchContext, OperationContext.Current.RequestContext.RequestMessage.Version, responseSerializer, serializationContext, callback, state);
			}
			catch (Exception ex)
			{
				if (!this.HandleException(ex, trustNamespace, requestAction, requestMessage.Version.Envelope))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00079680 File Offset: 0x00077880
		protected virtual System.ServiceModel.Channels.Message EndProcessCore(IAsyncResult ar, string requestAction, string responseAction, string trustNamespace)
		{
			if (ar == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ar");
			}
			WSTrustServiceContract.ProcessCoreAsyncResult processCoreAsyncResult = ar as WSTrustServiceContract.ProcessCoreAsyncResult;
			if (processCoreAsyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID2004", new object[]
				{
					typeof(WSTrustServiceContract.ProcessCoreAsyncResult),
					ar.GetType()
				}), "ar"));
			}
			System.ServiceModel.Channels.Message result = null;
			try
			{
				result = WSTrustServiceContract.ProcessCoreAsyncResult.End(ar);
			}
			catch (Exception ex)
			{
				if (!this.HandleException(ex, trustNamespace, requestAction, processCoreAsyncResult.MessageVersion.Envelope))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00079720 File Offset: 0x00077920
		protected virtual bool HandleException(Exception ex, string trustNamespace, string action, EnvelopeVersion requestEnvelopeVersion)
		{
			if (Fx.IsFatal(ex))
			{
				return false;
			}
			if (DiagnosticUtility.ShouldTrace(TraceEventType.Warning))
			{
				TraceUtility.TraceString(TraceEventType.Warning, "RequestFailed: TrustNamespace={0}, Action={1}, Exception={2}", new object[]
				{
					trustNamespace,
					action,
					ex
				});
			}
			if (this._requestFailed != null)
			{
				this._requestFailed(this, new WSTrustRequestProcessingErrorEventArgs(action, ex));
			}
			bool flag = false;
			ServiceDebugBehavior serviceDebugBehavior = OperationContext.Current.Host.Description.Behaviors.Find<ServiceDebugBehavior>();
			if (serviceDebugBehavior != null)
			{
				flag = serviceDebugBehavior.IncludeExceptionDetailInFaults;
			}
			if (string.IsNullOrEmpty(trustNamespace) || string.IsNullOrEmpty(action) || flag || ex is FaultException)
			{
				return false;
			}
			FaultException ex2 = OperationContext.Current.Host.Credentials.ExceptionMapper.FromException(ex, (requestEnvelopeVersion == EnvelopeVersion.Soap11) ? "http://schemas.xmlsoap.org/soap/envelope/" : "http://www.w3.org/2003/05/soap-envelope", trustNamespace);
			if (ex2 != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2);
			}
			return false;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000797FD File Offset: 0x000779FD
		public System.ServiceModel.Channels.Message ProcessTrust13Cancel(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0007982B File Offset: 0x00077A2B
		public System.ServiceModel.Channels.Message ProcessTrust13Issue(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00079859 File Offset: 0x00077A59
		public System.ServiceModel.Channels.Message ProcessTrust13Renew(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00079887 File Offset: 0x00077A87
		public System.ServiceModel.Channels.Message ProcessTrust13Validate(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000798B5 File Offset: 0x00077AB5
		public System.ServiceModel.Channels.Message ProcessTrust13CancelResponse(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000798E3 File Offset: 0x00077AE3
		public System.ServiceModel.Channels.Message ProcessTrust13IssueResponse(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00079911 File Offset: 0x00077B11
		public System.ServiceModel.Channels.Message ProcessTrust13RenewResponse(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0007993F File Offset: 0x00077B3F
		public System.ServiceModel.Channels.Message ProcessTrust13ValidateResponse(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Validate", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0007996D File Offset: 0x00077B6D
		public System.ServiceModel.Channels.Message ProcessTrustFeb2005Cancel(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0007999B File Offset: 0x00077B9B
		public System.ServiceModel.Channels.Message ProcessTrustFeb2005Issue(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000799C9 File Offset: 0x00077BC9
		public System.ServiceModel.Channels.Message ProcessTrustFeb2005Renew(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000799F7 File Offset: 0x00077BF7
		public System.ServiceModel.Channels.Message ProcessTrustFeb2005Validate(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00079A25 File Offset: 0x00077C25
		public System.ServiceModel.Channels.Message ProcessTrustFeb2005CancelResponse(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00079A53 File Offset: 0x00077C53
		public System.ServiceModel.Channels.Message ProcessTrustFeb2005IssueResponse(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00079A81 File Offset: 0x00077C81
		public System.ServiceModel.Channels.Message ProcessTrustFeb2005RenewResponse(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00079AAF File Offset: 0x00077CAF
		public System.ServiceModel.Channels.Message ProcessTrustFeb2005ValidateResponse(System.ServiceModel.Channels.Message message)
		{
			return this.ProcessCore(message, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00079ADD File Offset: 0x00077CDD
		public SecurityTokenServiceConfiguration SecurityTokenServiceConfiguration
		{
			get
			{
				return this._securityTokenServiceConfiguration;
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00079AE8 File Offset: 0x00077CE8
		public IAsyncResult BeginTrust13Cancel(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", callback, state);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00079B23 File Offset: 0x00077D23
		public System.ServiceModel.Channels.Message EndTrust13Cancel(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00079B3C File Offset: 0x00077D3C
		public IAsyncResult BeginTrust13Issue(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", callback, state);
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x00079B77 File Offset: 0x00077D77
		public System.ServiceModel.Channels.Message EndTrust13Issue(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00079B90 File Offset: 0x00077D90
		public IAsyncResult BeginTrust13Renew(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", callback, state);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00079BCB File Offset: 0x00077DCB
		public System.ServiceModel.Channels.Message EndTrust13Renew(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00079BE4 File Offset: 0x00077DE4
		public IAsyncResult BeginTrust13Validate(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", callback, state);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00079C1F File Offset: 0x00077E1F
		public System.ServiceModel.Channels.Message EndTrust13Validate(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00079C38 File Offset: 0x00077E38
		public IAsyncResult BeginTrust13CancelResponse(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", callback, state);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00079C73 File Offset: 0x00077E73
		public System.ServiceModel.Channels.Message EndTrust13CancelResponse(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00079C8C File Offset: 0x00077E8C
		public IAsyncResult BeginTrust13IssueResponse(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", callback, state);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00079CC7 File Offset: 0x00077EC7
		public System.ServiceModel.Channels.Message EndTrust13IssueResponse(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00079CE0 File Offset: 0x00077EE0
		public IAsyncResult BeginTrust13RenewResponse(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", callback, state);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00079D1B File Offset: 0x00077F1B
		public System.ServiceModel.Channels.Message EndTrust13RenewResponse(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00079D34 File Offset: 0x00077F34
		public IAsyncResult BeginTrust13ValidateResponse(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrust13RequestSerializer, this._securityTokenServiceConfiguration.WSTrust13ResponseSerializer, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Validate", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", callback, state);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00079D6F File Offset: 0x00077F6F
		public System.ServiceModel.Channels.Message EndTrust13ValidateResponse(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Validate", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00079D88 File Offset: 0x00077F88
		public IAsyncResult BeginTrustFeb2005Cancel(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust", callback, state);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00079DC3 File Offset: 0x00077FC3
		public System.ServiceModel.Channels.Message EndTrustFeb2005Cancel(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00079DDC File Offset: 0x00077FDC
		public IAsyncResult BeginTrustFeb2005Issue(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust", callback, state);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00079E17 File Offset: 0x00078017
		public System.ServiceModel.Channels.Message EndTrustFeb2005Issue(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00079E30 File Offset: 0x00078030
		public IAsyncResult BeginTrustFeb2005Renew(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust", callback, state);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00079E6B File Offset: 0x0007806B
		public System.ServiceModel.Channels.Message EndTrustFeb2005Renew(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x00079E84 File Offset: 0x00078084
		public IAsyncResult BeginTrustFeb2005Validate(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust", callback, state);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00079EBF File Offset: 0x000780BF
		public System.ServiceModel.Channels.Message EndTrustFeb2005Validate(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00079ED8 File Offset: 0x000780D8
		public IAsyncResult BeginTrustFeb2005CancelResponse(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust", callback, state);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00079F13 File Offset: 0x00078113
		public System.ServiceModel.Channels.Message EndTrustFeb2005CancelResponse(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00079F2C File Offset: 0x0007812C
		public IAsyncResult BeginTrustFeb2005IssueResponse(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust", callback, state);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00079F67 File Offset: 0x00078167
		public System.ServiceModel.Channels.Message EndTrustFeb2005IssueResponse(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00079F80 File Offset: 0x00078180
		public IAsyncResult BeginTrustFeb2005RenewResponse(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust", callback, state);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00079FBB File Offset: 0x000781BB
		public System.ServiceModel.Channels.Message EndTrustFeb2005RenewResponse(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00079FD4 File Offset: 0x000781D4
		public IAsyncResult BeginTrustFeb2005ValidateResponse(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
		{
			return this.BeginProcessCore(request, this._securityTokenServiceConfiguration.WSTrustFeb2005RequestSerializer, this._securityTokenServiceConfiguration.WSTrustFeb2005ResponseSerializer, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust", callback, state);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0007A00F File Offset: 0x0007820F
		public System.ServiceModel.Channels.Message EndTrustFeb2005ValidateResponse(IAsyncResult ar)
		{
			return this.EndProcessCore(ar, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0007A027 File Offset: 0x00078227
		public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0007A029 File Offset: 0x00078229
		public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0007A02B File Offset: 0x0007822B
		public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0007A02D File Offset: 0x0007822D
		public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0007A02F File Offset: 0x0007822F
		public virtual void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0007A034 File Offset: 0x00078234
		public virtual void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.WsdlPort == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3146"));
			}
			if (context.WsdlPort.Service == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3147"));
			}
			if (context.WsdlPort.Service.ServiceDescription == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3148"));
			}
			System.Web.Services.Description.ServiceDescription serviceDescription = context.WsdlPort.Service.ServiceDescription;
			foreach (object obj in serviceDescription.PortTypes)
			{
				PortType portType = (PortType)obj;
				if (StringComparer.Ordinal.Equals(portType.Name, "IWSTrustFeb2005Sync"))
				{
					this.IncludeNamespace(context, "t", "http://schemas.xmlsoap.org/ws/2005/02/trust");
					this.ImportSchema(exporter, context, "http://schemas.xmlsoap.org/ws/2005/02/trust");
					this.FixMessageElement(serviceDescription, portType, context, "TrustFeb2005Cancel", new XmlQualifiedName("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"), new XmlQualifiedName("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust"));
					this.FixMessageElement(serviceDescription, portType, context, "TrustFeb2005Issue", new XmlQualifiedName("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"), new XmlQualifiedName("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust"));
					this.FixMessageElement(serviceDescription, portType, context, "TrustFeb2005Renew", new XmlQualifiedName("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"), new XmlQualifiedName("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust"));
					this.FixMessageElement(serviceDescription, portType, context, "TrustFeb2005Validate", new XmlQualifiedName("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"), new XmlQualifiedName("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust"));
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(portType.Name, "IWSTrust13Sync"))
				{
					this.IncludeNamespace(context, "trust", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
					this.ImportSchema(exporter, context, "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
					this.FixMessageElement(serviceDescription, portType, context, "Trust13Cancel", new XmlQualifiedName("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"), new XmlQualifiedName("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"));
					this.FixMessageElement(serviceDescription, portType, context, "Trust13Issue", new XmlQualifiedName("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"), new XmlQualifiedName("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"));
					this.FixMessageElement(serviceDescription, portType, context, "Trust13Renew", new XmlQualifiedName("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"), new XmlQualifiedName("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"));
					this.FixMessageElement(serviceDescription, portType, context, "Trust13Validate", new XmlQualifiedName("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"), new XmlQualifiedName("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"));
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(portType.Name, "IWSTrustFeb2005Async"))
				{
					this.IncludeNamespace(context, "t", "http://schemas.xmlsoap.org/ws/2005/02/trust");
					this.ImportSchema(exporter, context, "http://schemas.xmlsoap.org/ws/2005/02/trust");
					this.FixMessageElement(serviceDescription, portType, context, "TrustFeb2005CancelAsync", new XmlQualifiedName("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"), new XmlQualifiedName("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust"));
					this.FixMessageElement(serviceDescription, portType, context, "TrustFeb2005IssueAsync", new XmlQualifiedName("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"), new XmlQualifiedName("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust"));
					this.FixMessageElement(serviceDescription, portType, context, "TrustFeb2005RenewAsync", new XmlQualifiedName("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"), new XmlQualifiedName("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust"));
					this.FixMessageElement(serviceDescription, portType, context, "TrustFeb2005ValidateAsync", new XmlQualifiedName("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"), new XmlQualifiedName("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust"));
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(portType.Name, "IWSTrust13Async"))
				{
					this.IncludeNamespace(context, "trust", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
					this.ImportSchema(exporter, context, "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
					this.FixMessageElement(serviceDescription, portType, context, "Trust13CancelAsync", new XmlQualifiedName("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"), new XmlQualifiedName("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"));
					this.FixMessageElement(serviceDescription, portType, context, "Trust13IssueAsync", new XmlQualifiedName("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"), new XmlQualifiedName("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"));
					this.FixMessageElement(serviceDescription, portType, context, "Trust13RenewAsync", new XmlQualifiedName("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"), new XmlQualifiedName("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"));
					this.FixMessageElement(serviceDescription, portType, context, "Trust13ValidateAsync", new XmlQualifiedName("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"), new XmlQualifiedName("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"));
				}
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0007A4D8 File Offset: 0x000786D8
		protected virtual void IncludeNamespace(WsdlEndpointConversionContext context, string prefix, string ns)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (string.IsNullOrEmpty(prefix))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("prefix");
			}
			if (string.IsNullOrEmpty(ns))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("ns");
			}
			bool flag = false;
			XmlQualifiedName[] array = context.WsdlBinding.ServiceDescription.Namespaces.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (StringComparer.Ordinal.Equals(array[i].Namespace, ns))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				context.WsdlBinding.ServiceDescription.Namespaces.Add(prefix, ns);
			}
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0007A578 File Offset: 0x00078778
		protected virtual void ImportSchema(WsdlExporter exporter, WsdlEndpointConversionContext context, string ns)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (string.IsNullOrEmpty(ns))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("ns");
			}
			foreach (object obj in context.WsdlPort.Service.ServiceDescription.Types.Schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Includes)
				{
					XmlSchemaImport xmlSchemaImport = xmlSchemaObject as XmlSchemaImport;
					if (xmlSchemaImport != null && StringComparer.Ordinal.Equals(xmlSchemaImport.Namespace, ns))
					{
						return;
					}
				}
			}
			XmlSchema xmlSchema2 = WSTrustServiceContract.GetXmlSchema(exporter, ns);
			if (xmlSchema2 == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3004", new object[]
				{
					ns
				}));
			}
			XmlSchema xmlSchema3;
			if (context.WsdlPort.Service.ServiceDescription.Types.Schemas.Count == 0)
			{
				xmlSchema3 = new XmlSchema();
				context.WsdlPort.Service.ServiceDescription.Types.Schemas.Add(xmlSchema3);
			}
			else
			{
				xmlSchema3 = context.WsdlPort.Service.ServiceDescription.Types.Schemas[0];
			}
			XmlSchemaImport xmlSchemaImport2 = new XmlSchemaImport();
			xmlSchemaImport2.Namespace = ns;
			exporter.GeneratedXmlSchemas.Add(xmlSchema2);
			xmlSchema3.Includes.Add(xmlSchemaImport2);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x0007A740 File Offset: 0x00078940
		private static XmlSchema GetXmlSchema(WsdlExporter exporter, string ns)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (string.IsNullOrEmpty(ns))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("ns");
			}
			ICollection collection = exporter.GeneratedXmlSchemas.Schemas(ns);
			if (collection != null && collection.Count > 0)
			{
				using (IEnumerator enumerator = collection.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return (XmlSchema)enumerator.Current;
					}
				}
			}
			string s;
			if (!(ns == "http://schemas.xmlsoap.org/ws/2005/02/trust"))
			{
				if (!(ns == "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID5004", new object[]
					{
						ns
					}));
				}
				s = "<?xml version='1.0' encoding='utf-8'?>\r\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'\r\n           xmlns:trust='http://docs.oasis-open.org/ws-sx/ws-trust/200512'\r\n           targetNamespace='http://docs.oasis-open.org/ws-sx/ws-trust/200512'\r\n           elementFormDefault='qualified' >\r\n\r\n<xs:element name='RequestSecurityToken' type='trust:RequestSecurityTokenType' />\r\n  <xs:complexType name='RequestSecurityTokenType' >\r\n    <xs:choice minOccurs='0' maxOccurs='unbounded' >\r\n        <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded' />\r\n    </xs:choice>\r\n    <xs:attribute name='Context' type='xs:anyURI' use='optional' />\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n<xs:element name='RequestSecurityTokenResponse' type='trust:RequestSecurityTokenResponseType' />\r\n  <xs:complexType name='RequestSecurityTokenResponseType' >\r\n    <xs:choice minOccurs='0' maxOccurs='unbounded' >\r\n        <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded' />\r\n    </xs:choice>\r\n    <xs:attribute name='Context' type='xs:anyURI' use='optional' />\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n  <xs:element name='RequestSecurityTokenResponseCollection' type='trust:RequestSecurityTokenResponseCollectionType' />\r\n  <xs:complexType name='RequestSecurityTokenResponseCollectionType' >\r\n    <xs:sequence>\r\n      <xs:element ref='trust:RequestSecurityTokenResponse' minOccurs='1' maxOccurs='unbounded' />\r\n    </xs:sequence>\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n        </xs:schema>";
			}
			else
			{
				s = "<?xml version='1.0' encoding='utf-8'?>\r\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'\r\n           xmlns:wst='http://schemas.xmlsoap.org/ws/2005/02/trust'\r\n           targetNamespace='http://schemas.xmlsoap.org/ws/2005/02/trust'\r\n           elementFormDefault='qualified' >\r\n\r\n<xs:element name='RequestSecurityToken' type='wst:RequestSecurityTokenType' />\r\n  <xs:complexType name='RequestSecurityTokenType' >\r\n    <xs:choice minOccurs='0' maxOccurs='unbounded' >\r\n        <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded' />\r\n    </xs:choice>\r\n    <xs:attribute name='Context' type='xs:anyURI' use='optional' />\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n<xs:element name='RequestSecurityTokenResponse' type='wst:RequestSecurityTokenResponseType' />\r\n  <xs:complexType name='RequestSecurityTokenResponseType' >\r\n    <xs:choice minOccurs='0' maxOccurs='unbounded' >\r\n        <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded' />\r\n    </xs:choice>\r\n    <xs:attribute name='Context' type='xs:anyURI' use='optional' />\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n        </xs:schema>";
			}
			StringReader input = new StringReader(s);
			return XmlSchema.Read(new XmlTextReader(input)
			{
				DtdProcessing = DtdProcessing.Prohibit
			}, null);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0007A840 File Offset: 0x00078A40
		protected virtual void FixMessageElement(System.Web.Services.Description.ServiceDescription serviceDescription, PortType portType, WsdlEndpointConversionContext context, string operationName, XmlQualifiedName inputMessageElement, XmlQualifiedName outputMessageElement)
		{
			if (serviceDescription == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceDescription");
			}
			if (portType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("portType");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (string.IsNullOrEmpty(operationName))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("operationName");
			}
			if (inputMessageElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inputMessageElement");
			}
			if (outputMessageElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("outputMessageElement");
			}
			Operation operation = null;
			System.Web.Services.Description.Message message = null;
			System.Web.Services.Description.Message message2 = null;
			foreach (object obj in portType.Operations)
			{
				Operation operation2 = (Operation)obj;
				if (StringComparer.Ordinal.Equals(operation2.Name, operationName))
				{
					operation = operation2;
					foreach (object obj2 in serviceDescription.Messages)
					{
						System.Web.Services.Description.Message message3 = (System.Web.Services.Description.Message)obj2;
						if (StringComparer.Ordinal.Equals(message3.Name, operation2.Messages.Input.Message.Name))
						{
							if (message3.Parts.Count != 1)
							{
								throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3144", new object[]
								{
									portType.Name,
									operation2.Name,
									message3.Name,
									message3.Parts.Count
								}));
							}
							message = message3;
						}
						else if (StringComparer.Ordinal.Equals(message3.Name, operation2.Messages.Output.Message.Name))
						{
							if (message3.Parts.Count != 1)
							{
								throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3144", new object[]
								{
									portType.Name,
									operation2.Name,
									message3.Name,
									message3.Parts.Count
								}));
							}
							message2 = message3;
						}
						if (message != null && message2 != null)
						{
							break;
						}
					}
				}
				if (operation != null)
				{
					break;
				}
			}
			if (operation == null)
			{
				return;
			}
			if (message == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3149", new object[]
				{
					portType.Name,
					portType.Namespaces,
					operationName
				}));
			}
			if (message2 == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3150", new object[]
				{
					portType.Name,
					portType.Namespaces,
					operationName
				}));
			}
			message.Parts[0].Element = inputMessageElement;
			message2.Parts[0].Element = outputMessageElement;
			message.Parts[0].Type = null;
			message2.Parts[0].Type = null;
		}

		// Token: 0x04001F29 RID: 7977
		private const string soap11Namespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x04001F2A RID: 7978
		private const string soap12Namespace = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x04001F2B RID: 7979
		private SecurityTokenServiceConfiguration _securityTokenServiceConfiguration;

		// Token: 0x02000B96 RID: 2966
		internal class ProcessCoreAsyncResult : AsyncResult
		{
			// Token: 0x0600735B RID: 29531 RVA: 0x001AE43C File Offset: 0x001AC63C
			public ProcessCoreAsyncResult(WSTrustServiceContract contract, DispatchContext dispatchContext, MessageVersion messageVersion, WSTrustResponseSerializer responseSerializer, WSTrustSerializationContext serializationContext, AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
			{
				if (contract == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
				}
				if (dispatchContext == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dispatchContext");
				}
				if (responseSerializer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseSerializer");
				}
				if (serializationContext == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializationContext");
				}
				this._trustServiceContract = contract;
				this._dispatchContext = dispatchContext;
				this._messageVersion = messageVersion;
				this._responseSerializer = responseSerializer;
				this._serializationContext = serializationContext;
				contract.BeginDispatchRequest(dispatchContext, new AsyncCallback(this.OnDispatchRequestCompleted), null);
			}

			// Token: 0x17001ABB RID: 6843
			// (get) Token: 0x0600735C RID: 29532 RVA: 0x001AE4DB File Offset: 0x001AC6DB
			public WSTrustServiceContract TrustServiceContract
			{
				get
				{
					return this._trustServiceContract;
				}
			}

			// Token: 0x17001ABC RID: 6844
			// (get) Token: 0x0600735D RID: 29533 RVA: 0x001AE4E3 File Offset: 0x001AC6E3
			public DispatchContext DispatchContext
			{
				get
				{
					return this._dispatchContext;
				}
			}

			// Token: 0x17001ABD RID: 6845
			// (get) Token: 0x0600735E RID: 29534 RVA: 0x001AE4EB File Offset: 0x001AC6EB
			public MessageVersion MessageVersion
			{
				get
				{
					return this._messageVersion;
				}
			}

			// Token: 0x17001ABE RID: 6846
			// (get) Token: 0x0600735F RID: 29535 RVA: 0x001AE4F3 File Offset: 0x001AC6F3
			public WSTrustResponseSerializer ResponseSerializer
			{
				get
				{
					return this._responseSerializer;
				}
			}

			// Token: 0x17001ABF RID: 6847
			// (get) Token: 0x06007360 RID: 29536 RVA: 0x001AE4FB File Offset: 0x001AC6FB
			public WSTrustSerializationContext SerializationContext
			{
				get
				{
					return this._serializationContext;
				}
			}

			// Token: 0x06007361 RID: 29537 RVA: 0x001AE504 File Offset: 0x001AC704
			public new static System.ServiceModel.Channels.Message End(IAsyncResult ar)
			{
				AsyncResult.End(ar);
				WSTrustServiceContract.ProcessCoreAsyncResult processCoreAsyncResult = ar as WSTrustServiceContract.ProcessCoreAsyncResult;
				if (processCoreAsyncResult == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2004", new object[]
					{
						typeof(WSTrustServiceContract.ProcessCoreAsyncResult),
						ar.GetType()
					}));
				}
				return System.ServiceModel.Channels.Message.CreateMessage(OperationContext.Current.RequestContext.RequestMessage.Version, processCoreAsyncResult.DispatchContext.ResponseAction, new WSTrustResponseBodyWriter(processCoreAsyncResult.DispatchContext.ResponseMessage, processCoreAsyncResult.ResponseSerializer, processCoreAsyncResult.SerializationContext));
			}

			// Token: 0x06007362 RID: 29538 RVA: 0x001AE590 File Offset: 0x001AC790
			private void OnDispatchRequestCompleted(IAsyncResult ar)
			{
				try
				{
					this._dispatchContext = this._trustServiceContract.EndDispatchRequest(ar);
					base.Complete(false);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Complete(false, exception);
				}
			}

			// Token: 0x04004131 RID: 16689
			private WSTrustServiceContract _trustServiceContract;

			// Token: 0x04004132 RID: 16690
			private DispatchContext _dispatchContext;

			// Token: 0x04004133 RID: 16691
			private MessageVersion _messageVersion;

			// Token: 0x04004134 RID: 16692
			private WSTrustResponseSerializer _responseSerializer;

			// Token: 0x04004135 RID: 16693
			private WSTrustSerializationContext _serializationContext;
		}

		// Token: 0x02000B97 RID: 2967
		internal class DispatchRequestAsyncResult : AsyncResult
		{
			// Token: 0x17001AC0 RID: 6848
			// (get) Token: 0x06007363 RID: 29539 RVA: 0x001AE5E0 File Offset: 0x001AC7E0
			public DispatchContext DispatchContext
			{
				get
				{
					return this._dispatchContext;
				}
			}

			// Token: 0x06007364 RID: 29540 RVA: 0x001AE5E8 File Offset: 0x001AC7E8
			public DispatchRequestAsyncResult(DispatchContext dispatchContext, AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
			{
				this._dispatchContext = dispatchContext;
				ClaimsPrincipal principal = dispatchContext.Principal;
				RequestSecurityToken requestSecurityToken = dispatchContext.RequestMessage as RequestSecurityToken;
				SecurityTokenService securityTokenService = dispatchContext.SecurityTokenService;
				if (requestSecurityToken == null)
				{
					base.Complete(true, DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3023"))));
					return;
				}
				string requestType = requestSecurityToken.RequestType;
				if (requestType == "http://schemas.microsoft.com/idfx/requesttype/cancel")
				{
					securityTokenService.BeginCancel(principal, requestSecurityToken, new AsyncCallback(this.OnCancelComplete), null);
					return;
				}
				if (requestType == "http://schemas.microsoft.com/idfx/requesttype/issue")
				{
					securityTokenService.BeginIssue(principal, requestSecurityToken, new AsyncCallback(this.OnIssueComplete), null);
					return;
				}
				if (requestType == "http://schemas.microsoft.com/idfx/requesttype/renew")
				{
					securityTokenService.BeginRenew(principal, requestSecurityToken, new AsyncCallback(this.OnRenewComplete), null);
					return;
				}
				if (!(requestType == "http://schemas.microsoft.com/idfx/requesttype/validate"))
				{
					base.Complete(true, DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3112", new object[]
					{
						requestSecurityToken.RequestType
					}))));
					return;
				}
				securityTokenService.BeginValidate(principal, requestSecurityToken, new AsyncCallback(this.OnValidateComplete), null);
			}

			// Token: 0x06007365 RID: 29541 RVA: 0x001AE70C File Offset: 0x001AC90C
			public new static DispatchContext End(IAsyncResult ar)
			{
				AsyncResult.End(ar);
				WSTrustServiceContract.DispatchRequestAsyncResult dispatchRequestAsyncResult = ar as WSTrustServiceContract.DispatchRequestAsyncResult;
				if (dispatchRequestAsyncResult == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2004", new object[]
					{
						typeof(WSTrustServiceContract.DispatchRequestAsyncResult),
						ar.GetType()
					}));
				}
				return dispatchRequestAsyncResult.DispatchContext;
			}

			// Token: 0x06007366 RID: 29542 RVA: 0x001AE75C File Offset: 0x001AC95C
			private void OnCancelComplete(IAsyncResult ar)
			{
				try
				{
					this._dispatchContext.ResponseMessage = this._dispatchContext.SecurityTokenService.EndCancel(ar);
					base.Complete(false);
				}
				catch (Exception exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007367 RID: 29543 RVA: 0x001AE7BC File Offset: 0x001AC9BC
			private void OnIssueComplete(IAsyncResult ar)
			{
				try
				{
					this._dispatchContext.ResponseMessage = this._dispatchContext.SecurityTokenService.EndIssue(ar);
					base.Complete(false);
				}
				catch (Exception exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007368 RID: 29544 RVA: 0x001AE81C File Offset: 0x001ACA1C
			private void OnRenewComplete(IAsyncResult ar)
			{
				try
				{
					this._dispatchContext.ResponseMessage = this._dispatchContext.SecurityTokenService.EndRenew(ar);
					base.Complete(false);
				}
				catch (Exception exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007369 RID: 29545 RVA: 0x001AE87C File Offset: 0x001ACA7C
			private void OnValidateComplete(IAsyncResult ar)
			{
				try
				{
					this._dispatchContext.ResponseMessage = this._dispatchContext.SecurityTokenService.EndValidate(ar);
					base.Complete(false);
				}
				catch (Exception exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Complete(false, exception);
				}
			}

			// Token: 0x04004136 RID: 16694
			private DispatchContext _dispatchContext;
		}
	}
}
