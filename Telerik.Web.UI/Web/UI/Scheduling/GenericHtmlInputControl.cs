using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x020007EB RID: 2027
	[SupportsEventValidation]
	[ValidationProperty("Value")]
	internal class GenericHtmlInputControl : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x06004662 RID: 18018 RVA: 0x000DD8DA File Offset: 0x000DBADA
		public GenericHtmlInputControl(string type) : base(type)
		{
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x000DD8E3 File Offset: 0x000DBAE3
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x000DD8ED File Offset: 0x000DBAED
		private bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.Value = postCollection[postDataKey];
			return false;
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x000DD8FD File Offset: 0x000DBAFD
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
		}
	}
}
