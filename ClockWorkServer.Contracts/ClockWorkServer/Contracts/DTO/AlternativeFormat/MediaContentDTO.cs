using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B49 RID: 2889
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaContentDTO : BasicMediaContentDTO, ICloneable<MediaContentDTO>, ICloneable
	{
		// Token: 0x06003D3B RID: 15675 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		public MediaContentDTO()
		{
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x0001DDD0 File Offset: 0x0001BFD0
		public MediaContentDTO(MediaContentDTO item) : base(item)
		{
			this.LongTitle = item.LongTitle;
			this.Length = item.Length;
			this.CourseIdList = item.CourseIdList;
			this.Notes = item.Notes;
			this.ContentCategory = item.ContentCategory;
			this.WhoEntered = item.WhoEntered;
			this.DateCreated = item.DateCreated;
			this.IsActive = item.IsActive;
			this.IsThumbnailAvailable = item.IsThumbnailAvailable;
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x0001DE5B File Offset: 0x0001C05B
		public MediaContentDTO(BasicMediaContentDTO item) : base(item)
		{
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x0001DE66 File Offset: 0x0001C066
		// (set) Token: 0x06003D3F RID: 15679 RVA: 0x0001DE6E File Offset: 0x0001C06E
		[DataMember]
		public string LongTitle { get; set; }

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x0001DE77 File Offset: 0x0001C077
		// (set) Token: 0x06003D41 RID: 15681 RVA: 0x0001DE7F File Offset: 0x0001C07F
		[DataMember]
		public string Length { get; set; }

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x0001DE88 File Offset: 0x0001C088
		// (set) Token: 0x06003D43 RID: 15683 RVA: 0x0001DE90 File Offset: 0x0001C090
		[DataMember]
		public IList<int> CourseIdList { get; set; }

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x0001DE99 File Offset: 0x0001C099
		// (set) Token: 0x06003D45 RID: 15685 RVA: 0x0001DEA1 File Offset: 0x0001C0A1
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x0001DEAA File Offset: 0x0001C0AA
		// (set) Token: 0x06003D47 RID: 15687 RVA: 0x0001DEB2 File Offset: 0x0001C0B2
		[DataMember]
		public eMediaContentCategory ContentCategory { get; set; }

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06003D48 RID: 15688 RVA: 0x0001DEBB File Offset: 0x0001C0BB
		// (set) Token: 0x06003D49 RID: 15689 RVA: 0x0001DEC3 File Offset: 0x0001C0C3
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x0001DECC File Offset: 0x0001C0CC
		// (set) Token: 0x06003D4B RID: 15691 RVA: 0x0001DED4 File Offset: 0x0001C0D4
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x0001DEDD File Offset: 0x0001C0DD
		// (set) Token: 0x06003D4D RID: 15693 RVA: 0x0001DEE5 File Offset: 0x0001C0E5
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x0001DEEE File Offset: 0x0001C0EE
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x0001DEF6 File Offset: 0x0001C0F6
		[DataMember]
		public bool IsThumbnailAvailable { get; set; }

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x0001DEFF File Offset: 0x0001C0FF
		// (set) Token: 0x06003D51 RID: 15697 RVA: 0x0001DF07 File Offset: 0x0001C107
		[DataMember]
		public string AvailableFormats { get; set; }

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x0001DF10 File Offset: 0x0001C110
		// (set) Token: 0x06003D53 RID: 15699 RVA: 0x0001DF18 File Offset: 0x0001C118
		[DataMember]
		public string CourseDescriptions { get; set; }

		// Token: 0x06003D54 RID: 15700 RVA: 0x0001DF24 File Offset: 0x0001C124
		public new MediaContentDTO Clone()
		{
			return new MediaContentDTO(this);
		}
	}
}
