using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000224 RID: 548
	public class Report : BusinessBase<int>
	{
		// Token: 0x060010AA RID: 4266 RVA: 0x00017A6C File Offset: 0x00015C6C
		public Report()
		{
			this.Functions = new List<ReportFunction>();
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00017A84 File Offset: 0x00015C84
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ReportId
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

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00017A9C File Offset: 0x00015C9C
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x00017AA4 File Offset: 0x00015CA4
		public List<ReportFunction> Functions { get; set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00017AAD File Offset: 0x00015CAD
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x00017AB5 File Offset: 0x00015CB5
		public string Title { get; set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00017ABE File Offset: 0x00015CBE
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x00017AC6 File Offset: 0x00015CC6
		public string Description { get; set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00017ACF File Offset: 0x00015CCF
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00017AD7 File Offset: 0x00015CD7
		public DateTime DateLastExecuted { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00017AE0 File Offset: 0x00015CE0
		// (set) Token: 0x060010B6 RID: 4278 RVA: 0x00017AE8 File Offset: 0x00015CE8
		public PersonBase WhoLastExecuted { get; set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00017AF1 File Offset: 0x00015CF1
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00017AF9 File Offset: 0x00015CF9
		public DateTime DateCreated { get; set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00017B02 File Offset: 0x00015D02
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x00017B0A File Offset: 0x00015D0A
		public PersonBase WhoCreated { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00017B13 File Offset: 0x00015D13
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x00017B1B File Offset: 0x00015D1B
		public DateTime DateLastModified { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00017B24 File Offset: 0x00015D24
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x00017B2C File Offset: 0x00015D2C
		public PersonBase WhoLastModified { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00017B35 File Offset: 0x00015D35
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00017B3D File Offset: 0x00015D3D
		public ReportParametersLegacy LegacyParameters { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00017B46 File Offset: 0x00015D46
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00017B4E File Offset: 0x00015D4E
		public ReportParameterForm ParameterForm { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00017B57 File Offset: 0x00015D57
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00017B5F File Offset: 0x00015D5F
		public IList<ReportParameter> ReportParameters { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00017B68 File Offset: 0x00015D68
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00017B70 File Offset: 0x00015D70
		public int GroupId { get; set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00017B79 File Offset: 0x00015D79
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00017B81 File Offset: 0x00015D81
		public int OrderNum { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00017B8A File Offset: 0x00015D8A
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00017B92 File Offset: 0x00015D92
		public bool IsTechnoProReport { get; set; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00017B9B File Offset: 0x00015D9B
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00017BA3 File Offset: 0x00015DA3
		public bool FunctionParametersAreEncrypted { get; set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00017BAC File Offset: 0x00015DAC
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00017BB4 File Offset: 0x00015DB4
		public IList<FormattedReport> FormattedReports { get; set; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00017BBD File Offset: 0x00015DBD
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00017BC5 File Offset: 0x00015DC5
		public ReportOptions ReportOptions { get; set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00017BCE File Offset: 0x00015DCE
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00017BD6 File Offset: 0x00015DD6
		public bool IsBuiltByTpro { get; set; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00017BDF File Offset: 0x00015DDF
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x00017BE7 File Offset: 0x00015DE7
		public byte[] BuiltByTproSignedAndEncryptedReportXml { get; set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00017BF0 File Offset: 0x00015DF0
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public Guid ReportUniqueId { get; set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00017C01 File Offset: 0x00015E01
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00017C09 File Offset: 0x00015E09
		public string CreatedByLocation { get; set; }
	}
}
