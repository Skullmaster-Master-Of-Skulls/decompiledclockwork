using System;
using System.ComponentModel;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050E RID: 1294
	public interface IRootGridEntry
	{
		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x060054CD RID: 21709
		// (set) Token: 0x060054CE RID: 21710
		AttributeCollection BrowsableAttributes { get; set; }

		// Token: 0x060054CF RID: 21711
		void ResetBrowsableAttributes();

		// Token: 0x060054D0 RID: 21712
		void ShowCategories(bool showCategories);
	}
}
