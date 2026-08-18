using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.MediaContent;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests
{
	// Token: 0x02000179 RID: 377
	public class StudentRequestWebViewModel : BusinessBase<MediaContentIdentifierDTO>, IEquatable<StudentRequestWebViewModel>
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0004940C File Offset: 0x0004760C
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00049430 File Offset: 0x00047630
		public override MediaContentIdentifierDTO Id
		{
			get
			{
				MediaContentWebView mediaContent = this.MediaContent;
				return (mediaContent != null) ? mediaContent.Identifier : null;
			}
			set
			{
				bool flag = this.MediaContent != null;
				if (flag)
				{
					this.MediaContent.Identifier = value;
				}
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00049458 File Offset: 0x00047658
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x00049460 File Offset: 0x00047660
		public MediaContentWebView MediaContent { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00049469 File Offset: 0x00047669
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x00049471 File Offset: 0x00047671
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0004947A File Offset: 0x0004767A
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x00049482 File Offset: 0x00047682
		public byte[] ProofOfPurchaseReceipt { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0004948B File Offset: 0x0004768B
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x00049493 File Offset: 0x00047693
		public string Filename { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0004949C File Offset: 0x0004769C
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x000494A4 File Offset: 0x000476A4
		public string Extension { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000494AD File Offset: 0x000476AD
		public bool NeedForProofOfPurchaseUpload
		{
			get
			{
				return this.MediaContent.ProofOfPurchaseRequired && this.ProofOfPurchaseReceipt == null;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x000494C8 File Offset: 0x000476C8
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x000494D0 File Offset: 0x000476D0
		public int? SelectedCourseId { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x000494D9 File Offset: 0x000476D9
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x000494E1 File Offset: 0x000476E1
		public MediaContentFormat? StudentSelectedFormat { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000494EA File Offset: 0x000476EA
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x000494F2 File Offset: 0x000476F2
		public IList<MediaContentFormatViewModel> StudentMediaContentSelection { get; set; }

		// Token: 0x06000B45 RID: 2885 RVA: 0x000494FC File Offset: 0x000476FC
		public bool Equals(StudentRequestWebViewModel other)
		{
			return this.Id.Equals(other.Id);
		}
	}
}
