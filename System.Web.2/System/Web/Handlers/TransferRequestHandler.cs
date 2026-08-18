using System;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace System.Web.Handlers
{
	// Token: 0x020001A7 RID: 423
	internal class TransferRequestHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x0600163B RID: 5691 RVA: 0x00046484 File Offset: 0x00044684
		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			return TaskAsyncHelper.BeginTask(() => this.ProcessRequestAsync(context), cb, extraData);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0000778C File Offset: 0x0000598C
		public void EndProcessRequest(IAsyncResult result)
		{
			TaskAsyncHelper.EndTask(result);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000464B8 File Offset: 0x000446B8
		private Task ProcessRequestAsync(HttpContext context)
		{
			IIS7WorkerRequest iis7WorkerRequest = context.WorkerRequest as IIS7WorkerRequest;
			if (iis7WorkerRequest == null)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			iis7WorkerRequest.ScheduleExecuteUrl(null, null, null, true, context.Request.EntityBody, null, false);
			Task task = context.ApplicationInstance.EnsureReleaseStateAsync();
			if (task.IsCompleted)
			{
				context.ApplicationInstance.CompleteRequest();
				return TaskAsyncHelper.CompletedTask;
			}
			return task.ContinueWith(delegate(Task _)
			{
				context.ApplicationInstance.CompleteRequest();
			});
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00046554 File Offset: 0x00044754
		public void ProcessRequest(HttpContext context)
		{
			string @string = SR.GetString("HttpTaskAsyncHandler_CannotExecuteSynchronously", new object[]
			{
				base.GetType()
			});
			throw new NotSupportedException(@string);
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
