using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
	// Token: 0x0200017E RID: 382
	internal class DataGridCaption
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x0004F93C File Offset: 0x0004DB3C
		internal DataGridCaption(DataGrid dataGrid)
		{
			this.dataGrid = dataGrid;
			this.downButtonVisible = dataGrid.ParentRowsVisible;
			DataGridCaption.colorMap[0].OldColor = Color.White;
			DataGridCaption.colorMap[0].NewColor = this.ForeColor;
			this.OnGridFontChanged();
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
		internal void OnGridFontChanged()
		{
			if (this.dataGridFont == null || !this.dataGridFont.Equals(this.dataGrid.Font))
			{
				try
				{
					this.dataGridFont = new Font(this.dataGrid.Font, FontStyle.Bold);
				}
				catch
				{
				}
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0004FA14 File Offset: 0x0004DC14
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x0004FA1C File Offset: 0x0004DC1C
		internal bool BackButtonActive
		{
			get
			{
				return this.backActive;
			}
			set
			{
				if (this.backActive != value)
				{
					this.backActive = value;
					this.InvalidateCaptionRect(this.backButtonRect);
				}
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0004FA3A File Offset: 0x0004DC3A
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x0004FA42 File Offset: 0x0004DC42
		internal bool DownButtonActive
		{
			get
			{
				return this.downActive;
			}
			set
			{
				if (this.downActive != value)
				{
					this.downActive = value;
					this.InvalidateCaptionRect(this.downButtonRect);
				}
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x000447B2 File Offset: 0x000429B2
		internal static SolidBrush DefaultBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ActiveCaption;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0004FA60 File Offset: 0x0004DC60
		internal static Pen DefaultTextBorderPen
		{
			get
			{
				return new Pen(SystemColors.ActiveCaptionText);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x000447BE File Offset: 0x000429BE
		internal static SolidBrush DefaultForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ActiveCaptionText;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0004FA6C File Offset: 0x0004DC6C
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x0004FA7C File Offset: 0x0004DC7C
		internal Color BackColor
		{
			get
			{
				return this.backBrush.Color;
			}
			set
			{
				if (!this.backBrush.Color.Equals(value))
				{
					if (value.IsEmpty)
					{
						throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[]
						{
							"Caption BackColor"
						}));
					}
					this.backBrush = new SolidBrush(value);
					this.Invalidate();
				}
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0004FAE3 File Offset: 0x0004DCE3
		internal EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0004FAFE File Offset: 0x0004DCFE
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x0004FB18 File Offset: 0x0004DD18
		internal Font Font
		{
			get
			{
				if (this.textFont == null)
				{
					return this.dataGridFont;
				}
				return this.textFont;
			}
			set
			{
				if (this.textFont == null || !this.textFont.Equals(value))
				{
					this.textFont = value;
					if (this.dataGrid.Caption != null)
					{
						this.dataGrid.RecalculateFonts();
						this.dataGrid.PerformLayout();
						this.dataGrid.Invalidate();
					}
				}
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0004FB70 File Offset: 0x0004DD70
		internal bool ShouldSerializeFont()
		{
			return this.textFont != null && !this.textFont.Equals(this.dataGridFont);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0004FB90 File Offset: 0x0004DD90
		internal bool ShouldSerializeBackColor()
		{
			return !this.backBrush.Equals(DataGridCaption.DefaultBackBrush);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0004FBA5 File Offset: 0x0004DDA5
		internal void ResetBackColor()
		{
			if (this.ShouldSerializeBackColor())
			{
				this.backBrush = DataGridCaption.DefaultBackBrush;
				this.Invalidate();
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0004FBC0 File Offset: 0x0004DDC0
		internal void ResetForeColor()
		{
			if (this.ShouldSerializeForeColor())
			{
				this.foreBrush = DataGridCaption.DefaultForeBrush;
				this.Invalidate();
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0004FBDB File Offset: 0x0004DDDB
		internal bool ShouldSerializeForeColor()
		{
			return !this.foreBrush.Equals(DataGridCaption.DefaultForeBrush);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0004FBF0 File Offset: 0x0004DDF0
		internal void ResetFont()
		{
			this.textFont = null;
			this.Invalidate();
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0004FBFF File Offset: 0x0004DDFF
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x0004FC07 File Offset: 0x0004DE07
		internal string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					this.text = "";
				}
				else
				{
					this.text = value;
				}
				this.Invalidate();
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0004FC26 File Offset: 0x0004DE26
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x0004FC2E File Offset: 0x0004DE2E
		internal bool TextBorderVisible
		{
			get
			{
				return this.textBorderVisible;
			}
			set
			{
				this.textBorderVisible = value;
				this.Invalidate();
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0004FC3D File Offset: 0x0004DE3D
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x0004FC4C File Offset: 0x0004DE4C
		internal Color ForeColor
		{
			get
			{
				return this.foreBrush.Color;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[]
					{
						"Caption ForeColor"
					}));
				}
				this.foreBrush = new SolidBrush(value);
				DataGridCaption.colorMap[0].NewColor = this.ForeColor;
				this.Invalidate();
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0004FCA4 File Offset: 0x0004DEA4
		internal Point MinimumBounds
		{
			get
			{
				return DataGridCaption.minimumBounds;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x0004FCB3 File Offset: 0x0004DEB3
		internal bool BackButtonVisible
		{
			get
			{
				return this.backButtonVisible;
			}
			set
			{
				if (this.backButtonVisible != value)
				{
					this.backButtonVisible = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0004FCCB File Offset: 0x0004DECB
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x0004FCD3 File Offset: 0x0004DED3
		internal bool DownButtonVisible
		{
			get
			{
				return this.downButtonVisible;
			}
			set
			{
				if (this.downButtonVisible != value)
				{
					this.downButtonVisible = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0004FCEC File Offset: 0x0004DEEC
		protected virtual void AddEventHandler(object key, Delegate handler)
		{
			lock (this)
			{
				if (handler != null)
				{
					for (DataGridCaption.EventEntry next = this.eventList; next != null; next = next.next)
					{
						if (next.key == key)
						{
							next.handler = Delegate.Combine(next.handler, handler);
							return;
						}
					}
					this.eventList = new DataGridCaption.EventEntry(this.eventList, key, handler);
				}
			}
		}

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x0600162A RID: 5674 RVA: 0x0004FD6C File Offset: 0x0004DF6C
		// (remove) Token: 0x0600162B RID: 5675 RVA: 0x0004FD7F File Offset: 0x0004DF7F
		internal event EventHandler BackwardClicked
		{
			add
			{
				this.Events.AddHandler(DataGridCaption.EVENT_BACKWARDCLICKED, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataGridCaption.EVENT_BACKWARDCLICKED, value);
			}
		}

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x0600162C RID: 5676 RVA: 0x0004FD92 File Offset: 0x0004DF92
		// (remove) Token: 0x0600162D RID: 5677 RVA: 0x0004FDA5 File Offset: 0x0004DFA5
		internal event EventHandler CaptionClicked
		{
			add
			{
				this.Events.AddHandler(DataGridCaption.EVENT_CAPTIONCLICKED, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataGridCaption.EVENT_CAPTIONCLICKED, value);
			}
		}

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x0600162E RID: 5678 RVA: 0x0004FDB8 File Offset: 0x0004DFB8
		// (remove) Token: 0x0600162F RID: 5679 RVA: 0x0004FDCB File Offset: 0x0004DFCB
		internal event EventHandler DownClicked
		{
			add
			{
				this.Events.AddHandler(DataGridCaption.EVENT_DOWNCLICKED, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataGridCaption.EVENT_DOWNCLICKED, value);
			}
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0004FDDE File Offset: 0x0004DFDE
		private void Invalidate()
		{
			if (this.dataGrid != null)
			{
				this.dataGrid.InvalidateCaption();
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0004FDF3 File Offset: 0x0004DFF3
		private void InvalidateCaptionRect(Rectangle r)
		{
			if (this.dataGrid != null)
			{
				this.dataGrid.InvalidateCaptionRect(r);
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0004FE0C File Offset: 0x0004E00C
		private void InvalidateLocation(DataGridCaption.CaptionLocation loc)
		{
			Rectangle r;
			if (loc == DataGridCaption.CaptionLocation.BackButton)
			{
				r = this.backButtonRect;
				r.Inflate(1, 1);
				this.InvalidateCaptionRect(r);
				return;
			}
			if (loc != DataGridCaption.CaptionLocation.DownButton)
			{
				return;
			}
			r = this.downButtonRect;
			r.Inflate(1, 1);
			this.InvalidateCaptionRect(r);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004FE54 File Offset: 0x0004E054
		protected void OnBackwardClicked(EventArgs e)
		{
			if (this.backActive)
			{
				EventHandler eventHandler = (EventHandler)this.Events[DataGridCaption.EVENT_BACKWARDCLICKED];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0004FE8C File Offset: 0x0004E08C
		protected void OnCaptionClicked(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)this.Events[DataGridCaption.EVENT_CAPTIONCLICKED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0004FEBC File Offset: 0x0004E0BC
		protected void OnDownClicked(EventArgs e)
		{
			if (this.downActive && this.downButtonVisible)
			{
				EventHandler eventHandler = (EventHandler)this.Events[DataGridCaption.EVENT_DOWNCLICKED];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0004FEFC File Offset: 0x0004E0FC
		private Bitmap GetBitmap(string bitmapName)
		{
			Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap(typeof(DataGridCaption), bitmapName);
				bitmap.MakeTransparent();
			}
			catch (Exception ex)
			{
			}
			return bitmap;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0004FF38 File Offset: 0x0004E138
		private Bitmap GetBackButtonBmp(bool alignRight)
		{
			if (alignRight)
			{
				if (DataGridCaption.leftButtonBitmap_bidi == null)
				{
					DataGridCaption.leftButtonBitmap_bidi = this.GetBitmap("DataGridCaption.backarrow_bidi.bmp");
				}
				return DataGridCaption.leftButtonBitmap_bidi;
			}
			if (DataGridCaption.leftButtonBitmap == null)
			{
				DataGridCaption.leftButtonBitmap = this.GetBitmap("DataGridCaption.backarrow.bmp");
			}
			return DataGridCaption.leftButtonBitmap;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0004FF76 File Offset: 0x0004E176
		private Bitmap GetDetailsBmp()
		{
			if (DataGridCaption.magnifyingGlassBitmap == null)
			{
				DataGridCaption.magnifyingGlassBitmap = this.GetBitmap("DataGridCaption.Details.bmp");
			}
			return DataGridCaption.magnifyingGlassBitmap;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0004FF94 File Offset: 0x0004E194
		protected virtual Delegate GetEventHandler(object key)
		{
			Delegate result;
			lock (this)
			{
				for (DataGridCaption.EventEntry next = this.eventList; next != null; next = next.next)
				{
					if (next.key == key)
					{
						return next.handler;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0004FFF4 File Offset: 0x0004E1F4
		internal Rectangle GetBackButtonRect(Rectangle bounds, bool alignRight, int downButtonWidth)
		{
			Bitmap backButtonBmp = this.GetBackButtonBmp(false);
			Bitmap obj = backButtonBmp;
			Size size;
			lock (obj)
			{
				size = backButtonBmp.Size;
			}
			return new Rectangle(bounds.Right - 12 - downButtonWidth - size.Width, bounds.Y + 1 + 2, size.Width, size.Height);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0005006C File Offset: 0x0004E26C
		internal int GetDetailsButtonWidth()
		{
			int result = 0;
			Bitmap detailsBmp = this.GetDetailsBmp();
			Bitmap obj = detailsBmp;
			lock (obj)
			{
				result = detailsBmp.Size.Width;
			}
			return result;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000500BC File Offset: 0x0004E2BC
		internal Rectangle GetDetailsButtonRect(Rectangle bounds, bool alignRight)
		{
			Bitmap detailsBmp = this.GetDetailsBmp();
			Bitmap obj = detailsBmp;
			Size size;
			lock (obj)
			{
				size = detailsBmp.Size;
			}
			int width = size.Width;
			return new Rectangle(bounds.Right - 6 - width, bounds.Y + 1 + 2, width, size.Height);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0005012C File Offset: 0x0004E32C
		internal void Paint(Graphics g, Rectangle bounds, bool alignRight)
		{
			Size size = new Size((int)g.MeasureString(this.text, this.Font).Width + 2, this.Font.Height + 2);
			this.downButtonRect = this.GetDetailsButtonRect(bounds, alignRight);
			int detailsButtonWidth = this.GetDetailsButtonWidth();
			this.backButtonRect = this.GetBackButtonRect(bounds, alignRight, detailsButtonWidth);
			int num = this.backButtonVisible ? (this.backButtonRect.Width + 3 + 4) : 0;
			int num2 = (this.downButtonVisible && !this.dataGrid.ParentRowsIsEmpty()) ? (detailsButtonWidth + 3 + 4) : 0;
			int val = bounds.Width - 3 - num - num2;
			this.textRect = new Rectangle(bounds.X, bounds.Y + 1, Math.Min(val, 4 + size.Width), 4 + size.Height);
			if (alignRight)
			{
				this.textRect.X = bounds.Right - this.textRect.Width;
				this.backButtonRect.X = bounds.X + 12 + detailsButtonWidth;
				this.downButtonRect.X = bounds.X + 6;
			}
			g.FillRectangle(this.backBrush, bounds);
			if (this.backButtonVisible)
			{
				this.PaintBackButton(g, this.backButtonRect, alignRight);
				if (this.backActive && this.lastMouseLocation == DataGridCaption.CaptionLocation.BackButton)
				{
					this.backButtonRect.Inflate(1, 1);
					ControlPaint.DrawBorder3D(g, this.backButtonRect, this.backPressed ? Border3DStyle.SunkenInner : Border3DStyle.RaisedInner);
				}
			}
			this.PaintText(g, this.textRect, alignRight);
			if (this.downButtonVisible && !this.dataGrid.ParentRowsIsEmpty())
			{
				this.PaintDownButton(g, this.downButtonRect);
				if (this.lastMouseLocation == DataGridCaption.CaptionLocation.DownButton)
				{
					this.downButtonRect.Inflate(1, 1);
					ControlPaint.DrawBorder3D(g, this.downButtonRect, this.downPressed ? Border3DStyle.SunkenInner : Border3DStyle.RaisedInner);
				}
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00050310 File Offset: 0x0004E510
		private void PaintIcon(Graphics g, Rectangle bounds, Bitmap b)
		{
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetRemapTable(DataGridCaption.colorMap, ColorAdjustType.Bitmap);
			g.DrawImage(b, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, imageAttributes);
			imageAttributes.Dispose();
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00050350 File Offset: 0x0004E550
		private void PaintBackButton(Graphics g, Rectangle bounds, bool alignRight)
		{
			Bitmap backButtonBmp = this.GetBackButtonBmp(alignRight);
			Bitmap obj = backButtonBmp;
			lock (obj)
			{
				this.PaintIcon(g, bounds, backButtonBmp);
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00050398 File Offset: 0x0004E598
		private void PaintDownButton(Graphics g, Rectangle bounds)
		{
			Bitmap detailsBmp = this.GetDetailsBmp();
			Bitmap obj = detailsBmp;
			lock (obj)
			{
				this.PaintIcon(g, bounds, detailsBmp);
			}
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000503E0 File Offset: 0x0004E5E0
		private void PaintText(Graphics g, Rectangle bounds, bool alignToRight)
		{
			Rectangle rectangle = bounds;
			if (rectangle.Width <= 0 || rectangle.Height <= 0)
			{
				return;
			}
			if (this.textBorderVisible)
			{
				g.DrawRectangle(this.textBorderPen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				rectangle.Inflate(-1, -1);
			}
			Rectangle rect = rectangle;
			rect.Height = 2;
			g.FillRectangle(this.backBrush, rect);
			rect.Y = rectangle.Bottom - 2;
			g.FillRectangle(this.backBrush, rect);
			rect = new Rectangle(rectangle.X, rectangle.Y + 2, 2, rectangle.Height - 4);
			g.FillRectangle(this.backBrush, rect);
			rect.X = rectangle.Right - 2;
			g.FillRectangle(this.backBrush, rect);
			rectangle.Inflate(-2, -2);
			g.FillRectangle(this.backBrush, rectangle);
			StringFormat stringFormat = new StringFormat();
			if (alignToRight)
			{
				stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				stringFormat.Alignment = StringAlignment.Far;
			}
			g.DrawString(this.text, this.Font, this.foreBrush, rectangle, stringFormat);
			stringFormat.Dispose();
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0005051C File Offset: 0x0004E71C
		private DataGridCaption.CaptionLocation FindLocation(int x, int y)
		{
			if (!this.backButtonRect.IsEmpty && this.backButtonRect.Contains(x, y))
			{
				return DataGridCaption.CaptionLocation.BackButton;
			}
			if (!this.downButtonRect.IsEmpty && this.downButtonRect.Contains(x, y))
			{
				return DataGridCaption.CaptionLocation.DownButton;
			}
			if (!this.textRect.IsEmpty && this.textRect.Contains(x, y))
			{
				return DataGridCaption.CaptionLocation.Text;
			}
			return DataGridCaption.CaptionLocation.Nowhere;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00050584 File Offset: 0x0004E784
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x0005058C File Offset: 0x0004E78C
		private bool DownButtonDown
		{
			get
			{
				return this.downButtonDown;
			}
			set
			{
				if (this.downButtonDown != value)
				{
					this.downButtonDown = value;
					this.InvalidateLocation(DataGridCaption.CaptionLocation.DownButton);
				}
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000505A5 File Offset: 0x0004E7A5
		internal bool GetDownButtonDirection()
		{
			return this.DownButtonDown;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000505B0 File Offset: 0x0004E7B0
		internal void MouseDown(int x, int y)
		{
			DataGridCaption.CaptionLocation loc = this.FindLocation(x, y);
			switch (loc)
			{
			case DataGridCaption.CaptionLocation.BackButton:
				this.backPressed = true;
				this.InvalidateLocation(loc);
				return;
			case DataGridCaption.CaptionLocation.DownButton:
				this.downPressed = true;
				this.InvalidateLocation(loc);
				return;
			case DataGridCaption.CaptionLocation.Text:
				this.OnCaptionClicked(EventArgs.Empty);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00050604 File Offset: 0x0004E804
		internal void MouseUp(int x, int y)
		{
			DataGridCaption.CaptionLocation captionLocation = this.FindLocation(x, y);
			if (captionLocation != DataGridCaption.CaptionLocation.BackButton)
			{
				if (captionLocation == DataGridCaption.CaptionLocation.DownButton && this.downPressed)
				{
					this.downPressed = false;
					this.OnDownClicked(EventArgs.Empty);
					return;
				}
			}
			else if (this.backPressed)
			{
				this.backPressed = false;
				this.OnBackwardClicked(EventArgs.Empty);
			}
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00050658 File Offset: 0x0004E858
		internal void MouseLeft()
		{
			DataGridCaption.CaptionLocation loc = this.lastMouseLocation;
			this.lastMouseLocation = DataGridCaption.CaptionLocation.Nowhere;
			this.InvalidateLocation(loc);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0005067C File Offset: 0x0004E87C
		internal void MouseOver(int x, int y)
		{
			DataGridCaption.CaptionLocation loc = this.FindLocation(x, y);
			this.InvalidateLocation(this.lastMouseLocation);
			this.InvalidateLocation(loc);
			this.lastMouseLocation = loc;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000506AC File Offset: 0x0004E8AC
		protected virtual void RaiseEvent(object key, EventArgs e)
		{
			Delegate eventHandler = this.GetEventHandler(key);
			if (eventHandler != null)
			{
				((EventHandler)eventHandler)(this, e);
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000506D4 File Offset: 0x0004E8D4
		protected virtual void RemoveEventHandler(object key, Delegate handler)
		{
			lock (this)
			{
				if (handler != null)
				{
					DataGridCaption.EventEntry next = this.eventList;
					DataGridCaption.EventEntry eventEntry = null;
					while (next != null)
					{
						if (next.key == key)
						{
							next.handler = Delegate.Remove(next.handler, handler);
							if (next.handler == null)
							{
								if (eventEntry == null)
								{
									this.eventList = next.next;
								}
								else
								{
									eventEntry.next = next.next;
								}
							}
							break;
						}
						eventEntry = next;
						next = next.next;
					}
				}
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00050768 File Offset: 0x0004E968
		protected virtual void RemoveEventHandlers()
		{
			this.eventList = null;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00050771 File Offset: 0x0004E971
		internal void SetDownButtonDirection(bool pointDown)
		{
			this.DownButtonDown = pointDown;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0005077A File Offset: 0x0004E97A
		internal bool ToggleDownButtonDirection()
		{
			this.DownButtonDown = !this.DownButtonDown;
			return this.DownButtonDown;
		}

		// Token: 0x04000A20 RID: 2592
		internal EventHandlerList events;

		// Token: 0x04000A21 RID: 2593
		private const int xOffset = 3;

		// Token: 0x04000A22 RID: 2594
		private const int yOffset = 1;

		// Token: 0x04000A23 RID: 2595
		private const int textPadding = 2;

		// Token: 0x04000A24 RID: 2596
		private const int buttonToText = 4;

		// Token: 0x04000A25 RID: 2597
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04000A26 RID: 2598
		private static readonly Point minimumBounds = new Point(50, 30);

		// Token: 0x04000A27 RID: 2599
		private DataGrid dataGrid;

		// Token: 0x04000A28 RID: 2600
		private bool backButtonVisible;

		// Token: 0x04000A29 RID: 2601
		private bool downButtonVisible;

		// Token: 0x04000A2A RID: 2602
		private SolidBrush backBrush = DataGridCaption.DefaultBackBrush;

		// Token: 0x04000A2B RID: 2603
		private SolidBrush foreBrush = DataGridCaption.DefaultForeBrush;

		// Token: 0x04000A2C RID: 2604
		private Pen textBorderPen = DataGridCaption.DefaultTextBorderPen;

		// Token: 0x04000A2D RID: 2605
		private string text = "";

		// Token: 0x04000A2E RID: 2606
		private bool textBorderVisible;

		// Token: 0x04000A2F RID: 2607
		private Font textFont;

		// Token: 0x04000A30 RID: 2608
		private Font dataGridFont;

		// Token: 0x04000A31 RID: 2609
		private bool backActive;

		// Token: 0x04000A32 RID: 2610
		private bool downActive;

		// Token: 0x04000A33 RID: 2611
		private bool backPressed;

		// Token: 0x04000A34 RID: 2612
		private bool downPressed;

		// Token: 0x04000A35 RID: 2613
		private bool downButtonDown;

		// Token: 0x04000A36 RID: 2614
		private static Bitmap leftButtonBitmap;

		// Token: 0x04000A37 RID: 2615
		private static Bitmap leftButtonBitmap_bidi;

		// Token: 0x04000A38 RID: 2616
		private static Bitmap magnifyingGlassBitmap;

		// Token: 0x04000A39 RID: 2617
		private Rectangle backButtonRect;

		// Token: 0x04000A3A RID: 2618
		private Rectangle downButtonRect;

		// Token: 0x04000A3B RID: 2619
		private Rectangle textRect;

		// Token: 0x04000A3C RID: 2620
		private DataGridCaption.CaptionLocation lastMouseLocation;

		// Token: 0x04000A3D RID: 2621
		private DataGridCaption.EventEntry eventList;

		// Token: 0x04000A3E RID: 2622
		private static readonly object EVENT_BACKWARDCLICKED = new object();

		// Token: 0x04000A3F RID: 2623
		private static readonly object EVENT_DOWNCLICKED = new object();

		// Token: 0x04000A40 RID: 2624
		private static readonly object EVENT_CAPTIONCLICKED = new object();

		// Token: 0x0200064B RID: 1611
		internal enum CaptionLocation
		{
			// Token: 0x040039D4 RID: 14804
			Nowhere,
			// Token: 0x040039D5 RID: 14805
			BackButton,
			// Token: 0x040039D6 RID: 14806
			DownButton,
			// Token: 0x040039D7 RID: 14807
			Text
		}

		// Token: 0x0200064C RID: 1612
		private sealed class EventEntry
		{
			// Token: 0x060064E3 RID: 25827 RVA: 0x00177B80 File Offset: 0x00175D80
			internal EventEntry(DataGridCaption.EventEntry next, object key, Delegate handler)
			{
				this.next = next;
				this.key = key;
				this.handler = handler;
			}

			// Token: 0x040039D8 RID: 14808
			internal DataGridCaption.EventEntry next;

			// Token: 0x040039D9 RID: 14809
			internal object key;

			// Token: 0x040039DA RID: 14810
			internal Delegate handler;
		}
	}
}
