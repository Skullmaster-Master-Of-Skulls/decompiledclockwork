using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Settings
{
	// Token: 0x02000269 RID: 617
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetValuesFromColumnReq : BaseMessageReq
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00006AB7 File Offset: 0x00004CB7
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x00006ABF File Offset: 0x00004CBF
		[DataMember]
		public string TableName { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00006AC8 File Offset: 0x00004CC8
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x00006AD0 File Offset: 0x00004CD0
		[DataMember]
		public string IdColumnName { get; set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00006AD9 File Offset: 0x00004CD9
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x00006AE1 File Offset: 0x00004CE1
		[DataMember]
		public string ColumnName { get; set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00006AEA File Offset: 0x00004CEA
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x00006AF2 File Offset: 0x00004CF2
		[DataMember]
		public bool IsValueEncrypted { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00006AFB File Offset: 0x00004CFB
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x00006B03 File Offset: 0x00004D03
		[DataMember]
		public string OverrideSql { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00006B0C File Offset: 0x00004D0C
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x00006B14 File Offset: 0x00004D14
		[DataMember]
		public bool OverrideSortByDisplayName { get; set; }
	}
}
