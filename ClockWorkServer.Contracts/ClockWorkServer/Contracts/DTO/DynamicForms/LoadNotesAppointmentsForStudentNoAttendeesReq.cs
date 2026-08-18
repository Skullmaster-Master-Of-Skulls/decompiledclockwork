using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200062E RID: 1582
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotesAppointmentsForStudentNoAttendeesReq : BaseMessageReq
	{
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0000E9D3 File Offset: 0x0000CBD3
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x0000E9DB File Offset: 0x0000CBDB
		[DataMember]
		public int PrimaryStudentPersonId { get; set; }

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		// (set) Token: 0x06002035 RID: 8245 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		[DataMember]
		public DateTime? StartDate { get; set; }

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x0000E9F5 File Offset: 0x0000CBF5
		// (set) Token: 0x06002037 RID: 8247 RVA: 0x0000E9FD File Offset: 0x0000CBFD
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0000EA06 File Offset: 0x0000CC06
		// (set) Token: 0x06002039 RID: 8249 RVA: 0x0000EA0E File Offset: 0x0000CC0E
		[DataMember]
		public IList<int> AppTypeIds { get; set; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0000EA17 File Offset: 0x0000CC17
		// (set) Token: 0x0600203B RID: 8251 RVA: 0x0000EA1F File Offset: 0x0000CC1F
		[DataMember]
		public IList<int> ScreenNums { get; set; }
	}
}
