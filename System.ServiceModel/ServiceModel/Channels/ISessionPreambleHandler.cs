using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000813 RID: 2067
	internal interface ISessionPreambleHandler
	{
		// Token: 0x06004D3E RID: 19774
		void HandleServerSessionPreamble(ServerSessionPreambleConnectionReader serverSessionPreambleReader, ConnectionDemuxer connectionDemuxer);
	}
}
