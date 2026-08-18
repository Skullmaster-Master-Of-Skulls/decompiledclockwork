using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IdentityModel;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Text;
using System.Xml;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Security
{
	// Token: 0x020002AF RID: 687
	internal static class SecurityAuditHelper
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x000504BC File Offset: 0x0004E6BC
		public static bool IsSecurityAuditSupported
		{
			get
			{
				if (SecurityAuditHelper.authzModule == null)
				{
					Type typeFromHandle = typeof(SecurityAuditHelper.SafeLoadLibraryHandle);
					lock (typeFromHandle)
					{
						SecurityAuditHelper.SafeLoadLibraryHandle safeLoadLibraryHandle = SecurityAuditHelper.SafeLoadLibraryHandle.LoadLibraryEx(Environment.SystemDirectory + "\\authz.dll");
						SecurityAuditHelper.isSecurityAuditSupported = safeLoadLibraryHandle.IsProcNameExist("AuthzInstallSecurityEventSource");
						SecurityAuditHelper.authzModule = safeLoadLibraryHandle;
					}
				}
				return SecurityAuditHelper.isSecurityAuditSupported;
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00050534 File Offset: 0x0004E734
		private static string ExceptionToString(Exception exception)
		{
			Exception ex = exception;
			StringBuilder stringBuilder = new StringBuilder(128);
			while (ex != null)
			{
				stringBuilder.Append(ex.GetType().Name);
				stringBuilder.Append(": ");
				stringBuilder.Append(ex.Message);
				ex = ex.InnerException;
				if (ex != null)
				{
					stringBuilder.Append(" ---> ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0005059C File Offset: 0x0004E79C
		public static void WriteServiceAuthorizationSuccessEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, Message message, Uri serviceUri, string action, string clientIdentity, string authContextId, string serviceAuthorizationManager)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance(1074135041L, 1, EventLogEntryType.Information), new object[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						authContextId,
						activityId,
						serviceAuthorizationManager
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(1U, 1074135041U, new string[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						authContextId,
						activityId,
						serviceAuthorizationManager
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "ServiceAuthorizationSuccess"), null, null, message);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "ServiceAuthorizationSuccess"), null, exception, message);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000506D0 File Offset: 0x0004E8D0
		public static void WriteServiceAuthorizationFailureEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, Message message, Uri serviceUri, string action, string clientIdentity, string authContextId, string serviceAuthorizationManager, Exception exception)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance((long)((ulong)-1073348606), 1, EventLogEntryType.Error), new object[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						authContextId,
						activityId,
						serviceAuthorizationManager,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(0U, 3221618690U, new string[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						authContextId,
						activityId,
						serviceAuthorizationManager,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "ServiceAuthorizationFailure"), null, null, message);
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "ServiceAuthorizationFailure"), null, exception2, message);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00050818 File Offset: 0x0004EA18
		public static void WriteMessageAuthenticationSuccessEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, Message message, Uri serviceUri, string action, string clientIdentity)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance(1074135043L, 2, EventLogEntryType.Information), new object[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						activityId
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(1U, 1074135043U, new string[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						activityId
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "MessageAuthenticationSuccess"), null, null, message);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "MessageAuthenticationSuccess"), null, exception, message);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00050938 File Offset: 0x0004EB38
		public static void WriteMessageAuthenticationFailureEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, Message message, Uri serviceUri, string action, string clientIdentity, Exception exception)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance((long)((ulong)-1073348604), 2, EventLogEntryType.Error), new object[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						activityId,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(0U, 3221618692U, new string[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						activityId,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "MessageAuthenticationFailure"), null, null, message);
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "MessageAuthenticationFailure"), null, exception2, message);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00050A6C File Offset: 0x0004EC6C
		public static void WriteImpersonationSuccessEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, string operationName, string clientIdentity)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance(1074135049L, 2, EventLogEntryType.Information), new object[]
					{
						operationName,
						clientIdentity,
						activityId
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(1U, 1074135049U, new string[]
					{
						operationName,
						clientIdentity,
						activityId
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "ImpersonationSuccess"), null, null);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "ImpersonationSuccess"), null, exception);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00050B74 File Offset: 0x0004ED74
		public static void WriteImpersonationFailureEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, string operationName, string clientIdentity, Exception exception)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance((long)((ulong)-1073348598), 2, EventLogEntryType.Error), new object[]
					{
						operationName,
						clientIdentity,
						activityId,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(0U, 3221618698U, new string[]
					{
						operationName,
						clientIdentity,
						activityId,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "ImpersonationFailure"), null, null);
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "ImpersonationFailure"), null, exception2);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00050C90 File Offset: 0x0004EE90
		public static void WriteSecurityNegotiationSuccessEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, Message message, Uri serviceUri, string action, string clientIdentity, string negotiationType)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance(1074135045L, 2, EventLogEntryType.Information), new object[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						activityId,
						negotiationType
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(1U, 1074135045U, new string[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						activityId,
						negotiationType
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "SecurityNegotiationSuccess"), null, null, message);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "SecurityNegotiationSuccess"), null, exception, message);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00050DB8 File Offset: 0x0004EFB8
		public static void WriteSecurityNegotiationFailureEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, Message message, Uri serviceUri, string action, string clientIdentity, string negotiationType, Exception exception)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance((long)((ulong)-1073348602), 2, EventLogEntryType.Error), new object[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						activityId,
						negotiationType,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(0U, 3221618694U, new string[]
					{
						serviceUri.AbsoluteUri,
						action,
						clientIdentity,
						activityId,
						negotiationType,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "SecurityNegotiationFailure"), null, null, message);
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "SecurityNegotiationFailure"), null, exception2, message);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00050EF4 File Offset: 0x0004F0F4
		public static void WriteTransportAuthenticationSuccessEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, Message message, Uri serviceUri, string clientIdentity)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance(1074135047L, 2, EventLogEntryType.Information), new object[]
					{
						serviceUri.AbsoluteUri,
						clientIdentity,
						activityId
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(1U, 1074135047U, new string[]
					{
						serviceUri.AbsoluteUri,
						clientIdentity,
						activityId
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "TransportAuthenticationSuccess"), null, null, message);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "TransportAuthenticationSuccess"), null, exception, message);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00051008 File Offset: 0x0004F208
		public static void WriteTransportAuthenticationFailureEvent(AuditLogLocation auditLogLocation, bool suppressAuditFailure, Message message, Uri serviceUri, string clientIdentity, Exception exception)
		{
			try
			{
				if (auditLogLocation == AuditLogLocation.Default)
				{
					auditLogLocation = (SecurityAuditHelper.IsSecurityAuditSupported ? AuditLogLocation.Security : AuditLogLocation.Application);
				}
				string activityId = SecurityAuditHelper.GetActivityId();
				if (auditLogLocation == AuditLogLocation.Application)
				{
					SecurityAuditHelper.WriteEventToApplicationLog(new EventInstance((long)((ulong)-1073348600), 2, EventLogEntryType.Error), new object[]
					{
						serviceUri.AbsoluteUri,
						clientIdentity,
						activityId,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				else
				{
					if (auditLogLocation != AuditLogLocation.Security)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("auditLogLocation", SR.GetString("SecurityAuditPlatformNotSupported")));
					}
					SecurityAuditHelper.WriteAuditEvent(0U, 3221618696U, new string[]
					{
						serviceUri.AbsoluteUri,
						clientIdentity,
						activityId,
						SecurityAuditHelper.ExceptionToString(exception)
					});
				}
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 458835, SR.GetString("TraceCodeSecurityAuditWrittenSuccess"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "TransportAuthenticationFailure"), null, null, message);
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458836, SR.GetString("TraceCodeSecurityAuditWrittenFailure"), new SecurityAuditHelper.SecurityAuditTraceRecord(auditLogLocation, "TransportAuthenticationFailure"), null, exception2, message);
				}
				if (!suppressAuditFailure)
				{
					throw;
				}
			}
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00051130 File Offset: 0x0004F330
		private static string GetActivityId()
		{
			Guid activityId = DiagnosticTraceBase.ActivityId;
			if (!(activityId == Guid.Empty))
			{
				return activityId.ToString();
			}
			return "<null>";
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00051164 File Offset: 0x0004F364
		private static void WriteEventToApplicationLog(EventInstance instance, params object[] parameters)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				string text = parameters[i] as string;
				if (text != null)
				{
					parameters[i] = EventLogger.NormalizeEventLogParameter(text);
				}
			}
			EventLog.WriteEvent("ServiceModel Audit 4.0.0.0", instance, parameters);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000511A0 File Offset: 0x0004F3A0
		private static void WriteAuditEvent(uint auditType, uint auditId, params string[] parameters)
		{
			if (!SecurityAuditHelper.IsSecurityAuditSupported)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new PlatformNotSupportedException(SR.GetString("SecurityAuditPlatformNotSupported")));
			}
			Privilege privilege = new Privilege("SeAuditPrivilege");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				try
				{
					privilege.Enable();
					SecurityAuditHelper.SafeSecurityAuditHandle safeSecurityAuditHandle;
					if (!SecurityAuditHelper.NativeMethods.AuthzRegisterSecurityEventSource(0U, "ServiceModel 4.0.0.0", out safeSecurityAuditHandle))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						Utility.CloseInvalidOutSafeHandle(safeSecurityAuditHandle);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
					}
					SecurityAuditHelper.SafeHGlobalHandle safeHGlobalHandle = null;
					SecurityAuditHelper.SafeHGlobalHandle[] array = new SecurityAuditHelper.SafeHGlobalHandle[parameters.Length];
					try
					{
						safeHGlobalHandle = SecurityAuditHelper.SafeHGlobalHandle.AllocHGlobal(parameters.Length * SecurityAuditHelper.NativeMethods.AUDIT_PARAM.Size);
						long num = safeHGlobalHandle.DangerousGetHandle().ToInt64();
						SecurityAuditHelper.NativeMethods.AUDIT_PARAM audit_PARAM;
						audit_PARAM.Type = SecurityAuditHelper.NativeMethods.AUDIT_PARAM_TYPE.APT_String;
						audit_PARAM.Length = 0U;
						audit_PARAM.Flags = 0U;
						audit_PARAM.Data1 = IntPtr.Zero;
						for (int i = 0; i < parameters.Length; i++)
						{
							if (!string.IsNullOrEmpty(parameters[i]))
							{
								string s = EventLogger.NormalizeEventLogParameter(parameters[i]);
								array[i] = SecurityAuditHelper.SafeHGlobalHandle.AllocHGlobal(s);
								audit_PARAM.Data0 = array[i].DangerousGetHandle();
							}
							else
							{
								audit_PARAM.Data0 = IntPtr.Zero;
							}
							Marshal.StructureToPtr(audit_PARAM, new IntPtr(num + (long)(i * SecurityAuditHelper.NativeMethods.AUDIT_PARAM.Size)), false);
						}
						SecurityAuditHelper.NativeMethods.AUDIT_PARAMS audit_PARAMS;
						audit_PARAMS.Length = 0U;
						audit_PARAMS.Flags = auditType;
						audit_PARAMS.Parameters = safeHGlobalHandle;
						audit_PARAMS.Count = (ushort)parameters.Length;
						if (!SecurityAuditHelper.NativeMethods.AuthzReportSecurityEventFromParams(auditType, safeSecurityAuditHandle, auditId, null, ref audit_PARAMS))
						{
							int lastWin32Error2 = Marshal.GetLastWin32Error();
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error2));
						}
					}
					finally
					{
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j] != null)
							{
								array[j].Close();
							}
						}
						if (safeHGlobalHandle != null)
						{
							safeHGlobalHandle.Close();
						}
						safeSecurityAuditHandle.Close();
					}
				}
				finally
				{
					int num2 = -1;
					string message = null;
					try
					{
						num2 = privilege.Revert();
						if (num2 != 0)
						{
							message = SR.GetString("RevertingPrivilegeFailed", new object[]
							{
								new Win32Exception(num2)
							});
						}
					}
					finally
					{
						if (num2 != 0)
						{
							DiagnosticUtility.FailFast(message);
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x04001B3E RID: 6974
		private static SecurityAuditHelper.SafeLoadLibraryHandle authzModule;

		// Token: 0x04001B3F RID: 6975
		private static bool isSecurityAuditSupported;

		// Token: 0x04001B40 RID: 6976
		private const string ApplicationEventSourceName = "ServiceModel Audit 4.0.0.0";

		// Token: 0x04001B41 RID: 6977
		private const string SecurityEventSourceName = "ServiceModel 4.0.0.0";

		// Token: 0x04001B42 RID: 6978
		private const int ServiceAuthorizationCategory = 1;

		// Token: 0x04001B43 RID: 6979
		private const int MessageAuthenticationCategory = 2;

		// Token: 0x04001B44 RID: 6980
		private const uint ServiceAuthorizationSuccess = 1074135041U;

		// Token: 0x04001B45 RID: 6981
		private const uint ServiceAuthorizationFailure = 3221618690U;

		// Token: 0x04001B46 RID: 6982
		private const uint MessageAuthenticationSuccess = 1074135043U;

		// Token: 0x04001B47 RID: 6983
		private const uint MessageAuthenticationFailure = 3221618692U;

		// Token: 0x04001B48 RID: 6984
		private const uint ImpersonationSuccess = 1074135049U;

		// Token: 0x04001B49 RID: 6985
		private const uint ImpersonationFailure = 3221618698U;

		// Token: 0x04001B4A RID: 6986
		private const uint SecurityNegotiationSuccess = 1074135045U;

		// Token: 0x04001B4B RID: 6987
		private const uint SecurityNegotiationFailure = 3221618694U;

		// Token: 0x04001B4C RID: 6988
		private const uint TransportAuthenticationSuccess = 1074135047U;

		// Token: 0x04001B4D RID: 6989
		private const uint TransportAuthenticationFailure = 3221618696U;

		// Token: 0x04001B4E RID: 6990
		private const uint APF_AuditFailure = 0U;

		// Token: 0x04001B4F RID: 6991
		private const uint APF_AuditSuccess = 1U;

		// Token: 0x02000B41 RID: 2881
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{
			// Token: 0x060070CA RID: 28874
			[DllImport("authz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern bool AuthzRegisterSecurityEventSource([In] uint dwFlags, [In] string szEventSourceName, out SecurityAuditHelper.SafeSecurityAuditHandle phEventProvider);

			// Token: 0x060070CB RID: 28875
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("authz.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool AuthzUnregisterSecurityEventSource([In] uint dwFlags, [In] [Out] ref IntPtr providerHandle);

			// Token: 0x060070CC RID: 28876
			[DllImport("authz.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool AuthzReportSecurityEventFromParams([In] uint dwFlags, [In] SecurityAuditHelper.SafeSecurityAuditHandle providerHandle, [In] uint auditId, [In] byte[] securityIdentifier, [In] ref SecurityAuditHelper.NativeMethods.AUDIT_PARAMS auditParams);

			// Token: 0x060070CD RID: 28877
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool CloseHandle([In] IntPtr handle);

			// Token: 0x060070CE RID: 28878
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			public static extern SecurityAuditHelper.SafeLoadLibraryHandle LoadLibraryExW([In] string lpwLibFileName, [In] IntPtr hFile, [In] uint dwFlags);

			// Token: 0x060070CF RID: 28879
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			public static extern bool FreeLibrary([In] IntPtr hModule);

			// Token: 0x060070D0 RID: 28880
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			public static extern IntPtr GetProcAddress([In] SecurityAuditHelper.SafeLoadLibraryHandle hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

			// Token: 0x04004028 RID: 16424
			public const string AUTHZ = "authz.dll";

			// Token: 0x04004029 RID: 16425
			public const string ADVAPI32 = "advapi32.dll";

			// Token: 0x0400402A RID: 16426
			public const string KERNEL32 = "kernel32.dll";

			// Token: 0x02000EDD RID: 3805
			public enum AUDIT_PARAM_TYPE
			{
				// Token: 0x04004CC9 RID: 19657
				APT_String = 2
			}

			// Token: 0x02000EDE RID: 3806
			public struct AUDIT_PARAMS
			{
				// Token: 0x04004CCA RID: 19658
				public uint Length;

				// Token: 0x04004CCB RID: 19659
				public uint Flags;

				// Token: 0x04004CCC RID: 19660
				public ushort Count;

				// Token: 0x04004CCD RID: 19661
				public SecurityAuditHelper.SafeHGlobalHandle Parameters;
			}

			// Token: 0x02000EDF RID: 3807
			public struct AUDIT_PARAM
			{
				// Token: 0x04004CCE RID: 19662
				public SecurityAuditHelper.NativeMethods.AUDIT_PARAM_TYPE Type;

				// Token: 0x04004CCF RID: 19663
				public uint Length;

				// Token: 0x04004CD0 RID: 19664
				public uint Flags;

				// Token: 0x04004CD1 RID: 19665
				public IntPtr Data0;

				// Token: 0x04004CD2 RID: 19666
				public IntPtr Data1;

				// Token: 0x04004CD3 RID: 19667
				public static readonly int Size = Marshal.SizeOf(typeof(SecurityAuditHelper.NativeMethods.AUDIT_PARAM));
			}
		}

		// Token: 0x02000B42 RID: 2882
		private class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060070D1 RID: 28881 RVA: 0x001A4122 File Offset: 0x001A2322
			private SafeTokenHandle() : base(true)
			{
			}

			// Token: 0x060070D2 RID: 28882 RVA: 0x001A412B File Offset: 0x001A232B
			protected override bool ReleaseHandle()
			{
				return SecurityAuditHelper.NativeMethods.CloseHandle(this.handle);
			}
		}

		// Token: 0x02000B43 RID: 2883
		private class SafeSecurityAuditHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060070D3 RID: 28883 RVA: 0x001A4138 File Offset: 0x001A2338
			private SafeSecurityAuditHandle() : base(true)
			{
			}

			// Token: 0x060070D4 RID: 28884 RVA: 0x001A4141 File Offset: 0x001A2341
			protected override bool ReleaseHandle()
			{
				return SecurityAuditHelper.NativeMethods.AuthzUnregisterSecurityEventSource(0U, ref this.handle);
			}
		}

		// Token: 0x02000B44 RID: 2884
		private class SafeLoadLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060070D5 RID: 28885 RVA: 0x001A414F File Offset: 0x001A234F
			private SafeLoadLibraryHandle() : base(true)
			{
			}

			// Token: 0x060070D6 RID: 28886 RVA: 0x001A4158 File Offset: 0x001A2358
			public static SecurityAuditHelper.SafeLoadLibraryHandle LoadLibraryEx(string library)
			{
				SecurityAuditHelper.SafeLoadLibraryHandle safeLoadLibraryHandle = SecurityAuditHelper.NativeMethods.LoadLibraryExW(library, IntPtr.Zero, 0U);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (safeLoadLibraryHandle.IsInvalid)
				{
					safeLoadLibraryHandle.SetHandleAsInvalid();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error, SR.GetString("SecurityAuditFailToLoadDll", new object[]
					{
						library
					})));
				}
				return safeLoadLibraryHandle;
			}

			// Token: 0x060070D7 RID: 28887 RVA: 0x001A41AC File Offset: 0x001A23AC
			protected override bool ReleaseHandle()
			{
				return SecurityAuditHelper.NativeMethods.FreeLibrary(this.handle);
			}

			// Token: 0x060070D8 RID: 28888 RVA: 0x001A41BC File Offset: 0x001A23BC
			public bool IsProcNameExist(string procName)
			{
				if (!this.IsInvalid)
				{
					try
					{
						return IntPtr.Zero != SecurityAuditHelper.NativeMethods.GetProcAddress(this, procName);
					}
					catch (ObjectDisposedException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						return false;
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x02000B45 RID: 2885
		private class SafeHGlobalHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060070D9 RID: 28889 RVA: 0x001A4204 File Offset: 0x001A2404
			private SafeHGlobalHandle() : base(true)
			{
			}

			// Token: 0x060070DA RID: 28890 RVA: 0x001A420D File Offset: 0x001A240D
			protected override bool ReleaseHandle()
			{
				Marshal.FreeHGlobal(this.handle);
				return true;
			}

			// Token: 0x060070DB RID: 28891 RVA: 0x001A421C File Offset: 0x001A241C
			public static SecurityAuditHelper.SafeHGlobalHandle AllocHGlobal(string s)
			{
				byte[] bytes = DiagnosticUtility.Utility.AllocateByteArray(checked((s.Length + 1) * 2));
				Encoding.Unicode.GetBytes(s, 0, s.Length, bytes, 0);
				return SecurityAuditHelper.SafeHGlobalHandle.AllocHGlobal(bytes);
			}

			// Token: 0x060070DC RID: 28892 RVA: 0x001A425C File Offset: 0x001A245C
			public static SecurityAuditHelper.SafeHGlobalHandle AllocHGlobal(byte[] bytes)
			{
				SecurityAuditHelper.SafeHGlobalHandle safeHGlobalHandle = SecurityAuditHelper.SafeHGlobalHandle.AllocHGlobal(bytes.Length);
				Marshal.Copy(bytes, 0, safeHGlobalHandle.DangerousGetHandle(), bytes.Length);
				return safeHGlobalHandle;
			}

			// Token: 0x060070DD RID: 28893 RVA: 0x001A4284 File Offset: 0x001A2484
			public static SecurityAuditHelper.SafeHGlobalHandle AllocHGlobal(int cb)
			{
				SecurityAuditHelper.SafeHGlobalHandle safeHGlobalHandle = new SecurityAuditHelper.SafeHGlobalHandle();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					IntPtr handle = Marshal.AllocHGlobal(cb);
					safeHGlobalHandle.SetHandle(handle);
				}
				return safeHGlobalHandle;
			}
		}

		// Token: 0x02000B46 RID: 2886
		private class SecurityAuditTraceRecord : TraceRecord
		{
			// Token: 0x060070DE RID: 28894 RVA: 0x001A42C0 File Offset: 0x001A24C0
			internal SecurityAuditTraceRecord(AuditLogLocation auditLogLocation, string auditType)
			{
				this.auditLogLocation = auditLogLocation;
				this.auditType = auditType;
			}

			// Token: 0x17001A54 RID: 6740
			// (get) Token: 0x060070DF RID: 28895 RVA: 0x001A42D6 File Offset: 0x001A24D6
			internal override string EventId
			{
				get
				{
					return base.BuildEventId("SecurityAudit");
				}
			}

			// Token: 0x060070E0 RID: 28896 RVA: 0x001A42E3 File Offset: 0x001A24E3
			internal override void WriteTo(XmlWriter writer)
			{
				writer.WriteElementString("AuditLogLocation", this.auditLogLocation.ToString());
				writer.WriteElementString("AuditType", this.auditType);
			}

			// Token: 0x0400402B RID: 16427
			private AuditLogLocation auditLogLocation;

			// Token: 0x0400402C RID: 16428
			private string auditType;
		}
	}
}
