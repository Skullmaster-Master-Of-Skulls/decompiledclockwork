using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020001FF RID: 511
	public class DataGridViewImageCell : DataGridViewCell
	{
		// Token: 0x0600213D RID: 8509 RVA: 0x0009C772 File Offset: 0x0009A972
		public DataGridViewImageCell() : this(false)
		{
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0009C77B File Offset: 0x0009A97B
		public DataGridViewImageCell(bool valueIsIcon)
		{
			if (valueIsIcon)
			{
				this.flags = 1;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0009C78D File Offset: 0x0009A98D
		public override object DefaultNewRowValue
		{
			get
			{
				if (DataGridViewImageCell.defaultTypeImage.IsAssignableFrom(this.ValueType))
				{
					return DataGridViewImageCell.ErrorBitmap;
				}
				if (DataGridViewImageCell.defaultTypeIcon.IsAssignableFrom(this.ValueType))
				{
					return DataGridViewImageCell.ErrorIcon;
				}
				return null;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0009C7C0 File Offset: 0x0009A9C0
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x0009C7ED File Offset: 0x0009A9ED
		[DefaultValue("")]
		public string Description
		{
			get
			{
				object @object = base.Properties.GetObject(DataGridViewImageCell.PropImageCellDescription);
				if (@object != null)
				{
					return (string)@object;
				}
				return string.Empty;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) || base.Properties.ContainsObject(DataGridViewImageCell.PropImageCellDescription))
				{
					base.Properties.SetObject(DataGridViewImageCell.PropImageCellDescription, value);
				}
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00015ECC File Offset: 0x000140CC
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0009C81A File Offset: 0x0009AA1A
		internal static Bitmap ErrorBitmap
		{
			get
			{
				if (DataGridViewImageCell.errorBmp == null)
				{
					DataGridViewImageCell.errorBmp = new Bitmap(typeof(DataGridView), "ImageInError.bmp");
				}
				return DataGridViewImageCell.errorBmp;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0009C841 File Offset: 0x0009AA41
		internal static Icon ErrorIcon
		{
			get
			{
				if (DataGridViewImageCell.errorIco == null)
				{
					DataGridViewImageCell.errorIco = new Icon(typeof(DataGridView), "IconInError.ico");
				}
				return DataGridViewImageCell.errorIco;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x0009C868 File Offset: 0x0009AA68
		public override Type FormattedValueType
		{
			get
			{
				if (this.ValueIsIcon)
				{
					return DataGridViewImageCell.defaultTypeIcon;
				}
				return DataGridViewImageCell.defaultTypeImage;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0009C880 File Offset: 0x0009AA80
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x0009C8A8 File Offset: 0x0009AAA8
		[DefaultValue(DataGridViewImageCellLayout.NotSet)]
		public DataGridViewImageCellLayout ImageLayout
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewImageCell.PropImageCellLayout, out flag);
				if (flag)
				{
					return (DataGridViewImageCellLayout)integer;
				}
				return DataGridViewImageCellLayout.Normal;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewImageCellLayout));
				}
				if (this.ImageLayout != value)
				{
					base.Properties.SetInteger(DataGridViewImageCell.PropImageCellLayout, (int)value);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x17000777 RID: 1911
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x0009C8FB File Offset: 0x0009AAFB
		internal DataGridViewImageCellLayout ImageLayoutInternal
		{
			set
			{
				if (this.ImageLayout != value)
				{
					base.Properties.SetInteger(DataGridViewImageCell.PropImageCellLayout, (int)value);
				}
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x0009C917 File Offset: 0x0009AB17
		// (set) Token: 0x0600214A RID: 8522 RVA: 0x0009C924 File Offset: 0x0009AB24
		[DefaultValue(false)]
		public bool ValueIsIcon
		{
			get
			{
				return (this.flags & 1) > 0;
			}
			set
			{
				if (this.ValueIsIcon != value)
				{
					this.ValueIsIconInternal = value;
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000779 RID: 1913
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x0009C970 File Offset: 0x0009AB70
		internal bool ValueIsIconInternal
		{
			set
			{
				if (this.ValueIsIcon != value)
				{
					if (value)
					{
						this.flags |= 1;
					}
					else
					{
						this.flags = (byte)((int)this.flags & -2);
					}
					if (base.DataGridView != null && base.RowIndex != -1 && base.DataGridView.NewRowIndex == base.RowIndex && !base.DataGridView.VirtualMode && ((value && base.Value == DataGridViewImageCell.ErrorBitmap) || (!value && base.Value == DataGridViewImageCell.ErrorIcon)))
					{
						base.Value = this.DefaultNewRowValue;
					}
				}
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x0009CA0C File Offset: 0x0009AC0C
		// (set) Token: 0x0600214D RID: 8525 RVA: 0x0009CA3E File Offset: 0x0009AC3E
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				if (this.ValueIsIcon)
				{
					return DataGridViewImageCell.defaultTypeIcon;
				}
				return DataGridViewImageCell.defaultTypeImage;
			}
			set
			{
				base.ValueType = value;
				this.ValueIsIcon = (value != null && DataGridViewImageCell.defaultTypeIcon.IsAssignableFrom(value));
			}
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0009CA64 File Offset: 0x0009AC64
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewImageCell dataGridViewImageCell;
			if (type == DataGridViewImageCell.cellType)
			{
				dataGridViewImageCell = new DataGridViewImageCell();
			}
			else
			{
				dataGridViewImageCell = (DataGridViewImageCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewImageCell);
			dataGridViewImageCell.ValueIsIconInternal = this.ValueIsIcon;
			dataGridViewImageCell.Description = this.Description;
			dataGridViewImageCell.ImageLayoutInternal = this.ImageLayout;
			return dataGridViewImageCell;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0009CAC5 File Offset: 0x0009ACC5
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewImageCell.DataGridViewImageCellAccessibleObject(this);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0009CAD0 File Offset: 0x0009ACD0
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates elementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out elementState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, elementState, formattedValue, null, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0009CB44 File Offset: 0x0009AD44
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null || !base.DataGridView.ShowCellErrors || string.IsNullOrEmpty(this.GetErrorText(rowIndex)))
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates elementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out elementState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, elementState, formattedValue, this.GetErrorText(rowIndex), cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0009CBD8 File Offset: 0x0009ADD8
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if ((context & DataGridViewDataErrorContexts.ClipboardContent) != (DataGridViewDataErrorContexts)0)
			{
				return this.Description;
			}
			object formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			if (formattedValue == null && cellStyle.NullValue == null)
			{
				return null;
			}
			if (this.ValueIsIcon)
			{
				Icon icon = formattedValue as Icon;
				if (icon == null)
				{
					icon = DataGridViewImageCell.ErrorIcon;
				}
				return icon;
			}
			Image image = formattedValue as Image;
			if (image == null)
			{
				image = DataGridViewImageCell.ErrorBitmap;
			}
			return image;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0009CC40 File Offset: 0x0009AE40
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Rectangle stdBorderWidths = base.StdBorderWidths;
			int num = stdBorderWidths.Left + stdBorderWidths.Width + cellStyle.Padding.Horizontal;
			int num2 = stdBorderWidths.Top + stdBorderWidths.Height + cellStyle.Padding.Vertical;
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			object formattedValue = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize);
			Image image = formattedValue as Image;
			Icon icon = null;
			if (image == null)
			{
				icon = (formattedValue as Icon);
			}
			Size empty;
			if (freeDimensionFromConstraint == DataGridViewFreeDimension.Height && this.ImageLayout == DataGridViewImageCellLayout.Zoom)
			{
				if (image != null || icon != null)
				{
					if (image != null)
					{
						int num3 = constraintSize.Width - num;
						if (num3 <= 0 || image.Width == 0)
						{
							empty = Size.Empty;
						}
						else
						{
							empty = new Size(0, Math.Min(image.Height, decimal.ToInt32(image.Height * num3 / image.Width)));
						}
					}
					else
					{
						int num4 = constraintSize.Width - num;
						if (num4 <= 0 || icon.Width == 0)
						{
							empty = Size.Empty;
						}
						else
						{
							empty = new Size(0, Math.Min(icon.Height, decimal.ToInt32(icon.Height * num4 / icon.Width)));
						}
					}
				}
				else
				{
					empty = new Size(0, 1);
				}
			}
			else if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width && this.ImageLayout == DataGridViewImageCellLayout.Zoom)
			{
				if (image != null || icon != null)
				{
					if (image != null)
					{
						int num5 = constraintSize.Height - num2;
						if (num5 <= 0 || image.Height == 0)
						{
							empty = Size.Empty;
						}
						else
						{
							empty = new Size(Math.Min(image.Width, decimal.ToInt32(image.Width * num5 / image.Height)), 0);
						}
					}
					else
					{
						int num6 = constraintSize.Height - num2;
						if (num6 <= 0 || icon.Height == 0)
						{
							empty = Size.Empty;
						}
						else
						{
							empty = new Size(Math.Min(icon.Width, decimal.ToInt32(icon.Width * num6 / icon.Height)), 0);
						}
					}
				}
				else
				{
					empty = new Size(1, 0);
				}
			}
			else
			{
				if (image != null)
				{
					empty = new Size(image.Width, image.Height);
				}
				else if (icon != null)
				{
					empty = new Size(icon.Width, icon.Height);
				}
				else
				{
					empty = new Size(1, 1);
				}
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Height)
				{
					empty.Width = 0;
				}
				else if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					empty.Height = 0;
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				empty.Width += num;
				if (base.DataGridView.ShowCellErrors)
				{
					empty.Width = Math.Max(empty.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				empty.Height += num2;
				if (base.DataGridView.ShowCellErrors)
				{
					empty.Height = Math.Max(empty.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return empty;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0009CFC0 File Offset: 0x0009B1C0
		protected override object GetValue(int rowIndex)
		{
			object value = base.GetValue(rowIndex);
			if (value == null)
			{
				DataGridViewImageColumn dataGridViewImageColumn = base.OwningColumn as DataGridViewImageColumn;
				if (dataGridViewImageColumn != null)
				{
					if (DataGridViewImageCell.defaultTypeImage.IsAssignableFrom(this.ValueType))
					{
						Image image = dataGridViewImageColumn.Image;
						if (image != null)
						{
							return image;
						}
					}
					else if (DataGridViewImageCell.defaultTypeIcon.IsAssignableFrom(this.ValueType))
					{
						Icon icon = dataGridViewImageColumn.Icon;
						if (icon != null)
						{
							return icon;
						}
					}
				}
			}
			return value;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0009D024 File Offset: 0x0009B224
		private Rectangle ImgBounds(Rectangle bounds, int imgWidth, int imgHeight, DataGridViewImageCellLayout imageLayout, DataGridViewCellStyle cellStyle)
		{
			Rectangle empty = Rectangle.Empty;
			if (imageLayout > DataGridViewImageCellLayout.Normal)
			{
				if (imageLayout == DataGridViewImageCellLayout.Zoom)
				{
					if (imgWidth * bounds.Height < imgHeight * bounds.Width)
					{
						empty = new Rectangle(bounds.X, bounds.Y, decimal.ToInt32(imgWidth * bounds.Height / imgHeight), bounds.Height);
					}
					else
					{
						empty = new Rectangle(bounds.X, bounds.Y, bounds.Width, decimal.ToInt32(imgHeight * bounds.Width / imgWidth));
					}
				}
			}
			else
			{
				empty = new Rectangle(bounds.X, bounds.Y, imgWidth, imgHeight);
			}
			if (base.DataGridView.RightToLeftInternal)
			{
				DataGridViewContentAlignment alignment = cellStyle.Alignment;
				if (alignment <= DataGridViewContentAlignment.MiddleLeft)
				{
					if (alignment != DataGridViewContentAlignment.TopLeft)
					{
						if (alignment != DataGridViewContentAlignment.TopRight)
						{
							if (alignment == DataGridViewContentAlignment.MiddleLeft)
							{
								empty.X = bounds.Right - empty.Width;
							}
						}
						else
						{
							empty.X = bounds.X;
						}
					}
					else
					{
						empty.X = bounds.Right - empty.Width;
					}
				}
				else if (alignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (alignment != DataGridViewContentAlignment.BottomLeft)
					{
						if (alignment == DataGridViewContentAlignment.BottomRight)
						{
							empty.X = bounds.X;
						}
					}
					else
					{
						empty.X = bounds.Right - empty.Width;
					}
				}
				else
				{
					empty.X = bounds.X;
				}
			}
			else
			{
				DataGridViewContentAlignment alignment2 = cellStyle.Alignment;
				if (alignment2 <= DataGridViewContentAlignment.MiddleLeft)
				{
					if (alignment2 != DataGridViewContentAlignment.TopLeft)
					{
						if (alignment2 != DataGridViewContentAlignment.TopRight)
						{
							if (alignment2 == DataGridViewContentAlignment.MiddleLeft)
							{
								empty.X = bounds.X;
							}
						}
						else
						{
							empty.X = bounds.Right - empty.Width;
						}
					}
					else
					{
						empty.X = bounds.X;
					}
				}
				else if (alignment2 != DataGridViewContentAlignment.MiddleRight)
				{
					if (alignment2 != DataGridViewContentAlignment.BottomLeft)
					{
						if (alignment2 == DataGridViewContentAlignment.BottomRight)
						{
							empty.X = bounds.Right - empty.Width;
						}
					}
					else
					{
						empty.X = bounds.X;
					}
				}
				else
				{
					empty.X = bounds.Right - empty.Width;
				}
			}
			DataGridViewContentAlignment alignment3 = cellStyle.Alignment;
			if (alignment3 == DataGridViewContentAlignment.TopCenter || alignment3 == DataGridViewContentAlignment.MiddleCenter || alignment3 == DataGridViewContentAlignment.BottomCenter)
			{
				empty.X = bounds.X + (bounds.Width - empty.Width) / 2;
			}
			DataGridViewContentAlignment alignment4 = cellStyle.Alignment;
			if (alignment4 > DataGridViewContentAlignment.MiddleCenter)
			{
				if (alignment4 <= DataGridViewContentAlignment.BottomLeft)
				{
					if (alignment4 == DataGridViewContentAlignment.MiddleRight)
					{
						goto IL_2F6;
					}
					if (alignment4 != DataGridViewContentAlignment.BottomLeft)
					{
						return empty;
					}
				}
				else if (alignment4 != DataGridViewContentAlignment.BottomCenter && alignment4 != DataGridViewContentAlignment.BottomRight)
				{
					return empty;
				}
				empty.Y = bounds.Bottom - empty.Height;
				return empty;
			}
			if (alignment4 <= DataGridViewContentAlignment.TopRight)
			{
				if (alignment4 - DataGridViewContentAlignment.TopLeft > 1 && alignment4 != DataGridViewContentAlignment.TopRight)
				{
					return empty;
				}
				empty.Y = bounds.Y;
				return empty;
			}
			else if (alignment4 != DataGridViewContentAlignment.MiddleLeft && alignment4 != DataGridViewContentAlignment.MiddleCenter)
			{
				return empty;
			}
			IL_2F6:
			empty.Y = bounds.Y + (bounds.Height - empty.Height) / 2;
			return empty;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0009D360 File Offset: 0x0009B560
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0009D398 File Offset: 0x0009B598
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			rectangle.Offset(rectangle2.X, rectangle2.Y);
			rectangle.Width -= rectangle2.Right;
			rectangle.Height -= rectangle2.Bottom;
			Rectangle result;
			if (rectangle.Width > 0 && rectangle.Height > 0 && (paint || computeContentBounds))
			{
				Rectangle rectangle3 = rectangle;
				if (cellStyle.Padding != Padding.Empty)
				{
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle3.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
					}
					else
					{
						rectangle3.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
					}
					rectangle3.Width -= cellStyle.Padding.Horizontal;
					rectangle3.Height -= cellStyle.Padding.Vertical;
				}
				bool flag = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
				SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
				if (rectangle3.Width > 0 && rectangle3.Height > 0)
				{
					Image image = formattedValue as Image;
					Icon icon = null;
					if (image == null)
					{
						icon = (formattedValue as Icon);
					}
					if (icon != null || image != null)
					{
						DataGridViewImageCellLayout dataGridViewImageCellLayout = this.ImageLayout;
						if (dataGridViewImageCellLayout == DataGridViewImageCellLayout.NotSet)
						{
							if (base.OwningColumn is DataGridViewImageColumn)
							{
								dataGridViewImageCellLayout = ((DataGridViewImageColumn)base.OwningColumn).ImageLayout;
							}
							else
							{
								dataGridViewImageCellLayout = DataGridViewImageCellLayout.Normal;
							}
						}
						if (dataGridViewImageCellLayout == DataGridViewImageCellLayout.Stretch)
						{
							if (paint)
							{
								if (DataGridViewCell.PaintBackground(paintParts))
								{
									DataGridViewCell.PaintPadding(g, rectangle, cellStyle, cachedBrush, base.DataGridView.RightToLeftInternal);
								}
								if (DataGridViewCell.PaintContentForeground(paintParts))
								{
									if (image != null)
									{
										ImageAttributes imageAttributes = new ImageAttributes();
										imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
										g.DrawImage(image, rectangle3, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
										imageAttributes.Dispose();
									}
									else
									{
										g.DrawIcon(icon, rectangle3);
									}
								}
							}
							result = rectangle3;
						}
						else
						{
							Rectangle rectangle4 = this.ImgBounds(rectangle3, (image == null) ? icon.Width : image.Width, (image == null) ? icon.Height : image.Height, dataGridViewImageCellLayout, cellStyle);
							result = rectangle4;
							if (paint)
							{
								if (DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
								{
									g.FillRectangle(cachedBrush, rectangle);
								}
								if (DataGridViewCell.PaintContentForeground(paintParts))
								{
									Region clip = g.Clip;
									g.SetClip(Rectangle.Intersect(Rectangle.Intersect(rectangle4, rectangle3), Rectangle.Truncate(g.VisibleClipBounds)));
									if (image != null)
									{
										g.DrawImage(image, rectangle4);
									}
									else
									{
										g.DrawIconUnstretched(icon, rectangle4);
									}
									g.Clip = clip;
								}
							}
						}
					}
					else
					{
						if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
						{
							g.FillRectangle(cachedBrush, rectangle);
						}
						result = Rectangle.Empty;
					}
				}
				else
				{
					if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
					{
						g.FillRectangle(cachedBrush, rectangle);
					}
					result = Rectangle.Empty;
				}
				Point currentCellAddress = base.DataGridView.CurrentCellAddress;
				if (paint && DataGridViewCell.PaintFocus(paintParts) && currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex && base.DataGridView.ShowFocusCues && base.DataGridView.Focused)
				{
					ControlPaint.DrawFocusRectangle(g, rectangle, Color.Empty, cachedBrush.Color);
				}
				if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
				{
					base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, rectangle, errorText);
				}
			}
			else if (computeErrorIconBounds)
			{
				if (!string.IsNullOrEmpty(errorText))
				{
					result = base.ComputeErrorIconBounds(rectangle);
				}
				else
				{
					result = Rectangle.Empty;
				}
			}
			else
			{
				result = Rectangle.Empty;
			}
			return result;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0009D7D0 File Offset: 0x0009B9D0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewImageCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x04000DEA RID: 3562
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04000DEB RID: 3563
		private static readonly int PropImageCellDescription = PropertyStore.CreateKey();

		// Token: 0x04000DEC RID: 3564
		private static readonly int PropImageCellLayout = PropertyStore.CreateKey();

		// Token: 0x04000DED RID: 3565
		private static Type defaultTypeImage = typeof(Image);

		// Token: 0x04000DEE RID: 3566
		private static Type defaultTypeIcon = typeof(Icon);

		// Token: 0x04000DEF RID: 3567
		private static Type cellType = typeof(DataGridViewImageCell);

		// Token: 0x04000DF0 RID: 3568
		private static Bitmap errorBmp = null;

		// Token: 0x04000DF1 RID: 3569
		private static Icon errorIco = null;

		// Token: 0x04000DF2 RID: 3570
		private const byte DATAGRIDVIEWIMAGECELL_valueIsIcon = 1;

		// Token: 0x04000DF3 RID: 3571
		private byte flags;

		// Token: 0x02000673 RID: 1651
		protected class DataGridViewImageCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			// Token: 0x0600667E RID: 26238 RVA: 0x0017C895 File Offset: 0x0017AA95
			public DataGridViewImageCellAccessibleObject(DataGridViewCell owner) : base(owner)
			{
			}

			// Token: 0x17001647 RID: 5703
			// (get) Token: 0x0600667F RID: 26239 RVA: 0x0017F055 File Offset: 0x0017D255
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17001648 RID: 5704
			// (get) Token: 0x06006680 RID: 26240 RVA: 0x0017F05C File Offset: 0x0017D25C
			public override string Description
			{
				get
				{
					DataGridViewImageCell dataGridViewImageCell = base.Owner as DataGridViewImageCell;
					if (dataGridViewImageCell != null)
					{
						return dataGridViewImageCell.Description;
					}
					return null;
				}
			}

			// Token: 0x17001649 RID: 5705
			// (get) Token: 0x06006681 RID: 26241 RVA: 0x0017F080 File Offset: 0x0017D280
			// (set) Token: 0x06006682 RID: 26242 RVA: 0x000072B6 File Offset: 0x000054B6
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return base.Value;
				}
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				set
				{
				}
			}

			// Token: 0x06006683 RID: 26243 RVA: 0x0017F088 File Offset: 0x0017D288
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (AccessibilityImprovements.Level3)
				{
					if (base.IsOwnerCellDestroyed())
					{
						return;
					}
					DataGridViewImageCell dataGridViewImageCell = (DataGridViewImageCell)base.Owner;
					DataGridView dataGridView = dataGridViewImageCell.DataGridView;
					if (dataGridView != null && dataGridViewImageCell.RowIndex != -1 && dataGridViewImageCell.OwningColumn != null && dataGridViewImageCell.OwningRow != null)
					{
						dataGridView.OnCellContentClickInternal(new DataGridViewCellEventArgs(dataGridViewImageCell.ColumnIndex, dataGridViewImageCell.RowIndex));
					}
				}
			}

			// Token: 0x06006684 RID: 26244 RVA: 0x00011A20 File Offset: 0x0000FC20
			public override int GetChildCount()
			{
				return 0;
			}

			// Token: 0x06006685 RID: 26245 RVA: 0x0017F0EB File Offset: 0x0017D2EB
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level2 || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006686 RID: 26246 RVA: 0x0017F0FC File Offset: 0x0017D2FC
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50006;
				}
				if (AccessibilityImprovements.Level3 && propertyID == 30031)
				{
					return true;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006687 RID: 26247 RVA: 0x0017F12E File Offset: 0x0017D32E
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerCellDestroyed() && ((AccessibilityImprovements.Level3 && patternId == 10000) || base.IsPatternSupported(patternId));
			}
		}
	}
}
