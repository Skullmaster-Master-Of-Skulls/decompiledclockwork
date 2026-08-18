using System;
using System.Security;

namespace NLog.Internal
{
	// Token: 0x0200007E RID: 126
	internal static class EnvironmentHelper
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00009424 File Offset: 0x00007624
		internal static string NewLine
		{
			get
			{
				return Environment.NewLine;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00009438 File Offset: 0x00007638
		internal static string GetSafeEnvironmentVariable(string name)
		{
			string result;
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable(name);
				if (environmentVariable == null || environmentVariable.Length == 0)
				{
					result = null;
				}
				else
				{
					result = environmentVariable;
				}
			}
			catch (SecurityException)
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
