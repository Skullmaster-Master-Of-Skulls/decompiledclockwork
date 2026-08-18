using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200056D RID: 1389
	public interface IErrorHandler
	{
		// Token: 0x0600360A RID: 13834
		void ProvideFault(Exception error, MessageVersion version, ref Message fault);

		// Token: 0x0600360B RID: 13835
		bool HandleError(Exception error);
	}
}
