using System;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C8 RID: 2248
	internal class TracingConnection : DelegatingConnection
	{
		// Token: 0x060055D3 RID: 21971 RVA: 0x0013A0ED File Offset: 0x001382ED
		public TracingConnection(IConnection connection, ServiceModelActivity activity) : base(connection)
		{
			this.activity = activity;
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x0013A100 File Offset: 0x00138300
		public TracingConnection(IConnection connection, bool inheritCurrentActivity) : base(connection)
		{
			this.activity = (inheritCurrentActivity ? ServiceModelActivity.CreateActivity(DiagnosticTraceBase.ActivityId, false) : ServiceModelActivity.CreateActivity());
			if (DiagnosticUtility.ShouldUseActivity && !inheritCurrentActivity && FxTrace.Trace != null)
			{
				FxTrace.Trace.TraceTransfer(this.activity.Id);
			}
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x0013A158 File Offset: 0x00138358
		public override void Abort()
		{
			try
			{
				using (ServiceModelActivity.BoundOperation(this.activity))
				{
					base.Abort();
				}
			}
			finally
			{
				if (this.activity != null)
				{
					this.activity.Dispose();
				}
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x060055D6 RID: 21974 RVA: 0x0013A1B4 File Offset: 0x001383B4
		private static WaitCallback Callback
		{
			get
			{
				if (TracingConnection.callback == null)
				{
					TracingConnection.callback = new WaitCallback(TracingConnection.WaitCallback);
				}
				return TracingConnection.callback;
			}
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x0013A1D4 File Offset: 0x001383D4
		public override void Close(TimeSpan timeout, bool asyncAndLinger)
		{
			try
			{
				using (ServiceModelActivity.BoundOperation(this.activity, true))
				{
					base.Close(timeout, asyncAndLinger);
				}
			}
			finally
			{
				if (this.activity != null)
				{
					this.activity.Dispose();
				}
			}
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x0013A234 File Offset: 0x00138434
		public override void Shutdown(TimeSpan timeout)
		{
			using (ServiceModelActivity.BoundOperation(this.activity, true))
			{
				base.Shutdown(timeout);
			}
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x0013A274 File Offset: 0x00138474
		internal void ActivityStart(string name)
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				ServiceModelActivity.Start(this.activity, SR.GetString("ActivityReceiveBytes", new object[]
				{
					name
				}), ActivityType.ReceiveBytes);
			}
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x0013A2CC File Offset: 0x001384CC
		internal void ActivityStart(Uri uri)
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				ServiceModelActivity.Start(this.activity, SR.GetString("ActivityReceiveBytes", new object[]
				{
					uri.ToString()
				}), ActivityType.ReceiveBytes);
			}
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x0013A328 File Offset: 0x00138528
		public override AsyncCompletionResult BeginWrite(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, WaitCallback callback, object state)
		{
			AsyncCompletionResult result;
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				result = base.BeginWrite(buffer, offset, size, immediate, timeout, callback, state);
			}
			return result;
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x0013A370 File Offset: 0x00138570
		public override void EndWrite()
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				base.EndWrite();
			}
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x0013A3AC File Offset: 0x001385AC
		public override void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout)
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				base.Write(buffer, offset, size, immediate, timeout);
			}
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x0013A3F0 File Offset: 0x001385F0
		public override void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, BufferManager bufferManager)
		{
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				base.Write(buffer, offset, size, immediate, timeout, bufferManager);
			}
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x0013A434 File Offset: 0x00138634
		public override int Read(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			int result;
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				result = base.Read(buffer, offset, size, timeout);
			}
			return result;
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x0013A478 File Offset: 0x00138678
		private static void WaitCallback(object state)
		{
			TracingConnection.TracingConnectionState tracingConnectionState = (TracingConnection.TracingConnectionState)state;
			tracingConnectionState.ExecuteCallback();
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x0013A494 File Offset: 0x00138694
		public override AsyncCompletionResult BeginRead(int offset, int size, TimeSpan timeout, WaitCallback callback, object state)
		{
			AsyncCompletionResult result;
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				TracingConnection.TracingConnectionState state2 = new TracingConnection.TracingConnectionState(callback, this.activity, state);
				result = base.BeginRead(offset, size, timeout, TracingConnection.Callback, state2);
			}
			return result;
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x0013A4EC File Offset: 0x001386EC
		public override int EndRead()
		{
			int result = 0;
			try
			{
				if (this.activity != null)
				{
					ExceptionUtility.UseActivityId(this.activity.Id);
				}
				result = base.EndRead();
			}
			finally
			{
				ExceptionUtility.ClearActivityId();
			}
			return result;
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x0013A534 File Offset: 0x00138734
		public override object DuplicateAndClose(int targetProcessId)
		{
			object result;
			using (ServiceModelActivity.BoundOperation(this.activity, true))
			{
				result = base.DuplicateAndClose(targetProcessId);
			}
			return result;
		}

		// Token: 0x0400350B RID: 13579
		private ServiceModelActivity activity;

		// Token: 0x0400350C RID: 13580
		private static WaitCallback callback;

		// Token: 0x02000D88 RID: 3464
		private class TracingConnectionState
		{
			// Token: 0x06007E83 RID: 32387 RVA: 0x001D7AD2 File Offset: 0x001D5CD2
			internal TracingConnectionState(WaitCallback callback, ServiceModelActivity activity, object state)
			{
				this.activity = activity;
				this.callback = callback;
				this.state = state;
			}

			// Token: 0x06007E84 RID: 32388 RVA: 0x001D7AF0 File Offset: 0x001D5CF0
			internal void ExecuteCallback()
			{
				using (ServiceModelActivity.BoundOperation(this.activity))
				{
					this.callback(this.state);
				}
			}

			// Token: 0x04004896 RID: 18582
			private object state;

			// Token: 0x04004897 RID: 18583
			private WaitCallback callback;

			// Token: 0x04004898 RID: 18584
			private ServiceModelActivity activity;
		}
	}
}
