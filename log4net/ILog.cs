using System;
using log4net.Core;

namespace log4net
{
	// Token: 0x02000071 RID: 113
	public interface ILog : ILoggerWrapper
	{
		// Token: 0x060003D9 RID: 985
		void Debug(object message);

		// Token: 0x060003DA RID: 986
		void Debug(object message, Exception exception);

		// Token: 0x060003DB RID: 987
		void DebugFormat(string format, params object[] args);

		// Token: 0x060003DC RID: 988
		void DebugFormat(string format, object arg0);

		// Token: 0x060003DD RID: 989
		void DebugFormat(string format, object arg0, object arg1);

		// Token: 0x060003DE RID: 990
		void DebugFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x060003DF RID: 991
		void DebugFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x060003E0 RID: 992
		void Info(object message);

		// Token: 0x060003E1 RID: 993
		void Info(object message, Exception exception);

		// Token: 0x060003E2 RID: 994
		void InfoFormat(string format, params object[] args);

		// Token: 0x060003E3 RID: 995
		void InfoFormat(string format, object arg0);

		// Token: 0x060003E4 RID: 996
		void InfoFormat(string format, object arg0, object arg1);

		// Token: 0x060003E5 RID: 997
		void InfoFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x060003E6 RID: 998
		void InfoFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x060003E7 RID: 999
		void Warn(object message);

		// Token: 0x060003E8 RID: 1000
		void Warn(object message, Exception exception);

		// Token: 0x060003E9 RID: 1001
		void WarnFormat(string format, params object[] args);

		// Token: 0x060003EA RID: 1002
		void WarnFormat(string format, object arg0);

		// Token: 0x060003EB RID: 1003
		void WarnFormat(string format, object arg0, object arg1);

		// Token: 0x060003EC RID: 1004
		void WarnFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x060003ED RID: 1005
		void WarnFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x060003EE RID: 1006
		void Error(object message);

		// Token: 0x060003EF RID: 1007
		void Error(object message, Exception exception);

		// Token: 0x060003F0 RID: 1008
		void ErrorFormat(string format, params object[] args);

		// Token: 0x060003F1 RID: 1009
		void ErrorFormat(string format, object arg0);

		// Token: 0x060003F2 RID: 1010
		void ErrorFormat(string format, object arg0, object arg1);

		// Token: 0x060003F3 RID: 1011
		void ErrorFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x060003F4 RID: 1012
		void ErrorFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x060003F5 RID: 1013
		void Fatal(object message);

		// Token: 0x060003F6 RID: 1014
		void Fatal(object message, Exception exception);

		// Token: 0x060003F7 RID: 1015
		void FatalFormat(string format, params object[] args);

		// Token: 0x060003F8 RID: 1016
		void FatalFormat(string format, object arg0);

		// Token: 0x060003F9 RID: 1017
		void FatalFormat(string format, object arg0, object arg1);

		// Token: 0x060003FA RID: 1018
		void FatalFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x060003FB RID: 1019
		void FatalFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003FC RID: 1020
		bool IsDebugEnabled { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003FD RID: 1021
		bool IsInfoEnabled { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003FE RID: 1022
		bool IsWarnEnabled { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003FF RID: 1023
		bool IsErrorEnabled { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000400 RID: 1024
		bool IsFatalEnabled { get; }
	}
}
