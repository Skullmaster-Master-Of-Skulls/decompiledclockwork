using System;
using System.Threading;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x0200007F RID: 127
	internal static class ExceptionHelper
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000947C File Offset: 0x0000767C
		public static void MarkAsLoggedToInternalLogger(this Exception exception)
		{
			if (exception != null)
			{
				exception.Data["NLog.ExceptionLoggedToInternalLogger"] = true;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00009498 File Offset: 0x00007698
		public static bool IsLoggedToInternalLogger(this Exception exception)
		{
			return exception != null && ((exception.Data["NLog.ExceptionLoggedToInternalLogger"] as bool?) ?? false);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000094D8 File Offset: 0x000076D8
		public static bool MustBeRethrown(this Exception exception)
		{
			if (exception.MustBeRethrownImmediately())
			{
				return true;
			}
			bool flag = exception is NLogConfigurationException;
			if (!exception.IsLoggedToInternalLogger())
			{
				LogLevel level = flag ? LogLevel.Warn : LogLevel.Error;
				InternalLogger.Log(exception, level, "Error has been raised.");
			}
			return flag ? (LogManager.ThrowConfigExceptions ?? LogManager.ThrowExceptions) : LogManager.ThrowExceptions;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00009546 File Offset: 0x00007746
		public static bool MustBeRethrownImmediately(this Exception exception)
		{
			return exception is StackOverflowException || exception is ThreadAbortException || exception is OutOfMemoryException;
		}

		// Token: 0x040000D5 RID: 213
		private const string LoggedKey = "NLog.ExceptionLoggedToInternalLogger";
	}
}
