using System;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000033 RID: 51
	public interface IFormatterLogger
	{
		// Token: 0x06000182 RID: 386
		void LogError(string errorPath, string errorMessage);

		// Token: 0x06000183 RID: 387
		void LogError(string errorPath, Exception exception);
	}
}
