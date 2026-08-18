using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001A8 RID: 424
	public static class ColorQuantizer
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x0007E6C0 File Offset: 0x0007C8C0
		public static Bitmap Quantize(Image image, PixelFormat bitmapPixelFormat)
		{
			return ColorQuantizer.Quantize(image, bitmapPixelFormat, true);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0007E6CC File Offset: 0x0007C8CC
		public static Bitmap Quantize(Image image, PixelFormat pixelFormat, bool useDither)
		{
			Bitmap bitmap = image as Bitmap;
			if (bitmap != null && bitmap.PixelFormat == PixelFormat.Format32bppArgb)
			{
				return ColorQuantizer.DoQuantize(bitmap, pixelFormat, useDither);
			}
			int width = image.Width;
			int height = image.Height;
			Rectangle destRect = Rectangle.FromLTRB(0, 0, width, height);
			Bitmap result;
			using (Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format32bppArgb))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap2))
				{
					graphics.DrawImage(image, destRect, 0, 0, width, height, GraphicsUnit.Pixel);
				}
				result = ColorQuantizer.DoQuantize(bitmap2, pixelFormat, useDither);
			}
			return result;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0007E77C File Offset: 0x0007C97C
		private static Bitmap DoQuantize(Bitmap bitmapSource, PixelFormat pixelFormat, bool useDither)
		{
			int width = bitmapSource.Width;
			int height = bitmapSource.Height;
			Rectangle rect = Rectangle.FromLTRB(0, 0, width, height);
			Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap(width, height, pixelFormat);
				BitmapData bitmapData = bitmapSource.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
				try
				{
					ColorQuantizer.Octree octree = new ColorQuantizer.Octree(pixelFormat);
					int num = Math.Abs(bitmapData.Stride);
					byte[] array = new byte[num * height];
					Marshal.Copy(bitmapData.Scan0, array, 0, array.Length);
					int num2 = 0;
					for (int i = 0; i < height; i++)
					{
						for (int j = 0; j < width; j++)
						{
							ColorQuantizer.Pixel sourcePixel = ColorQuantizer.GetSourcePixel(array, num2, j);
							octree.AddColor(sourcePixel);
						}
						num2 += num;
					}
					Color[] paletteColors = octree.GetPaletteColors();
					ColorPalette palette = bitmap.Palette;
					for (int k = 0; k < palette.Entries.Length; k++)
					{
						palette.Entries[k] = ((k < paletteColors.Length) ? paletteColors[k] : Color.Transparent);
					}
					bitmap.Palette = palette;
					BitmapData bitmapData2 = bitmap.LockBits(rect, ImageLockMode.ReadWrite, pixelFormat);
					try
					{
						int num3 = Math.Abs(bitmapData2.Stride);
						byte[] array2 = new byte[num3 * height];
						num2 = 0;
						int num4 = 0;
						for (int l = 0; l < height; l++)
						{
							for (int m = 0; m < width; m++)
							{
								ColorQuantizer.Pixel sourcePixel2 = ColorQuantizer.GetSourcePixel(array, num2, m);
								int paletteIndex = octree.GetPaletteIndex(sourcePixel2);
								if (useDither && sourcePixel2.Alpha != 0)
								{
									Color color = paletteColors[paletteIndex];
									int deltaRed = (int)(sourcePixel2.Red - color.R);
									int deltaGreen = (int)(sourcePixel2.Green - color.G);
									int deltaBlue = (int)(sourcePixel2.Blue - color.B);
									if (m + 1 < width)
									{
										ColorQuantizer.DitherSourcePixel(array, num2, m + 1, deltaRed, deltaGreen, deltaBlue, 7);
									}
									if (l + 1 < height)
									{
										int rowStart = num2 + num;
										if (m > 0)
										{
											ColorQuantizer.DitherSourcePixel(array, rowStart, m - 1, deltaRed, deltaGreen, deltaBlue, 3);
										}
										ColorQuantizer.DitherSourcePixel(array, rowStart, m, deltaRed, deltaGreen, deltaBlue, 5);
										if (m + 1 < width)
										{
											ColorQuantizer.DitherSourcePixel(array, rowStart, m + 1, deltaRed, deltaGreen, deltaBlue, 1);
										}
									}
								}
								if (pixelFormat != PixelFormat.Format1bppIndexed)
								{
									if (pixelFormat != PixelFormat.Format4bppIndexed)
									{
										if (pixelFormat == PixelFormat.Format8bppIndexed)
										{
											array2[num4 + m] = (byte)paletteIndex;
										}
									}
									else
									{
										byte[] array3 = array2;
										int num5 = num4 + (m >> 1);
										array3[num5] |= (((m & 1) == 1) ? ((byte)(paletteIndex & 15)) : ((byte)(paletteIndex << 4)));
									}
								}
								else if (paletteIndex != 0)
								{
									byte[] array4 = array2;
									int num6 = num4 + (m >> 3);
									array4[num6] |= (byte)(128 >> (m & 7));
								}
							}
							num2 += num;
							num4 += num3;
						}
						Marshal.Copy(array2, 0, bitmapData2.Scan0, array2.Length);
					}
					finally
					{
						bitmap.UnlockBits(bitmapData2);
						bitmapData2 = null;
					}
				}
				finally
				{
					bitmapSource.UnlockBits(bitmapData);
					bitmapData = null;
				}
			}
			catch (Exception)
			{
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
				throw;
			}
			return bitmap;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0007EAE4 File Offset: 0x0007CCE4
		private static void DitherSourcePixel(byte[] buffer, int rowStart, int col, int deltaRed, int deltaGreen, int deltaBlue, int weight)
		{
			int num = rowStart + col * 4;
			buffer[num + 2] = ColorQuantizer.ChannelAdjustment(buffer[num + 2], deltaRed * weight >> 4);
			buffer[num + 1] = ColorQuantizer.ChannelAdjustment(buffer[num + 1], deltaGreen * weight >> 4);
			buffer[num] = ColorQuantizer.ChannelAdjustment(buffer[num], deltaBlue * weight >> 4);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0007EB34 File Offset: 0x0007CD34
		private static ColorQuantizer.Pixel GetSourcePixel(byte[] buffer, int rowStart, int col)
		{
			int num = rowStart + col * 4;
			return new ColorQuantizer.Pixel
			{
				Alpha = buffer[num + 3],
				Red = buffer[num + 2],
				Green = buffer[num + 1],
				Blue = buffer[num]
			};
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0007EB78 File Offset: 0x0007CD78
		private static byte ChannelAdjustment(byte current, int offset)
		{
			return (byte)Math.Min(255, Math.Max(0, (int)current + offset));
		}

		// Token: 0x020001A9 RID: 425
		private class Octree
		{
			// Token: 0x060015D1 RID: 5585 RVA: 0x0007EB90 File Offset: 0x0007CD90
			internal Octree(PixelFormat pixelFormat)
			{
				if (pixelFormat != PixelFormat.Format1bppIndexed)
				{
					if (pixelFormat != PixelFormat.Format4bppIndexed)
					{
						if (pixelFormat != PixelFormat.Format8bppIndexed)
						{
							throw new ArgumentException("Invalid Pixel Format", "pixelFormat");
						}
						this.m_maxColors = 256;
					}
					else
					{
						this.m_maxColors = 16;
					}
				}
				else
				{
					this.m_maxColors = 2;
				}
				this.m_reducibleNodes = new ColorQuantizer.Octree.OctreeNode[7];
				this.m_root = new ColorQuantizer.Octree.OctreeNode(0, this);
			}

			// Token: 0x060015D2 RID: 5586 RVA: 0x0007EC08 File Offset: 0x0007CE08
			internal void AddColor(ColorQuantizer.Pixel pixel)
			{
				if (pixel.Alpha <= 0)
				{
					this.m_hasTransparent = true;
					return;
				}
				if (this.m_lastNode != null && pixel.ARGB == this.m_lastArgb)
				{
					this.m_lastNode.AddColor(pixel);
					return;
				}
				this.m_colorCount += (this.m_root.AddColor(pixel) ? 1 : 0);
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x0007EC6C File Offset: 0x0007CE6C
			internal int GetPaletteIndex(ColorQuantizer.Pixel pixel)
			{
				int num = 0;
				if (pixel.Alpha > 0)
				{
					num = this.m_root.GetPaletteIndex(pixel);
					if (num < 0)
					{
						int num2 = int.MaxValue;
						for (int i = 0; i < this.m_palette.Length; i++)
						{
							Color color = this.m_palette[i];
							int num3 = (int)(pixel.Red - color.R);
							int num4 = (int)(pixel.Green - color.G);
							int num5 = (int)(pixel.Blue - color.B);
							int num6 = num3 * num3 + num4 * num4 + num5 * num5;
							if (num6 < num2)
							{
								num2 = num6;
								num = i;
							}
						}
					}
				}
				return num;
			}

			// Token: 0x060015D4 RID: 5588 RVA: 0x0007ED14 File Offset: 0x0007CF14
			internal Color[] GetPaletteColors()
			{
				if (this.m_palette == null)
				{
					int num = this.m_reducibleNodes.Length - 1;
					int num2 = this.m_maxColors - (this.m_hasTransparent ? 1 : 0);
					while (this.m_colorCount > num2)
					{
						while (num > 0 && this.m_reducibleNodes[num] == null)
						{
							num--;
						}
						if (this.m_reducibleNodes[num] == null)
						{
							break;
						}
						ColorQuantizer.Octree.OctreeNode octreeNode = this.m_reducibleNodes[num];
						this.m_reducibleNodes[num] = octreeNode.NextReducibleNode;
						this.m_colorCount -= octreeNode.Reduce() - 1;
					}
					if (num == 0 && !this.m_hasTransparent)
					{
						this.m_palette = new Color[2];
						this.m_palette[0] = Color.Black;
						this.m_palette[1] = Color.White;
						this.m_root = new ColorQuantizer.Octree.OctreeNode(0, this);
					}
					else
					{
						int num3 = 0;
						this.m_palette = new Color[this.m_colorCount + (this.m_hasTransparent ? 1 : 0)];
						if (this.m_hasTransparent)
						{
							this.m_palette[num3++] = Color.Transparent;
						}
						this.m_root.AddColorsToPalette(this.m_palette, ref num3);
					}
				}
				return this.m_palette;
			}

			// Token: 0x060015D5 RID: 5589 RVA: 0x0007EE4B File Offset: 0x0007D04B
			private void SetLastNode(ColorQuantizer.Octree.OctreeNode node, int argb)
			{
				this.m_lastNode = node;
				this.m_lastArgb = argb;
			}

			// Token: 0x060015D6 RID: 5590 RVA: 0x0007EE5B File Offset: 0x0007D05B
			private void AddReducibleNode(ColorQuantizer.Octree.OctreeNode reducibleNode)
			{
				reducibleNode.NextReducibleNode = this.m_reducibleNodes[reducibleNode.Level];
				this.m_reducibleNodes[reducibleNode.Level] = reducibleNode;
			}

			// Token: 0x04000B85 RID: 2949
			private readonly int m_maxColors;

			// Token: 0x04000B86 RID: 2950
			private readonly ColorQuantizer.Octree.OctreeNode[] m_reducibleNodes;

			// Token: 0x04000B87 RID: 2951
			private int m_colorCount;

			// Token: 0x04000B88 RID: 2952
			private bool m_hasTransparent;

			// Token: 0x04000B89 RID: 2953
			private int m_lastArgb;

			// Token: 0x04000B8A RID: 2954
			private ColorQuantizer.Octree.OctreeNode m_lastNode;

			// Token: 0x04000B8B RID: 2955
			private Color[] m_palette;

			// Token: 0x04000B8C RID: 2956
			private ColorQuantizer.Octree.OctreeNode m_root;

			// Token: 0x020001AA RID: 426
			private class OctreeNode
			{
				// Token: 0x060015D7 RID: 5591 RVA: 0x0007EE7E File Offset: 0x0007D07E
				internal OctreeNode(int level, ColorQuantizer.Octree octree)
				{
					this.m_octree = octree;
					this.m_level = level;
					this.m_isLeaf = (level == 7);
					if (!this.m_isLeaf)
					{
						this.m_childNodes = new ColorQuantizer.Octree.OctreeNode[8];
						this.m_octree.AddReducibleNode(this);
					}
				}

				// Token: 0x17000553 RID: 1363
				// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0007EEBE File Offset: 0x0007D0BE
				internal int Level
				{
					get
					{
						return this.m_level;
					}
				}

				// Token: 0x17000554 RID: 1364
				// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0007EEC6 File Offset: 0x0007D0C6
				internal Color NodeColor
				{
					get
					{
						return Color.FromArgb(this.m_redSum / this.m_pixelCount, this.m_greenSum / this.m_pixelCount, this.m_blueSum / this.m_pixelCount);
					}
				}

				// Token: 0x17000555 RID: 1365
				// (get) Token: 0x060015DA RID: 5594 RVA: 0x0007EEF4 File Offset: 0x0007D0F4
				// (set) Token: 0x060015DB RID: 5595 RVA: 0x0007EEFC File Offset: 0x0007D0FC
				internal ColorQuantizer.Octree.OctreeNode NextReducibleNode { get; set; }

				// Token: 0x060015DC RID: 5596 RVA: 0x0007EF08 File Offset: 0x0007D108
				internal bool AddColor(ColorQuantizer.Pixel pixel)
				{
					bool result;
					if (this.m_isLeaf)
					{
						result = (++this.m_pixelCount == 1);
						this.m_redSum += (int)pixel.Red;
						this.m_greenSum += (int)pixel.Green;
						this.m_blueSum += (int)pixel.Blue;
						this.m_octree.SetLastNode(this, pixel.ARGB);
					}
					else
					{
						int childIndex = this.GetChildIndex(pixel);
						if (this.m_childNodes[childIndex] == null)
						{
							this.m_childNodes[childIndex] = new ColorQuantizer.Octree.OctreeNode(this.m_level + 1, this.m_octree);
						}
						result = this.m_childNodes[childIndex].AddColor(pixel);
					}
					return result;
				}

				// Token: 0x060015DD RID: 5597 RVA: 0x0007EFC0 File Offset: 0x0007D1C0
				internal int GetPaletteIndex(ColorQuantizer.Pixel pixel)
				{
					int result = -1;
					if (this.m_isLeaf)
					{
						result = this.m_paletteIndex;
					}
					else
					{
						int childIndex = this.GetChildIndex(pixel);
						if (this.m_childNodes[childIndex] != null)
						{
							result = this.m_childNodes[childIndex].GetPaletteIndex(pixel);
						}
					}
					return result;
				}

				// Token: 0x060015DE RID: 5598 RVA: 0x0007F004 File Offset: 0x0007D204
				internal int Reduce()
				{
					int num = 0;
					if (!this.m_isLeaf)
					{
						for (int i = 0; i < this.m_childNodes.Length; i++)
						{
							if (this.m_childNodes[i] != null)
							{
								ColorQuantizer.Octree.OctreeNode octreeNode = this.m_childNodes[i];
								this.m_pixelCount += octreeNode.m_pixelCount;
								this.m_redSum += octreeNode.m_redSum;
								this.m_greenSum += octreeNode.m_greenSum;
								this.m_blueSum += octreeNode.m_blueSum;
								num++;
							}
						}
						this.m_childNodes = null;
						this.m_isLeaf = true;
					}
					return num;
				}

				// Token: 0x060015DF RID: 5599 RVA: 0x0007F0A4 File Offset: 0x0007D2A4
				internal void AddColorsToPalette(Color[] colorArray, ref int paletteIndex)
				{
					if (this.m_isLeaf)
					{
						this.m_paletteIndex = paletteIndex++;
						colorArray[this.m_paletteIndex] = this.NodeColor;
						return;
					}
					for (int i = 0; i < this.m_childNodes.Length; i++)
					{
						if (this.m_childNodes[i] != null)
						{
							this.m_childNodes[i].AddColorsToPalette(colorArray, ref paletteIndex);
						}
					}
				}

				// Token: 0x060015E0 RID: 5600 RVA: 0x0007F10C File Offset: 0x0007D30C
				private int GetChildIndex(ColorQuantizer.Pixel pixel)
				{
					int num = 7 - this.m_level;
					int num2 = (int)ColorQuantizer.Octree.OctreeNode.s_levelMasks[this.m_level];
					return ((int)pixel.Red & num2) >> num - 2 | ((int)pixel.Green & num2) >> num - 1 | ((int)pixel.Blue & num2) >> num;
				}

				// Token: 0x04000B8D RID: 2957
				private static readonly byte[] s_levelMasks = new byte[]
				{
					128,
					64,
					32,
					16,
					8,
					4,
					2,
					1
				};

				// Token: 0x04000B8E RID: 2958
				private readonly int m_level;

				// Token: 0x04000B8F RID: 2959
				private readonly ColorQuantizer.Octree m_octree;

				// Token: 0x04000B90 RID: 2960
				private int m_blueSum;

				// Token: 0x04000B91 RID: 2961
				private ColorQuantizer.Octree.OctreeNode[] m_childNodes;

				// Token: 0x04000B92 RID: 2962
				private int m_greenSum;

				// Token: 0x04000B93 RID: 2963
				private bool m_isLeaf;

				// Token: 0x04000B94 RID: 2964
				private int m_paletteIndex;

				// Token: 0x04000B95 RID: 2965
				private int m_pixelCount;

				// Token: 0x04000B96 RID: 2966
				private int m_redSum;
			}
		}

		// Token: 0x020001AB RID: 427
		private class Pixel
		{
			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0007F180 File Offset: 0x0007D380
			// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0007F188 File Offset: 0x0007D388
			public byte Blue { get; set; }

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0007F191 File Offset: 0x0007D391
			// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0007F199 File Offset: 0x0007D399
			public byte Green { get; set; }

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0007F1A2 File Offset: 0x0007D3A2
			// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0007F1AA File Offset: 0x0007D3AA
			public byte Red { get; set; }

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0007F1B3 File Offset: 0x0007D3B3
			// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0007F1BB File Offset: 0x0007D3BB
			public byte Alpha { get; set; }

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060015EA RID: 5610 RVA: 0x0007F1C4 File Offset: 0x0007D3C4
			public int ARGB
			{
				get
				{
					return (int)this.Alpha << 24 | (int)this.Red << 16 | (int)this.Green << 8 | (int)this.Blue;
				}
			}
		}
	}
}
