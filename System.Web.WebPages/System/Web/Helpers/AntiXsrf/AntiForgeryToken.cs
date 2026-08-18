using System;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000030 RID: 48
	internal sealed class AntiForgeryToken
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004EEC File Offset: 0x000030EC
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00004EFD File Offset: 0x000030FD
		public string AdditionalData
		{
			get
			{
				return this._additionalData ?? string.Empty;
			}
			set
			{
				this._additionalData = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004F06 File Offset: 0x00003106
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00004F0E File Offset: 0x0000310E
		public BinaryBlob ClaimUid { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004F17 File Offset: 0x00003117
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00004F1F File Offset: 0x0000311F
		public bool IsSessionToken { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004F28 File Offset: 0x00003128
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00004F48 File Offset: 0x00003148
		public BinaryBlob SecurityToken
		{
			get
			{
				if (this._securityToken == null)
				{
					this._securityToken = new BinaryBlob(128);
				}
				return this._securityToken;
			}
			set
			{
				this._securityToken = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004F51 File Offset: 0x00003151
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00004F62 File Offset: 0x00003162
		public string Username
		{
			get
			{
				return this._username ?? string.Empty;
			}
			set
			{
				this._username = value;
			}
		}

		// Token: 0x04000066 RID: 102
		internal const int SecurityTokenBitLength = 128;

		// Token: 0x04000067 RID: 103
		internal const int ClaimUidBitLength = 256;

		// Token: 0x04000068 RID: 104
		private string _additionalData;

		// Token: 0x04000069 RID: 105
		private BinaryBlob _securityToken;

		// Token: 0x0400006A RID: 106
		private string _username;
	}
}
