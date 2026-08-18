using System;
using System.Diagnostics;
using ClockWorkLogger;

namespace TechnoPro.Common.Win32
{
	// Token: 0x0200000A RID: 10
	public static class CommandPrompt
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003484 File Offset: 0x00001684
		public static bool ExecuteProgram(string filename, string arguments, int waitForExitMilliseconds = 0)
		{
			bool result;
			try
			{
				Process process = Process.Start(new ProcessStartInfo
				{
					FileName = filename,
					Arguments = arguments,
					Verb = "runas"
				});
				CWLogger.Logger.Info("CommandPrompt::ExecuteProgram:: Started process ({0}): filename='{1}', arguments='{2}'", (process != null) ? process.Id.ToString() : "NULL", filename ?? "NULL", arguments ?? "NULL");
				if (waitForExitMilliseconds > 0)
				{
					CWLogger.Logger.Info("CommandPrompt::ExecuteProgram:: Process ({0}): waiting for {1} milliseconds", (process != null) ? process.Id.ToString() : "NULL", waitForExitMilliseconds);
					if (process != null && !process.WaitForExit(waitForExitMilliseconds))
					{
						CWLogger.Logger.Error("CommandPrompt::ExecuteProgram:: Process ({0}): timeout after {1} milliseconds", process.Id, waitForExitMilliseconds);
						return false;
					}
					CWLogger.Logger.Info("CommandPrompt::ExecuteProgram:: Process ({0}): ended successfully", process.Id);
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("CommandPrompt::ExecuteProgram:: {0}", ex.ToString()), ex);
				result = false;
			}
			return result;
		}
	}
}
