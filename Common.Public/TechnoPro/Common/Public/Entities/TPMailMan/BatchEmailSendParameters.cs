using System;

namespace TechnoPro.Common.Public.Entities.TPMailMan
{
	// Token: 0x02000166 RID: 358
	public class BatchEmailSendParameters : BusinessBase<int>
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00012004 File Offset: 0x00010204
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0000E258 File Offset: 0x0000C458
		public int EmailTemplateId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001201C File Offset: 0x0001021C
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x00012024 File Offset: 0x00010224
		public string Title { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0001202D File Offset: 0x0001022D
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x00012035 File Offset: 0x00010235
		public bool SendReport { get; set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0001203E File Offset: 0x0001023E
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x00012046 File Offset: 0x00010246
		public bool TestMode { get; set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001204F File Offset: 0x0001024F
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x00012057 File Offset: 0x00010257
		public string EmailTypeCode { get; set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00012060 File Offset: 0x00010260
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x00012068 File Offset: 0x00010268
		public string AdminEmail { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00012071 File Offset: 0x00010271
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00012079 File Offset: 0x00010279
		public int EmailDelay { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00012082 File Offset: 0x00010282
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0001208A File Offset: 0x0001028A
		public int AppIconEmailSent { get; set; }
	}
}
