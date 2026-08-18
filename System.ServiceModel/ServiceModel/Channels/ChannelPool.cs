using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200073D RID: 1853
	internal class ChannelPool<TChannel> : IdlingCommunicationPool<ChannelPoolKey, TChannel> where TChannel : class, IChannel
	{
		// Token: 0x06004678 RID: 18040 RVA: 0x00106BDD File Offset: 0x00104DDD
		public ChannelPool(ChannelPoolSettings settings) : base(settings.MaxOutboundChannelsPerEndpoint, settings.IdleTimeout, settings.LeaseTimeout)
		{
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x00106BF7 File Offset: 0x00104DF7
		protected override void AbortItem(TChannel item)
		{
			item.Abort();
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x00106C04 File Offset: 0x00104E04
		protected override void CloseItem(TChannel item, TimeSpan timeout)
		{
			item.Close(timeout);
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x00106C14 File Offset: 0x00104E14
		protected override void CloseItemAsync(TChannel item, TimeSpan timeout)
		{
			bool flag = false;
			try
			{
				IAsyncResult asyncResult = item.BeginClose(timeout, ChannelPool<TChannel>.onCloseComplete, item);
				if (asyncResult.CompletedSynchronously)
				{
					item.EndClose(asyncResult);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					item.Abort();
				}
			}
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00106C74 File Offset: 0x00104E74
		protected override ChannelPoolKey GetPoolKey(EndpointAddress address, Uri via)
		{
			return new ChannelPoolKey(address, via);
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x00106C80 File Offset: 0x00104E80
		private static void OnCloseComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			TChannel tchannel = (TChannel)((object)result.AsyncState);
			bool flag = false;
			try
			{
				tchannel.EndClose(result);
				flag = true;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
			}
			finally
			{
				if (!flag)
				{
					tchannel.Abort();
				}
			}
		}

		// Token: 0x04002D8A RID: 11658
		private static AsyncCallback onCloseComplete = Fx.ThunkCallback(new AsyncCallback(ChannelPool<TChannel>.OnCloseComplete));
	}
}
