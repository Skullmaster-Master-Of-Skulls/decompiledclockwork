using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200007B RID: 123
	internal class HttpApplicationFactory
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0000FB43 File Offset: 0x0000DD43
		internal HttpApplicationFactory()
		{
			this._sessionOnEndEventHandlerAspCompatHelper = new EventHandler(this.SessionOnEndEventHandlerAspCompatHelper);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000FB73 File Offset: 0x0000DD73
		internal static void ThrowIfApplicationOnStartCalled()
		{
			if (HttpApplicationFactory._theApplicationFactory._appOnStartCalled)
			{
				throw new InvalidOperationException(SR.GetString("MethodCannotBeCalledAfterAppStart"));
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000FB94 File Offset: 0x0000DD94
		private void Init()
		{
			if (HttpApplicationFactory._customApplication != null)
			{
				return;
			}
			try
			{
				try
				{
					this._appFilename = HttpApplicationFactory.GetApplicationFile();
					this.CompileApplication();
				}
				finally
				{
					this.SetupChangesMonitor();
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
		internal static void SetupFileChangeNotifications()
		{
			if (HttpRuntime.CodegenDirInternal != null)
			{
				HttpApplicationFactory._theApplicationFactory.EnsureInited();
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		private void EnsureInited()
		{
			if (!this._inited)
			{
				lock (this)
				{
					if (!this._inited)
					{
						this.Init();
						this._inited = true;
					}
				}
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000FC50 File Offset: 0x0000DE50
		internal static void EnsureAppStartCalledForIntegratedMode(HttpContext context, HttpApplication app)
		{
			if (!HttpApplicationFactory._theApplicationFactory._appOnStartCalled)
			{
				Exception ex = null;
				HttpApplicationFactory theApplicationFactory = HttpApplicationFactory._theApplicationFactory;
				lock (theApplicationFactory)
				{
					if (!HttpApplicationFactory._theApplicationFactory._appOnStartCalled)
					{
						using (new DisposableHttpContextWrapper(context))
						{
							WebBaseEvent.RaiseSystemEvent(HttpApplicationFactory._theApplicationFactory, 1001);
							if (HttpApplicationFactory._theApplicationFactory._onStartMethod != null)
							{
								app.ProcessSpecialRequest(context, HttpApplicationFactory._theApplicationFactory._onStartMethod, HttpApplicationFactory._theApplicationFactory._onStartParamCount, HttpApplicationFactory._theApplicationFactory, EventArgs.Empty, null);
							}
						}
					}
					HttpApplicationFactory._theApplicationFactory._appOnStartCalled = true;
					ex = context.Error;
				}
				if (ex != null)
				{
					throw new HttpException(ex.Message, ex);
				}
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000FD30 File Offset: 0x0000DF30
		private void EnsureAppStartCalled(HttpContext context)
		{
			if (!this._appOnStartCalled)
			{
				lock (this)
				{
					if (!this._appOnStartCalled)
					{
						using (new DisposableHttpContextWrapper(context))
						{
							WebBaseEvent.RaiseSystemEvent(this, 1001);
							this.FireApplicationOnStart(context);
						}
						this._appOnStartCalled = true;
					}
				}
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		internal static string GetApplicationFile()
		{
			return Path.Combine(HttpRuntime.AppDomainAppPathInternal, "global.asax");
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		private void CompileApplication()
		{
			this._theApplicationType = BuildManager.GetGlobalAsaxType();
			BuildResultCompiledGlobalAsaxType globalAsaxBuildResult = BuildManager.GetGlobalAsaxBuildResult();
			if (globalAsaxBuildResult != null)
			{
				if (globalAsaxBuildResult.HasAppOrSessionObjects)
				{
					this.GetAppStateByParsingGlobalAsax();
				}
				this._fileDependencies = globalAsaxBuildResult.VirtualPathDependencies;
			}
			if (this._state == null)
			{
				this._state = new HttpApplicationState();
			}
			this.ReflectOnApplicationType();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000FE18 File Offset: 0x0000E018
		private void GetAppStateByParsingGlobalAsax()
		{
			using (new ApplicationImpersonationContext())
			{
				if (FileUtil.FileExists(this._appFilename))
				{
					ApplicationFileParser applicationFileParser = new ApplicationFileParser();
					AssemblySet referencedAssemblies = Util.GetReferencedAssemblies(this._theApplicationType.Assembly);
					referencedAssemblies.Add(typeof(string).Assembly);
					VirtualPath virtualPath = HttpRuntime.AppDomainAppVirtualPathObject.SimpleCombine("global.asax");
					applicationFileParser.Parse(referencedAssemblies, virtualPath);
					this._state = new HttpApplicationState(applicationFileParser.ApplicationObjects, applicationFileParser.SessionObjects);
				}
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		private bool ReflectOnMethodInfoIfItLooksLikeEventHandler(MethodInfo m)
		{
			if (m.ReturnType != typeof(void))
			{
				return false;
			}
			ParameterInfo[] parameters = m.GetParameters();
			int num = parameters.Length;
			if (num != 0)
			{
				if (num != 2)
				{
					return false;
				}
				if (parameters[0].ParameterType != typeof(object))
				{
					return false;
				}
				if (parameters[1].ParameterType != typeof(EventArgs) && !parameters[1].ParameterType.IsSubclassOf(typeof(EventArgs)))
				{
					return false;
				}
			}
			string name = m.Name;
			int num2 = name.IndexOf('_');
			if (num2 <= 0 || num2 > name.Length - 1)
			{
				return false;
			}
			if (StringUtil.EqualsIgnoreCase(name, "Application_OnStart") || StringUtil.EqualsIgnoreCase(name, "Application_Start"))
			{
				this._onStartMethod = m;
				this._onStartParamCount = parameters.Length;
			}
			else if (StringUtil.EqualsIgnoreCase(name, "Application_OnEnd") || StringUtil.EqualsIgnoreCase(name, "Application_End"))
			{
				this._onEndMethod = m;
				this._onEndParamCount = parameters.Length;
			}
			else if (StringUtil.EqualsIgnoreCase(name, "Session_OnEnd") || StringUtil.EqualsIgnoreCase(name, "Session_End"))
			{
				this._sessionOnEndMethod = m;
				this._sessionOnEndParamCount = parameters.Length;
			}
			return true;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		private void ReflectOnApplicationType()
		{
			ArrayList arrayList = new ArrayList();
			MethodInfo[] methods = this._theApplicationType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				if (this.ReflectOnMethodInfoIfItLooksLikeEventHandler(methodInfo))
				{
					arrayList.Add(methodInfo);
				}
			}
			Type baseType = this._theApplicationType.BaseType;
			if (baseType != null && baseType != typeof(HttpApplication))
			{
				methods = baseType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
				foreach (MethodInfo methodInfo2 in methods)
				{
					if (methodInfo2.IsPrivate && this.ReflectOnMethodInfoIfItLooksLikeEventHandler(methodInfo2))
					{
						arrayList.Add(methodInfo2);
					}
				}
			}
			this._eventHandlerMethods = new MethodInfo[arrayList.Count];
			for (int k = 0; k < this._eventHandlerMethods.Length; k++)
			{
				this._eventHandlerMethods[k] = (MethodInfo)arrayList[k];
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000100D8 File Offset: 0x0000E2D8
		private void SetupChangesMonitor()
		{
			FileChangeEventHandler callback = new FileChangeEventHandler(this.OnAppFileChange);
			HttpRuntime.FileChangesMonitor.StartMonitoringFile(this._appFilename, callback);
			if (this._fileDependencies != null)
			{
				foreach (object obj in this._fileDependencies)
				{
					string virtualPath = (string)obj;
					HttpRuntime.FileChangesMonitor.StartMonitoringFile(HostingEnvironment.MapPathInternal(virtualPath), callback);
				}
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00010164 File Offset: 0x0000E364
		private void OnAppFileChange(object sender, FileChangeEvent e)
		{
			string text = FileChangesMonitor.GenerateErrorMessage(e.Action, e.FileName);
			if (text == null)
			{
				text = "Change in GLOBAL.ASAX";
			}
			HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.ChangeInGlobalAsax, text);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00010194 File Offset: 0x0000E394
		private HttpApplication GetNormalApplicationInstance(HttpContext context)
		{
			HttpApplication httpApplication = null;
			if (!this._freeList.TryTake(out httpApplication))
			{
				httpApplication = (HttpApplication)HttpRuntime.CreateNonPublicInstance(this._theApplicationType);
				using (new ApplicationImpersonationContext())
				{
					httpApplication.InitInternal(context, this._state, this._eventHandlerMethods);
				}
			}
			if (AppSettings.UseTaskFriendlySynchronizationContext)
			{
				httpApplication.ApplicationInstanceConsumersCounter = new CountdownTask(1);
				httpApplication.ApplicationInstanceConsumersCounter.Task.ContinueWith(delegate(Task _, object o)
				{
					HttpApplicationFactory.RecycleApplicationInstance((HttpApplication)o);
				}, httpApplication, TaskContinuationOptions.ExecuteSynchronously);
			}
			return httpApplication;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00010244 File Offset: 0x0000E444
		private void RecycleNormalApplicationInstance(HttpApplication app)
		{
			this._freeList.Add(app);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00010254 File Offset: 0x0000E454
		private void TrimApplicationInstanceFreeList(bool trimAll = false)
		{
			int count = this._freeList.Count;
			if (count <= 1)
			{
				return;
			}
			int numOfInstancesToDispose = count * 3 / 100 + 1;
			HttpApplicationFactory.DisposeHttpApplicationInstances(this._freeList, numOfInstancesToDispose);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00010287 File Offset: 0x0000E487
		internal static HttpApplication GetPipelineApplicationInstance(IntPtr appContext, HttpContext context)
		{
			HttpApplicationFactory._theApplicationFactory.EnsureInited();
			return HttpApplicationFactory._theApplicationFactory.GetSpecialApplicationInstance(appContext, context);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001029F File Offset: 0x0000E49F
		internal static void RecyclePipelineApplicationInstance(HttpApplication app)
		{
			HttpApplicationFactory._theApplicationFactory.RecycleSpecialApplicationInstance(app);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000102AC File Offset: 0x0000E4AC
		private HttpApplication GetSpecialApplicationInstance(IntPtr appContext, HttpContext context)
		{
			HttpApplication httpApplication = null;
			if (!this._specialFreeList.TryTake(out httpApplication))
			{
				using (new DisposableHttpContextWrapper(context))
				{
					httpApplication = (HttpApplication)HttpRuntime.CreateNonPublicInstance(this._theApplicationType);
					using (new ApplicationImpersonationContext())
					{
						httpApplication.InitSpecial(this._state, this._eventHandlerMethods, appContext, context);
					}
				}
			}
			return httpApplication;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00010330 File Offset: 0x0000E530
		private HttpApplication GetSpecialApplicationInstance()
		{
			return this.GetSpecialApplicationInstance(IntPtr.Zero, null);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001033E File Offset: 0x0000E53E
		private void RecycleSpecialApplicationInstance(HttpApplication app)
		{
			if (this._specialFreeList.Count < 20)
			{
				this._specialFreeList.Add(app);
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001035C File Offset: 0x0000E55C
		private void FireApplicationOnStart(HttpContext context)
		{
			if (this._onStartMethod != null)
			{
				HttpApplication specialApplicationInstance = this.GetSpecialApplicationInstance();
				specialApplicationInstance.ProcessSpecialRequest(context, this._onStartMethod, this._onStartParamCount, this, EventArgs.Empty, null);
				this.RecycleSpecialApplicationInstance(specialApplicationInstance);
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000103A0 File Offset: 0x0000E5A0
		private void FireApplicationOnEnd()
		{
			if (this._onEndMethod != null)
			{
				HttpApplication specialApplicationInstance = this.GetSpecialApplicationInstance();
				specialApplicationInstance.ProcessSpecialRequest(null, this._onEndMethod, this._onEndParamCount, this, EventArgs.Empty, null);
				this.RecycleSpecialApplicationInstance(specialApplicationInstance);
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000103E4 File Offset: 0x0000E5E4
		private void SessionOnEndEventHandlerAspCompatHelper(object eventSource, EventArgs eventArgs)
		{
			HttpApplicationFactory.AspCompatSessionOnEndHelper aspCompatSessionOnEndHelper = (HttpApplicationFactory.AspCompatSessionOnEndHelper)eventSource;
			aspCompatSessionOnEndHelper.Application.ProcessSpecialRequest(null, this._sessionOnEndMethod, this._sessionOnEndParamCount, aspCompatSessionOnEndHelper.Source, aspCompatSessionOnEndHelper.Args, aspCompatSessionOnEndHelper.Session);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00010424 File Offset: 0x0000E624
		private void FireSessionOnEnd(HttpSessionState session, object eventSource, EventArgs eventArgs)
		{
			if (this._sessionOnEndMethod != null)
			{
				HttpApplication specialApplicationInstance = this.GetSpecialApplicationInstance();
				if (AspCompatApplicationStep.AnyStaObjectsInSessionState(session) || HttpRuntime.ApartmentThreading)
				{
					HttpApplicationFactory.AspCompatSessionOnEndHelper source = new HttpApplicationFactory.AspCompatSessionOnEndHelper(specialApplicationInstance, session, eventSource, eventArgs);
					AspCompatApplicationStep.RaiseAspCompatEvent(null, specialApplicationInstance, session.SessionID, this._sessionOnEndEventHandlerAspCompatHelper, source, EventArgs.Empty);
				}
				else
				{
					specialApplicationInstance.ProcessSpecialRequest(null, this._sessionOnEndMethod, this._sessionOnEndParamCount, eventSource, eventArgs, session);
				}
				this.RecycleSpecialApplicationInstance(specialApplicationInstance);
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00010498 File Offset: 0x0000E698
		private void FireApplicationOnError(Exception error)
		{
			HttpApplication specialApplicationInstance = this.GetSpecialApplicationInstance();
			specialApplicationInstance.RaiseErrorWithoutContext(error);
			this.RecycleSpecialApplicationInstance(specialApplicationInstance);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000104BC File Offset: 0x0000E6BC
		private void Dispose()
		{
			HttpApplicationFactory.DisposeHttpApplicationInstances(this._freeList, this._freeList.Count);
			if (this._appOnStartCalled && !this._appOnEndCalled)
			{
				lock (this)
				{
					if (!this._appOnEndCalled)
					{
						this.FireApplicationOnEnd();
						this._appOnEndCalled = true;
					}
				}
			}
			if (!AppSettings.DoNotDisposeSpecialHttpApplicationInstances)
			{
				HttpApplicationFactory.DisposeHttpApplicationInstances(this._specialFreeList, this._specialFreeList.Count);
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001054C File Offset: 0x0000E74C
		private static void DisposeHttpApplicationInstances(ConcurrentBag<HttpApplication> freeList, int numOfInstancesToDispose)
		{
			if (numOfInstancesToDispose <= 0)
			{
				return;
			}
			List<HttpApplication> list = new List<HttpApplication>();
			HttpApplication item = null;
			int num = 0;
			while (num < numOfInstancesToDispose && freeList.TryTake(out item))
			{
				list.Add(item);
				num++;
			}
			foreach (HttpApplication httpApplication in list)
			{
				httpApplication.DisposeInternal();
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000105C8 File Offset: 0x0000E7C8
		internal static void SetCustomApplication(IHttpHandler customApplication)
		{
			if (HttpRuntime.AppDomainAppId == null)
			{
				HttpApplicationFactory._customApplication = customApplication;
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x000105D8 File Offset: 0x0000E7D8
		internal static IHttpHandler GetApplicationInstance(HttpContext context)
		{
			if (HttpApplicationFactory._customApplication != null)
			{
				return HttpApplicationFactory._customApplication;
			}
			if (context.Request.IsDebuggingRequest)
			{
				return new HttpDebugHandler();
			}
			HttpApplicationFactory._theApplicationFactory.EnsureInited();
			HttpApplicationFactory._theApplicationFactory.EnsureAppStartCalled(context);
			return HttpApplicationFactory._theApplicationFactory.GetNormalApplicationInstance(context);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00010625 File Offset: 0x0000E825
		internal static void RecycleApplicationInstance(HttpApplication app)
		{
			HttpApplicationFactory._theApplicationFactory.RecycleNormalApplicationInstance(app);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00010632 File Offset: 0x0000E832
		internal static void TrimApplicationInstances(bool removeAll = false)
		{
			if (HttpApplicationFactory._theApplicationFactory != null)
			{
				if (removeAll)
				{
					HttpApplicationFactory.DisposeHttpApplicationInstances(HttpApplicationFactory._theApplicationFactory._freeList, HttpApplicationFactory._theApplicationFactory._freeList.Count);
					return;
				}
				HttpApplicationFactory._theApplicationFactory.TrimApplicationInstanceFreeList(false);
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00010668 File Offset: 0x0000E868
		internal static void EndApplication()
		{
			HttpApplicationFactory._theApplicationFactory.Dispose();
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00010674 File Offset: 0x0000E874
		internal static void EndSession(HttpSessionState session, object eventSource, EventArgs eventArgs)
		{
			HttpApplicationFactory._theApplicationFactory.FireSessionOnEnd(session, eventSource, eventArgs);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00010683 File Offset: 0x0000E883
		internal static void RaiseError(Exception error)
		{
			HttpApplicationFactory._theApplicationFactory.EnsureInited();
			HttpApplicationFactory._theApplicationFactory.FireApplicationOnError(error);
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001069C File Offset: 0x0000E89C
		internal static HttpApplicationState ApplicationState
		{
			get
			{
				HttpApplicationState httpApplicationState = HttpApplicationFactory._theApplicationFactory._state;
				if (httpApplicationState == null)
				{
					httpApplicationState = new HttpApplicationState();
				}
				return httpApplicationState;
			}
		}

		// Token: 0x04000276 RID: 630
		internal const string applicationFileName = "global.asax";

		// Token: 0x04000277 RID: 631
		private static HttpApplicationFactory _theApplicationFactory = new HttpApplicationFactory();

		// Token: 0x04000278 RID: 632
		private bool _inited;

		// Token: 0x04000279 RID: 633
		private string _appFilename;

		// Token: 0x0400027A RID: 634
		private ICollection _fileDependencies;

		// Token: 0x0400027B RID: 635
		private bool _appOnStartCalled;

		// Token: 0x0400027C RID: 636
		private bool _appOnEndCalled;

		// Token: 0x0400027D RID: 637
		private HttpApplicationState _state;

		// Token: 0x0400027E RID: 638
		private Type _theApplicationType;

		// Token: 0x0400027F RID: 639
		private ConcurrentBag<HttpApplication> _freeList = new ConcurrentBag<HttpApplication>();

		// Token: 0x04000280 RID: 640
		private ConcurrentBag<HttpApplication> _specialFreeList = new ConcurrentBag<HttpApplication>();

		// Token: 0x04000281 RID: 641
		private const int _maxFreeSpecialAppInstances = 20;

		// Token: 0x04000282 RID: 642
		private MethodInfo _onStartMethod;

		// Token: 0x04000283 RID: 643
		private int _onStartParamCount;

		// Token: 0x04000284 RID: 644
		private MethodInfo _onEndMethod;

		// Token: 0x04000285 RID: 645
		private int _onEndParamCount;

		// Token: 0x04000286 RID: 646
		private MethodInfo _sessionOnEndMethod;

		// Token: 0x04000287 RID: 647
		private int _sessionOnEndParamCount;

		// Token: 0x04000288 RID: 648
		private EventHandler _sessionOnEndEventHandlerAspCompatHelper;

		// Token: 0x04000289 RID: 649
		private MethodInfo[] _eventHandlerMethods;

		// Token: 0x0400028A RID: 650
		private static IHttpHandler _customApplication;

		// Token: 0x020008DE RID: 2270
		private class AspCompatSessionOnEndHelper
		{
			// Token: 0x0600683A RID: 26682 RVA: 0x00172446 File Offset: 0x00170646
			internal AspCompatSessionOnEndHelper(HttpApplication app, HttpSessionState session, object eventSource, EventArgs eventArgs)
			{
				this._app = app;
				this._session = session;
				this._eventSource = eventSource;
				this._eventArgs = eventArgs;
			}

			// Token: 0x17001CFA RID: 7418
			// (get) Token: 0x0600683B RID: 26683 RVA: 0x0017246B File Offset: 0x0017066B
			internal HttpApplication Application
			{
				get
				{
					return this._app;
				}
			}

			// Token: 0x17001CFB RID: 7419
			// (get) Token: 0x0600683C RID: 26684 RVA: 0x00172473 File Offset: 0x00170673
			internal HttpSessionState Session
			{
				get
				{
					return this._session;
				}
			}

			// Token: 0x17001CFC RID: 7420
			// (get) Token: 0x0600683D RID: 26685 RVA: 0x0017247B File Offset: 0x0017067B
			internal object Source
			{
				get
				{
					return this._eventSource;
				}
			}

			// Token: 0x17001CFD RID: 7421
			// (get) Token: 0x0600683E RID: 26686 RVA: 0x00172483 File Offset: 0x00170683
			internal EventArgs Args
			{
				get
				{
					return this._eventArgs;
				}
			}

			// Token: 0x04003637 RID: 13879
			private HttpApplication _app;

			// Token: 0x04003638 RID: 13880
			private HttpSessionState _session;

			// Token: 0x04003639 RID: 13881
			private object _eventSource;

			// Token: 0x0400363A RID: 13882
			private EventArgs _eventArgs;
		}
	}
}
