using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Policy;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200053E RID: 1342
	internal sealed class AuthorizationBehavior
	{
		// Token: 0x060032BE RID: 12990 RVA: 0x000C3EBB File Offset: 0x000C20BB
		private AuthorizationBehavior()
		{
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000C3EC4 File Offset: 0x000C20C4
		public void Authorize(ref MessageRpc rpc)
		{
			if (TD.DispatchMessageBeforeAuthorizationIsEnabled())
			{
				TD.DispatchMessageBeforeAuthorization(rpc.EventTraceActivity);
			}
			SecurityMessageProperty orCreate = SecurityMessageProperty.GetOrCreate(rpc.Request);
			orCreate.ExternalAuthorizationPolicies = this.externalAuthorizationPolicies;
			ServiceAuthorizationManager serviceAuthorizationManager = this.serviceAuthorizationManager ?? AuthorizationBehavior.DefaultServiceAuthorizationManager;
			bool flag = DS.AuthorizationIsEnabled();
			Stopwatch stopwatch = null;
			if (flag)
			{
				stopwatch = Stopwatch.StartNew();
			}
			try
			{
				if (!serviceAuthorizationManager.CheckAccess(rpc.OperationContext, ref rpc.Request))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(AuthorizationBehavior.CreateAccessDeniedFaultException());
				}
				if (flag)
				{
					DS.Authorization(this.serviceAuthorizationManager.GetType(), true, stopwatch.Elapsed);
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
					DS.Authorization(this.serviceAuthorizationManager.GetType(), false, stopwatch.Elapsed);
				}
				if (PerformanceCounters.PerformanceCountersEnabled)
				{
					PerformanceCounters.AuthorizationFailed(rpc.Operation.Name);
				}
				if (AuditLevel.Failure == (this.serviceAuthorizationAuditLevel & AuditLevel.Failure))
				{
					try
					{
						AuthorizationContext authorizationContext = orCreate.ServiceSecurityContext.AuthorizationContext;
						string clientIdentity;
						string authContextId;
						if (authorizationContext != null)
						{
							clientIdentity = SecurityUtils.GetIdentityNamesFromContext(authorizationContext);
							authContextId = authorizationContext.Id;
						}
						else
						{
							clientIdentity = SecurityUtils.AnonymousIdentity.Name;
							authContextId = "<null>";
						}
						SecurityAuditHelper.WriteServiceAuthorizationFailureEvent(this.auditLogLocation, this.suppressAuditFailure, rpc.Request, rpc.Request.Headers.To, rpc.Request.Headers.Action, clientIdentity, authContextId, (serviceAuthorizationManager == AuthorizationBehavior.DefaultServiceAuthorizationManager) ? "<default>" : serviceAuthorizationManager.GetType().Name, exception);
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
				throw;
			}
			if (AuditLevel.Success == (this.serviceAuthorizationAuditLevel & AuditLevel.Success))
			{
				AuthorizationContext authorizationContext2 = orCreate.ServiceSecurityContext.AuthorizationContext;
				string clientIdentity2;
				string authContextId2;
				if (authorizationContext2 != null)
				{
					clientIdentity2 = SecurityUtils.GetIdentityNamesFromContext(authorizationContext2);
					authContextId2 = authorizationContext2.Id;
				}
				else
				{
					clientIdentity2 = SecurityUtils.AnonymousIdentity.Name;
					authContextId2 = "<null>";
				}
				SecurityAuditHelper.WriteServiceAuthorizationSuccessEvent(this.auditLogLocation, this.suppressAuditFailure, rpc.Request, rpc.Request.Headers.To, rpc.Request.Headers.Action, clientIdentity2, authContextId2, (serviceAuthorizationManager == AuthorizationBehavior.DefaultServiceAuthorizationManager) ? "<default>" : serviceAuthorizationManager.GetType().Name);
			}
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000C4110 File Offset: 0x000C2310
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static AuthorizationBehavior CreateAuthorizationBehavior(DispatchRuntime dispatch)
		{
			return new AuthorizationBehavior
			{
				externalAuthorizationPolicies = dispatch.ExternalAuthorizationPolicies,
				serviceAuthorizationManager = dispatch.ServiceAuthorizationManager,
				auditLogLocation = dispatch.SecurityAuditLogLocation,
				suppressAuditFailure = dispatch.SuppressAuditFailure,
				serviceAuthorizationAuditLevel = dispatch.ServiceAuthorizationAuditLevel
			};
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000C4160 File Offset: 0x000C2360
		public static AuthorizationBehavior TryCreate(DispatchRuntime dispatch)
		{
			if (dispatch == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("dispatch"));
			}
			if (!dispatch.RequiresAuthorization)
			{
				return null;
			}
			return AuthorizationBehavior.CreateAuthorizationBehavior(dispatch);
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000C418C File Offset: 0x000C238C
		internal static Exception CreateAccessDeniedFaultException()
		{
			SecurityVersion @default = SecurityVersion.Default;
			FaultCode code = FaultCode.CreateSenderFaultCode(@default.FailedAuthenticationFaultCode.Value, @default.HeaderNamespace.Value);
			FaultReason reason = new FaultReason(SR.GetString("AccessDenied"), CultureInfo.CurrentCulture);
			return new FaultException(reason, code);
		}

		// Token: 0x0400273B RID: 10043
		private static ServiceAuthorizationManager DefaultServiceAuthorizationManager = new ServiceAuthorizationManager();

		// Token: 0x0400273C RID: 10044
		private ReadOnlyCollection<IAuthorizationPolicy> externalAuthorizationPolicies;

		// Token: 0x0400273D RID: 10045
		private ServiceAuthorizationManager serviceAuthorizationManager;

		// Token: 0x0400273E RID: 10046
		private AuditLogLocation auditLogLocation;

		// Token: 0x0400273F RID: 10047
		private bool suppressAuditFailure;

		// Token: 0x04002740 RID: 10048
		private AuditLevel serviceAuthorizationAuditLevel;
	}
}
