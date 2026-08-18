using System;

namespace System.Web.Optimization
{
	// Token: 0x02000026 RID: 38
	public class OptimizationSettings
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004EF9 File Offset: 0x000030F9
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00004F01 File Offset: 0x00003101
		public string ApplicationPath { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004F0A File Offset: 0x0000310A
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00004F12 File Offset: 0x00003112
		public BundleCollection BundleTable { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004F1B File Offset: 0x0000311B
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00004F23 File Offset: 0x00003123
		public string BundleManifestPath { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004F2C File Offset: 0x0000312C
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00004F34 File Offset: 0x00003134
		public Action<BundleCollection> BundleSetupMethod { get; set; }
	}
}
