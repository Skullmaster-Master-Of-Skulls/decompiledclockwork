using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200033E RID: 830
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportDTO
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00008BDF File Offset: 0x00006DDF
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x00008BE7 File Offset: 0x00006DE7
		[DataMember]
		public int ReportId { get; set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00008BF0 File Offset: 0x00006DF0
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x00008BF8 File Offset: 0x00006DF8
		[DataMember]
		public List<ReportFunctionDTO> Functions { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00008C01 File Offset: 0x00006E01
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00008C09 File Offset: 0x00006E09
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00008C12 File Offset: 0x00006E12
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00008C1A File Offset: 0x00006E1A
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00008C23 File Offset: 0x00006E23
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x00008C2B File Offset: 0x00006E2B
		[DataMember]
		public DateTime DateLastExecuted { get; set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00008C34 File Offset: 0x00006E34
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x00008C3C File Offset: 0x00006E3C
		[DataMember]
		public PersonBaseDTO WhoLastExecuted { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00008C45 File Offset: 0x00006E45
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x00008C4D File Offset: 0x00006E4D
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00008C56 File Offset: 0x00006E56
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x00008C5E File Offset: 0x00006E5E
		[DataMember]
		public PersonBaseDTO WhoCreated { get; set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00008C67 File Offset: 0x00006E67
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00008C6F File Offset: 0x00006E6F
		[DataMember]
		public DateTime DateLastModified { get; set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00008C78 File Offset: 0x00006E78
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00008C80 File Offset: 0x00006E80
		[DataMember]
		public PersonBaseDTO WhoLastModified { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00008C89 File Offset: 0x00006E89
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x00008C91 File Offset: 0x00006E91
		[DataMember]
		public ReportParametersLegacyDTO LegacyParameters { get; set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00008C9A File Offset: 0x00006E9A
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x00008CA2 File Offset: 0x00006EA2
		[DataMember]
		public ReportParameterFormDTO ParameterForm { get; set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00008CAB File Offset: 0x00006EAB
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x00008CB3 File Offset: 0x00006EB3
		[DataMember]
		public IList<ReportParameterDTO> ReportParameters { get; set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00008CBC File Offset: 0x00006EBC
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x00008CC4 File Offset: 0x00006EC4
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00008CCD File Offset: 0x00006ECD
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x00008CD5 File Offset: 0x00006ED5
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00008CDE File Offset: 0x00006EDE
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00008CE6 File Offset: 0x00006EE6
		[DataMember]
		public bool IsTechnoProReport { get; set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00008CEF File Offset: 0x00006EEF
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x00008CF7 File Offset: 0x00006EF7
		[DataMember]
		public bool FunctionParametersAreEncrypted { get; set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00008D00 File Offset: 0x00006F00
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x00008D08 File Offset: 0x00006F08
		[DataMember]
		public IList<FormattedReportDTO> FormattedReports { get; set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00008D11 File Offset: 0x00006F11
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x00008D19 File Offset: 0x00006F19
		[DataMember]
		public ReportOptionsDTO ReportOptions { get; set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00008D22 File Offset: 0x00006F22
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x00008D2A File Offset: 0x00006F2A
		[DataMember]
		public bool IsBuiltByTpro { get; set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00008D33 File Offset: 0x00006F33
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x00008D3B File Offset: 0x00006F3B
		[DataMember]
		public byte[] BuiltByTproSignedAndEncryptedReportXml { get; set; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00008D44 File Offset: 0x00006F44
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x00008D4C File Offset: 0x00006F4C
		[DataMember]
		public Guid ReportUniqueId { get; set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00008D55 File Offset: 0x00006F55
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x00008D5D File Offset: 0x00006F5D
		[DataMember]
		public string CreatedByLocation { get; set; }
	}
}
