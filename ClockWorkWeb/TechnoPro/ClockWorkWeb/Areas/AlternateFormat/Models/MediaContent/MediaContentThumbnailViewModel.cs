using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.MediaContent
{
	// Token: 0x0200017F RID: 383
	public class MediaContentThumbnailViewModel
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000495FC File Offset: 0x000477FC
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00049604 File Offset: 0x00047804
		public int ThumbnailWidth { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0004960D File Offset: 0x0004780D
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00049615 File Offset: 0x00047815
		public int ThumbnailHeight { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0004961E File Offset: 0x0004781E
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00049626 File Offset: 0x00047826
		public string ThumbnailUrl { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0004962F File Offset: 0x0004782F
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00049637 File Offset: 0x00047837
		public MediaContentIdentifierDTO Identifier { get; set; }

		// Token: 0x06000B66 RID: 2918 RVA: 0x00049640 File Offset: 0x00047840
		public MediaContentThumbnailViewModel()
		{
			this.ThumbnailWidth = 100;
			this.ThumbnailHeight = 150;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0004965F File Offset: 0x0004785F
		public MediaContentThumbnailViewModel(MediaContentWebView mContent) : this()
		{
			this.ThumbnailUrl = ((mContent != null) ? mContent.ThumbnailUrl : null);
			this.Identifier = ((mContent != null) ? mContent.Identifier : null);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0004968F File Offset: 0x0004788F
		public MediaContentThumbnailViewModel(MediaContentDTO mContent) : this()
		{
			this.ThumbnailUrl = ((mContent != null) ? mContent.ThumbnailImageUrl : null);
			this.Identifier = ((mContent != null) ? mContent.Identifier : null);
		}
	}
}
