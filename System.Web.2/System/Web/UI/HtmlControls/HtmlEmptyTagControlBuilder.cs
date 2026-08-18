using System;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000346 RID: 838
	public sealed class HtmlEmptyTagControlBuilder : ControlBuilder
	{
		// Token: 0x060026A4 RID: 9892 RVA: 0x00007722 File Offset: 0x00005922
		public override bool HasBody()
		{
			return false;
		}
	}
}
