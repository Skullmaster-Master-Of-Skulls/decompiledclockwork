using System;
using System.Data.Common;

namespace System.Data.Sql
{
	// Token: 0x02000296 RID: 662
	public sealed class SqlNotificationRequest
	{
		// Token: 0x06002253 RID: 8787 RVA: 0x0028BAF8 File Offset: 0x0028AEF8
		public SqlNotificationRequest() : this(null, null, 0)
		{
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x0028BB18 File Offset: 0x0028AF18
		public SqlNotificationRequest(string userData, string options, int timeout)
		{
			this.UserData = userData;
			this.Timeout = timeout;
			this.Options = options;
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0028BB48 File Offset: 0x0028AF48
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x0028BB68 File Offset: 0x0028AF68
		public string Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != null && 65535 < value.Length)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "Service");
				}
				this._options = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x0028BBA8 File Offset: 0x0028AFA8
		// (set) Token: 0x06002258 RID: 8792 RVA: 0x0028BBC8 File Offset: 0x0028AFC8
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "Timeout");
				}
				this._timeout = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x0028BBF8 File Offset: 0x0028AFF8
		// (set) Token: 0x0600225A RID: 8794 RVA: 0x0028BC18 File Offset: 0x0028B018
		public string UserData
		{
			get
			{
				return this._userData;
			}
			set
			{
				if (value != null && 65535 < value.Length)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "UserData");
				}
				this._userData = value;
			}
		}

		// Token: 0x04001660 RID: 5728
		private string _userData;

		// Token: 0x04001661 RID: 5729
		private string _options;

		// Token: 0x04001662 RID: 5730
		private int _timeout;
	}
}
