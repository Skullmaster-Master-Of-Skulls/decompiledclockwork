using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020001B7 RID: 439
	[ComVisible(true)]
	public class MenuCommandsChangedEventArgs : EventArgs
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x0005AA53 File Offset: 0x00058C53
		public MenuCommandsChangedEventArgs(MenuCommandsChangedType changeType, MenuCommand command)
		{
			this.changeType = changeType;
			this.command = command;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x0005AA69 File Offset: 0x00058C69
		public MenuCommandsChangedType ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0005AA71 File Offset: 0x00058C71
		public MenuCommand Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x0400094C RID: 2380
		private MenuCommand command;

		// Token: 0x0400094D RID: 2381
		private MenuCommandsChangedType changeType;
	}
}
