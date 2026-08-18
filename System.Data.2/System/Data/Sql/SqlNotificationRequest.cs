using System;
using System.Data.Common;

namespace System.Data.Sql
{
	// Token: 0x0200014D RID: 333
	public sealed class SqlNotificationRequest
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x0009A560 File Offset: 0x00099960
		public SqlNotificationRequest() : this(null, null, 0)
		{
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0009A578 File Offset: 0x00099978
		public SqlNotificationRequest(string userData, string options, int timeout)
		{
			this.UserData = userData;
			this.Timeout = timeout;
			this.Options = options;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0009A5A0 File Offset: 0x000999A0
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x0009A5B4 File Offset: 0x000999B4
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

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0009A5E8 File Offset: 0x000999E8
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x0009A5FC File Offset: 0x000999FC
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

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0009A624 File Offset: 0x00099A24
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x0009A638 File Offset: 0x00099A38
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

		// Token: 0x04000D3B RID: 3387
		private string _userData;

		// Token: 0x04000D3C RID: 3388
		private string _options;

		// Token: 0x04000D3D RID: 3389
		private int _timeout;
	}
}
