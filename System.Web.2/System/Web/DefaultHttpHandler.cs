using System;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000056 RID: 86
	public class DefaultHttpHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00007F06 File Offset: 0x00006106
		protected HttpContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00007F0E File Offset: 0x0000610E
		protected NameValueCollection ExecuteUrlHeaders
		{
			get
			{
				if (this._executeUrlHeaders == null && this._context != null)
				{
					this._executeUrlHeaders = new NameValueCollection(this._context.Request.Headers);
				}
				return this._executeUrlHeaders;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void OnExecuteUrlPreconditionFailure()
		{
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string OverrideExecuteUrlPath()
		{
			return null;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00007F41 File Offset: 0x00006141
		internal static bool IsClassicAspRequest(string filePath)
		{
			return StringUtil.StringEndsWithIgnoreCase(filePath, ".asp");
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00007F4E File Offset: 0x0000614E
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static string MapPathWithAssert(HttpContext context, string virtualPath)
		{
			return context.Request.MapPath(virtualPath);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00007F5C File Offset: 0x0000615C
		public virtual IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object state)
		{
			if (HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Method_Not_Supported_By_Iis_Integrated_Mode", new object[]
				{
					"DefaultHttpHandler.BeginProcessRequest"
				}));
			}
			this._context = context;
			HttpResponse response = this._context.Response;
			if (response.CanExecuteUrlForEntireResponse)
			{
				string text = this.OverrideExecuteUrlPath();
				if (text != null && !HttpRuntime.IsFullTrust && !base.GetType().Assembly.GlobalAssemblyCache)
				{
					HttpRuntime.CheckFilePermission(DefaultHttpHandler.MapPathWithAssert(context, text));
				}
				return response.BeginExecuteUrlForEntireResponse(text, this._executeUrlHeaders, callback, state);
			}
			this.OnExecuteUrlPreconditionFailure();
			this._context = null;
			HttpRequest request = context.Request;
			if (request.HttpVerb == HttpVerb.POST)
			{
				throw new HttpException(405, SR.GetString("Method_not_allowed", new object[]
				{
					request.HttpMethod,
					request.Path
				}));
			}
			if (DefaultHttpHandler.IsClassicAspRequest(request.FilePath))
			{
				throw new HttpException(403, SR.GetString("Path_forbidden", new object[]
				{
					request.Path
				}));
			}
			StaticFileHandler.ProcessRequestInternal(context, this.OverrideExecuteUrlPath());
			return new HttpAsyncResult(callback, state, true, null, null);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000807C File Offset: 0x0000627C
		public virtual void EndProcessRequest(IAsyncResult result)
		{
			if (this._context != null)
			{
				HttpResponse response = this._context.Response;
				this._context = null;
				response.EndExecuteUrlForEntireResponse(result);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000080AB File Offset: 0x000062AB
		public virtual void ProcessRequest(HttpContext context)
		{
			throw new InvalidOperationException(SR.GetString("Cannot_call_defaulthttphandler_sync"));
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000162 RID: 354
		private HttpContext _context;

		// Token: 0x04000163 RID: 355
		private NameValueCollection _executeUrlHeaders;
	}
}
