using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001082 RID: 4226
	public sealed class SpellCheckerLanguageCollection : EditorNameValueItemCollection<SpellCheckerLanguage>
	{
		// Token: 0x0600A9F6 RID: 43510 RVA: 0x0024DF0D File Offset: 0x0024C10D
		public new void Add(string title, string code)
		{
			base.Add(title, code);
		}

		// Token: 0x0600A9F7 RID: 43511 RVA: 0x0024DF17 File Offset: 0x0024C117
		internal SpellCheckerLanguageCollection()
		{
		}
	}
}
