using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Policy;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200053D RID: 1341
	internal sealed class AuthenticationBehavior
	{
		// Token: 0x060032B9 RID: 12985 RVA: 0x000C3BEB File Offset: 0x000C1DEB
		private AuthenticationBehavior(ServiceAuthenticationManager authenticationManager)
		{
			this.serviceAuthenticationManager = authenticationManager;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000C3BFC File Offset: 0x000C1DFC
		public void Authenticate(ref MessageRpc rpc)
		{
			SecurityMessageProperty orCreate = SecurityMessageProperty.GetOrCreate(rpc.Request);
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = orCreate.ServiceSecurityContext.AuthorizationPolicies;
			bool flag = DS.AuthenticationIsEnabled();
			Stopwatch stopwatch = null;
			if (flag)
			{
				stopwatch = Stopwatch.StartNew();
			}
			try
			{
				readOnlyCollection = this.serviceAuthenticationManager.Authenticate(orCreate.ServiceSecurityContext.AuthorizationPolicies, rpc.Channel.ListenUri, ref rpc.Request);
				if (readOnlyCollection == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AuthenticationManagerShouldNotReturnNull")));
				}
				if (flag)
				{
					DS.Authentication(this.serviceAuthenticationManager.GetType(), true, stopwatch.Elapsed);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (flag)
				{
					DS.Authentication(this.serviceAuthenticationManager.GetType(), false, stopwatch.Elapsed);
				}
				if (PerformanceCounters.PerformanceCountersEnabled)
				{
					PerformanceCounters.AuthenticationFailed(rpc.Request, rpc.Channel.ListenUri);
				}
				if (AuditLevel.Failure == (this.messageAuthenticationAuditLevel & AuditLevel.Failure))
				{
					try
					{
						AuthorizationContext authorizationContext = orCreate.ServiceSecurityContext.AuthorizationContext;
						string clientIdentity;
						if (authorizationContext != null)
						{
							clientIdentity = SecurityUtils.GetIdentityNamesFromContext(authorizationContext);
						}
						else
						{
							clientIdentity = SecurityUtils.AnonymousIdentity.Name;
						}
						SecurityAuditHelper.WriteMessageAuthenticationFailureEvent(this.auditLogLocation, this.suppressAuditFailure, rpc.Request, rpc.Channel.ListenUri, rpc.Request.Headers.Action, clientIdentity, exception);
					}
					catch (Exception exception2)
					{
						if (Fx.IsFatal(exception2))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(AuthenticationBehavior.CreateFailedAuthenticationFaultException());
			}
			rpc.Request.Properties.Security.ServiceSecurityContext.AuthorizationPolicies = readOnlyCollection;
			if (AuditLevel.Success == (this.messageAuthenticationAuditLevel & AuditLevel.Success))
			{
				AuthorizationContext authorizationContext2 = orCreate.ServiceSecurityContext.AuthorizationContext;
				string clientIdentity2;
				if (authorizationContext2 != null)
				{
					clientIdentity2 = SecurityUtils.GetIdentityNamesFromContext(authorizationContext2);
				}
				else
				{
					clientIdentity2 = SecurityUtils.AnonymousIdentity.Name;
				}
				SecurityAuditHelper.WriteMessageAuthenticationSuccessEvent(this.auditLogLocation, this.suppressAuditFailure, rpc.Request, rpc.Channel.ListenUri, rpc.Request.Headers.Action, clientIdentity2);
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000C3E0C File Offset: 0x000C200C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static AuthenticationBehavior CreateAuthenticationBehavior(DispatchRuntime dispatch)
		{
			return new AuthenticationBehavior(dispatch.ServiceAuthenticationManager)
			{
				auditLogLocation = dispatch.SecurityAuditLogLocation,
				suppressAuditFailure = dispatch.SuppressAuditFailure,
				messageAuthenticationAuditLevel = dispatch.MessageAuthenticationAuditLevel
			};
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000C3E4A File Offset: 0x000C204A
		public static AuthenticationBehavior TryCreate(DispatchRuntime dispatch)
		{
			if (dispatch == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dispatch");
			}
			if (!dispatch.RequiresAuthentication)
			{
				return null;
			}
			return AuthenticationBehavior.CreateAuthenticationBehavior(dispatch);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000C3E70 File Offset: 0x000C2070
		internal static Exception CreateFailedAuthenticationFaultException()
		{
			SecurityVersion @default = SecurityVersion.Default;
			FaultCode code = FaultCode.CreateSenderFaultCode(@default.InvalidSecurityFaultCode.Value, @default.HeaderNamespace.Value);
			FaultReason reason = new FaultReason(SR.GetString("AuthenticationOfClientFailed"), CultureInfo.CurrentCulture);
			return new FaultException(reason, code);
		}

		// Token: 0x04002737 RID: 10039
		private ServiceAuthenticationManager serviceAuthenticationManager;

		// Token: 0x04002738 RID: 10040
		private AuditLogLocation auditLogLocation;

		// Token: 0x04002739 RID: 10041
		private bool suppressAuditFailure;

		// Token: 0x0400273A RID: 10042
		private AuditLevel messageAuthenticationAuditLevel;
	}
}
