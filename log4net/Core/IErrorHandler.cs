using System;

namespace log4net.Core
{
	// Token: 0x0200005D RID: 93
	public interface IErrorHandler
	{
		// Token: 0x0600030E RID: 782
		void Error(string message, Exception e, ErrorCode errorCode);

		// Token: 0x0600030F RID: 783
		void Error(string message, Exception e);

		// Token: 0x06000310 RID: 784
		void Error(string message);
	}
}
