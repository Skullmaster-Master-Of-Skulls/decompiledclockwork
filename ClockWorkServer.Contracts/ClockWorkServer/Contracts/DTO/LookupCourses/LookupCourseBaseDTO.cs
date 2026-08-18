using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A0 RID: 1952
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(LookupCourseDTO))]
	public class LookupCourseBaseDTO : ICloneable<LookupCourseBaseDTO>, ICloneable
	{
		// Token: 0x0600280A RID: 10250 RVA: 0x00012D74 File Offset: 0x00010F74
		public LookupCourseBaseDTO()
		{
			this.Subject = new LookupSubjectDTO
			{
				SubjectDescription = ""
			};
			this.Term = "";
			this.Duration = "";
			this.Course = "";
			this.Section = "";
			this.TimeOfDay = "";
			this.Campus = "";
			this.Department = "";
			this.Location = "";
			this.CourseNote = "";
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x00012E0D File Offset: 0x0001100D
		public LookupCourseBaseDTO(LookupCourseBaseDTO item)
		{
			LookupCourseBaseDTO.CloneLookupCourseBaseItem<LookupCourseBaseDTO>(this, item);
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x00012E20 File Offset: 0x00011020
		public static void CloneLookupCourseBaseItem<T>(T clonedItem, T itemToClone) where T : LookupCourseBaseDTO
		{
			bool flag = clonedItem == null || itemToClone == null;
			if (!flag)
			{
				clonedItem.LuCourseId = itemToClone.LuCourseId;
				clonedItem.StartDate = itemToClone.StartDate;
				clonedItem.EndDate = itemToClone.EndDate;
				clonedItem.Duration = itemToClone.Duration;
				clonedItem.Term = itemToClone.Term;
				clonedItem.Subject = ((itemToClone.Subject == null) ? null : itemToClone.Subject.Clone());
				clonedItem.Course = itemToClone.Course;
				clonedItem.Section = itemToClone.Section;
				clonedItem.TimeOfDay = itemToClone.TimeOfDay;
				clonedItem.Campus = itemToClone.Campus;
				clonedItem.Department = itemToClone.Department;
				clonedItem.Location = itemToClone.Location;
				clonedItem.CourseNote = itemToClone.CourseNote;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x00012F8B File Offset: 0x0001118B
		// (set) Token: 0x0600280E RID: 10254 RVA: 0x00012F93 File Offset: 0x00011193
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x00012F9C File Offset: 0x0001119C
		// (set) Token: 0x06002810 RID: 10256 RVA: 0x00012FA4 File Offset: 0x000111A4
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x00012FAD File Offset: 0x000111AD
		// (set) Token: 0x06002812 RID: 10258 RVA: 0x00012FB5 File Offset: 0x000111B5
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x00012FBE File Offset: 0x000111BE
		// (set) Token: 0x06002814 RID: 10260 RVA: 0x00012FC6 File Offset: 0x000111C6
		[DataMember]
		public string Duration { get; set; }

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x00012FCF File Offset: 0x000111CF
		// (set) Token: 0x06002816 RID: 10262 RVA: 0x00012FD7 File Offset: 0x000111D7
		[DataMember]
		public string Term { get; set; }

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x00012FE0 File Offset: 0x000111E0
		// (set) Token: 0x06002818 RID: 10264 RVA: 0x00012FE8 File Offset: 0x000111E8
		[DataMember]
		public LookupSubjectDTO Subject { get; set; }

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x00012FF1 File Offset: 0x000111F1
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x00012FF9 File Offset: 0x000111F9
		[DataMember]
		public string Course { get; set; }

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x00013002 File Offset: 0x00011202
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x0001300A File Offset: 0x0001120A
		[DataMember]
		public string Section { get; set; }

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x00013013 File Offset: 0x00011213
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x0001301B File Offset: 0x0001121B
		[DataMember]
		public string TimeOfDay { get; set; }

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x00013024 File Offset: 0x00011224
		// (set) Token: 0x06002820 RID: 10272 RVA: 0x0001302C File Offset: 0x0001122C
		[DataMember]
		public string Campus { get; set; }

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x00013035 File Offset: 0x00011235
		// (set) Token: 0x06002822 RID: 10274 RVA: 0x0001303D File Offset: 0x0001123D
		[DataMember]
		public string Department { get; set; }

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x00013046 File Offset: 0x00011246
		// (set) Token: 0x06002824 RID: 10276 RVA: 0x0001304E File Offset: 0x0001124E
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00013057 File Offset: 0x00011257
		// (set) Token: 0x06002826 RID: 10278 RVA: 0x0001305F File Offset: 0x0001125F
		[DataMember]
		public string CourseNote { get; set; }

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x00013068 File Offset: 0x00011268
		// (set) Token: 0x06002828 RID: 10280 RVA: 0x00013070 File Offset: 0x00011270
		[DataMember]
		public decimal Credits { get; set; }

		// Token: 0x06002829 RID: 10281 RVA: 0x0001307C File Offset: 0x0001127C
		public LookupCourseBaseDTO Clone()
		{
			return new LookupCourseBaseDTO(this);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x00013094 File Offset: 0x00011294
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
