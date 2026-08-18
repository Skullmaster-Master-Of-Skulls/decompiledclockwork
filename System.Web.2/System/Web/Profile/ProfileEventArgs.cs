using System;

namespace System.Web.Profile
{
	// Token: 0x0200015F RID: 351
	public sealed class ProfileEventArgs : EventArgs
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0003A5E5 File Offset: 0x000387E5
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0003A5ED File Offset: 0x000387ED
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x0003A5F5 File Offset: 0x000387F5
		public ProfileBase Profile
		{
			get
			{
				return this._Profile;
			}
			set
			{
				this._Profile = value;
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0003A5FE File Offset: 0x000387FE
		public ProfileEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04001507 RID: 5383
		private HttpContext _Context;

		// Token: 0x04001508 RID: 5384
		private ProfileBase _Profile;
	}
}
