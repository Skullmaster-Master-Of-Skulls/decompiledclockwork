using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000514 RID: 1300
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class PropertyGridCommands
	{
		// Token: 0x0400374B RID: 14155
		protected static readonly Guid wfcMenuGroup = new Guid("{a72bd644-1979-4cbc-a620-ea4112198a66}");

		// Token: 0x0400374C RID: 14156
		protected static readonly Guid wfcMenuCommand = new Guid("{5a51cf82-7619-4a5d-b054-47f438425aa7}");

		// Token: 0x0400374D RID: 14157
		public static readonly CommandID Reset = new CommandID(PropertyGridCommands.wfcMenuCommand, 12288);

		// Token: 0x0400374E RID: 14158
		public static readonly CommandID Description = new CommandID(PropertyGridCommands.wfcMenuCommand, 12289);

		// Token: 0x0400374F RID: 14159
		public static readonly CommandID Hide = new CommandID(PropertyGridCommands.wfcMenuCommand, 12290);

		// Token: 0x04003750 RID: 14160
		public static readonly CommandID Commands = new CommandID(PropertyGridCommands.wfcMenuCommand, 12304);
	}
}
