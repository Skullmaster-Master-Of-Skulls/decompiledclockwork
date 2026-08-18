using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B9E RID: 2974
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaContentRequestedInfoDTO : ICloneable<MediaContentRequestedInfoDTO>, ICloneable
	{
		// Token: 0x06003E98 RID: 16024 RVA: 0x000036BD File Offset: 0x000018BD
		public MediaContentRequestedInfoDTO()
		{
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x0001EA40 File Offset: 0x0001CC40
		public MediaContentRequestedInfoDTO(MediaContentRequestedInfoDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.ProofOfPurchase = item.ProofOfPurchase;
				this.ProofOfPurchaseId = item.ProofOfPurchaseId;
				this.RequestStatus = item.RequestStatus;
				this.IsApproved = item.IsApproved;
				this.IsCancelled = item.IsCancelled;
				this.AvailableStartTime = item.AvailableStartTime;
				this.AvailableEndTime = item.AvailableEndTime;
				this.ContentDetailRequested = item.ContentDetailRequested;
				this.MediaJobId = item.MediaJobId;
				this.StudentRequestId = item.StudentRequestId;
				this.Campus = item.Campus;
				this.RequestMadeFromStudent = item.RequestMadeFromStudent;
				this.CreatedDatetime = item.CreatedDatetime;
				this.CompletedDateTime = item.CompletedDateTime;
				this.CompletionNotes = item.CompletionNotes;
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06003E9A RID: 16026 RVA: 0x0001EB25 File Offset: 0x0001CD25
		// (set) Token: 0x06003E9B RID: 16027 RVA: 0x0001EB2D File Offset: 0x0001CD2D
		[DataMember]
		public int MediaContentRequestedInfoID { get; set; }

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06003E9C RID: 16028 RVA: 0x0001EB36 File Offset: 0x0001CD36
		// (set) Token: 0x06003E9D RID: 16029 RVA: 0x0001EB3E File Offset: 0x0001CD3E
		[DataMember]
		public ProofOfPurchaseInfoDTO ProofOfPurchase { get; set; }

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x0001EB47 File Offset: 0x0001CD47
		// (set) Token: 0x06003E9F RID: 16031 RVA: 0x0001EB4F File Offset: 0x0001CD4F
		[DataMember]
		public int ProofOfPurchaseId { get; set; }

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x0001EB58 File Offset: 0x0001CD58
		// (set) Token: 0x06003EA1 RID: 16033 RVA: 0x0001EB60 File Offset: 0x0001CD60
		[DataMember]
		public MediaRequestStatus RequestStatus { get; set; }

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06003EA2 RID: 16034 RVA: 0x0001EB69 File Offset: 0x0001CD69
		// (set) Token: 0x06003EA3 RID: 16035 RVA: 0x0001EB71 File Offset: 0x0001CD71
		[DataMember]
		public bool IsApproved { get; set; }

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x0001EB7A File Offset: 0x0001CD7A
		// (set) Token: 0x06003EA5 RID: 16037 RVA: 0x0001EB82 File Offset: 0x0001CD82
		[DataMember]
		public bool IsCompleted { get; set; }

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x0001EB8B File Offset: 0x0001CD8B
		// (set) Token: 0x06003EA7 RID: 16039 RVA: 0x0001EB93 File Offset: 0x0001CD93
		[DataMember]
		public bool IsCancelled { get; set; }

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
		[DataMember]
		public DateTime? AvailableStartTime { get; set; }

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06003EAA RID: 16042 RVA: 0x0001EBAD File Offset: 0x0001CDAD
		// (set) Token: 0x06003EAB RID: 16043 RVA: 0x0001EBB5 File Offset: 0x0001CDB5
		[DataMember]
		public DateTime? AvailableEndTime { get; set; }

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06003EAC RID: 16044 RVA: 0x0001EBBE File Offset: 0x0001CDBE
		// (set) Token: 0x06003EAD RID: 16045 RVA: 0x0001EBC6 File Offset: 0x0001CDC6
		[DataMember]
		public MediaContentDetailDTO ContentDetailRequested { get; set; }

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x0001EBCF File Offset: 0x0001CDCF
		// (set) Token: 0x06003EAF RID: 16047 RVA: 0x0001EBD7 File Offset: 0x0001CDD7
		[DataMember]
		public int MediaJobId { get; set; }

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x0001EBE0 File Offset: 0x0001CDE0
		// (set) Token: 0x06003EB1 RID: 16049 RVA: 0x0001EBE8 File Offset: 0x0001CDE8
		[DataMember]
		public string MediaJobTitle { get; set; }

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x0001EBF1 File Offset: 0x0001CDF1
		// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x0001EBF9 File Offset: 0x0001CDF9
		[DataMember]
		public int StudentRequestId { get; set; }

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x0001EC02 File Offset: 0x0001CE02
		// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		[DataMember]
		public SchoolCampusDTO Campus { get; set; }

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x0001EC13 File Offset: 0x0001CE13
		// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x0001EC1B File Offset: 0x0001CE1B
		[DataMember]
		public PersonBaseDTO RequestMadeFromStudent { get; set; }

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x0001EC24 File Offset: 0x0001CE24
		// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x0001EC2C File Offset: 0x0001CE2C
		[DataMember]
		public DateTime CreatedDatetime { get; set; }

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x0001EC35 File Offset: 0x0001CE35
		// (set) Token: 0x06003EBB RID: 16059 RVA: 0x0001EC3D File Offset: 0x0001CE3D
		[DataMember]
		public DateTime? CompletedDateTime { get; set; }

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x0001EC46 File Offset: 0x0001CE46
		// (set) Token: 0x06003EBD RID: 16061 RVA: 0x0001EC4E File Offset: 0x0001CE4E
		[DataMember]
		public string CompletionNotes { get; set; }

		// Token: 0x06003EBE RID: 16062 RVA: 0x0001EC58 File Offset: 0x0001CE58
		public MediaContentRequestedInfoDTO Clone()
		{
			return new MediaContentRequestedInfoDTO(this);
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x0001EC70 File Offset: 0x0001CE70
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
