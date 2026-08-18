using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002DC RID: 732
	public sealed class ListViewInsertionMark
	{
		// Token: 0x06002E32 RID: 11826 RVA: 0x000D19EB File Offset: 0x000CFBEB
		internal ListViewInsertionMark(ListView listView)
		{
			this.listView = listView;
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x000D1A05 File Offset: 0x000CFC05
		// (set) Token: 0x06002E34 RID: 11828 RVA: 0x000D1A0D File Offset: 0x000CFC0D
		public bool AppearsAfterItem
		{
			get
			{
				return this.appearsAfterItem;
			}
			set
			{
				if (this.appearsAfterItem != value)
				{
					this.appearsAfterItem = value;
					if (this.listView.IsHandleCreated)
					{
						this.UpdateListView();
					}
				}
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000D1A34 File Offset: 0x000CFC34
		public Rectangle Bounds
		{
			get
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				this.listView.SendMessage(4265, 0, ref rect);
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000D1A7A File Offset: 0x000CFC7A
		// (set) Token: 0x06002E37 RID: 11831 RVA: 0x000D1AB4 File Offset: 0x000CFCB4
		public Color Color
		{
			get
			{
				if (this.color.IsEmpty)
				{
					this.color = SafeNativeMethods.ColorFromCOLORREF((int)this.listView.SendMessage(4267, 0, 0));
				}
				return this.color;
			}
			set
			{
				if (this.color != value)
				{
					this.color = value;
					if (this.listView.IsHandleCreated)
					{
						this.listView.SendMessage(4266, 0, SafeNativeMethods.ColorToCOLORREF(this.color));
					}
				}
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x000D1B00 File Offset: 0x000CFD00
		// (set) Token: 0x06002E39 RID: 11833 RVA: 0x000D1B08 File Offset: 0x000CFD08
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				if (this.index != value)
				{
					this.index = value;
					if (this.listView.IsHandleCreated)
					{
						this.UpdateListView();
					}
				}
			}
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000D1B30 File Offset: 0x000CFD30
		public int NearestIndex(Point pt)
		{
			NativeMethods.POINT point = new NativeMethods.POINT();
			point.x = pt.X;
			point.y = pt.Y;
			NativeMethods.LVINSERTMARK lvinsertmark = new NativeMethods.LVINSERTMARK();
			UnsafeNativeMethods.SendMessage(new HandleRef(this.listView, this.listView.Handle), 4264, point, lvinsertmark);
			return lvinsertmark.iItem;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000D1B8C File Offset: 0x000CFD8C
		internal void UpdateListView()
		{
			NativeMethods.LVINSERTMARK lvinsertmark = new NativeMethods.LVINSERTMARK();
			lvinsertmark.dwFlags = (this.appearsAfterItem ? 1 : 0);
			lvinsertmark.iItem = this.index;
			UnsafeNativeMethods.SendMessage(new HandleRef(this.listView, this.listView.Handle), 4262, 0, lvinsertmark);
			if (!this.color.IsEmpty)
			{
				this.listView.SendMessage(4266, 0, SafeNativeMethods.ColorToCOLORREF(this.color));
			}
		}

		// Token: 0x04001329 RID: 4905
		private ListView listView;

		// Token: 0x0400132A RID: 4906
		private int index;

		// Token: 0x0400132B RID: 4907
		private Color color = Color.Empty;

		// Token: 0x0400132C RID: 4908
		private bool appearsAfterItem;
	}
}
