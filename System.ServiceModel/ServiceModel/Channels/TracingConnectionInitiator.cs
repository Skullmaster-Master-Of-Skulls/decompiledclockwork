using System;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C9 RID: 2249
	internal class TracingConnectionInitiator : IConnectionInitiator
	{
		// Token: 0x060055E4 RID: 21988 RVA: 0x0013A574 File Offset: 0x00138774
		internal TracingConnectionInitiator(IConnectionInitiator connectionInitiator, bool isClient)
		{
			this.connectionInitiator = connectionInitiator;
			this.activity = ServiceModelActivity.CreateActivity(DiagnosticTraceBase.ActivityId);
			this.isClient = isClient;
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x0013A59C File Offset: 0x0013879C
		public IConnection Connect(Uri uri, TimeSpan timeout)
		{
			IConnection result;
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				IConnection connection = this.connectionInitiator.Connect(uri, timeout);
				if (!this.isClient)
				{
					TracingConnection tracingConnection = new TracingConnection(connection, false);
					tracingConnection.ActivityStart(uri);
					connection = tracingConnection;
				}
				result = connection;
			}
			return result;
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x0013A5FC File Offset: 0x001387FC
		public IAsyncResult BeginConnect(Uri uri, TimeSpan timeout, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				this.connectedUri = uri;
				result = this.connectionInitiator.BeginConnect(uri, timeout, callback, state);
			}
			return result;
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x0013A64C File Offset: 0x0013884C
		public IConnection EndConnect(IAsyncResult result)
		{
			IConnection result2;
			using (ServiceModelActivity.BoundOperation(this.activity))
			{
				TracingConnection tracingConnection = new TracingConnection(this.connectionInitiator.EndConnect(result), false);
				tracingConnection.ActivityStart(this.connectedUri);
				result2 = tracingConnection;
			}
			return result2;
		}

		// Token: 0x0400350D RID: 13581
		private IConnectionInitiator connectionInitiator;

		// Token: 0x0400350E RID: 13582
		private ServiceModelActivity activity;

		// Token: 0x0400350F RID: 13583
		private Uri connectedUri;

		// Token: 0x04003510 RID: 13584
		private bool isClient;
	}
}
