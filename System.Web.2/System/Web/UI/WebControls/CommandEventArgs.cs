using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000398 RID: 920
	public class CommandEventArgs : EventArgs
	{
		// Token: 0x06002BF6 RID: 11254 RVA: 0x0008F690 File Offset: 0x0008D890
		public CommandEventArgs(CommandEventArgs e) : this(e.CommandName, e.CommandArgument)
		{
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x0008F6A4 File Offset: 0x0008D8A4
		public CommandEventArgs(string commandName, object argument)
		{
			this.commandName = commandName;
			this.argument = argument;
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x0008F6BA File Offset: 0x0008D8BA
		public string CommandName
		{
			get
			{
				return this.commandName;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x0008F6C2 File Offset: 0x0008D8C2
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x04001F2A RID: 7978
		private string commandName;

		// Token: 0x04001F2B RID: 7979
		private object argument;
	}
}
