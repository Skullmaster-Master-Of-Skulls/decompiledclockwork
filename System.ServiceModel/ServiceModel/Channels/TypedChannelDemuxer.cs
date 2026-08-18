using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200071F RID: 1823
	internal abstract class TypedChannelDemuxer
	{
		// Token: 0x06004531 RID: 17713 RVA: 0x00103440 File Offset: 0x00101640
		internal static void AbortMessage(RequestContext request)
		{
			try
			{
				TypedChannelDemuxer.AbortMessage(request.RequestMessage);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x00103480 File Offset: 0x00101680
		internal static void AbortMessage(Message message)
		{
			try
			{
				message.Close();
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
		}

		// Token: 0x06004533 RID: 17715
		public abstract IChannelListener<TChannel> BuildChannelListener<TChannel>(ChannelDemuxerFilter filter) where TChannel : class, IChannel;
	}
}
