using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.Adapters;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;

namespace TechnoPro.Common.UI.Web.Entity.AlternateFormat
{
	// Token: 0x02000050 RID: 80
	public class StudentRequestWebView : WrapperBase<MediaContentRequestedInfoExtendedDTO>
	{
		// Token: 0x06000243 RID: 579 RVA: 0x00004D46 File Offset: 0x00002F46
		public StudentRequestWebView(MediaContentRequestedInfoExtendedDTO item) : base(item)
		{
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00004D54 File Offset: 0x00002F54
		public int MediaContentRequestedInfoID
		{
			get
			{
				return (base.Item != null) ? base.Item.MediaContentRequestedInfoID : 0;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00004D7C File Offset: 0x00002F7C
		public string MediaContentId
		{
			get
			{
				return (base.Item != null && base.Item.ContentDetailRequested != null && base.Item.ContentDetailRequested.MediaContent != null) ? base.Item.ContentDetailRequested.MediaContent.MediaContentUniqueId.ToString() : string.Empty;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public string MediaContentTitle
		{
			get
			{
				return (base.Item != null && base.Item.ContentDetailRequested != null && base.Item.ContentDetailRequested.MediaContent != null) ? (base.Item.ContentDetailRequested.MediaContent.ShortTitle ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00004E40 File Offset: 0x00003040
		public string ISBN
		{
			get
			{
				return (base.Item != null && base.Item.ContentDetailRequested != null && base.Item.ContentDetailRequested.MediaContent != null) ? (base.Item.ContentDetailRequested.MediaContent.ISBN ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00004EA0 File Offset: 0x000030A0
		public string Edition
		{
			get
			{
				return (base.Item != null && base.Item.ContentDetailRequested != null && base.Item.ContentDetailRequested.MediaContent != null) ? (base.Item.ContentDetailRequested.MediaContent.Edition ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00004F00 File Offset: 0x00003100
		public string Authors
		{
			get
			{
				return (base.Item != null && base.Item.ContentDetailRequested != null && base.Item.ContentDetailRequested.MediaContent != null && base.Item.ContentDetailRequested.MediaContent.Authors != null) ? base.Item.ContentDetailRequested.MediaContent.Authors.CommaSeparatedValues<string>() : string.Empty;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00004F74 File Offset: 0x00003174
		public MediaContentFormat? MediaContentFormat
		{
			get
			{
				return (base.Item != null && base.Item.ContentDetailRequested != null) ? new MediaContentFormat?(base.Item.ContentDetailRequested.MediaContentFormat) : null;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00004FBC File Offset: 0x000031BC
		public int MediaContentPerFormatId
		{
			get
			{
				return (base.Item != null && base.Item.ContentDetailRequested != null) ? base.Item.ContentDetailRequested.MediaContentPerFormatId : 0;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00004FF8 File Offset: 0x000031F8
		public MediaRequestStatus? RequestStatus
		{
			get
			{
				return (base.Item != null) ? new MediaRequestStatus?(base.Item.RequestStatus) : null;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00005030 File Offset: 0x00003230
		public DateTime? CreatedDateTime
		{
			get
			{
				return (base.Item != null) ? new DateTime?(base.Item.CreatedDatetime) : null;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00005068 File Offset: 0x00003268
		public DateTime? CompletedDateTime
		{
			get
			{
				return (base.Item != null) ? base.Item.CompletedDateTime : null;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00005098 File Offset: 0x00003298
		public string CompletionNotes
		{
			get
			{
				MediaContentRequestedInfoExtendedDTO item = base.Item;
				return ((item != null) ? item.CompletionNotes : null) ?? string.Empty;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000050B5 File Offset: 0x000032B5
		public int FileSize
		{
			get
			{
				MediaContentRequestedInfoExtendedDTO item = base.Item;
				return (item != null) ? item.FileSize : 0;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000050CC File Offset: 0x000032CC
		public bool IsApproved
		{
			get
			{
				return base.Item != null && base.Item.IsApproved;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000050F4 File Offset: 0x000032F4
		public bool IsCompleted
		{
			get
			{
				return base.Item != null && base.Item.IsCompleted;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000511C File Offset: 0x0000331C
		public bool IsCancelled
		{
			get
			{
				return base.Item != null && base.Item.IsCancelled;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00005144 File Offset: 0x00003344
		public bool ProofOfPurchaseRequired
		{
			get
			{
				return base.Item != null && base.Item.ContentDetailRequested != null && base.Item.ContentDetailRequested.MediaContent != null && base.Item.ContentDetailRequested != null && base.Item.ContentDetailRequested.MediaContent.ProofOfPurchaseRequired;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000051A4 File Offset: 0x000033A4
		public bool ProofOfPurchaseAvailable
		{
			get
			{
				return base.Item.ProofOfPurchaseId > 0;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000051C4 File Offset: 0x000033C4
		public bool IsCancellable
		{
			get
			{
				return !this.IsCancelled && (this.RequestStatus == null || this.RequestStatus.Value.IsCancellable());
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00005208 File Offset: 0x00003408
		public bool NeedForProofOfPurchaseUpload
		{
			get
			{
				return this.ProofOfPurchaseRequired && !this.ProofOfPurchaseAvailable;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00005230 File Offset: 0x00003430
		public bool ReadyToDownload
		{
			get
			{
				return base.Item != null && base.Item.IsReadyToDownload();
			}
		}
	}
}
