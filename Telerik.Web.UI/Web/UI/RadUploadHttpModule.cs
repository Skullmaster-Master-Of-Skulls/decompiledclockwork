using System;
using System.Reflection;
using System.Web;
using Telerik.Web.UI.AsyncUpload;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI
{
	// Token: 0x02001343 RID: 4931
	public class RadUploadHttpModule : IHttpModule
	{
		// Token: 0x0600CD87 RID: 52615 RVA: 0x002DC01C File Offset: 0x002DA21C
		public void Dispose()
		{
		}

		// Token: 0x0600CD88 RID: 52616 RVA: 0x002DC020 File Offset: 0x002DA220
		public virtual void Init(HttpApplication app)
		{
			AspNetHostingPermissionLevel currentTrustLevel = SecurityHelper.GetCurrentTrustLevel();
			if (currentTrustLevel == AspNetHostingPermissionLevel.High || currentTrustLevel == AspNetHostingPermissionLevel.Unrestricted)
			{
				app.PreRequestHandlerExecute += this.CaptureWorkerRequest;
				app.PostRequestHandlerExecute += this.ReleaseWorkerRequest;
				app.Error += this.ReleaseWorkerRequest;
			}
		}

		// Token: 0x0600CD89 RID: 52617 RVA: 0x002DC07C File Offset: 0x002DA27C
		protected virtual void CaptureWorkerRequest(object sender, EventArgs e)
		{
			this._application = (sender as HttpApplication);
			this.Context = this.Application.Context;
			if (!this.IsUploadRequest(this.Application))
			{
				return;
			}
			FieldInfo workerRequestField = this.GetWorkerRequestField();
			if (workerRequestField == null || !RadAjaxControl.HasReflectionPermission())
			{
				return;
			}
			HttpWorkerRequest httpWorkerRequest = workerRequestField.GetValue(this.Context.Request) as HttpWorkerRequest;
			if (httpWorkerRequest == null)
			{
				return;
			}
			ProgressWorkerRequest progressWorker = this.GetProgressWorker(httpWorkerRequest);
			this.UpdateUploadContext(progressWorker);
			workerRequestField.SetValue(this.Context.Request, progressWorker);
		}

		// Token: 0x0600CD8A RID: 52618 RVA: 0x002DC109 File Offset: 0x002DA309
		private ProgressWorkerRequest GetProgressWorker(HttpWorkerRequest workerRequest)
		{
			if (this.IsAsyncUploadRequest)
			{
				return new AsyncProgressWorkerRequest(workerRequest, this.Context.Request);
			}
			return new ProgressWorkerRequest(workerRequest, this.Context.Request);
		}

		// Token: 0x1700420B RID: 16907
		// (get) Token: 0x0600CD8B RID: 52619 RVA: 0x002DC136 File Offset: 0x002DA336
		private bool IsAsyncUploadRequest
		{
			get
			{
				return this.Application.Request.QueryString[HandlerRouter.HandlerUrlKey] == RadAsyncUpload.HandlerRouterKey;
			}
		}

		// Token: 0x0600CD8C RID: 52620 RVA: 0x002DC15C File Offset: 0x002DA35C
		private void UpdateUploadContext(ProgressWorkerRequest progressWorker)
		{
			if (RadUploadContext.GetCurrent(this.Context) == null)
			{
				RadUploadContext.SetUploadContext(this.Context, this.CreateContext(progressWorker));
				return;
			}
			if (this.IsAsyncUploadRequest)
			{
				RadAsyncUploadContext radAsyncUploadContext = RadUploadContext.Current as RadAsyncUploadContext;
				if (radAsyncUploadContext == null)
				{
					return;
				}
				radAsyncUploadContext.RequestLength += this.Context.Request.ContentLength;
				radAsyncUploadContext.UploadsInProgress++;
			}
		}

		// Token: 0x0600CD8D RID: 52621 RVA: 0x002DC1CC File Offset: 0x002DA3CC
		private RadUploadContext CreateContext(ProgressWorkerRequest progressWorker)
		{
			if (this.IsAsyncUploadRequest)
			{
				RadAsyncUploadContext radAsyncUploadContext = new RadAsyncUploadContext(this.Context.Request.ContentLength, progressWorker.RequestStateStore);
				radAsyncUploadContext.UploadsInProgress++;
				return radAsyncUploadContext;
			}
			return new RadUploadContext(this.Context.Request.ContentLength, progressWorker.RequestStateStore);
		}

		// Token: 0x0600CD8E RID: 52622 RVA: 0x002DC228 File Offset: 0x002DA428
		protected virtual void ReleaseWorkerRequest(object sender, EventArgs e)
		{
			if (this.Application == null)
			{
				this._application = (sender as HttpApplication);
			}
			if (!this.IsUploadRequest(this.Application))
			{
				return;
			}
			FieldInfo workerRequestField = this.GetWorkerRequestField();
			this.ReleaseContexts();
			if (workerRequestField == null || !RadAjaxControl.HasReflectionPermission())
			{
				return;
			}
			ProgressWorkerRequest progressWorkerRequest = workerRequestField.GetValue(this.Context.Request) as ProgressWorkerRequest;
			if (progressWorkerRequest != null)
			{
				workerRequestField.SetValue(this.Context.Request, progressWorkerRequest._originalWorkerRequest);
			}
			this.Context = null;
		}

		// Token: 0x0600CD8F RID: 52623 RVA: 0x002DC2B0 File Offset: 0x002DA4B0
		private void ReleaseContexts()
		{
			if (this.IsAsyncUploadRequest)
			{
				RadAsyncUploadContext radAsyncUploadContext = RadUploadContext.Current as RadAsyncUploadContext;
				if (radAsyncUploadContext == null)
				{
					return;
				}
				radAsyncUploadContext.UploadsInProgress--;
				if (radAsyncUploadContext.UploadsInProgress > 0)
				{
					return;
				}
			}
			RadProgressContext.RemoveProgressContext(this.Context);
			RadUploadContext.RemoveUploadContext(this.Context);
		}

		// Token: 0x0600CD90 RID: 52624 RVA: 0x002DC304 File Offset: 0x002DA504
		private FieldInfo GetWorkerRequestField()
		{
			FieldInfo field = this.Context.Request.GetType().GetField("_wr", BindingFlags.Instance | BindingFlags.NonPublic);
			if (field == null)
			{
				field = this.Context.Request.GetType().GetField("worker_request", BindingFlags.Instance | BindingFlags.NonPublic);
			}
			return field;
		}

		// Token: 0x1700420C RID: 16908
		// (get) Token: 0x0600CD91 RID: 52625 RVA: 0x002DC358 File Offset: 0x002DA558
		public static bool IsRegistered
		{
			get
			{
				if (!SecurityHelper.IsPermissionGranted(new AspNetHostingPermission(AspNetHostingPermissionLevel.High)))
				{
					return true;
				}
				HttpModuleCollection modules = HttpContext.Current.ApplicationInstance.Modules;
				foreach (string name in modules.AllKeys)
				{
					if (modules[name] is RadUploadHttpModule)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700420D RID: 16909
		// (get) Token: 0x0600CD92 RID: 52626 RVA: 0x002DC3BB File Offset: 0x002DA5BB
		// (set) Token: 0x0600CD93 RID: 52627 RVA: 0x002DC3C3 File Offset: 0x002DA5C3
		public HttpContext Context { get; set; }

		// Token: 0x1700420E RID: 16910
		// (get) Token: 0x0600CD94 RID: 52628 RVA: 0x002DC3CC File Offset: 0x002DA5CC
		private HttpApplication Application
		{
			get
			{
				return this._application;
			}
		}

		// Token: 0x0600CD95 RID: 52629 RVA: 0x002DC3D4 File Offset: 0x002DA5D4
		private bool IsUploadRequest(HttpApplication application)
		{
			return application.Request != null && application.Request.ContentType != null && application.Request.ContentType.ToLower().StartsWith("multipart/form-data");
		}

		// Token: 0x040036EB RID: 14059
		private HttpApplication _application;
	}
}
