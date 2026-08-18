using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200078C RID: 1932
	[DataContract(Namespace = "http://tpro.ca")]
	public class AlternateContactDTO
	{
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x00012BA8 File Offset: 0x00010DA8
		// (set) Token: 0x060027C2 RID: 10178 RVA: 0x00012BB0 File Offset: 0x00010DB0
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x00012BB9 File Offset: 0x00010DB9
		// (set) Token: 0x060027C4 RID: 10180 RVA: 0x00012BC1 File Offset: 0x00010DC1
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00012BCA File Offset: 0x00010DCA
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x00012BD2 File Offset: 0x00010DD2
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x00012BDB File Offset: 0x00010DDB
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x00012BE3 File Offset: 0x00010DE3
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x00012BEC File Offset: 0x00010DEC
		// (set) Token: 0x060027CA RID: 10186 RVA: 0x00012BF4 File Offset: 0x00010DF4
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x00012BFD File Offset: 0x00010DFD
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x00012C05 File Offset: 0x00010E05
		[DataMember]
		public int PermissionLevel { get; set; }

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x00012C0E File Offset: 0x00010E0E
		// (set) Token: 0x060027CE RID: 10190 RVA: 0x00012C16 File Offset: 0x00010E16
		[DataMember]
		public string EmployeeId { get; set; }
	}
}
