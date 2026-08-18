using System;
using System.IO;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001812 RID: 6162
	internal class WhiteSpaceStrippingHtmlTextWriter : HtmlTextWriter
	{
		// Token: 0x0600F002 RID: 61442 RVA: 0x0036A1AE File Offset: 0x003683AE
		public WhiteSpaceStrippingHtmlTextWriter(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x0600F003 RID: 61443 RVA: 0x0036A1B7 File Offset: 0x003683B7
		protected override void OutputTabs()
		{
		}

		// Token: 0x0600F004 RID: 61444 RVA: 0x0036A1B9 File Offset: 0x003683B9
		public override void WriteLine()
		{
		}
	}
}
