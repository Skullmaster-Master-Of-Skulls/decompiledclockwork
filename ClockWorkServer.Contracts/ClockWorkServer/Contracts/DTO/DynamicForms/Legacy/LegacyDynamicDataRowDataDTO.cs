using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy
{
	// Token: 0x020006B7 RID: 1719
	[DataContract(Namespace = "http://tpro.ca")]
	public class LegacyDynamicDataRowDataDTO
	{
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x0000FF5E File Offset: 0x0000E15E
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x0000FF66 File Offset: 0x0000E166
		[DataMember]
		public eLegacyDynamicDataRowState RowState { get; set; }

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x0000FF6F File Offset: 0x0000E16F
		// (set) Token: 0x060022F1 RID: 8945 RVA: 0x0000FF77 File Offset: 0x0000E177
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x0000FF80 File Offset: 0x0000E180
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x0000FF88 File Offset: 0x0000E188
		[DataMember]
		public int? ControlValueInt { get; set; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0000FF91 File Offset: 0x0000E191
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x0000FF99 File Offset: 0x0000E199
		[DataMember]
		public byte[] ControlValueBytes { get; set; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x0000FFA2 File Offset: 0x0000E1A2
		// (set) Token: 0x060022F7 RID: 8951 RVA: 0x0000FFAA File Offset: 0x0000E1AA
		[DataMember]
		public DateTime? ControlValueDateTime { get; set; }
	}
}
