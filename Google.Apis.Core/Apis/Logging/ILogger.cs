using System;

namespace Google.Apis.Logging
{
	// Token: 0x0200001A RID: 26
	public interface ILogger
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008C RID: 140
		bool IsDebugEnabled { get; }

		// Token: 0x0600008D RID: 141
		ILogger ForType(Type type);

		// Token: 0x0600008E RID: 142
		ILogger ForType<T>();

		// Token: 0x0600008F RID: 143
		void Debug(string message, params object[] formatArgs);

		// Token: 0x06000090 RID: 144
		void Info(string message, params object[] formatArgs);

		// Token: 0x06000091 RID: 145
		void Warning(string message, params object[] formatArgs);

		// Token: 0x06000092 RID: 146
		void Error(Exception exception, string message, params object[] formatArgs);

		// Token: 0x06000093 RID: 147
		void Error(string message, params object[] formatArgs);
	}
}
