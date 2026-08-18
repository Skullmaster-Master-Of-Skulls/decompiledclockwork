using System;

namespace System.Web.ApplicationServices
{
	// Token: 0x0200011E RID: 286
	public class CreatingCookieEventArgs : EventArgs
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00036132 File Offset: 0x00034332
		public string UserName
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0003613A File Offset: 0x0003433A
		public string Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00036142 File Offset: 0x00034342
		public string CustomCredential
		{
			get
			{
				return this._customCredential;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0003614A File Offset: 0x0003434A
		public bool IsPersistent
		{
			get
			{
				return this._isPersistent;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0003615B File Offset: 0x0003435B
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x00036152 File Offset: 0x00034352
		public bool CookieIsSet
		{
			get
			{
				return this._cookieIsSet;
			}
			set
			{
				this._cookieIsSet = value;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00036163 File Offset: 0x00034363
		internal CreatingCookieEventArgs(string username, string password, bool isPersistent, string customCredential)
		{
			this._cookieIsSet = false;
			this._userName = username;
			this._password = password;
			this._password = password;
			this._isPersistent = isPersistent;
			this._customCredential = customCredential;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00035E5A File Offset: 0x0003405A
		private CreatingCookieEventArgs()
		{
		}

		// Token: 0x04000439 RID: 1081
		private string _userName;

		// Token: 0x0400043A RID: 1082
		private string _password;

		// Token: 0x0400043B RID: 1083
		private string _customCredential;

		// Token: 0x0400043C RID: 1084
		private bool _isPersistent;

		// Token: 0x0400043D RID: 1085
		private bool _cookieIsSet;
	}
}
