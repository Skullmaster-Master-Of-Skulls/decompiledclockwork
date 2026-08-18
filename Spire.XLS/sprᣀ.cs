using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004C2 RID: 1218
internal class sprᣀ
{
	// Token: 0x06004AF8 RID: 19192 RVA: 0x002D7C2C File Offset: 0x002D6C2C
	public Image ᜀ(XlsWorksheet A_0, int A_1, int A_2, int A_3, int A_4)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ(A_0, A_1, A_2, A_3, A_4, ImageType.Bitmap, null);
	}

	// Token: 0x06004AF9 RID: 19193 RVA: 0x002D7C6C File Offset: 0x002D6C6C
	public Image ᜀ(XlsWorksheet A_0, int A_1, int A_2, int A_3, int A_4, EmfType A_5, Stream A_6)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ(A_0, A_1, A_2, A_3, A_4, ImageType.Metafile, A_6, A_5);
	}

	// Token: 0x06004AFA RID: 19194 RVA: 0x002D7CBC File Offset: 0x002D6CBC
	public Image ᜀ(XlsWorksheet A_0, int A_1, int A_2, int A_3, int A_4, ImageType A_5, Stream A_6)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, EmfType.EmfOnly);
	}

	// Token: 0x06004AFB RID: 19195 RVA: 0x002D7D0C File Offset: 0x002D6D0C
	public Image ᜀ(XlsWorksheet A_0, int A_1, int A_2, int A_3, int A_4, ImageType A_5, Stream A_6, EmfType A_7)
	{
		switch (0)
		{
		default:
		{
			Image image;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return image;
				default:
				{
					if (false)
					{
					}
					sprᱥ sprᱥ = new sprᱥ(new sprᱥ.ᜀ(A_0.GetRowHeightPixels));
					sprᱥ sprᱥ2 = new sprᱥ(new sprᱥ.ᜀ(A_0.GetColumnWidthPixels));
					int num = sprᱥ.ᜀ(A_1, A_3);
					int num2 = sprᱥ2.ᜀ(A_2, A_4);
					image = this.ᜀ(num2, num, A_5, A_6, A_7);
					this.ᜀ(image, A_0, A_1, A_2, A_3, A_4, sprᱥ, sprᱥ2, num2, num);
					int num3 = 4;
					for (;;)
					{
						ImageFormat imageFormat;
						switch (num3)
						{
						case 0:
							return image;
						case 1:
							num3 = 5;
							continue;
						case 2:
							if (!image.RawFormat.Equals(ImageFormat.MemoryBmp))
							{
								num3 = 8;
								continue;
							}
							num3 = 7;
							continue;
						case 3:
							if (true)
							{
							}
							num3 = 2;
							continue;
						case 4:
							if (A_5 == ImageType.Bitmap)
							{
								num3 = 1;
								continue;
							}
							return image;
						case 5:
							if (A_6 != null)
							{
								num3 = 3;
								continue;
							}
							return image;
						case 6:
							imageFormat = image.RawFormat;
							goto IL_123;
						case 7:
							imageFormat = ImageFormat.Bmp;
							goto IL_123;
						case 8:
							num3 = 6;
							continue;
						}
						break;
						IL_123:
						ImageFormat format = imageFormat;
						image.Save(A_6, format);
						num3 = 0;
					}
					break;
				}
				}
			}
			return image;
		}
		}
	}

	// Token: 0x06004AFC RID: 19196 RVA: 0x002D7E94 File Offset: 0x002D6E94
	private void ᜀ(Image A_0, XlsWorksheet A_1, int A_2, int A_3, int A_4, int A_5, sprᱥ A_6, sprᱥ A_7, int A_8, int A_9)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			Graphics graphics = Graphics.FromImage(A_0);
			try
			{
				if (true)
				{
				}
				graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, A_8, A_9));
				this.ᜀ(A_1, A_2, A_3, A_4, A_5, graphics, A_6, A_7, A_8, A_9);
				this.ᜀ(A_1, A_2, A_3, A_4, A_5, graphics, A_6, A_7, new sprᣀ.ᜁ(this.ᜀ));
				this.ᜀ(A_1, A_2, A_3, A_4, A_5, graphics, A_6, A_7, new sprᣀ.ᜀ(this.ᜀ));
				this.ᜀ(A_1, A_2, A_3, A_4, A_5, graphics, A_6, A_7);
				this.ᜀ(A_1, A_2, A_3, A_4, A_5, graphics, A_6, A_7, new sprᣀ.ᜀ(this.ᜁ));
				this.ᜀ(A_1, graphics, A_2, A_3, A_4, A_5, A_6, A_7);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						((IDisposable)graphics).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_120;
					}
					if (graphics == null)
					{
						break;
					}
					num = 1;
				}
				IL_120:;
			}
			break;
		}
		}
	}

	// Token: 0x06004AFD RID: 19197 RVA: 0x002D7FD4 File Offset: 0x002D6FD4
	private void ᜀ(XlsWorksheet A_0, int A_1, int A_2, int A_3, int A_4, Graphics A_5, sprᱥ A_6, sprᱥ A_7, sprᣀ.ᜀ A_8)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_0E;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_72;
					case 2:
						goto IL_72;
					case 3:
					{
						List<spr\u25A6.ᜀ> list = new List<spr\u25A6.ᜀ>();
						spr\u1FBC spr_u1FBC = A_0.MergeCells;
						spr_u1FBC.ᜀ(A_0[A_1, A_2, A_3, A_4], list);
						int num2 = 0;
						int count = list.Count;
						num = 2;
						continue;
					}
					case 4:
						return;
					case 5:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						List<spr\u25A6.ᜀ> list;
						A_8(A_0, list[num2], A_1, A_2, A_5, A_6, A_7);
						num2++;
						num = 1;
						continue;
					}
					}
					if (A_0.HasMergedCells)
					{
						num = 3;
						continue;
					}
					return;
					IL_72:
					num = 5;
				}
			}
			return;
		}
	}

	// Token: 0x06004AFE RID: 19198 RVA: 0x002D80D4 File Offset: 0x002D70D4
	private void ᜁ(XlsWorksheet A_0, spr\u25A6.ᜀ A_1, int A_2, int A_3, Graphics A_4, sprᱥ A_5, sprᱥ A_6)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				for (;;)
				{
					IL_51:
					spr\u1FBC spr_u1FBC = A_0.MergeCells;
					spr\u192F a_ = spr_u1FBC.ᜀ(A_1);
					Rectangle rectangle = this.ᜀ(A_0, A_1, A_2, A_3, A_5, A_6);
					IXLSRange a_2 = A_0[A_1.ᜂ() + 1, A_1.ᜅ() + 1];
					int num = 4;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_0E;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								num = 1;
								continue;
							case 1:
								if (rectangle.Width > 0)
								{
									num = 3;
									continue;
								}
								return;
							case 2:
								return;
							case 3:
								if (true)
								{
								}
								this.ᜀ(a_, a_2, rectangle, rectangle, A_4);
								num = 2;
								continue;
							case 4:
								if (rectangle.Height > 0)
								{
									num = 0;
									continue;
								}
								return;
							}
							goto IL_51;
						}
					}
				}
			}
			return;
		}
	}

	// Token: 0x06004AFF RID: 19199 RVA: 0x002D81D0 File Offset: 0x002D71D0
	private Rectangle ᜀ(XlsWorksheet A_0, spr\u25A6.ᜀ A_1, int A_2, int A_3, sprᱥ A_4, sprᱥ A_5)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		int num = A_4.ᜁ(A_1.ᜂ());
		int num2 = A_5.ᜁ(A_1.ᜅ());
		num -= A_4.ᜁ(A_2 - 1);
		num2 -= A_5.ᜁ(A_3 - 1);
		int height = A_4.ᜀ(A_1.ᜂ() + 1, A_1.ᜇ() + 1);
		int width = A_5.ᜀ(A_1.ᜅ() + 1, A_1.ᜃ() + 1);
		IXLSRange ixlsrange = A_0[A_1.ᜂ() + 1, A_1.ᜅ() + 1];
		return new Rectangle(num2, num, width, height);
	}

	// Token: 0x06004B00 RID: 19200 RVA: 0x002D8294 File Offset: 0x002D7294
	private void ᜀ(XlsWorksheet A_0, spr\u25A6.ᜀ A_1, int A_2, int A_3, Graphics A_4, sprᱥ A_5, sprᱥ A_6)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				for (;;)
				{
					IL_51:
					spr\u1FBC spr_u1FBC = A_0.MergeCells;
					spr\u192F a_ = spr_u1FBC.ᜀ(A_1);
					int num = A_5.ᜁ(A_1.ᜂ());
					int num2 = A_6.ᜁ(A_1.ᜅ());
					num -= A_5.ᜁ(A_2 - 1);
					num2 -= A_6.ᜁ(A_3 - 1);
					int num3 = A_5.ᜀ(A_1.ᜂ() + 1, A_1.ᜇ() + 1);
					int num4 = A_6.ᜀ(A_1.ᜅ() + 1, A_1.ᜃ() + 1);
					IXLSRange ixlsrange = A_0[A_1.ᜂ() + 1, A_1.ᜅ() + 1];
					Rectangle a_2 = new Rectangle(num2, num, num4, num3);
					int num5 = 3;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_0E;
						default:
							if (false)
							{
							}
							switch (num5)
							{
							case 0:
								this.ᜀ(a_, a_2, A_4);
								num5 = 2;
								continue;
							case 1:
								if (true)
								{
								}
								if (num4 > 0)
								{
									num5 = 0;
									continue;
								}
								return;
							case 2:
								return;
							case 3:
								if (num3 > 0)
								{
									num5 = 4;
									continue;
								}
								return;
							case 4:
								num5 = 1;
								continue;
							}
							goto IL_51;
						}
					}
				}
			}
			return;
		}
	}

	// Token: 0x06004B01 RID: 19201 RVA: 0x002D83F0 File Offset: 0x002D73F0
	private void ᜀ(XlsWorksheet A_0, int A_1, int A_2, int A_3, int A_4, Graphics A_5, sprᱥ A_6, sprᱥ A_7, sprᣀ.ᜁ A_8)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u24F1 spr_u24F = new spr\u24F1(A_0.AppImplementation, A_0);
				int num = A_1;
				int num2 = 7;
				for (;;)
				{
					if (true)
					{
					}
					int num3;
					int y;
					int num4;
					switch (num2)
					{
					case 0:
						if (num3 > 0)
						{
							num2 = 9;
							continue;
						}
						goto IL_64;
					case 1:
						if (num > A_3)
						{
							num2 = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_161;
						default:
							if (false)
							{
							}
							y = A_6.ᜀ(A_1, num - 1);
							num3 = A_6.ᜀ(num);
							num2 = 0;
							continue;
						}
						break;
					case 2:
						goto IL_64;
					case 3:
						goto IL_125;
					case 4:
						goto IL_155;
					case 5:
						return;
					case 6:
						goto IL_161;
					case 7:
						goto IL_125;
					case 8:
						goto IL_155;
					case 9:
						num4 = A_2;
						num2 = 4;
						continue;
					}
					break;
					IL_64:
					num++;
					num2 = 3;
					continue;
					IL_161:
					if (num4 > A_4)
					{
						num2 = 2;
						continue;
					}
					int x = A_7.ᜀ(A_2, num4 - 1);
					spr_u24F.ᜀ(num, num4);
					int width = A_7.ᜀ(num4);
					Rectangle a_ = new Rectangle(x, y, width, num3);
					A_8(spr_u24F, a_, A_5);
					num4++;
					num2 = 8;
					continue;
					IL_125:
					num2 = 1;
					continue;
					IL_155:
					num2 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06004B02 RID: 19202 RVA: 0x002D8578 File Offset: 0x002D7578
	private void ᜀ(XlsWorksheet A_0, int A_1, int A_2, int A_3, int A_4, Graphics A_5, sprᱥ A_6, sprᱥ A_7)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u24F1 spr_u24F = new spr\u24F1(A_0.AppImplementation, A_0);
				spr\u24F1 a_ = new spr\u24F1(A_0.AppImplementation, A_0);
				int maxColumnCount = spr_u24F.Workbook.MaxColumnCount;
				int num = A_1;
				int num2 = 11;
				for (;;)
				{
					int num4;
					switch (num2)
					{
					case 0:
						goto IL_107;
					case 1:
					{
						int num3;
						if (num3 > 0)
						{
							num2 = 3;
							continue;
						}
						goto IL_107;
					}
					case 2:
						if (!spr_u24F.HasMerged)
						{
							num2 = 10;
							continue;
						}
						goto IL_152;
					case 3:
						num4 = A_2;
						if (true)
						{
						}
						num2 = 4;
						continue;
					case 4:
						goto IL_94;
					case 5:
						goto IL_152;
					case 6:
					{
						if (num > A_3)
						{
							num2 = 13;
							continue;
						}
						int y = A_6.ᜀ(A_1, num - 1);
						int num3 = A_6.ᜀ(num);
						num2 = 1;
						continue;
					}
					case 7:
						goto IL_94;
					case 8:
						goto IL_169;
					case 9:
					{
						int num3;
						int y;
						int x;
						int num5;
						Rectangle a_2 = new Rectangle(x, y, num5, num3);
						Rectangle a_3 = this.ᜀ(spr_u24F, a_2, A_7, A_2, a_);
						this.ᜀ(spr_u24F, a_2, a_3, A_5);
						num2 = 5;
						continue;
					}
					case 10:
					{
						int num5 = A_7.ᜀ(num4);
						num2 = 14;
						continue;
					}
					case 11:
						goto IL_169;
					case 12:
					{
						if (num4 > A_4)
						{
							num2 = 0;
							continue;
						}
						int x = A_7.ᜀ(A_2, num4 - 1);
						spr_u24F.ᜀ(num, num4);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					}
					case 13:
						return;
					case 14:
					{
						int num5;
						if (num5 > 0)
						{
							num2 = 9;
							continue;
						}
						goto IL_152;
					}
					}
					break;
					IL_94:
					num2 = 12;
					continue;
					IL_107:
					num++;
					num2 = 8;
					continue;
					IL_152:
					num4++;
					num2 = 7;
					continue;
					IL_169:
					num2 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06004B03 RID: 19203 RVA: 0x002D879C File Offset: 0x002D779C
	private Rectangle ᜀ(IXLSRange A_0, Rectangle A_1, sprᱥ A_2, int A_3, spr\u24F1 A_4)
	{
		switch (0)
		{
		default:
		{
			Rectangle result;
			for (;;)
			{
				result = A_1;
				int num = 18;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int num5;
					int row;
					int maxColumnCount;
					HorizontalAlignType horizontalAlignment;
					switch (num)
					{
					case 0:
						if (!A_0.HasString)
						{
							num = 15;
							continue;
						}
						goto IL_21B;
					case 1:
						if (true)
						{
						}
						if (A_0.FormulaStringValue == null)
						{
							return result;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30B;
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						break;
					case 2:
						if (num2 <= num3)
						{
							num = 23;
							continue;
						}
						num3 += A_2.ᜀ(A_4.Column);
						num4 += num5;
						A_4.ᜀ(row, num4 + num5);
						num = 4;
						continue;
					case 3:
						num = 2;
						continue;
					case 4:
						goto IL_181;
					case 5:
						goto IL_181;
					case 6:
						if (num4 > 0)
						{
							num = 3;
							continue;
						}
						goto IL_1A7;
					case 7:
						if (A_4.IsBlank)
						{
							num = 13;
							continue;
						}
						goto IL_1A7;
					case 8:
						if (!A_0.IsWrapText)
						{
							num = 21;
							continue;
						}
						return result;
					case 9:
						num = 8;
						continue;
					case 10:
						num5 = -1;
						num = 17;
						continue;
					case 11:
						goto IL_30B;
					case 12:
						goto IL_21B;
					case 13:
						num = 16;
						continue;
					case 14:
						num = 6;
						continue;
					case 15:
						num = 1;
						continue;
					case 16:
						if (num4 < maxColumnCount)
						{
							num = 14;
							continue;
						}
						goto IL_1A7;
					case 17:
						goto IL_C3;
					case 18:
						if (!A_0.IsBlank)
						{
							num = 9;
							continue;
						}
						return result;
					case 19:
						if (horizontalAlignment == HorizontalAlignType.Right)
						{
							num = 10;
							continue;
						}
						goto IL_C3;
					case 20:
						if (num2 > num3)
						{
							num = 11;
							continue;
						}
						return result;
					case 21:
						num = 0;
						continue;
					case 22:
						return result;
					case 23:
						goto IL_1A7;
					}
					break;
					IL_C3:
					A_4.ᜀ(row, num4 + num5);
					num = 5;
					continue;
					IL_181:
					num = 7;
					continue;
					IL_1A7:
					int num6 = Math.Min(num4, A_0.Column);
					int a_ = Math.Max(num4, A_0.Column);
					int x = A_2.ᜀ(A_3, num6 - 1);
					int width = A_2.ᜀ(num6, a_);
					result = new Rectangle(x, A_1.Y, width, A_1.Height);
					num = 22;
					continue;
					IL_21B:
					row = A_0.Row;
					int column = A_0.Column;
					num4 = column;
					XlsWorksheet xlsWorksheet = A_0.Worksheet as XlsWorksheet;
					maxColumnCount = xlsWorksheet.ParentWorkbook.MaxColumnCount;
					SizeF sizeF = xlsWorksheet.ᜀ(A_0, false, false);
					num3 = A_2.ᜀ(column);
					num2 = (int)sizeF.Width;
					num = 20;
					continue;
					IL_30B:
					IStyle style = A_0.Style;
					horizontalAlignment = style.HorizontalAlignment;
					num5 = 1;
					num = 19;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06004B04 RID: 19204 RVA: 0x002D8AF0 File Offset: 0x002D7AF0
	private void ᜀ(XlsWorksheet A_0, int A_1, int A_2, int A_3, int A_4, Graphics A_5, sprᱥ A_6, sprᱥ A_7, int A_8, int A_9)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				Pen pen;
				int num4;
				int num5;
				Pen pen2;
				switch (num)
				{
				case 0:
					num = 11;
					continue;
				case 1:
					goto IL_153;
				case 2:
				{
					A_5.DrawLine(pen, 0, 0, 0, A_9);
					int num2 = A_2;
					int num3 = 0;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				case 3:
					if (num4 > A_3)
					{
						num = 2;
						continue;
					}
					num5 += A_6.ᜀ(num4);
					A_5.DrawLine(pen, 0, num5, A_8, num5);
					num4++;
					num = 5;
					continue;
				case 4:
					goto IL_B8;
				case 5:
					goto IL_B8;
				case 6:
					pen2 = new Pen(A_0.Workbook.GetPaletteColor(A_0.GridLineColor));
					goto IL_20B;
				case 7:
				{
					int num2;
					if (num2 > A_4)
					{
						num = 0;
						continue;
					}
					int num3;
					num3 += A_7.ᜀ(num2);
					A_5.DrawLine(pen, num3, 0, num3, A_9);
					num2++;
					num = 12;
					continue;
				}
				case 9:
					num = 10;
					continue;
				case 10:
					if (!A_0.DefaultGridlineColor)
					{
						num = 15;
						continue;
					}
					num = 16;
					continue;
				case 11:
					if (!A_0.DefaultGridlineColor)
					{
						num = 13;
						continue;
					}
					return;
				case 12:
					goto IL_153;
				case 13:
					pen.Dispose();
					num = 14;
					continue;
				case 14:
					return;
				case 15:
					if (true)
					{
					}
					num = 6;
					continue;
				case 16:
					pen2 = Pens.LightGray;
					goto IL_20B;
				}
				if (A_0.GridLinesVisible)
				{
					num = 9;
					continue;
				}
				break;
				IL_B8:
				num = 3;
				continue;
				IL_153:
				num = 7;
				continue;
				IL_20B:
				pen = pen2;
				A_5.DrawLine(pen, 0, 0, A_8, 0);
				num4 = A_1;
				num5 = 0;
				num = 4;
			}
			return;
		}
		}
	}

	// Token: 0x06004B05 RID: 19205 RVA: 0x002D8D2C File Offset: 0x002D7D2C
	private void ᜀ(XlsWorksheet A_0, Graphics A_1, int A_2, int A_3, int A_4, int A_5, sprᱥ A_6, sprᱥ A_7)
	{
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				int num4;
				switch (num)
				{
				case 0:
				{
					IPictureShape pictureShape;
					int num2;
					if (pictureShape.Left + pictureShape.Width >= num2)
					{
						num = 12;
						continue;
					}
					goto IL_1C3;
				}
				case 1:
				{
					IPictureShape pictureShape;
					int num3;
					if (pictureShape.Top <= num3)
					{
						num = 8;
						continue;
					}
					goto IL_1C3;
				}
				case 2:
				{
					int count;
					if (num4 >= count)
					{
						num = 10;
						continue;
					}
					IPictures pictures;
					IPictureShape pictureShape = pictures[num4];
					if (true)
					{
					}
					num = 1;
					continue;
				}
				case 3:
					goto IL_14E;
				case 4:
				{
					IPictureShape pictureShape;
					int num5;
					if (pictureShape.Left <= num5)
					{
						num = 9;
						continue;
					}
					goto IL_1C3;
				}
				case 5:
					goto IL_14E;
				case 6:
					goto IL_1C3;
				case 7:
					num = 4;
					continue;
				case 8:
					num = 14;
					continue;
				case 9:
					num = 0;
					continue;
				case 10:
					return;
				case 11:
				{
					IPictures pictures = A_0.Pictures;
					int num2 = A_7.ᜁ(A_3 - 1);
					int num6 = A_6.ᜁ(A_2 - 1);
					int num5 = A_7.ᜁ(A_5);
					int num3 = A_6.ᜁ(A_4);
					num4 = 0;
					int count = pictures.Count;
					num = 3;
					continue;
				}
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_229;
					default:
					{
						if (false)
						{
						}
						IPictureShape pictureShape;
						Rectangle rect = new Rectangle(pictureShape.Left, pictureShape.Top, pictureShape.Width, pictureShape.Height);
						int num2;
						int num6;
						rect.Offset(-num2, -num6);
						A_1.DrawImage(pictureShape.Picture, rect);
						num = 6;
						continue;
					}
					}
					break;
				case 14:
				{
					IPictureShape pictureShape;
					int num6;
					if (pictureShape.Top + pictureShape.Height >= num6)
					{
						goto IL_229;
					}
					goto IL_1C3;
				}
				}
				if (A_0.HasPictures)
				{
					num = 11;
					continue;
				}
				break;
				IL_14E:
				num = 2;
				continue;
				IL_1C3:
				num4++;
				num = 5;
				continue;
				IL_229:
				num = 7;
			}
			return;
		}
		}
	}

	// Token: 0x06004B06 RID: 19206 RVA: 0x002D8F70 File Offset: 0x002D7F70
	private void ᜀ(spr\u24F1 A_0, Rectangle A_1, Rectangle A_2, Graphics A_3)
	{
		int a_ = 15;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_3 == null)
				{
					num = 3;
					continue;
				}
				goto IL_98;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				goto IL_3C;
			case 3:
				goto IL_82;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_98;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("♄≆╈❊", a_));
		IL_82:
		throw new ArgumentNullException(RecordTableEnumerator.b("≄㕆⡈㭊╌♎㉐⁒", a_));
		IL_98:
		int extendedFormatIndex = (int)A_0.ExtendedFormatIndex;
		spr\u192F a_2 = A_0.Workbook.InnerExtFormats.ᜁ(extendedFormatIndex);
		this.ᜀ(a_2, A_0, A_1, A_2, A_3);
	}

	// Token: 0x06004B07 RID: 19207 RVA: 0x002D9044 File Offset: 0x002D8044
	private void ᜀ(spr\u192F A_0, IXLSRange A_1, Rectangle A_2, Rectangle A_3, Graphics A_4)
	{
		switch (0)
		{
		default:
		{
			Brush brush;
			for (;;)
			{
				A_0 = this.ᜀ.ᜀ(A_1, A_0);
				IFont font = A_0.ᜀ();
				Color color = this.ᜀ(font.Color);
				brush = new SolidBrush(color);
				int num = 6;
				for (;;)
				{
					string text;
					Font font2;
					StringFormat stringFormat;
					StringFormatFlags stringFormatFlags;
					switch (num)
					{
					case 0:
						if (A_0.\u171B() != 255)
						{
							num = 1;
							continue;
						}
						goto IL_15A;
					case 1:
						num = 7;
						continue;
					case 2:
						goto IL_183;
					case 3:
						goto IL_1A4;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15A;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 5:
						this.ᜀ(A_3, A_0, A_1, text, A_4, font2, brush, stringFormat);
						num = 3;
						continue;
					case 6:
						if (!A_0.\u1733())
						{
							num = 4;
							continue;
						}
						num = 9;
						continue;
					case 7:
						if (A_0.\u171B() != 0)
						{
							num = 5;
							continue;
						}
						goto IL_15A;
					case 8:
						stringFormatFlags = StringFormatFlags.NoWrap;
						goto IL_C3;
					case 9:
						stringFormatFlags = (StringFormatFlags)0;
						goto IL_C3;
					}
					break;
					IL_C3:
					StringFormatFlags options = stringFormatFlags;
					stringFormat = new StringFormat(options);
					stringFormat.Alignment = this.ᜀ(A_0, A_1);
					stringFormat.LineAlignment = this.ᜀ(A_0);
					stringFormat.Trimming = StringTrimming.None;
					text = A_1.NumberText;
					text = this.ᜀ(text, A_0.\u171B());
					font2 = font.GenerateNativeFont();
					num = 0;
					continue;
					IL_15A:
					A_4.DrawString(text, font2, brush, A_3, stringFormat);
					if (true)
					{
					}
					num = 2;
				}
			}
			IL_183:
			IL_1A4:
			brush.Dispose();
			this.ᜀ(A_0.ᜪ(), A_2, A_4, A_1);
			return;
		}
		}
	}

	// Token: 0x06004B08 RID: 19208 RVA: 0x002D9218 File Offset: 0x002D8218
	private void ᜀ(Rectangle A_0, spr\u192F A_1, IXLSRange A_2, string A_3, Graphics A_4, Font A_5, Brush A_6, StringFormat A_7)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		GraphicsState gstate = A_4.Save();
		int num = A_0.X;
		int num2 = A_0.Y;
		Matrix matrix = new Matrix();
		int num3 = this.ᜀ(A_1.\u171B());
		double num4 = (double)num3 * 3.141592653589793 / 180.0;
		int num5 = this.ᜀ(A_1.\u171B());
		XlsWorksheet xlsWorksheet = A_2.Worksheet as XlsWorksheet;
		SizeF a_ = xlsWorksheet.ᜀ(A_2, false, false);
		float height = a_.Height;
		double num6 = (double)A_0.X;
		double num7 = (double)A_0.Y;
		matrix.RotateAt((float)num5, new PointF((float)num6, (float)num7));
		PointF[] array = new PointF[]
		{
			new PointF((float)num6, (float)num7)
		};
		matrix.TransformPoints(array);
		num = (int)Math.Round((double)array[0].X);
		num2 = (int)Math.Round((double)array[0].Y);
		array = this.ᜀ(a_, A_1.\u171B());
		PointF pointF = this.ᜀ(array, A_0, A_7.Alignment, A_7.LineAlignment);
		matrix.Translate(pointF.X, pointF.Y, MatrixOrder.Append);
		A_4.Transform = matrix;
		A_4.DrawString(A_3, A_5, A_6, (float)num, (float)num2, A_7);
		A_4.Restore(gstate);
	}

	// Token: 0x06004B09 RID: 19209 RVA: 0x002D93A8 File Offset: 0x002D83A8
	private PointF[] ᜀ(SizeF A_0, int A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Matrix matrix = new Matrix();
		PointF[] array = new PointF[]
		{
			new PointF(0f, 0f),
			new PointF(0f, A_0.Height),
			new PointF(A_0.Width, A_0.Height),
			new PointF(A_0.Width, 0f)
		};
		matrix.Rotate((float)A_1);
		matrix.TransformPoints(array);
		return array;
	}

	// Token: 0x06004B0A RID: 19210 RVA: 0x002D9478 File Offset: 0x002D8478
	private PointF ᜀ(PointF[] A_0, Rectangle A_1, StringAlignment A_2, StringAlignment A_3)
	{
		switch (0)
		{
		default:
		{
			float x;
			float y;
			for (;;)
			{
				x = 0f;
				y = 0f;
				float num = float.MaxValue;
				float num2 = float.MaxValue;
				float num3 = float.MinValue;
				float num4 = float.MinValue;
				int num5 = 0;
				int num6 = A_0.Length;
				int num7 = 17;
				for (;;)
				{
					switch (num7)
					{
					case 0:
						goto IL_2F1;
					case 1:
						goto IL_1B4;
					case 2:
					{
						PointF pointF;
						num2 = pointF.Y;
						num7 = 24;
						continue;
					}
					case 3:
						switch (A_2)
						{
						case StringAlignment.Near:
							x = -num;
							num7 = 6;
							continue;
						case StringAlignment.Center:
							x = ((float)A_1.Width - num3) / 2f;
							num7 = 16;
							continue;
						case StringAlignment.Far:
							x = (float)A_1.Width - num3;
							num7 = 0;
							continue;
						default:
							num7 = 15;
							continue;
						}
						break;
					case 4:
					{
						if (num5 >= num6)
						{
							num7 = 27;
							continue;
						}
						PointF pointF = A_0[num5];
						num7 = 9;
						continue;
					}
					case 5:
						goto IL_1DA;
					case 6:
						goto IL_334;
					case 7:
					{
						PointF pointF;
						num4 = pointF.Y;
						num7 = 11;
						continue;
					}
					case 8:
					{
						PointF pointF;
						if (num2 > pointF.Y)
						{
							num7 = 2;
							continue;
						}
						goto IL_336;
					}
					case 9:
					{
						PointF pointF;
						if (num > pointF.X)
						{
							num7 = 25;
							continue;
						}
						goto IL_149;
					}
					case 10:
						switch (A_3)
						{
						case StringAlignment.Near:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_334;
							default:
								if (false)
								{
								}
								y = -num2;
								num7 = 12;
								continue;
							}
							break;
						case StringAlignment.Center:
							y = ((float)A_1.Height - num4) / 2f;
							num7 = 20;
							continue;
						case StringAlignment.Far:
							x = (float)A_1.Height - num4;
							num7 = 22;
							continue;
						default:
							num7 = 18;
							continue;
						}
						break;
					case 11:
						goto IL_2DA;
					case 12:
						goto IL_22F;
					case 13:
						goto IL_2F1;
					case 14:
					{
						PointF pointF;
						num3 = pointF.X;
						num7 = 5;
						continue;
					}
					case 15:
						num7 = 13;
						continue;
					case 16:
						goto IL_2F1;
					case 17:
						goto IL_1B4;
					case 18:
						num7 = 19;
						continue;
					case 19:
						goto IL_144;
					case 20:
						goto IL_1AF;
					case 21:
					{
						PointF pointF;
						if (num3 < pointF.X)
						{
							num7 = 14;
							continue;
						}
						goto IL_1DA;
					}
					case 22:
						goto IL_24C;
					case 23:
					{
						PointF pointF;
						if (num4 < pointF.Y)
						{
							num7 = 7;
							continue;
						}
						goto IL_2DA;
					}
					case 24:
						goto IL_336;
					case 25:
					{
						PointF pointF;
						num = pointF.X;
						num7 = 26;
						continue;
					}
					case 26:
						goto IL_149;
					case 27:
						if (true)
						{
						}
						num7 = 3;
						continue;
					}
					break;
					IL_149:
					num7 = 21;
					continue;
					IL_1B4:
					num7 = 4;
					continue;
					IL_1DA:
					num7 = 8;
					continue;
					IL_2DA:
					num5++;
					num7 = 1;
					continue;
					IL_2F1:
					num7 = 10;
					continue;
					IL_334:
					goto IL_2F1;
					IL_336:
					num7 = 23;
				}
			}
			IL_144:
			IL_1AF:
			IL_22F:
			IL_24C:
			return new PointF(x, y);
		}
		}
	}

	// Token: 0x06004B0B RID: 19211 RVA: 0x002D97F8 File Offset: 0x002D87F8
	private int ᜀ(int A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_45;
				case 1:
					return A_0;
				case 3:
					A_0 -= 90;
					num = 1;
					continue;
				}
				if (A_0 > 90)
				{
					num = 3;
				}
				else
				{
					A_0 = -A_0;
					num = 0;
				}
			}
			IL_45:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_5B;
			}
		}
		IL_5B:
		if (true)
		{
		}
		if (false)
		{
		}
		return A_0;
	}

	// Token: 0x06004B0C RID: 19212 RVA: 0x002D9884 File Offset: 0x002D8884
	private string ᜀ(string A_0, int A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_78:
			num = 4;
			break;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		for (;;)
		{
			IL_3F:
			switch (num)
			{
			case 0:
				return A_0;
			case 1:
				goto IL_CB;
			case 2:
			{
				StringBuilder stringBuilder;
				A_0 = stringBuilder.ToString();
				num = 0;
				continue;
			}
			case 4:
			{
				int num2;
				int length;
				if (num2 >= length)
				{
					num = 2;
					continue;
				}
				StringBuilder stringBuilder;
				int num3;
				stringBuilder.Insert(num3, '\n');
				num2++;
				num3 += 2;
				num = 5;
				continue;
			}
			case 5:
				goto IL_AB;
			case 6:
			{
				StringBuilder stringBuilder = new StringBuilder(A_0);
				int num2 = 0;
				int num3 = 1;
				int length = A_0.Length;
				num = 1;
				continue;
			}
			}
			if (A_1 != 255)
			{
				return A_0;
			}
			num = 6;
		}
		IL_AB:
		goto IL_78;
		IL_CB:
		if (true)
		{
		}
		goto IL_78;
		IL_20:
		if (false)
		{
		}
		switch (0)
		{
		default:
			num = 3;
			goto IL_3F;
		}
	}

	// Token: 0x06004B0D RID: 19213 RVA: 0x002D9980 File Offset: 0x002D8980
	private void ᜀ(IXLSRange A_0, Rectangle A_1, Graphics A_2)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u192F spr_u192F = ((spr\u21A0)(A_0.Style as CellStyle).Wrapped).Wrapped;
		spr_u192F = this.ᜀ.ᜀ(A_0, spr_u192F);
		this.ᜀ(spr_u192F, A_1, A_2);
	}

	// Token: 0x06004B0E RID: 19214 RVA: 0x002D99F0 File Offset: 0x002D89F0
	private void ᜀ(IInternalAddtionalFormat A_0, Rectangle A_1, Graphics A_2)
	{
		Brush brush;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (A_1.Width > 0)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					return;
				case 2:
					return;
				case 3:
					goto IL_CE;
				case 5:
					num = 6;
					continue;
				case 6:
					if (A_0.FillPattern == ExcelPatternType.None)
					{
						num = 3;
						continue;
					}
					brush = this.ᜀ(A_0);
					A_2.FillRectangle(brush, A_1);
					brush.Dispose();
					num = 2;
					continue;
				}
				if (A_1.Height <= 0)
				{
					return;
				}
				num = 0;
			}
			IL_CE:
			break;
		}
		}
		A_1.Offset(1, 1);
		A_1.Width--;
		A_1.Height--;
		IBorders borders = A_0.Borders;
		A_1 = this.ᜀ(A_1, borders);
		brush = Brushes.White;
		A_2.FillRectangle(brush, A_1);
	}

	// Token: 0x06004B0F RID: 19215 RVA: 0x002D9B1C File Offset: 0x002D8B1C
	private Rectangle ᜀ(Rectangle A_0, IBorders A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (true)
				{
				}
				goto IL_81;
			case 2:
				goto IL_A8;
			case 3:
				A_0.Offset(-1, 0);
				A_0.Width++;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_134;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 4:
				A_0.Offset(0, -1);
				A_0.Height++;
				num = 1;
				continue;
			case 5:
				A_0.Height++;
				num = 8;
				continue;
			case 6:
				if (A_1[BordersLineType.EdgeTop].LineStyle != LineStyleType.None)
				{
					num = 4;
					continue;
				}
				goto IL_81;
			case 7:
				return A_0;
			case 8:
				goto IL_134;
			case 9:
				if (A_1[BordersLineType.EdgeBottom].LineStyle != LineStyleType.None)
				{
					num = 5;
					continue;
				}
				goto IL_134;
			case 10:
				A_0.Width++;
				num = 7;
				continue;
			case 11:
				if (A_1[BordersLineType.EdgeRight].LineStyle != LineStyleType.None)
				{
					num = 10;
					continue;
				}
				return A_0;
			}
			if (A_1[BordersLineType.EdgeLeft].LineStyle != LineStyleType.None)
			{
				num = 3;
				continue;
			}
			goto IL_A8;
			IL_81:
			num = 9;
			continue;
			IL_A8:
			num = 6;
			continue;
			IL_134:
			num = 11;
		}
		return A_0;
	}

	// Token: 0x06004B10 RID: 19216 RVA: 0x002D9CB8 File Offset: 0x002D8CB8
	private Brush ᜀ(IInternalAddtionalFormat A_0)
	{
		switch (0)
		{
		default:
		{
			Brush result;
			for (;;)
			{
				result = null;
				int num = 9;
				for (;;)
				{
					HatchStyle hatchstyle;
					switch (num)
					{
					case 0:
						return result;
					case 1:
						goto IL_170;
					case 2:
						goto IL_170;
					case 3:
						goto IL_170;
					case 4:
						goto IL_170;
					case 5:
						goto IL_170;
					case 6:
						if (true)
						{
						}
						goto IL_170;
					case 7:
						goto IL_170;
					case 8:
					{
						Color color = this.ᜀ(A_0.Color);
						result = new SolidBrush(color);
						num = 23;
						continue;
					}
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_131;
						default:
						{
							if (false)
							{
							}
							if (A_0.FillPattern == ExcelPatternType.Solid)
							{
								num = 8;
								continue;
							}
							hatchstyle = HatchStyle.Percent05;
							ExcelPatternType fillPattern = A_0.FillPattern;
							num = 10;
							continue;
						}
						}
						break;
					case 10:
					{
						ExcelPatternType fillPattern;
						switch (fillPattern)
						{
						case ExcelPatternType.Percent50:
							hatchstyle = HatchStyle.Percent50;
							num = 21;
							continue;
						case ExcelPatternType.Percent70:
							hatchstyle = HatchStyle.Percent70;
							num = 20;
							continue;
						case ExcelPatternType.Percent25:
							hatchstyle = HatchStyle.Percent25;
							num = 16;
							continue;
						case ExcelPatternType.DarkHorizontal:
							hatchstyle = HatchStyle.DarkHorizontal;
							num = 17;
							continue;
						case ExcelPatternType.DarkVertical:
							hatchstyle = HatchStyle.DarkVertical;
							num = 1;
							continue;
						case ExcelPatternType.DarkDownwardDiagonal:
							hatchstyle = HatchStyle.DarkDownwardDiagonal;
							num = 19;
							continue;
						case ExcelPatternType.DarkUpwardDiagonal:
							hatchstyle = HatchStyle.DarkUpwardDiagonal;
							num = 5;
							continue;
						case ExcelPatternType.ForwardDiagonal:
							hatchstyle = HatchStyle.ForwardDiagonal;
							num = 18;
							continue;
						case ExcelPatternType.Percent75:
							hatchstyle = HatchStyle.Percent75;
							num = 15;
							continue;
						case ExcelPatternType.Horizontal:
							hatchstyle = HatchStyle.Horizontal;
							num = 7;
							continue;
						case ExcelPatternType.Vertical:
							hatchstyle = HatchStyle.Vertical;
							num = 2;
							continue;
						case ExcelPatternType.LightDownwardDiagonal:
							hatchstyle = HatchStyle.LightDownwardDiagonal;
							num = 13;
							continue;
						case ExcelPatternType.LightUpwardDiagonal:
							hatchstyle = HatchStyle.LightUpwardDiagonal;
							num = 4;
							continue;
						case ExcelPatternType.Angle:
							hatchstyle = HatchStyle.SmallGrid;
							num = 22;
							continue;
						case ExcelPatternType.Percent60:
							hatchstyle = HatchStyle.Percent60;
							num = 14;
							continue;
						case ExcelPatternType.Percent10:
							hatchstyle = HatchStyle.Percent10;
							num = 3;
							continue;
						case ExcelPatternType.Percent05:
							hatchstyle = HatchStyle.Percent05;
							num = 6;
							continue;
						default:
							num = 11;
							continue;
						}
						break;
					}
					case 11:
						num = 12;
						continue;
					case 12:
						goto IL_170;
					case 13:
						goto IL_170;
					case 14:
						goto IL_170;
					case 15:
						goto IL_170;
					case 16:
						goto IL_170;
					case 17:
						goto IL_170;
					case 18:
						goto IL_170;
					case 19:
						goto IL_170;
					case 20:
						goto IL_170;
					case 21:
						goto IL_170;
					case 22:
						goto IL_131;
					case 23:
						return result;
					}
					break;
					IL_170:
					result = new HatchBrush(hatchstyle, A_0.PatternColor, A_0.Color);
					num = 0;
					continue;
					IL_131:
					goto IL_170;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06004B11 RID: 19217 RVA: 0x002D9F90 File Offset: 0x002D8F90
	private Color ᜀ(Color A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return Color.FromArgb(255, (int)A_0.R, (int)A_0.G, (int)A_0.B);
	}

	// Token: 0x06004B12 RID: 19218 RVA: 0x002D9FEC File Offset: 0x002D8FEC
	private StringAlignment ᜀ(IExtendedFormat A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6F:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		StringAlignment result;
		VerticalAlignType verticalAlignment;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				goto IL_9B;
			case 2:
				goto IL_87;
			case 3:
				goto IL_7B;
			case 4:
				return result;
			case 5:
				switch (verticalAlignment)
				{
				case VerticalAlignType.Top:
					result = StringAlignment.Near;
					num = 3;
					continue;
				case VerticalAlignType.Center:
					result = StringAlignment.Center;
					num = 2;
					continue;
				case VerticalAlignType.Bottom:
					goto IL_9B;
				default:
					num = 0;
					continue;
				}
				break;
			}
			goto IL_46;
			IL_9B:
			result = StringAlignment.Far;
			num = 4;
		}
		IL_7B:
		return result;
		IL_87:
		if (true)
		{
		}
		return result;
		IL_46:
		verticalAlignment = A_0.VerticalAlignment;
		num = 5;
		goto IL_28;
	}

	// Token: 0x06004B13 RID: 19219 RVA: 0x002DA0A4 File Offset: 0x002D90A4
	private StringAlignment ᜀ(IExtendedFormat A_0, IXLSRange A_1)
	{
		switch (0)
		{
		default:
		{
			StringAlignment result;
			for (;;)
			{
				IL_7B:
				HorizontalAlignType horizontalAlignment = A_0.HorizontalAlignment;
				for (;;)
				{
					IL_83:
					int num = 6;
					for (;;)
					{
						StringAlignment stringAlignment;
						CellFormatType cellFormatType;
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_1A7;
						case 2:
							stringAlignment = StringAlignment.Near;
							goto IL_124;
						case 3:
							if (true)
							{
							}
							result = StringAlignment.Center;
							num = 22;
							continue;
						case 4:
							if (A_0.Rotation == 255)
							{
								num = 3;
								continue;
							}
							num = 16;
							continue;
						case 5:
							if (A_1.FormulaStringValue == null)
							{
								num = 14;
								continue;
							}
							goto IL_20E;
						case 6:
							switch (horizontalAlignment)
							{
							case HorizontalAlignType.General:
								num = 4;
								continue;
							case HorizontalAlignType.Left:
							case HorizontalAlignType.Fill:
							case HorizontalAlignType.Justify:
								goto IL_1A7;
							case HorizontalAlignType.Center:
							case HorizontalAlignType.CenterAcrossSelection:
								result = StringAlignment.Center;
								num = 7;
								continue;
							case HorizontalAlignType.Right:
								result = StringAlignment.Far;
								num = 8;
								continue;
							default:
								num = 0;
								continue;
							}
							break;
						case 7:
							return result;
						case 8:
							return result;
						case 9:
							num = 20;
							continue;
						case 10:
							return result;
						case 11:
							return result;
						case 12:
							return result;
						case 13:
							num = 24;
							continue;
						case 14:
							num = 21;
							continue;
						case 15:
							num = 18;
							continue;
						case 16:
							if (!A_1.HasNumber)
							{
								num = 9;
								continue;
							}
							goto IL_153;
						case 17:
							if (cellFormatType != CellFormatType.Text)
							{
								num = 13;
								continue;
							}
							num = 2;
							continue;
						case 18:
							if (!A_1.HasFormulaBoolValue)
							{
								num = 23;
								continue;
							}
							goto IL_20E;
						case 19:
							num = 5;
							continue;
						case 20:
							if (A_1.HasFormula)
							{
								num = 19;
								continue;
							}
							goto IL_20E;
						case 21:
							if (!A_1.HasFormulaErrorValue)
							{
								num = 15;
								continue;
							}
							goto IL_20E;
						case 22:
							return result;
						case 23:
							goto IL_153;
						case 24:
							stringAlignment = StringAlignment.Far;
							goto IL_124;
						}
						goto IL_7B;
						IL_124:
						result = stringAlignment;
						num = 12;
						continue;
						IL_153:
						XlsWorkbook xlsWorkbook = A_1.Worksheet.Workbook as XlsWorkbook;
						sprᤅ sprᤅ = xlsWorkbook.InnerFormats.ᜁ(A_0.NumberFormatIndex);
						cellFormatType = sprᤅ.ᜀ(A_1.NumberValue);
						num = 17;
						continue;
						IL_1A7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_83;
						default:
							if (false)
							{
							}
							result = StringAlignment.Near;
							num = 11;
							continue;
						}
						IL_20E:
						result = StringAlignment.Near;
						num = 10;
					}
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06004B14 RID: 19220 RVA: 0x002DA384 File Offset: 0x002D9384
	private void ᜀ(IBorders A_0, Rectangle A_1, Graphics A_2, IXLSRange A_3)
	{
		IBorder border;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_132:
			this.ᜁ(A_0, border, A_1.Left, A_1.Top, A_1.Right, A_1.Bottom, A_2, A_3);
			num = 4;
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			goto IL_4E;
		}
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 0:
				if (border.ShowDiagonalLine)
				{
					num = 1;
					continue;
				}
				return;
			case 1:
				this.ᜁ(A_0, border, A_1.Left, A_1.Bottom, A_1.Right, A_1.Top, A_2, A_3);
				num = 3;
				continue;
			case 2:
				goto IL_132;
			case 3:
				return;
			case 4:
				goto IL_168;
			case 5:
				if (border.ShowDiagonalLine)
				{
					num = 2;
					continue;
				}
				goto IL_168;
			}
			goto IL_4E;
			IL_168:
			border = A_0[BordersLineType.DiagonalUp];
			num = 0;
		}
		return;
		IL_4E:
		border = A_0[BordersLineType.EdgeLeft];
		this.ᜁ(A_0, border, A_1.Left, A_1.Top, A_1.Left, A_1.Bottom, A_2, A_3);
		border = A_0[BordersLineType.EdgeRight];
		this.ᜁ(A_0, border, A_1.Right, A_1.Top, A_1.Right, A_1.Bottom, A_2, A_3);
		border = A_0[BordersLineType.EdgeTop];
		this.ᜁ(A_0, border, A_1.Left, A_1.Top, A_1.Right, A_1.Top, A_2, A_3);
		border = A_0[BordersLineType.EdgeBottom];
		this.ᜁ(A_0, border, A_1.Left, A_1.Bottom, A_1.Right, A_1.Bottom, A_2, A_3);
		border = A_0[BordersLineType.DiagonalDown];
		num = 5;
		goto IL_30;
	}

	// Token: 0x06004B15 RID: 19221 RVA: 0x002DA558 File Offset: 0x002D9558
	private void ᜁ(IBorders A_0, IBorder A_1, int A_2, int A_3, int A_4, int A_5, Graphics A_6, IXLSRange A_7)
	{
		if (true)
		{
		}
		if (A_1.LineStyle == LineStyleType.Double)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_29;
				}
			}
			IL_29:
			if (false)
			{
			}
			this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
			return;
		}
		this.ᜀ(A_1, A_2, A_3, A_4, A_5, A_6);
	}

	// Token: 0x06004B16 RID: 19222 RVA: 0x002DA5C4 File Offset: 0x002D95C4
	private void ᜀ(IBorders A_0, IBorder A_1, int A_2, int A_3, int A_4, int A_5, Graphics A_6, IXLSRange A_7)
	{
		switch (0)
		{
		default:
		{
			Pen a_;
			BordersLineType bordersLineType;
			int a_2;
			int a_3;
			for (;;)
			{
				a_ = this.ᜂ(A_1);
				XlsBorder xlsBorder = A_1 as XlsBorder;
				bordersLineType = xlsBorder.BorderIndex;
				BordersLineType bordersLineType2 = bordersLineType;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_8C;
					case 1:
						switch (bordersLineType2)
						{
						case BordersLineType.EdgeLeft:
							a_2 = -1;
							a_3 = 0;
							num = 7;
							continue;
						case BordersLineType.EdgeTop:
							a_2 = 0;
							a_3 = -1;
							num = 0;
							continue;
						case BordersLineType.EdgeBottom:
							a_2 = 0;
							a_3 = 1;
							num = 5;
							continue;
						case BordersLineType.EdgeRight:
							a_2 = 1;
							a_3 = 0;
							num = 4;
							continue;
						default:
							num = 2;
							continue;
						}
						break;
					case 2:
						num = 6;
						continue;
					case 3:
						goto IL_C1;
					case 4:
						goto IL_F7;
					case 5:
						goto IL_E5;
					case 6:
						a_2 = 1;
						a_3 = 1;
						num = 3;
						continue;
					case 7:
						goto IL_A1;
					}
					break;
				}
			}
			IL_8C:
			IL_A1:
			goto IL_115;
			IL_C1:
			if (true)
			{
			}
			IL_E5:
			goto IL_115;
			IL_F7:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			IL_115:
			this.ᜀ(A_6, a_, A_0, bordersLineType, A_2, A_3, A_4, A_5, a_2, a_3, A_7);
			this.ᜁ(A_6, a_, A_0, bordersLineType, A_2, A_3, A_4, A_5, a_2, a_3, A_7);
			return;
		}
		}
	}

	// Token: 0x06004B17 RID: 19223 RVA: 0x002DA714 File Offset: 0x002D9714
	private void ᜁ(Graphics A_0, Pen A_1, IBorders A_2, BordersLineType A_3, int A_4, int A_5, int A_6, int A_7, int A_8, int A_9, IXLSRange A_10)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		BordersLineType bordersLineType;
		BordersLineType bordersLineType2;
		this.ᜀ(A_3, out bordersLineType, out bordersLineType2);
		int num = A_8;
		int num2 = A_8;
		int num3 = A_9;
		int num4 = A_9;
		int num5 = A_10.Row + A_9;
		int num6 = A_10.Column + A_8;
		IBorders borders = A_10.Worksheet[num5, num6].Borders;
		this.ᜀ(A_10.Worksheet, num5, num6, A_8, A_9, ref num, ref num3, true, borders, bordersLineType2, bordersLineType, true);
		this.ᜀ(A_10.Worksheet, num5, num6, A_8, A_9, ref num2, ref num4, true, borders, bordersLineType, bordersLineType2, false);
		A_0.DrawLine(A_1, A_4 + num, A_5 + num3, A_6 + num2, A_7 + num4);
	}

	// Token: 0x06004B18 RID: 19224 RVA: 0x002DA7F0 File Offset: 0x002D97F0
	private void ᜀ(Graphics A_0, Pen A_1, IBorders A_2, BordersLineType A_3, int A_4, int A_5, int A_6, int A_7, int A_8, int A_9, IXLSRange A_10)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		BordersLineType bordersLineType;
		BordersLineType bordersLineType2;
		this.ᜀ(A_3, out bordersLineType, out bordersLineType2);
		int num = A_8;
		int num2 = A_8;
		int num3 = A_9;
		int num4 = A_9;
		int row = A_10.Row;
		int column = A_10.Column;
		IWorksheet worksheet = A_10.Worksheet;
		this.ᜀ(worksheet, row, column, -A_8, -A_9, ref num, ref num3, false, A_2, bordersLineType2, bordersLineType, true);
		this.ᜀ(worksheet, row, column, -A_8, -A_9, ref num2, ref num4, false, A_2, bordersLineType, bordersLineType2, false);
		A_0.DrawLine(A_1, A_4 - num, A_5 - num3, A_6 - num2, A_7 - num4);
	}

	// Token: 0x06004B19 RID: 19225 RVA: 0x002DA8B0 File Offset: 0x002D98B0
	private void ᜀ(IWorksheet A_0, int A_1, int A_2, int A_3, int A_4, ref int A_5, ref int A_6, bool A_7, IBorders A_8, BordersLineType A_9, BordersLineType A_10, bool A_11)
	{
		int num6;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					int num = A_1 + A_3;
					int num2 = A_2 + A_4;
					IWorkbook workbook = A_0.Workbook;
					int maxRowCount = workbook.MaxRowCount;
					int maxColumnCount = workbook.MaxColumnCount;
					int num3 = 21;
					for (;;)
					{
						int num4;
						bool flag;
						int num5;
						bool flag2;
						switch (num3)
						{
						case 0:
							if (A_8[A_10].LineStyle == LineStyleType.Double)
							{
								num3 = 7;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num3 = 22;
								continue;
							}
							break;
						case 1:
							if (A_10 != (BordersLineType)(-1))
							{
								num3 = 6;
								continue;
							}
							goto IL_1B2;
						case 2:
							num4 = -1;
							goto IL_2E4;
						case 3:
							if (flag)
							{
								num3 = 10;
								continue;
							}
							goto IL_1B2;
						case 4:
							num5 = 1;
							goto IL_2C7;
						case 5:
							num3 = 15;
							continue;
						case 6:
							num3 = 3;
							continue;
						case 7:
							flag2 = true;
							goto IL_2A7;
						case 8:
							if (A_4 != 0)
							{
								num3 = 13;
								continue;
							}
							num3 = 23;
							continue;
						case 9:
							if (!A_11)
							{
								num3 = 26;
								continue;
							}
							num3 = 4;
							continue;
						case 10:
							num3 = 16;
							continue;
						case 11:
							if (true)
							{
							}
							flag = !flag;
							num3 = 30;
							continue;
						case 12:
							num3 = 29;
							continue;
						case 13:
							goto IL_188;
						case 14:
							goto IL_16C;
						case 15:
							if (num2 > 0)
							{
								num3 = 27;
								continue;
							}
							return;
						case 16:
							if (!A_11)
							{
								num3 = 33;
								continue;
							}
							num3 = 2;
							continue;
						case 17:
							A_6 = num6;
							num3 = 19;
							continue;
						case 18:
						{
							if (num2 > maxColumnCount)
							{
								num3 = 20;
								continue;
							}
							IBorders borders = A_0[A_1 + A_3, A_2 + A_4].Borders;
							num3 = 0;
							continue;
						}
						case 19:
							goto IL_251;
						case 20:
							goto IL_1AD;
						case 21:
							if (num > 0)
							{
								num3 = 12;
								continue;
							}
							return;
						case 22:
							num3 = 28;
							continue;
						case 23:
							if (A_3 != 0)
							{
								num3 = 17;
								continue;
							}
							return;
						case 24:
							num5 = -1;
							goto IL_2C7;
						case 25:
							num4 = 1;
							goto IL_2E4;
						case 26:
							num3 = 24;
							continue;
						case 27:
							num3 = 18;
							continue;
						case 28:
						{
							IBorders borders;
							flag2 = (borders[A_9].LineStyle == LineStyleType.Double);
							goto IL_2A7;
						}
						case 29:
							if (num <= maxRowCount)
							{
								num3 = 5;
								continue;
							}
							return;
						case 30:
							goto IL_2F7;
						case 31:
							goto IL_16C;
						case 32:
							if (A_7)
							{
								num3 = 11;
								continue;
							}
							goto IL_2F7;
						case 33:
							num3 = 25;
							continue;
						}
						break;
						IL_16C:
						num3 = 8;
						continue;
						IL_1B2:
						num3 = 9;
						continue;
						IL_2A7:
						flag = flag2;
						num3 = 32;
						continue;
						IL_2C7:
						num6 = num5;
						num3 = 14;
						continue;
						IL_2E4:
						num6 = num4;
						num3 = 31;
						continue;
						IL_2F7:
						num3 = 1;
					}
				}
				break;
			}
		}
		IL_188:
		A_5 = num6;
		return;
		IL_1AD:
		return;
		IL_251:;
	}

	// Token: 0x06004B1A RID: 19226 RVA: 0x002DAC40 File Offset: 0x002D9C40
	private void ᜀ(BordersLineType A_0, out BordersLineType A_1, out BordersLineType A_2)
	{
		A_1 = (BordersLineType)(-1);
		A_2 = (BordersLineType)(-1);
		switch (A_0)
		{
		case BordersLineType.EdgeLeft:
		case BordersLineType.EdgeRight:
			A_1 = BordersLineType.EdgeTop;
			A_2 = BordersLineType.EdgeBottom;
			return;
		case BordersLineType.EdgeTop:
		case BordersLineType.EdgeBottom:
			break;
		default:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return;
			}
			break;
		}
		A_1 = BordersLineType.EdgeLeft;
		A_2 = BordersLineType.EdgeRight;
	}

	// Token: 0x06004B1B RID: 19227 RVA: 0x002DACB0 File Offset: 0x002D9CB0
	private void ᜀ(IBorder A_0, int A_1, int A_2, int A_3, int A_4, Graphics A_5)
	{
		for (;;)
		{
			IL_14:
			Pen pen;
			int num;
			LineStyleType lineStyle;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A8:
				pen = this.ᜂ(A_0);
				num = 0;
				break;
			default:
				if (false)
				{
				}
				lineStyle = A_0.LineStyle;
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BB;
				case 1:
					goto IL_52;
				case 2:
					if (true)
					{
					}
					if (lineStyle != LineStyleType.None)
					{
						num = 1;
						continue;
					}
					return;
				}
				goto IL_14;
			}
			IL_BB:
			try
			{
				A_5.DrawLine(pen, A_1, A_2, A_3, A_4);
				break;
			}
			finally
			{
				num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						((IDisposable)pen).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_A5;
					}
					if (pen == null)
					{
						break;
					}
					num = 1;
				}
				IL_A5:;
			}
			IL_52:
			goto IL_A8;
		}
	}

	// Token: 0x06004B1C RID: 19228 RVA: 0x002DAD8C File Offset: 0x002D9D8C
	private Pen ᜂ(IBorder A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		Color color = this.ᜀ(A_0.Color);
		return new Pen(color, this.ᜀ(A_0))
		{
			DashStyle = this.ᜁ(A_0)
		};
	}

	// Token: 0x06004B1D RID: 19229 RVA: 0x002DADF0 File Offset: 0x002D9DF0
	private DashStyle ᜁ(IBorder A_0)
	{
		DashStyle result;
		for (;;)
		{
			IL_44:
			result = DashStyle.Solid;
			LineStyleType lineStyle = A_0.LineStyle;
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						return result;
					case 1:
						return result;
					case 2:
						return result;
					case 3:
						switch (lineStyle)
						{
						case LineStyleType.Thin:
						case LineStyleType.Medium:
						case LineStyleType.Thick:
						case LineStyleType.Double:
						case LineStyleType.Hair:
							result = DashStyle.Solid;
							num = 0;
							continue;
						case LineStyleType.Dashed:
						case LineStyleType.MediumDashed:
							result = DashStyle.Dash;
							num = 1;
							continue;
						case LineStyleType.Dotted:
							result = DashStyle.Dot;
							num = 6;
							continue;
						case LineStyleType.DashDot:
						case LineStyleType.MediumDashDot:
						case LineStyleType.SlantedDashDot:
							goto IL_B0;
						case LineStyleType.DashDotDot:
						case LineStyleType.MediumDashDotDot:
							result = DashStyle.DashDotDot;
							num = 4;
							continue;
						default:
							num = 7;
							continue;
						}
						break;
					case 4:
						return result;
					case 5:
						return result;
					case 6:
						return result;
					case 7:
						if (true)
						{
						}
						num = 5;
						continue;
					}
					goto IL_44;
				}
				IL_B0:
				result = DashStyle.DashDot;
				num = 2;
			}
		}
		return result;
	}

	// Token: 0x06004B1E RID: 19230 RVA: 0x002DAF08 File Offset: 0x002D9F08
	private float ᜀ(IBorder A_0)
	{
		float result;
		for (;;)
		{
			result = 0f;
			LineStyleType lineStyle = A_0.LineStyle;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					return result;
				case 2:
					return result;
				case 3:
					return result;
				case 4:
					return result;
				case 5:
					switch (lineStyle)
					{
					case LineStyleType.Thin:
					case LineStyleType.Dashed:
					case LineStyleType.Dotted:
					case LineStyleType.Double:
					case LineStyleType.DashDot:
					case LineStyleType.DashDotDot:
					case LineStyleType.SlantedDashDot:
						result = 1f;
						num = 6;
						continue;
					case LineStyleType.Medium:
					case LineStyleType.MediumDashed:
					case LineStyleType.MediumDashDot:
					case LineStyleType.MediumDashDotDot:
						result = 2f;
						num = 1;
						continue;
					case LineStyleType.Thick:
						result = 3f;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case LineStyleType.Hair:
						result = 0.5f;
						if (true)
						{
						}
						num = 4;
						continue;
					default:
						num = 0;
						continue;
					}
					break;
				case 6:
					return result;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06004B1F RID: 19231 RVA: 0x002DB020 File Offset: 0x002DA020
	private Image ᜀ(int A_0, int A_1, ImageType A_2, Stream A_3, EmfType A_4)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			Image result;
			for (;;)
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					Image image;
					switch (num)
					{
					case 0:
						A_3 = new MemoryStream();
						num = 7;
						continue;
					case 1:
						switch (A_2)
						{
						case ImageType.Bitmap:
							result = new Bitmap(A_0, A_1);
							num = 2;
							continue;
						case ImageType.Metafile:
							num = 6;
							continue;
						default:
							num = 3;
							continue;
						}
						break;
					case 2:
						goto IL_176;
					case 3:
						num = 4;
						continue;
					case 4:
						goto IL_198;
					case 5:
						try
						{
							Graphics graphics = Graphics.FromImage(image);
							try
							{
								IntPtr hdc = graphics.GetHdc();
								Rectangle frameRect = new Rectangle(0, 0, A_0, A_1);
								result = new Metafile(A_3, hdc, frameRect, MetafileFrameUnit.Pixel, A_4);
								graphics.ReleaseHdc();
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										switch (num)
										{
										case 0:
											goto IL_103;
										case 1:
											((IDisposable)graphics).Dispose();
											num = 0;
											continue;
										}
										break;
									}
									IL_E6:
									if (graphics != null)
									{
										num = 1;
										continue;
									}
									break;
									goto IL_E6;
								}
								IL_103:;
							}
							return result;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_146;
								case 2:
									((IDisposable)image).Dispose();
									num = 1;
									continue;
								}
								if (image == null)
								{
									break;
								}
								num = 2;
							}
							IL_146:;
						}
						goto IL_149;
					case 6:
						if (A_3 == null)
						{
							num = 0;
							continue;
						}
						goto IL_149;
					case 7:
						goto IL_149;
					}
					break;
					IL_149:
					image = new Bitmap(A_0, A_1);
					num = 5;
				}
			}
			IL_176:
			return result;
			IL_198:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷圹崻夽┿ᙁ㵃㙅ⵇ", a_));
		}
		}
	}

	// Token: 0x06004B20 RID: 19232 RVA: 0x002DB218 File Offset: 0x002DA218
	private Image ᜀ(int A_0, int A_1, int A_2, int A_3, sprᱥ A_4, sprᱥ A_5)
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (false)
		{
		}
		if (true)
		{
		}
		int height = A_4.ᜀ(A_0, A_2);
		int width = A_5.ᜀ(A_1, A_3);
		Image image = new Bitmap(width, height);
		Graphics graphics = Graphics.FromImage(image);
		try
		{
			graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
		}
		finally
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					((IDisposable)graphics).Dispose();
					num = 2;
					continue;
				case 2:
					goto IL_B3;
				}
				if (graphics == null)
				{
					break;
				}
				num = 0;
			}
			IL_B3:;
		}
		return image;
	}

	// Token: 0x04002203 RID: 8707
	private spr\u2310 ᜀ = new spr\u2310();

	// Token: 0x020004C3 RID: 1219
	// (Invoke) Token: 0x06004B23 RID: 19235
	private delegate void ᜀ(XlsWorksheet A_0, spr\u25A6.ᜀ A_1, int A_2, int A_3, Graphics A_4, sprᱥ A_5, sprᱥ A_6);

	// Token: 0x020004C4 RID: 1220
	// (Invoke) Token: 0x06004B27 RID: 19239
	private delegate void ᜁ(IXLSRange A_0, Rectangle A_1, Graphics A_2);
}
