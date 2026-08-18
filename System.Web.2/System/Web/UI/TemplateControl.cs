using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI
{
	// Token: 0x0200030F RID: 783
	public abstract class TemplateControl : Control, INamingContainer, IFilterResolutionService
	{
		// Token: 0x06002413 RID: 9235 RVA: 0x00075C3C File Offset: 0x00073E3C
		static TemplateControl()
		{
			TemplateControl._eventObjects = new Hashtable(16);
			TemplateControl._eventObjects.Add("Page_PreInit", Page.EventPreInit);
			TemplateControl._eventObjects.Add("Page_Init", Control.EventInit);
			TemplateControl._eventObjects.Add("Page_InitComplete", Page.EventInitComplete);
			TemplateControl._eventObjects.Add("Page_Load", Control.EventLoad);
			TemplateControl._eventObjects.Add("Page_PreLoad", Page.EventPreLoad);
			TemplateControl._eventObjects.Add("Page_LoadComplete", Page.EventLoadComplete);
			TemplateControl._eventObjects.Add("Page_PreRenderComplete", Page.EventPreRenderComplete);
			TemplateControl._eventObjects.Add("Page_DataBind", Control.EventDataBinding);
			TemplateControl._eventObjects.Add("Page_PreRender", Control.EventPreRender);
			TemplateControl._eventObjects.Add("Page_SaveStateComplete", Page.EventSaveStateComplete);
			TemplateControl._eventObjects.Add("Page_Unload", Control.EventUnload);
			TemplateControl._eventObjects.Add("Page_Error", TemplateControl.EventError);
			TemplateControl._eventObjects.Add("Page_AbortTransaction", TemplateControl.EventAbortTransaction);
			TemplateControl._eventObjects.Add("OnTransactionAbort", TemplateControl.EventAbortTransaction);
			TemplateControl._eventObjects.Add("Page_CommitTransaction", TemplateControl.EventCommitTransaction);
			TemplateControl._eventObjects.Add("OnTransactionCommit", TemplateControl.EventCommitTransaction);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x00075DD1 File Offset: 0x00073FD1
		protected TemplateControl()
		{
			this.Construct();
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Construct()
		{
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002416 RID: 9238 RVA: 0x00075DDF File Offset: 0x00073FDF
		// (remove) Token: 0x06002417 RID: 9239 RVA: 0x00075DF2 File Offset: 0x00073FF2
		[WebSysDescription("Page_OnCommitTransaction")]
		public event EventHandler CommitTransaction
		{
			add
			{
				base.Events.AddHandler(TemplateControl.EventCommitTransaction, value);
			}
			remove
			{
				base.Events.RemoveHandler(TemplateControl.EventCommitTransaction, value);
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00075E05 File Offset: 0x00074005
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x00075E0D File Offset: 0x0007400D
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x00075E18 File Offset: 0x00074018
		protected virtual void OnCommitTransaction(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TemplateControl.EventCommitTransaction];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x0600241B RID: 9243 RVA: 0x00075E46 File Offset: 0x00074046
		// (remove) Token: 0x0600241C RID: 9244 RVA: 0x00075E59 File Offset: 0x00074059
		[WebSysDescription("Page_OnAbortTransaction")]
		public event EventHandler AbortTransaction
		{
			add
			{
				base.Events.AddHandler(TemplateControl.EventAbortTransaction, value);
			}
			remove
			{
				base.Events.RemoveHandler(TemplateControl.EventAbortTransaction, value);
			}
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x00075E6C File Offset: 0x0007406C
		protected virtual void OnAbortTransaction(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TemplateControl.EventAbortTransaction];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600241E RID: 9246 RVA: 0x00075E9A File Offset: 0x0007409A
		// (remove) Token: 0x0600241F RID: 9247 RVA: 0x00075EAD File Offset: 0x000740AD
		[WebSysDescription("Page_Error")]
		public event EventHandler Error
		{
			add
			{
				base.Events.AddHandler(TemplateControl.EventError, value);
			}
			remove
			{
				base.Events.RemoveHandler(TemplateControl.EventError, value);
			}
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x00075EC0 File Offset: 0x000740C0
		protected virtual void OnError(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TemplateControl.EventError];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x00075EEE File Offset: 0x000740EE
		internal void SetNoCompileBuildResult(BuildResultNoCompileTemplateControl noCompileBuildResult)
		{
			this._noCompileBuildResult = noCompileBuildResult;
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x00075EF7 File Offset: 0x000740F7
		internal bool NoCompile
		{
			get
			{
				return this._noCompileBuildResult != null;
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00075F02 File Offset: 0x00074102
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void FrameworkInitialize()
		{
			if (this.NoCompile)
			{
				if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && !HttpRuntime.ProcessRequestInApplicationTrust)
				{
					HttpRuntime.NamedPermissionSet.PermitOnly();
				}
				this._noCompileBuildResult.FrameworkInitialize(this);
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x000097B7 File Offset: 0x000079B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual bool SupportAutoEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x00075F37 File Offset: 0x00074137
		internal IntPtr StringResourcePointer
		{
			get
			{
				return this._stringResourcePointer;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x00075F3F File Offset: 0x0007413F
		internal int MaxResourceOffset
		{
			get
			{
				return this._maxResourceOffset;
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00075F47 File Offset: 0x00074147
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static object ReadStringResource(Type t)
		{
			return StringResourceManager.ReadSafeStringResource(t);
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00075F4F File Offset: 0x0007414F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object ReadStringResource()
		{
			return StringResourceManager.ReadSafeStringResource(base.GetType());
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00075F5C File Offset: 0x0007415C
		protected LiteralControl CreateResourceBasedLiteralControl(int offset, int size, bool fAsciiOnly)
		{
			return new ResourceBasedLiteralControl(this, offset, size, fAsciiOnly);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00075F68 File Offset: 0x00074168
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void SetStringResourcePointer(object stringResourcePointer, int maxResourceOffset)
		{
			SafeStringResource safeStringResource = (SafeStringResource)stringResourcePointer;
			this._stringResourcePointer = safeStringResource.StringResourcePointer;
			this._maxResourceOffset = safeStringResource.ResourceSize;
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x00075F94 File Offset: 0x00074194
		internal VirtualPath VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x00075F9C File Offset: 0x0007419C
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x00075FA9 File Offset: 0x000741A9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public string AppRelativeVirtualPath
		{
			get
			{
				return VirtualPath.GetAppRelativeVirtualPathString(this.TemplateControlVirtualPath);
			}
			set
			{
				this.TemplateControlVirtualPath = VirtualPath.CreateNonRelative(value);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x00075F94 File Offset: 0x00074194
		// (set) Token: 0x0600242F RID: 9263 RVA: 0x00075FB7 File Offset: 0x000741B7
		internal VirtualPath TemplateControlVirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				this._virtualPath = value;
				base.TemplateControlVirtualDirectory = this._virtualPath.Parent;
			}
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00075FD1 File Offset: 0x000741D1
		public virtual bool TestDeviceFilter(string filterName)
		{
			return this.Context.Request.Browser.IsBrowser(filterName);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x00075FE9 File Offset: 0x000741E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void WriteUTF8ResourceString(HtmlTextWriter output, int offset, int size, bool fAsciiOnly)
		{
			if (offset < 0 || size < 0 || checked(offset + size) > this._maxResourceOffset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			output.WriteUTF8ResourceString(this.StringResourcePointer, offset, size, fAsciiOnly);
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x00006164 File Offset: 0x00004364
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use of this property is not recommended because it is no longer useful. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual int AutoHandlers
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00004335 File Offset: 0x00002535
		internal override TemplateControl GetTemplateControl()
		{
			return this;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0007601C File Offset: 0x0007421C
		internal void HookUpAutomaticHandlers()
		{
			if (!this.SupportAutoEvents)
			{
				return;
			}
			object obj = TemplateControl._eventListCache[base.GetType()];
			TemplateControl.EventList eventList;
			if (obj == null)
			{
				object lockObject = TemplateControl._lockObject;
				lock (lockObject)
				{
					obj = (TemplateControl.EventList)TemplateControl._eventListCache[base.GetType()];
					if (obj == null)
					{
						eventList = new TemplateControl.EventList();
						this.GetDelegateInformation(eventList);
						if (eventList.IsEmpty)
						{
							obj = TemplateControl._emptyEventSingleton;
						}
						else
						{
							obj = eventList;
						}
						TemplateControl._eventListCache[base.GetType()] = obj;
					}
				}
			}
			if (obj == TemplateControl._emptyEventSingleton)
			{
				return;
			}
			eventList = (TemplateControl.EventList)obj;
			IDictionary<string, TemplateControl.SyncEventMethodInfo> syncEvents = eventList.SyncEvents;
			foreach (KeyValuePair<string, TemplateControl.SyncEventMethodInfo> keyValuePair in syncEvents)
			{
				string key = keyValuePair.Key;
				TemplateControl.SyncEventMethodInfo value = keyValuePair.Value;
				bool flag2 = false;
				MethodInfo methodInfo = value.MethodInfo;
				Delegate @delegate = base.Events[TemplateControl._eventObjects[key]];
				if (@delegate != null)
				{
					foreach (Delegate delegate2 in @delegate.GetInvocationList())
					{
						if (delegate2.Method.Equals(methodInfo))
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					IntPtr functionPointer = methodInfo.MethodHandle.GetFunctionPointer();
					EventHandler handler = new CalliEventHandlerDelegateProxy(this, functionPointer, value.IsArgless).Handler;
					base.Events.AddHandler(TemplateControl._eventObjects[key], handler);
				}
			}
			IDictionary<string, TemplateControl.AsyncEventMethodInfo> asyncEvents = eventList.AsyncEvents;
			TemplateControl.AsyncEventMethodInfo asyncEventMethodInfo;
			if (asyncEvents.TryGetValue("Page_PreRenderCompleteAsync", out asyncEventMethodInfo))
			{
				Page page = (Page)this;
				if (asyncEventMethodInfo.RequiresCancellationToken)
				{
					Func<CancellationToken, Task> handler2 = FastDelegateCreator<Func<CancellationToken, Task>>.BindTo(this, asyncEventMethodInfo.MethodInfo);
					page.RegisterAsyncTask(new PageAsyncTask(handler2));
					return;
				}
				Func<Task> handler3 = FastDelegateCreator<Func<Task>>.BindTo(this, asyncEventMethodInfo.MethodInfo);
				page.RegisterAsyncTask(new PageAsyncTask(handler3));
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00076230 File Offset: 0x00074430
		private void GetDelegateInformation(TemplateControl.EventList eventList)
		{
			if (HttpRuntime.IsFullTrust)
			{
				this.GetDelegateInformationWithNoAssert(eventList);
				return;
			}
			this.GetDelegateInformationWithAssert(eventList);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00076248 File Offset: 0x00074448
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		private void GetDelegateInformationWithAssert(TemplateControl.EventList eventList)
		{
			this.GetDelegateInformationWithNoAssert(eventList);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00076254 File Offset: 0x00074454
		private void GetDelegateInformationWithNoAssert(TemplateControl.EventList eventList)
		{
			IDictionary<string, TemplateControl.SyncEventMethodInfo> syncEvents = eventList.SyncEvents;
			IDictionary<string, TemplateControl.AsyncEventMethodInfo> asyncEvents = eventList.AsyncEvents;
			if (this is Page)
			{
				this.GetDelegateInformationFromSyncMethod("Page_PreInit", syncEvents);
				this.GetDelegateInformationFromSyncMethod("Page_PreLoad", syncEvents);
				this.GetDelegateInformationFromSyncMethod("Page_LoadComplete", syncEvents);
				this.GetDelegateInformationFromSyncMethod("Page_PreRenderComplete", syncEvents);
				this.GetDelegateInformationFromSyncMethod("Page_InitComplete", syncEvents);
				this.GetDelegateInformationFromSyncMethod("Page_SaveStateComplete", syncEvents);
				this.GetDelegateInformationFromAsyncMethod("Page_PreRenderCompleteAsync", asyncEvents);
			}
			this.GetDelegateInformationFromSyncMethod("Page_Init", syncEvents);
			this.GetDelegateInformationFromSyncMethod("Page_Load", syncEvents);
			this.GetDelegateInformationFromSyncMethod("Page_DataBind", syncEvents);
			this.GetDelegateInformationFromSyncMethod("Page_PreRender", syncEvents);
			this.GetDelegateInformationFromSyncMethod("Page_Unload", syncEvents);
			this.GetDelegateInformationFromSyncMethod("Page_Error", syncEvents);
			if (!this.GetDelegateInformationFromSyncMethod("Page_AbortTransaction", syncEvents))
			{
				this.GetDelegateInformationFromSyncMethod("OnTransactionAbort", syncEvents);
			}
			if (!this.GetDelegateInformationFromSyncMethod("Page_CommitTransaction", syncEvents))
			{
				this.GetDelegateInformationFromSyncMethod("OnTransactionCommit", syncEvents);
			}
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00076358 File Offset: 0x00074558
		private bool GetDelegateInformationFromAsyncMethod(string methodName, IDictionary<string, TemplateControl.AsyncEventMethodInfo> dictionary)
		{
			MethodInfo instanceMethodInfo = this.GetInstanceMethodInfo(typeof(Func<CancellationToken, Task>), methodName);
			if (instanceMethodInfo != null)
			{
				dictionary[methodName] = new TemplateControl.AsyncEventMethodInfo(instanceMethodInfo, true);
				return true;
			}
			MethodInfo instanceMethodInfo2 = this.GetInstanceMethodInfo(typeof(Func<Task>), methodName);
			if (instanceMethodInfo2 != null)
			{
				dictionary[methodName] = new TemplateControl.AsyncEventMethodInfo(instanceMethodInfo2, false);
				return true;
			}
			return false;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000763BC File Offset: 0x000745BC
		private bool GetDelegateInformationFromSyncMethod(string methodName, IDictionary<string, TemplateControl.SyncEventMethodInfo> dictionary)
		{
			MethodInfo instanceMethodInfo = this.GetInstanceMethodInfo(typeof(EventHandler), methodName);
			if (instanceMethodInfo != null)
			{
				dictionary[methodName] = new TemplateControl.SyncEventMethodInfo(instanceMethodInfo, false);
				return true;
			}
			MethodInfo instanceMethodInfo2 = this.GetInstanceMethodInfo(typeof(VoidMethod), methodName);
			if (instanceMethodInfo2 != null)
			{
				dictionary[methodName] = new TemplateControl.SyncEventMethodInfo(instanceMethodInfo2, true);
				return true;
			}
			return false;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00076420 File Offset: 0x00074620
		private MethodInfo GetInstanceMethodInfo(Type delegateType, string methodName)
		{
			Delegate @delegate = Delegate.CreateDelegate(delegateType, this, methodName, true, false);
			if (@delegate == null)
			{
				return null;
			}
			return @delegate.Method;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00076443 File Offset: 0x00074643
		public Control LoadControl(string virtualPath)
		{
			return this.LoadControl(VirtualPath.Create(virtualPath));
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00076454 File Offset: 0x00074654
		internal Control LoadControl(VirtualPath virtualPath)
		{
			virtualPath = VirtualPath.Combine(base.TemplateControlVirtualDirectory, virtualPath);
			BuildResult vpathBuildResult = BuildManager.GetVPathBuildResult(this.Context, virtualPath);
			return this.LoadControl((IWebObjectFactory)vpathBuildResult, virtualPath, null, null);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x0007648C File Offset: 0x0007468C
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		private void AddStackContextToHashCode(HashCodeCombiner combinedHashCode)
		{
			StackTrace stackTrace = new StackTrace();
			int num = 2;
			for (;;)
			{
				StackFrame frame = stackTrace.GetFrame(num);
				if (frame.GetMethod().DeclaringType != typeof(TemplateControl))
				{
					break;
				}
				num++;
			}
			for (int i = num; i < num + 2; i++)
			{
				StackFrame frame2 = stackTrace.GetFrame(i);
				MethodBase method = frame2.GetMethod();
				combinedHashCode.AddObject(method.DeclaringType.AssemblyQualifiedName);
				combinedHashCode.AddObject(method.Name);
				combinedHashCode.AddObject(frame2.GetNativeOffset());
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00076517 File Offset: 0x00074717
		public Control LoadControl(Type t, object[] parameters)
		{
			return this.LoadControl(null, null, t, parameters);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00076524 File Offset: 0x00074724
		private Control LoadControl(IWebObjectFactory objectFactory, VirtualPath virtualPath, Type t, object[] parameters)
		{
			BuildResultNoCompileUserControl buildResultNoCompileUserControl = null;
			if (objectFactory != null)
			{
				BuildResultCompiledType buildResultCompiledType = objectFactory as BuildResultCompiledType;
				if (buildResultCompiledType != null)
				{
					t = buildResultCompiledType.ResultType;
					Util.CheckAssignableType(typeof(UserControl), t);
				}
				else
				{
					buildResultNoCompileUserControl = (BuildResultNoCompileUserControl)objectFactory;
				}
			}
			else if (t != null)
			{
				Util.CheckAssignableType(typeof(Control), t);
			}
			PartialCachingAttribute partialCachingAttribute;
			if (t != null)
			{
				partialCachingAttribute = (PartialCachingAttribute)TypeDescriptor.GetAttributes(t)[typeof(PartialCachingAttribute)];
			}
			else
			{
				partialCachingAttribute = buildResultNoCompileUserControl.CachingAttribute;
			}
			if (partialCachingAttribute == null)
			{
				Control control;
				if (objectFactory != null)
				{
					control = (Control)objectFactory.CreateInstance();
				}
				else
				{
					control = (Control)HttpRuntime.CreatePublicInstance(t, parameters);
				}
				UserControl userControl = control as UserControl;
				if (userControl != null)
				{
					if (virtualPath != null)
					{
						userControl.TemplateControlVirtualPath = virtualPath;
					}
					userControl.InitializeAsUserControl(this.Page);
				}
				return control;
			}
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			if (objectFactory != null)
			{
				hashCodeCombiner.AddObject(objectFactory);
			}
			else
			{
				hashCodeCombiner.AddObject(t);
			}
			if (!partialCachingAttribute.Shared)
			{
				this.AddStackContextToHashCode(hashCodeCombiner);
			}
			string combinedHashString = hashCodeCombiner.CombinedHashString;
			return new PartialCachingControl(objectFactory, t, partialCachingAttribute, "_" + combinedHashString, parameters);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00076643 File Offset: 0x00074843
		public ITemplate LoadTemplate(string virtualPath)
		{
			return this.LoadTemplate(VirtualPath.Create(virtualPath));
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00076654 File Offset: 0x00074854
		internal ITemplate LoadTemplate(VirtualPath virtualPath)
		{
			virtualPath = VirtualPath.Combine(base.TemplateControlVirtualDirectory, virtualPath);
			ITypedWebObjectFactory objectFactory = (ITypedWebObjectFactory)BuildManager.GetVPathBuildResult(this.Context, virtualPath);
			return new TemplateControl.SimpleTemplate(objectFactory);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00076687 File Offset: 0x00074887
		public Control ParseControl(string content)
		{
			return this.ParseControl(content, true);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00076691 File Offset: 0x00074891
		public Control ParseControl(string content, bool ignoreParserFilter)
		{
			return TemplateParser.ParseControl(content, VirtualPath.Create(this.AppRelativeVirtualPath), ignoreParserFilter);
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000766A5 File Offset: 0x000748A5
		private void CheckPageExists()
		{
			if (this.Page == null)
			{
				throw new InvalidOperationException(SR.GetString("TemplateControl_DataBindingRequiresPage"));
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000766BF File Offset: 0x000748BF
		protected internal object Eval(string expression)
		{
			this.CheckPageExists();
			return DataBinder.Eval(this.Page.GetDataItem(), expression);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000766D8 File Offset: 0x000748D8
		protected internal string Eval(string expression, string format)
		{
			this.CheckPageExists();
			return DataBinder.Eval(this.Page.GetDataItem(), expression, format);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000766F2 File Offset: 0x000748F2
		protected internal object XPath(string xPathExpression)
		{
			this.CheckPageExists();
			return XPathBinder.Eval(this.Page.GetDataItem(), xPathExpression);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x0007670B File Offset: 0x0007490B
		protected internal object XPath(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			this.CheckPageExists();
			return XPathBinder.Eval(this.Page.GetDataItem(), xPathExpression, resolver);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00076725 File Offset: 0x00074925
		protected internal string XPath(string xPathExpression, string format)
		{
			this.CheckPageExists();
			return XPathBinder.Eval(this.Page.GetDataItem(), xPathExpression, format);
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x0007673F File Offset: 0x0007493F
		protected internal string XPath(string xPathExpression, string format, IXmlNamespaceResolver resolver)
		{
			this.CheckPageExists();
			return XPathBinder.Eval(this.Page.GetDataItem(), xPathExpression, format, resolver);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0007675A File Offset: 0x0007495A
		protected internal IEnumerable XPathSelect(string xPathExpression)
		{
			this.CheckPageExists();
			return XPathBinder.Select(this.Page.GetDataItem(), xPathExpression);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00076773 File Offset: 0x00074973
		protected internal IEnumerable XPathSelect(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			this.CheckPageExists();
			return XPathBinder.Select(this.Page.GetDataItem(), xPathExpression, resolver);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0007678D File Offset: 0x0007498D
		protected object GetLocalResourceObject(string resourceKey)
		{
			if (this._resourceProvider == null)
			{
				this._resourceProvider = ResourceExpressionBuilder.GetLocalResourceProvider(this);
			}
			return ResourceExpressionBuilder.GetResourceObject(this._resourceProvider, resourceKey, null);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000767B0 File Offset: 0x000749B0
		protected object GetLocalResourceObject(string resourceKey, Type objType, string propName)
		{
			if (this._resourceProvider == null)
			{
				this._resourceProvider = ResourceExpressionBuilder.GetLocalResourceProvider(this);
			}
			return ResourceExpressionBuilder.GetResourceObject(this._resourceProvider, resourceKey, null, objType, propName);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000767D5 File Offset: 0x000749D5
		protected object GetGlobalResourceObject(string className, string resourceKey)
		{
			return ResourceExpressionBuilder.GetGlobalResourceObject(className, resourceKey, null, null, null);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000767E1 File Offset: 0x000749E1
		protected object GetGlobalResourceObject(string className, string resourceKey, Type objType, string propName)
		{
			return ResourceExpressionBuilder.GetGlobalResourceObject(className, resourceKey, objType, propName, null);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000767EE File Offset: 0x000749EE
		bool IFilterResolutionService.EvaluateFilter(string filterName)
		{
			return this.TestDeviceFilter(filterName);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000767F7 File Offset: 0x000749F7
		int IFilterResolutionService.CompareFilters(string filter1, string filter2)
		{
			return BrowserCapabilitiesCompiler.BrowserCapabilitiesFactory.CompareFilters(filter1, filter2);
		}

		// Token: 0x04001CE6 RID: 7398
		private IntPtr _stringResourcePointer;

		// Token: 0x04001CE7 RID: 7399
		private int _maxResourceOffset;

		// Token: 0x04001CE8 RID: 7400
		private static object _lockObject = new object();

		// Token: 0x04001CE9 RID: 7401
		private static Hashtable _eventListCache = new Hashtable();

		// Token: 0x04001CEA RID: 7402
		private static object _emptyEventSingleton = new TemplateControl.EventList();

		// Token: 0x04001CEB RID: 7403
		private VirtualPath _virtualPath;

		// Token: 0x04001CEC RID: 7404
		private IResourceProvider _resourceProvider;

		// Token: 0x04001CED RID: 7405
		private const string _pagePreInitEventName = "Page_PreInit";

		// Token: 0x04001CEE RID: 7406
		private const string _pageInitEventName = "Page_Init";

		// Token: 0x04001CEF RID: 7407
		private const string _pageInitCompleteEventName = "Page_InitComplete";

		// Token: 0x04001CF0 RID: 7408
		private const string _pageLoadEventName = "Page_Load";

		// Token: 0x04001CF1 RID: 7409
		private const string _pagePreLoadEventName = "Page_PreLoad";

		// Token: 0x04001CF2 RID: 7410
		private const string _pageLoadCompleteEventName = "Page_LoadComplete";

		// Token: 0x04001CF3 RID: 7411
		private const string _pagePreRenderCompleteEventName = "Page_PreRenderComplete";

		// Token: 0x04001CF4 RID: 7412
		private const string _pagePreRenderCompleteAsyncEventName = "Page_PreRenderCompleteAsync";

		// Token: 0x04001CF5 RID: 7413
		private const string _pageDataBindEventName = "Page_DataBind";

		// Token: 0x04001CF6 RID: 7414
		private const string _pagePreRenderEventName = "Page_PreRender";

		// Token: 0x04001CF7 RID: 7415
		private const string _pageSaveStateCompleteEventName = "Page_SaveStateComplete";

		// Token: 0x04001CF8 RID: 7416
		private const string _pageUnloadEventName = "Page_Unload";

		// Token: 0x04001CF9 RID: 7417
		private const string _pageErrorEventName = "Page_Error";

		// Token: 0x04001CFA RID: 7418
		private const string _pageAbortTransactionEventName = "Page_AbortTransaction";

		// Token: 0x04001CFB RID: 7419
		private const string _onTransactionAbortEventName = "OnTransactionAbort";

		// Token: 0x04001CFC RID: 7420
		private const string _pageCommitTransactionEventName = "Page_CommitTransaction";

		// Token: 0x04001CFD RID: 7421
		private const string _onTransactionCommitEventName = "OnTransactionCommit";

		// Token: 0x04001CFE RID: 7422
		private static IDictionary _eventObjects;

		// Token: 0x04001CFF RID: 7423
		private BuildResultNoCompileTemplateControl _noCompileBuildResult;

		// Token: 0x04001D00 RID: 7424
		private static readonly object EventCommitTransaction = new object();

		// Token: 0x04001D01 RID: 7425
		private static readonly object EventAbortTransaction = new object();

		// Token: 0x04001D02 RID: 7426
		private static readonly object EventError = new object();

		// Token: 0x0200098A RID: 2442
		internal class SimpleTemplate : ITemplate
		{
			// Token: 0x06006A5E RID: 27230 RVA: 0x0017BCB5 File Offset: 0x00179EB5
			internal SimpleTemplate(ITypedWebObjectFactory objectFactory)
			{
				Util.CheckAssignableType(typeof(UserControl), objectFactory.InstantiatedType);
				this._objectFactory = objectFactory;
			}

			// Token: 0x06006A5F RID: 27231 RVA: 0x0017BCDC File Offset: 0x00179EDC
			public virtual void InstantiateIn(Control control)
			{
				UserControl userControl = (UserControl)this._objectFactory.CreateInstance();
				userControl.InitializeAsUserControl(control.Page);
				control.Controls.Add(userControl);
			}

			// Token: 0x040038C8 RID: 14536
			private IWebObjectFactory _objectFactory;
		}

		// Token: 0x0200098B RID: 2443
		private class EventList
		{
			// Token: 0x17001D41 RID: 7489
			// (get) Token: 0x06006A60 RID: 27232 RVA: 0x0017BD12 File Offset: 0x00179F12
			internal bool IsEmpty
			{
				get
				{
					return this.AsyncEvents.Count == 0 && this.SyncEvents.Count == 0;
				}
			}

			// Token: 0x040038C9 RID: 14537
			internal readonly IDictionary<string, TemplateControl.AsyncEventMethodInfo> AsyncEvents = new Dictionary<string, TemplateControl.AsyncEventMethodInfo>(StringComparer.Ordinal);

			// Token: 0x040038CA RID: 14538
			internal readonly IDictionary<string, TemplateControl.SyncEventMethodInfo> SyncEvents = new Dictionary<string, TemplateControl.SyncEventMethodInfo>(StringComparer.Ordinal);
		}

		// Token: 0x0200098C RID: 2444
		private class SyncEventMethodInfo
		{
			// Token: 0x06006A62 RID: 27234 RVA: 0x0017BD59 File Offset: 0x00179F59
			internal SyncEventMethodInfo(MethodInfo methodInfo, bool isArgless)
			{
				if (TemplateControl.SyncEventMethodInfo.IsAsyncVoidMethod(methodInfo))
				{
					SynchronizationContextUtil.ValidateModeForPageAsyncVoidMethods();
				}
				this.MethodInfo = methodInfo;
				this.IsArgless = isArgless;
			}

			// Token: 0x17001D42 RID: 7490
			// (get) Token: 0x06006A63 RID: 27235 RVA: 0x0017BD7C File Offset: 0x00179F7C
			// (set) Token: 0x06006A64 RID: 27236 RVA: 0x0017BD84 File Offset: 0x00179F84
			internal bool IsArgless { get; private set; }

			// Token: 0x17001D43 RID: 7491
			// (get) Token: 0x06006A65 RID: 27237 RVA: 0x0017BD8D File Offset: 0x00179F8D
			// (set) Token: 0x06006A66 RID: 27238 RVA: 0x0017BD95 File Offset: 0x00179F95
			internal MethodInfo MethodInfo { get; private set; }

			// Token: 0x06006A67 RID: 27239 RVA: 0x0017BD9E File Offset: 0x00179F9E
			private static bool IsAsyncVoidMethod(MethodInfo methodInfo)
			{
				return methodInfo.IsDefined(typeof(AsyncStateMachineAttribute), false);
			}
		}

		// Token: 0x0200098D RID: 2445
		private class AsyncEventMethodInfo
		{
			// Token: 0x06006A68 RID: 27240 RVA: 0x0017BDB1 File Offset: 0x00179FB1
			internal AsyncEventMethodInfo(MethodInfo methodInfo, bool requiresCancellationToken)
			{
				this.MethodInfo = methodInfo;
				this.RequiresCancellationToken = requiresCancellationToken;
			}

			// Token: 0x17001D44 RID: 7492
			// (get) Token: 0x06006A69 RID: 27241 RVA: 0x0017BDC7 File Offset: 0x00179FC7
			// (set) Token: 0x06006A6A RID: 27242 RVA: 0x0017BDCF File Offset: 0x00179FCF
			internal MethodInfo MethodInfo { get; private set; }

			// Token: 0x17001D45 RID: 7493
			// (get) Token: 0x06006A6B RID: 27243 RVA: 0x0017BDD8 File Offset: 0x00179FD8
			// (set) Token: 0x06006A6C RID: 27244 RVA: 0x0017BDE0 File Offset: 0x00179FE0
			internal bool RequiresCancellationToken { get; private set; }
		}
	}
}
