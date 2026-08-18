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
	// Token: 0x020007BA RID: 1978
	internal sealed class PipelineRuntime : MarshalByRefObject, IPipelineRuntime, IRegisteredObject
	{
		// Token: 0x06005EE0 RID: 24288 RVA: 0x00147680 File Offset: 0x00145880
		public IntPtr GetAsyncCompletionDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._asyncCompletionDelegatePointer)
			{
				object delegatelock = PipelineRuntime._delegatelock;
				lock (delegatelock)
				{
					if (IntPtr.Zero == PipelineRuntime._asyncCompletionDelegatePointer)
					{
						AsyncCompletionDelegate asyncCompletionDelegate = new AsyncCompletionDelegate(PipelineRuntime.AsyncCompletionHandler);
						if (asyncCompletionDelegate != null)
						{
							IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(asyncCompletionDelegate);
							if (IntPtr.Zero != functionPointerForDelegate)
							{
								PipelineRuntime._asyncCompletionDelegate = asyncCompletionDelegate;
								PipelineRuntime._asyncCompletionDelegatePointer = functionPointerForDelegate;
							}
						}
					}
				}
			}
			return PipelineRuntime._asyncCompletionDelegatePointer;
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x00147710 File Offset: 0x00145910
		public IntPtr GetAsyncDisconnectNotificationDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._asyncDisconnectNotificationDelegatePointer)
			{
				object delegatelock = PipelineRuntime._delegatelock;
				lock (delegatelock)
				{
					if (IntPtr.Zero == PipelineRuntime._asyncDisconnectNotificationDelegatePointer)
					{
						AsyncDisconnectNotificationDelegate asyncDisconnectNotificationDelegate = new AsyncDisconnectNotificationDelegate(PipelineRuntime.AsyncDisconnectNotificationHandler);
						if (asyncDisconnectNotificationDelegate != null)
						{
							IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(asyncDisconnectNotificationDelegate);
							if (IntPtr.Zero != functionPointerForDelegate)
							{
								PipelineRuntime._asyncDisconnectNotificationDelegate = asyncDisconnectNotificationDelegate;
								PipelineRuntime._asyncDisconnectNotificationDelegatePointer = functionPointerForDelegate;
							}
						}
					}
				}
			}
			return PipelineRuntime._asyncDisconnectNotificationDelegatePointer;
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x001477A0 File Offset: 0x001459A0
		public IntPtr GetExecuteDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._executeDelegatePointer)
			{
				object delegatelock = PipelineRuntime._delegatelock;
				lock (delegatelock)
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

		// Token: 0x06005EE3 RID: 24291 RVA: 0x00147838 File Offset: 0x00145A38
		public IntPtr GetDisposeDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._disposeDelegatePointer)
			{
				object delegatelock = PipelineRuntime._delegatelock;
				lock (delegatelock)
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

		// Token: 0x06005EE4 RID: 24292 RVA: 0x001478D0 File Offset: 0x00145AD0
		public IntPtr GetRoleDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._roleDelegatePointer)
			{
				object delegatelock = PipelineRuntime._delegatelock;
				lock (delegatelock)
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

		// Token: 0x06005EE5 RID: 24293 RVA: 0x00147968 File Offset: 0x00145B68
		public IntPtr GetPrincipalDelegate()
		{
			if (IntPtr.Zero == PipelineRuntime._principalDelegatePointer)
			{
				object delegatelock = PipelineRuntime._delegatelock;
				lock (delegatelock)
				{
					if (IntPtr.Zero == PipelineRuntime._principalDelegatePointer)
					{
						PrincipalFunctionDelegate principalFunctionDelegate = new PrincipalFunctionDelegate(PipelineRuntime.GetManagedPrincipalHandler);
						if (principalFunctionDelegate != null)
						{
							IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(principalFunctionDelegate);
							if (IntPtr.Zero != functionPointerForDelegate)
							{
								Thread.MemoryBarrier();
								PipelineRuntime._principalDelegate = principalFunctionDelegate;
								PipelineRuntime._principalDelegatePointer = functionPointerForDelegate;
							}
						}
					}
				}
			}
			return PipelineRuntime._principalDelegatePointer;
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x000474BC File Offset: 0x000456BC
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public PipelineRuntime()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x0000298D File Offset: 0x00000B8D
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x00147A00 File Offset: 0x00145C00
		public void StartProcessing()
		{
			HostingEnvironment.SetupStopListeningHandler();
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x00147A07 File Offset: 0x00145C07
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		public void StopProcessing()
		{
			if (!HostingEnvironment.StopListeningWasCalled && !HostingEnvironment.ShutdownInitiated)
			{
				HttpRuntime.SetShutdownReason(ApplicationShutdownReason.ConfigurationChange, "IIS configuration change");
			}
			PipelineRuntime.s_StopProcessingCalled = true;
			HostingEnvironment.InitiateShutdownWithoutDemand();
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x00147A2D File Offset: 0x00145C2D
		internal static void WaitForRequestsToDrain()
		{
			if (PipelineRuntime.s_ApplicationContext == IntPtr.Zero)
			{
				return;
			}
			while (!PipelineRuntime.s_StopProcessingCalled || PipelineRuntime._inIndicateCompletionCount > 0)
			{
				Thread.Sleep(250);
			}
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x00147A5C File Offset: 0x00145C5C
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

		// Token: 0x06005EEC RID: 24300 RVA: 0x00147B00 File Offset: 0x00145D00
		public void InitializeApplication(IntPtr appContext)
		{
			PipelineRuntime.s_ApplicationContext = appContext;
			HttpRuntime.PopulateIISVersionInformation();
			HttpApplication httpApplication = null;
			try
			{
				if (!HttpRuntime.HostingInitFailed)
				{
					HttpWorkerRequest wr = new SimpleWorkerRequest("", "", new StringWriter(CultureInfo.InvariantCulture));
					MimeMapping.SetIntegratedApplicationContext(appContext);
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

		// Token: 0x06005EED RID: 24301 RVA: 0x00147C2C File Offset: 0x00145E2C
		private static HttpContext UnwrapContext(IntPtr rootedObjectsPointer)
		{
			RootedObjects rootedObjects = RootedObjects.FromPointer(rootedObjectsPointer);
			return rootedObjects.HttpContext;
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x00147C46 File Offset: 0x00145E46
		internal bool HostingShutdownInitiated
		{
			get
			{
				return HostingEnvironment.ShutdownInitiated;
			}
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x00147C50 File Offset: 0x00145E50
		internal static void AsyncCompletionHandler(IntPtr rootedObjectsPointer, int bytesCompleted, int hresult, IntPtr pAsyncCompletionContext)
		{
			HttpContext httpContext = PipelineRuntime.UnwrapContext(rootedObjectsPointer);
			IIS7WorkerRequest iis7WorkerRequest = httpContext.WorkerRequest as IIS7WorkerRequest;
			iis7WorkerRequest.OnAsyncCompletion(bytesCompleted, hresult, pAsyncCompletionContext);
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x00147C7C File Offset: 0x00145E7C
		internal static void AsyncDisconnectNotificationHandler(IntPtr pManagedRootedObjects)
		{
			if (pManagedRootedObjects != IntPtr.Zero)
			{
				RootedObjects rootedObjects = RootedObjects.FromPointer(pManagedRootedObjects);
				if (rootedObjects != null)
				{
					IIS7WorkerRequest workerRequest = rootedObjects.WorkerRequest;
					if (workerRequest != null)
					{
						workerRequest.NotifyOfAsyncDisconnect();
					}
				}
			}
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x00147CB0 File Offset: 0x00145EB0
		internal static int RoleHandler(IntPtr pRootedObjects, IntPtr pszRole, int cchRole, out bool isInRole)
		{
			isInRole = false;
			IPrincipal principal = RootedObjects.FromPointer(pRootedObjects).Principal;
			if (principal != null)
			{
				try
				{
					isInRole = principal.IsInRole(StringUtil.StringFromWCharPtr(pszRole, cchRole));
				}
				catch (Exception e)
				{
					return Marshal.GetHRForException(e);
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x00147D00 File Offset: 0x00145F00
		internal static IntPtr GetManagedPrincipalHandler(IntPtr pRootedObjects, int requestingAppDomainId)
		{
			if (requestingAppDomainId != AppDomain.CurrentDomain.Id)
			{
				return IntPtr.Zero;
			}
			IPrincipal principal = RootedObjects.FromPointer(pRootedObjects).Principal;
			return GCUtil.RootObject(principal);
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x00147D34 File Offset: 0x00145F34
		internal static void DisposeHandler(IntPtr rootedObjectsPointer)
		{
			RootedObjects rootedObjects = RootedObjects.FromPointer(rootedObjectsPointer);
			rootedObjects.Destroy();
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x00147D4E File Offset: 0x00145F4E
		internal static void DisposeHandler(HttpContext context, IntPtr nativeRequestContext, RequestNotificationStatus status)
		{
			if (UnsafeIISMethods.MgdCanDisposeManagedContext(nativeRequestContext, status))
			{
				context.RootedObjects.Destroy();
			}
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x00147D64 File Offset: 0x00145F64
		internal static int ProcessRequestNotification(IntPtr rootedObjectsPointer, IntPtr nativeRequestContext, IntPtr moduleData, int flags)
		{
			int result;
			try
			{
				result = PipelineRuntime.ProcessRequestNotificationHelper(rootedObjectsPointer, nativeRequestContext, moduleData, flags);
			}
			catch (Exception e)
			{
				ApplicationManager.RecordFatalException(e);
				throw;
			}
			return result;
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x00147D98 File Offset: 0x00145F98
		internal static int ProcessRequestNotificationHelper(IntPtr rootedObjectsPointer, IntPtr nativeRequestContext, IntPtr moduleData, int flags)
		{
			IIS7WorkerRequest iis7WorkerRequest = null;
			HttpContext httpContext = null;
			RequestNotificationStatus requestNotificationStatus = RequestNotificationStatus.Continue;
			bool flag = false;
			RootedObjects rootedObjects;
			if (rootedObjectsPointer == IntPtr.Zero)
			{
				PipelineRuntime.InitializeRequestContext(nativeRequestContext, flags, out iis7WorkerRequest, out httpContext);
				flag = true;
				if (httpContext == null)
				{
					return 2;
				}
				rootedObjects = RootedObjects.Create();
				rootedObjects.HttpContext = httpContext;
				rootedObjects.WorkerRequest = iis7WorkerRequest;
				rootedObjects.WriteTransferEventIfNecessary();
				httpContext.RootedObjects = rootedObjects;
				UnsafeIISMethods.MgdSetManagedHttpContext(nativeRequestContext, rootedObjects.Pointer);
			}
			else
			{
				rootedObjects = RootedObjects.FromPointer(rootedObjectsPointer);
				httpContext = rootedObjects.HttpContext;
				iis7WorkerRequest = rootedObjects.WorkerRequest;
			}
			int result;
			using (rootedObjects.WithinTraceBlock())
			{
				if (flag)
				{
					AspNetEventSource.Instance.RequestStarted(iis7WorkerRequest);
				}
				int num;
				bool isPostNotification;
				int num2;
				UnsafeIISMethods.MgdGetCurrentNotificationInfo(nativeRequestContext, out num, out isPostNotification, out num2);
				if (httpContext == null || httpContext.HasWebSocketRequestTransitionStarted)
				{
					result = 0;
				}
				else
				{
					if (httpContext.InIndicateCompletion && httpContext.ThreadInsideIndicateCompletion != Thread.CurrentThread && 536870912 != num2)
					{
						while (httpContext.InIndicateCompletion)
						{
							Thread.Sleep(10);
						}
					}
					NotificationContext notificationContext = httpContext.NotificationContext;
					bool isInCancellablePeriod = httpContext.IsInCancellablePeriod;
					bool flag2 = false;
					try
					{
						if (isInCancellablePeriod)
						{
							httpContext.EndCancellablePeriod();
						}
						bool flag3 = notificationContext != null;
						if (flag3)
						{
							httpContext.ApplicationInstance.AcquireNotifcationContextLock(ref flag2);
						}
						httpContext.NotificationContext = new NotificationContext(flags, flag3);
						Action<RequestNotificationStatus> action = null;
						if (AppVerifier.IsAppVerifierEnabled)
						{
							action = AppVerifier.GetRequestNotificationStatusCheckDelegate(httpContext, (RequestNotification)num2, isPostNotification);
						}
						requestNotificationStatus = HttpRuntime.ProcessRequestNotification(iis7WorkerRequest, httpContext);
						if (action != null)
						{
							AppVerifier.InvokeVerifierCheck<RequestNotificationStatus>(action, requestNotificationStatus);
						}
					}
					finally
					{
						if (requestNotificationStatus != RequestNotificationStatus.Pending)
						{
							httpContext.NotificationContext = notificationContext;
							if (isInCancellablePeriod && !httpContext.IsInCancellablePeriod)
							{
								httpContext.BeginCancellablePeriod();
							}
							else if (!isInCancellablePeriod && httpContext.IsInCancellablePeriod)
							{
								httpContext.EndCancellablePeriod();
							}
						}
						if (flag2)
						{
							httpContext.ApplicationInstance.ReleaseNotifcationContextLock();
						}
					}
					if (requestNotificationStatus != RequestNotificationStatus.Pending)
					{
						UnsafeIISMethods.MgdGetCurrentNotificationInfo(nativeRequestContext, out num, out isPostNotification, out num2);
						ThreadContext indicateCompletionContext = httpContext.IndicateCompletionContext;
						if (!httpContext.InIndicateCompletion && httpContext.IndicateCompletionContext != null)
						{
							if (requestNotificationStatus == RequestNotificationStatus.Continue)
							{
								try
								{
									httpContext.InIndicateCompletion = true;
									Interlocked.Increment(ref PipelineRuntime._inIndicateCompletionCount);
									httpContext.ThreadInsideIndicateCompletion = Thread.CurrentThread;
									UnsafeIISMethods.MgdIndicateCompletion(nativeRequestContext, ref requestNotificationStatus);
									goto IL_2AF;
								}
								finally
								{
									httpContext.ThreadInsideIndicateCompletion = null;
									Interlocked.Decrement(ref PipelineRuntime._inIndicateCompletionCount);
									if (!indicateCompletionContext.HasBeenDisassociatedFromThread || httpContext.InIndicateCompletion)
									{
										ThreadContext obj = indicateCompletionContext;
										lock (obj)
										{
											if (!indicateCompletionContext.HasBeenDisassociatedFromThread)
											{
												indicateCompletionContext.DisassociateFromCurrentThread();
											}
											httpContext.IndicateCompletionContext = null;
											httpContext.InIndicateCompletion = false;
										}
									}
								}
							}
							if (!indicateCompletionContext.HasBeenDisassociatedFromThread || httpContext.InIndicateCompletion)
							{
								ThreadContext obj2 = indicateCompletionContext;
								lock (obj2)
								{
									if (!indicateCompletionContext.HasBeenDisassociatedFromThread)
									{
										indicateCompletionContext.DisassociateFromCurrentThread();
									}
									httpContext.IndicateCompletionContext = null;
									httpContext.InIndicateCompletion = false;
								}
							}
						}
					}
					IL_2AF:
					if (httpContext.HasWebSocketRequestTransitionStarted && requestNotificationStatus == RequestNotificationStatus.Pending && httpContext.DidCurrentThreadStartWebSocketTransition)
					{
						rootedObjects.ReleaseHttpContext();
						rootedObjects.WebSocketPipeline.ProcessRequest();
					}
					result = (int)requestNotificationStatus;
				}
			}
			return result;
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x0014810C File Offset: 0x0014630C
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

		// Token: 0x06005EF8 RID: 24312 RVA: 0x00148158 File Offset: 0x00146358
		void IRegisteredObject.Stop(bool immediate)
		{
			while (!PipelineRuntime.s_InitializationCompleted && !PipelineRuntime.s_StopProcessingCalled)
			{
				Thread.Sleep(250);
			}
			PipelineRuntime.RemoveThisAppDomainFromUnmanagedTable();
			HostingEnvironment.UnregisterObject(this);
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x0014817F File Offset: 0x0014637F
		internal void SetThisAppDomainsIsapiAppId(string appId)
		{
			PipelineRuntime.s_thisAppDomainsIsapiAppId = appId;
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x00148188 File Offset: 0x00146388
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

		// Token: 0x06005EFB RID: 24315 RVA: 0x001481F8 File Offset: 0x001463F8
		internal static bool ShouldRethrowException(Exception ex)
		{
			return ex is NullReferenceException || ex is AccessViolationException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException;
		}

		// Token: 0x04003173 RID: 12659
		internal const string InitExceptionModuleName = "AspNetInitializationExceptionModule";

		// Token: 0x04003174 RID: 12660
		private const string s_InitExceptionModulePrecondition = "";

		// Token: 0x04003175 RID: 12661
		private static int s_isThisAppDomainRemovedFromUnmanagedTable;

		// Token: 0x04003176 RID: 12662
		private static IntPtr s_ApplicationContext;

		// Token: 0x04003177 RID: 12663
		private static string s_thisAppDomainsIsapiAppId;

		// Token: 0x04003178 RID: 12664
		private static bool s_StopProcessingCalled;

		// Token: 0x04003179 RID: 12665
		private static bool s_InitializationCompleted;

		// Token: 0x0400317A RID: 12666
		private static object _delegatelock = new object();

		// Token: 0x0400317B RID: 12667
		private static int _inIndicateCompletionCount;

		// Token: 0x0400317C RID: 12668
		private static IntPtr _asyncCompletionDelegatePointer = IntPtr.Zero;

		// Token: 0x0400317D RID: 12669
		private static AsyncCompletionDelegate _asyncCompletionDelegate = null;

		// Token: 0x0400317E RID: 12670
		private static IntPtr _asyncDisconnectNotificationDelegatePointer = IntPtr.Zero;

		// Token: 0x0400317F RID: 12671
		private static AsyncDisconnectNotificationDelegate _asyncDisconnectNotificationDelegate = null;

		// Token: 0x04003180 RID: 12672
		private static IntPtr _executeDelegatePointer = IntPtr.Zero;

		// Token: 0x04003181 RID: 12673
		private static ExecuteFunctionDelegate _executeDelegate = null;

		// Token: 0x04003182 RID: 12674
		private static IntPtr _disposeDelegatePointer = IntPtr.Zero;

		// Token: 0x04003183 RID: 12675
		private static DisposeFunctionDelegate _disposeDelegate = null;

		// Token: 0x04003184 RID: 12676
		private static IntPtr _roleDelegatePointer = IntPtr.Zero;

		// Token: 0x04003185 RID: 12677
		private static RoleFunctionDelegate _roleDelegate = null;

		// Token: 0x04003186 RID: 12678
		private static IntPtr _principalDelegatePointer = IntPtr.Zero;

		// Token: 0x04003187 RID: 12679
		private static PrincipalFunctionDelegate _principalDelegate = null;
	}
}
