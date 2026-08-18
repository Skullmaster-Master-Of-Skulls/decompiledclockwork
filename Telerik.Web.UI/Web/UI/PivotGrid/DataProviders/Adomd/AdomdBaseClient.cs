using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D53 RID: 3411
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Will fix soon.")]
	internal abstract class AdomdBaseClient : IAdomdClient
	{
		// Token: 0x14000136 RID: 310
		// (add) Token: 0x06007F24 RID: 32548 RVA: 0x001D11D0 File Offset: 0x001CF3D0
		// (remove) Token: 0x06007F25 RID: 32549 RVA: 0x001D1208 File Offset: 0x001CF408
		public event EventHandler<AdomdClientRequestCompletedEventArgs> SendRequestCompleted;

		// Token: 0x06007F26 RID: 32550 RVA: 0x001D123D File Offset: 0x001CF43D
		private void EnterCriticalRegion()
		{
			Monitor.Enter(this.locker);
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x001D124A File Offset: 0x001CF44A
		private void ExitCriticalRegion()
		{
			Monitor.Exit(this.locker);
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x001D1257 File Offset: 0x001CF457
		public void SendRequestAsync(AdomdClientRequestInfo requestInfo)
		{
			if (this.RequestIsBeingProcessed(requestInfo))
			{
				return;
			}
			this.BeginNewRequest(requestInfo);
		}

		// Token: 0x06007F29 RID: 32553 RVA: 0x001D126A File Offset: 0x001CF46A
		private void BeginNewRequest(AdomdClientRequestInfo requestInfo)
		{
			this.InitializeLocalState(requestInfo);
			this.BeginNewRequestCore(this.currentRequest);
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x001D127F File Offset: 0x001CF47F
		private void InitializeLocalState(AdomdClientRequestInfo request)
		{
			this.EnterCriticalRegion();
			this.currentRequest = request;
			this.InitializeLocalStateCore(request);
			this.ExitCriticalRegion();
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x001D129B File Offset: 0x001CF49B
		protected virtual void InitializeLocalStateCore(AdomdClientRequestInfo request)
		{
		}

		// Token: 0x06007F2C RID: 32556
		protected abstract void BeginNewRequestCore(AdomdClientRequestInfo requestInfo);

		// Token: 0x06007F2D RID: 32557 RVA: 0x001D12A0 File Offset: 0x001CF4A0
		protected void HandleRequestError(AdomdClientRequestInfo request, OlapCommunicationException error)
		{
			if (this.RequestIsBeingProcessed(request))
			{
				AdomdClientRequestCompletedEventArgs e = new AdomdClientRequestCompletedEventArgs(null, request, error);
				this.OnSendRequestCompleted(e);
			}
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x001D12C6 File Offset: 0x001CF4C6
		private void ClearLocalStateState()
		{
			this.EnterCriticalRegion();
			this.currentRequest = null;
			this.ExitCriticalRegion();
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x001D12DC File Offset: 0x001CF4DC
		protected void OnSendRequestCompleted(AdomdClientRequestCompletedEventArgs e)
		{
			bool flag = this.RequestIsBeingProcessed(e.RequestInfo);
			if (this.SendRequestCompleted != null && flag)
			{
				this.SendRequestCompleted(this, e);
				this.ClearLocalStateState();
			}
		}

		// Token: 0x06007F30 RID: 32560 RVA: 0x001D1314 File Offset: 0x001CF514
		private bool RequestIsBeingProcessed(AdomdClientRequestInfo newRequest)
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

		// Token: 0x06007F31 RID: 32561 RVA: 0x001D1348 File Offset: 0x001CF548
		protected AdomdClientRequestInfo GetCurrentRequest()
		{
			this.EnterCriticalRegion();
			AdomdClientRequestInfo result = this.currentRequest;
			this.ExitCriticalRegion();
			return result;
		}

		// Token: 0x04002301 RID: 8961
		private readonly object locker = new object();

		// Token: 0x04002302 RID: 8962
		private AdomdClientRequestInfo currentRequest;
	}
}
