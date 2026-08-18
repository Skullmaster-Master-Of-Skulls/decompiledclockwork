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
	// Token: 0x020001ED RID: 493
	internal class AspCompatApplicationStep : HttpApplication.IExecutionStep, IManagedContext
	{
		// Token: 0x06001854 RID: 6228 RVA: 0x0004BA12 File Offset: 0x00049C12
		internal AspCompatApplicationStep(HttpContext context, AspCompatCallback code)
		{
			this._code = code;
			this.Init(context, context.ApplicationInstance);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0004BA2E File Offset: 0x00049C2E
		private AspCompatApplicationStep(HttpContext context, HttpApplication app, string sessionId, EventHandler codeEventHandler, object codeEventSource, EventArgs codeEventArgs)
		{
			this._codeEventHandler = codeEventHandler;
			this._codeEventSource = codeEventSource;
			this._codeEventArgs = codeEventArgs;
			this._sessionId = sessionId;
			this.Init(context, app);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0004BA60 File Offset: 0x00049C60
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

		// Token: 0x06001857 RID: 6231 RVA: 0x0004BAD2 File Offset: 0x00049CD2
		private void MarkCallContext(AspCompatApplicationStep mark)
		{
			CallContext.SetData("AspCompat", mark);
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0004BADF File Offset: 0x00049CDF
		private static AspCompatApplicationStep Current
		{
			get
			{
				return (AspCompatApplicationStep)CallContext.GetData("AspCompat");
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0004BAF0 File Offset: 0x00049CF0
		internal static bool IsInAspCompatMode
		{
			get
			{
				return AspCompatApplicationStep.Current != null;
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0004BAFA File Offset: 0x00049CFA
		void HttpApplication.IExecutionStep.Execute()
		{
			SynchronizationContextUtil.ValidateModeForAspCompat();
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

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x000097B7 File Offset: 0x000079B7
		bool HttpApplication.IExecutionStep.CompletedSynchronously
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x0004BB34 File Offset: 0x00049D34
		bool HttpApplication.IExecutionStep.IsCancellable
		{
			get
			{
				return this._context != null;
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0004BB3F File Offset: 0x00049D3F
		private void RememberStaComponent(object component)
		{
			if (this._staComponents == null)
			{
				this._staComponents = new ArrayList();
			}
			this._staComponents.Add(component);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0004BB64 File Offset: 0x00049D64
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

		// Token: 0x0600185F RID: 6239 RVA: 0x0004BBAC File Offset: 0x00049DAC
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

		// Token: 0x06001860 RID: 6240 RVA: 0x0004BC00 File Offset: 0x00049E00
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

		// Token: 0x06001861 RID: 6241 RVA: 0x0004BC44 File Offset: 0x00049E44
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

		// Token: 0x06001862 RID: 6242 RVA: 0x0004BCBC File Offset: 0x00049EBC
		internal static void CheckThreadingModel(string progidDisplayName, Guid clsid)
		{
			if (AspCompatApplicationStep.IsInAspCompatMode)
			{
				return;
			}
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string key = "s" + progidDisplayName;
			string text = (string)internalCache.Get(key);
			RegistryKey registryKey = null;
			if (text == null)
			{
				try
				{
					RegistryKey classesRoot = Registry.ClassesRoot;
					string str = "CLSID\\{";
					Guid guid = clsid;
					registryKey = classesRoot.OpenSubKey(str + guid.ToString() + "}\\InprocServer32");
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
				internalCache.Insert(key, text, null);
			}
			if (StringUtil.EqualsIgnoreCase(text, "Apartment"))
			{
				throw new HttpException(SR.GetString("Apartment_component_not_allowed", new object[]
				{
					progidDisplayName
				}));
			}
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0004BD9C File Offset: 0x00049F9C
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		internal IAsyncResult BeginAspCompatExecution(AsyncCallback cb, object extraData)
		{
			SynchronizationContextUtil.ValidateModeForAspCompat();
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

		// Token: 0x06001864 RID: 6244 RVA: 0x0004BE5C File Offset: 0x0004A05C
		internal void EndAspCompatExecution(IAsyncResult ar)
		{
			this._ar.End();
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0004BE6C File Offset: 0x0004A06C
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

		// Token: 0x06001866 RID: 6246 RVA: 0x0004BEC0 File Offset: 0x0004A0C0
		private void ExecuteAspCompatCode()
		{
			this.MarkCallContext(this);
			try
			{
				bool flag = true;
				if (this._context != null)
				{
					ThreadContext threadContext = null;
					try
					{
						threadContext = this._app.OnThreadEnter();
						this._error = this._app.ExecuteStep(this, ref flag);
						return;
					}
					finally
					{
						if (threadContext != null)
						{
							threadContext.DisassociateFromCurrentThread();
						}
					}
				}
				this._error = this._app.ExecuteStep(this, ref flag);
			}
			finally
			{
				this.MarkCallContext(null);
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0004BF48 File Offset: 0x0004A148
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
					HttpApplication app = this._app;
					lock (app)
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

		// Token: 0x06001868 RID: 6248 RVA: 0x0004C01C File Offset: 0x0004A21C
		private void OnAspCompatCompletion()
		{
			this._rootedThis.Free();
			this._ar.Complete(false, null, this._error);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0004C03C File Offset: 0x0004A23C
		private static string EncodeTab(string value)
		{
			if (string.IsNullOrEmpty(value) || value.IndexOfAny(AspCompatApplicationStep.TabOrBackSpace) < 0)
			{
				return value;
			}
			return value.Replace("\b", "\bB").Replace("\t", "\bT");
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0004C075 File Offset: 0x0004A275
		private static string EncodeTab(object value)
		{
			return AspCompatApplicationStep.EncodeTab((string)value);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0004C084 File Offset: 0x0004A284
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
				string str = AspCompatApplicationStep.EncodeTab(c.GetKey(i));
				string[] values = c.GetValues(i);
				int num = (values != null) ? values.Length : 0;
				stringBuilder.Append(str + "\t" + num.ToString() + "\t");
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

		// Token: 0x0600186C RID: 6252 RVA: 0x0004C150 File Offset: 0x0004A350
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
					stringBuilder.Append(count2.ToString() + "\t");
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

		// Token: 0x0600186D RID: 6253 RVA: 0x0004C2EC File Offset: 0x0004A4EC
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

		// Token: 0x0600186E RID: 6254 RVA: 0x0004C33C File Offset: 0x0004A53C
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

		// Token: 0x0600186F RID: 6255 RVA: 0x0004C39C File Offset: 0x0004A59C
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

		// Token: 0x06001870 RID: 6256 RVA: 0x0004C3FC File Offset: 0x0004A5FC
		int IManagedContext.Context_IsPresent()
		{
			if (this._context == null)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0004C409 File Offset: 0x0004A609
		void IManagedContext.Application_Lock()
		{
			this._context.Application.Lock();
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0004C41B File Offset: 0x0004A61B
		void IManagedContext.Application_UnLock()
		{
			this._context.Application.UnLock();
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0004C42D File Offset: 0x0004A62D
		string IManagedContext.Application_GetContentsNames()
		{
			return AspCompatApplicationStep.StringArrayToString(this._context.Application.AllKeys);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0004C444 File Offset: 0x0004A644
		string IManagedContext.Application_GetStaticNames()
		{
			return AspCompatApplicationStep.DictEnumKeysToString((IDictionaryEnumerator)this._context.Application.StaticObjects.GetEnumerator());
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0004C465 File Offset: 0x0004A665
		object IManagedContext.Application_GetContentsObject(string name)
		{
			return this._context.Application[name];
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0004C478 File Offset: 0x0004A678
		void IManagedContext.Application_SetContentsObject(string name, object obj)
		{
			this._context.Application[name] = obj;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0004C48C File Offset: 0x0004A68C
		void IManagedContext.Application_RemoveContentsObject(string name)
		{
			this._context.Application.Remove(name);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0004C49F File Offset: 0x0004A69F
		void IManagedContext.Application_RemoveAllContentsObjects()
		{
			this._context.Application.RemoveAll();
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0004C4B1 File Offset: 0x0004A6B1
		object IManagedContext.Application_GetStaticObject(string name)
		{
			return this._context.Application.StaticObjects[name];
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0004C4CC File Offset: 0x0004A6CC
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

		// Token: 0x0600187B RID: 6267 RVA: 0x0004C544 File Offset: 0x0004A744
		string IManagedContext.Request_GetCookiesAsString()
		{
			return AspCompatApplicationStep.CookiesToString(this._context.Request.Cookies);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0004C55B File Offset: 0x0004A75B
		int IManagedContext.Request_GetTotalBytes()
		{
			return this._context.Request.TotalBytes;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0004C56D File Offset: 0x0004A76D
		int IManagedContext.Request_BinaryRead(byte[] bytes, int size)
		{
			return this._context.Request.InputStream.Read(bytes, 0, size);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0004C587 File Offset: 0x0004A787
		string IManagedContext.Response_GetCookiesAsString()
		{
			return AspCompatApplicationStep.CookiesToString(this._context.Response.Cookies);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0004C59E File Offset: 0x0004A79E
		void IManagedContext.Response_AddCookie(string name)
		{
			this._context.Response.Cookies.Add(new HttpCookie(name));
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0004C5BB File Offset: 0x0004A7BB
		void IManagedContext.Response_SetCookieText(string name, string text)
		{
			this._context.Response.Cookies[name].Value = text;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0004C5D9 File Offset: 0x0004A7D9
		void IManagedContext.Response_SetCookieSubValue(string name, string key, string value)
		{
			this._context.Response.Cookies[name].Values[key] = value;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0004C5FD File Offset: 0x0004A7FD
		void IManagedContext.Response_SetCookieExpires(string name, double dtExpires)
		{
			this._context.Response.Cookies[name].Expires = DateTime.FromOADate(dtExpires);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0004C620 File Offset: 0x0004A820
		void IManagedContext.Response_SetCookieDomain(string name, string domain)
		{
			this._context.Response.Cookies[name].Domain = domain;
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0004C63E File Offset: 0x0004A83E
		void IManagedContext.Response_SetCookiePath(string name, string path)
		{
			this._context.Response.Cookies[name].Path = path;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0004C65C File Offset: 0x0004A85C
		void IManagedContext.Response_SetCookieSecure(string name, int secure)
		{
			this._context.Response.Cookies[name].Secure = (secure != 0);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0004C67D File Offset: 0x0004A87D
		void IManagedContext.Response_Write(string text)
		{
			this._context.Response.Write(text);
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0004C690 File Offset: 0x0004A890
		void IManagedContext.Response_BinaryWrite(byte[] bytes, int size)
		{
			this._context.Response.OutputStream.Write(bytes, 0, size);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0004C6AA File Offset: 0x0004A8AA
		void IManagedContext.Response_Redirect(string url)
		{
			this._context.Response.Redirect(url);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0004C6BD File Offset: 0x0004A8BD
		void IManagedContext.Response_AddHeader(string name, string value)
		{
			this._context.Response.AppendHeader(name, value);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0004C6D1 File Offset: 0x0004A8D1
		void IManagedContext.Response_Pics(string value)
		{
			this._context.Response.Pics(value);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0004C6E4 File Offset: 0x0004A8E4
		void IManagedContext.Response_Clear()
		{
			this._context.Response.Clear();
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0004C6F6 File Offset: 0x0004A8F6
		void IManagedContext.Response_Flush()
		{
			this._context.Response.Flush();
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0004C708 File Offset: 0x0004A908
		void IManagedContext.Response_End()
		{
			this._context.Response.End();
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0004C71A File Offset: 0x0004A91A
		void IManagedContext.Response_AppendToLog(string entry)
		{
			this._context.Response.AppendToLog(entry);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0004C72D File Offset: 0x0004A92D
		string IManagedContext.Response_GetContentType()
		{
			return this._context.Response.ContentType;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0004C73F File Offset: 0x0004A93F
		void IManagedContext.Response_SetContentType(string contentType)
		{
			this._context.Response.ContentType = contentType;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0004C752 File Offset: 0x0004A952
		string IManagedContext.Response_GetCharSet()
		{
			return this._context.Response.Charset;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0004C764 File Offset: 0x0004A964
		void IManagedContext.Response_SetCharSet(string charSet)
		{
			this._context.Response.Charset = charSet;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0004C777 File Offset: 0x0004A977
		string IManagedContext.Response_GetCacheControl()
		{
			return this._context.Response.CacheControl;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0004C789 File Offset: 0x0004A989
		void IManagedContext.Response_SetCacheControl(string cacheControl)
		{
			this._context.Response.CacheControl = cacheControl;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0004C79C File Offset: 0x0004A99C
		string IManagedContext.Response_GetStatus()
		{
			return this._context.Response.Status;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0004C7AE File Offset: 0x0004A9AE
		void IManagedContext.Response_SetStatus(string status)
		{
			this._context.Response.Status = status;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0004C7C1 File Offset: 0x0004A9C1
		int IManagedContext.Response_GetExpiresMinutes()
		{
			return this._context.Response.Expires;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0004C7D3 File Offset: 0x0004A9D3
		void IManagedContext.Response_SetExpiresMinutes(int expiresMinutes)
		{
			this._context.Response.Expires = expiresMinutes;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0004C7E8 File Offset: 0x0004A9E8
		double IManagedContext.Response_GetExpiresAbsolute()
		{
			return this._context.Response.ExpiresAbsolute.ToOADate();
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0004C80D File Offset: 0x0004AA0D
		void IManagedContext.Response_SetExpiresAbsolute(double dtExpires)
		{
			this._context.Response.ExpiresAbsolute = DateTime.FromOADate(dtExpires);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0004C825 File Offset: 0x0004AA25
		int IManagedContext.Response_GetIsBuffering()
		{
			if (!this._context.Response.BufferOutput)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0004C83C File Offset: 0x0004AA3C
		void IManagedContext.Response_SetIsBuffering(int isBuffering)
		{
			this._context.Response.BufferOutput = (isBuffering != 0);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0004C852 File Offset: 0x0004AA52
		int IManagedContext.Response_IsClientConnected()
		{
			if (!this._context.Response.IsClientConnected)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0004C869 File Offset: 0x0004AA69
		object IManagedContext.Server_CreateObject(string progId)
		{
			return this._context.Server.CreateObject(progId);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0004C87C File Offset: 0x0004AA7C
		string IManagedContext.Server_MapPath(string logicalPath)
		{
			return this._context.Server.MapPath(logicalPath);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00023F59 File Offset: 0x00022159
		string IManagedContext.Server_HTMLEncode(string str)
		{
			return HttpUtility.HtmlEncode(str);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0004C88F File Offset: 0x0004AA8F
		string IManagedContext.Server_URLEncode(string str)
		{
			return this._context.Server.UrlEncode(str);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0004C8A2 File Offset: 0x0004AAA2
		string IManagedContext.Server_URLPathEncode(string str)
		{
			return this._context.Server.UrlPathEncode(str);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0004C8B5 File Offset: 0x0004AAB5
		int IManagedContext.Server_GetScriptTimeout()
		{
			return this._context.Server.ScriptTimeout;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0004C8C7 File Offset: 0x0004AAC7
		void IManagedContext.Server_SetScriptTimeout(int timeoutSeconds)
		{
			this._context.Server.ScriptTimeout = timeoutSeconds;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0004C8DA File Offset: 0x0004AADA
		void IManagedContext.Server_Execute(string url)
		{
			this._context.Server.Execute(url);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0004C8ED File Offset: 0x0004AAED
		void IManagedContext.Server_Transfer(string url)
		{
			this._context.Server.Transfer(url);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0004C900 File Offset: 0x0004AB00
		int IManagedContext.Session_IsPresent()
		{
			if (this._context.Session == null)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0004C912 File Offset: 0x0004AB12
		string IManagedContext.Session_GetID()
		{
			return this._context.Session.SessionID;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0004C924 File Offset: 0x0004AB24
		int IManagedContext.Session_GetTimeout()
		{
			return this._context.Session.Timeout;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0004C936 File Offset: 0x0004AB36
		void IManagedContext.Session_SetTimeout(int value)
		{
			this._context.Session.Timeout = value;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0004C949 File Offset: 0x0004AB49
		int IManagedContext.Session_GetCodePage()
		{
			return this._context.Session.CodePage;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0004C95B File Offset: 0x0004AB5B
		void IManagedContext.Session_SetCodePage(int value)
		{
			this._context.Session.CodePage = value;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0004C96E File Offset: 0x0004AB6E
		int IManagedContext.Session_GetLCID()
		{
			return this._context.Session.LCID;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0004C980 File Offset: 0x0004AB80
		void IManagedContext.Session_SetLCID(int value)
		{
			this._context.Session.LCID = value;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0004C993 File Offset: 0x0004AB93
		void IManagedContext.Session_Abandon()
		{
			this._context.Session.Abandon();
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0004C9A5 File Offset: 0x0004ABA5
		string IManagedContext.Session_GetContentsNames()
		{
			return AspCompatApplicationStep.EnumKeysToString(this._context.Session.GetEnumerator());
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0004C9BC File Offset: 0x0004ABBC
		string IManagedContext.Session_GetStaticNames()
		{
			return AspCompatApplicationStep.DictEnumKeysToString((IDictionaryEnumerator)this._context.Session.StaticObjects.GetEnumerator());
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0004C9DD File Offset: 0x0004ABDD
		object IManagedContext.Session_GetContentsObject(string name)
		{
			return this._context.Session[name];
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0004C9F0 File Offset: 0x0004ABF0
		void IManagedContext.Session_SetContentsObject(string name, object obj)
		{
			this._context.Session[name] = obj;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0004CA04 File Offset: 0x0004AC04
		void IManagedContext.Session_RemoveContentsObject(string name)
		{
			this._context.Session.Remove(name);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0004CA17 File Offset: 0x0004AC17
		void IManagedContext.Session_RemoveAllContentsObjects()
		{
			this._context.Session.RemoveAll();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0004CA29 File Offset: 0x0004AC29
		object IManagedContext.Session_GetStaticObject(string name)
		{
			return this._context.Session.StaticObjects[name];
		}

		// Token: 0x04001772 RID: 6002
		private GCHandle _rootedThis;

		// Token: 0x04001773 RID: 6003
		private HttpContext _context;

		// Token: 0x04001774 RID: 6004
		private HttpApplication _app;

		// Token: 0x04001775 RID: 6005
		private string _sessionId;

		// Token: 0x04001776 RID: 6006
		private AspCompatCallback _code;

		// Token: 0x04001777 RID: 6007
		private EventHandler _codeEventHandler;

		// Token: 0x04001778 RID: 6008
		private object _codeEventSource;

		// Token: 0x04001779 RID: 6009
		private EventArgs _codeEventArgs;

		// Token: 0x0400177A RID: 6010
		private Exception _error;

		// Token: 0x0400177B RID: 6011
		private HttpAsyncResult _ar;

		// Token: 0x0400177C RID: 6012
		private bool _syncCaller;

		// Token: 0x0400177D RID: 6013
		private AspCompatCallback _execCallback;

		// Token: 0x0400177E RID: 6014
		private WorkItemCallback _compCallback;

		// Token: 0x0400177F RID: 6015
		private ArrayList _staComponents;

		// Token: 0x04001780 RID: 6016
		private static char[] TabOrBackSpace = new char[]
		{
			'\t',
			'\b'
		};
	}
}
