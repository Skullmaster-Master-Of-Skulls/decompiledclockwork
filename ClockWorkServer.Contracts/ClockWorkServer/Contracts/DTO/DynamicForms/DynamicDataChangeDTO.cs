using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000635 RID: 1589
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicDataChangeDTO
	{
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0000EAE3 File Offset: 0x0000CCE3
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x0000EAEB File Offset: 0x0000CCEB
		[DataMember]
		public int Id { get; set; }

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x0000EAFC File Offset: 0x0000CCFC
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x0000EB05 File Offset: 0x0000CD05
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x0000EB0D File Offset: 0x0000CD0D
		[DataMember]
		public DynamicDataDTO Data { get; set; }

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x0000EB16 File Offset: 0x0000CD16
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x0000EB1E File Offset: 0x0000CD1E
		[DataMember]
		public object PreviousValue { get; set; }

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x0000EB27 File Offset: 0x0000CD27
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x0000EB2F File Offset: 0x0000CD2F
		[DataMember]
		public DateTime LastDateOfChange { get; set; }

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x0000EB38 File Offset: 0x0000CD38
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x0000EB40 File Offset: 0x0000CD40
		[DataMember]
		public PersonBaseDTO WhoLastChanged { get; set; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x0000EB49 File Offset: 0x0000CD49
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x0000EB51 File Offset: 0x0000CD51
		[DataMember]
		public eDynamicDataChangeActionDTO ChangeAction { get; set; }
	}
}
