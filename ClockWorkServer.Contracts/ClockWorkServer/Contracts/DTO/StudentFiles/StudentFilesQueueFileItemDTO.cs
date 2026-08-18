using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000230 RID: 560
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFilesQueueFileItemDTO
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00005C38 File Offset: 0x00003E38
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x00005C40 File Offset: 0x00003E40
		[DataMember]
		public int FileId { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00005C49 File Offset: 0x00003E49
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x00005C51 File Offset: 0x00003E51
		[DataMember]
		public StudentFilesStatusDTO Status { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00005C5A File Offset: 0x00003E5A
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x00005C62 File Offset: 0x00003E62
		[DataMember]
		public string StudentComment { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00005C6B File Offset: 0x00003E6B
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x00005C73 File Offset: 0x00003E73
		[DataMember]
		public string StaffComment { get; set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00005C7C File Offset: 0x00003E7C
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x00005C84 File Offset: 0x00003E84
		[DataMember]
		public string DateAddedStr { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00005C90 File Offset: 0x00003E90
		public DateTime? DateAdded
		{
			get
			{
				DateTime value;
				return (string.IsNullOrWhiteSpace(this.DateAddedStr) || !DateTime.TryParse(this.DateAddedStr, out value)) ? null : new DateTime?(value);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00005CCA File Offset: 0x00003ECA
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x00005CD2 File Offset: 0x00003ED2
		[DataMember]
		public string FileName { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00005CDB File Offset: 0x00003EDB
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00005CE3 File Offset: 0x00003EE3
		[DataMember]
		public string[] OriginalColumn { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00005CEC File Offset: 0x00003EEC
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x00005CF4 File Offset: 0x00003EF4
		[DataMember]
		public bool WasModified { get; set; }
	}
}
