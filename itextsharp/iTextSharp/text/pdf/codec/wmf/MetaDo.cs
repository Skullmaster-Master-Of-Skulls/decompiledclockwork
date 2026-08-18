using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec.wmf
{
	// Token: 0x020002D6 RID: 726
	public class MetaDo
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x0009FA08 File Offset: 0x0009EA08
		public MetaDo(Stream meta, PdfContentByte cb)
		{
			this.cb = cb;
			this.meta = new InputMeta(meta);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0009FA30 File Offset: 0x0009EA30
		public void ReadAll()
		{
			if (this.meta.ReadInt() != -1698247209)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("not.a.placeable.windows.metafile"));
			}
			this.meta.ReadWord();
			this.left = this.meta.ReadShort();
			this.top = this.meta.ReadShort();
			this.right = this.meta.ReadShort();
			this.bottom = this.meta.ReadShort();
			this.inch = this.meta.ReadWord();
			this.state.ScalingX = (float)(this.right - this.left) / (float)this.inch * 72f;
			this.state.ScalingY = (float)(this.bottom - this.top) / (float)this.inch * 72f;
			this.state.OffsetWx = this.left;
			this.state.OffsetWy = this.top;
			this.state.ExtentWx = this.right - this.left;
			this.state.ExtentWy = this.bottom - this.top;
			this.meta.ReadInt();
			this.meta.ReadWord();
			this.meta.Skip(18);
			this.cb.SetLineCap(1);
			this.cb.SetLineJoin(1);
			for (;;)
			{
				int length = this.meta.Length;
				int num = this.meta.ReadInt();
				if (num >= 3)
				{
					int num2 = this.meta.ReadWord();
					int num3 = num2;
					if (num3 <= 764)
					{
						if (num3 <= 295)
						{
							if (num3 <= 247)
							{
								if (num3 != 0)
								{
									if (num3 != 30)
									{
										if (num3 == 247)
										{
											goto IL_3A5;
										}
									}
									else
									{
										this.state.SaveState(this.cb);
									}
								}
							}
							else if (num3 != 258)
							{
								if (num3 != 262)
								{
									if (num3 == 295)
									{
										int index = this.meta.ReadShort();
										this.state.RestoreState(index, this.cb);
									}
								}
								else
								{
									this.state.PolyFillMode = this.meta.ReadWord();
								}
							}
							else
							{
								this.state.BackgroundMode = this.meta.ReadWord();
							}
						}
						else if (num3 <= 496)
						{
							switch (num3)
							{
							case 301:
							{
								int index2 = this.meta.ReadWord();
								this.state.SelectMetaObject(index2, this.cb);
								break;
							}
							case 302:
								this.state.TextAlign = this.meta.ReadWord();
								break;
							default:
								if (num3 == 322)
								{
									goto IL_3A5;
								}
								if (num3 == 496)
								{
									int index3 = this.meta.ReadWord();
									this.state.DeleteMetaObject(index3);
								}
								break;
							}
						}
						else if (num3 <= 524)
						{
							if (num3 != 513)
							{
								switch (num3)
								{
								case 521:
									this.state.CurrentTextColor = this.meta.ReadColor();
									break;
								case 523:
									this.state.OffsetWy = this.meta.ReadShort();
									this.state.OffsetWx = this.meta.ReadShort();
									break;
								case 524:
									this.state.ExtentWy = this.meta.ReadShort();
									this.state.ExtentWx = this.meta.ReadShort();
									break;
								}
							}
							else
							{
								this.state.CurrentBackgroundColor = this.meta.ReadColor();
							}
						}
						else
						{
							switch (num3)
							{
							case 531:
							{
								int y = this.meta.ReadShort();
								int x = this.meta.ReadShort();
								Point currentPoint = this.state.CurrentPoint;
								this.cb.MoveTo(this.state.TransformX(currentPoint.X), this.state.TransformY(currentPoint.Y));
								this.cb.LineTo(this.state.TransformX(x), this.state.TransformY(y));
								this.cb.Stroke();
								this.state.CurrentPoint = new Point(x, y);
								break;
							}
							case 532:
							{
								int y2 = this.meta.ReadShort();
								Point currentPoint2 = new Point(this.meta.ReadShort(), y2);
								this.state.CurrentPoint = currentPoint2;
								break;
							}
							default:
								switch (num3)
								{
								case 762:
								{
									MetaPen metaPen = new MetaPen();
									metaPen.Init(this.meta);
									this.state.AddMetaObject(metaPen);
									break;
								}
								case 763:
								{
									MetaFont metaFont = new MetaFont();
									metaFont.Init(this.meta);
									this.state.AddMetaObject(metaFont);
									break;
								}
								case 764:
								{
									MetaBrush metaBrush = new MetaBrush();
									metaBrush.Init(this.meta);
									this.state.AddMetaObject(metaBrush);
									break;
								}
								}
								break;
							}
						}
					}
					else if (num3 <= 1564)
					{
						if (num3 <= 1051)
						{
							switch (num3)
							{
							case 804:
								if (!this.IsNullStrokeFill(false))
								{
									int num4 = this.meta.ReadWord();
									int x2 = this.meta.ReadShort();
									int y3 = this.meta.ReadShort();
									this.cb.MoveTo(this.state.TransformX(x2), this.state.TransformY(y3));
									for (int i = 1; i < num4; i++)
									{
										int x3 = this.meta.ReadShort();
										int y4 = this.meta.ReadShort();
										this.cb.LineTo(this.state.TransformX(x3), this.state.TransformY(y4));
									}
									this.cb.LineTo(this.state.TransformX(x2), this.state.TransformY(y3));
									this.StrokeAndFill();
								}
								break;
							case 805:
							{
								this.state.LineJoinPolygon = this.cb;
								int num5 = this.meta.ReadWord();
								int x4 = this.meta.ReadShort();
								int y5 = this.meta.ReadShort();
								this.cb.MoveTo(this.state.TransformX(x4), this.state.TransformY(y5));
								for (int j = 1; j < num5; j++)
								{
									x4 = this.meta.ReadShort();
									y5 = this.meta.ReadShort();
									this.cb.LineTo(this.state.TransformX(x4), this.state.TransformY(y5));
								}
								this.cb.Stroke();
								break;
							}
							default:
								switch (num3)
								{
								case 1046:
								{
									float num6 = this.state.TransformY(this.meta.ReadShort());
									float num7 = this.state.TransformX(this.meta.ReadShort());
									float num8 = this.state.TransformY(this.meta.ReadShort());
									float num9 = this.state.TransformX(this.meta.ReadShort());
									this.cb.Rectangle(num9, num6, num7 - num9, num8 - num6);
									this.cb.EoClip();
									this.cb.NewPath();
									break;
								}
								case 1047:
									break;
								case 1048:
									if (!this.IsNullStrokeFill(this.state.LineNeutral))
									{
										int y6 = this.meta.ReadShort();
										int x5 = this.meta.ReadShort();
										int y7 = this.meta.ReadShort();
										int x6 = this.meta.ReadShort();
										this.cb.Arc(this.state.TransformX(x6), this.state.TransformY(y6), this.state.TransformX(x5), this.state.TransformY(y7), 0f, 360f);
										this.StrokeAndFill();
									}
									break;
								default:
									if (num3 == 1051)
									{
										if (!this.IsNullStrokeFill(true))
										{
											float num10 = this.state.TransformY(this.meta.ReadShort());
											float num11 = this.state.TransformX(this.meta.ReadShort());
											float num12 = this.state.TransformY(this.meta.ReadShort());
											float num13 = this.state.TransformX(this.meta.ReadShort());
											this.cb.Rectangle(num13, num10, num11 - num13, num12 - num10);
											this.StrokeAndFill();
										}
									}
									break;
								}
								break;
							}
						}
						else if (num3 <= 1313)
						{
							if (num3 != 1055)
							{
								if (num3 == 1313)
								{
									int num14 = this.meta.ReadWord();
									byte[] array = new byte[num14];
									int k;
									for (k = 0; k < num14; k++)
									{
										byte b = (byte)this.meta.ReadByte();
										if (b == 0)
										{
											break;
										}
										array[k] = b;
									}
									string @string;
									try
									{
										@string = Encoding.GetEncoding(1252).GetString(array, 0, k);
									}
									catch
									{
										@string = Encoding.ASCII.GetString(array, 0, k);
									}
									num14 = (num14 + 1 & 65534);
									this.meta.Skip(num14 - k);
									int y8 = this.meta.ReadShort();
									int x7 = this.meta.ReadShort();
									this.OutputText(x7, y8, 0, 0, 0, 0, 0, @string);
								}
							}
							else
							{
								BaseColor colorFill = this.meta.ReadColor();
								int y9 = this.meta.ReadShort();
								int x8 = this.meta.ReadShort();
								this.cb.SaveState();
								this.cb.SetColorFill(colorFill);
								this.cb.Rectangle(this.state.TransformX(x8), this.state.TransformY(y9), 0.2f, 0.2f);
								this.cb.Fill();
								this.cb.RestoreState();
							}
						}
						else if (num3 != 1336)
						{
							if (num3 == 1564)
							{
								if (!this.IsNullStrokeFill(true))
								{
									float num15 = this.state.TransformY(0) - this.state.TransformY(this.meta.ReadShort());
									float num16 = this.state.TransformX(this.meta.ReadShort()) - this.state.TransformX(0);
									float num17 = this.state.TransformY(this.meta.ReadShort());
									float num18 = this.state.TransformX(this.meta.ReadShort());
									float num19 = this.state.TransformY(this.meta.ReadShort());
									float num20 = this.state.TransformX(this.meta.ReadShort());
									this.cb.RoundRectangle(num20, num17, num18 - num20, num19 - num17, (num15 + num16) / 4f);
									this.StrokeAndFill();
								}
							}
						}
						else if (!this.IsNullStrokeFill(false))
						{
							int num21 = this.meta.ReadWord();
							int[] array2 = new int[num21];
							for (int l = 0; l < array2.Length; l++)
							{
								array2[l] = this.meta.ReadWord();
							}
							foreach (int num22 in array2)
							{
								int x9 = this.meta.ReadShort();
								int y10 = this.meta.ReadShort();
								this.cb.MoveTo(this.state.TransformX(x9), this.state.TransformY(y10));
								for (int n = 1; n < num22; n++)
								{
									int x10 = this.meta.ReadShort();
									int y11 = this.meta.ReadShort();
									this.cb.LineTo(this.state.TransformX(x10), this.state.TransformY(y11));
								}
								this.cb.LineTo(this.state.TransformX(x9), this.state.TransformY(y10));
							}
							this.StrokeAndFill();
						}
					}
					else if (num3 <= 2074)
					{
						if (num3 == 1791)
						{
							goto IL_3A5;
						}
						if (num3 != 2071)
						{
							if (num3 == 2074)
							{
								if (!this.IsNullStrokeFill(this.state.LineNeutral))
								{
									float yDot = this.state.TransformY(this.meta.ReadShort());
									float xDot = this.state.TransformX(this.meta.ReadShort());
									float yDot2 = this.state.TransformY(this.meta.ReadShort());
									float xDot2 = this.state.TransformX(this.meta.ReadShort());
									float num23 = this.state.TransformY(this.meta.ReadShort());
									float num24 = this.state.TransformX(this.meta.ReadShort());
									float num25 = this.state.TransformY(this.meta.ReadShort());
									float num26 = this.state.TransformX(this.meta.ReadShort());
									float num27 = (num24 + num26) / 2f;
									float num28 = (num25 + num23) / 2f;
									float arc = MetaDo.GetArc(num27, num28, xDot2, yDot2);
									float num29 = MetaDo.GetArc(num27, num28, xDot, yDot);
									num29 -= arc;
									if (num29 <= 0f)
									{
										num29 += 360f;
									}
									List<float[]> list = PdfContentByte.BezierArc(num26, num23, num24, num25, arc, num29);
									if (list.Count != 0)
									{
										float[] array3 = list[0];
										this.cb.MoveTo(num27, num28);
										this.cb.LineTo(array3[0], array3[1]);
										for (int num30 = 0; num30 < list.Count; num30++)
										{
											array3 = list[num30];
											this.cb.CurveTo(array3[2], array3[3], array3[4], array3[5], array3[6], array3[7]);
										}
										this.cb.LineTo(num27, num28);
										this.StrokeAndFill();
									}
								}
							}
						}
						else if (!this.IsNullStrokeFill(this.state.LineNeutral))
						{
							float yDot3 = this.state.TransformY(this.meta.ReadShort());
							float xDot3 = this.state.TransformX(this.meta.ReadShort());
							float yDot4 = this.state.TransformY(this.meta.ReadShort());
							float xDot4 = this.state.TransformX(this.meta.ReadShort());
							float num31 = this.state.TransformY(this.meta.ReadShort());
							float num32 = this.state.TransformX(this.meta.ReadShort());
							float num33 = this.state.TransformY(this.meta.ReadShort());
							float num34 = this.state.TransformX(this.meta.ReadShort());
							float xCenter = (num32 + num34) / 2f;
							float yCenter = (num33 + num31) / 2f;
							float arc2 = MetaDo.GetArc(xCenter, yCenter, xDot4, yDot4);
							float num35 = MetaDo.GetArc(xCenter, yCenter, xDot3, yDot3);
							num35 -= arc2;
							if (num35 <= 0f)
							{
								num35 += 360f;
							}
							this.cb.Arc(num34, num31, num32, num33, arc2, num35);
							this.cb.Stroke();
						}
					}
					else if (num3 <= 2610)
					{
						if (num3 != 2096)
						{
							if (num3 == 2610)
							{
								int y12 = this.meta.ReadShort();
								int x11 = this.meta.ReadShort();
								int num36 = this.meta.ReadWord();
								int num37 = this.meta.ReadWord();
								int x12 = 0;
								int y13 = 0;
								int x13 = 0;
								int y14 = 0;
								if ((num37 & 6) != 0)
								{
									x12 = this.meta.ReadShort();
									y13 = this.meta.ReadShort();
									x13 = this.meta.ReadShort();
									y14 = this.meta.ReadShort();
								}
								byte[] array4 = new byte[num36];
								int num38;
								for (num38 = 0; num38 < num36; num38++)
								{
									byte b2 = (byte)this.meta.ReadByte();
									if (b2 == 0)
									{
										break;
									}
									array4[num38] = b2;
								}
								string string2;
								try
								{
									string2 = Encoding.GetEncoding(1252).GetString(array4, 0, num38);
								}
								catch
								{
									string2 = Encoding.ASCII.GetString(array4, 0, num38);
								}
								this.OutputText(x11, y12, num37, x12, y13, x13, y14, string2);
							}
						}
						else if (!this.IsNullStrokeFill(this.state.LineNeutral))
						{
							float yDot5 = this.state.TransformY(this.meta.ReadShort());
							float xDot5 = this.state.TransformX(this.meta.ReadShort());
							float yDot6 = this.state.TransformY(this.meta.ReadShort());
							float xDot6 = this.state.TransformX(this.meta.ReadShort());
							float num39 = this.state.TransformY(this.meta.ReadShort());
							float num40 = this.state.TransformX(this.meta.ReadShort());
							float num41 = this.state.TransformY(this.meta.ReadShort());
							float num42 = this.state.TransformX(this.meta.ReadShort());
							float num43 = (num40 + num42) / 2f;
							float num44 = (num41 + num39) / 2f;
							float arc3 = MetaDo.GetArc(num43, num44, xDot6, yDot6);
							float num45 = MetaDo.GetArc(num43, num44, xDot5, yDot5);
							num45 -= arc3;
							if (num45 <= 0f)
							{
								num45 += 360f;
							}
							List<float[]> list2 = PdfContentByte.BezierArc(num42, num39, num40, num41, arc3, num45);
							if (list2.Count != 0)
							{
								float[] array5 = list2[0];
								num43 = array5[0];
								num44 = array5[1];
								this.cb.MoveTo(num43, num44);
								for (int num46 = 0; num46 < list2.Count; num46++)
								{
									array5 = list2[num46];
									this.cb.CurveTo(array5[2], array5[3], array5[4], array5[5], array5[6], array5[7]);
								}
								this.cb.LineTo(num43, num44);
								this.StrokeAndFill();
							}
						}
					}
					else if (num3 == 2881 || num3 == 3907)
					{
						this.meta.ReadInt();
						if (num2 == 3907)
						{
							this.meta.ReadWord();
						}
						int num47 = this.meta.ReadShort();
						int num48 = this.meta.ReadShort();
						int num49 = this.meta.ReadShort();
						int num50 = this.meta.ReadShort();
						float num51 = this.state.TransformY(this.meta.ReadShort()) - this.state.TransformY(0);
						float num52 = this.state.TransformX(this.meta.ReadShort()) - this.state.TransformX(0);
						float num53 = this.state.TransformY(this.meta.ReadShort());
						float num54 = this.state.TransformX(this.meta.ReadShort());
						byte[] array6 = new byte[num * 2 - (this.meta.Length - length)];
						for (int num55 = 0; num55 < array6.Length; num55++)
						{
							array6[num55] = (byte)this.meta.ReadByte();
						}
						try
						{
							MemoryStream isp = new MemoryStream(array6);
							Image image = BmpImage.GetImage(isp, true, array6.Length);
							this.cb.SaveState();
							this.cb.Rectangle(num54, num53, num52, num51);
							this.cb.Clip();
							this.cb.NewPath();
							image.ScaleAbsolute(num52 * image.Width / (float)num48, -num51 * image.Height / (float)num47);
							image.SetAbsolutePosition(num54 - num52 * (float)num50 / (float)num48, num53 + num51 * (float)num49 / (float)num47 - image.ScaledHeight);
							this.cb.AddImage(image);
							this.cb.RestoreState();
						}
						catch
						{
						}
					}
					IL_14A1:
					this.meta.Skip(num * 2 - (this.meta.Length - length));
					continue;
					IL_3A5:
					this.state.AddMetaObject(new MetaObject());
					goto IL_14A1;
				}
				break;
			}
			this.state.Cleanup(this.cb);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000A0F38 File Offset: 0x0009FF38
		public void OutputText(int x, int y, int flag, int x1, int y1, int x2, int y2, string text)
		{
			MetaFont currentFont = this.state.CurrentFont;
			float e = this.state.TransformX(x);
			float f = this.state.TransformY(y);
			float num = this.state.TransformAngle(currentFont.Angle);
			float num2 = (float)Math.Sin((double)num);
			float num3 = (float)Math.Cos((double)num);
			float fontSize = currentFont.GetFontSize(this.state);
			BaseFont font = currentFont.Font;
			int textAlign = this.state.TextAlign;
			float widthPoint = font.GetWidthPoint(text, fontSize);
			float x3 = 0f;
			float fontDescriptor = font.GetFontDescriptor(3, fontSize);
			float fontDescriptor2 = font.GetFontDescriptor(8, fontSize);
			this.cb.SaveState();
			this.cb.ConcatCTM(num3, num2, -num2, num3, e, f);
			if ((textAlign & MetaState.TA_CENTER) == MetaState.TA_CENTER)
			{
				x3 = -widthPoint / 2f;
			}
			else if ((textAlign & MetaState.TA_RIGHT) == MetaState.TA_RIGHT)
			{
				x3 = -widthPoint;
			}
			float num4;
			if ((textAlign & MetaState.TA_BASELINE) == MetaState.TA_BASELINE)
			{
				num4 = 0f;
			}
			else if ((textAlign & MetaState.TA_BOTTOM) == MetaState.TA_BOTTOM)
			{
				num4 = -fontDescriptor;
			}
			else
			{
				num4 = -fontDescriptor2;
			}
			BaseColor colorFill;
			if (this.state.BackgroundMode == MetaState.OPAQUE)
			{
				colorFill = this.state.CurrentBackgroundColor;
				this.cb.SetColorFill(colorFill);
				this.cb.Rectangle(x3, num4 + fontDescriptor, widthPoint, fontDescriptor2 - fontDescriptor);
				this.cb.Fill();
			}
			colorFill = this.state.CurrentTextColor;
			this.cb.SetColorFill(colorFill);
			this.cb.BeginText();
			this.cb.SetFontAndSize(font, fontSize);
			this.cb.SetTextMatrix(x3, num4);
			this.cb.ShowText(text);
			this.cb.EndText();
			if (currentFont.IsUnderline())
			{
				this.cb.Rectangle(x3, num4 - fontSize / 4f, widthPoint, fontSize / 15f);
				this.cb.Fill();
			}
			if (currentFont.IsStrikeout())
			{
				this.cb.Rectangle(x3, num4 + fontSize / 3f, widthPoint, fontSize / 15f);
				this.cb.Fill();
			}
			this.cb.RestoreState();
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000A118C File Offset: 0x000A018C
		public bool IsNullStrokeFill(bool isRectangle)
		{
			MetaPen currentPen = this.state.CurrentPen;
			MetaBrush currentBrush = this.state.CurrentBrush;
			bool flag = currentPen.Style == 5;
			int style = currentBrush.Style;
			bool flag2 = style == 0 || (style == 2 && this.state.BackgroundMode == MetaState.OPAQUE);
			bool result = flag && !flag2;
			if (!flag)
			{
				if (isRectangle)
				{
					this.state.LineJoinRectangle = this.cb;
				}
				else
				{
					this.state.LineJoinPolygon = this.cb;
				}
			}
			return result;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000A121C File Offset: 0x000A021C
		public void StrokeAndFill()
		{
			MetaPen currentPen = this.state.CurrentPen;
			MetaBrush currentBrush = this.state.CurrentBrush;
			int style = currentPen.Style;
			int style2 = currentBrush.Style;
			if (style == 5)
			{
				this.cb.ClosePath();
				if (this.state.PolyFillMode == MetaState.ALTERNATE)
				{
					this.cb.EoFill();
					return;
				}
				this.cb.Fill();
				return;
			}
			else
			{
				bool flag = style2 == 0 || (style2 == 2 && this.state.BackgroundMode == MetaState.OPAQUE);
				if (!flag)
				{
					this.cb.ClosePathStroke();
					return;
				}
				if (this.state.PolyFillMode == MetaState.ALTERNATE)
				{
					this.cb.ClosePathEoFillStroke();
					return;
				}
				this.cb.ClosePathFillStroke();
				return;
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000A12E4 File Offset: 0x000A02E4
		internal static float GetArc(float xCenter, float yCenter, float xDot, float yDot)
		{
			double num = Math.Atan2((double)(yDot - yCenter), (double)(xDot - xCenter));
			if (num < 0.0)
			{
				num += 6.283185307179586;
			}
			return (float)(num / 3.141592653589793 * 180.0);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000A1330 File Offset: 0x000A0330
		public static byte[] WrapBMP(Image image)
		{
			if (image.OriginalType != 4)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("only.bmp.can.be.wrapped.in.wmf"));
			}
			byte[] array;
			if (image.OriginalData == null)
			{
				Stream responseStream = WebRequest.Create(image.Url).GetResponse().GetResponseStream();
				MemoryStream memoryStream = new MemoryStream();
				int num;
				while ((num = responseStream.ReadByte()) != -1)
				{
					memoryStream.WriteByte((byte)num);
				}
				responseStream.Close();
				array = memoryStream.ToArray();
			}
			else
			{
				array = image.OriginalData;
			}
			int num2 = array.Length - 14 + 1 >> 1;
			MemoryStream memoryStream2 = new MemoryStream();
			MetaDo.WriteWord(memoryStream2, 1);
			MetaDo.WriteWord(memoryStream2, 9);
			MetaDo.WriteWord(memoryStream2, 768);
			MetaDo.WriteDWord(memoryStream2, 23 + (13 + num2) + 3);
			MetaDo.WriteWord(memoryStream2, 1);
			MetaDo.WriteDWord(memoryStream2, 14 + num2);
			MetaDo.WriteWord(memoryStream2, 0);
			MetaDo.WriteDWord(memoryStream2, 4);
			MetaDo.WriteWord(memoryStream2, 259);
			MetaDo.WriteWord(memoryStream2, 8);
			MetaDo.WriteDWord(memoryStream2, 5);
			MetaDo.WriteWord(memoryStream2, 523);
			MetaDo.WriteWord(memoryStream2, 0);
			MetaDo.WriteWord(memoryStream2, 0);
			MetaDo.WriteDWord(memoryStream2, 5);
			MetaDo.WriteWord(memoryStream2, 524);
			MetaDo.WriteWord(memoryStream2, (int)image.Height);
			MetaDo.WriteWord(memoryStream2, (int)image.Width);
			MetaDo.WriteDWord(memoryStream2, 13 + num2);
			MetaDo.WriteWord(memoryStream2, 2881);
			MetaDo.WriteDWord(memoryStream2, 13369376);
			MetaDo.WriteWord(memoryStream2, (int)image.Height);
			MetaDo.WriteWord(memoryStream2, (int)image.Width);
			MetaDo.WriteWord(memoryStream2, 0);
			MetaDo.WriteWord(memoryStream2, 0);
			MetaDo.WriteWord(memoryStream2, (int)image.Height);
			MetaDo.WriteWord(memoryStream2, (int)image.Width);
			MetaDo.WriteWord(memoryStream2, 0);
			MetaDo.WriteWord(memoryStream2, 0);
			memoryStream2.Write(array, 14, array.Length - 14);
			if ((array.Length & 1) == 1)
			{
				memoryStream2.WriteByte(0);
			}
			MetaDo.WriteDWord(memoryStream2, 3);
			MetaDo.WriteWord(memoryStream2, 0);
			memoryStream2.Close();
			return memoryStream2.ToArray();
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000A1530 File Offset: 0x000A0530
		public static void WriteWord(Stream os, int v)
		{
			os.WriteByte((byte)(v & 255));
			os.WriteByte((byte)(v >> 8 & 255));
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000A1550 File Offset: 0x000A0550
		public static void WriteDWord(Stream os, int v)
		{
			MetaDo.WriteWord(os, v & 65535);
			MetaDo.WriteWord(os, v >> 16 & 65535);
		}

		// Token: 0x040011FC RID: 4604
		public const int META_SETBKCOLOR = 513;

		// Token: 0x040011FD RID: 4605
		public const int META_SETBKMODE = 258;

		// Token: 0x040011FE RID: 4606
		public const int META_SETMAPMODE = 259;

		// Token: 0x040011FF RID: 4607
		public const int META_SETROP2 = 260;

		// Token: 0x04001200 RID: 4608
		public const int META_SETRELABS = 261;

		// Token: 0x04001201 RID: 4609
		public const int META_SETPOLYFILLMODE = 262;

		// Token: 0x04001202 RID: 4610
		public const int META_SETSTRETCHBLTMODE = 263;

		// Token: 0x04001203 RID: 4611
		public const int META_SETTEXTCHAREXTRA = 264;

		// Token: 0x04001204 RID: 4612
		public const int META_SETTEXTCOLOR = 521;

		// Token: 0x04001205 RID: 4613
		public const int META_SETTEXTJUSTIFICATION = 522;

		// Token: 0x04001206 RID: 4614
		public const int META_SETWINDOWORG = 523;

		// Token: 0x04001207 RID: 4615
		public const int META_SETWINDOWEXT = 524;

		// Token: 0x04001208 RID: 4616
		public const int META_SETVIEWPORTORG = 525;

		// Token: 0x04001209 RID: 4617
		public const int META_SETVIEWPORTEXT = 526;

		// Token: 0x0400120A RID: 4618
		public const int META_OFFSETWINDOWORG = 527;

		// Token: 0x0400120B RID: 4619
		public const int META_SCALEWINDOWEXT = 1040;

		// Token: 0x0400120C RID: 4620
		public const int META_OFFSETVIEWPORTORG = 529;

		// Token: 0x0400120D RID: 4621
		public const int META_SCALEVIEWPORTEXT = 1042;

		// Token: 0x0400120E RID: 4622
		public const int META_LINETO = 531;

		// Token: 0x0400120F RID: 4623
		public const int META_MOVETO = 532;

		// Token: 0x04001210 RID: 4624
		public const int META_EXCLUDECLIPRECT = 1045;

		// Token: 0x04001211 RID: 4625
		public const int META_INTERSECTCLIPRECT = 1046;

		// Token: 0x04001212 RID: 4626
		public const int META_ARC = 2071;

		// Token: 0x04001213 RID: 4627
		public const int META_ELLIPSE = 1048;

		// Token: 0x04001214 RID: 4628
		public const int META_FLOODFILL = 1049;

		// Token: 0x04001215 RID: 4629
		public const int META_PIE = 2074;

		// Token: 0x04001216 RID: 4630
		public const int META_RECTANGLE = 1051;

		// Token: 0x04001217 RID: 4631
		public const int META_ROUNDRECT = 1564;

		// Token: 0x04001218 RID: 4632
		public const int META_PATBLT = 1565;

		// Token: 0x04001219 RID: 4633
		public const int META_SAVEDC = 30;

		// Token: 0x0400121A RID: 4634
		public const int META_SETPIXEL = 1055;

		// Token: 0x0400121B RID: 4635
		public const int META_OFFSETCLIPRGN = 544;

		// Token: 0x0400121C RID: 4636
		public const int META_TEXTOUT = 1313;

		// Token: 0x0400121D RID: 4637
		public const int META_BITBLT = 2338;

		// Token: 0x0400121E RID: 4638
		public const int META_STRETCHBLT = 2851;

		// Token: 0x0400121F RID: 4639
		public const int META_POLYGON = 804;

		// Token: 0x04001220 RID: 4640
		public const int META_POLYLINE = 805;

		// Token: 0x04001221 RID: 4641
		public const int META_ESCAPE = 1574;

		// Token: 0x04001222 RID: 4642
		public const int META_RESTOREDC = 295;

		// Token: 0x04001223 RID: 4643
		public const int META_FILLREGION = 552;

		// Token: 0x04001224 RID: 4644
		public const int META_FRAMEREGION = 1065;

		// Token: 0x04001225 RID: 4645
		public const int META_INVERTREGION = 298;

		// Token: 0x04001226 RID: 4646
		public const int META_PAINTREGION = 299;

		// Token: 0x04001227 RID: 4647
		public const int META_SELECTCLIPREGION = 300;

		// Token: 0x04001228 RID: 4648
		public const int META_SELECTOBJECT = 301;

		// Token: 0x04001229 RID: 4649
		public const int META_SETTEXTALIGN = 302;

		// Token: 0x0400122A RID: 4650
		public const int META_CHORD = 2096;

		// Token: 0x0400122B RID: 4651
		public const int META_SETMAPPERFLAGS = 561;

		// Token: 0x0400122C RID: 4652
		public const int META_EXTTEXTOUT = 2610;

		// Token: 0x0400122D RID: 4653
		public const int META_SETDIBTODEV = 3379;

		// Token: 0x0400122E RID: 4654
		public const int META_SELECTPALETTE = 564;

		// Token: 0x0400122F RID: 4655
		public const int META_REALIZEPALETTE = 53;

		// Token: 0x04001230 RID: 4656
		public const int META_ANIMATEPALETTE = 1078;

		// Token: 0x04001231 RID: 4657
		public const int META_SETPALENTRIES = 55;

		// Token: 0x04001232 RID: 4658
		public const int META_POLYPOLYGON = 1336;

		// Token: 0x04001233 RID: 4659
		public const int META_RESIZEPALETTE = 313;

		// Token: 0x04001234 RID: 4660
		public const int META_DIBBITBLT = 2368;

		// Token: 0x04001235 RID: 4661
		public const int META_DIBSTRETCHBLT = 2881;

		// Token: 0x04001236 RID: 4662
		public const int META_DIBCREATEPATTERNBRUSH = 322;

		// Token: 0x04001237 RID: 4663
		public const int META_STRETCHDIB = 3907;

		// Token: 0x04001238 RID: 4664
		public const int META_EXTFLOODFILL = 1352;

		// Token: 0x04001239 RID: 4665
		public const int META_DELETEOBJECT = 496;

		// Token: 0x0400123A RID: 4666
		public const int META_CREATEPALETTE = 247;

		// Token: 0x0400123B RID: 4667
		public const int META_CREATEPATTERNBRUSH = 505;

		// Token: 0x0400123C RID: 4668
		public const int META_CREATEPENINDIRECT = 762;

		// Token: 0x0400123D RID: 4669
		public const int META_CREATEFONTINDIRECT = 763;

		// Token: 0x0400123E RID: 4670
		public const int META_CREATEBRUSHINDIRECT = 764;

		// Token: 0x0400123F RID: 4671
		public const int META_CREATEREGION = 1791;

		// Token: 0x04001240 RID: 4672
		public PdfContentByte cb;

		// Token: 0x04001241 RID: 4673
		public InputMeta meta;

		// Token: 0x04001242 RID: 4674
		private int left;

		// Token: 0x04001243 RID: 4675
		private int top;

		// Token: 0x04001244 RID: 4676
		private int right;

		// Token: 0x04001245 RID: 4677
		private int bottom;

		// Token: 0x04001246 RID: 4678
		private int inch;

		// Token: 0x04001247 RID: 4679
		private MetaState state = new MetaState();
	}
}
