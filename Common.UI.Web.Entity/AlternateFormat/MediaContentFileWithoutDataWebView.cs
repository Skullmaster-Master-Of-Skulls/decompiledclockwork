using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;

namespace TechnoPro.Common.UI.Web.Entity.AlternateFormat
{
	// Token: 0x0200004E RID: 78
	public class MediaContentFileWithoutDataWebView : WrapperBase<StudentMediaContentFileWithProofOfPurchaseInfoDTO>
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00004616 File Offset: 0x00002816
		public MediaContentFileWithoutDataWebView(StudentMediaContentFileWithProofOfPurchaseInfoDTO item) : base(item)
		{
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00004621 File Offset: 0x00002821
		public int MediaContentFileId
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (item != null) ? item.MediaContentFileId : 0;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00004638 File Offset: 0x00002838
		public Guid? MediaContentFileUniqueId
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (item != null) ? item.MediaContentFileUniqueId : null;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00004660 File Offset: 0x00002860
		public string MediaContentUniqueId
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				string text;
				if (item == null)
				{
					text = null;
				}
				else
				{
					MediaContentDTO mediaContent = item.MediaContent;
					text = ((mediaContent != null) ? mediaContent.MediaContentUniqueId.ToString() : null);
				}
				return text ?? string.Empty;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000046A2 File Offset: 0x000028A2
		public string ShortTitle
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (((item != null) ? item.MediaContent : null) != null) ? (base.Item.MediaContent.ShortTitle ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000046D8 File Offset: 0x000028D8
		public string Authors
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				bool flag;
				if (item == null)
				{
					flag = (null != null);
				}
				else
				{
					MediaContentDTO mediaContent = item.MediaContent;
					flag = (((mediaContent != null) ? mediaContent.Authors : null) != null);
				}
				return (flag && base.Item.MediaContent.Authors.Count > 0) ? string.Join(", ", base.Item.MediaContent.Authors.ToArray<string>()) : string.Empty;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00004743 File Offset: 0x00002943
		public string Edition
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (((item != null) ? item.MediaContent : null) != null) ? (base.Item.MediaContent.Edition ?? string.Empty) : string.Empty;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00004779 File Offset: 0x00002979
		public string ISBN
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				string value;
				if (item == null)
				{
					value = null;
				}
				else
				{
					MediaContentDTO mediaContent = item.MediaContent;
					value = ((mediaContent != null) ? mediaContent.ISBN : null);
				}
				return (!string.IsNullOrEmpty(value)) ? base.Item.MediaContent.ISBN : string.Empty;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000047B7 File Offset: 0x000029B7
		public string Courses
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				bool flag;
				if (item == null)
				{
					flag = (null != null);
				}
				else
				{
					MediaContentDTO mediaContent = item.MediaContent;
					flag = (((mediaContent != null) ? mediaContent.CourseDescriptions : null) != null);
				}
				return flag ? base.Item.MediaContent.CourseDescriptions : string.Empty;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000047F0 File Offset: 0x000029F0
		public string ContentFormat
		{
			get
			{
				return (base.Item != null) ? base.Item.ContentFormat.ToDisplayString() : string.Empty;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00004811 File Offset: 0x00002A11
		public long Size
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (item != null) ? item.Size : 0L;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00004826 File Offset: 0x00002A26
		public int MediaContentPerFormatId
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (item != null) ? item.MediaContentPerFormatId : 0;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000483A File Offset: 0x00002A3A
		public string Filename
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return ((item != null) ? item.Filename : null) ?? string.Empty;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00004858 File Offset: 0x00002A58
		public DateTime? DateCreated
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (item != null) ? new DateTime?(item.DateCreated) : null;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00004884 File Offset: 0x00002A84
		public int StudentPersonId
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (item != null) ? item.StudentPersonId : 0;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00004898 File Offset: 0x00002A98
		public eStudentMediaContentFileStatus? FileStatus
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (item != null) ? new eStudentMediaContentFileStatus?(item.FileStatus) : null;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000048C4 File Offset: 0x00002AC4
		public int ProofOfPuchaseId
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return (item != null) ? item.ProofOfPurchaseId : 0;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000048D8 File Offset: 0x00002AD8
		public string StudentCompletionNotes
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return ((item != null) ? item.StudentCompletionRequestNotes : null) ?? string.Empty;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000048F5 File Offset: 0x00002AF5
		public bool HardCopy
		{
			get
			{
				StudentMediaContentFileWithProofOfPurchaseInfoDTO item = base.Item;
				return item != null && item.HardCopy;
			}
		}
	}
}
