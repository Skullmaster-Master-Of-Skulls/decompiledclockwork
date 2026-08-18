using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Web.SessionState;
using Microsoft.Web.Infrastructure.DynamicValidationHelper;

namespace System.Web.WebPages
{
	// Token: 0x02000096 RID: 150
	public class WebPageHttpHandler : IHttpHandler, IRequiresSessionState
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0000F398 File Offset: 0x0000D598
		public WebPageHttpHandler(WebPage webPage) : this(webPage, new Lazy<WebPageRenderingBase>(() => System.Web.WebPages.StartPage.GetStartPage(webPage, "_PageStart", WebPageHttpHandler.GetRegisteredExtensions())))
		{
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000F3D6 File Offset: 0x0000D5D6
		internal WebPageHttpHandler(WebPage webPage, Lazy<WebPageRenderingBase> startPage)
		{
			if (webPage == null)
			{
				throw new ArgumentNullException("webPage");
			}
			this._webPage = webPage;
			this._startPage = startPage;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000F3FA File Offset: 0x0000D5FA
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0000F401 File Offset: 0x0000D601
		public static bool DisableWebPagesResponseHeader { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000F409 File Offset: 0x0000D609
		public virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000F40C File Offset: 0x0000D60C
		internal WebPage RequestedPage
		{
			get
			{
				return this._webPage;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000F414 File Offset: 0x0000D614
		internal WebPageRenderingBase StartPage
		{
			get
			{
				return this._startPage.Value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000F421 File Offset: 0x0000D621
		internal static string[] SupportedExtensions
		{
			get
			{
				return WebPageHttpHandler._supportedExtensions;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000F428 File Offset: 0x0000D628
		internal static void AddVersionHeader(HttpContextBase httpContext)
		{
			if (!WebPageHttpHandler.DisableWebPagesResponseHeader)
			{
				httpContext.Response.AppendHeader(WebPageHttpHandler.WebPagesVersionHeaderName, WebPageHttpHandler.WebPagesVersion);
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000F446 File Offset: 0x0000D646
		public static IHttpHandler CreateFromVirtualPath(string virtualPath)
		{
			return WebPageHttpHandler.CreateFromVirtualPath(virtualPath, VirtualPathFactoryManager.Instance);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000F454 File Offset: 0x0000D654
		internal static IHttpHandler CreateFromVirtualPath(string virtualPath, IVirtualPathFactory virtualPathFactory)
		{
			WebPage webPage = virtualPathFactory.CreateInstance(virtualPath);
			if (webPage == null)
			{
				return virtualPathFactory.CreateInstance(virtualPath);
			}
			webPage.TopLevelPage = true;
			webPage.VirtualPath = virtualPath;
			webPage.VirtualPathFactory = virtualPathFactory;
			return new WebPageHttpHandler(webPage);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000F48F File Offset: 0x0000D68F
		public static ReadOnlyCollection<string> GetRegisteredExtensions()
		{
			return new ReadOnlyCollection<string>(WebPageHttpHandler._supportedExtensions);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000F49B File Offset: 0x0000D69B
		private static string GetVersionString()
		{
			return new AssemblyName(typeof(WebPageHttpHandler).Assembly.FullName).Version.ToString(2);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000F4C1 File Offset: 0x0000D6C1
		private static bool HandleError(Exception e)
		{
			if (e is SecurityException)
			{
				return false;
			}
			throw new HttpUnhandledException(null, e);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		internal static void GenerateSourceFilesHeader(WebPageContext context)
		{
			if (context.SourceFiles.Any<string>())
			{
				string s = string.Join("|", context.SourceFiles);
				string value = "=?UTF-8?B?" + Convert.ToBase64String(Encoding.UTF8.GetBytes(s)) + "?=";
				context.HttpContext.Response.AddHeader("X-SourceFiles", value);
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000F535 File Offset: 0x0000D735
		public virtual void ProcessRequest(HttpContext context)
		{
			this.ProcessRequestInternal(context);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000F540 File Offset: 0x0000D740
		internal void ProcessRequestInternal(HttpContext context)
		{
			ValidationUtility.EnableDynamicValidation(context);
			context.Request.ValidateInput();
			HttpContextBase httpContext = new HttpContextWrapper(context);
			this.ProcessRequestInternal(httpContext);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000F56C File Offset: 0x0000D76C
		internal void ProcessRequestInternal(HttpContextBase httpContext)
		{
			try
			{
				WebPageHttpHandler.AddVersionHeader(httpContext);
				this._webPage.ExecutePageHierarchy(new WebPageContext
				{
					HttpContext = httpContext
				}, httpContext.Response.Output, this.StartPage);
				if (WebPageHttpHandler.ShouldGenerateSourceHeader(httpContext))
				{
					WebPageHttpHandler.GenerateSourceFilesHeader(this._webPage.PageContext);
				}
			}
			catch (Exception e)
			{
				if (!WebPageHttpHandler.HandleError(e))
				{
					throw;
				}
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public static void RegisterExtension(string extension)
		{
			WebPageHttpHandler._supportedExtensions = WebPageHttpHandler._supportedExtensions.AppendAndReallocate(extension);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000F5F2 File Offset: 0x0000D7F2
		internal static bool ShouldGenerateSourceHeader(HttpContextBase context)
		{
			return context.Request.IsLocal;
		}

		// Token: 0x0400014E RID: 334
		internal const string StartPageFileName = "_PageStart";

		// Token: 0x0400014F RID: 335
		public static readonly string WebPagesVersionHeaderName = "X-AspNetWebPages-Version";

		// Token: 0x04000150 RID: 336
		private static string[] _supportedExtensions = Empty<string>.Array;

		// Token: 0x04000151 RID: 337
		internal static readonly string WebPagesVersion = WebPageHttpHandler.GetVersionString();

		// Token: 0x04000152 RID: 338
		private readonly WebPage _webPage;

		// Token: 0x04000153 RID: 339
		private readonly Lazy<WebPageRenderingBase> _startPage;
	}
}
