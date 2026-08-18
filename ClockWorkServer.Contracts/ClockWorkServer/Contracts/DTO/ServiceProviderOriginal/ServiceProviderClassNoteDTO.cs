using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002DC RID: 732
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceProviderClassNoteDTO
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00007AB0 File Offset: 0x00005CB0
		// (set) Token: 0x06001085 RID: 4229 RVA: 0x00007AB8 File Offset: 0x00005CB8
		[DataMember]
		public int NotetakerDocumentId { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00007AC1 File Offset: 0x00005CC1
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x00007AC9 File Offset: 0x00005CC9
		[DataMember]
		public BinaryFileDTO File { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x00007AD2 File Offset: 0x00005CD2
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x00007ADA File Offset: 0x00005CDA
		[DataMember]
		public int NumPages { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00007AE3 File Offset: 0x00005CE3
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x00007AEB File Offset: 0x00005CEB
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00007AF4 File Offset: 0x00005CF4
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x00007AFC File Offset: 0x00005CFC
		[DataMember]
		public ServiceProviderBaseDTO Provider { get; set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00007B05 File Offset: 0x00005D05
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x00007B0D File Offset: 0x00005D0D
		[DataMember]
		public string NotesFromProvider { get; set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00007B16 File Offset: 0x00005D16
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x00007B1E File Offset: 0x00005D1E
		[DataMember]
		public DateTime LectureDate { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00007B27 File Offset: 0x00005D27
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x00007B2F File Offset: 0x00005D2F
		[DataMember]
		public bool IsSampleNotes { get; set; }
	}
}
