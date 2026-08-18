using System;

namespace Telerik.Web.UI.Spell
{
	// Token: 0x020011EC RID: 4588
	public interface ITextWord
	{
		// Token: 0x17003D26 RID: 15654
		// (get) Token: 0x0600BD8A RID: 48522
		string Word { get; }

		// Token: 0x17003D27 RID: 15655
		// (get) Token: 0x0600BD8B RID: 48523
		int Offset { get; }

		// Token: 0x17003D28 RID: 15656
		// (get) Token: 0x0600BD8C RID: 48524
		string HtmlWord { get; }

		// Token: 0x0600BD8D RID: 48525
		bool StartsWithUpper();

		// Token: 0x0600BD8E RID: 48526
		bool AllUpper();

		// Token: 0x0600BD8F RID: 48527
		void MakeUpper();
	}
}
