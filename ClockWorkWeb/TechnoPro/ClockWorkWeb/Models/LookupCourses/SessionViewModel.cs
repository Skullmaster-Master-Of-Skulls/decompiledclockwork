using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.ClockWorkWeb.Models.LookupCourses
{
	// Token: 0x0200010E RID: 270
	public class SessionViewModel
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0003A5ED File Offset: 0x000387ED
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0003A5F5 File Offset: 0x000387F5
		public List<SessionView> SessionList { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0003A5FE File Offset: 0x000387FE
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0003A606 File Offset: 0x00038806
		public string SelectedSessionId { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0003A610 File Offset: 0x00038810
		public IEnumerable<SelectListItem> SessionItems
		{
			get
			{
				IEnumerable<SelectListItem> second = from f in this.SessionList
				select new SelectListItem
				{
					Value = f.Id,
					Text = f.Title
				};
				return this.DefaultSessionItem.Concat(second);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0003A65C File Offset: 0x0003885C
		public IEnumerable<SelectListItem> DefaultSessionItem
		{
			get
			{
				return Enumerable.Repeat<SelectListItem>(new SelectListItem
				{
					Value = "",
					Text = "Select a session"
				}, 1);
			}
		}
	}
}
