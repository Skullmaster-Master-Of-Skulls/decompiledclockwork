using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000E6 RID: 230
	public class TimeBlockChooser : UserControl
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x00044BC0 File Offset: 0x00043BC0
		public TimeBlockChooser()
		{
			this.InitializeComponent();
			this._highlightedBlocks = new int[this._numBlocks];
			for (int i = 0; i < this._numBlocks; i++)
			{
				this._highlightedBlocks[i] = 0;
			}
			base.Invalidated += this.TimeBlockChooser_Invalidated;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00044C94 File Offset: 0x00043C94
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

		// Token: 0x060008E5 RID: 2277 RVA: 0x00044CD0 File Offset: 0x00043CD0
		private void InitializeComponent()
		{
			this.panel1 = new Panel();
			this.lbl_title = new Label();
			this.btn_clear = new Button();
			this.btn_830_to_430 = new Button();
			this.button1 = new Button();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.lbl_title);
			this.panel1.Controls.Add(this.btn_clear);
			this.panel1.Controls.Add(this.btn_830_to_430);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = DockStyle.Top;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(540, 18);
			this.panel1.TabIndex = 0;
			this.lbl_title.Dock = DockStyle.Fill;
			this.lbl_title.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbl_title.Location = new Point(0, 0);
			this.lbl_title.Name = "lbl_title";
			this.lbl_title.Size = new Size(342, 18);
			this.lbl_title.TabIndex = 3;
			this.lbl_title.Text = "Title";
			this.lbl_title.TextAlign = ContentAlignment.MiddleLeft;
			this.btn_clear.Dock = DockStyle.Right;
			this.btn_clear.Font = new Font("Arial", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btn_clear.Location = new Point(342, 0);
			this.btn_clear.Name = "btn_clear";
			this.btn_clear.Size = new Size(66, 18);
			this.btn_clear.TabIndex = 2;
			this.btn_clear.Text = "CLEAR";
			this.btn_clear.Click += this.button3_Click;
			this.btn_830_to_430.Dock = DockStyle.Right;
			this.btn_830_to_430.Font = new Font("Arial", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btn_830_to_430.Location = new Point(408, 0);
			this.btn_830_to_430.Name = "btn_830_to_430";
			this.btn_830_to_430.Size = new Size(66, 18);
			this.btn_830_to_430.TabIndex = 1;
			this.btn_830_to_430.Text = "8:30 - 4:30";
			this.btn_830_to_430.Click += this.button2_Click;
			this.button1.Dock = DockStyle.Right;
			this.button1.Font = new Font("Arial", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button1.Location = new Point(474, 0);
			this.button1.Name = "button1";
			this.button1.Size = new Size(66, 18);
			this.button1.TabIndex = 0;
			this.button1.Text = "ANY TIME";
			this.button1.Visible = false;
			base.Controls.Add(this.panel1);
			this.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Name = "TimeBlockChooser";
			base.Size = new Size(540, 60);
			base.MouseMove += this.TimeBlockChooser_MouseMove;
			base.MouseDown += this.TimeBlockChooser_MouseDown;
			base.MouseUp += this.TimeBlockChooser_MouseUp;
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000450D4 File Offset: 0x000440D4
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x000450EC File Offset: 0x000440EC
		public bool DrawTimeLabels
		{
			get
			{
				return this._drawTimeLabels;
			}
			set
			{
				this._drawTimeLabels = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000450F8 File Offset: 0x000440F8
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x00045110 File Offset: 0x00044110
		public int HourStart
		{
			get
			{
				return this._hourStart;
			}
			set
			{
				this._hourStart = value;
				this.SetBlockWidthAndNumBlocks_BasedOnHourStartAndHourEnd();
				base.Invalidate();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00045128 File Offset: 0x00044128
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x00045140 File Offset: 0x00044140
		public int HourEnd
		{
			get
			{
				return this._hourEnd;
			}
			set
			{
				this._hourEnd = value;
				this.SetBlockWidthAndNumBlocks_BasedOnHourStartAndHourEnd();
				base.Invalidate();
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00045158 File Offset: 0x00044158
		private void SetBlockWidthAndNumBlocks_BasedOnHourStartAndHourEnd()
		{
			if (this._hourStart < this._hourEnd)
			{
				int num = this._hourEnd - this._hourStart;
				this._numBlocks = num * 4;
				this._blockWidth = Convert.ToInt32(base.Width / this._numBlocks) * 2;
				this._numMinutesPerBlock = Convert.ToInt32(num * 60 / this._numBlocks);
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000451C4 File Offset: 0x000441C4
		public void BeginUpdate()
		{
			this._noDrawing = true;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000451CE File Offset: 0x000441CE
		public void EndUpdate()
		{
			this._noDrawing = false;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000451D8 File Offset: 0x000441D8
		protected override void OnPaint(PaintEventArgs e)
		{
			if (this._blockWidth > 0)
			{
				using (SolidBrush solidBrush = new SolidBrush(this._blockHighlightColour))
				{
					using (SolidBrush solidBrush2 = new SolidBrush(this._blockBackColour))
					{
						Pen pen = new Pen(this._blockForeColour);
						int num = this._hourStart * 60;
						for (int i = 0; i < this._numBlocks; i++)
						{
							int num2 = i * this._blockWidth + 2;
							int num3 = this.lbl_title.Height + 3;
							if (this._highlightedBlocks[i] > 0)
							{
								e.Graphics.FillRectangle(solidBrush, num2, num3, this._blockWidth, this._blockWidth);
							}
							else
							{
								e.Graphics.FillRectangle(solidBrush2, num2, num3, this._blockWidth, this._blockWidth);
							}
							e.Graphics.DrawRectangle(pen, num2, num3, this._blockWidth, this._blockWidth);
							DateTime dateTime = new DateTime(2000, 1, 1);
							dateTime = dateTime.AddMinutes((double)num);
							string text = dateTime.ToString("hh:mm");
							if (dateTime.Hour == 12 || dateTime.Hour == 0)
							{
								text += dateTime.ToString(" tt");
							}
							if (i % 2 == 0)
							{
								num3 += this._blockWidth;
								e.Graphics.DrawLine(pen, num2, num3, num2, num3 + 4);
								e.Graphics.DrawLine(pen, num2 + 1, num3, num2 + 1, num3 + 4);
								num3 += 6;
								e.Graphics.DrawString(text, this.Font, new SolidBrush(this.ForeColor), (float)num2, (float)num3);
							}
							num += this._numMinutesPerBlock;
						}
					}
				}
			}
			base.OnPaint(e);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00045430 File Offset: 0x00044430
		private void DrawBlock(Graphics g, int blockIndex)
		{
			SolidBrush brush;
			if (this._highlightedBlocks[blockIndex] > 0)
			{
				brush = new SolidBrush(this._blockHighlightColour);
			}
			else
			{
				brush = new SolidBrush(this._blockBackColour);
			}
			Pen pen = new Pen(this._blockForeColour);
			int x = blockIndex * this._blockWidth + 2;
			int y = this.lbl_title.Height + 3;
			g.FillRectangle(brush, x, y, this._blockWidth, this._blockWidth);
			g.DrawRectangle(pen, x, y, this._blockWidth, this._blockWidth);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000454C0 File Offset: 0x000444C0
		private void TimeBlockChooser_Invalidated(object sender, InvalidateEventArgs e)
		{
			this._numBlocks = (this._hourEnd - this._hourStart) * 2;
			if (this._highlightedBlocks == null)
			{
				this._highlightedBlocks = new int[this._numBlocks];
				for (int i = 0; i < this._highlightedBlocks.Length; i++)
				{
					this._highlightedBlocks[i] = 0;
				}
			}
			else if (this._highlightedBlocks.Length != this._numBlocks)
			{
				this._highlightedBlocks = new int[this._numBlocks];
				for (int i = 0; i < this._highlightedBlocks.Length; i++)
				{
					this._highlightedBlocks[i] = 0;
				}
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0004556F File Offset: 0x0004456F
		protected override void OnSizeChanged(EventArgs e)
		{
			this.SetBlockWidthAndNumBlocks_BasedOnHourStartAndHourEnd();
			base.Invalidate();
			base.OnSizeChanged(e);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00045588 File Offset: 0x00044588
		private void TimeBlockChooser_MouseDown(object sender, MouseEventArgs e)
		{
			int num = this.lbl_title.Height + 2;
			int num2 = num + this._blockWidth;
			if (e.Y >= num && e.Y <= num2)
			{
				int blockIndex = this.GetBlockIndex(e.X);
				if (blockIndex >= 0)
				{
					bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
					Graphics g = base.CreateGraphics();
					this._dragStartBlockIndex = blockIndex;
					if (!flag)
					{
						for (int i = 0; i < this._highlightedBlocks.Length; i++)
						{
							this._highlightedBlocks[i] = 0;
							this.DrawBlock(g, i);
						}
					}
					this.lbl_title.Text = "e.x=" + e.X.ToString() + ", currBlockIndex=" + blockIndex.ToString();
					if (this._highlightedBlocks[blockIndex] == 0)
					{
						this._highlightedBlocks[blockIndex] = 1;
						this.DrawBlock(g, blockIndex);
					}
					this._dragLastBlockIndex = blockIndex;
					this._dragging = true;
				}
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000456B4 File Offset: 0x000446B4
		private void TimeBlockChooser_MouseUp(object sender, MouseEventArgs e)
		{
			if (this._dragging)
			{
			}
			this._dragging = false;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000456D8 File Offset: 0x000446D8
		private int GetBlockIndex(int x)
		{
			int num;
			if (x >= 0 && x < base.Width)
			{
				num = (int)(Convert.ToDouble(x) / Convert.ToDouble(this._blockWidth));
			}
			else
			{
				num = -1;
			}
			if (num >= this._numBlocks)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0004572C File Offset: 0x0004472C
		private void TimeBlockChooser_MouseMove(object sender, MouseEventArgs e)
		{
			if (this._dragging)
			{
				int blockIndex = this.GetBlockIndex(e.X);
				if (blockIndex >= 0)
				{
					if (blockIndex != this._dragLastBlockIndex)
					{
						Graphics g = base.CreateGraphics();
						int dragStartBlockIndex = this._dragStartBlockIndex;
						int num = blockIndex;
						int dragLastBlockIndex = this._dragLastBlockIndex;
						int num2 = dragLastBlockIndex;
						int num3;
						if (num > dragLastBlockIndex)
						{
							num3 = 1;
						}
						else
						{
							num3 = -1;
						}
						do
						{
							int num4 = 1;
							if (num3 < 0 && num2 > dragStartBlockIndex)
							{
								num4 = 0;
							}
							else if (num3 > 0 && num2 < dragStartBlockIndex)
							{
								num4 = 0;
							}
							if (this._highlightedBlocks[num2] != num4)
							{
								this._highlightedBlocks[num2] = num4;
								this.DrawBlock(g, num2);
							}
							num2 += num3;
						}
						while (num2 != num);
						if (this._highlightedBlocks[blockIndex] == 0)
						{
							this._highlightedBlocks[blockIndex] = 1;
							this.DrawBlock(g, blockIndex);
						}
						this._dragLastBlockIndex = blockIndex;
					}
				}
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00045866 File Offset: 0x00044866
		private void button2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00045869 File Offset: 0x00044869
		private void button3_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x04000670 RID: 1648
		private Panel panel1;

		// Token: 0x04000671 RID: 1649
		private Button button1;

		// Token: 0x04000672 RID: 1650
		private Button btn_830_to_430;

		// Token: 0x04000673 RID: 1651
		private Button btn_clear;

		// Token: 0x04000674 RID: 1652
		private Label lbl_title;

		// Token: 0x04000675 RID: 1653
		private Container components = null;

		// Token: 0x04000676 RID: 1654
		private bool _noDrawing = false;

		// Token: 0x04000677 RID: 1655
		private bool _drawTimeLabels = true;

		// Token: 0x04000678 RID: 1656
		private bool _ableToDrawTimeLabels = true;

		// Token: 0x04000679 RID: 1657
		private int _hourStart = 0;

		// Token: 0x0400067A RID: 1658
		private int _hourEnd = 23;

		// Token: 0x0400067B RID: 1659
		private bool _validated = false;

		// Token: 0x0400067C RID: 1660
		private int _blockWidth = 10;

		// Token: 0x0400067D RID: 1661
		private int _numBlocks = 46;

		// Token: 0x0400067E RID: 1662
		private int _numMinutesPerBlock = 30;

		// Token: 0x0400067F RID: 1663
		private bool _dragging = false;

		// Token: 0x04000680 RID: 1664
		private int _dragStartBlockIndex;

		// Token: 0x04000681 RID: 1665
		private int _dragLastBlockIndex;

		// Token: 0x04000682 RID: 1666
		private int[] _highlightedBlocks;

		// Token: 0x04000683 RID: 1667
		private Color _blockBackColour = Color.White;

		// Token: 0x04000684 RID: 1668
		private Color _blockForeColour = Color.Black;

		// Token: 0x04000685 RID: 1669
		private Color _blockHighlightColour = Color.DarkBlue;
	}
}
