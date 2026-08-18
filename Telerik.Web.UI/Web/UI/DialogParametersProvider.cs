using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001039 RID: 4153
	public abstract class DialogParametersProvider
	{
		// Token: 0x1700339E RID: 13214
		// (get) Token: 0x0600A387 RID: 41863 RVA: 0x002462F9 File Offset: 0x002444F9
		protected Page Page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x0600A388 RID: 41864 RVA: 0x00246301 File Offset: 0x00244501
		public DialogParametersProvider(Page page)
		{
			this._page = page;
		}

		// Token: 0x0600A389 RID: 41865
		public abstract DialogParameters GetDialogParameters(string dialogOpenerIdentifier, string dialogName);

		// Token: 0x0600A38A RID: 41866
		public abstract void StoreAllParameters(string dialogOpenerIdentifier, DialogParametersDictionary dialogParameters);

		// Token: 0x04002D7D RID: 11645
		private readonly Page _page;
	}
}
