using System;
using System.Web.SessionState;

namespace System.Web.Mvc
{
	// Token: 0x020000A0 RID: 160
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class SessionStateAttribute : Attribute
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x0000D07A File Offset: 0x0000B27A
		public SessionStateAttribute(SessionStateBehavior behavior)
		{
			this.Behavior = behavior;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000D089 File Offset: 0x0000B289
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000D091 File Offset: 0x0000B291
		public SessionStateBehavior Behavior { get; private set; }
	}
}
