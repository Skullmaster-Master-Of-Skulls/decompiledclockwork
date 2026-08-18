using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA1 RID: 2977
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaJobDTO : ICloneable<MediaJobDTO>, ICloneable
	{
		// Token: 0x06003ED8 RID: 16088 RVA: 0x000036BD File Offset: 0x000018BD
		public MediaJobDTO()
		{
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x0001ED4C File Offset: 0x0001CF4C
		public MediaJobDTO(MediaJobDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.JobTitle = item.JobTitle;
				this.MediaContent = item.MediaContent;
				this.MediaContentFormat = item.MediaContentFormat;
				this.MediaContentPerFormatId = item.MediaContentPerFormatId;
				this.JobStartTime = item.JobStartTime;
				this.JobDueDate = item.JobDueDate;
				this.JobCurrentStatusNameAboutPublisher = item.JobCurrentStatusNameAboutPublisher;
				this.JobCurrentStatusNameAboutVendor = item.JobCurrentStatusNameAboutVendor;
				this.JobCurrentStatusNameAboutInHouse = item.JobCurrentStatusNameGeneral;
				this.AssignedTo = item.AssignedTo;
				this.IsCompleted = item.IsCompleted;
				this.IsCancelled = item.IsCancelled;
				this.WhoCreatedJob = item.WhoCreatedJob;
				this.JobPriority = item.JobPriority;
				this.Campus = item.Campus;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x0001EE31 File Offset: 0x0001D031
		// (set) Token: 0x06003EDB RID: 16091 RVA: 0x0001EE39 File Offset: 0x0001D039
		[DataMember]
		public virtual int MediaJobId { get; set; }

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x0001EE42 File Offset: 0x0001D042
		// (set) Token: 0x06003EDD RID: 16093 RVA: 0x0001EE4A File Offset: 0x0001D04A
		[DataMember]
		public virtual string JobTitle { get; set; }

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x0001EE53 File Offset: 0x0001D053
		// (set) Token: 0x06003EDF RID: 16095 RVA: 0x0001EE5B File Offset: 0x0001D05B
		[DataMember]
		public virtual MediaContentDTO MediaContent { get; set; }

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x0001EE64 File Offset: 0x0001D064
		// (set) Token: 0x06003EE1 RID: 16097 RVA: 0x0001EE6C File Offset: 0x0001D06C
		[DataMember]
		public virtual MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x0001EE75 File Offset: 0x0001D075
		// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x0001EE7D File Offset: 0x0001D07D
		[DataMember]
		public virtual int MediaContentPerFormatId { get; set; }

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x0001EE86 File Offset: 0x0001D086
		// (set) Token: 0x06003EE5 RID: 16101 RVA: 0x0001EE8E File Offset: 0x0001D08E
		[DataMember]
		public virtual DateTime JobStartTime { get; set; }

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x0001EE97 File Offset: 0x0001D097
		// (set) Token: 0x06003EE7 RID: 16103 RVA: 0x0001EE9F File Offset: 0x0001D09F
		[DataMember]
		public virtual DateTime JobDueDate { get; set; }

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x0001EEA8 File Offset: 0x0001D0A8
		// (set) Token: 0x06003EE9 RID: 16105 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
		[DataMember]
		public virtual string JobCurrentStatusNameAboutPublisher { get; set; }

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06003EEA RID: 16106 RVA: 0x0001EEB9 File Offset: 0x0001D0B9
		// (set) Token: 0x06003EEB RID: 16107 RVA: 0x0001EEC1 File Offset: 0x0001D0C1
		[DataMember]
		public virtual string JobCurrentStatusNameAboutVendor { get; set; }

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x0001EECA File Offset: 0x0001D0CA
		// (set) Token: 0x06003EED RID: 16109 RVA: 0x0001EED2 File Offset: 0x0001D0D2
		[DataMember]
		public virtual string JobCurrentStatusNameAboutInHouse { get; set; }

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x0001EEDB File Offset: 0x0001D0DB
		// (set) Token: 0x06003EEF RID: 16111 RVA: 0x0001EEE3 File Offset: 0x0001D0E3
		[DataMember]
		public virtual string JobCurrentStatusNameGeneral { get; set; }

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06003EF0 RID: 16112 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		// (set) Token: 0x06003EF1 RID: 16113 RVA: 0x0001EEF4 File Offset: 0x0001D0F4
		[DataMember]
		public virtual PersonBaseDTO AssignedTo { get; set; }

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06003EF2 RID: 16114 RVA: 0x0001EEFD File Offset: 0x0001D0FD
		// (set) Token: 0x06003EF3 RID: 16115 RVA: 0x0001EF05 File Offset: 0x0001D105
		[DataMember]
		public virtual bool IsCompleted { get; set; }

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x0001EF0E File Offset: 0x0001D10E
		// (set) Token: 0x06003EF5 RID: 16117 RVA: 0x0001EF16 File Offset: 0x0001D116
		[DataMember]
		public virtual bool IsCancelled { get; set; }

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x0001EF1F File Offset: 0x0001D11F
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x0001EF27 File Offset: 0x0001D127
		[DataMember]
		public virtual PersonBaseDTO WhoCreatedJob { get; set; }

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x0001EF30 File Offset: 0x0001D130
		// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x0001EF38 File Offset: 0x0001D138
		[DataMember]
		public virtual eMediaJobPriority JobPriority { get; set; }

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x0001EF41 File Offset: 0x0001D141
		// (set) Token: 0x06003EFB RID: 16123 RVA: 0x0001EF49 File Offset: 0x0001D149
		[DataMember]
		public virtual SchoolCampusDTO Campus { get; set; }

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x0001EF52 File Offset: 0x0001D152
		// (set) Token: 0x06003EFD RID: 16125 RVA: 0x0001EF5A File Offset: 0x0001D15A
		[DataMember]
		public PersonBaseDTO WhoMadeChange { get; set; }

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x0001EF63 File Offset: 0x0001D163
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x0001EF6B File Offset: 0x0001D16B
		[DataMember]
		public string ChangeReason { get; set; }

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06003F00 RID: 16128 RVA: 0x0001EF74 File Offset: 0x0001D174
		// (set) Token: 0x06003F01 RID: 16129 RVA: 0x0001EF7C File Offset: 0x0001D17C
		[DataMember]
		public int StartPageIndex { get; set; }

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06003F02 RID: 16130 RVA: 0x0001EF85 File Offset: 0x0001D185
		// (set) Token: 0x06003F03 RID: 16131 RVA: 0x0001EF8D File Offset: 0x0001D18D
		[DataMember]
		public int EndPageIndex { get; set; }

		// Token: 0x06003F04 RID: 16132 RVA: 0x0001EF98 File Offset: 0x0001D198
		public MediaJobDTO Clone()
		{
			return new MediaJobDTO(this);
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
