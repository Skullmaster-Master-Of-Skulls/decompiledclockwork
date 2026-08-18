using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000634 RID: 1588
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(byte[]))]
	public class DynamicDataBaseDTO
	{
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0000EA9F File Offset: 0x0000CC9F
		// (set) Token: 0x06002051 RID: 8273 RVA: 0x0000EAA7 File Offset: 0x0000CCA7
		[DataMember]
		public int DataId { get; set; }

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		[DataMember]
		public object Value { get; set; }

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0000EAC1 File Offset: 0x0000CCC1
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x0000EAC9 File Offset: 0x0000CCC9
		[DataMember]
		public int ValueId { get; set; }

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x0000EAD2 File Offset: 0x0000CCD2
		// (set) Token: 0x06002057 RID: 8279 RVA: 0x0000EADA File Offset: 0x0000CCDA
		[DataMember]
		public int ControlId { get; set; }
	}
}
