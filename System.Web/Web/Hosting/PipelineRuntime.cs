using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x02000297 RID: 663
	internal sealed class PipelineRuntime : MarshalByRefObject, IPipelineRuntime, IRegisteredObject
	{
		// Token: 0x060022B8 RID: 8888 RVA: 0x00095D5C File Offset: 0x00094D5C
		public IntPtr GetExecuteDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._executeDelegatePointer)
			{
				lock (PipelineRuntime._delegatelock)
				{
					if (IntPtr.Zero == PipelineRuntime._executeDelegatePointer)
					{
						ExecuteFunctionDelegate executeFunctionDelegate = new ExecuteFunctionDelegate(PipelineRuntime.ProcessRequestNotification);
						if (executeFunctionDelegate != null)
						{
							IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(executeFunctionDelegate);
							if (IntPtr.Zero != functionPointerForDelegate)
							{
								Thread.MemoryBarrier();
								PipelineRuntime._executeDelegate = executeFunctionDelegate;
								PipelineRuntime._executeDelegatePointer = functionPointerForDelegate;
							}
						}
					}
				}
			}
			return PipelineRuntime._executeDelegatePointer;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x00095DEC File Offset: 0x00094DEC
		public IntPtr GetDisposeDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._disposeDelegatePointer)
			{
				lock (PipelineRuntime._delegatelock)
				{
					if (IntPtr.Zero == PipelineRuntime._disposeDelegatePointer)
					{
						DisposeFunctionDelegate disposeFunctionDelegate = new DisposeFunctionDelegate(PipelineRuntime.DisposeHandler);
						if (disposeFunctionDelegate != null)
						{
							IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(disposeFunctionDelegate);
							if (IntPtr.Zero != functionPointerForDelegate)
							{
								Thread.MemoryBarrier();
								PipelineRuntime._disposeDelegate = disposeFunctionDelegate;
								PipelineRuntime._disposeDelegatePointer = functionPointerForDelegate;
							}
						}
					}
				}
			}
			return PipelineRuntime._disposeDelegatePointer;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00095E7C File Offset: 0x00094E7C
		public IntPtr GetRoleDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._roleDelegatePointer)
			{
				lock (PipelineRuntime._delegatelock)
				{
					if (IntPtr.Zero == PipelineRuntime._roleDelegatePointer)
					{
						RoleFunctionDelegate roleFunctionDelegate = new RoleFunctionDelegate(PipelineRuntime.RoleHandler);
						if (roleFunctionDelegate != null)
						{
							IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(roleFunctionDelegate);
							if (IntPtr.Zero != functionPointerForDelegate)
							{
								Thread.MemoryBarrier();
								PipelineRuntime._roleDelegate = roleFunctionDelegate;
								PipelineRuntime._roleDelegatePointer = functionPointerForDelegate;
							}
						}
					}
				}
			}
			return PipelineRuntime._roleDelegatePointer;
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00095F0C File Offset: 0x00094F0C
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public PipelineRuntime()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00095F1A File Offset: 0x00094F1A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00095F1D File Offset: 0x00094F1D
		public void StartProcessing()
		{
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00095F1F File Offset: 0x00094F1F
		public void StopProcessing()
		{
			if (UnsafeIISMethods.MgdHasConfigChanged() && !HostingEnvironment.ShutdownInitiated)
			{
				HttpRuntime.SetShutdownReason(ApplicationShutdownReason.ConfigurationChange, "IIS configuration change");
			}
			PipelineRuntime.s_StopProcessingCalled = true;
			HostingEnvironment.InitiateShutdown();
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x00095F45 File Offset: 0x00094F45
		internal static void WaitForRequestsToDrain()
		{
			while (!PipelineRuntime.s_StopProcessingCalled || PipelineRuntime._inIndicateCompletionCount > 0)
			{
				Thread.Sleep(250);
			}
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x00095F64 File Offset: 0x00094F64
		private StringBuilder FormatExceptionMessage(Exception e, string[] strings)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			if (strings != null)
			{
				for (int i = 0; i < strings.Length; i++)
				{
					stringBuilder.Append(strings[i]);
				}
			}
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				if (ex == e)
				{
					stringBuilder.Append("\r\n\r\nException: ");
				}
				else
				{
					stringBuilder.Append("\r\n\r\nInnerException: ");
				}
				stringBuilder.Append(ex.GetType().FullName);
				stringBuilder.Append("\r\nMessage: ");
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("\r\nStackTrace: ");
				stringBuilder.Append(ex.StackTrace);
			}
			return stringBuilder;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x00096008 File Offset: 0x00095008
		public void InitializeApplication(IntPtr appContext)
		{
			PipelineRuntime.s_ApplicationContext = appContext;
			HttpApplication httpApplication = null;
			try
			{
				HttpRuntime.UseIntegratedPipeline = true;
				if (!HttpRuntime.HostingInitFailed)
				{
					HttpWorkerRequest wr = new SimpleWorkerRequest("", "", new StringWriter(CultureInfo.InvariantCulture));
					HttpContext context = new HttpContext(wr);
					httpApplication = HttpApplicationFactory.GetPipelineApplicationInstance(appContext, context);
				}
			}
			catch (Exception initializationException)
			{
				if (HttpRuntime.InitializationException == null)
				{
					HttpRuntime.InitializationException = initializationException;
				}
			}
			finally
			{
				PipelineRuntime.s_InitializationCompleted = true;
				if (HttpRuntime.InitializationException != null)
				{
					int num = UnsafeIISMethods.MgdRegisterEventSubscription(appContext, "AspNetInitializationExceptionModule", RequestNotification.BeginRequest, (RequestNotification)0, "AspNetInitializationExceptionModule", "", new IntPtr(-1), false);
					if (num < 0)
					{
						throw new COMException(SR.GetString("Failed_Pipeline_Subscription", new object[]
						{
							"AspNetInitializationExceptionModule"
						}), num);
					}
					num = UnsafeIISMethods.MgdRegisterEventSubscription(appContext, "ManagedPipelineHandler", RequestNotification.ExecuteRequestHandler, (RequestNotification)0, string.Empty, "managedHandler", new IntPtr(-1), false);
					if (num < 0)
					{
						throw new COMException(SR.GetString("Failed_Pipeline_Subscription", new object[]
						{
							"ManagedPipelineHandler"
						}), num);
					}
				}
				if (httpApplication != null)
				{
					HttpApplicationFactory.RecyclePipelineApplicationInstance(httpApplication);
				}
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x00096138 File Offset: 0x00095138
		private static HttpContext UnwrapContext(IntPtr contextPtr)
		{
			return (HttpContext)GCHandle.FromIntPtr(contextPtr).Target;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00096158 File Offset: 0x00095158
		internal bool HostingShutdownInitiated
		{
			get
			{
				return HostingEnvironment.ShutdownInitiated;
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x00096160 File Offset: 0x00095160
		internal static bool RoleHandler(IntPtr pManagedPrincipal, IntPtr pszRole, int cchRole, bool disposing)
		{
			GCHandle gchandle = GCHandle.FromIntPtr(pManagedPrincipal);
			IPrincipal principal = (IPrincipal)gchandle.Target;
			if (principal == null)
			{
				return false;
			}
			if (disposing)
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				WindowsIdentity windowsIdentity = principal.Identity as WindowsIdentity;
				if (windowsIdentity != null)
				{
					windowsIdentity.Dispose();
				}
				return false;
			}
			return principal.IsInRole(StringUtil.StringFromWCharPtr(pszRole, cchRole));
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000961C0 File Offset: 0x000951C0
		internal static void DisposeHandler(IntPtr managedHttpContext)
		{
			HttpContext context = PipelineRuntime.UnwrapContext(managedHttpContext);
			PipelineRuntime.DisposeHandlerPrivate(context);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000961DA File Offset: 0x000951DA
		internal static void DisposeHandler(HttpContext context, IntPtr nativeRequestContext, RequestNotificationStatus status)
		{
			if (UnsafeIISMethods.MgdCanDisposeManagedContext(nativeRequestContext, status))
			{
				PipelineRuntime.DisposeHandlerPrivate(context);
			}
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000961EC File Offset: 0x000951EC
		private static void DisposeHandlerPrivate(HttpContext context)
		{
			try
			{
				context.FinishPipelineRequest();
				IIS7WorkerRequest iis7WorkerRequest = context.WorkerRequest as IIS7WorkerRequest;
				if (iis7WorkerRequest != null)
				{
					iis7WorkerRequest.Dispose();
				}
				PerfCounters.DecrementCounter(AppPerfCounter.REQUESTS_EXECUTING);
				context.DisposePrincipal();
			}
			finally
			{
				if (context != null)
				{
					context.Unroot();
				}
				HttpRuntime.DecrementActivePipelineCount();
			}
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x00096244 File Offset: 0x00095244
		internal static int ProcessRequestNotification(IntPtr managedHttpContext, IntPtr nativeRequestContext, IntPtr moduleData, int flags)
		{
			int result;
			try
			{
				result = PipelineRuntime.ProcessRequestNotificationHelper(managedHttpContext, nativeRequestContext, moduleData, flags);
			}
			catch (Exception e)
			{
				ApplicationManager.RecordFatalException(e);
				throw;
			}
			return result;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00096278 File Offset: 0x00095278
		internal static int ProcessRequestNotificationHelper(IntPtr managedHttpContext, IntPtr nativeRequestContext, IntPtr moduleData, int flags)
		{
			IIS7WorkerRequest wr = null;
			HttpContext httpContext = null;
			RequestNotificationStatus requestNotificationStatus = RequestNotificationStatus.Continue;
			if (managedHttpContext == IntPtr.Zero)
			{
				PipelineRuntime.InitializeRequestContext(nativeRequestContext, flags, out wr, out httpContext);
				if (httpContext == null)
				{
					return 2;
				}
				httpContext.Root();
				UnsafeIISMethods.MgdSetManagedHttpContext(nativeRequestContext, httpContext.ContextPtr);
				HttpRuntime.IncrementActivePipelineCount();
			}
			else
			{
				httpContext = PipelineRuntime.UnwrapContext(managedHttpContext);
				wr = (httpContext.WorkerRequest as IIS7WorkerRequest);
			}
			if (httpContext.InIndicateCompletion && httpContext.CurrentThread != Thread.CurrentThread && 536870912 != UnsafeIISMethods.MgdGetCurrentNotification(nativeRequestContext))
			{
				while (httpContext.InIndicateCompletion)
				{
					Thread.Sleep(10);
				}
			}
			NotificationContext notificationContext = httpContext.NotificationContext;
			bool flag = false;
			try
			{
				bool flag2 = notificationContext != null;
				if (flag2)
				{
					httpContext.ApplicationInstance.AcquireNotifcationContextLock(ref flag);
				}
				httpContext.NotificationContext = new NotificationContext(flags, flag2);
				requestNotificationStatus = HttpRuntime.ProcessRequestNotification(wr, httpContext);
			}
			finally
			{
				if (requestNotificationStatus != RequestNotificationStatus.Pending)
				{
					httpContext.NotificationContext = notificationContext;
				}
				if (flag)
				{
					httpContext.ApplicationInstance.ReleaseNotifcationContextLock();
				}
			}
			if (requestNotificationStatus != RequestNotificationStatus.Pending)
			{
				HttpApplication.ThreadContext indicateCompletionContext = httpContext.IndicateCompletionContext;
				if (!httpContext.InIndicateCompletion && indicateCompletionContext != null)
				{
					if (requestNotificationStatus == RequestNotificationStatus.Continue)
					{
						try
						{
							httpContext.InIndicateCompletion = true;
							Interlocked.Increment(ref PipelineRuntime._inIndicateCompletionCount);
							UnsafeIISMethods.MgdIndicateCompletion(nativeRequestContext, ref requestNotificationStatus);
							return (int)requestNotificationStatus;
						}
						finally
						{
							Interlocked.Decrement(ref PipelineRuntime._inIndicateCompletionCount);
							if (!indicateCompletionContext.HasLeaveBeenCalled)
							{
								lock (indicateCompletionContext)
								{
									if (!indicateCompletionContext.HasLeaveBeenCalled)
									{
										indicateCompletionContext.Leave();
										httpContext.IndicateCompletionContext = null;
										httpContext.InIndicateCompletion = false;
									}
								}
							}
						}
					}
					if (!indicateCompletionContext.HasLeaveBeenCalled)
					{
						lock (indicateCompletionContext)
						{
							if (!indicateCompletionContext.HasLeaveBeenCalled)
							{
								indicateCompletionContext.Leave();
								httpContext.IndicateCompletionContext = null;
								httpContext.InIndicateCompletion = false;
							}
						}
					}
				}
			}
			return (int)requestNotificationStatus;
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0009646C File Offset: 0x0009546C
		private static void InitializeRequestContext(IntPtr nativeRequestContext, int flags, out IIS7WorkerRequest wr, out HttpContext context)
		{
			wr = null;
			context = null;
			try
			{
				bool etwProviderEnabled = (flags & 64) == 64;
				wr = IIS7WorkerRequest.CreateWorkerRequest(nativeRequestContext, etwProviderEnabled);
				context = new HttpContext(wr, false);
			}
			catch
			{
				UnsafeIISMethods.MgdSetBadRequestStatus(nativeRequestContext);
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000964B8 File Offset: 0x000954B8
		[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
		void IRegisteredObject.Stop(bool immediate)
		{
			while (!PipelineRuntime.s_InitializationCompleted && !PipelineRuntime.s_StopProcessingCalled)
			{
				Thread.Sleep(250);
			}
			PipelineRuntime.RemoveThisAppDomainFromUnmanagedTable();
			HostingEnvironment.UnregisterObject(this);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000964DF File Offset: 0x000954DF
		internal void SetThisAppDomainsIsapiAppId(string appId)
		{
			PipelineRuntime.s_thisAppDomainsIsapiAppId = appId;
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000964E8 File Offset: 0x000954E8
		internal static void RemoveThisAppDomainFromUnmanagedTable()
		{
			if (Interlocked.Exchange(ref PipelineRuntime.s_isThisAppDomainRemovedFromUnmanagedTable, 1) != 0)
			{
				return;
			}
			try
			{
				if (PipelineRuntime.s_thisAppDomainsIsapiAppId != null && PipelineRuntime.s_ApplicationContext != IntPtr.Zero)
				{
					UnsafeIISMethods.MgdAppDomainShutdown(PipelineRuntime.s_ApplicationContext);
				}
				HttpRuntime.AddAppDomainTraceMessage(SR.GetString("App_Domain_Restart"));
			}
			catch (Exception ex)
			{
				if (PipelineRuntime.ShouldRethrowException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x00096554 File Offset: 0x00095554
		internal static bool ShouldRethrowException(Exception ex)
		{
			return ex is NullReferenceException || ex is AccessViolationException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException;
		}

		// Token: 0x04001B62 RID: 7010
		internal const string InitExceptionModuleName = "AspNetInitializationExceptionModule";

		// Token: 0x04001B63 RID: 7011
		private const string s_InitExceptionModulePrecondition = "";

		// Token: 0x04001B64 RID: 7012
		private static int s_isThisAppDomainRemovedFromUnmanagedTable;

		// Token: 0x04001B65 RID: 7013
		private static IntPtr s_ApplicationContext;

		// Token: 0x04001B66 RID: 7014
		private static string s_thisAppDomainsIsapiAppId;

		// Token: 0x04001B67 RID: 7015
		private static bool s_StopProcessingCalled;

		// Token: 0x04001B68 RID: 7016
		private static bool s_InitializationCompleted;

		// Token: 0x04001B69 RID: 7017
		private static object _delegatelock = new object();

		// Token: 0x04001B6A RID: 7018
		private static int _inIndicateCompletionCount;

		// Token: 0x04001B6B RID: 7019
		private static IntPtr _executeDelegatePointer = IntPtr.Zero;

		// Token: 0x04001B6C RID: 7020
		private static ExecuteFunctionDelegate _executeDelegate = null;

		// Token: 0x04001B6D RID: 7021
		private static IntPtr _disposeDelegatePointer = IntPtr.Zero;

		// Token: 0x04001B6E RID: 7022
		private static DisposeFunctionDelegate _disposeDelegate = null;

		// Token: 0x04001B6F RID: 7023
		private static IntPtr _roleDelegatePointer = IntPtr.Zero;

		// Token: 0x04001B70 RID: 7024
		private static RoleFunctionDelegate _roleDelegate = null;
	}
}
