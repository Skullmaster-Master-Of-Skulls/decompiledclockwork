using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000575 RID: 1397
	public class MediaContentFileWithoutData : BusinessBase<int>
	{
		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x00031F44 File Offset: 0x00030144
		// (set) Token: 0x06002CF6 RID: 11510 RVA: 0x0000E258 File Offset: 0x0000C458
		public int MediaContentFileId
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

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x00031F5C File Offset: 0x0003015C
		// (set) Token: 0x06002CF8 RID: 11512 RVA: 0x00031F64 File Offset: 0x00030164
		public Guid? MediaContentFileUniqueId { get; set; }

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x00031F6D File Offset: 0x0003016D
		// (set) Token: 0x06002CFA RID: 11514 RVA: 0x00031F75 File Offset: 0x00030175
		public MediaContent MediaContent { get; set; }

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x00031F7E File Offset: 0x0003017E
		// (set) Token: 0x06002CFC RID: 11516 RVA: 0x00031F86 File Offset: 0x00030186
		public MediaContentFormat ContentFormat { get; set; }

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x00031F8F File Offset: 0x0003018F
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x00031F97 File Offset: 0x00030197
		public long Size { get; set; }

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x00031FA0 File Offset: 0x000301A0
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x00031FA8 File Offset: 0x000301A8
		public eMediaContentLanguage ContentLanguage { get; set; }

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x00031FB1 File Offset: 0x000301B1
		// (set) Token: 0x06002D02 RID: 11522 RVA: 0x00031FB9 File Offset: 0x000301B9
		public string SourceProvider { get; set; }

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x00031FC2 File Offset: 0x000301C2
		// (set) Token: 0x06002D04 RID: 11524 RVA: 0x00031FCA File Offset: 0x000301CA
		public string Notes { get; set; }

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x00031FD3 File Offset: 0x000301D3
		// (set) Token: 0x06002D06 RID: 11526 RVA: 0x00031FDB File Offset: 0x000301DB
		public PersonBase UniqueStudentOwner { get; set; }

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x00031FE4 File Offset: 0x000301E4
		// (set) Token: 0x06002D08 RID: 11528 RVA: 0x00031FEC File Offset: 0x000301EC
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x00031FF5 File Offset: 0x000301F5
		// (set) Token: 0x06002D0A RID: 11530 RVA: 0x00031FFD File Offset: 0x000301FD
		public string Filename { get; set; }

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x00032006 File Offset: 0x00030206
		// (set) Token: 0x06002D0C RID: 11532 RVA: 0x0003200E File Offset: 0x0003020E
		public DateTime DateCreated { get; set; }

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x00032017 File Offset: 0x00030217
		// (set) Token: 0x06002D0E RID: 11534 RVA: 0x0003201F File Offset: 0x0003021F
		public PersonBase WhoUploadFile { get; set; }

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x00032028 File Offset: 0x00030228
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x00032030 File Offset: 0x00030230
		public bool HardCopy { get; set; }
	}
}
