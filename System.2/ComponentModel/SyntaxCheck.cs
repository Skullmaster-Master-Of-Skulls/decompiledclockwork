using System;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005AE RID: 1454
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public static class SyntaxCheck
	{
		// Token: 0x06003631 RID: 13873 RVA: 0x000EC8AA File Offset: 0x000EAAAA
		public static bool CheckMachineName(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.IndexOf('\\') == -1;
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000EC8D3 File Offset: 0x000EAAD3
		public static bool CheckPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.StartsWith("\\\\");
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000EC8FC File Offset: 0x000EAAFC
		public static bool CheckRootedPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && Path.IsPathRooted(value);
		}
	}
}
