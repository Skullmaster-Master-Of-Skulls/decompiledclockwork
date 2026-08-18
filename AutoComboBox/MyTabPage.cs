using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000CA RID: 202
	public class MyTabPage : UserControl
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x0003DFB5 File Offset: 0x0003CFB5
		public MyTabPage()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0003DFD0 File Offset: 0x0003CFD0
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0003DFE8 File Offset: 0x0003CFE8
		public new string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0003DFF2 File Offset: 0x0003CFF2
		public MyTabPage(string text)
		{
			this.text = text;
			this.InitializeComponent();
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0003E014 File Offset: 0x0003D014
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0003E04F File Offset: 0x0003D04F
		private void InitializeComponent()
		{
			this.AutoScroll = true;
			base.Name = "MyTabPage";
			base.Size = new Size(592, 464);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0003E07C File Offset: 0x0003D07C
		public static MyTabPage FindParentTabPage(Control containedControl)
		{
			MyTabPage result;
			if (containedControl is MyTabPage)
			{
				result = (MyTabPage)containedControl;
			}
			else if (containedControl.Parent == null)
			{
				result = null;
			}
			else
			{
				result = MyTabPage.FindParentTabPage(containedControl.Parent);
			}
			return result;
		}

		// Token: 0x040005EC RID: 1516
		private Container components = null;

		// Token: 0x040005ED RID: 1517
		private string text;
	}
}
