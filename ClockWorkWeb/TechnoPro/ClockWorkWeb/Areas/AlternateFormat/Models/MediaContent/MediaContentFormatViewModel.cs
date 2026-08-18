using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.MediaContent
{
	// Token: 0x0200017E RID: 382
	public class MediaContentFormatViewModel
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x000495A0 File Offset: 0x000477A0
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x000495A8 File Offset: 0x000477A8
		public MediaContentFormat Format { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x000495B1 File Offset: 0x000477B1
		public string FormatId { get; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000495B9 File Offset: 0x000477B9
		public string FormatTitle
		{
			get
			{
				return this.Format.ToDisplayString();
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000495C6 File Offset: 0x000477C6
		public string FormatDescription
		{
			get
			{
				return this.Format.GetDefinition();
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0000AF9E File Offset: 0x0000919E
		public MediaContentFormatViewModel()
		{
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000495D3 File Offset: 0x000477D3
		public MediaContentFormatViewModel(MediaContentFormat format, MediaContentIdentifierDTO mediaContentId)
		{
			this.Format = format;
			this.FormatId = string.Format("{0}-{1}", mediaContentId, format);
		}
	}
}
