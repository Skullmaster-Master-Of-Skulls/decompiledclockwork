using System;

namespace Telerik.Licensing
{
	// Token: 0x0200040F RID: 1039
	internal class SessionChangedEventArgs : EventArgs
	{
		// Token: 0x060025C9 RID: 9673 RVA: 0x0007CFDB File Offset: 0x0007B1DB
		public SessionChangedEventArgs(Session session)
		{
			this._session = session;
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x0007CFEA File Offset: 0x0007B1EA
		public Session Session
		{
			get
			{
				return this._session;
			}
		}

		// Token: 0x040009A8 RID: 2472
		private readonly Session _session;
	}
}
