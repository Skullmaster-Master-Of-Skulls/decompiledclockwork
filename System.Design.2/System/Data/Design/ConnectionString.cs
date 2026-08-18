using System;

namespace System.Data.Design
{
	// Token: 0x02000219 RID: 537
	internal class ConnectionString
	{
		// Token: 0x060013F7 RID: 5111 RVA: 0x00070B70 File Offset: 0x0006ED70
		public ConnectionString(string providerName, string connectionString)
		{
			this.connectionString = connectionString;
			this.providerName = providerName;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00070B86 File Offset: 0x0006ED86
		public string ToFullString()
		{
			return this.connectionString.ToString();
		}

		// Token: 0x04000AAA RID: 2730
		private string providerName;

		// Token: 0x04000AAB RID: 2731
		private string connectionString;
	}
}
