using System;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x0200032D RID: 813
	internal static class AutomationMessages
	{
		// Token: 0x06003517 RID: 13591 RVA: 0x000F145C File Offset: 0x000EF65C
		public static IntPtr WriteAutomationText(string text)
		{
			IntPtr zero = IntPtr.Zero;
			string text2 = AutomationMessages.GenerateLogFileName(ref zero);
			if (text2 != null)
			{
				try
				{
					FileStream fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write);
					StreamWriter streamWriter = new StreamWriter(fileStream);
					streamWriter.WriteLine(text);
					streamWriter.Dispose();
					fileStream.Dispose();
				}
				catch
				{
					zero = IntPtr.Zero;
				}
			}
			return zero;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000F14BC File Offset: 0x000EF6BC
		public static string ReadAutomationText(IntPtr fileId)
		{
			string result = null;
			if (fileId != IntPtr.Zero)
			{
				string path = AutomationMessages.GenerateLogFileName(ref fileId);
				if (File.Exists(path))
				{
					try
					{
						FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
						StreamReader streamReader = new StreamReader(fileStream);
						result = streamReader.ReadToEnd();
						streamReader.Dispose();
						fileStream.Dispose();
					}
					catch
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000F1524 File Offset: 0x000EF724
		private static string GenerateLogFileName(ref IntPtr fileId)
		{
			string result = null;
			string environmentVariable = Environment.GetEnvironmentVariable("TEMP");
			if (environmentVariable != null)
			{
				if (fileId == IntPtr.Zero)
				{
					Random random = new Random(DateTime.Now.Millisecond);
					fileId = new IntPtr(random.Next());
				}
				result = environmentVariable + "\\Maui" + fileId.ToString() + ".log";
			}
			return result;
		}

		// Token: 0x04001F39 RID: 7993
		private const int WM_USER = 1024;

		// Token: 0x04001F3A RID: 7994
		internal const int PGM_GETBUTTONCOUNT = 1104;

		// Token: 0x04001F3B RID: 7995
		internal const int PGM_GETBUTTONSTATE = 1106;

		// Token: 0x04001F3C RID: 7996
		internal const int PGM_SETBUTTONSTATE = 1105;

		// Token: 0x04001F3D RID: 7997
		internal const int PGM_GETBUTTONTEXT = 1107;

		// Token: 0x04001F3E RID: 7998
		internal const int PGM_GETBUTTONTOOLTIPTEXT = 1108;

		// Token: 0x04001F3F RID: 7999
		internal const int PGM_GETROWCOORDS = 1109;

		// Token: 0x04001F40 RID: 8000
		internal const int PGM_GETVISIBLEROWCOUNT = 1110;

		// Token: 0x04001F41 RID: 8001
		internal const int PGM_GETSELECTEDROW = 1111;

		// Token: 0x04001F42 RID: 8002
		internal const int PGM_SETSELECTEDTAB = 1112;

		// Token: 0x04001F43 RID: 8003
		internal const int PGM_GETTESTINGINFO = 1113;
	}
}
