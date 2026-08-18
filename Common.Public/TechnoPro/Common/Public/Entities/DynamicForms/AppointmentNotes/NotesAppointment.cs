using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes
{
	// Token: 0x020003AC RID: 940
	public class NotesAppointment
	{
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x00020BB9 File Offset: 0x0001EDB9
		// (set) Token: 0x06001C90 RID: 7312 RVA: 0x00020BC1 File Offset: 0x0001EDC1
		public int AppointmentId { get; set; }

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x00020BCA File Offset: 0x0001EDCA
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x00020BD2 File Offset: 0x0001EDD2
		public DateTime DateBooked { get; set; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x00020BDB File Offset: 0x0001EDDB
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x00020BE3 File Offset: 0x0001EDE3
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x00020BEC File Offset: 0x0001EDEC
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x00020BF4 File Offset: 0x0001EDF4
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00020BFD File Offset: 0x0001EDFD
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x00020C05 File Offset: 0x0001EE05
		public AppType AppointmentType { get; set; }

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00020C0E File Offset: 0x0001EE0E
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x00020C16 File Offset: 0x0001EE16
		public AppShowTimeAsType ShowTimeAs { get; set; }

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00020C1F File Offset: 0x0001EE1F
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x00020C27 File Offset: 0x0001EE27
		public bool IsPrimaryStudentNoShow { get; set; }

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00020C30 File Offset: 0x0001EE30
		// (set) Token: 0x06001C9E RID: 7326 RVA: 0x00020C38 File Offset: 0x0001EE38
		public string CancelReason { get; set; }

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00020C41 File Offset: 0x0001EE41
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x00020C49 File Offset: 0x0001EE49
		public bool HasNotes { get; set; }

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00020C52 File Offset: 0x0001EE52
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x00020C5A File Offset: 0x0001EE5A
		public bool IsCancelled { get; set; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x00020C63 File Offset: 0x0001EE63
		// (set) Token: 0x06001CA4 RID: 7332 RVA: 0x00020C6B File Offset: 0x0001EE6B
		public PersonBase PrimaryStudent { get; set; }

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00020C74 File Offset: 0x0001EE74
		// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x00020C7C File Offset: 0x0001EE7C
		public IList<Attendee> Attendees { get; set; }

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00020C85 File Offset: 0x0001EE85
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x00020C8D File Offset: 0x0001EE8D
		public string MemoText { get; set; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00020C96 File Offset: 0x0001EE96
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x00020C9E File Offset: 0x0001EE9E
		public string Subtitle { get; set; }

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x00020CA7 File Offset: 0x0001EEA7
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x00020CAF File Offset: 0x0001EEAF
		public bool IsPrivate { get; set; }

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x00020CB8 File Offset: 0x0001EEB8
		// (set) Token: 0x06001CAE RID: 7342 RVA: 0x00020CC0 File Offset: 0x0001EEC0
		public bool IsLocked { get; set; }

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x00020CC9 File Offset: 0x0001EEC9
		// (set) Token: 0x06001CB0 RID: 7344 RVA: 0x00020CD1 File Offset: 0x0001EED1
		public int WhoBookedPersonId { get; set; }
	}
}
