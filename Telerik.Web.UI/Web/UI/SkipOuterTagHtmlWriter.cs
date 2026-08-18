using System;
using System.IO;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FD6 RID: 4054
	internal class SkipOuterTagHtmlWriter : HtmlTextWriter
	{
		// Token: 0x06009D96 RID: 40342 RVA: 0x0023278C File Offset: 0x0023098C
		internal SkipOuterTagHtmlWriter() : base(new StringWriter())
		{
		}

		// Token: 0x170031CE RID: 12750
		// (get) Token: 0x06009D97 RID: 40343 RVA: 0x002327A0 File Offset: 0x002309A0
		public string Buffer
		{
			get
			{
				return base.InnerWriter.ToString();
			}
		}

		// Token: 0x06009D98 RID: 40344 RVA: 0x002327AD File Offset: 0x002309AD
		protected override bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			if (this.firstTag)
			{
				this.firstTag = false;
				return false;
			}
			return base.OnTagRender(name, key);
		}

		// Token: 0x04002C5C RID: 11356
		private bool firstTag = true;
	}
}
