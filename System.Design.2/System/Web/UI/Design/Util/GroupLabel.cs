using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000161 RID: 353
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class GroupLabel : Label
	{
		// Token: 0x06000C6B RID: 3179 RVA: 0x0005126A File Offset: 0x0004F46A
		public GroupLabel()
		{
			base.SetStyle(ControlStyles.UserPaint, true);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0005127C File Offset: 0x0004F47C
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = base.ClientRectangle;
			string text = this.Text;
			Brush brush = new SolidBrush(this.ForeColor);
			graphics.DrawString(text, this.Font, brush, 0f, 0f);
			brush.Dispose();
			int num = clientRectangle.X;
			if (text.Length != 0)
			{
				num += 4 + Size.Ceiling(graphics.MeasureString(text, this.Font)).Width;
			}
			int num2 = clientRectangle.Height / 2;
			graphics.DrawLine(SystemPens.ControlDark, num, num2, clientRectangle.Width, num2);
			num2++;
			graphics.DrawLine(SystemPens.ControlLightLight, num, num2, clientRectangle.Width, num2);
		}
	}
}
