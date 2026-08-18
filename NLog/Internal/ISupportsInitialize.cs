using System;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x02000094 RID: 148
	internal interface ISupportsInitialize
	{
		// Token: 0x060004C2 RID: 1218
		void Initialize(LoggingConfiguration configuration);

		// Token: 0x060004C3 RID: 1219
		void Close();
	}
}
