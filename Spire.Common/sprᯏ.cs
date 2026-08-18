using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Spire.Pdf.Graphics;
using Spire.Xls;
using Spire.Xls.Core;

// Token: 0x02000011 RID: 17
internal class sprᯏ
{
	// Token: 0x06000062 RID: 98 RVA: 0x00004EF8 File Offset: 0x000030F8
	public static Metafile ᜀ(string A_0)
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
		return sprᯏ.ᜀ.ᜀ(A_0);
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00004F3C File Offset: 0x0000313C
	private void ᜀ(PdfCanvas A_0, RectangleF A_1, Color A_2, Color A_3, GradientVariantsType A_4, PdfLinearGradientMode A_5)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			RectangleF empty;
			RectangleF empty2;
			PdfLinearGradientMode mode;
			for (;;)
			{
				PdfLinearGradientMode pdfLinearGradientMode;
				switch (num)
				{
				case 0:
					pdfLinearGradientMode = A_5;
					goto IL_2BC;
				case 1:
					goto IL_30B;
				case 2:
					pdfLinearGradientMode = PdfLinearGradientMode.Vertical;
					goto IL_2BC;
				case 3:
					num = 1;
					continue;
				case 4:
					goto IL_2FA;
				case 5:
					num = 4;
					continue;
				case 6:
					goto IL_261;
				case 8:
					switch (A_5)
					{
					case PdfLinearGradientMode.BackwardDiagonal:
					case PdfLinearGradientMode.ForwardDiagonal:
						empty = new RectangleF(A_1.Left, A_1.Top, A_1.Width, A_1.Height / 2f + 1f);
						empty2 = new RectangleF(A_1.Left, A_1.Top + A_1.Height / 2f - 1f, A_1.Width, A_1.Height / 2f + 1f);
						num = 11;
						continue;
					case PdfLinearGradientMode.Horizontal:
						empty = new RectangleF(A_1.Left, A_1.Top, A_1.Width / 2f, A_1.Height);
						empty2 = new RectangleF(A_1.Left + A_1.Width / 2f, A_1.Top, A_1.Width / 2f, A_1.Height);
						num = 12;
						continue;
					case PdfLinearGradientMode.Vertical:
						empty = new RectangleF(A_1.Left, A_1.Top, A_1.Width, A_1.Height / 2f);
						empty2 = new RectangleF(A_1.Left, A_1.Top + A_1.Height / 2f, A_1.Width, A_1.Height / 2f);
						num = 6;
						continue;
					default:
						num = 5;
						continue;
					}
					break;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 10:
					switch (A_4)
					{
					case GradientVariantsType.ShadingVariants2:
						goto IL_29E;
					case GradientVariantsType.ShadingVariants3:
						empty = RectangleF.Empty;
						empty2 = RectangleF.Empty;
						num = 8;
						continue;
					default:
						num = 3;
						continue;
					}
					break;
				case 11:
					goto IL_1DE;
				case 12:
					goto IL_F2;
				}
				if (A_5 != PdfLinearGradientMode.Horizontal)
				{
					num = 9;
					continue;
				}
				num = 0;
				continue;
				IL_2BC:
				mode = pdfLinearGradientMode;
				num = 10;
			}
			IL_F2:
			if (true)
			{
			}
			IL_1DE:
			IL_261:
			IL_263:
			PdfBrush brush = new PdfLinearGradientBrush(empty, A_2, A_3, mode);
			A_0.DrawRectangle(brush, empty);
			brush = new PdfLinearGradientBrush(empty2, A_3, A_2, mode);
			A_0.DrawRectangle(brush, empty2);
			return;
			IL_29E:
			brush = new PdfLinearGradientBrush(A_1, A_3, A_2, mode);
			A_0.DrawRectangle(brush, A_1);
			return;
			IL_2FA:
			goto IL_263;
			IL_30B:
			brush = new PdfLinearGradientBrush(A_1, A_2, A_3, mode);
			A_0.DrawRectangle(brush, A_1);
			return;
		}
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00005278 File Offset: 0x00003478
	public void ᜀ(spr\u192F A_0, RectangleF A_1, PdfCanvas A_2)
	{
		switch (0)
		{
		default:
		{
			IGradient gradient;
			Color backColor;
			Color foreColor;
			for (;;)
			{
				gradient = A_0.ᝐ();
				backColor = gradient.BackColor;
				foreColor = gradient.ForeColor;
				GradientStyleType gradientStyle = gradient.GradientStyle;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (gradient.GradientStyle == GradientStyleType.Diagonl_Up)
						{
							num = 1;
							continue;
						}
						double num2;
						A_2.RotateTransform((float)num2);
						float num3;
						A_2.TranslateTransform(0f, -num3);
						RectangleF a_;
						this.ᜀ(A_2, a_, backColor, foreColor, gradient.GradientVariant, PdfLinearGradientMode.BackwardDiagonal);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_74;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					}
					case 1:
					{
						double num2;
						A_2.RotateTransform(-(float)num2);
						float num4;
						A_2.TranslateTransform(-num4, 0f);
						RectangleF a_;
						this.ᜀ(A_2, a_, backColor, foreColor, gradient.GradientVariant, PdfLinearGradientMode.ForwardDiagonal);
						num = 4;
						continue;
					}
					case 2:
						return;
					case 3:
						switch (gradientStyle)
						{
						case GradientStyleType.Horizontal:
							goto IL_1A5;
						case GradientStyleType.Vertical:
							goto IL_223;
						case GradientStyleType.Diagonl_Up:
						case GradientStyleType.Diagonl_Down:
						{
							double num2 = Math.Tanh((double)(A_1.Height / A_1.Width));
							double num5 = Math.Cos(num2);
							double num6 = Math.Sin(num2);
							float width = (float)((double)A_1.Width * num5 + (double)A_1.Height * num6);
							float height = (float)((double)A_1.Width * num6 + (double)A_1.Height * num5);
							float num4 = (float)((double)A_1.Height * num6);
							float num3 = (float)((double)A_1.Height * num5);
							RectangleF a_ = new RectangleF(0f, 0f, width, height);
							num2 = num2 / 3.141592653589793 * 180.0;
							A_2.Save();
							A_2.SetClip(A_1);
							A_2.TranslateTransform(A_1.X, A_1.Y);
							num = 0;
							continue;
						}
						case GradientStyleType.From_Corner:
						case GradientStyleType.From_Center:
							goto IL_235;
						}
						goto IL_74;
					case 4:
						goto IL_B7;
					case 5:
						goto IL_221;
					}
					break;
					IL_74:
					num = 2;
				}
			}
			return;
			IL_B7:
			goto IL_1B7;
			IL_1A5:
			this.ᜀ(A_2, A_1, backColor, foreColor, gradient.GradientVariant, PdfLinearGradientMode.Vertical);
			return;
			IL_1B7:
			A_2.Restore();
			return;
			IL_221:
			goto IL_1B7;
			IL_223:
			this.ᜀ(A_2, A_1, backColor, foreColor, gradient.GradientVariant, PdfLinearGradientMode.Horizontal);
			return;
			IL_235:
			this.ᜀ(A_2, A_1, gradient.GradientStyle, gradient.GradientVariant, backColor, foreColor);
			return;
		}
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000054D0 File Offset: 0x000036D0
	public void ᜀ(PdfCanvas A_0, RectangleF A_1, GradientStyleType A_2, GradientVariantsType A_3, Color A_4, Color A_5)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Color color = A_4;
				A_4 = A_5;
				A_5 = color;
				float num = 2f;
				float width = 1f;
				A_0.Save();
				A_0.SetClip(A_1);
				RectangleF rect = new RectangleF(A_1.Left - 1f, A_1.Top - 1f, A_1.Width + 2f, A_1.Height + 2f);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch (A_3)
						{
						case GradientVariantsType.ShadingVariants1:
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
								PointF location = rect.Location;
								PointF pointF = new PointF(rect.Right, rect.Top);
								PointF pointF2 = new PointF(rect.Right, rect.Bottom);
								PointF pointF3 = new PointF(rect.Left, rect.Bottom);
								PdfRadialGradientBrush brush = new PdfRadialGradientBrush(pointF2, num, location, num, A_4, A_5);
								A_0.DrawLine(new PdfPen(brush, width), pointF2, location);
								PdfLinearGradientBrush brush2 = new PdfLinearGradientBrush(rect, A_5, A_4, PdfLinearGradientMode.Horizontal);
								A_0.DrawPolygon(brush2, new PointF[]
								{
									location,
									pointF,
									pointF2
								});
								PdfLinearGradientBrush brush3 = new PdfLinearGradientBrush(rect, A_5, A_4, PdfLinearGradientMode.Vertical);
								A_0.DrawPolygon(brush3, new PointF[]
								{
									location,
									pointF2,
									pointF3
								});
								num2 = 3;
								continue;
							}
							}
							break;
						case GradientVariantsType.ShadingVariants2:
						{
							PointF location2 = rect.Location;
							PointF pointF4 = new PointF(rect.Right, rect.Top);
							PointF pointF5 = new PointF(rect.Right, rect.Bottom);
							PointF pointF6 = new PointF(rect.Left, rect.Bottom);
							PdfRadialGradientBrush brush4 = new PdfRadialGradientBrush(pointF6, num, pointF4, num, A_4, A_5);
							A_0.DrawLine(new PdfPen(brush4, width), pointF6, pointF4);
							PdfLinearGradientBrush brush5 = new PdfLinearGradientBrush(rect, A_4, A_5, PdfLinearGradientMode.Horizontal);
							A_0.DrawPolygon(brush5, new PointF[]
							{
								location2,
								pointF4,
								pointF6
							});
							PdfLinearGradientBrush brush6 = new PdfLinearGradientBrush(rect, A_5, A_4, PdfLinearGradientMode.Vertical);
							A_0.DrawPolygon(brush6, new PointF[]
							{
								pointF4,
								pointF5,
								pointF6
							});
							break;
						}
						case GradientVariantsType.ShadingVariants3:
						{
							if (true)
							{
							}
							PointF location3 = rect.Location;
							PointF pointF7 = new PointF(rect.Right, rect.Top);
							PointF pointF8 = new PointF(rect.Right, rect.Bottom);
							PointF pointF9 = new PointF(rect.Left, rect.Bottom);
							PdfRadialGradientBrush brush7 = new PdfRadialGradientBrush(pointF7, num, pointF9, num, A_4, A_5);
							A_0.DrawLine(new PdfPen(brush7, width), pointF7, pointF9);
							PdfLinearGradientBrush brush8 = new PdfLinearGradientBrush(rect, A_4, A_5, PdfLinearGradientMode.Vertical);
							A_0.DrawPolygon(brush8, new PointF[]
							{
								location3,
								pointF7,
								pointF9
							});
							PdfLinearGradientBrush brush9 = new PdfLinearGradientBrush(rect, A_5, A_4, PdfLinearGradientMode.Horizontal);
							A_0.DrawPolygon(brush9, new PointF[]
							{
								pointF7,
								pointF8,
								pointF9
							});
							num2 = 5;
							continue;
						}
						case GradientVariantsType.ShadingVariants4:
						{
							PointF location4 = rect.Location;
							PointF pointF10 = new PointF(rect.Right, rect.Top);
							PointF pointF11 = new PointF(rect.Right, rect.Bottom);
							PointF pointF12 = new PointF(rect.Left, rect.Bottom);
							PdfRadialGradientBrush brush10 = new PdfRadialGradientBrush(location4, num, pointF11, num, A_4, A_5);
							A_0.DrawLine(new PdfPen(brush10, width), location4, pointF11);
							PdfLinearGradientBrush brush11 = new PdfLinearGradientBrush(rect, A_4, A_5, PdfLinearGradientMode.Horizontal);
							A_0.DrawPolygon(brush11, new PointF[]
							{
								location4,
								pointF11,
								pointF12
							});
							PdfLinearGradientBrush brush12 = new PdfLinearGradientBrush(rect, A_4, A_5, PdfLinearGradientMode.Vertical);
							A_0.DrawPolygon(brush12, new PointF[]
							{
								location4,
								pointF11,
								pointF10
							});
							num2 = 8;
							continue;
						}
						default:
							num2 = 9;
							continue;
						}
						num2 = 6;
						continue;
					case 1:
						goto IL_7F1;
					case 2:
						if (A_2 == GradientStyleType.From_Center)
						{
							num2 = 4;
							continue;
						}
						num2 = 0;
						continue;
					case 3:
						goto IL_347;
					case 4:
					{
						RectangleF rect2 = new RectangleF(rect.Left, rect.Top, rect.Width / 2f, rect.Height);
						RectangleF rect3 = new RectangleF(rect.Left, rect.Top, rect.Width, rect.Height / 2f);
						RectangleF rect4 = new RectangleF(rect.Left + rect.Width / 2f, rect.Top, rect.Width / 2f, rect.Height);
						RectangleF rect5 = new RectangleF(rect.Left, rect.Top + rect.Height / 2f, rect.Width, rect.Height / 2f);
						PointF pointF13 = new PointF(rect4.Left, rect3.Bottom);
						PointF location5 = rect2.Location;
						PointF pointF14 = new PointF(rect3.Right, rect3.Top);
						PointF pointF15 = new PointF(rect4.Right, rect4.Bottom);
						PointF pointF16 = new PointF(rect5.Left, rect5.Bottom);
						PdfRadialGradientBrush brush13 = new PdfRadialGradientBrush(location5, num, pointF13, num, A_4, A_5);
						A_0.DrawLine(new PdfPen(brush13, width), location5, pointF13);
						PdfRadialGradientBrush brush14 = new PdfRadialGradientBrush(pointF14, num, pointF13, num, A_4, A_5);
						A_0.DrawLine(new PdfPen(brush14, width), pointF14, pointF13);
						PdfRadialGradientBrush brush15 = new PdfRadialGradientBrush(pointF15, num, pointF13, num, A_4, A_5);
						A_0.DrawLine(new PdfPen(brush15, width), pointF15, pointF13);
						PdfRadialGradientBrush brush16 = new PdfRadialGradientBrush(pointF16, num, pointF13, num, A_4, A_5);
						A_0.DrawLine(new PdfPen(brush16, width), pointF16, pointF13);
						PdfLinearGradientBrush brush17 = new PdfLinearGradientBrush(rect2, A_4, A_5, PdfLinearGradientMode.Horizontal);
						A_0.DrawPolygon(brush17, new PointF[]
						{
							pointF13,
							location5,
							pointF16
						});
						PdfLinearGradientBrush brush18 = new PdfLinearGradientBrush(rect3, A_4, A_5, PdfLinearGradientMode.Vertical);
						A_0.DrawPolygon(brush18, new PointF[]
						{
							pointF13,
							location5,
							pointF14
						});
						PdfLinearGradientBrush brush19 = new PdfLinearGradientBrush(rect4, A_5, A_4, PdfLinearGradientMode.Horizontal);
						A_0.DrawPolygon(brush19, new PointF[]
						{
							pointF13,
							pointF14,
							pointF15
						});
						PdfLinearGradientBrush brush20 = new PdfLinearGradientBrush(rect5, A_5, A_4, PdfLinearGradientMode.Vertical);
						A_0.DrawPolygon(brush20, new PointF[]
						{
							pointF13,
							pointF15,
							pointF16
						});
						num2 = 1;
						continue;
					}
					case 5:
						goto IL_92B;
					case 6:
						goto IL_1F9;
					case 7:
						goto IL_93C;
					case 8:
						goto IL_4B3;
					case 9:
						num2 = 7;
						continue;
					}
					break;
				}
			}
			IL_1F9:
			IL_347:
			IL_4B3:
			IL_7F1:
			IL_92B:
			IL_93C:
			A_0.Restore();
			return;
		}
	}

	// Token: 0x02000012 RID: 18
	private class ᜀ : RichTextBox
	{
		// Token: 0x06000067 RID: 103
		[DllImport("USER32.dll")]
		private static extern IntPtr SendMessage(IntPtr A_0, int A_1, IntPtr A_2, IntPtr A_3);

		// Token: 0x06000068 RID: 104 RVA: 0x00005E38 File Offset: 0x00004038
		public int ᜀ(Graphics A_0)
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
			sprᯏ.ᜀ.ᜀ ᜀ = default(sprᯏ.ᜀ.ᜀ);
			ᜀ.ᜁ = 0;
			ᜀ.ᜃ = 16777216;
			ᜀ.ᜀ = 0;
			ᜀ.ᜂ = 16777216;
			IntPtr hdc = A_0.GetHdc();
			sprᯏ.ᜀ.ᜃ ᜃ;
			ᜃ.ᜄ.ᜁ = this.TextLength;
			ᜃ.ᜄ.ᜀ = 0;
			ᜃ.ᜀ = hdc;
			ᜃ.ᜁ = hdc;
			ᜃ.ᜂ = ᜀ;
			ᜃ.ᜃ = ᜀ;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			zero = new IntPtr(1);
			IntPtr intPtr2 = IntPtr.Zero;
			intPtr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(ᜃ));
			Marshal.StructureToPtr(ᜃ, intPtr2, false);
			intPtr = sprᯏ.ᜀ.SendMessage(base.Handle, 1081, zero, intPtr2);
			Marshal.FreeCoTaskMem(intPtr2);
			A_0.ReleaseHdc(hdc);
			return intPtr.ToInt32();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005F50 File Offset: 0x00004150
		public static Metafile ᜀ(string A_0)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				Metafile result;
				lock (sprᯏ.ᜀ.ᜃ)
				{
					sprᯏ.ᜀ.ᜃ.Rtf = A_0;
					Bitmap bitmap = new Bitmap(1, 1);
					try
					{
						Graphics graphics = Graphics.FromImage(bitmap);
						try
						{
							IntPtr hdc = graphics.GetHdc();
							Metafile metafile = null;
							Metafile metafile2 = null;
							try
							{
								metafile = new Metafile(hdc, EmfType.EmfOnly);
								metafile2 = new Metafile(hdc, EmfType.EmfOnly);
							}
							finally
							{
								graphics.ReleaseHdc(hdc);
							}
							try
							{
								Graphics graphics2 = Graphics.FromImage(metafile);
								try
								{
									sprᯏ.ᜀ.ᜃ.ᜀ(graphics2);
								}
								finally
								{
									int num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
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
											((IDisposable)graphics2).Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_E8;
										}
										if (graphics2 == null)
										{
											break;
										}
										num = 0;
									}
									IL_E8:;
								}
								sprᯏ.ᜀ.ᜁ.ᜀ(metafile, metafile2);
								result = metafile2;
							}
							finally
							{
								metafile.Dispose();
							}
						}
						finally
						{
							int num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_13E;
								case 2:
									((IDisposable)graphics).Dispose();
									num = 0;
									continue;
								}
								if (graphics == null)
								{
									break;
								}
								num = 2;
							}
							IL_13E:;
						}
					}
					finally
					{
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								((IDisposable)bitmap).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_17C;
							}
							if (bitmap == null)
							{
								break;
							}
							num = 0;
						}
						IL_17C:;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006190 File Offset: 0x00004390
		// Note: this type is marked as 'beforefieldinit'.
		static ᜀ()
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
			sprᯏ.ᜀ.ᜃ = new sprᯏ.ᜀ();
		}

		// Token: 0x0400002B RID: 43
		private const double ᜀ = 14.4;

		// Token: 0x0400002C RID: 44
		private const int ᜁ = 1024;

		// Token: 0x0400002D RID: 45
		private const int ᜂ = 1081;

		// Token: 0x0400002E RID: 46
		private static readonly sprᯏ.ᜀ ᜃ;

		// Token: 0x02000013 RID: 19
		private struct ᜀ
		{
			// Token: 0x0400002F RID: 47
			public int ᜀ;

			// Token: 0x04000030 RID: 48
			public int ᜁ;

			// Token: 0x04000031 RID: 49
			public int ᜂ;

			// Token: 0x04000032 RID: 50
			public int ᜃ;
		}

		// Token: 0x02000014 RID: 20
		private struct ᜂ
		{
			// Token: 0x04000033 RID: 51
			public int ᜀ;

			// Token: 0x04000034 RID: 52
			public int ᜁ;
		}

		// Token: 0x02000015 RID: 21
		private struct ᜃ
		{
			// Token: 0x04000035 RID: 53
			public IntPtr ᜀ;

			// Token: 0x04000036 RID: 54
			public IntPtr ᜁ;

			// Token: 0x04000037 RID: 55
			public sprᯏ.ᜀ.ᜀ ᜂ;

			// Token: 0x04000038 RID: 56
			public sprᯏ.ᜀ.ᜀ ᜃ;

			// Token: 0x04000039 RID: 57
			public sprᯏ.ᜀ.ᜂ ᜄ;
		}

		// Token: 0x02000016 RID: 22
		private class ᜁ
		{
			// Token: 0x0600006C RID: 108 RVA: 0x000061D8 File Offset: 0x000043D8
			private ᜁ(Metafile A_0, Metafile A_1)
			{
				this.ᜀ = A_0;
				this.ᜁ = A_1;
			}

			// Token: 0x0600006D RID: 109 RVA: 0x000061FC File Offset: 0x000043FC
			private void ᜀ()
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					Graphics graphics = Graphics.FromImage(this.ᜁ);
					try
					{
						graphics.EnumerateMetafile(this.ᜀ, Point.Empty, new Graphics.EnumerateMetafileProc(this.ᜀ));
					}
					finally
					{
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_92;
							case 1:
								((IDisposable)graphics).Dispose();
								num = 0;
								continue;
							}
							if (graphics == null)
							{
								break;
							}
							num = 1;
						}
						IL_92:;
					}
					break;
				}
				}
			}

			// Token: 0x0600006E RID: 110 RVA: 0x000062B0 File Offset: 0x000044B0
			public static void ᜀ(Metafile A_0, Metafile A_1)
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
				sprᯏ.ᜀ.ᜁ ᜁ = new sprᯏ.ᜀ.ᜁ(A_0, A_1);
				ᜁ.ᜀ();
			}

			// Token: 0x0600006F RID: 111 RVA: 0x000062FC File Offset: 0x000044FC
			private bool ᜀ(EmfPlusRecordType A_0, int A_1, int A_2, IntPtr A_3, PlayRecordCallback A_4)
			{
				int num = 2;
				for (;;)
				{
					byte[] array;
					int num2;
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						if (A_0 == EmfPlusRecordType.EmfExtTextOutW)
						{
							goto IL_11B;
						}
						this.ᜀ.PlayRecord(A_0, A_1, A_2, array);
						num = 7;
						continue;
					case 3:
						if (A_0 != EmfPlusRecordType.EmfExtTextOutA)
						{
							num = 9;
							continue;
						}
						goto IL_6F;
					case 4:
						goto IL_6F;
					case 5:
						if (num2 != 0)
						{
							num = 6;
							continue;
						}
						return true;
					case 6:
						this.ᜀ.PlayRecord(A_0, A_1, A_2, array);
						num = 0;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11B;
						default:
							goto IL_FB;
						}
						break;
					case 8:
						array = new byte[A_2];
						Marshal.Copy(A_3, array, 0, A_2);
						num = 3;
						continue;
					case 9:
						num = 1;
						continue;
					}
					if (A_3 != IntPtr.Zero)
					{
						num = 8;
						continue;
					}
					break;
					IL_6F:
					num2 = BitConverter.ToInt32(array, 36);
					num = 5;
					continue;
					IL_11B:
					num = 4;
				}
				IL_6A:
				return true;
				IL_FB:
				if (false)
				{
				}
				if (true)
				{
				}
				return true;
			}

			// Token: 0x0400003A RID: 58
			private Metafile ᜀ;

			// Token: 0x0400003B RID: 59
			private Metafile ᜁ;
		}
	}
}
