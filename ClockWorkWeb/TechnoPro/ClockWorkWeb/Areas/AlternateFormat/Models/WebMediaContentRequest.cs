using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000177 RID: 375
	public class WebMediaContentRequest : AlternateFormatBaseViewModel
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0004930F File Offset: 0x0004750F
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x00049317 File Offset: 0x00047517
		[HiddenInput(DisplayValue = false)]
		public Guid MediaContentUniqueId { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00049320 File Offset: 0x00047520
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x00049328 File Offset: 0x00047528
		[Required(ErrorMessage = "Please enter a media content title")]
		public string Title { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00049331 File Offset: 0x00047531
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x00049339 File Offset: 0x00047539
		public string ISBN { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00049342 File Offset: 0x00047542
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0004934A File Offset: 0x0004754A
		public string Authors { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00049353 File Offset: 0x00047553
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0004935B File Offset: 0x0004755B
		public string Edition { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00049364 File Offset: 0x00047564
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0004936C File Offset: 0x0004756C
		[HiddenInput(DisplayValue = false)]
		public byte[] Thumbnail { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00049375 File Offset: 0x00047575
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0004937D File Offset: 0x0004757D
		[HiddenInput(DisplayValue = false)]
		public string ThumbnailImageMimeType { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00049386 File Offset: 0x00047586
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0004938E File Offset: 0x0004758E
		public int PublisherId { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00049397 File Offset: 0x00047597
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0004939F File Offset: 0x0004759F
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
		public DateTime PublishedDate { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x000493A8 File Offset: 0x000475A8
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x000493B0 File Offset: 0x000475B0
		public IEnumerable<MediaPublisherDTO> Publishers { get; set; }
	}
}
