using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F9 RID: 1273
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CommandEventArgs : EventArgs
	{
		// Token: 0x06003E27 RID: 15911 RVA: 0x00103894 File Offset: 0x00102894
		public CommandEventArgs(CommandEventArgs e) : this(e.CommandName, e.CommandArgument)
		{
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x001038A8 File Offset: 0x001028A8
		public CommandEventArgs(string commandName, object argument)
		{
			this.commandName = commandName;
			this.argument = argument;
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x001038BE File Offset: 0x001028BE
		public string CommandName
		{
			get
			{
				return this.commandName;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x001038C6 File Offset: 0x001028C6
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x04002794 RID: 10132
		private string commandName;

		// Token: 0x04002795 RID: 10133
		private object argument;
	}
}
