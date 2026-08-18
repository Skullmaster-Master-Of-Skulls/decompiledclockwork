using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy
{
	// Token: 0x020006B9 RID: 1721
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveDataPSReq : BaseMessageReq
	{
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x0000FFD5 File Offset: 0x0000E1D5
		// (set) Token: 0x060022FF RID: 8959 RVA: 0x0000FFDD File Offset: 0x0000E1DD
		[DataMember]
		public LegacyDynamicDataRowDatasDTO LegacyData { get; set; }

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x0000FFE6 File Offset: 0x0000E1E6
		// (set) Token: 0x06002301 RID: 8961 RVA: 0x0000FFEE File Offset: 0x0000E1EE
		[DataMember]
		public string TableName { get; set; }

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x0000FFF7 File Offset: 0x0000E1F7
		// (set) Token: 0x06002303 RID: 8963 RVA: 0x0000FFFF File Offset: 0x0000E1FF
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x00010008 File Offset: 0x0000E208
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x00010010 File Offset: 0x0000E210
		[DataMember]
		public int StudentPid { get; set; }

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x00010019 File Offset: 0x0000E219
		// (set) Token: 0x06002307 RID: 8967 RVA: 0x00010021 File Offset: 0x0000E221
		[DataMember]
		public int WhoModifiedPid { get; set; }

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0001002A File Offset: 0x0000E22A
		// (set) Token: 0x06002309 RID: 8969 RVA: 0x00010032 File Offset: 0x0000E232
		[DataMember]
		public bool TablesStoreScreenNum { get; set; }
	}
}
