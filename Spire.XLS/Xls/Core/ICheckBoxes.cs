using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000213 RID: 531
	public interface ICheckBoxes
	{
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06001EE8 RID: 7912
		int Count { get; }

		// Token: 0x17000B65 RID: 2917
		ICheckBoxShape this[int index]
		{
			get;
		}

		// Token: 0x06001EEA RID: 7914
		ICheckBoxShape AddCheckBox(int row, int column, int height, int width);
	}
}
