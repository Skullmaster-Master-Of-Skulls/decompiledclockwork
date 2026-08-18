using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace AutoComboBox.MyControls
{
	// Token: 0x0200008C RID: 140
	public class MyExpandableGroupBox : ExpandablePanel
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x0002F714 File Offset: 0x0002E714
		public MyExpandableGroupBox()
		{
			base.CreateControl();
			base.CanvasColor = SystemColors.Control;
			base.ColorSchemeStyle = 4;
			base.Style.Alignment = StringAlignment.Center;
			base.Style.Border = 1;
			base.Style.BorderColor.ColorSchemePart = 8;
			base.Style.GradientAngle = 90;
			base.TitleStyle.Alignment = StringAlignment.Center;
			base.TitleStyle.BackColor1.ColorSchemePart = 51;
			base.TitleStyle.BackColor2.ColorSchemePart = 52;
			base.TitleStyle.Border = 7;
			base.TitleStyle.BorderColor.ColorSchemePart = 53;
			base.TitleStyle.ForeColor.ColorSchemePart = 54;
			base.TitleStyle.GradientAngle = 90;
			base.ExpandOnTitleClick = true;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002F7FB File Offset: 0x0002E7FB
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0002F806 File Offset: 0x0002E806
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}
	}
}
