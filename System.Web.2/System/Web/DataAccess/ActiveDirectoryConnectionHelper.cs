using System;
using System.DirectoryServices;
using System.Web.Security;

namespace System.Web.DataAccess
{
	// Token: 0x020001A8 RID: 424
	internal static class ActiveDirectoryConnectionHelper
	{
		// Token: 0x06001641 RID: 5697 RVA: 0x00046584 File Offset: 0x00044784
		internal static DirectoryEntryHolder GetDirectoryEntry(DirectoryInformation directoryInfo, string objectDN, bool revertImpersonation)
		{
			DirectoryEntryHolder directoryEntryHolder = new DirectoryEntryHolder(new DirectoryEntry(directoryInfo.GetADsPath(objectDN), directoryInfo.GetUsername(), directoryInfo.GetPassword(), directoryInfo.AuthenticationTypes));
			directoryEntryHolder.Open(null, revertImpersonation);
			return directoryEntryHolder;
		}
	}
}
