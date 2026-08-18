using System;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000299 RID: 665
	public interface IMessageFilter
	{
		// Token: 0x06002A04 RID: 10756
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		bool PreFilterMessage(ref Message m);
	}
}
