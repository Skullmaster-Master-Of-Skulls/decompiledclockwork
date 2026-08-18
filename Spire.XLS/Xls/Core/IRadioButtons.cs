using System;

namespace Spire.Xls.Core
{
	// Token: 0x0200020E RID: 526
	public interface IRadioButtons
	{
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06001ED5 RID: 7893
		int Count { get; }

		// Token: 0x17000B5C RID: 2908
		IRadioButton this[int index]
		{
			get;
		}

		// Token: 0x17000B5D RID: 2909
		IRadioButton this[string name]
		{
			get;
		}

		// Token: 0x06001ED8 RID: 7896
		IRadioButton Add(int row, int column, int height, int width);

		// Token: 0x06001ED9 RID: 7897
		IRadioButton Add();

		// Token: 0x06001EDA RID: 7898
		IRadioButton Add(int row, int column);
	}
}
