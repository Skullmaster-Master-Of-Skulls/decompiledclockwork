using System;
using System.Collections.Specialized;

namespace Telerik.Pdf.Security
{
	// Token: 0x0200167A RID: 5754
	public class SecurityOptions
	{
		// Token: 0x1700440B RID: 17419
		// (get) Token: 0x0600DE7E RID: 56958 RVA: 0x00309B11 File Offset: 0x00307D11
		// (set) Token: 0x0600DE7F RID: 56959 RVA: 0x00309B19 File Offset: 0x00307D19
		protected string m_ownerPassword { get; set; }

		// Token: 0x1700440C RID: 17420
		// (get) Token: 0x0600DE80 RID: 56960 RVA: 0x00309B22 File Offset: 0x00307D22
		// (set) Token: 0x0600DE81 RID: 56961 RVA: 0x00309B2A File Offset: 0x00307D2A
		protected string m_userPassword { get; set; }

		// Token: 0x1700440D RID: 17421
		// (get) Token: 0x0600DE82 RID: 56962 RVA: 0x00309B33 File Offset: 0x00307D33
		// (set) Token: 0x0600DE83 RID: 56963 RVA: 0x00309B3B File Offset: 0x00307D3B
		public string OwnerPassword
		{
			get
			{
				return this.m_ownerPassword;
			}
			set
			{
				this.m_ownerPassword = value;
			}
		}

		// Token: 0x1700440E RID: 17422
		// (get) Token: 0x0600DE84 RID: 56964 RVA: 0x00309B44 File Offset: 0x00307D44
		// (set) Token: 0x0600DE85 RID: 56965 RVA: 0x00309B4C File Offset: 0x00307D4C
		public string UserPassword
		{
			get
			{
				return this.m_userPassword;
			}
			set
			{
				this.m_userPassword = value;
			}
		}

		// Token: 0x1700440F RID: 17423
		// (get) Token: 0x0600DE86 RID: 56966 RVA: 0x00309B55 File Offset: 0x00307D55
		// (set) Token: 0x0600DE87 RID: 56967 RVA: 0x00309B62 File Offset: 0x00307D62
		public int Permissions
		{
			get
			{
				return this.m_permissions.Data;
			}
			set
			{
				this.m_permissions = new BitVector32(value);
			}
		}

		// Token: 0x0600DE88 RID: 56968 RVA: 0x00309B70 File Offset: 0x00307D70
		public void EnablePrinting(bool enable)
		{
			this.m_permissions[4] = enable;
		}

		// Token: 0x0600DE89 RID: 56969 RVA: 0x00309B7F File Offset: 0x00307D7F
		public void EnableChanging(bool enable)
		{
			this.m_permissions[8] = enable;
		}

		// Token: 0x0600DE8A RID: 56970 RVA: 0x00309B8E File Offset: 0x00307D8E
		public void EnableCopying(bool enable)
		{
			this.m_permissions[16] = enable;
		}

		// Token: 0x0600DE8B RID: 56971 RVA: 0x00309B9E File Offset: 0x00307D9E
		public void EnableAdding(bool enable)
		{
			this.m_permissions[32] = enable;
		}

		// Token: 0x04003FFE RID: 16382
		private BitVector32 m_permissions = new BitVector32(-4);
	}
}
