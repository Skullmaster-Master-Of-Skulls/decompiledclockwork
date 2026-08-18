using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200033A RID: 826
	[ComVisible(true)]
	public class QueryContinueDragEventArgs : EventArgs
	{
		// Token: 0x0600357B RID: 13691 RVA: 0x000F2917 File Offset: 0x000F0B17
		public QueryContinueDragEventArgs(int keyState, bool escapePressed, DragAction action)
		{
			this.keyState = keyState;
			this.escapePressed = escapePressed;
			this.action = action;
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x000F2934 File Offset: 0x000F0B34
		public int KeyState
		{
			get
			{
				return this.keyState;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000F293C File Offset: 0x000F0B3C
		public bool EscapePressed
		{
			get
			{
				return this.escapePressed;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x000F2944 File Offset: 0x000F0B44
		// (set) Token: 0x0600357F RID: 13695 RVA: 0x000F294C File Offset: 0x000F0B4C
		public DragAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x04001F5A RID: 8026
		private readonly int keyState;

		// Token: 0x04001F5B RID: 8027
		private readonly bool escapePressed;

		// Token: 0x04001F5C RID: 8028
		private DragAction action;
	}
}
