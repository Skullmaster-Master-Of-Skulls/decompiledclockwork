using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field
{
	// Token: 0x0200075B RID: 1883
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataInstanceDTO
	{
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x00011FC8 File Offset: 0x000101C8
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x00011FD0 File Offset: 0x000101D0
		[DataMember]
		public Guid DataInstanceId { get; set; }

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x00011FD9 File Offset: 0x000101D9
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x00011FE1 File Offset: 0x000101E1
		[DataMember]
		public eCustomDataPrimitiveType DataType { get; set; }

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x00011FEA File Offset: 0x000101EA
		// (set) Token: 0x060026C7 RID: 9927 RVA: 0x00011FF2 File Offset: 0x000101F2
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x00011FFB File Offset: 0x000101FB
		// (set) Token: 0x060026C9 RID: 9929 RVA: 0x00012003 File Offset: 0x00010203
		[DataMember]
		public eCustomDataPurposeCode Purpose { get; set; }

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0001200C File Offset: 0x0001020C
		// (set) Token: 0x060026CB RID: 9931 RVA: 0x00012014 File Offset: 0x00010214
		[DataMember]
		public bool IsHidden { get; set; }
	}
}
