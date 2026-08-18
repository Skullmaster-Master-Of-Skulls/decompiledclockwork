using System;

namespace Telerik.Licensing
{
	// Token: 0x0200041F RID: 1055
	internal class DesignTimeKey : ILicenseKey
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x0007D121 File Offset: 0x0007B321
		public DesignTimeKey()
		{
			this.Key = this.ReadKey();
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x0007D135 File Offset: 0x0007B335
		public DesignTimeKey(string instalaltionId)
		{
			this.Key = instalaltionId;
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x0007D144 File Offset: 0x0007B344
		// (set) Token: 0x06002606 RID: 9734 RVA: 0x0007D14C File Offset: 0x0007B34C
		public string Key { get; set; }

		// Token: 0x06002607 RID: 9735 RVA: 0x0007D155 File Offset: 0x0007B355
		public bool IsValid()
		{
			return true;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0007D158 File Offset: 0x0007B358
		private string ReadKey()
		{
			return string.Empty;
		}
	}
}
