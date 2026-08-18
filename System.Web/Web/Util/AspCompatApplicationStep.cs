using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.SessionState;
using Microsoft.Win32;

namespace System.Web.Util
{
	// Token: 0x02000758 RID: 1880
	internal class AspCompatApplicationStep : HttpApplication.IExecutionStep, IManagedContext
	{
		// Token: 0x06005B49 RID: 23369 RVA: 0x0016F39C File Offset: 0x0016E39C
		internal AspCompatApplicationStep(HttpContext context, AspCompatCallback code)
		{
			this._code = code;
			this.Init(context, context.ApplicationInstance);
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x0016F3B8 File Offset: 0x0016E3B8
		private AspCompatApplicationStep(HttpContext context, HttpApplication app, string sessionId, EventHandler codeEventHandler, object codeEventSource, EventArgs codeEventArgs)
		{
			this._codeEventHandler = codeEventHandler;
			this._codeEventSource = codeEventSource;
			this._codeEventArgs = codeEventArgs;
			this._sessionId = sessionId;
			this.Init(context, app);
		}

		// Token: 0x06005B4B RID: 23371 RVA: 0x0016F3E8 File Offset: 0x0016E3E8
		private void Init(HttpContext context, HttpApplication app)
		{
			this._context = context;
			this._app = app;
			this._execCallback = new AspCompatCallback(this.OnAspCompatExecution);
			this._compCallback = new WorkItemCallback(this.OnAspCompatCompletion);
			if (this._sessionId == null && this._context != null && this._context.Session != null)
			{
				this._sessionId = this._context.Session.SessionID;
			}
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x0016F45A File Offset: 0x0016E45A
		private void MarkCallContext(AspCompatApplicationStep mark)
		{
			CallContext.SetData("AspCompat", mark);
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x0016F467 File Offset: 0x0016E467
		private static AspCompatApplicationStep Current
		{
			get
			{
				return (AspCompatApplicationStep)CallContext.GetData("AspCompat");
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x0016F478 File Offset: 0x0016E478
		internal static bool IsInAspCompatMode
		{
			get
			{
				return AspCompatApplicationStep.Current != null;
			}
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x0016F485 File Offset: 0x0016E485
		void HttpApplication.IExecutionStep.Execute()
		{
			if (this._code != null)
			{
				this._code();
				return;
			}
			if (this._codeEventHandler != null)
			{
				this._codeEventHandler(this._codeEventSource, this._codeEventArgs);
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06005B50 RID: 23376 RVA: 0x0016F4BA File Offset: 0x0016E4BA
		bool HttpApplication.IExecutionStep.CompletedSynchronously
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x0016F4BD File Offset: 0x0016E4BD
		bool HttpApplication.IExecutionStep.IsCancellable
		{
			get
			{
				return this._context != null;
			}
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x0016F4CB File Offset: 0x0016E4CB
		private void RememberStaComponent(object component)
		{
			if (this._staComponents == null)
			{
				this._staComponents = new ArrayList();
			}
			this._staComponents.Add(component);
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x0016F4F0 File Offset: 0x0016E4F0
		private bool IsStaComponentInSessionState(object component)
		{
			if (this._context == null)
			{
				return false;
			}
			HttpSessionState session = this._context.Session;
			if (session == null)
			{
				return false;
			}
			int count = session.Count;
			for (int i = 0; i < count; i++)
			{
				if (component == session[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x0016F538 File Offset: 0x0016E538
		internal static bool AnyStaObjectsInSessionState(HttpSessionState session)
		{
			if (session == null)
			{
				return false;
			}
			int count = session.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = session[i];
				if (obj != null && obj.GetType().FullName == "System.__ComObject" && UnsafeNativeMethods.AspCompatIsApartmentComponent(obj) != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x0016F58C File Offset: 0x0016E58C
		internal static void OnPageStart(object component)
		{
			if (!AspCompatApplicationStep.IsInAspCompatMode)
			{
				return;
			}
			int num = UnsafeNativeMethods.AspCompatOnPageStart(component);
			if (num != 1)
			{
				throw new HttpException(SR.GetString("Error_onpagestart"));
			}
			if (UnsafeNativeMethods.AspCompatIsApartmentComponent(component) != 0)
			{
				AspCompatApplicationStep.Current.RememberStaComponent(component);
			}
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x0016F5D0 File Offset: 0x0016E5D0
		internal static void OnPageStartSessionObjects()
		{
			if (!AspCompatApplicationStep.IsInAspCompatMode)
			{
				return;
			}
			HttpContext context = AspCompatApplicationStep.Current._context;
			if (context == null)
			{
				return;
			}
			HttpSessionState session = context.Session;
			if (session == null)
			{
				return;
			}
			int count = session.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = session[i];
				if (obj != null && !(obj is string))
				{
					int num = UnsafeNativeMethods.AspCompatOnPageStart(obj);
					if (num != 1)
					{
						throw new HttpException(SR.GetString("Error_onpagestart"));
					}
				}
			}
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x0016F648 File Offset: 0x0016E648
		internal static void CheckThreadingModel(string progidDisplayName, Guid clsid)
		{
			if (AspCompatApplicationStep.IsInAspCompatMode)
			{
				return;
			}
			CacheInternal cacheInternal = HttpRuntime.CacheInternal;
			string key = "s" + progidDisplayName;
			string text = (string)cacheInternal.Get(key);
			RegistryKey registryKey = null;
			if (text == null)
			{
				try
				{
					registryKey = Registry.ClassesRoot.OpenSubKey("CLSID\\{" + clsid + "}\\InprocServer32");
					if (registryKey != null)
					{
						text = (string)registryKey.GetValue("ThreadingModel");
					}
				}
				catch
				{
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
				if (text == null)
				{
					text = string.Empty;
				}
				cacheInternal.UtcInsert(key, text);
			}
			if (StringUtil.EqualsIgnoreCase(text, "Apartment"))
			{
				throw new HttpException(SR.GetString("Apartment_component_not_allowed", new object[]
				{
					progidDisplayName
				}));
			}
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x0016F720 File Offset: 0x0016E720
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		internal IAsyncResult BeginAspCompatExecution(AsyncCallback cb, object extraData)
		{
			if (AspCompatApplicationStep.IsInAspCompatMode)
			{
				bool flag = true;
				Exception error = this._app.ExecuteStep(this, ref flag);
				this._ar = new HttpAsyncResult(cb, extraData, true, null, error);
				this._syncCaller = true;
			}
			else
			{
				this._ar = new HttpAsyncResult(cb, extraData);
				this._syncCaller = (cb == null);
				this._rootedThis = GCHandle.Alloc(this);
				bool flag2 = this._sessionId != null;
				int activityHash = flag2 ? this._sessionId.GetHashCode() : 0;
				if (UnsafeNativeMethods.AspCompatProcessRequest(this._execCallback, this, flag2, activityHash) != 1)
				{
					this._rootedThis.Free();
					this._ar.Complete(true, null, new HttpException(SR.GetString("Cannot_access_AspCompat")));
				}
			}
			return this._ar;
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x0016F7DE File Offset: 0x0016E7DE
		internal void EndAspCompatExecution(IAsyncResult ar)
		{
			this._ar.End();
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x0016F7EC File Offset: 0x0016E7EC
		internal static void RaiseAspCompatEvent(HttpContext context, HttpApplication app, string sessionId, EventHandler eventHandler, object source, EventArgs eventArgs)
		{
			AspCompatApplicationStep aspCompatApplicationStep = new AspCompatApplicationStep(context, app, sessionId, eventHandler, source, eventArgs);
			IAsyncResult asyncResult = aspCompatApplicationStep.BeginAspCompatExecution(null, null);
			if (!asyncResult.IsCompleted)
			{
				WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
				if (asyncWaitHandle != null)
				{
					asyncWaitHandle.WaitOne();
				}
				else
				{
					while (!asyncResult.IsCompleted)
					{
						Thread.Sleep(1);
					}
				}
			}
			aspCompatApplicationStep.EndAspCompatExecution(asyncResult);
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x0016F840 File Offset: 0x0016E840
		private void ExecuteAspCompatCode()
		{
			this.MarkCallContext(this);
			try
			{
				bool flag = true;
				if (this._context != null)
				{
					HttpApplication.ThreadContext threadContext = null;
					try
					{
						threadContext = this._app.OnThreadEnter();
						this._error = this._app.ExecuteStep(this, ref flag);
						goto IL_53;
					}
					finally
					{
						if (threadContext != null)
						{
							threadContext.Leave();
						}
					}
				}
				this._error = this._app.ExecuteStep(this, ref flag);
				IL_53:;
			}
			finally
			{
				this.MarkCallContext(null);
			}
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x0016F8C8 File Offset: 0x0016E8C8
		private void OnAspCompatExecution()
		{
			try
			{
				if (this._syncCaller)
				{
					this.ExecuteAspCompatCode();
				}
				else
				{
					lock (this._app)
					{
						this.ExecuteAspCompatCode();
					}
				}
			}
			finally
			{
				UnsafeNativeMethods.AspCompatOnPageEnd();
				if (this._staComponents != null)
				{
					foreach (object obj in this._staComponents)
					{
						if (!this.IsStaComponentInSessionState(obj))
						{
							Marshal.ReleaseComObject(obj);
						}
					}
				}
				this._ar.SetComplete();
				WorkItem.PostInternal(this._compCallback);
			}
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x0016F990 File Offset: 0x0016E990
		private void OnAspCompatCompletion()
		{
			this._rootedThis.Free();
			this._ar.Complete(false, null, this._error);
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x0016F9B0 File Offset: 0x0016E9B0
		private static string EncodeTab(string value)
		{
			if (string.IsNullOrEmpty(value) || value.IndexOfAny(AspCompatApplicationStep.TabOrBackSpace) < 0)
			{
				return value;
			}
			return value.Replace("\b", "\bB").Replace("\t", "\bT");
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x0016F9E9 File Offset: 0x0016E9E9
		private static string EncodeTab(object value)
		{
			return AspCompatApplicationStep.EncodeTab((string)value);
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x0016F9F8 File Offset: 0x0016E9F8
		private static string CollectionToString(NameValueCollection c)
		{
			int count = c.Count;
			if (count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			for (int i = 0; i < count; i++)
			{
				string text = AspCompatApplicationStep.EncodeTab(c.GetKey(i));
				string[] values = c.GetValues(i);
				int num = (values != null) ? values.Length : 0;
				stringBuilder.Append(string.Concat(new object[]
				{
					text,
					"\t",
					num,
					"\t"
				}));
				for (int j = 0; j < num; j++)
				{
					stringBuilder.Append(AspCompatApplicationStep.EncodeTab(values[j]));
					if (j < values.Length - 1)
					{
						stringBuilder.Append("\t");
					}
				}
				if (i < count - 1)
				{
					stringBuilder.Append("\t");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x0016FAE0 File Offset: 0x0016EAE0
		private static string CookiesToString(HttpCookieCollection cc)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			StringBuilder stringBuilder2 = new StringBuilder(128);
			int count = cc.Count;
			stringBuilder.Append(count.ToString() + "\t");
			for (int i = 0; i < count; i++)
			{
				HttpCookie httpCookie = cc[i];
				string text = AspCompatApplicationStep.EncodeTab(httpCookie.Name);
				string text2 = AspCompatApplicationStep.EncodeTab(httpCookie.Value);
				stringBuilder.Append(text + "\t" + text2 + "\t");
				if (i > 0)
				{
					stringBuilder2.Append(";" + text + "=" + text2);
				}
				else
				{
					stringBuilder2.Append(text + "=" + text2);
				}
				NameValueCollection values = httpCookie.Values;
				int count2 = values.Count;
				bool flag = false;
				if (values.HasKeys())
				{
					for (int j = 0; j < count2; j++)
					{
						string key = values.GetKey(j);
						if (!string.IsNullOrEmpty(key))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					stringBuilder.Append(count2 + "\t");
					for (int k = 0; k < count2; k++)
					{
						stringBuilder.Append(AspCompatApplicationStep.EncodeTab(values.GetKey(k)) + "\t" + AspCompatApplicationStep.EncodeTab(values.Get(k)) + "\t");
					}
				}
				else
				{
					stringBuilder.Append("0\t");
				}
			}
			stringBuilder2.Append("\t");
			stringBuilder2.Append(stringBuilder.ToString());
			return stringBuilder2.ToString();
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x0016FC7C File Offset: 0x0016EC7C
		private static string StringArrayToString(string[] ss)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (ss != null)
			{
				for (int i = 0; i < ss.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append("\t");
					}
					stringBuilder.Append(AspCompatApplicationStep.EncodeTab(ss[i]));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x0016FCCC File Offset: 0x0016ECCC
		private static string EnumKeysToString(IEnumerator e)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (e.MoveNext())
			{
				stringBuilder.Append(AspCompatApplicationStep.EncodeTab(e.Current));
				while (e.MoveNext())
				{
					stringBuilder.Append("\t");
					stringBuilder.Append(AspCompatApplicationStep.EncodeTab(e.Current));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x0016FD2C File Offset: 0x0016ED2C
		private static string DictEnumKeysToString(IDictionaryEnumerator e)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (e.MoveNext())
			{
				stringBuilder.Append(AspCompatApplicationStep.EncodeTab(e.Key));
				while (e.MoveNext())
				{
					stringBuilder.Append("\t");
					stringBuilder.Append(AspCompatApplicationStep.EncodeTab(e.Key));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x0016FD8C File Offset: 0x0016ED8C
		int IManagedContext.Context_IsPresent()
		{
			if (this._context == null)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x0016FD99 File Offset: 0x0016ED99
		void IManagedContext.Application_Lock()
		{
			this._context.Application.Lock();
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0016FDAB File Offset: 0x0016EDAB
		void IManagedContext.Application_UnLock()
		{
			this._context.Application.UnLock();
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0016FDBD File Offset: 0x0016EDBD
		string IManagedContext.Application_GetContentsNames()
		{
			return AspCompatApplicationStep.StringArrayToString(this._context.Application.AllKeys);
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x0016FDD4 File Offset: 0x0016EDD4
		string IManagedContext.Application_GetStaticNames()
		{
			return AspCompatApplicationStep.DictEnumKeysToString((IDictionaryEnumerator)this._context.Application.StaticObjects.GetEnumerator());
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x0016FDF5 File Offset: 0x0016EDF5
		object IManagedContext.Application_GetContentsObject(string name)
		{
			return this._context.Application[name];
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x0016FE08 File Offset: 0x0016EE08
		void IManagedContext.Application_SetContentsObject(string name, object obj)
		{
			this._context.Application[name] = obj;
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x0016FE1C File Offset: 0x0016EE1C
		void IManagedContext.Application_RemoveContentsObject(string name)
		{
			this._context.Application.Remove(name);
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x0016FE2F File Offset: 0x0016EE2F
		void IManagedContext.Application_RemoveAllContentsObjects()
		{
			this._context.Application.RemoveAll();
		}

		// Token: 0x06005B6E RID: 23406 RVA: 0x0016FE41 File Offset: 0x0016EE41
		object IManagedContext.Application_GetStaticObject(string name)
		{
			return this._context.Application.StaticObjects[name];
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x0016FE5C File Offset: 0x0016EE5C
		string IManagedContext.Request_GetAsString(int what)
		{
			string empty = string.Empty;
			switch (what)
			{
			case 1:
				return AspCompatApplicationStep.CollectionToString(this._context.Request.QueryString);
			case 2:
				return AspCompatApplicationStep.CollectionToString(this._context.Request.Form);
			case 3:
				return string.Empty;
			case 4:
				return AspCompatApplicationStep.CollectionToString(this._context.Request.ServerVariables);
			default:
				return empty;
			}
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x0016FED4 File Offset: 0x0016EED4
		string IManagedContext.Request_GetCookiesAsString()
		{
			return AspCompatApplicationStep.CookiesToString(this._context.Request.Cookies);
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x0016FEEB File Offset: 0x0016EEEB
		int IManagedContext.Request_GetTotalBytes()
		{
			return this._context.Request.TotalBytes;
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x0016FEFD File Offset: 0x0016EEFD
		int IManagedContext.Request_BinaryRead(byte[] bytes, int size)
		{
			return this._context.Request.InputStream.Read(bytes, 0, size);
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x0016FF17 File Offset: 0x0016EF17
		string IManagedContext.Response_GetCookiesAsString()
		{
			return AspCompatApplicationStep.CookiesToString(this._context.Response.Cookies);
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x0016FF2E File Offset: 0x0016EF2E
		void IManagedContext.Response_AddCookie(string name)
		{
			this._context.Response.Cookies.Add(new HttpCookie(name));
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x0016FF4B File Offset: 0x0016EF4B
		void IManagedContext.Response_SetCookieText(string name, string text)
		{
			this._context.Response.Cookies[name].Value = text;
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x0016FF69 File Offset: 0x0016EF69
		void IManagedContext.Response_SetCookieSubValue(string name, string key, string value)
		{
			this._context.Response.Cookies[name].Values[key] = value;
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x0016FF8D File Offset: 0x0016EF8D
		void IManagedContext.Response_SetCookieExpires(string name, double dtExpires)
		{
			this._context.Response.Cookies[name].Expires = DateTime.FromOADate(dtExpires);
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x0016FFB0 File Offset: 0x0016EFB0
		void IManagedContext.Response_SetCookieDomain(string name, string domain)
		{
			this._context.Response.Cookies[name].Domain = domain;
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x0016FFCE File Offset: 0x0016EFCE
		void IManagedContext.Response_SetCookiePath(string name, string path)
		{
			this._context.Response.Cookies[name].Path = path;
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x0016FFEC File Offset: 0x0016EFEC
		void IManagedContext.Response_SetCookieSecure(string name, int secure)
		{
			this._context.Response.Cookies[name].Secure = (secure != 0);
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x00170010 File Offset: 0x0016F010
		void IManagedContext.Response_Write(string text)
		{
			this._context.Response.Write(text);
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x00170023 File Offset: 0x0016F023
		void IManagedContext.Response_BinaryWrite(byte[] bytes, int size)
		{
			this._context.Response.OutputStream.Write(bytes, 0, size);
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x0017003D File Offset: 0x0016F03D
		void IManagedContext.Response_Redirect(string url)
		{
			this._context.Response.Redirect(url);
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x00170050 File Offset: 0x0016F050
		void IManagedContext.Response_AddHeader(string name, string value)
		{
			this._context.Response.AppendHeader(name, value);
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x00170064 File Offset: 0x0016F064
		void IManagedContext.Response_Pics(string value)
		{
			this._context.Response.Pics(value);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x00170077 File Offset: 0x0016F077
		void IManagedContext.Response_Clear()
		{
			this._context.Response.Clear();
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x00170089 File Offset: 0x0016F089
		void IManagedContext.Response_Flush()
		{
			this._context.Response.Flush();
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x0017009B File Offset: 0x0016F09B
		void IManagedContext.Response_End()
		{
			this._context.Response.End();
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x001700AD File Offset: 0x0016F0AD
		void IManagedContext.Response_AppendToLog(string entry)
		{
			this._context.Response.AppendToLog(entry);
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x001700C0 File Offset: 0x0016F0C0
		string IManagedContext.Response_GetContentType()
		{
			return this._context.Response.ContentType;
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x001700D2 File Offset: 0x0016F0D2
		void IManagedContext.Response_SetContentType(string contentType)
		{
			this._context.Response.ContentType = contentType;
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x001700E5 File Offset: 0x0016F0E5
		string IManagedContext.Response_GetCharSet()
		{
			return this._context.Response.Charset;
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x001700F7 File Offset: 0x0016F0F7
		void IManagedContext.Response_SetCharSet(string charSet)
		{
			this._context.Response.Charset = charSet;
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x0017010A File Offset: 0x0016F10A
		string IManagedContext.Response_GetCacheControl()
		{
			return this._context.Response.CacheControl;
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x0017011C File Offset: 0x0016F11C
		void IManagedContext.Response_SetCacheControl(string cacheControl)
		{
			this._context.Response.CacheControl = cacheControl;
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x0017012F File Offset: 0x0016F12F
		string IManagedContext.Response_GetStatus()
		{
			return this._context.Response.Status;
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x00170141 File Offset: 0x0016F141
		void IManagedContext.Response_SetStatus(string status)
		{
			this._context.Response.Status = status;
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x00170154 File Offset: 0x0016F154
		int IManagedContext.Response_GetExpiresMinutes()
		{
			return this._context.Response.Expires;
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x00170166 File Offset: 0x0016F166
		void IManagedContext.Response_SetExpiresMinutes(int expiresMinutes)
		{
			this._context.Response.Expires = expiresMinutes;
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x0017017C File Offset: 0x0016F17C
		double IManagedContext.Response_GetExpiresAbsolute()
		{
			return this._context.Response.ExpiresAbsolute.ToOADate();
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x001701A1 File Offset: 0x0016F1A1
		void IManagedContext.Response_SetExpiresAbsolute(double dtExpires)
		{
			this._context.Response.ExpiresAbsolute = DateTime.FromOADate(dtExpires);
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x001701B9 File Offset: 0x0016F1B9
		int IManagedContext.Response_GetIsBuffering()
		{
			if (!this._context.Response.BufferOutput)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x001701D0 File Offset: 0x0016F1D0
		void IManagedContext.Response_SetIsBuffering(int isBuffering)
		{
			this._context.Response.BufferOutput = (isBuffering != 0);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x001701E9 File Offset: 0x0016F1E9
		int IManagedContext.Response_IsClientConnected()
		{
			if (!this._context.Response.IsClientConnected)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x00170200 File Offset: 0x0016F200
		object IManagedContext.Server_CreateObject(string progId)
		{
			return this._context.Server.CreateObject(progId);
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x00170213 File Offset: 0x0016F213
		string IManagedContext.Server_MapPath(string logicalPath)
		{
			return this._context.Server.MapPath(logicalPath);
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x00170226 File Offset: 0x0016F226
		string IManagedContext.Server_HTMLEncode(string str)
		{
			return HttpUtility.HtmlEncode(str);
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x0017022E File Offset: 0x0016F22E
		string IManagedContext.Server_URLEncode(string str)
		{
			return this._context.Server.UrlEncode(str);
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x00170241 File Offset: 0x0016F241
		string IManagedContext.Server_URLPathEncode(string str)
		{
			return this._context.Server.UrlPathEncode(str);
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x00170254 File Offset: 0x0016F254
		int IManagedContext.Server_GetScriptTimeout()
		{
			return this._context.Server.ScriptTimeout;
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x00170266 File Offset: 0x0016F266
		void IManagedContext.Server_SetScriptTimeout(int timeoutSeconds)
		{
			this._context.Server.ScriptTimeout = timeoutSeconds;
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x00170279 File Offset: 0x0016F279
		void IManagedContext.Server_Execute(string url)
		{
			this._context.Server.Execute(url);
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x0017028C File Offset: 0x0016F28C
		void IManagedContext.Server_Transfer(string url)
		{
			this._context.Server.Transfer(url);
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x0017029F File Offset: 0x0016F29F
		int IManagedContext.Session_IsPresent()
		{
			if (this._context.Session == null)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x001702B1 File Offset: 0x0016F2B1
		string IManagedContext.Session_GetID()
		{
			return this._context.Session.SessionID;
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x001702C3 File Offset: 0x0016F2C3
		int IManagedContext.Session_GetTimeout()
		{
			return this._context.Session.Timeout;
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x001702D5 File Offset: 0x0016F2D5
		void IManagedContext.Session_SetTimeout(int value)
		{
			this._context.Session.Timeout = value;
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x001702E8 File Offset: 0x0016F2E8
		int IManagedContext.Session_GetCodePage()
		{
			return this._context.Session.CodePage;
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x001702FA File Offset: 0x0016F2FA
		void IManagedContext.Session_SetCodePage(int value)
		{
			this._context.Session.CodePage = value;
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x0017030D File Offset: 0x0016F30D
		int IManagedContext.Session_GetLCID()
		{
			return this._context.Session.LCID;
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x0017031F File Offset: 0x0016F31F
		void IManagedContext.Session_SetLCID(int value)
		{
			this._context.Session.LCID = value;
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x00170332 File Offset: 0x0016F332
		void IManagedContext.Session_Abandon()
		{
			this._context.Session.Abandon();
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x00170344 File Offset: 0x0016F344
		string IManagedContext.Session_GetContentsNames()
		{
			return AspCompatApplicationStep.EnumKeysToString(this._context.Session.GetEnumerator());
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x0017035B File Offset: 0x0016F35B
		string IManagedContext.Session_GetStaticNames()
		{
			return AspCompatApplicationStep.DictEnumKeysToString((IDictionaryEnumerator)this._context.Session.StaticObjects.GetEnumerator());
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x0017037C File Offset: 0x0016F37C
		object IManagedContext.Session_GetContentsObject(string name)
		{
			return this._context.Session[name];
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x0017038F File Offset: 0x0016F38F
		void IManagedContext.Session_SetContentsObject(string name, object obj)
		{
			this._context.Session[name] = obj;
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x001703A3 File Offset: 0x0016F3A3
		void IManagedContext.Session_RemoveContentsObject(string name)
		{
			this._context.Session.Remove(name);
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x001703B6 File Offset: 0x0016F3B6
		void IManagedContext.Session_RemoveAllContentsObjects()
		{
			this._context.Session.RemoveAll();
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x001703C8 File Offset: 0x0016F3C8
		object IManagedContext.Session_GetStaticObject(string name)
		{
			return this._context.Session.StaticObjects[name];
		}

		// Token: 0x04003107 RID: 12551
		private GCHandle _rootedThis;

		// Token: 0x04003108 RID: 12552
		private HttpContext _context;

		// Token: 0x04003109 RID: 12553
		private HttpApplication _app;

		// Token: 0x0400310A RID: 12554
		private string _sessionId;

		// Token: 0x0400310B RID: 12555
		private AspCompatCallback _code;

		// Token: 0x0400310C RID: 12556
		private EventHandler _codeEventHandler;

		// Token: 0x0400310D RID: 12557
		private object _codeEventSource;

		// Token: 0x0400310E RID: 12558
		private EventArgs _codeEventArgs;

		// Token: 0x0400310F RID: 12559
		private Exception _error;

		// Token: 0x04003110 RID: 12560
		private HttpAsyncResult _ar;

		// Token: 0x04003111 RID: 12561
		private bool _syncCaller;

		// Token: 0x04003112 RID: 12562
		private AspCompatCallback _execCallback;

		// Token: 0x04003113 RID: 12563
		private WorkItemCallback _compCallback;

		// Token: 0x04003114 RID: 12564
		private ArrayList _staComponents;

		// Token: 0x04003115 RID: 12565
		private static char[] TabOrBackSpace = new char[]
		{
			'\t',
			'\b'
		};
	}
}
