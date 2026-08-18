using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000881 RID: 2177
	internal interface IStreamedMessageEncoder
	{
		// Token: 0x06005290 RID: 21136
		Stream GetResponseMessageStream(Message message);
	}
}
