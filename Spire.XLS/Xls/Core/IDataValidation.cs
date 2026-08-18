using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x02000275 RID: 629
	public interface IDataValidation : IExcelApplication, IOptimizedUpdate
	{
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060025F9 RID: 9721
		// (set) Token: 0x060025FA RID: 9722
		string InputTitle { get; set; }

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060025FB RID: 9723
		// (set) Token: 0x060025FC RID: 9724
		string InputMessage { get; set; }

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060025FD RID: 9725
		// (set) Token: 0x060025FE RID: 9726
		string ErrorTitle { get; set; }

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060025FF RID: 9727
		// (set) Token: 0x06002600 RID: 9728
		string ErrorMessage { get; set; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002601 RID: 9729
		// (set) Token: 0x06002602 RID: 9730
		string Formula1 { get; set; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002603 RID: 9731
		// (set) Token: 0x06002604 RID: 9732
		DateTime DateTime1 { get; set; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002605 RID: 9733
		// (set) Token: 0x06002606 RID: 9734
		string Formula2 { get; set; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002607 RID: 9735
		// (set) Token: 0x06002608 RID: 9736
		DateTime DateTime2 { get; set; }

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002609 RID: 9737
		// (set) Token: 0x0600260A RID: 9738
		CellDataType AllowType { get; set; }

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600260B RID: 9739
		// (set) Token: 0x0600260C RID: 9740
		ValidationComparisonOperator CompareOperator { get; set; }

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600260D RID: 9741
		// (set) Token: 0x0600260E RID: 9742
		bool IsListInFormula { get; set; }

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600260F RID: 9743
		// (set) Token: 0x06002610 RID: 9744
		bool IgnoreBlank { get; set; }

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06002611 RID: 9745
		// (set) Token: 0x06002612 RID: 9746
		bool IsSuppressDropDownArrow { get; set; }

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002613 RID: 9747
		// (set) Token: 0x06002614 RID: 9748
		bool ShowInput { get; set; }

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002615 RID: 9749
		// (set) Token: 0x06002616 RID: 9750
		bool ShowError { get; set; }

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002617 RID: 9751
		// (set) Token: 0x06002618 RID: 9752
		int PromptBoxHPosition { get; set; }

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002619 RID: 9753
		// (set) Token: 0x0600261A RID: 9754
		int PromptBoxVPosition { get; set; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600261B RID: 9755
		// (set) Token: 0x0600261C RID: 9756
		bool IsInputVisible { get; set; }

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x0600261D RID: 9757
		// (set) Token: 0x0600261E RID: 9758
		bool IsInputPositionFixed { get; set; }

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600261F RID: 9759
		// (set) Token: 0x06002620 RID: 9760
		AlertStyleType AlertStyle { get; set; }

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06002621 RID: 9761
		// (set) Token: 0x06002622 RID: 9762
		string[] Values { get; set; }

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06002623 RID: 9763
		// (set) Token: 0x06002624 RID: 9764
		IXLSRange DataRange { get; set; }
	}
}
