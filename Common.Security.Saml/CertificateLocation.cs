using System;
using System.Security.Cryptography.X509Certificates;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000006 RID: 6
	public class CertificateLocation
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024B2 File Offset: 0x000006B2
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024BA File Offset: 0x000006BA
		public StoreLocation StoreLocation { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024C3 File Offset: 0x000006C3
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000024CB File Offset: 0x000006CB
		public StoreName StoreName { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024D4 File Offset: 0x000006D4
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000024DC File Offset: 0x000006DC
		public X509FindType FindType { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024E5 File Offset: 0x000006E5
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000024ED File Offset: 0x000006ED
		public string FindValue { get; set; }
	}
}
