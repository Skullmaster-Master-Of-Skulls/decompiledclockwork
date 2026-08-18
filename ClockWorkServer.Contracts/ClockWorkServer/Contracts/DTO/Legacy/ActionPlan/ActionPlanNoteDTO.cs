using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ActionPlan
{
	// Token: 0x020004E9 RID: 1257
	[DataContract(Namespace = "http://tpro.ca")]
	public class ActionPlanNoteDTO
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0000C493 File Offset: 0x0000A693
		// (set) Token: 0x06001A9D RID: 6813 RVA: 0x0000C49B File Offset: 0x0000A69B
		[DataMember]
		public int NoteId { get; set; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		// (set) Token: 0x06001A9F RID: 6815 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0000C4B5 File Offset: 0x0000A6B5
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x0000C4BD File Offset: 0x0000A6BD
		[DataMember]
		public int WhoAddedPersonId { get; set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0000C4C6 File Offset: 0x0000A6C6
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x0000C4CE File Offset: 0x0000A6CE
		[DataMember]
		public int WhoLastModifiedPersonId { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0000C4D7 File Offset: 0x0000A6D7
		// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x0000C4DF File Offset: 0x0000A6DF
		[DataMember]
		public string NoteGroup { get; set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		// (set) Token: 0x06001AA7 RID: 6823 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		[DataMember]
		public string NoteDescription { get; set; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0000C4F9 File Offset: 0x0000A6F9
		// (set) Token: 0x06001AA9 RID: 6825 RVA: 0x0000C501 File Offset: 0x0000A701
		[DataMember]
		public string StaffNotes { get; set; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0000C50A File Offset: 0x0000A70A
		// (set) Token: 0x06001AAB RID: 6827 RVA: 0x0000C512 File Offset: 0x0000A712
		[DataMember]
		public DateTime? DateLastModified { get; set; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0000C51B File Offset: 0x0000A71B
		// (set) Token: 0x06001AAD RID: 6829 RVA: 0x0000C523 File Offset: 0x0000A723
		[DataMember]
		public DateTime DateAdded { get; set; }

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0000C52C File Offset: 0x0000A72C
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x0000C534 File Offset: 0x0000A734
		[DataMember]
		public PersonBaseDTO WhoLastModified { get; set; }
	}
}
