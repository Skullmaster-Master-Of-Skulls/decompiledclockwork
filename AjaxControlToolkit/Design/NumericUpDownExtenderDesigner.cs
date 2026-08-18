using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000150 RID: 336
	public class NumericUpDownExtenderDesigner : ExtenderControlBaseDesigner<NumericUpDownExtender>
	{
		// Token: 0x02000151 RID: 337
		// (Invoke) Token: 0x060008CA RID: 2250
		[PageMethodSignature("\"Get Next\" NumericUpDown", "ServiceUpPath", "ServiceUpMethod")]
		private delegate int GetNextValue(int current, string tag);

		// Token: 0x02000152 RID: 338
		// (Invoke) Token: 0x060008CE RID: 2254
		[PageMethodSignature("\"Get Previous\" NumericUpDown", "ServiceDownPath", "ServiceDownMethod")]
		private delegate int GetPreviousValue(int current, string tag);
	}
}
