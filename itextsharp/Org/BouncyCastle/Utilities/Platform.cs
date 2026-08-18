using System;
using System.Security;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x020004AF RID: 1199
	internal sealed class Platform
	{
		// Token: 0x06002886 RID: 10374 RVA: 0x000F6351 File Offset: 0x000F5351
		private Platform()
		{
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000F6359 File Offset: 0x000F5359
		internal static Exception CreateNotImplementedException(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000F6364 File Offset: 0x000F5364
		internal static string GetEnvironmentVariable(string variable)
		{
			string result;
			try
			{
				result = Environment.GetEnvironmentVariable(variable);
			}
			catch (SecurityException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000F6390 File Offset: 0x000F5390
		private static string GetNewLine()
		{
			return Environment.NewLine;
		}

		// Token: 0x04001CAE RID: 7342
		internal static readonly string NewLine = Platform.GetNewLine();
	}
}
