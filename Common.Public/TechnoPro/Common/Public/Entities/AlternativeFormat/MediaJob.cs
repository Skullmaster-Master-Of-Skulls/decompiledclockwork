using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000590 RID: 1424
	public class MediaJob : BusinessBase<int>
	{
		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x00032E4C File Offset: 0x0003104C
		// (set) Token: 0x06002E1F RID: 11807 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int MediaJobId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x00032E64 File Offset: 0x00031064
		// (set) Token: 0x06002E21 RID: 11809 RVA: 0x00032E6C File Offset: 0x0003106C
		public virtual string JobTitle { get; set; }

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x00032E75 File Offset: 0x00031075
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x00032E7D File Offset: 0x0003107D
		public virtual MediaContent MediaContent { get; set; }

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x00032E86 File Offset: 0x00031086
		// (set) Token: 0x06002E25 RID: 11813 RVA: 0x00032E8E File Offset: 0x0003108E
		public virtual MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x00032E97 File Offset: 0x00031097
		// (set) Token: 0x06002E27 RID: 11815 RVA: 0x00032E9F File Offset: 0x0003109F
		public virtual int MediaContentPerFormatId { get; set; }

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x00032EA8 File Offset: 0x000310A8
		// (set) Token: 0x06002E29 RID: 11817 RVA: 0x00032EB0 File Offset: 0x000310B0
		public virtual DateTime JobStartTime { get; set; }

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x00032EB9 File Offset: 0x000310B9
		// (set) Token: 0x06002E2B RID: 11819 RVA: 0x00032EC1 File Offset: 0x000310C1
		public virtual DateTime JobDueDate { get; set; }

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x00032ECA File Offset: 0x000310CA
		// (set) Token: 0x06002E2D RID: 11821 RVA: 0x00032ED2 File Offset: 0x000310D2
		public virtual string JobCurrentStatusNameAboutPublisher { get; set; }

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x00032EDB File Offset: 0x000310DB
		// (set) Token: 0x06002E2F RID: 11823 RVA: 0x00032EE3 File Offset: 0x000310E3
		public virtual string JobCurrentStatusNameAboutVendor { get; set; }

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x00032EEC File Offset: 0x000310EC
		// (set) Token: 0x06002E31 RID: 11825 RVA: 0x00032EF4 File Offset: 0x000310F4
		public virtual string JobCurrentStatusNameAboutInHouse { get; set; }

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x00032EFD File Offset: 0x000310FD
		// (set) Token: 0x06002E33 RID: 11827 RVA: 0x00032F05 File Offset: 0x00031105
		public virtual string JobCurrentStatusNameGeneral { get; set; }

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x00032F0E File Offset: 0x0003110E
		// (set) Token: 0x06002E35 RID: 11829 RVA: 0x00032F16 File Offset: 0x00031116
		public virtual PersonBase AssignedTo { get; set; }

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x00032F1F File Offset: 0x0003111F
		// (set) Token: 0x06002E37 RID: 11831 RVA: 0x00032F27 File Offset: 0x00031127
		public virtual bool IsCompleted { get; set; }

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x00032F30 File Offset: 0x00031130
		// (set) Token: 0x06002E39 RID: 11833 RVA: 0x00032F38 File Offset: 0x00031138
		public virtual bool IsCancelled { get; set; }

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x00032F41 File Offset: 0x00031141
		// (set) Token: 0x06002E3B RID: 11835 RVA: 0x00032F49 File Offset: 0x00031149
		public virtual PersonBase WhoCreatedJob { get; set; }

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x00032F52 File Offset: 0x00031152
		// (set) Token: 0x06002E3D RID: 11837 RVA: 0x00032F5A File Offset: 0x0003115A
		public virtual eMediaJobPriority JobPriority { get; set; }

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x00032F63 File Offset: 0x00031163
		// (set) Token: 0x06002E3F RID: 11839 RVA: 0x00032F6B File Offset: 0x0003116B
		public virtual SchoolCampus Campus { get; set; }

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x00032F74 File Offset: 0x00031174
		// (set) Token: 0x06002E41 RID: 11841 RVA: 0x00032F7C File Offset: 0x0003117C
		public PersonBase WhoMadeChange { get; set; }

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06002E42 RID: 11842 RVA: 0x00032F85 File Offset: 0x00031185
		// (set) Token: 0x06002E43 RID: 11843 RVA: 0x00032F8D File Offset: 0x0003118D
		public string ChangeReason { get; set; }

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x00032F96 File Offset: 0x00031196
		// (set) Token: 0x06002E45 RID: 11845 RVA: 0x00032F9E File Offset: 0x0003119E
		public int StartPageIndex { get; set; }

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x00032FA7 File Offset: 0x000311A7
		// (set) Token: 0x06002E47 RID: 11847 RVA: 0x00032FAF File Offset: 0x000311AF
		public int EndPageIndex { get; set; }
	}
}
