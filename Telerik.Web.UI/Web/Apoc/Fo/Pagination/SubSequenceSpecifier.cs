using System;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x0200142C RID: 5164
	internal interface SubSequenceSpecifier
	{
		// Token: 0x0600D31F RID: 54047
		string GetNextPageMaster(int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage);

		// Token: 0x0600D320 RID: 54048
		void Reset();
	}
}
