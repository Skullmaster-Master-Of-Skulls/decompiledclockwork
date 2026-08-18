using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000687 RID: 1671
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WizardNavigationEventArgs : EventArgs
	{
		// Token: 0x06005201 RID: 20993 RVA: 0x0014B98A File Offset: 0x0014A98A
		public WizardNavigationEventArgs(int currentStepIndex, int nextStepIndex)
		{
			this._currentStepIndex = currentStepIndex;
			this._nextStepIndex = nextStepIndex;
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06005202 RID: 20994 RVA: 0x0014B9A0 File Offset: 0x0014A9A0
		// (set) Token: 0x06005203 RID: 20995 RVA: 0x0014B9A8 File Offset: 0x0014A9A8
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

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x0014B9B1 File Offset: 0x0014A9B1
		public int CurrentStepIndex
		{
			get
			{
				return this._currentStepIndex;
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06005205 RID: 20997 RVA: 0x0014B9B9 File Offset: 0x0014A9B9
		public int NextStepIndex
		{
			get
			{
				return this._nextStepIndex;
			}
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x0014B9C1 File Offset: 0x0014A9C1
		internal void SetNextStepIndex(int nextStepIndex)
		{
			this._nextStepIndex = nextStepIndex;
		}

		// Token: 0x04002DD4 RID: 11732
		private int _currentStepIndex;

		// Token: 0x04002DD5 RID: 11733
		private int _nextStepIndex;

		// Token: 0x04002DD6 RID: 11734
		private bool _cancel;
	}
}
