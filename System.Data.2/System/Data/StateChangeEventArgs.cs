using System;

namespace System.Data
{
	// Token: 0x0200012F RID: 303
	public sealed class StateChangeEventArgs : EventArgs
	{
		// Token: 0x060011FA RID: 4602 RVA: 0x00089E34 File Offset: 0x00089234
		public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
		{
			this.originalState = originalState;
			this.currentState = currentState;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00089E58 File Offset: 0x00089258
		public ConnectionState CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x00089E6C File Offset: 0x0008926C
		public ConnectionState OriginalState
		{
			get
			{
				return this.originalState;
			}
		}

		// Token: 0x0400063D RID: 1597
		private ConnectionState originalState;

		// Token: 0x0400063E RID: 1598
		private ConnectionState currentState;
	}
}
