using System;

namespace System.Windows.Forms
{
	// Token: 0x02000248 RID: 584
	internal class DropSource : UnsafeNativeMethods.IOleDropSource
	{
		// Token: 0x0600250F RID: 9487 RVA: 0x000AD56A File Offset: 0x000AB76A
		public DropSource(ISupportOleDropSource peer)
		{
			if (peer == null)
			{
				throw new ArgumentNullException("peer");
			}
			this.peer = peer;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000AD588 File Offset: 0x000AB788
		public int OleQueryContinueDrag(int fEscapePressed, int grfKeyState)
		{
			bool flag = fEscapePressed != 0;
			DragAction action = DragAction.Continue;
			if (flag)
			{
				action = DragAction.Cancel;
			}
			else if ((grfKeyState & 1) == 0 && (grfKeyState & 2) == 0 && (grfKeyState & 16) == 0)
			{
				action = DragAction.Drop;
			}
			QueryContinueDragEventArgs queryContinueDragEventArgs = new QueryContinueDragEventArgs(grfKeyState, flag, action);
			this.peer.OnQueryContinueDrag(queryContinueDragEventArgs);
			int result = 0;
			DragAction action2 = queryContinueDragEventArgs.Action;
			if (action2 != DragAction.Drop)
			{
				if (action2 == DragAction.Cancel)
				{
					result = 262401;
				}
			}
			else
			{
				result = 262400;
			}
			return result;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000AD5F4 File Offset: 0x000AB7F4
		public int OleGiveFeedback(int dwEffect)
		{
			GiveFeedbackEventArgs giveFeedbackEventArgs = new GiveFeedbackEventArgs((DragDropEffects)dwEffect, true);
			this.peer.OnGiveFeedback(giveFeedbackEventArgs);
			if (giveFeedbackEventArgs.UseDefaultCursors)
			{
				return 262402;
			}
			return 0;
		}

		// Token: 0x04000F66 RID: 3942
		private const int DragDropSDrop = 262400;

		// Token: 0x04000F67 RID: 3943
		private const int DragDropSCancel = 262401;

		// Token: 0x04000F68 RID: 3944
		private const int DragDropSUseDefaultCursors = 262402;

		// Token: 0x04000F69 RID: 3945
		private ISupportOleDropSource peer;
	}
}
