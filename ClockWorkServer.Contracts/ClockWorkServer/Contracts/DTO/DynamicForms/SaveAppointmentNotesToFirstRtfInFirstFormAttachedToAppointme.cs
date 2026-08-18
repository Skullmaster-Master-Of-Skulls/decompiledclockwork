using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200062B RID: 1579
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq : BaseMessageReq
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x0000E94B File Offset: 0x0000CB4B
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x0000E953 File Offset: 0x0000CB53
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0000E95C File Offset: 0x0000CB5C
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x0000E964 File Offset: 0x0000CB64
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0000E96D File Offset: 0x0000CB6D
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x0000E975 File Offset: 0x0000CB75
		[DataMember]
		public int AppTypeId { get; set; }

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0000E97E File Offset: 0x0000CB7E
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x0000E986 File Offset: 0x0000CB86
		[DataMember]
		public string NotesRtf { get; set; }
	}
}
