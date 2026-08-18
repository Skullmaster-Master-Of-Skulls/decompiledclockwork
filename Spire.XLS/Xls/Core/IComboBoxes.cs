using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000206 RID: 518
	public interface IComboBoxes
	{
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06001DB0 RID: 7600
		int Count { get; }

		// Token: 0x17000B0D RID: 2829
		IComboBoxShape this[int index]
		{
			get;
		}

		// Token: 0x06001DB2 RID: 7602
		IComboBoxShape AddComboBox(int row, int column, int height, int width);
	}
}
