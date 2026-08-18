using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000700 RID: 1792
	internal class ChannelTracker<TChannel, TState> : CommunicationObject where TChannel : IChannel where TState : class
	{
		// Token: 0x06004498 RID: 17560 RVA: 0x001027FA File Offset: 0x001009FA
		public ChannelTracker()
		{
			this.receivers = new Dictionary<TChannel, TState>();
			this.onInnerChannelClosed = new EventHandler(this.OnInnerChannelClosed);
			this.onInnerChannelFaulted = new EventHandler(this.OnInnerChannelFaulted);
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x00102834 File Offset: 0x00100A34
		public void Add(TChannel channel, TState channelReceiver)
		{
			bool flag = false;
			Dictionary<TChannel, TState> obj = this.receivers;
			lock (obj)
			{
				if (base.State != CommunicationState.Opened)
				{
					flag = true;
				}
				else
				{
					this.receivers.Add(channel, channelReceiver);
				}
			}
			if (flag)
			{
				channel.Abort();
			}
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x0010289C File Offset: 0x00100A9C
		public void PrepareChannel(TChannel channel)
		{
			channel.Faulted += this.onInnerChannelFaulted;
			channel.Closed += this.onInnerChannelClosed;
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x001028C4 File Offset: 0x00100AC4
		private void OnInnerChannelFaulted(object sender, EventArgs e)
		{
			TChannel tchannel = (TChannel)((object)sender);
			tchannel.Abort();
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x001028E8 File Offset: 0x00100AE8
		private void OnInnerChannelClosed(object sender, EventArgs e)
		{
			TChannel channel = (TChannel)((object)sender);
			this.Remove(channel);
			channel.Faulted -= this.onInnerChannelFaulted;
			channel.Closed -= this.onInnerChannelClosed;
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x0010292C File Offset: 0x00100B2C
		public bool Remove(TChannel channel)
		{
			Dictionary<TChannel, TState> obj = this.receivers;
			bool result;
			lock (obj)
			{
				result = this.receivers.Remove(channel);
			}
			return result;
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x00102974 File Offset: 0x00100B74
		private TChannel[] GetChannels()
		{
			Dictionary<TChannel, TState> obj = this.receivers;
			TChannel[] result;
			lock (obj)
			{
				TChannel[] array = new TChannel[this.receivers.Keys.Count];
				this.receivers.Keys.CopyTo(array, 0);
				this.receivers.Clear();
				result = array;
			}
			return result;
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x001029E4 File Offset: 0x00100BE4
		protected override void OnAbort()
		{
			TChannel[] channels = this.GetChannels();
			for (int i = 0; i < channels.Length; i++)
			{
				channels[i].Abort();
			}
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x00102A1C File Offset: 0x00100C1C
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			TChannel[] channels = this.GetChannels();
			for (int i = 0; i < channels.Length; i++)
			{
				bool flag = false;
				try
				{
					channels[i].Close(timeoutHelper.RemainingTime());
					flag = true;
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (TimeoutException ex)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				finally
				{
					if (!flag)
					{
						channels[i].Abort();
					}
				}
			}
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x00102AD4 File Offset: 0x00100CD4
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TChannel[] channels = this.GetChannels();
			List<ICommunicationObject> list = new List<ICommunicationObject>();
			for (int i = 0; i < channels.Length; i++)
			{
				list.Add(channels[i]);
			}
			return new CloseCollectionAsyncResult(timeout, callback, state, list);
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x00102B17 File Offset: 0x00100D17
		protected override void OnEndClose(IAsyncResult result)
		{
			CloseCollectionAsyncResult.End(result);
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x00102B1F File Offset: 0x00100D1F
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x00102B26 File Offset: 0x00100D26
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x00102B2D File Offset: 0x00100D2D
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x00102B2F File Offset: 0x00100D2F
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x00102B38 File Offset: 0x00100D38
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x04002D3C RID: 11580
		private Dictionary<TChannel, TState> receivers;

		// Token: 0x04002D3D RID: 11581
		private EventHandler onInnerChannelClosed;

		// Token: 0x04002D3E RID: 11582
		private EventHandler onInnerChannelFaulted;
	}
}
