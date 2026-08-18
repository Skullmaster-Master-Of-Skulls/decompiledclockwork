using System;
using System.Drawing;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x0200015E RID: 350
	internal sealed class AutoSizeComboBox : ComboBox
	{
		// Token: 0x06000C56 RID: 3158 RVA: 0x00050E5C File Offset: 0x0004F05C
		private void AutoSizeComboBoxDropDown()
		{
			int num = 0;
			using (Graphics graphics = Graphics.FromImage(new Bitmap(1, 1)))
			{
				foreach (object obj in base.Items)
				{
					if (obj != null)
					{
						num = Math.Max(num, graphics.MeasureString(obj.ToString(), this.Font, 0, new StringFormat(StringFormatFlags.FitBlackBox | StringFormatFlags.NoWrap)).ToSize().Width);
						if (num >= 600)
						{
							num = 600;
							break;
						}
					}
				}
			}
			int num2 = num + SystemInformation.VerticalScrollBarWidth + 2 * SystemInformation.BorderSize.Width;
			base.DropDownWidth = num2 + 1;
			base.DropDownWidth = num2;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00050F48 File Offset: 0x0004F148
		public void InvalidateDropDownWidth()
		{
			this._dropDownWidthValid = false;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00050F51 File Offset: 0x0004F151
		protected override void OnDropDown(EventArgs e)
		{
			if (!this._dropDownWidthValid)
			{
				this.AutoSizeComboBoxDropDown();
				this._dropDownWidthValid = true;
			}
			base.OnDropDown(e);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00050F6F File Offset: 0x0004F16F
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this._dropDownWidthValid = false;
		}

		// Token: 0x0400079E RID: 1950
		private const int MaxDropDownWidth = 600;

		// Token: 0x0400079F RID: 1951
		private bool _dropDownWidthValid;
	}
}
