using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000B8 RID: 184
	public sealed class HttpServerUtility
	{
		// Token: 0x06000CAF RID: 3247 RVA: 0x00022EC0 File Offset: 0x000210C0
		internal HttpServerUtility(HttpContext context)
		{
			this._context = context;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00022ECF File Offset: 0x000210CF
		internal HttpServerUtility(HttpApplication application)
		{
			this._application = application;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00022EE0 File Offset: 0x000210E0
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public object CreateObject(string progID)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			Type type = null;
			try
			{
				type = Type.GetTypeFromProgID(progID);
			}
			catch
			{
			}
			if (type == null)
			{
				throw new HttpException(SR.GetString("Could_not_create_object_of_type", new object[]
				{
					progID
				}));
			}
			AspCompatApplicationStep.CheckThreadingModel(progID, type.GUID);
			object obj = Activator.CreateInstance(type);
			AspCompatApplicationStep.OnPageStart(obj);
			return obj;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00022F50 File Offset: 0x00021150
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public object CreateObject(Type type)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			AspCompatApplicationStep.CheckThreadingModel(type.FullName, type.GUID);
			object obj = Activator.CreateInstance(type);
			AspCompatApplicationStep.OnPageStart(obj);
			return obj;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00022F84 File Offset: 0x00021184
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public object CreateObjectFromClsid(string clsid)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			object obj = null;
			Guid clsid2 = new Guid(clsid);
			AspCompatApplicationStep.CheckThreadingModel(clsid, clsid2);
			try
			{
				Type typeFromCLSID = Type.GetTypeFromCLSID(clsid2, null, true);
				obj = Activator.CreateInstance(typeFromCLSID);
			}
			catch
			{
			}
			if (obj == null)
			{
				throw new HttpException(SR.GetString("Could_not_create_object_from_clsid", new object[]
				{
					clsid
				}));
			}
			AspCompatApplicationStep.OnPageStart(obj);
			return obj;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00022FF4 File Offset: 0x000211F4
		internal static CultureInfo CreateReadOnlyCultureInfo(string name)
		{
			if (!HttpServerUtility._cultureCache.Contains(name))
			{
				IDictionary cultureCache = HttpServerUtility._cultureCache;
				lock (cultureCache)
				{
					if (HttpServerUtility._cultureCache[name] == null)
					{
						HttpServerUtility._cultureCache[name] = CultureInfo.ReadOnly(new CultureInfo(name));
					}
				}
			}
			return (CultureInfo)HttpServerUtility._cultureCache[name];
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00023070 File Offset: 0x00021270
		internal static CultureInfo CreateReadOnlySpecificCultureInfo(string name)
		{
			if (name.IndexOf('-') > 0)
			{
				return HttpServerUtility.CreateReadOnlyCultureInfo(name);
			}
			CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(name);
			if (!HttpServerUtility._cultureCache.Contains(cultureInfo.Name))
			{
				IDictionary cultureCache = HttpServerUtility._cultureCache;
				lock (cultureCache)
				{
					if (HttpServerUtility._cultureCache[cultureInfo.Name] == null)
					{
						HttpServerUtility._cultureCache[cultureInfo.Name] = CultureInfo.ReadOnly(cultureInfo);
					}
				}
			}
			return (CultureInfo)HttpServerUtility._cultureCache[cultureInfo.Name];
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00023114 File Offset: 0x00021314
		internal static CultureInfo CreateReadOnlyCultureInfo(int culture)
		{
			if (!HttpServerUtility._cultureCache.Contains(culture))
			{
				IDictionary cultureCache = HttpServerUtility._cultureCache;
				lock (cultureCache)
				{
					if (HttpServerUtility._cultureCache[culture] == null)
					{
						HttpServerUtility._cultureCache[culture] = CultureInfo.ReadOnly(new CultureInfo(culture));
					}
				}
			}
			return (CultureInfo)HttpServerUtility._cultureCache[culture];
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000231A4 File Offset: 0x000213A4
		public string MapPath(string path)
		{
			if (this._context == null)
			{
				throw new HttpException(SR.GetString("Server_not_available"));
			}
			bool hideRequestResponse = this._context.HideRequestResponse;
			string result;
			try
			{
				if (hideRequestResponse)
				{
					this._context.HideRequestResponse = false;
				}
				result = this._context.Request.MapPath(path);
			}
			finally
			{
				if (hideRequestResponse)
				{
					this._context.HideRequestResponse = true;
				}
			}
			return result;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002321C File Offset: 0x0002141C
		public Exception GetLastError()
		{
			if (this._context != null)
			{
				return this._context.Error;
			}
			if (this._application != null)
			{
				return this._application.LastError;
			}
			return null;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00023247 File Offset: 0x00021447
		public void ClearError()
		{
			if (this._context != null)
			{
				this._context.ClearError();
				return;
			}
			if (this._application != null)
			{
				this._application.ClearError();
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00023270 File Offset: 0x00021470
		public void Execute(string path)
		{
			this.Execute(path, null, true);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002327B File Offset: 0x0002147B
		public void Execute(string path, TextWriter writer)
		{
			this.Execute(path, writer, true);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00023286 File Offset: 0x00021486
		public void Execute(string path, bool preserveForm)
		{
			this.Execute(path, null, preserveForm);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00023294 File Offset: 0x00021494
		public void Execute(string path, TextWriter writer, bool preserveForm)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			if (this._context == null)
			{
				throw new HttpException(SR.GetString("Server_not_available"));
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string queryStringOverride = null;
			HttpRequest request = this._context.Request;
			HttpResponse response = this._context.Response;
			path = response.RemoveAppPathModifier(path);
			int num = path.IndexOf('?');
			if (num >= 0)
			{
				queryStringOverride = path.Substring(num + 1);
				path = path.Substring(0, num);
			}
			if (!UrlPath.IsValidVirtualPathWithoutProtocol(path))
			{
				throw new ArgumentException(SR.GetString("Invalid_path_for_child_request", new object[]
				{
					path
				}));
			}
			VirtualPath virtualPath = VirtualPath.Create(path);
			IHttpHandler handler = null;
			string text = request.MapPath(virtualPath);
			VirtualPath virtualPath2 = request.FilePathObject.Combine(virtualPath);
			InternalSecurityPermissions.FileReadAccess(text).Demand();
			if (HttpRuntime.IsLegacyCas)
			{
				InternalSecurityPermissions.Unrestricted.Assert();
			}
			try
			{
				if (StringUtil.StringEndsWith(virtualPath.VirtualPathString, '.'))
				{
					throw new HttpException(404, string.Empty);
				}
				bool useAppConfig = !virtualPath2.IsWithinAppRoot;
				using (new DisposableHttpContextWrapper(this._context))
				{
					try
					{
						HttpContext context = this._context;
						int serverExecuteDepth = context.ServerExecuteDepth;
						context.ServerExecuteDepth = serverExecuteDepth + 1;
						if (this._context.WorkerRequest is IIS7WorkerRequest)
						{
							handler = this._context.ApplicationInstance.MapIntegratedHttpHandler(this._context, request.RequestType, virtualPath2, text, useAppConfig, true);
						}
						else
						{
							handler = this._context.ApplicationInstance.MapHttpHandler(this._context, request.RequestType, virtualPath2, text, useAppConfig);
						}
					}
					finally
					{
						HttpContext context2 = this._context;
						int serverExecuteDepth = context2.ServerExecuteDepth;
						context2.ServerExecuteDepth = serverExecuteDepth - 1;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is HttpException)
				{
					int httpCode = ((HttpException)ex).GetHttpCode();
					if (httpCode != 500 && httpCode != 404)
					{
						ex = null;
					}
				}
				throw new HttpException(SR.GetString("Error_executing_child_request_for_path", new object[]
				{
					path
				}), ex);
			}
			this.ExecuteInternal(handler, writer, preserveForm, true, virtualPath, virtualPath2, text, null, queryStringOverride);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x000234D4 File Offset: 0x000216D4
		public void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
		{
			if (this._context == null)
			{
				throw new HttpException(SR.GetString("Server_not_available"));
			}
			this.Execute(handler, writer, preserveForm, true);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000234F8 File Offset: 0x000216F8
		internal void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm, bool setPreviousPage)
		{
			HttpRequest request = this._context.Request;
			VirtualPath currentExecutionFilePathObject = request.CurrentExecutionFilePathObject;
			string physPath = request.MapPath(currentExecutionFilePathObject);
			this.ExecuteInternal(handler, writer, preserveForm, setPreviousPage, null, currentExecutionFilePathObject, physPath, null, null);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00023530 File Offset: 0x00021730
		private void ExecuteInternal(IHttpHandler handler, TextWriter writer, bool preserveForm, bool setPreviousPage, VirtualPath path, VirtualPath filePath, string physPath, Exception error, string queryStringOverride)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			HttpRequest request = this._context.Request;
			HttpResponse response = this._context.Response;
			HttpApplication applicationInstance = this._context.ApplicationInstance;
			HttpValueCollection httpValueCollection = null;
			VirtualPath path2 = null;
			string text = null;
			TextWriter textWriter = null;
			AspNetSynchronizationContextBase aspNetSynchronizationContextBase = null;
			this.VerifyTransactionFlow(handler);
			this._context.PushTraceContext();
			this._context.SetCurrentHandler(handler);
			bool enabled = this._context.SyncContext.Enabled;
			this._context.SyncContext.Disable();
			try
			{
				try
				{
					HttpContext context = this._context;
					int serverExecuteDepth = context.ServerExecuteDepth;
					context.ServerExecuteDepth = serverExecuteDepth + 1;
					path2 = request.SwitchCurrentExecutionFilePath(filePath);
					if (!preserveForm)
					{
						httpValueCollection = request.SwitchForm(new HttpValueCollection());
						if (queryStringOverride == null)
						{
							queryStringOverride = string.Empty;
						}
					}
					if (queryStringOverride != null)
					{
						text = request.QueryStringText;
						request.QueryStringText = queryStringOverride;
					}
					if (writer != null)
					{
						textWriter = response.SwitchWriter(writer);
					}
					Page page = handler as Page;
					if (page != null)
					{
						if (setPreviousPage)
						{
							page.SetPreviousPage(this._context.PreviousHandler as Page);
						}
						Page page2 = this._context.Handler as Page;
						if (page2 != null && page2.SmartNavigation)
						{
							page.SmartNavigation = true;
						}
						if (page is IHttpAsyncHandler)
						{
							aspNetSynchronizationContextBase = this._context.InstallNewAspNetSynchronizationContext();
						}
					}
					if ((handler is StaticFileHandler || handler is DefaultHttpHandler) && !DefaultHttpHandler.IsClassicAspRequest(filePath.VirtualPathString))
					{
						try
						{
							response.WriteFile(physPath);
							goto IL_38C;
						}
						catch
						{
							error = new HttpException(404, string.Empty);
							goto IL_38C;
						}
					}
					if (!(handler is Page))
					{
						error = new HttpException(404, string.Empty);
					}
					else
					{
						if (handler is IHttpAsyncHandler)
						{
							bool isInCancellablePeriod = this._context.IsInCancellablePeriod;
							if (isInCancellablePeriod)
							{
								this._context.EndCancellablePeriod();
							}
							try
							{
								IHttpAsyncHandler httpAsyncHandler = (IHttpAsyncHandler)handler;
								if (!AppSettings.UseTaskFriendlySynchronizationContext)
								{
									IAsyncResult asyncResult = httpAsyncHandler.BeginProcessRequest(this._context, null, null);
									if (!asyncResult.IsCompleted)
									{
										bool flag = false;
										try
										{
											try
											{
											}
											finally
											{
												this._context.SyncContext.DisassociateFromCurrentThread();
												flag = true;
											}
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
										finally
										{
											if (flag)
											{
												this._context.SyncContext.AssociateWithCurrentThread();
											}
										}
									}
									try
									{
										httpAsyncHandler.EndProcessRequest(asyncResult);
										goto IL_349;
									}
									catch (Exception ex)
									{
										error = ex;
										goto IL_349;
									}
								}
								IAsyncResult result;
								bool flag2;
								using (CountdownEvent countdownEvent = new CountdownEvent(1))
								{
									using (this._context.SyncContext.AcquireThreadLock())
									{
										result = httpAsyncHandler.BeginProcessRequest(this._context, delegate(IAsyncResult _)
										{
											countdownEvent.Signal();
										}, null);
									}
									flag2 = !countdownEvent.IsSet;
									countdownEvent.Wait();
								}
								try
								{
									using (this._context.SyncContext.AcquireThreadLock())
									{
										httpAsyncHandler.EndProcessRequest(result);
									}
									if (flag2 && !this._context.SyncContext.AllowAsyncDuringSyncStages)
									{
										throw new InvalidOperationException(SR.GetString("Server_execute_blocked_on_async_handler"));
									}
								}
								catch (Exception ex2)
								{
									error = ex2;
								}
								IL_349:
								goto IL_38C;
							}
							finally
							{
								if (isInCancellablePeriod)
								{
									this._context.BeginCancellablePeriod();
								}
							}
						}
						using (new DisposableHttpContextWrapper(this._context))
						{
							try
							{
								handler.ProcessRequest(this._context);
							}
							catch (Exception ex3)
							{
								error = ex3;
							}
						}
					}
					IL_38C:;
				}
				finally
				{
					HttpContext context2 = this._context;
					int serverExecuteDepth = context2.ServerExecuteDepth;
					context2.ServerExecuteDepth = serverExecuteDepth - 1;
					this._context.RestoreCurrentHandler();
					if (textWriter != null)
					{
						response.SwitchWriter(textWriter);
					}
					if (queryStringOverride != null && text != null)
					{
						request.QueryStringText = text;
					}
					if (httpValueCollection != null)
					{
						request.SwitchForm(httpValueCollection);
					}
					request.SwitchCurrentExecutionFilePath(path2);
					if (aspNetSynchronizationContextBase != null)
					{
						this._context.RestoreSavedAspNetSynchronizationContext(aspNetSynchronizationContextBase);
					}
					if (enabled)
					{
						this._context.SyncContext.Enable();
					}
					this._context.PopTraceContext();
				}
			}
			catch
			{
				throw;
			}
			if (error == null)
			{
				return;
			}
			if (error is HttpException && ((HttpException)error).GetHttpCode() != 500)
			{
				error = null;
			}
			if (path != null)
			{
				throw new HttpException(SR.GetString("Error_executing_child_request_for_path", new object[]
				{
					path
				}), error);
			}
			throw new HttpException(SR.GetString("Error_executing_child_request_for_handler", new object[]
			{
				handler.GetType().ToString()
			}), error);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00023B04 File Offset: 0x00021D04
		public void Transfer(string path, bool preserveForm)
		{
			Page page = this._context.Handler as Page;
			if (page != null && page.IsCallback)
			{
				throw new ApplicationException(SR.GetString("Transfer_not_allowed_in_callback"));
			}
			this.Execute(path, null, preserveForm);
			this._context.Response.End();
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00023B58 File Offset: 0x00021D58
		public void Transfer(string path)
		{
			bool preventPostback = this._context.PreventPostback;
			this._context.PreventPostback = true;
			this.Transfer(path, true);
			this._context.PreventPostback = preventPostback;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00023B94 File Offset: 0x00021D94
		public void Transfer(IHttpHandler handler, bool preserveForm)
		{
			Page page = handler as Page;
			if (page != null && page.IsCallback)
			{
				throw new ApplicationException(SR.GetString("Transfer_not_allowed_in_callback"));
			}
			this.Execute(handler, null, preserveForm);
			this._context.Response.End();
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00023BDC File Offset: 0x00021DDC
		public void TransferRequest(string path)
		{
			this.TransferRequest(path, false, null, null, true);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00023BE9 File Offset: 0x00021DE9
		public void TransferRequest(string path, bool preserveForm)
		{
			this.TransferRequest(path, preserveForm, null, null, true);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00023BF6 File Offset: 0x00021DF6
		public void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
		{
			this.TransferRequest(path, preserveForm, method, headers, true);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00023C04 File Offset: 0x00021E04
		public void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers, bool preserveUser)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			if (this._context == null)
			{
				throw new HttpException(SR.GetString("Server_not_available"));
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			IIS7WorkerRequest iis7WorkerRequest = this._context.WorkerRequest as IIS7WorkerRequest;
			HttpRequest request = this._context.Request;
			HttpResponse response = this._context.Response;
			if (iis7WorkerRequest == null)
			{
				throw new HttpException(SR.GetString("Server_not_available"));
			}
			path = response.RemoveAppPathModifier(path);
			string queryString = null;
			int num = path.IndexOf('?');
			if (num >= 0)
			{
				queryString = ((num < path.Length - 1) ? path.Substring(num + 1) : string.Empty);
				path = path.Substring(0, num);
			}
			if (!UrlPath.IsValidVirtualPathWithoutProtocol(path))
			{
				throw new ArgumentException(SR.GetString("Invalid_path_for_child_request", new object[]
				{
					path
				}));
			}
			VirtualPath virtualPath = request.FilePathObject.Combine(VirtualPath.Create(path));
			iis7WorkerRequest.ScheduleExecuteUrl(virtualPath.VirtualPathString, queryString, method, preserveForm, preserveForm ? request.EntityBody : null, headers, preserveUser);
			this._context.ApplicationInstance.EnsureReleaseState();
			this._context.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00023D48 File Offset: 0x00021F48
		private void VerifyTransactionFlow(IHttpHandler handler)
		{
			Page page = this._context.Handler as Page;
			Page page2 = handler as Page;
			if (page2 != null && page2.IsInAspCompatMode && page != null && !page.IsInAspCompatMode && Transactions.Utils.IsInTransaction)
			{
				throw new HttpException(SR.GetString("Transacted_page_calls_aspcompat"));
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00023D9C File Offset: 0x00021F9C
		internal static void ExecuteLocalRequestAndCaptureResponse(string path, TextWriter writer, ErrorFormatterGenerator errorFormatterGenerator)
		{
			HttpRequest request = new HttpRequest(VirtualPath.CreateAbsolute(path), string.Empty);
			HttpResponse response = new HttpResponse(writer);
			HttpContext httpContext = new HttpContext(request, response);
			HttpApplication httpApplication = HttpApplicationFactory.GetApplicationInstance(httpContext) as HttpApplication;
			httpContext.ApplicationInstance = httpApplication;
			try
			{
				httpContext.Server.Execute(path);
			}
			catch (HttpException e)
			{
				if (errorFormatterGenerator != null)
				{
					httpContext.Response.SetOverrideErrorFormatter(errorFormatterGenerator.GetErrorFormatter(e));
				}
				httpContext.Response.ReportRuntimeError(e, false, true);
			}
			finally
			{
				if (httpApplication != null)
				{
					httpContext.ApplicationInstance = null;
					HttpApplicationFactory.RecycleApplicationInstance(httpApplication);
				}
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00023E40 File Offset: 0x00022040
		public string MachineName
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
			get
			{
				return HttpServerUtility.GetMachineNameInternal();
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00023E48 File Offset: 0x00022048
		internal static string GetMachineNameInternal()
		{
			if (HttpServerUtility._machineName != null)
			{
				return HttpServerUtility._machineName;
			}
			object machineNameLock = HttpServerUtility._machineNameLock;
			lock (machineNameLock)
			{
				if (HttpServerUtility._machineName != null)
				{
					return HttpServerUtility._machineName;
				}
				StringBuilder stringBuilder = new StringBuilder(256);
				int num = 256;
				if (UnsafeNativeMethods.GetComputerName(stringBuilder, ref num) == 0)
				{
					throw new HttpException(SR.GetString("Get_computer_name_failed"));
				}
				HttpServerUtility._machineName = stringBuilder.ToString();
			}
			return HttpServerUtility._machineName;
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00023EDC File Offset: 0x000220DC
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00023F0C File Offset: 0x0002210C
		public int ScriptTimeout
		{
			get
			{
				if (this._context != null)
				{
					return Convert.ToInt32(this._context.Timeout.TotalSeconds);
				}
				return 110;
			}
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
			set
			{
				if (this._context == null)
				{
					throw new HttpException(SR.GetString("Server_not_available"));
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._context.Timeout = new TimeSpan(0, 0, value);
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00023F48 File Offset: 0x00022148
		public string HtmlDecode(string s)
		{
			return HttpUtility.HtmlDecode(s);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00023F50 File Offset: 0x00022150
		public void HtmlDecode(string s, TextWriter output)
		{
			HttpUtility.HtmlDecode(s, output);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00023F59 File Offset: 0x00022159
		public string HtmlEncode(string s)
		{
			return HttpUtility.HtmlEncode(s);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00023F61 File Offset: 0x00022161
		public void HtmlEncode(string s, TextWriter output)
		{
			HttpUtility.HtmlEncode(s, output);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00023F6C File Offset: 0x0002216C
		public string UrlEncode(string s)
		{
			Encoding e = (this._context != null) ? this._context.Response.ContentEncoding : Encoding.UTF8;
			return HttpUtility.UrlEncode(s, e);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00023FA0 File Offset: 0x000221A0
		public string UrlPathEncode(string s)
		{
			return HttpUtility.UrlPathEncode(s);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00023FA8 File Offset: 0x000221A8
		public void UrlEncode(string s, TextWriter output)
		{
			if (s != null)
			{
				output.Write(this.UrlEncode(s));
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00023FBC File Offset: 0x000221BC
		public string UrlDecode(string s)
		{
			Encoding e = (this._context != null) ? this._context.Request.ContentEncoding : Encoding.UTF8;
			return HttpUtility.UrlDecode(s, e);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00023FF0 File Offset: 0x000221F0
		public void UrlDecode(string s, TextWriter output)
		{
			if (s != null)
			{
				output.Write(this.UrlDecode(s));
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00024002 File Offset: 0x00022202
		public static string UrlTokenEncode(byte[] input)
		{
			return HttpEncoder.Current.UrlTokenEncode(input);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002400F File Offset: 0x0002220F
		public static byte[] UrlTokenDecode(string input)
		{
			return HttpEncoder.Current.UrlTokenDecode(input);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002401C File Offset: 0x0002221C
		internal void EnsureHasNotTransitionedToWebSocket()
		{
			if (this._context != null)
			{
				this._context.EnsureHasNotTransitionedToWebSocket();
			}
		}

		// Token: 0x040004D5 RID: 1237
		private HttpContext _context;

		// Token: 0x040004D6 RID: 1238
		private HttpApplication _application;

		// Token: 0x040004D7 RID: 1239
		private static IDictionary _cultureCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040004D8 RID: 1240
		private static object _machineNameLock = new object();

		// Token: 0x040004D9 RID: 1241
		private static string _machineName;

		// Token: 0x040004DA RID: 1242
		private const int _maxMachineNameLength = 256;
	}
}
