using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000CB RID: 203
	public class MyMonthCalendar : MonthCalendar
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060007CD RID: 1997 RVA: 0x0003E0C8 File Offset: 0x0003D0C8
		// (remove) Token: 0x060007CE RID: 1998 RVA: 0x0003E104 File Offset: 0x0003D104
		public new event EventHandler DoubleClick;

		// Token: 0x060007CF RID: 1999 RVA: 0x0003E140 File Offset: 0x0003D140
		protected override void OnDoubleClick(EventArgs e)
		{
			if (this.DoubleClick != null)
			{
				this.DoubleClick(this, new EventArgs());
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0003E170 File Offset: 0x0003D170
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (!this.m_LastClickRaisedDoubleClick && DateTime.Now.Ticks - this.m_LastClickTime <= (long)(SystemInformation.DoubleClickTime * 10000) && this.IsInDoubleClickArea(this.m_LastClickPosition, Cursor.Position))
				{
					this.OnDoubleClick(EventArgs.Empty);
					this.m_LastClickRaisedDoubleClick = true;
				}
				else
				{
					this.m_LastClickRaisedDoubleClick = false;
				}
				this.m_LastClickPosition = Cursor.Position;
				this.m_LastClickTime = DateTime.Now.Ticks;
			}
			base.OnMouseDown(e);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0003E224 File Offset: 0x0003D224
		private bool IsInDoubleClickArea(Point Point1, Point Point2)
		{
			return Math.Abs(Point1.X - Point2.X) <= SystemInformation.DoubleClickSize.Width && Math.Abs(Point1.Y - Point2.Y) <= SystemInformation.DoubleClickSize.Height;
		}

		// Token: 0x040005EE RID: 1518
		private Point m_LastClickPosition;

		// Token: 0x040005EF RID: 1519
		private long m_LastClickTime;

		// Token: 0x040005F0 RID: 1520
		private bool m_LastClickRaisedDoubleClick;
	}
}
