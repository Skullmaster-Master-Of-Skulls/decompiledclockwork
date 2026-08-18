using System;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CF RID: 1231
	internal sealed class LayoutTransaction : IDisposable
	{
		// Token: 0x060050D8 RID: 20696 RVA: 0x001505AF File Offset: 0x0014E7AF
		public LayoutTransaction(Control controlToLayout, IArrangedElement controlCausingLayout, string property) : this(controlToLayout, controlCausingLayout, property, true)
		{
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x001505BC File Offset: 0x0014E7BC
		public LayoutTransaction(Control controlToLayout, IArrangedElement controlCausingLayout, string property, bool resumeLayout)
		{
			CommonProperties.xClearPreferredSizeCache(controlCausingLayout);
			this._controlToLayout = controlToLayout;
			this._resumeLayout = resumeLayout;
			if (this._controlToLayout != null)
			{
				this._controlToLayout.SuspendLayout();
				CommonProperties.xClearPreferredSizeCache(this._controlToLayout);
				if (resumeLayout)
				{
					this._controlToLayout.PerformLayout(new LayoutEventArgs(controlCausingLayout, property));
				}
			}
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00150618 File Offset: 0x0014E818
		public void Dispose()
		{
			if (this._controlToLayout != null)
			{
				this._controlToLayout.ResumeLayout(this._resumeLayout);
			}
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x00150634 File Offset: 0x0014E834
		public static IDisposable CreateTransactionIf(bool condition, Control controlToLayout, IArrangedElement elementCausingLayout, string property)
		{
			if (condition)
			{
				return new LayoutTransaction(controlToLayout, elementCausingLayout, property);
			}
			CommonProperties.xClearPreferredSizeCache(elementCausingLayout);
			return default(NullLayoutTransaction);
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x00150661 File Offset: 0x0014E861
		public static void DoLayout(IArrangedElement elementToLayout, IArrangedElement elementCausingLayout, string property)
		{
			if (elementCausingLayout != null)
			{
				CommonProperties.xClearPreferredSizeCache(elementCausingLayout);
				if (elementToLayout != null)
				{
					CommonProperties.xClearPreferredSizeCache(elementToLayout);
					elementToLayout.PerformLayout(elementCausingLayout, property);
				}
			}
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0015067D File Offset: 0x0014E87D
		public static void DoLayoutIf(bool condition, IArrangedElement elementToLayout, IArrangedElement elementCausingLayout, string property)
		{
			if (!condition)
			{
				if (elementCausingLayout != null)
				{
					CommonProperties.xClearPreferredSizeCache(elementCausingLayout);
					return;
				}
			}
			else
			{
				LayoutTransaction.DoLayout(elementToLayout, elementCausingLayout, property);
			}
		}

		// Token: 0x040034AA RID: 13482
		private Control _controlToLayout;

		// Token: 0x040034AB RID: 13483
		private bool _resumeLayout;
	}
}
