using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020001EF RID: 495
	public class ViewEngineResult
	{
		// Token: 0x06000F1A RID: 3866 RVA: 0x00027CE2 File Offset: 0x00025EE2
		public ViewEngineResult(IEnumerable<string> searchedLocations)
		{
			if (searchedLocations == null)
			{
				throw new ArgumentNullException("searchedLocations");
			}
			this.SearchedLocations = searchedLocations;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00027CFF File Offset: 0x00025EFF
		public ViewEngineResult(IView view, IViewEngine viewEngine)
		{
			if (view == null)
			{
				throw new ArgumentNullException("view");
			}
			if (viewEngine == null)
			{
				throw new ArgumentNullException("viewEngine");
			}
			this.View = view;
			this.ViewEngine = viewEngine;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00027D31 File Offset: 0x00025F31
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x00027D39 File Offset: 0x00025F39
		public IEnumerable<string> SearchedLocations { get; private set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00027D42 File Offset: 0x00025F42
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00027D4A File Offset: 0x00025F4A
		public IView View { get; private set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00027D53 File Offset: 0x00025F53
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x00027D5B File Offset: 0x00025F5B
		public IViewEngine ViewEngine { get; private set; }
	}
}
