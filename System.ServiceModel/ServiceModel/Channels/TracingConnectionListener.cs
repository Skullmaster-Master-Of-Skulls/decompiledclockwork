using System;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008CA RID: 2250
	internal class TracingConnectionListener : IConnectionListener, IDisposable
	{
		// Token: 0x060055E8 RID: 21992 RVA: 0x0013A6A4 File Offset: 0x001388A4
		internal TracingConnectionListener(IConnectionListener listener, string traceStartInfo) : this(listener, traceStartInfo, true)
		{
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x0013A6AF File Offset: 0x001388AF
		internal TracingConnectionListener(IConnectionListener listener, Uri uri) : this(listener, uri.ToString())
		{
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x0013A6BE File Offset: 0x001388BE
		internal TracingConnectionListener(IConnectionListener listener)
		{
			this.listener = listener;
			this.activity = ServiceModelActivity.CreateActivity(DiagnosticTraceBase.ActivityId, false);
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x0013A6E0 File Offset: 0x001388E0
		internal TracingConnectionListener(IConnectionListener listener, string traceStartInfo, bool newActivity)
		{
			this.listener = listener;
			if (newActivity)
			{
				this.activity = ServiceModelActivity.CreateActivity();
				if (DiagnosticUtility.ShouldUseActivity)
				{
					if (FxTrace.Trace != null)
					{
						FxTrace.Trace.TraceTransfer(this.activity.Id);
					}
					ServiceModelActivity.Start(this.activity, SR.GetString("ActivityListenAt", new object[]
					{
						traceStartInfo
					}), ActivityType.ListenAt);
					return;
				}
			}
			else
			{
				this.activity = ServiceModelActivity.CreateActivity(DiagnosticTraceBase.ActivityId, false);
				if (this.activity != null)
				{
					this.activity.Name = traceStartInfo;
				}
			}
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x0013A774 File Offset: 0x00138974
		public void Listen()
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				this.listener.Listen();
			}
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x0013A7B4 File Offset: 0x001389B4
		public IAsyncResult BeginAccept(AsyncCallback callback, object state)
		{
			IAsyncResult result;
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				result = this.listener.BeginAccept(callback, state);
			}
			return result;
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x0013A7F8 File Offset: 0x001389F8
		public IConnection EndAccept(IAsyncResult result)
		{
			IConnection result2;
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateActivity();
				if (serviceModelActivity != null && FxTrace.Trace != null)
				{
					FxTrace.Trace.TraceTransfer(serviceModelActivity.Id);
				}
				using (ServiceModelActivity.BoundOperation(serviceModelActivity))
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityReceiveBytes", new object[]
					{
						this.activity.Name
					}), ActivityType.ReceiveBytes);
					IConnection connection = this.listener.EndAccept(result);
					if (connection == null)
					{
						result2 = null;
					}
					else
					{
						TracingConnection tracingConnection = new TracingConnection(connection, serviceModelActivity);
						result2 = tracingConnection;
					}
				}
			}
			return result2;
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x0013A8B4 File Offset: 0x00138AB4
		public void Dispose()
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				this.listener.Dispose();
				this.activity.Dispose();
			}
		}

		// Token: 0x04003511 RID: 13585
		private ServiceModelActivity activity;

		// Token: 0x04003512 RID: 13586
		private IConnectionListener listener;
	}
}
