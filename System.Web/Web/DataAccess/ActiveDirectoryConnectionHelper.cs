using System;
using System.DirectoryServices;
using System.Web.Security;

namespace System.Web.DataAccess
{
	// Token: 0x02000273 RID: 627
	internal static class ActiveDirectoryConnectionHelper
	{
		// Token: 0x060020BC RID: 8380 RVA: 0x0008E59C File Offset: 0x0008D59C
		internal static DirectoryEntryHolder GetDirectoryEntry(DirectoryInformation directoryInfo, string objectDN, bool revertImpersonation)
		{
			DirectoryEntryHolder directoryEntryHolder = new DirectoryEntryHolder(new DirectoryEntry(directoryInfo.GetADsPath(objectDN), directoryInfo.GetUsername(), directoryInfo.GetPassword(), directoryInfo.AuthenticationTypes));
			directoryEntryHolder.Open(null, revertImpersonation);
			return directoryEntryHolder;
		}
	}
}
