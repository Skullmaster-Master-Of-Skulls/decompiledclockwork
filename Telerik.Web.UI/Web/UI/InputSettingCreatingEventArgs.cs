using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200190B RID: 6411
	public class InputSettingCreatingEventArgs : EventArgs
	{
		// Token: 0x0600F8CC RID: 63692 RVA: 0x003830D4 File Offset: 0x003812D4
		public InputSettingCreatingEventArgs(TextBox textBox, TargetInput targetInput, InputSetting inputSetting)
		{
			this.textBox = textBox;
			this.targetInput = targetInput;
			this.inputSetting = inputSetting;
		}

		// Token: 0x17004B30 RID: 19248
		// (get) Token: 0x0600F8CD RID: 63693 RVA: 0x003830F1 File Offset: 0x003812F1
		// (set) Token: 0x0600F8CE RID: 63694 RVA: 0x003830F9 File Offset: 0x003812F9
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
			set
			{
				this.canceled = value;
			}
		}

		// Token: 0x17004B31 RID: 19249
		// (get) Token: 0x0600F8CF RID: 63695 RVA: 0x00383102 File Offset: 0x00381302
		public TextBox TextBox
		{
			get
			{
				return this.textBox;
			}
		}

		// Token: 0x17004B32 RID: 19250
		// (get) Token: 0x0600F8D0 RID: 63696 RVA: 0x0038310A File Offset: 0x0038130A
		public TargetInput TargetInput
		{
			get
			{
				return this.targetInput;
			}
		}

		// Token: 0x17004B33 RID: 19251
		// (get) Token: 0x0600F8D1 RID: 63697 RVA: 0x00383112 File Offset: 0x00381312
		public InputSetting InputSetting
		{
			get
			{
				return this.inputSetting;
			}
		}

		// Token: 0x040046CC RID: 18124
		private TextBox textBox;

		// Token: 0x040046CD RID: 18125
		private TargetInput targetInput;

		// Token: 0x040046CE RID: 18126
		private InputSetting inputSetting;

		// Token: 0x040046CF RID: 18127
		private bool canceled;
	}
}
