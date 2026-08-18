using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A4 RID: 1956
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupCourseDTO : LookupCourseBaseDTO
	{
		// Token: 0x0600283B RID: 10299 RVA: 0x00013184 File Offset: 0x00011384
		public LookupCourseDTO()
		{
			this.Instructors = new List<LookupInstructorDTO>();
			this.TimetableItems = new List<LookupTimetableItemDTO>();
			this.AlternateContacts = new List<AlternateContactDTO>();
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x000131B2 File Offset: 0x000113B2
		// (set) Token: 0x0600283D RID: 10301 RVA: 0x000131BA File Offset: 0x000113BA
		[DataMember]
		public List<LookupInstructorDTO> Instructors { get; set; }

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x000131C3 File Offset: 0x000113C3
		// (set) Token: 0x0600283F RID: 10303 RVA: 0x000131CB File Offset: 0x000113CB
		[DataMember]
		public List<LookupTimetableItemDTO> TimetableItems { get; set; }

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000131D4 File Offset: 0x000113D4
		// (set) Token: 0x06002841 RID: 10305 RVA: 0x000131DC File Offset: 0x000113DC
		[DataMember]
		public List<AlternateContactDTO> AlternateContacts { get; set; }

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000131E5 File Offset: 0x000113E5
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x000131ED File Offset: 0x000113ED
		[DataMember]
		public bool IsExemptFromDataSync { get; set; }

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x000131F6 File Offset: 0x000113F6
		// (set) Token: 0x06002845 RID: 10309 RVA: 0x000131FE File Offset: 0x000113FE
		[DataMember]
		public string ExternalCourseId { get; set; }

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x00013207 File Offset: 0x00011407
		// (set) Token: 0x06002847 RID: 10311 RVA: 0x0001320F File Offset: 0x0001140F
		[DataMember]
		public int BatchDataSyncLogId { get; set; }
	}
}
