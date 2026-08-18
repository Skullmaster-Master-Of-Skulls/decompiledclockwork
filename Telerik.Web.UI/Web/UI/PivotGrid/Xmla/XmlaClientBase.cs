using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D95 RID: 3477
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Will fix soon.")]
	internal abstract class XmlaClientBase : IXmlaClient
	{
		// Token: 0x14000138 RID: 312
		// (add) Token: 0x0600813E RID: 33086 RVA: 0x001D8114 File Offset: 0x001D6314
		// (remove) Token: 0x0600813F RID: 33087 RVA: 0x001D814C File Offset: 0x001D634C
		public event EventHandler<XmlaClientRequestCompletedEventArgs> SendRequestCompleted;

		// Token: 0x170028FA RID: 10490
		// (get) Token: 0x06008140 RID: 33088 RVA: 0x001D8181 File Offset: 0x001D6381
		public virtual int MaximumRetriesCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170028FB RID: 10491
		// (get) Token: 0x06008141 RID: 33089 RVA: 0x001D8184 File Offset: 0x001D6384
		public virtual int RetryDelayInMilliseconds
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x06008142 RID: 33090 RVA: 0x001D818B File Offset: 0x001D638B
		private void EnterCriticalRegion()
		{
			Monitor.Enter(this.locker);
		}

		// Token: 0x06008143 RID: 33091 RVA: 0x001D8198 File Offset: 0x001D6398
		private void ExitCriticalRegion()
		{
			Monitor.Exit(this.locker);
		}

		// Token: 0x06008144 RID: 33092 RVA: 0x001D81A5 File Offset: 0x001D63A5
		public void SendRequestAsync(XmlaClientRequestInfo requestInfo)
		{
			if (this.RequestIsBeingProcessed(requestInfo))
			{
				return;
			}
			this.BeginNewRequest(requestInfo);
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x001D81B8 File Offset: 0x001D63B8
		private void BeginNewRequest(XmlaClientRequestInfo requestInfo)
		{
			this.InitializeLocalState(requestInfo);
			this.BeginNewRequestCore(this.currentRequest);
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x001D81CD File Offset: 0x001D63CD
		private void InitializeLocalState(XmlaClientRequestInfo request)
		{
			this.EnterCriticalRegion();
			this.currentRequestRetries = 0;
			this.currentRequest = request;
			this.InitializeLocalStateCore(request);
			this.ExitCriticalRegion();
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x001D81F0 File Offset: 0x001D63F0
		protected virtual void InitializeLocalStateCore(XmlaClientRequestInfo request)
		{
		}

		// Token: 0x06008148 RID: 33096
		protected abstract void BeginNewRequestCore(XmlaClientRequestInfo requestInfo);

		// Token: 0x06008149 RID: 33097 RVA: 0x001D81F2 File Offset: 0x001D63F2
		private bool RetriesLimitReached()
		{
			return this.currentRequestRetries >= this.MaximumRetriesCount;
		}

		// Token: 0x0600814A RID: 33098 RVA: 0x001D8208 File Offset: 0x001D6408
		protected void HandleRequestError(XmlaClientRequestInfo request, OlapCommunicationException error)
		{
			bool flag = !this.RetriesLimitReached() && this.RequestIsBeingProcessed(request);
			if (flag)
			{
				this.currentRequestRetries++;
				this.BeginNewRequestCore(request);
				return;
			}
			XmlaClientRequestCompletedEventArgs e = new XmlaClientRequestCompletedEventArgs(string.Empty, request, error);
			this.OnSendRequestCompleted(e);
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x001D8255 File Offset: 0x001D6455
		private void ClearLocalStateState()
		{
			this.EnterCriticalRegion();
			this.currentRequestRetries = 0;
			this.currentRequest = null;
			this.ExitCriticalRegion();
		}

		// Token: 0x0600814C RID: 33100 RVA: 0x001D8274 File Offset: 0x001D6474
		protected void OnSendRequestCompleted(XmlaClientRequestCompletedEventArgs e)
		{
			bool flag = this.RequestIsBeingProcessed(e.RequestInfo);
			if (this.SendRequestCompleted != null && flag)
			{
				this.SendRequestCompleted(this, e);
				this.ClearLocalStateState();
			}
		}

		// Token: 0x0600814D RID: 33101 RVA: 0x001D82AC File Offset: 0x001D64AC
		private bool RequestIsBeingProcessed(XmlaClientRequestInfo newRequest)
		{
			this.EnterCriticalRegion();
			bool result = false;
			if (this.currentRequest != null)
			{
				result = this.currentRequest.Equals(newRequest);
			}
			this.ExitCriticalRegion();
			return result;
		}

		// Token: 0x040023AB RID: 9131
		private readonly object locker = new object();

		// Token: 0x040023AC RID: 9132
		private XmlaClientRequestInfo currentRequest;

		// Token: 0x040023AD RID: 9133
		private int currentRequestRetries;
	}
}
