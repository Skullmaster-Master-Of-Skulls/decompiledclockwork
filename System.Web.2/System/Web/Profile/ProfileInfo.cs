using System;

namespace System.Web.Profile
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	public class ProfileInfo
	{
		// Token: 0x060013ED RID: 5101 RVA: 0x0003A610 File Offset: 0x00038810
		public ProfileInfo(string username, bool isAnonymous, DateTime lastActivityDate, DateTime lastUpdatedDate, int size)
		{
			if (username != null)
			{
				username = username.Trim();
			}
			this._UserName = username;
			if (lastActivityDate.Kind == DateTimeKind.Local)
			{
				lastActivityDate = lastActivityDate.ToUniversalTime();
			}
			this._LastActivityDate = lastActivityDate;
			if (lastUpdatedDate.Kind == DateTimeKind.Local)
			{
				lastUpdatedDate = lastUpdatedDate.ToUniversalTime();
			}
			this._LastUpdatedDate = lastUpdatedDate;
			this._IsAnonymous = isAnonymous;
			this._Size = size;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000030B5 File Offset: 0x000012B5
		protected ProfileInfo()
		{
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0003A679 File Offset: 0x00038879
		public virtual string UserName
		{
			get
			{
				return this._UserName;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0003A681 File Offset: 0x00038881
		public virtual DateTime LastActivityDate
		{
			get
			{
				return this._LastActivityDate.ToLocalTime();
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0003A68E File Offset: 0x0003888E
		public virtual DateTime LastUpdatedDate
		{
			get
			{
				return this._LastUpdatedDate.ToLocalTime();
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0003A69B File Offset: 0x0003889B
		public virtual bool IsAnonymous
		{
			get
			{
				return this._IsAnonymous;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0003A6A3 File Offset: 0x000388A3
		public virtual int Size
		{
			get
			{
				return this._Size;
			}
		}

		// Token: 0x04001509 RID: 5385
		private string _UserName;

		// Token: 0x0400150A RID: 5386
		private DateTime _LastActivityDate;

		// Token: 0x0400150B RID: 5387
		private DateTime _LastUpdatedDate;

		// Token: 0x0400150C RID: 5388
		private bool _IsAnonymous;

		// Token: 0x0400150D RID: 5389
		private int _Size;
	}
}
