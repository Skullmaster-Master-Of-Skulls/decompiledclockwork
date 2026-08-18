using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200072A RID: 1834
	internal class DuplexSessionChannelDemuxer : SessionChannelDemuxer<IDuplexSessionChannel, Message>
	{
		// Token: 0x060045C2 RID: 17858 RVA: 0x001054A2 File Offset: 0x001036A2
		public DuplexSessionChannelDemuxer(BindingContext context, TimeSpan peekTimeout, int maxPendingSessions) : base(context, peekTimeout, maxPendingSessions)
		{
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x001054AD File Offset: 0x001036AD
		protected override void AbortItem(Message message)
		{
			TypedChannelDemuxer.AbortMessage(message);
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x001054B5 File Offset: 0x001036B5
		protected override IAsyncResult BeginReceive(IDuplexSessionChannel channel, AsyncCallback callback, object state)
		{
			return channel.BeginReceive(callback, state);
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x001054BF File Offset: 0x001036BF
		protected override IAsyncResult BeginReceive(IDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return channel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x001054CB File Offset: 0x001036CB
		protected override IDuplexSessionChannel CreateChannel(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel, Message firstMessage)
		{
			return new DuplexSessionChannelWrapper(channelManager, innerChannel, firstMessage);
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x001054D8 File Offset: 0x001036D8
		private void EndpointNotFoundCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			DuplexSessionChannelDemuxer.ChannelAndMessageAsyncState channelAndMessageAsyncState = (DuplexSessionChannelDemuxer.ChannelAndMessageAsyncState)result.AsyncState;
			bool flag = true;
			try
			{
				DuplexSessionDemuxFailureAsyncResult.End(result);
				flag = false;
			}
			catch (TimeoutException ex)
			{
				if (TD.SendTimeoutIsEnabled())
				{
					TD.SendTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (ObjectDisposedException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			catch (Exception exception3)
			{
				if (Fx.IsFatal(exception3))
				{
					throw;
				}
				base.HandleUnknownException(exception3);
			}
			finally
			{
				if (flag)
				{
					this.AbortItem(channelAndMessageAsyncState.message);
					channelAndMessageAsyncState.channel.Abort();
				}
			}
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x001055AC File Offset: 0x001037AC
		protected override void EndpointNotFound(IDuplexSessionChannel channel, Message message)
		{
			bool flag = true;
			try
			{
				if (base.DemuxFailureHandler != null)
				{
					try
					{
						DuplexSessionDemuxFailureAsyncResult duplexSessionDemuxFailureAsyncResult = new DuplexSessionDemuxFailureAsyncResult(base.DemuxFailureHandler, channel, message, Fx.ThunkCallback(new AsyncCallback(this.EndpointNotFoundCallback)), new DuplexSessionChannelDemuxer.ChannelAndMessageAsyncState(channel, message));
						duplexSessionDemuxFailureAsyncResult.Start();
						if (!duplexSessionDemuxFailureAsyncResult.CompletedSynchronously)
						{
							flag = false;
						}
						else
						{
							DuplexSessionDemuxFailureAsyncResult.End(duplexSessionDemuxFailureAsyncResult);
							flag = false;
						}
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (TimeoutException ex)
					{
						if (TD.SendTimeoutIsEnabled())
						{
							TD.SendTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					}
					catch (ObjectDisposedException exception2)
					{
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					}
					catch (Exception exception3)
					{
						if (Fx.IsFatal(exception3))
						{
							throw;
						}
						base.HandleUnknownException(exception3);
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.AbortItem(message);
					channel.Abort();
				}
			}
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x001056AC File Offset: 0x001038AC
		protected override Message EndReceive(IDuplexSessionChannel channel, IAsyncResult result)
		{
			return channel.EndReceive(result);
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x001056B5 File Offset: 0x001038B5
		protected override Message GetMessage(Message message)
		{
			return message;
		}

		// Token: 0x02000CCE RID: 3278
		private struct ChannelAndMessageAsyncState
		{
			// Token: 0x060079CB RID: 31179 RVA: 0x001C65E1 File Offset: 0x001C47E1
			public ChannelAndMessageAsyncState(IChannel channel, Message message)
			{
				this.channel = channel;
				this.message = message;
			}

			// Token: 0x040045AD RID: 17837
			public IChannel channel;

			// Token: 0x040045AE RID: 17838
			public Message message;
		}
	}
}
