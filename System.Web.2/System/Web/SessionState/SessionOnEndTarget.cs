using System;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x02000133 RID: 307
	internal class SessionOnEndTarget
	{
		// Token: 0x06001253 RID: 4691 RVA: 0x000030B5 File Offset: 0x000012B5
		internal SessionOnEndTarget()
		{
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x00032F4B File Offset: 0x0003114B
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x00032F53 File Offset: 0x00031153
		internal int SessionEndEventHandlerCount
		{
			get
			{
				return this._sessionEndEventHandlerCount;
			}
			set
			{
				this._sessionEndEventHandlerCount = value;
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00032F5C File Offset: 0x0003115C
		internal void RaiseOnEnd(HttpSessionState sessionState)
		{
			if (this._sessionEndEventHandlerCount > 0)
			{
				HttpApplicationFactory.EndSession(sessionState, this, EventArgs.Empty);
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00032F74 File Offset: 0x00031174
		internal void RaiseSessionOnEnd(string id, SessionStateStoreData item)
		{
			HttpSessionStateContainer container = new HttpSessionStateContainer(id, item.Items, item.StaticObjects, item.Timeout, false, SessionStateModule.s_configCookieless, SessionStateModule.s_configMode, true);
			HttpSessionState sessionState = new HttpSessionState(container);
			if (HttpRuntime.ShutdownInProgress)
			{
				this.RaiseOnEnd(sessionState);
				return;
			}
			SessionOnEndTargetWorkItem @object = new SessionOnEndTargetWorkItem(this, sessionState);
			WorkItem.PostInternal(new WorkItemCallback(@object.RaiseOnEndCallback));
		}

		// Token: 0x04001436 RID: 5174
		internal int _sessionEndEventHandlerCount;
	}
}
