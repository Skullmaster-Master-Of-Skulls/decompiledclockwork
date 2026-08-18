using System;

namespace System.Web.Profile
{
	// Token: 0x02000166 RID: 358
	public sealed class ProfileMigrateEventArgs : EventArgs
	{
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0003B474 File Offset: 0x00039674
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0003B47C File Offset: 0x0003967C
		public string AnonymousID
		{
			get
			{
				return this._AnonymousId;
			}
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0003B484 File Offset: 0x00039684
		public ProfileMigrateEventArgs(HttpContext context, string anonymousId)
		{
			this._Context = context;
			this._AnonymousId = anonymousId;
		}

		// Token: 0x04001522 RID: 5410
		private HttpContext _Context;

		// Token: 0x04001523 RID: 5411
		private string _AnonymousId;
	}
}
