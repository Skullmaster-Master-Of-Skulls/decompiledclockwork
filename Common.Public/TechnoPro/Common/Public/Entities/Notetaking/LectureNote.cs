using System;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Public.Entities.Notetaking
{
	// Token: 0x02000281 RID: 641
	public class LectureNote : BusinessBase<int>
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x00019640 File Offset: 0x00017840
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int NotetakerDocumentId
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

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x00019658 File Offset: 0x00017858
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x00019688 File Offset: 0x00017888
		public override int Id
		{
			get
			{
				return (this.LectureNoteDescription == null) ? base.Id : this.LectureNoteDescription.Id;
			}
			set
			{
				bool flag = this.LectureNoteDescription == null;
				if (flag)
				{
					base.Id = value;
				}
				else
				{
					this.LectureNoteDescription.Id = value;
				}
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x000196BA File Offset: 0x000178BA
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x000196C2 File Offset: 0x000178C2
		public LectureNoteDescription LectureNoteDescription { get; set; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x000196CB File Offset: 0x000178CB
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x000196D3 File Offset: 0x000178D3
		public BinaryFile LectureNoteDocument { get; set; }
	}
}
