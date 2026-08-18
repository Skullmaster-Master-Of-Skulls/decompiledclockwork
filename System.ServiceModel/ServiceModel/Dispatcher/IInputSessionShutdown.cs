using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200056E RID: 1390
	public interface IInputSessionShutdown
	{
		// Token: 0x0600360C RID: 13836
		void ChannelFaulted(IDuplexContextChannel channel);

		// Token: 0x0600360D RID: 13837
		void DoneReceiving(IDuplexContextChannel channel);
	}
}
