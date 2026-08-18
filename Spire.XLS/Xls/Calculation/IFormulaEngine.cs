using System;

namespace Spire.Xls.Calculation
{
	// Token: 0x020001AF RID: 431
	public interface IFormulaEngine
	{
		// Token: 0x0600175D RID: 5981
		object GetCaculateValue(int row, int col);

		// Token: 0x0600175E RID: 5982
		void SetCaculateValue(object value, int row, int col);

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600175F RID: 5983
		// (remove) Token: 0x06001760 RID: 5984
		event ValueChangedEventHandler CaculateValueChanged;
	}
}
