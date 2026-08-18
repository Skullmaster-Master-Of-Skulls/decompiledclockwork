using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000211 RID: 529
	public interface ITextBoxes
	{
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06001EE0 RID: 7904
		int Count { get; }

		// Token: 0x17000B61 RID: 2913
		ITextBoxShape this[int index]
		{
			get;
		}

		// Token: 0x06001EE2 RID: 7906
		ITextBoxShape AddTextBox(int row, int column, int height, int width);
	}
}
