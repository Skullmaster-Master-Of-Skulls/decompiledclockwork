using System;

namespace System.Web.SessionState
{
	// Token: 0x02000132 RID: 306
	internal class SessionOnEndTargetWorkItem
	{
		// Token: 0x06001251 RID: 4689 RVA: 0x00032F22 File Offset: 0x00031122
		internal SessionOnEndTargetWorkItem(SessionOnEndTarget target, HttpSessionState sessionState)
		{
			this._target = target;
			this._sessionState = sessionState;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00032F38 File Offset: 0x00031138
		internal void RaiseOnEndCallback()
		{
			this._target.RaiseOnEnd(this._sessionState);
		}

		// Token: 0x04001434 RID: 5172
		private SessionOnEndTarget _target;

		// Token: 0x04001435 RID: 5173
		private HttpSessionState _sessionState;
	}
}
