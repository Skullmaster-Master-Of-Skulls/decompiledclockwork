using System;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000164 RID: 356
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class NumberEdit : TextBox
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0005165C File Offset: 0x0004F85C
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00051664 File Offset: 0x0004F864
		public bool AllowDecimal
		{
			get
			{
				return this.allowDecimal;
			}
			set
			{
				this.allowDecimal = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0005166D File Offset: 0x0004F86D
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00051675 File Offset: 0x0004F875
		public bool AllowNegative
		{
			get
			{
				return this.allowNegative;
			}
			set
			{
				this.allowNegative = value;
			}
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00051680 File Offset: 0x0004F880
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 258)
			{
				char c = (char)((int)m.WParam);
				if ((c < '0' || c > '9') && (!NumberFormatInfo.CurrentInfo.NumberDecimalSeparator.Contains(c.ToString(CultureInfo.CurrentCulture)) || !this.allowDecimal) && (!NumberFormatInfo.CurrentInfo.NegativeSign.Contains(c.ToString(CultureInfo.CurrentCulture)) || !this.allowNegative) && c != '\b')
				{
					Console.Beep();
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x040007AB RID: 1963
		private bool allowNegative = true;

		// Token: 0x040007AC RID: 1964
		private bool allowDecimal = true;
	}
}
