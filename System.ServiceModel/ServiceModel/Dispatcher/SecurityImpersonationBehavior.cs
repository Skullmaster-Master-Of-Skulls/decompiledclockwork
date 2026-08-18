using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IdentityModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.ServiceModel.ComIntegration;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200053F RID: 1343
	internal sealed class SecurityImpersonationBehavior
	{
		// Token: 0x060032C4 RID: 12996 RVA: 0x000C41E4 File Offset: 0x000C23E4
		private SecurityImpersonationBehavior(DispatchRuntime dispatch)
		{
			this.principalPermissionMode = dispatch.PrincipalPermissionMode;
			this.impersonateCallerForAllOperations = dispatch.ImpersonateCallerForAllOperations;
			this.auditLevel = dispatch.MessageAuthenticationAuditLevel;
			this.auditLogLocation = dispatch.SecurityAuditLogLocation;
			this.suppressAuditFailure = dispatch.SuppressAuditFailure;
			if (dispatch.IsRoleProviderSet)
			{
				this.ApplyRoleProvider(dispatch);
			}
			this.domainNameMap = new Dictionary<string, string>(5, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000C425A File Offset: 0x000C245A
		public static SecurityImpersonationBehavior CreateIfNecessary(DispatchRuntime dispatch)
		{
			if (SecurityImpersonationBehavior.IsSecurityBehaviorNeeded(dispatch))
			{
				return new SecurityImpersonationBehavior(dispatch);
			}
			return null;
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000C426C File Offset: 0x000C246C
		private static WindowsPrincipal AnonymousWindowsPrincipal
		{
			get
			{
				if (SecurityImpersonationBehavior.anonymousWindowsPrincipal == null)
				{
					SecurityImpersonationBehavior.anonymousWindowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetAnonymous());
				}
				return SecurityImpersonationBehavior.anonymousWindowsPrincipal;
			}
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000C4289 File Offset: 0x000C2489
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ApplyRoleProvider(DispatchRuntime dispatch)
		{
			this.roleProvider = dispatch.RoleProvider;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000C4298 File Offset: 0x000C2498
		private static bool IsSecurityBehaviorNeeded(DispatchRuntime dispatch)
		{
			if (AspNetEnvironment.Current.RequiresImpersonation)
			{
				return true;
			}
			if (dispatch.PrincipalPermissionMode != PrincipalPermissionMode.None)
			{
				return true;
			}
			for (int i = 0; i < dispatch.Operations.Count; i++)
			{
				DispatchOperation dispatchOperation = dispatch.Operations[i];
				if (dispatchOperation.Impersonation == ImpersonationOption.Required)
				{
					return true;
				}
				if (dispatchOperation.Impersonation == ImpersonationOption.NotAllowed)
				{
					return false;
				}
			}
			return dispatch.ImpersonateCallerForAllOperations;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000C42FC File Offset: 0x000C24FC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private IPrincipal SetCurrentThreadPrincipal(ServiceSecurityContext securityContext, out bool isThreadPrincipalSet)
		{
			IPrincipal result = null;
			IPrincipal principal = null;
			ClaimsPrincipal claimsPrincipal = OperationContext.Current.ClaimsPrincipal;
			if (this.principalPermissionMode == PrincipalPermissionMode.UseWindowsGroups)
			{
				IPrincipal principal2;
				if (!(claimsPrincipal is WindowsPrincipal))
				{
					principal2 = this.GetWindowsPrincipal(securityContext);
				}
				else
				{
					IPrincipal principal3 = claimsPrincipal;
					principal2 = principal3;
				}
				principal = principal2;
			}
			else if (this.principalPermissionMode == PrincipalPermissionMode.UseAspNetRoles)
			{
				principal = new RoleProviderPrincipal(this.roleProvider, securityContext);
			}
			else if (this.principalPermissionMode == PrincipalPermissionMode.Custom)
			{
				principal = SecurityImpersonationBehavior.GetCustomPrincipal(securityContext);
			}
			else if (this.principalPermissionMode == PrincipalPermissionMode.Always)
			{
				principal = (claimsPrincipal ?? new ClaimsPrincipal(new ClaimsIdentity()));
			}
			if (principal != null)
			{
				result = Thread.CurrentPrincipal;
				Thread.CurrentPrincipal = principal;
				isThreadPrincipalSet = true;
			}
			else
			{
				isThreadPrincipalSet = false;
			}
			return result;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000C4394 File Offset: 0x000C2594
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IPrincipal GetCustomPrincipal(ServiceSecurityContext securityContext)
		{
			object obj;
			if (securityContext.AuthorizationContext.Properties.TryGetValue("Principal", out obj) && obj is IPrincipal)
			{
				return (IPrincipal)obj;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoPrincipalSpecifiedInAuthorizationContext")));
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000C43E2 File Offset: 0x000C25E2
		internal bool IsSecurityContextImpersonationRequired(ref MessageRpc rpc)
		{
			return rpc.Operation.Impersonation == ImpersonationOption.Required || (rpc.Operation.Impersonation == ImpersonationOption.Allowed && this.impersonateCallerForAllOperations);
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000C440A File Offset: 0x000C260A
		internal bool IsImpersonationEnabledOnCurrentOperation(ref MessageRpc rpc)
		{
			return this.IsSecurityContextImpersonationRequired(ref rpc) || AspNetEnvironment.Current.RequiresImpersonation || this.principalPermissionMode > PrincipalPermissionMode.None;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000C442C File Offset: 0x000C262C
		[SecurityCritical]
		public void StartImpersonation(ref MessageRpc rpc, out IDisposable impersonationContext, out IPrincipal originalPrincipal, out bool isThreadPrincipalSet)
		{
			impersonationContext = null;
			originalPrincipal = null;
			isThreadPrincipalSet = false;
			bool flag = this.principalPermissionMode > PrincipalPermissionMode.None;
			bool flag2 = this.IsSecurityContextImpersonationRequired(ref rpc);
			ServiceSecurityContext serviceSecurityContext;
			if (flag || flag2)
			{
				serviceSecurityContext = this.GetAndCacheSecurityContext(ref rpc);
			}
			else
			{
				serviceSecurityContext = null;
			}
			if (flag && serviceSecurityContext != null)
			{
				originalPrincipal = this.SetCurrentThreadPrincipal(serviceSecurityContext, out isThreadPrincipalSet);
			}
			if (flag2 || AspNetEnvironment.Current.RequiresImpersonation)
			{
				impersonationContext = this.StartImpersonation2(ref rpc, serviceSecurityContext, flag2);
			}
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000C4494 File Offset: 0x000C2694
		[SecurityCritical]
		private IDisposable StartImpersonation2(ref MessageRpc rpc, ServiceSecurityContext securityContext, bool isSecurityContextImpersonationOn)
		{
			IDisposable result = null;
			try
			{
				if (isSecurityContextImpersonationOn)
				{
					if (securityContext == null)
					{
						throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxSecurityContextPropertyMissingFromRequestMessage")), rpc.Request);
					}
					WindowsIdentity windowsIdentity = securityContext.WindowsIdentity;
					if (!(windowsIdentity.User != null))
					{
						if (securityContext.PrimaryIdentity is WindowsSidIdentity)
						{
							WindowsSidIdentity windowsSidIdentity = (WindowsSidIdentity)securityContext.PrimaryIdentity;
							if (windowsSidIdentity.SecurityIdentifier.IsWellKnown(WellKnownSidType.AnonymousSid))
							{
								result = new SecurityImpersonationBehavior.WindowsAnonymousIdentity().Impersonate();
								goto IL_FB;
							}
							string upnFromDownlevelName = this.GetUpnFromDownlevelName(windowsSidIdentity.Name);
							using (WindowsIdentity windowsIdentity2 = new WindowsIdentity(upnFromDownlevelName, "Kerberos"))
							{
								result = windowsIdentity2.Impersonate();
								goto IL_FB;
							}
						}
						throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityContextDoesNotAllowImpersonation", new object[]
						{
							rpc.Operation.Action
						})), rpc.Request);
					}
					result = windowsIdentity.Impersonate();
				}
				else if (AspNetEnvironment.Current.RequiresImpersonation && rpc.HostingProperty != null)
				{
					result = rpc.HostingProperty.Impersonate();
				}
				IL_FB:
				SecurityTraceRecordHelper.TraceImpersonationSucceeded(rpc.EventTraceActivity, rpc.Operation);
				if (AuditLevel.Success == (this.auditLevel & AuditLevel.Success))
				{
					SecurityAuditHelper.WriteImpersonationSuccessEvent(this.auditLogLocation, this.suppressAuditFailure, rpc.Operation.Name, System.ServiceModel.Security.SecurityUtils.GetIdentityNamesFromContext(securityContext.AuthorizationContext));
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				SecurityTraceRecordHelper.TraceImpersonationFailed(rpc.EventTraceActivity, rpc.Operation, ex);
				if (AuditLevel.Failure == (this.auditLevel & AuditLevel.Failure))
				{
					try
					{
						string clientIdentity;
						if (securityContext != null)
						{
							clientIdentity = System.ServiceModel.Security.SecurityUtils.GetIdentityNamesFromContext(securityContext.AuthorizationContext);
						}
						else
						{
							clientIdentity = System.ServiceModel.Security.SecurityUtils.AnonymousIdentity.Name;
						}
						SecurityAuditHelper.WriteImpersonationFailureEvent(this.auditLogLocation, this.suppressAuditFailure, rpc.Operation.Name, clientIdentity, ex);
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					}
				}
				throw;
			}
			return result;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000C46B8 File Offset: 0x000C28B8
		public void StopImpersonation(ref MessageRpc rpc, IDisposable impersonationContext, IPrincipal originalPrincipal, bool isThreadPrincipalSet)
		{
			try
			{
				if ((this.IsSecurityContextImpersonationRequired(ref rpc) || AspNetEnvironment.Current.RequiresImpersonation) && impersonationContext != null)
				{
					impersonationContext.Dispose();
				}
				if (isThreadPrincipalSet)
				{
					Thread.CurrentPrincipal = originalPrincipal;
				}
			}
			catch
			{
				string message = null;
				try
				{
					message = SR.GetString("SFxRevertImpersonationFailed0");
				}
				finally
				{
					DiagnosticUtility.FailFast(message);
				}
			}
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000C4728 File Offset: 0x000C2928
		private IPrincipal GetWindowsPrincipal(ServiceSecurityContext securityContext)
		{
			WindowsIdentity windowsIdentity = securityContext.WindowsIdentity;
			if (!windowsIdentity.IsAnonymous)
			{
				return new WindowsPrincipal(windowsIdentity);
			}
			WindowsSidIdentity windowsSidIdentity = securityContext.PrimaryIdentity as WindowsSidIdentity;
			if (windowsSidIdentity != null)
			{
				return new SecurityImpersonationBehavior.WindowsSidPrincipal(windowsSidIdentity, securityContext);
			}
			return SecurityImpersonationBehavior.AnonymousWindowsPrincipal;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000C4768 File Offset: 0x000C2968
		private ServiceSecurityContext GetAndCacheSecurityContext(ref MessageRpc rpc)
		{
			ServiceSecurityContext serviceSecurityContext = rpc.SecurityContext;
			if (!rpc.HasSecurityContext)
			{
				SecurityMessageProperty security = rpc.Request.Properties.Security;
				if (security == null)
				{
					serviceSecurityContext = null;
				}
				else
				{
					serviceSecurityContext = security.ServiceSecurityContext;
					if (serviceSecurityContext == null)
					{
						throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityContextMissing", new object[]
						{
							rpc.Operation.Name
						})), rpc.Request);
					}
				}
				rpc.SecurityContext = serviceSecurityContext;
				rpc.HasSecurityContext = true;
			}
			return serviceSecurityContext;
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000C47E4 File Offset: 0x000C29E4
		private string GetUpnFromDownlevelName(string downlevelName)
		{
			if (downlevelName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("downlevelName");
			}
			int num = downlevelName.IndexOf('\\');
			if (num < 0 || num == 0 || num == downlevelName.Length - 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("DownlevelNameCannotMapToUpn", new object[]
				{
					downlevelName
				})));
			}
			string text = downlevelName.Substring(0, num + 1);
			string str = downlevelName.Substring(num + 1);
			Dictionary<string, string> obj = this.domainNameMap;
			string text2;
			bool flag2;
			lock (obj)
			{
				flag2 = this.domainNameMap.TryGetValue(text, out text2);
			}
			if (!flag2)
			{
				uint capacity = 50U;
				StringBuilder stringBuilder = new StringBuilder((int)capacity);
				if (!SafeNativeMethods.TranslateName(text, EXTENDED_NAME_FORMAT.NameSamCompatible, EXTENDED_NAME_FORMAT.NameCanonical, stringBuilder, out capacity))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 122)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("DownlevelNameCannotMapToUpn", new object[]
						{
							downlevelName
						}), new Win32Exception(lastWin32Error)));
					}
					stringBuilder = new StringBuilder((int)capacity);
					if (!SafeNativeMethods.TranslateName(text, EXTENDED_NAME_FORMAT.NameSamCompatible, EXTENDED_NAME_FORMAT.NameCanonical, stringBuilder, out capacity))
					{
						lastWin32Error = Marshal.GetLastWin32Error();
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("DownlevelNameCannotMapToUpn", new object[]
						{
							downlevelName
						}), new Win32Exception(lastWin32Error)));
					}
				}
				stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
				text2 = stringBuilder.ToString();
				Dictionary<string, string> obj2 = this.domainNameMap;
				lock (obj2)
				{
					if (this.domainNameMap.Count >= 5)
					{
						if (this.random == null)
						{
							this.random = new Random((int)DateTime.Now.Ticks);
						}
						int num2 = this.random.Next() % this.domainNameMap.Count;
						foreach (string key in this.domainNameMap.Keys)
						{
							if (num2 <= 0)
							{
								this.domainNameMap.Remove(key);
								break;
							}
							num2--;
						}
					}
					this.domainNameMap[text] = text2;
				}
			}
			return str + "@" + text2;
		}

		// Token: 0x04002741 RID: 10049
		private PrincipalPermissionMode principalPermissionMode;

		// Token: 0x04002742 RID: 10050
		private object roleProvider;

		// Token: 0x04002743 RID: 10051
		private bool impersonateCallerForAllOperations;

		// Token: 0x04002744 RID: 10052
		private Dictionary<string, string> domainNameMap;

		// Token: 0x04002745 RID: 10053
		private Random random;

		// Token: 0x04002746 RID: 10054
		private const int maxDomainNameMapSize = 5;

		// Token: 0x04002747 RID: 10055
		private static WindowsPrincipal anonymousWindowsPrincipal;

		// Token: 0x04002748 RID: 10056
		private AuditLevel auditLevel;

		// Token: 0x04002749 RID: 10057
		private AuditLogLocation auditLogLocation;

		// Token: 0x0400274A RID: 10058
		private bool suppressAuditFailure = true;

		// Token: 0x02000C53 RID: 3155
		private class WindowsSidPrincipal : IPrincipal
		{
			// Token: 0x060077A3 RID: 30627 RVA: 0x001BF21A File Offset: 0x001BD41A
			public WindowsSidPrincipal(WindowsSidIdentity identity, ServiceSecurityContext securityContext)
			{
				this.identity = identity;
				this.securityContext = securityContext;
			}

			// Token: 0x17001B55 RID: 6997
			// (get) Token: 0x060077A4 RID: 30628 RVA: 0x001BF230 File Offset: 0x001BD430
			public IIdentity Identity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x060077A5 RID: 30629 RVA: 0x001BF238 File Offset: 0x001BD438
			public bool IsInRole(string role)
			{
				if (role == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("role");
				}
				NTAccount ntaccount = new NTAccount(role);
				System.IdentityModel.Claims.Claim claim = System.IdentityModel.Claims.Claim.CreateWindowsSidClaim((SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier)));
				System.IdentityModel.Policy.AuthorizationContext authorizationContext = this.securityContext.AuthorizationContext;
				for (int i = 0; i < authorizationContext.ClaimSets.Count; i++)
				{
					ClaimSet claimSet = authorizationContext.ClaimSets[i];
					if (claimSet.ContainsClaim(claim))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04004472 RID: 17522
			private WindowsSidIdentity identity;

			// Token: 0x04004473 RID: 17523
			private ServiceSecurityContext securityContext;
		}

		// Token: 0x02000C54 RID: 3156
		private class WindowsAnonymousIdentity
		{
			// Token: 0x060077A6 RID: 30630 RVA: 0x001BF2B8 File Offset: 0x001BD4B8
			public IDisposable Impersonate()
			{
				IntPtr currentThread = SafeNativeMethods.GetCurrentThread();
				SafeCloseHandle safeCloseHandle;
				if (!SafeNativeMethods.OpenCurrentThreadToken(currentThread, TokenAccessLevels.Impersonate, true, out safeCloseHandle))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
					if (lastWin32Error != 1008)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
					}
					safeCloseHandle = new SafeCloseHandle(IntPtr.Zero, false);
				}
				if (!SafeNativeMethods.ImpersonateAnonymousUserOnCurrentThread(currentThread))
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error2));
				}
				return new SecurityImpersonationBehavior.WindowsAnonymousIdentity.ImpersonationContext(currentThread, safeCloseHandle);
			}

			// Token: 0x02000F39 RID: 3897
			private class ImpersonationContext : IDisposable
			{
				// Token: 0x0600868F RID: 34447 RVA: 0x001F2B45 File Offset: 0x001F0D45
				public ImpersonationContext(IntPtr threadHandle, SafeCloseHandle tokenHandle)
				{
					this.threadHandle = threadHandle;
					this.tokenHandle = tokenHandle;
				}

				// Token: 0x06008690 RID: 34448 RVA: 0x001F2B5C File Offset: 0x001F0D5C
				private void Undo()
				{
					if (!SafeNativeMethods.SetCurrentThreadToken(IntPtr.Zero, this.tokenHandle))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityException(SR.GetString("RevertImpersonationFailure", new object[]
						{
							new Win32Exception(lastWin32Error).Message
						})));
					}
					this.tokenHandle.Close();
				}

				// Token: 0x06008691 RID: 34449 RVA: 0x001F2BBA File Offset: 0x001F0DBA
				public void Dispose()
				{
					if (!this.disposed)
					{
						this.Undo();
					}
					this.disposed = true;
				}

				// Token: 0x04004E32 RID: 20018
				private IntPtr threadHandle;

				// Token: 0x04004E33 RID: 20019
				private SafeCloseHandle tokenHandle;

				// Token: 0x04004E34 RID: 20020
				private bool disposed;
			}
		}
	}
}
