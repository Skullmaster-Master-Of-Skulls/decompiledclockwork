using System;

namespace System.Data
{
	// Token: 0x020000E5 RID: 229
	public sealed class StateChangeEventArgs : EventArgs
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x00216BA8 File Offset: 0x00215FA8
		public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
		{
			this.originalState = originalState;
			this.currentState = currentState;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00216BD8 File Offset: 0x00215FD8
		public ConnectionState CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00216BF8 File Offset: 0x00215FF8
		public ConnectionState OriginalState
		{
			get
			{
				return this.originalState;
			}
		}

		// Token: 0x0400095A RID: 2394
		private ConnectionState originalState;

		// Token: 0x0400095B RID: 2395
		private ConnectionState currentState;
	}
}
