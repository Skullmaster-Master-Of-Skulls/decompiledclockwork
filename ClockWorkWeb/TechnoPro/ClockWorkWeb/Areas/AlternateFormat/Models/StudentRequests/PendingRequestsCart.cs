using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests
{
	// Token: 0x02000178 RID: 376
	public class PendingRequestsCart : ShoppingCart<MediaContentIdentifierDTO, StudentRequestWebViewModel>
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000493B9 File Offset: 0x000475B9
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x000493C1 File Offset: 0x000475C1
		public PersonBaseDTO Student { get; set; }

		// Token: 0x06000B2F RID: 2863 RVA: 0x000493CA File Offset: 0x000475CA
		public void RemoveRequestById(MediaContentIdentifierDTO id)
		{
			base.Remove(id);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000493D5 File Offset: 0x000475D5
		public void AddRequest(MediaContentWebView mContent, int? selectedCourseId = null)
		{
			base.Add(new StudentRequestWebViewModel
			{
				MediaContent = mContent,
				Student = this.Student,
				SelectedCourseId = selectedCourseId
			});
		}
	}
}
