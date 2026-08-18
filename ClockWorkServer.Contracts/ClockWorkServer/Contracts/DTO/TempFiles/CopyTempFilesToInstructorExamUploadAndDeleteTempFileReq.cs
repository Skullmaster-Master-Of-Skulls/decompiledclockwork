using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles
{
	// Token: 0x020001E0 RID: 480
	[DataContract(Namespace = "http://tpro.ca")]
	public class CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq : BaseMessageReq
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00004F89 File Offset: 0x00003189
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x00004F91 File Offset: 0x00003191
		[DataMember]
		public TempFileContextDTO Context { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00004F9A File Offset: 0x0000319A
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x00004FA2 File Offset: 0x000031A2
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00004FAB File Offset: 0x000031AB
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x00004FB3 File Offset: 0x000031B3
		[DataMember]
		public int WhoEnteredPersonId { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00004FBC File Offset: 0x000031BC
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x00004FC4 File Offset: 0x000031C4
		[DataMember]
		public string Description { get; set; }
	}
}
