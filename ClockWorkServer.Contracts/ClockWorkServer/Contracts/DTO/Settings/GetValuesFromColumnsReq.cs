using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Settings
{
	// Token: 0x0200026B RID: 619
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetValuesFromColumnsReq : BaseMessageReq
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x00006B2E File Offset: 0x00004D2E
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x00006B36 File Offset: 0x00004D36
		[DataMember]
		public string TableName { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x00006B3F File Offset: 0x00004D3F
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x00006B47 File Offset: 0x00004D47
		[DataMember]
		public string IdColumnName { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00006B50 File Offset: 0x00004D50
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x00006B58 File Offset: 0x00004D58
		[DataMember]
		public string[] ColumnNames { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00006B61 File Offset: 0x00004D61
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00006B69 File Offset: 0x00004D69
		[DataMember]
		public bool[] IsValueEncrypted { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00006B72 File Offset: 0x00004D72
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00006B7A File Offset: 0x00004D7A
		[DataMember]
		public string OverrideSql { get; set; }
	}
}
