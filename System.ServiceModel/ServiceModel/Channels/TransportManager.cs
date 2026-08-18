using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200079E RID: 1950
	internal abstract class TransportManager
	{
		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x060049BF RID: 18879 RVA: 0x0010ED81 File Offset: 0x0010CF81
		protected ServiceModelActivity Activity
		{
			get
			{
				return this.activity;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x060049C0 RID: 18880
		internal abstract string Scheme { get; }

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x060049C1 RID: 18881 RVA: 0x0010ED89 File Offset: 0x0010CF89
		internal object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0010ED91 File Offset: 0x0010CF91
		internal void Close(TransportChannelListener channelListener, TimeSpan timeout)
		{
			this.Cleanup(channelListener, timeout, false);
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x0010ED9C File Offset: 0x0010CF9C
		private void Cleanup(TransportChannelListener channelListener, TimeSpan timeout, bool aborting)
		{
			using (ServiceModelActivity.BoundOperation(this.Activity))
			{
				this.Unregister(channelListener);
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.openCount <= 0)
				{
					throw Fx.AssertAndThrow("Invalid Open/Close state machine.");
				}
				this.openCount--;
				if (this.openCount == 0)
				{
					using (ServiceModelActivity.BoundOperation(this.Activity, true))
					{
						if (aborting)
						{
							this.OnAbort();
						}
						else
						{
							this.OnClose(timeout);
						}
					}
					if (this.Activity != null)
					{
						this.Activity.Dispose();
					}
				}
			}
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x0010EE78 File Offset: 0x0010D078
		internal static void EnsureRegistered<TChannelListener>(UriPrefixTable<TChannelListener> addressTable, TChannelListener channelListener, HostNameComparisonMode registeredComparisonMode) where TChannelListener : TransportChannelListener
		{
			TChannelListener tchannelListener;
			if (!addressTable.TryLookupUri(channelListener.Uri, registeredComparisonMode, out tchannelListener) || tchannelListener != channelListener)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ListenerFactoryNotRegistered", new object[]
				{
					channelListener.Uri
				})));
			}
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0010EED8 File Offset: 0x0010D0D8
		protected void Fault<TChannelListener>(UriPrefixTable<TChannelListener> addressTable, Exception exception) where TChannelListener : ChannelListenerBase
		{
			foreach (KeyValuePair<BaseUriWithWildcard, TChannelListener> keyValuePair in addressTable.GetAll())
			{
				TChannelListener value = keyValuePair.Value;
				value.Fault(exception);
				value.Abort();
			}
		}

		// Token: 0x060049C6 RID: 18886
		internal abstract void OnClose(TimeSpan timeout);

		// Token: 0x060049C7 RID: 18887
		internal abstract void OnOpen();

		// Token: 0x060049C8 RID: 18888 RVA: 0x0010EF40 File Offset: 0x0010D140
		internal virtual void OnAbort()
		{
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x0010EF44 File Offset: 0x0010D144
		internal void Open(TransportChannelListener channelListener)
		{
			if (DiagnosticUtility.ShouldUseActivity)
			{
				if (this.activity == null)
				{
					this.activity = ServiceModelActivity.CreateActivity(true);
					if (DiagnosticUtility.ShouldUseActivity)
					{
						if (FxTrace.Trace != null)
						{
							FxTrace.Trace.TraceTransfer(this.Activity.Id);
						}
						ServiceModelActivity.Start(this.Activity, SR.GetString("ActivityListenAt", new object[]
						{
							channelListener.Uri.ToString()
						}), ActivityType.ListenAt);
					}
				}
				channelListener.Activity = this.Activity;
			}
			using (ServiceModelActivity.BoundOperation(this.Activity))
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 524342, SR.GetString("TraceCodeTransportListen", new object[]
					{
						channelListener.Uri.ToString()
					}), this);
				}
				this.Register(channelListener);
				try
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.openCount == 0)
						{
							this.OnOpen();
						}
						this.openCount++;
					}
				}
				catch
				{
					this.Unregister(channelListener);
					throw;
				}
			}
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0010F080 File Offset: 0x0010D280
		internal void Abort(TransportChannelListener channelListener)
		{
			this.Cleanup(channelListener, TimeSpan.Zero, true);
		}

		// Token: 0x060049CB RID: 18891
		internal abstract void Register(TransportChannelListener channelListener);

		// Token: 0x060049CC RID: 18892 RVA: 0x0010F08F File Offset: 0x0010D28F
		protected void ThrowIfOpen()
		{
			if (this.openCount > 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransportManagerOpen")));
			}
		}

		// Token: 0x060049CD RID: 18893
		internal abstract void Unregister(TransportChannelListener channelListener);

		// Token: 0x04002ED1 RID: 11985
		private ServiceModelActivity activity;

		// Token: 0x04002ED2 RID: 11986
		private int openCount;

		// Token: 0x04002ED3 RID: 11987
		private object thisLock = new object();
	}
}
