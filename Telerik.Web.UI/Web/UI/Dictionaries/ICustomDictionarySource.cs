using System;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011D0 RID: 4560
	public interface ICustomDictionarySource
	{
		// Token: 0x17003CE1 RID: 15585
		// (get) Token: 0x0600BC7F RID: 48255
		// (set) Token: 0x0600BC80 RID: 48256
		string DictionaryPath { get; set; }

		// Token: 0x17003CE2 RID: 15586
		// (get) Token: 0x0600BC81 RID: 48257
		// (set) Token: 0x0600BC82 RID: 48258
		string Language { get; set; }

		// Token: 0x17003CE3 RID: 15587
		// (get) Token: 0x0600BC83 RID: 48259
		// (set) Token: 0x0600BC84 RID: 48260
		string CustomAppendix { get; set; }

		// Token: 0x0600BC85 RID: 48261
		string ReadWord();

		// Token: 0x0600BC86 RID: 48262
		void AddWord(string word);
	}
}
