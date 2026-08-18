using System;
using log4net.Appender;

namespace log4net.Core
{
	// Token: 0x02000015 RID: 21
	public interface IAppenderAttachable
	{
		// Token: 0x060000D2 RID: 210
		void AddAppender(IAppender appender);

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D3 RID: 211
		AppenderCollection Appenders { get; }

		// Token: 0x060000D4 RID: 212
		IAppender GetAppender(string name);

		// Token: 0x060000D5 RID: 213
		void RemoveAllAppenders();

		// Token: 0x060000D6 RID: 214
		IAppender RemoveAppender(IAppender appender);

		// Token: 0x060000D7 RID: 215
		IAppender RemoveAppender(string name);
	}
}
