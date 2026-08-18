using System;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000573 RID: 1395
	[Serializable]
	public sealed class UserPersonalizationStateInfo : PersonalizationStateInfo
	{
		// Token: 0x060046C2 RID: 18114 RVA: 0x000E9F2D File Offset: 0x000E812D
		public UserPersonalizationStateInfo(string path, DateTime lastUpdatedDate, int size, string username, DateTime lastActivityDate) : base(path, lastUpdatedDate, size)
		{
			this._username = StringUtil.CheckAndTrimString(username, "username");
			this._lastActivityDate = lastActivityDate.ToUniversalTime();
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x060046C3 RID: 18115 RVA: 0x000E9F57 File Offset: 0x000E8157
		public string Username
		{
			get
			{
				return this._username;
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x060046C4 RID: 18116 RVA: 0x000E9F5F File Offset: 0x000E815F
		public DateTime LastActivityDate
		{
			get
			{
				return this._lastActivityDate.ToLocalTime();
			}
		}

		// Token: 0x040026BC RID: 9916
		private string _username;

		// Token: 0x040026BD RID: 9917
		private DateTime _lastActivityDate;
	}
}
