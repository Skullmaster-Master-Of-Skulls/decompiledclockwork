using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200046D RID: 1133
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeDefaultPrinterSettingsDTO
	{
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0000B44D File Offset: 0x0000964D
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x0000B455 File Offset: 0x00009655
		[DataMember]
		public string PrinterName { get; set; }

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0000B45E File Offset: 0x0000965E
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x0000B466 File Offset: 0x00009666
		[DataMember]
		public string DefaultPageSize { get; set; }

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0000B46F File Offset: 0x0000966F
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x0000B477 File Offset: 0x00009677
		[DataMember]
		public ePageOrientationDTO Orientation { get; set; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0000B480 File Offset: 0x00009680
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x0000B488 File Offset: 0x00009688
		[DataMember]
		public int CopyCount { get; set; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0000B491 File Offset: 0x00009691
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x0000B499 File Offset: 0x00009699
		[DataMember]
		public int MarginLeft { get; set; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0000B4A2 File Offset: 0x000096A2
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x0000B4AA File Offset: 0x000096AA
		[DataMember]
		public int MarginRight { get; set; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0000B4B3 File Offset: 0x000096B3
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x0000B4BB File Offset: 0x000096BB
		[DataMember]
		public int MarginTop { get; set; }

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0000B4C4 File Offset: 0x000096C4
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x0000B4CC File Offset: 0x000096CC
		[DataMember]
		public int MarginBottom { get; set; }
	}
}
