using System;
using System.Security.Cryptography.X509Certificates;

namespace TechnoPro.Common.Public.Entities.Authentication.ADFS
{
	// Token: 0x020004A6 RID: 1190
	public class AdfsParameters
	{
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00027339 File Offset: 0x00025539
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x00027341 File Offset: 0x00025541
		public string IssuerName { get; set; }

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x0002734A File Offset: 0x0002554A
		// (set) Token: 0x060023DD RID: 9181 RVA: 0x00027352 File Offset: 0x00025552
		public string UriToken { get; set; }

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x0002735B File Offset: 0x0002555B
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x00027363 File Offset: 0x00025563
		public StoreLocation StoreLocation { get; set; }

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0002736C File Offset: 0x0002556C
		// (set) Token: 0x060023E1 RID: 9185 RVA: 0x00027374 File Offset: 0x00025574
		public StoreName StoreName { get; set; }

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x0002737D File Offset: 0x0002557D
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x00027385 File Offset: 0x00025585
		public string CertificateThumbprint { get; set; }
	}
}
