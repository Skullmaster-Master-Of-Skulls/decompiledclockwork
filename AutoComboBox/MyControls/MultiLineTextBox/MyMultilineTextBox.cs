using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.MultiLineTextBox
{
	// Token: 0x020000DA RID: 218
	public class MyMultilineTextBox : ListBox
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00042568 File Offset: 0x00041568
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00042580 File Offset: 0x00041580
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
				this.tbox.IsReadOnly = value;
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00042598 File Offset: 0x00041598
		public MyMultilineTextBox()
		{
			this.DoubleBuffered = true;
			this.tbox = new MyMultiLinePopupEdit();
			this.tbox.mllb = this;
			this.DrawMode = DrawMode.OwnerDrawVariable;
			base.ScrollAlwaysVisible = true;
			this.tbox.HideMe();
			base.Controls.Add(this.tbox);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00042604 File Offset: 0x00041604
		protected override void Dispose(bool disposing)
		{
			if (this.tbox != null)
			{
				this.tbox.Dispose();
				this.tbox = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0004263C File Offset: 0x0004163C
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.OnDoubleClick(new EventArgs());
			}
			base.OnKeyUp(e);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00042674 File Offset: 0x00041674
		protected override void OnMouseUp(MouseEventArgs e)
		{
			int num = base.IndexFromPoint(e.X, e.Y);
			if (num != -1 && num != 65535)
			{
				if (e.Button == MouseButtons.Right)
				{
					MultiLineItem multiLineItem = (MultiLineItem)base.Items[num];
					string text = multiLineItem.Text;
					Rectangle itemRectangle = base.GetItemRectangle(num);
					int num2 = base.Height - itemRectangle.Y - 4;
					if (num2 <= 0)
					{
						num2 = 10;
					}
					itemRectangle = new Rectangle(itemRectangle.X, itemRectangle.Y, itemRectangle.Width, num2);
					this.tbox.Location = new Point(itemRectangle.X, itemRectangle.Y);
					this.tbox.Size = new Size(itemRectangle.Width, itemRectangle.Height);
					this.tbox.Text = text;
					this.tbox.index = num;
					this.tbox.SelectAll();
					this.tbox.ShowMe(multiLineItem);
					this.tbox.Focus();
				}
			}
			base.OnMouseUp(e);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000427B0 File Offset: 0x000417B0
		protected override void OnDoubleClick(EventArgs e)
		{
			Point p = Cursor.Position;
			p = base.PointToClient(p);
			int num = base.IndexFromPoint(p.X, p.Y);
			if (num != -1 && num != 65535)
			{
				MultiLineItem multiLineItem = (MultiLineItem)base.Items[num];
				string text = multiLineItem.Text;
				Rectangle itemRectangle = base.GetItemRectangle(num);
				int num2 = base.Height - itemRectangle.Y - 4;
				if (num2 <= 0)
				{
					num2 = 10;
				}
				itemRectangle = new Rectangle(itemRectangle.X, itemRectangle.Y, itemRectangle.Width, num2);
				this.tbox.Location = new Point(itemRectangle.X, itemRectangle.Y);
				this.tbox.Size = new Size(itemRectangle.Width, itemRectangle.Height);
				this.tbox.Text = text;
				this.tbox.index = num;
				this.tbox.SelectAll();
				this.tbox.ShowMe(multiLineItem);
				this.tbox.Focus();
			}
			base.OnDoubleClick(e);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000428E2 File Offset: 0x000418E2
		public void SortAscending()
		{
			this.items.Sort(new MultiLineItemComparer());
			this.RefreshList();
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000428FD File Offset: 0x000418FD
		public void SortDescending()
		{
			this.items.Sort(new ReverseComparer(new MultiLineItemComparer()));
			this.RefreshList();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00042920 File Offset: 0x00041920
		public void RefreshList()
		{
			base.BeginUpdate();
			base.Items.Clear();
			foreach (object obj in this.items)
			{
				MultiLineItem item = (MultiLineItem)obj;
				base.Items.Add(item);
			}
			base.EndUpdate();
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000429A8 File Offset: 0x000419A8
		public void SetItems(string xml)
		{
			this.items = new MultiLineItemCollection(xml);
			this.RefreshList();
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000429C0 File Offset: 0x000419C0
		public void FixItem(int index)
		{
			MultiLineItem item = (MultiLineItem)base.Items[index];
			base.Items.RemoveAt(index);
			base.Items.Insert(index, item);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000429FC File Offset: 0x000419FC
		public void FixItem(MultiLineItem item)
		{
			for (int i = 0; i < base.Items.Count; i++)
			{
				MultiLineItem multiLineItem = (MultiLineItem)base.Items[i];
				if (multiLineItem == item)
				{
					this.FixItem(i);
				}
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00042A4C File Offset: 0x00041A4C
		public string GetItemsAsXml()
		{
			string result;
			if (base.Items.Count <= 0)
			{
				result = "";
			}
			else
			{
				DataTable dataTable = new DataTable("items");
				dataTable.Columns.Add("text");
				dataTable.Columns.Add("whoentered");
				dataTable.Columns.Add("dateentered");
				foreach (object obj in base.Items)
				{
					MultiLineItem multiLineItem = (MultiLineItem)obj;
					dataTable.Rows.Add(new object[]
					{
						multiLineItem.Text,
						multiLineItem.WhoEntered,
						multiLineItem.DateEnteredString
					});
				}
				StringBuilder stringBuilder = new StringBuilder();
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add(dataTable);
				StringWriter writer = new StringWriter(stringBuilder);
				dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00042B80 File Offset: 0x00041B80
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyData == Keys.F2)
			{
				int num = this.SelectedIndex;
				if (num == -1 || num == 65535)
				{
					if (base.Items.Count > 0)
					{
						num = 0;
					}
				}
				if (num != -1 && num != 65535)
				{
					MultiLineItem multiLineItem = (MultiLineItem)base.Items[num];
					string text = multiLineItem.Text;
					Rectangle itemRectangle = base.GetItemRectangle(num);
					if (text.Length > 0)
					{
						itemRectangle.Inflate(0, 75);
					}
					this.tbox.Location = new Point(itemRectangle.X, itemRectangle.Y);
					this.tbox.Size = new Size(itemRectangle.Width, itemRectangle.Height);
					this.tbox.Text = text;
					this.tbox.index = num;
					this.tbox.SelectAll();
					this.tbox.ShowMe(multiLineItem);
					this.tbox.Focus();
				}
			}
			base.OnKeyDown(e);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00042CC0 File Offset: 0x00041CC0
		public int AddItem(MultiLineItem item)
		{
			if (this.items == null)
			{
				this.items = new MultiLineItemCollection();
			}
			this.items.Add(item);
			return base.Items.Add(item);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00042D08 File Offset: 0x00041D08
		public void RemoveItemAt(int index)
		{
			try
			{
				MultiLineItem item = this.items[index];
				this.items.Remove(item);
			}
			catch
			{
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00042D4C File Offset: 0x00041D4C
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.Size = new Size(120, 95);
			base.ResumeLayout(false);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00042D70 File Offset: 0x00041D70
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (this.Site == null)
			{
				if (e.Index > -1)
				{
					MultiLineItem multiLineItem = (MultiLineItem)base.Items[e.Index];
					string text = multiLineItem.Header + Environment.NewLine + multiLineItem.Text;
					SizeF sizeF = e.Graphics.MeasureString(text, this.Font, base.Width);
					int num = (e.Index == 0) ? 15 : 10;
					int num2 = (int)sizeF.Height + num;
					if (num2 < 35)
					{
						num2 = 35;
					}
					if (num2 > 255)
					{
						num2 = 255;
					}
					e.ItemHeight = num2;
					e.ItemWidth = base.Width;
				}
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00042E50 File Offset: 0x00041E50
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.Site == null)
			{
				if (e.Index > -1)
				{
					MultiLineItem item = (MultiLineItem)base.Items[e.Index];
					Color window;
					if (e.Index % 2 == 0)
					{
						window = SystemColors.Window;
					}
					else
					{
						window = SystemColors.Window;
					}
					if ((e.State & DrawItemState.Focus) == DrawItemState.None)
					{
						using (Brush brush = new SolidBrush(window))
						{
							e.Graphics.FillRectangle(brush, e.Bounds);
						}
						this.DrawString(e.Graphics, item, this.Font, e.Bounds, SystemColors.Highlight);
					}
					else
					{
						using (Brush brush = new SolidBrush(SystemColors.Highlight))
						{
							e.Graphics.FillRectangle(brush, e.Bounds);
						}
						this.DrawString(e.Graphics, item, this.Font, e.Bounds, SystemColors.HighlightText);
						using (Pen pen = new Pen(SystemColors.WindowFrame))
						{
							e.Graphics.DrawRectangle(pen, e.Bounds);
						}
					}
				}
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00042FE8 File Offset: 0x00041FE8
		private void DrawString(Graphics g, MultiLineItem item, Font font, Rectangle bounds, Color penColour)
		{
			Rectangle r = bounds;
			using (Pen pen = new Pen(penColour))
			{
				using (Font font2 = new Font(font, FontStyle.Bold))
				{
					SizeF sizeF = g.MeasureString(item.Header, font2, r.Width);
					g.DrawString(item.Header, font2, pen.Brush, r);
					r.Offset(0, Convert.ToInt32(sizeF.Height));
				}
				g.DrawString(item.Text, font, pen.Brush, r);
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000430B4 File Offset: 0x000420B4
		public void ShowPopupSpellChecker()
		{
			if (this.tbox != null && this.tbox.Visible)
			{
				this.tbox.ShowSpellCheck();
			}
		}

		// Token: 0x0400063E RID: 1598
		private bool isReadOnly = false;

		// Token: 0x0400063F RID: 1599
		private MultiLineItemCollection items;

		// Token: 0x04000640 RID: 1600
		private MyMultiLinePopupEdit tbox;
	}
}
