using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000518 RID: 1304
	public class WizardNavigationEventArgs : EventArgs
	{
		// Token: 0x0600422F RID: 16943 RVA: 0x000D85E5 File Offset: 0x000D67E5
		public WizardNavigationEventArgs(int currentStepIndex, int nextStepIndex)
		{
			this._currentStepIndex = currentStepIndex;
			this._nextStepIndex = nextStepIndex;
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x000D85FB File Offset: 0x000D67FB
		// (set) Token: 0x06004231 RID: 16945 RVA: 0x000D8603 File Offset: 0x000D6803
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x000D860C File Offset: 0x000D680C
		public int CurrentStepIndex
		{
			get
			{
				return this._currentStepIndex;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x000D8614 File Offset: 0x000D6814
		public int NextStepIndex
		{
			get
			{
				return this._nextStepIndex;
			}
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000D861C File Offset: 0x000D681C
		internal void SetNextStepIndex(int nextStepIndex)
		{
			this._nextStepIndex = nextStepIndex;
		}

		// Token: 0x04002558 RID: 9560
		private int _currentStepIndex;

		// Token: 0x04002559 RID: 9561
		private int _nextStepIndex;

		// Token: 0x0400255A RID: 9562
		private bool _cancel;
	}
}
