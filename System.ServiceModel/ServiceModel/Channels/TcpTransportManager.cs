using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200085A RID: 2138
	internal abstract class TcpTransportManager : ConnectionOrientedTransportManager<TcpChannelListener>
	{
		// Token: 0x06005027 RID: 20519 RVA: 0x0012612A File Offset: 0x0012432A
		internal TcpTransportManager()
		{
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x00126132 File Offset: 0x00124332
		internal override string Scheme
		{
			get
			{
				return Uri.UriSchemeNetTcp;
			}
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x00126139 File Offset: 0x00124339
		protected virtual bool IsCompatible(TcpChannelListener channelListener)
		{
			return base.IsCompatible(channelListener);
		}
	}
}
