using System;

namespace System.Web.Profile
{
	// Token: 0x02000168 RID: 360
	public sealed class ProfileAutoSaveEventArgs : EventArgs
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0003B49A File Offset: 0x0003969A
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0003B4A2 File Offset: 0x000396A2
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x0003B4AA File Offset: 0x000396AA
		public bool ContinueWithProfileAutoSave
		{
			get
			{
				return this._ContinueSave;
			}
			set
			{
				this._ContinueSave = value;
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0003B4B3 File Offset: 0x000396B3
		public ProfileAutoSaveEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04001524 RID: 5412
		private HttpContext _Context;

		// Token: 0x04001525 RID: 5413
		private bool _ContinueSave = true;
	}
}
