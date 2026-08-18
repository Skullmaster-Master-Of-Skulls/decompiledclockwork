using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200058C RID: 1420
	public class MediaContent : BasicMediaContent
	{
		// Token: 0x06002DF5 RID: 11765 RVA: 0x0003294B File Offset: 0x00030B4B
		public MediaContent()
		{
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0003295C File Offset: 0x00030B5C
		public MediaContent(BasicMediaContent content) : base(content)
		{
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x0003296E File Offset: 0x00030B6E
		// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x00032976 File Offset: 0x00030B76
		public string LongTitle { get; set; }

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x0003297F File Offset: 0x00030B7F
		// (set) Token: 0x06002DFA RID: 11770 RVA: 0x00032987 File Offset: 0x00030B87
		public string Length { get; set; }

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x00032990 File Offset: 0x00030B90
		// (set) Token: 0x06002DFC RID: 11772 RVA: 0x00032998 File Offset: 0x00030B98
		public IList<int> CourseIdList { get; set; }

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000329A1 File Offset: 0x00030BA1
		// (set) Token: 0x06002DFE RID: 11774 RVA: 0x000329A9 File Offset: 0x00030BA9
		public string Notes { get; set; }

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000329B2 File Offset: 0x00030BB2
		// (set) Token: 0x06002E00 RID: 11776 RVA: 0x000329BA File Offset: 0x00030BBA
		public eMediaContentCategory ContentCategory { get; set; }

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000329C3 File Offset: 0x00030BC3
		// (set) Token: 0x06002E02 RID: 11778 RVA: 0x000329CB File Offset: 0x00030BCB
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x000329D4 File Offset: 0x00030BD4
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x000329DC File Offset: 0x00030BDC
		public DateTime DateCreated { get; set; }

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000329E5 File Offset: 0x00030BE5
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x000329ED File Offset: 0x00030BED
		public bool IsActive { get; set; } = true;

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000329F8 File Offset: 0x00030BF8
		// (set) Token: 0x06002E08 RID: 11784 RVA: 0x00032A23 File Offset: 0x00030C23
		public bool IsThumbnailAvailable
		{
			get
			{
				return this._isThumbnailAvailable || !string.IsNullOrEmpty(base.ThumbnailImageUrl);
			}
			set
			{
				this._isThumbnailAvailable = value;
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x00032A2D File Offset: 0x00030C2D
		// (set) Token: 0x06002E0A RID: 11786 RVA: 0x00032A35 File Offset: 0x00030C35
		public string AvailableFormats { get; set; }

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x00032A3E File Offset: 0x00030C3E
		// (set) Token: 0x06002E0C RID: 11788 RVA: 0x00032A46 File Offset: 0x00030C46
		public string CourseDescriptions { get; set; }

		// Token: 0x04002034 RID: 8244
		private bool _isThumbnailAvailable;
	}
}
