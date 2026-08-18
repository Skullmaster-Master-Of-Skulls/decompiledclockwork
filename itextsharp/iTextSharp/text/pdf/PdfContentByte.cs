using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;
using iTextSharp.text.error_messages;
using iTextSharp.text.exceptions;
using iTextSharp.text.pdf.intern;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000159 RID: 345
	public class PdfContentByte
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x00043B70 File Offset: 0x00042B70
		static PdfContentByte()
		{
			PdfContentByte.abrev[PdfName.BITSPERCOMPONENT] = "/BPC ";
			PdfContentByte.abrev[PdfName.COLORSPACE] = "/CS ";
			PdfContentByte.abrev[PdfName.DECODE] = "/D ";
			PdfContentByte.abrev[PdfName.DECODEPARMS] = "/DP ";
			PdfContentByte.abrev[PdfName.FILTER] = "/F ";
			PdfContentByte.abrev[PdfName.HEIGHT] = "/H ";
			PdfContentByte.abrev[PdfName.IMAGEMASK] = "/IM ";
			PdfContentByte.abrev[PdfName.INTENT] = "/Intent ";
			PdfContentByte.abrev[PdfName.INTERPOLATE] = "/I ";
			PdfContentByte.abrev[PdfName.WIDTH] = "/W ";
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00043C68 File Offset: 0x00042C68
		public PdfContentByte(PdfWriter wr)
		{
			if (wr != null)
			{
				this.writer = wr;
				this.pdf = this.writer.PdfDocument;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00043CBF File Offset: 0x00042CBF
		public override string ToString()
		{
			return this.content.ToString();
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00043CCC File Offset: 0x00042CCC
		public ByteBuffer InternalBuffer
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00043CD4 File Offset: 0x00042CD4
		public byte[] ToPdf(PdfWriter writer)
		{
			this.SanityCheck();
			return this.content.ToByteArray();
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00043CE7 File Offset: 0x00042CE7
		public void Add(PdfContentByte other)
		{
			if (other.writer != null && this.writer != other.writer)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("inconsistent.writers.are.you.mixing.two.documents"));
			}
			this.content.Append(other.content);
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00043D21 File Offset: 0x00042D21
		public float XTLM
		{
			get
			{
				return this.state.xTLM;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00043D2E File Offset: 0x00042D2E
		public float YTLM
		{
			get
			{
				return this.state.yTLM;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00043D3B File Offset: 0x00042D3B
		public float CharacterSpacing
		{
			get
			{
				return this.state.charSpace;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00043D48 File Offset: 0x00042D48
		public float WordSpacing
		{
			get
			{
				return this.state.wordSpace;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00043D55 File Offset: 0x00042D55
		public float HorizontalScaling
		{
			get
			{
				return this.state.scale;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00043D62 File Offset: 0x00042D62
		public float Leading
		{
			get
			{
				return this.state.leading;
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00043D6F File Offset: 0x00042D6F
		public void SetLeading(float v)
		{
			this.state.leading = v;
			this.content.Append(v).Append(" TL").Append_i(this.separator);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00043D9F File Offset: 0x00042D9F
		public void SetFlatness(float value)
		{
			if (value >= 0f && value <= 100f)
			{
				this.content.Append(value).Append(" i").Append_i(this.separator);
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00043DD3 File Offset: 0x00042DD3
		public void SetLineCap(int value)
		{
			if (value >= 0 && value <= 2)
			{
				this.content.Append(value).Append(" J").Append_i(this.separator);
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00043DFF File Offset: 0x00042DFF
		public void SetLineDash(float value)
		{
			this.content.Append("[] ").Append(value).Append(" d").Append_i(this.separator);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00043E2D File Offset: 0x00042E2D
		public void SetLineDash(float unitsOn, float phase)
		{
			this.content.Append('[').Append(unitsOn).Append("] ").Append(phase).Append(" d").Append_i(this.separator);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00043E68 File Offset: 0x00042E68
		public void SetLineDash(float unitsOn, float unitsOff, float phase)
		{
			this.content.Append('[').Append(unitsOn).Append(' ').Append(unitsOff).Append("] ").Append(phase).Append(" d").Append_i(this.separator);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00043EBC File Offset: 0x00042EBC
		public void SetLineDash(float[] array, float phase)
		{
			this.content.Append('[');
			for (int i = 0; i < array.Length; i++)
			{
				this.content.Append(array[i]);
				if (i < array.Length - 1)
				{
					this.content.Append(' ');
				}
			}
			this.content.Append("] ").Append(phase).Append(" d").Append_i(this.separator);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00043F36 File Offset: 0x00042F36
		public void SetLineJoin(int value)
		{
			if (value >= 0 && value <= 2)
			{
				this.content.Append(value).Append(" j").Append_i(this.separator);
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00043F62 File Offset: 0x00042F62
		public void SetLineWidth(float value)
		{
			this.content.Append(value).Append(" w").Append_i(this.separator);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00043F86 File Offset: 0x00042F86
		public void SetMiterLimit(float value)
		{
			if (value > 1f)
			{
				this.content.Append(value).Append(" M").Append_i(this.separator);
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00043FB2 File Offset: 0x00042FB2
		public void Clip()
		{
			this.content.Append('W').Append_i(this.separator);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00043FCD File Offset: 0x00042FCD
		public void EoClip()
		{
			this.content.Append("W*").Append_i(this.separator);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00043FEB File Offset: 0x00042FEB
		public virtual void SetGrayFill(float value)
		{
			this.content.Append(value).Append(" g").Append_i(this.separator);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0004400F File Offset: 0x0004300F
		public virtual void ResetGrayFill()
		{
			this.content.Append("0 g").Append_i(this.separator);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0004402D File Offset: 0x0004302D
		public virtual void SetGrayStroke(float value)
		{
			this.content.Append(value).Append(" G").Append_i(this.separator);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00044051 File Offset: 0x00043051
		public virtual void ResetGrayStroke()
		{
			this.content.Append("0 G").Append_i(this.separator);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00044070 File Offset: 0x00043070
		private void HelperRGB(float red, float green, float blue)
		{
			PdfXConformanceImp.CheckPDFXConformance(this.writer, 3, null);
			if (red < 0f)
			{
				red = 0f;
			}
			else if (red > 1f)
			{
				red = 1f;
			}
			if (green < 0f)
			{
				green = 0f;
			}
			else if (green > 1f)
			{
				green = 1f;
			}
			if (blue < 0f)
			{
				blue = 0f;
			}
			else if (blue > 1f)
			{
				blue = 1f;
			}
			this.content.Append(red).Append(' ').Append(green).Append(' ').Append(blue);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00044111 File Offset: 0x00043111
		public virtual void SetRGBColorFillF(float red, float green, float blue)
		{
			this.HelperRGB(red, green, blue);
			this.content.Append(" rg").Append_i(this.separator);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00044138 File Offset: 0x00043138
		public virtual void ResetRGBColorFill()
		{
			this.content.Append("0 g").Append_i(this.separator);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00044156 File Offset: 0x00043156
		public virtual void SetRGBColorStrokeF(float red, float green, float blue)
		{
			this.HelperRGB(red, green, blue);
			this.content.Append(" RG").Append_i(this.separator);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0004417D File Offset: 0x0004317D
		public virtual void ResetRGBColorStroke()
		{
			this.content.Append("0 G").Append_i(this.separator);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0004419C File Offset: 0x0004319C
		private void HelperCMYK(float cyan, float magenta, float yellow, float black)
		{
			if (cyan < 0f)
			{
				cyan = 0f;
			}
			else if (cyan > 1f)
			{
				cyan = 1f;
			}
			if (magenta < 0f)
			{
				magenta = 0f;
			}
			else if (magenta > 1f)
			{
				magenta = 1f;
			}
			if (yellow < 0f)
			{
				yellow = 0f;
			}
			else if (yellow > 1f)
			{
				yellow = 1f;
			}
			if (black < 0f)
			{
				black = 0f;
			}
			else if (black > 1f)
			{
				black = 1f;
			}
			this.content.Append(cyan).Append(' ').Append(magenta).Append(' ').Append(yellow).Append(' ').Append(black);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00044260 File Offset: 0x00043260
		public virtual void SetCMYKColorFillF(float cyan, float magenta, float yellow, float black)
		{
			this.HelperCMYK(cyan, magenta, yellow, black);
			this.content.Append(" k").Append_i(this.separator);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00044289 File Offset: 0x00043289
		public virtual void ResetCMYKColorFill()
		{
			this.content.Append("0 0 0 1 k").Append_i(this.separator);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x000442A7 File Offset: 0x000432A7
		public virtual void SetCMYKColorStrokeF(float cyan, float magenta, float yellow, float black)
		{
			this.HelperCMYK(cyan, magenta, yellow, black);
			this.content.Append(" K").Append_i(this.separator);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x000442D0 File Offset: 0x000432D0
		public virtual void ResetCMYKColorStroke()
		{
			this.content.Append("0 0 0 1 K").Append_i(this.separator);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000442EE File Offset: 0x000432EE
		public void MoveTo(float x, float y)
		{
			this.content.Append(x).Append(' ').Append(y).Append(" m").Append_i(this.separator);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0004431F File Offset: 0x0004331F
		public void LineTo(float x, float y)
		{
			this.content.Append(x).Append(' ').Append(y).Append(" l").Append_i(this.separator);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00044350 File Offset: 0x00043350
		public void CurveTo(float x1, float y1, float x2, float y2, float x3, float y3)
		{
			this.content.Append(x1).Append(' ').Append(y1).Append(' ').Append(x2).Append(' ').Append(y2).Append(' ').Append(x3).Append(' ').Append(y3).Append(" c").Append_i(this.separator);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000443C4 File Offset: 0x000433C4
		public void CurveTo(float x2, float y2, float x3, float y3)
		{
			this.content.Append(x2).Append(' ').Append(y2).Append(' ').Append(x3).Append(' ').Append(y3).Append(" v").Append_i(this.separator);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0004441C File Offset: 0x0004341C
		public void CurveFromTo(float x1, float y1, float x3, float y3)
		{
			this.content.Append(x1).Append(' ').Append(y1).Append(' ').Append(x3).Append(' ').Append(y3).Append(" y").Append_i(this.separator);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00044474 File Offset: 0x00043474
		public void Circle(float x, float y, float r)
		{
			float num = 0.5523f;
			this.MoveTo(x + r, y);
			this.CurveTo(x + r, y + r * num, x + r * num, y + r, x, y + r);
			this.CurveTo(x - r * num, y + r, x - r, y + r * num, x - r, y);
			this.CurveTo(x - r, y - r * num, x - r * num, y - r, x, y - r);
			this.CurveTo(x + r * num, y - r, x + r, y - r * num, x + r, y);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000444FC File Offset: 0x000434FC
		public void Rectangle(float x, float y, float w, float h)
		{
			this.content.Append(x).Append(' ').Append(y).Append(' ').Append(w).Append(' ').Append(h).Append(" re").Append_i(this.separator);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00044553 File Offset: 0x00043553
		private bool CompareColors(BaseColor c1, BaseColor c2)
		{
			if (c1 == null && c2 == null)
			{
				return true;
			}
			if (c1 == null || c2 == null)
			{
				return false;
			}
			if (c1 is ExtendedColor)
			{
				return c1.Equals(c2);
			}
			return c2.Equals(c1);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0004457C File Offset: 0x0004357C
		public void VariableRectangle(Rectangle rect)
		{
			float top = rect.Top;
			float bottom = rect.Bottom;
			float right = rect.Right;
			float left = rect.Left;
			float borderWidthTop = rect.BorderWidthTop;
			float borderWidthBottom = rect.BorderWidthBottom;
			float borderWidthRight = rect.BorderWidthRight;
			float borderWidthLeft = rect.BorderWidthLeft;
			BaseColor borderColorTop = rect.BorderColorTop;
			BaseColor borderColorBottom = rect.BorderColorBottom;
			BaseColor borderColorRight = rect.BorderColorRight;
			BaseColor borderColorLeft = rect.BorderColorLeft;
			this.SaveState();
			this.SetLineCap(0);
			this.SetLineJoin(0);
			float num = 0f;
			bool flag = false;
			BaseColor c = null;
			bool flag2 = false;
			BaseColor c2 = null;
			if (borderWidthTop > 0f)
			{
				this.SetLineWidth(num = borderWidthTop);
				flag = true;
				if (borderColorTop == null)
				{
					this.ResetRGBColorStroke();
				}
				else
				{
					this.SetColorStroke(borderColorTop);
				}
				c = borderColorTop;
				this.MoveTo(left, top - borderWidthTop / 2f);
				this.LineTo(right, top - borderWidthTop / 2f);
				this.Stroke();
			}
			if (borderWidthBottom > 0f)
			{
				if (borderWidthBottom != num)
				{
					this.SetLineWidth(num = borderWidthBottom);
				}
				if (!flag || !this.CompareColors(c, borderColorBottom))
				{
					flag = true;
					if (borderColorBottom == null)
					{
						this.ResetRGBColorStroke();
					}
					else
					{
						this.SetColorStroke(borderColorBottom);
					}
					c = borderColorBottom;
				}
				this.MoveTo(right, bottom + borderWidthBottom / 2f);
				this.LineTo(left, bottom + borderWidthBottom / 2f);
				this.Stroke();
			}
			if (borderWidthRight > 0f)
			{
				if (borderWidthRight != num)
				{
					this.SetLineWidth(num = borderWidthRight);
				}
				if (!flag || !this.CompareColors(c, borderColorRight))
				{
					flag = true;
					if (borderColorRight == null)
					{
						this.ResetRGBColorStroke();
					}
					else
					{
						this.SetColorStroke(borderColorRight);
					}
					c = borderColorRight;
				}
				bool flag3 = this.CompareColors(borderColorTop, borderColorRight);
				bool flag4 = this.CompareColors(borderColorBottom, borderColorRight);
				this.MoveTo(right - borderWidthRight / 2f, flag3 ? top : (top - borderWidthTop));
				this.LineTo(right - borderWidthRight / 2f, flag4 ? bottom : (bottom + borderWidthBottom));
				this.Stroke();
				if (!flag3 || !flag4)
				{
					flag2 = true;
					if (borderColorRight == null)
					{
						this.ResetRGBColorFill();
					}
					else
					{
						this.SetColorFill(borderColorRight);
					}
					c2 = borderColorRight;
					if (!flag3)
					{
						this.MoveTo(right, top);
						this.LineTo(right, top - borderWidthTop);
						this.LineTo(right - borderWidthRight, top - borderWidthTop);
						this.Fill();
					}
					if (!flag4)
					{
						this.MoveTo(right, bottom);
						this.LineTo(right, bottom + borderWidthBottom);
						this.LineTo(right - borderWidthRight, bottom + borderWidthBottom);
						this.Fill();
					}
				}
			}
			if (borderWidthLeft > 0f)
			{
				if (borderWidthLeft != num)
				{
					this.SetLineWidth(borderWidthLeft);
				}
				if (!flag || !this.CompareColors(c, borderColorLeft))
				{
					if (borderColorLeft == null)
					{
						this.ResetRGBColorStroke();
					}
					else
					{
						this.SetColorStroke(borderColorLeft);
					}
				}
				bool flag5 = this.CompareColors(borderColorTop, borderColorLeft);
				bool flag6 = this.CompareColors(borderColorBottom, borderColorLeft);
				this.MoveTo(left + borderWidthLeft / 2f, flag5 ? top : (top - borderWidthTop));
				this.LineTo(left + borderWidthLeft / 2f, flag6 ? bottom : (bottom + borderWidthBottom));
				this.Stroke();
				if (!flag5 || !flag6)
				{
					if (!flag2 || !this.CompareColors(c2, borderColorLeft))
					{
						if (borderColorLeft == null)
						{
							this.ResetRGBColorFill();
						}
						else
						{
							this.SetColorFill(borderColorLeft);
						}
					}
					if (!flag5)
					{
						this.MoveTo(left, top);
						this.LineTo(left, top - borderWidthTop);
						this.LineTo(left + borderWidthLeft, top - borderWidthTop);
						this.Fill();
					}
					if (!flag6)
					{
						this.MoveTo(left, bottom);
						this.LineTo(left, bottom + borderWidthBottom);
						this.LineTo(left + borderWidthLeft, bottom + borderWidthBottom);
						this.Fill();
					}
				}
			}
			this.RestoreState();
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00044900 File Offset: 0x00043900
		public void Rectangle(Rectangle rectangle)
		{
			float left = rectangle.Left;
			float bottom = rectangle.Bottom;
			float right = rectangle.Right;
			float top = rectangle.Top;
			BaseColor backgroundColor = rectangle.BackgroundColor;
			if (backgroundColor != null)
			{
				this.SaveState();
				this.SetColorFill(backgroundColor);
				this.Rectangle(left, bottom, right - left, top - bottom);
				this.Fill();
				this.RestoreState();
			}
			if (!rectangle.HasBorders())
			{
				return;
			}
			if (rectangle.UseVariableBorders)
			{
				this.VariableRectangle(rectangle);
				return;
			}
			if (rectangle.BorderWidth != -1f)
			{
				this.SetLineWidth(rectangle.BorderWidth);
			}
			BaseColor borderColor = rectangle.BorderColor;
			if (borderColor != null)
			{
				this.SetColorStroke(borderColor);
			}
			if (rectangle.HasBorder(15))
			{
				this.Rectangle(left, bottom, right - left, top - bottom);
			}
			else
			{
				if (rectangle.HasBorder(8))
				{
					this.MoveTo(right, bottom);
					this.LineTo(right, top);
				}
				if (rectangle.HasBorder(4))
				{
					this.MoveTo(left, bottom);
					this.LineTo(left, top);
				}
				if (rectangle.HasBorder(2))
				{
					this.MoveTo(left, bottom);
					this.LineTo(right, bottom);
				}
				if (rectangle.HasBorder(1))
				{
					this.MoveTo(left, top);
					this.LineTo(right, top);
				}
			}
			this.Stroke();
			if (borderColor != null)
			{
				this.ResetRGBColorStroke();
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00044A31 File Offset: 0x00043A31
		public void ClosePath()
		{
			this.content.Append('h').Append_i(this.separator);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00044A4C File Offset: 0x00043A4C
		public void NewPath()
		{
			this.content.Append('n').Append_i(this.separator);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00044A67 File Offset: 0x00043A67
		public void Stroke()
		{
			this.content.Append('S').Append_i(this.separator);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00044A82 File Offset: 0x00043A82
		public void ClosePathStroke()
		{
			this.content.Append('s').Append_i(this.separator);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00044A9D File Offset: 0x00043A9D
		public void Fill()
		{
			this.content.Append('f').Append_i(this.separator);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00044AB8 File Offset: 0x00043AB8
		public void EoFill()
		{
			this.content.Append("f*").Append_i(this.separator);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00044AD6 File Offset: 0x00043AD6
		public void FillStroke()
		{
			this.content.Append('B').Append_i(this.separator);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00044AF1 File Offset: 0x00043AF1
		public void ClosePathFillStroke()
		{
			this.content.Append('b').Append_i(this.separator);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00044B0C File Offset: 0x00043B0C
		public void EoFillStroke()
		{
			this.content.Append("B*").Append_i(this.separator);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00044B2A File Offset: 0x00043B2A
		public void ClosePathEoFillStroke()
		{
			this.content.Append("b*").Append_i(this.separator);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00044B48 File Offset: 0x00043B48
		public virtual void AddImage(Image image)
		{
			this.AddImage(image, false);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00044B54 File Offset: 0x00043B54
		public virtual void AddImage(Image image, bool inlineImage)
		{
			if (!image.HasAbsolutePosition())
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("the.image.must.have.absolute.positioning"));
			}
			float[] matrix = image.Matrix;
			matrix[4] = image.AbsoluteX - matrix[4];
			matrix[5] = image.AbsoluteY - matrix[5];
			this.AddImage(image, matrix[0], matrix[1], matrix[2], matrix[3], matrix[4], matrix[5], inlineImage);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00044BB4 File Offset: 0x00043BB4
		public virtual void AddImage(Image image, float a, float b, float c, float d, float e, float f)
		{
			this.AddImage(image, a, b, c, d, e, f, false);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00044BD4 File Offset: 0x00043BD4
		public void AddImage(Image image, Matrix transform)
		{
			float[] elements = transform.Elements;
			this.AddImage(image, elements[0], elements[1], elements[2], elements[3], elements[4], elements[5], false);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00044C04 File Offset: 0x00043C04
		public virtual void AddImage(Image image, float a, float b, float c, float d, float e, float f, bool inlineImage)
		{
			if (image.Layer != null)
			{
				this.BeginLayer(image.Layer);
			}
			if (image.IsImgTemplate())
			{
				this.writer.AddDirectImageSimple(image);
				PdfTemplate templateData = image.TemplateData;
				float width = templateData.Width;
				float height = templateData.Height;
				this.AddTemplate(templateData, a / width, b / width, c / height, d / height, e, f);
			}
			else
			{
				this.content.Append("q ");
				this.content.Append(a).Append(' ');
				this.content.Append(b).Append(' ');
				this.content.Append(c).Append(' ');
				this.content.Append(d).Append(' ');
				this.content.Append(e).Append(' ');
				this.content.Append(f).Append(" cm");
				if (inlineImage)
				{
					this.content.Append("\nBI\n");
					PdfImage pdfImage = new PdfImage(image, "", null);
					if (image is ImgJBIG2)
					{
						byte[] globalBytes = ((ImgJBIG2)image).GlobalBytes;
						if (globalBytes != null)
						{
							PdfDictionary pdfDictionary = new PdfDictionary();
							pdfDictionary.Put(PdfName.JBIG2GLOBALS, this.writer.GetReferenceJBIG2Globals(globalBytes));
							pdfImage.Put(PdfName.DECODEPARMS, pdfDictionary);
						}
					}
					foreach (PdfName pdfName in pdfImage.Keys)
					{
						if (PdfContentByte.abrev.ContainsKey(pdfName))
						{
							PdfObject pdfObject = pdfImage.Get(pdfName);
							string str = PdfContentByte.abrev[pdfName];
							this.content.Append(str);
							bool flag = true;
							if (pdfName.Equals(PdfName.COLORSPACE) && pdfObject.IsArray())
							{
								PdfArray pdfArray = (PdfArray)pdfObject;
								if (pdfArray.Size == 4 && PdfName.INDEXED.Equals(pdfArray.GetAsName(0)) && pdfArray[1].IsName() && pdfArray[2].IsNumber() && pdfArray[3].IsString())
								{
									flag = false;
								}
							}
							if (flag && pdfName.Equals(PdfName.COLORSPACE) && !pdfObject.IsName())
							{
								PdfName colorspaceName = this.writer.GetColorspaceName();
								PageResources pageResources = this.PageResources;
								pageResources.AddColor(colorspaceName, this.writer.AddToBody(pdfObject).IndirectReference);
								pdfObject = colorspaceName;
							}
							pdfObject.ToPdf(null, this.content);
							this.content.Append('\n');
						}
					}
					this.content.Append("ID\n");
					pdfImage.WriteContent(this.content);
					this.content.Append("\nEI\nQ").Append_i(this.separator);
				}
				else
				{
					PageResources pageResources2 = this.PageResources;
					Image imageMask = image.ImageMask;
					PdfName pdfName2;
					if (imageMask != null)
					{
						pdfName2 = this.writer.AddDirectImageSimple(imageMask);
						pageResources2.AddXObject(pdfName2, this.writer.GetImageReference(pdfName2));
					}
					pdfName2 = this.writer.AddDirectImageSimple(image);
					pdfName2 = pageResources2.AddXObject(pdfName2, this.writer.GetImageReference(pdfName2));
					this.content.Append(' ').Append(pdfName2.GetBytes()).Append(" Do Q").Append_i(this.separator);
				}
			}
			if (image.HasBorders())
			{
				this.SaveState();
				float width2 = image.Width;
				float height2 = image.Height;
				this.ConcatCTM(a / width2, b / width2, c / height2, d / height2, e, f);
				this.Rectangle(image);
				this.RestoreState();
			}
			if (image.Layer != null)
			{
				this.EndLayer();
			}
			Annotation annotation = image.Annotation;
			if (annotation == null)
			{
				return;
			}
			float[] array = new float[PdfContentByte.unitRect.Length];
			for (int i = 0; i < PdfContentByte.unitRect.Length; i += 2)
			{
				array[i] = a * PdfContentByte.unitRect[i] + c * PdfContentByte.unitRect[i + 1] + e;
				array[i + 1] = b * PdfContentByte.unitRect[i] + d * PdfContentByte.unitRect[i + 1] + f;
			}
			float num = array[0];
			float num2 = array[1];
			float num3 = num;
			float num4 = num2;
			for (int j = 2; j < array.Length; j += 2)
			{
				num = Math.Min(num, array[j]);
				num2 = Math.Min(num2, array[j + 1]);
				num3 = Math.Max(num3, array[j]);
				num4 = Math.Max(num4, array[j + 1]);
			}
			annotation = new Annotation(annotation);
			annotation.SetDimensions(num, num2, num3, num4);
			PdfAnnotation pdfAnnotation = PdfAnnotationsImp.ConvertAnnotation(this.writer, annotation, new Rectangle(num, num2, num3, num4));
			if (pdfAnnotation == null)
			{
				return;
			}
			this.AddAnnotation(pdfAnnotation);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00045108 File Offset: 0x00044108
		public void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00045111 File Offset: 0x00044111
		public void Reset(bool validateContent)
		{
			this.content.Reset();
			if (validateContent)
			{
				this.SanityCheck();
			}
			this.state = new PdfContentByte.GraphicState();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00045134 File Offset: 0x00044134
		public void BeginText()
		{
			if (this.inText)
			{
				throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.begin.end.text.operators"));
			}
			this.inText = true;
			this.state.xTLM = 0f;
			this.state.yTLM = 0f;
			this.content.Append("BT").Append_i(this.separator);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0004519C File Offset: 0x0004419C
		public void EndText()
		{
			if (!this.inText)
			{
				throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.begin.end.text.operators"));
			}
			this.inText = false;
			this.content.Append("ET").Append_i(this.separator);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000451D9 File Offset: 0x000441D9
		public void SaveState()
		{
			this.content.Append('q').Append_i(this.separator);
			this.stateList.Add(new PdfContentByte.GraphicState(this.state));
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0004520C File Offset: 0x0004420C
		public void RestoreState()
		{
			this.content.Append('Q').Append_i(this.separator);
			int num = this.stateList.Count - 1;
			if (num < 0)
			{
				throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.save.restore.state.operators"));
			}
			this.state = this.stateList[num];
			this.stateList.RemoveAt(num);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00045272 File Offset: 0x00044272
		public void SetCharacterSpacing(float value)
		{
			this.state.charSpace = value;
			this.content.Append(value).Append(" Tc").Append_i(this.separator);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000452A2 File Offset: 0x000442A2
		public void SetWordSpacing(float value)
		{
			this.state.wordSpace = value;
			this.content.Append(value).Append(" Tw").Append_i(this.separator);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000452D2 File Offset: 0x000442D2
		public void SetHorizontalScaling(float value)
		{
			this.state.scale = value;
			this.content.Append(value).Append(" Tz").Append_i(this.separator);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00045304 File Offset: 0x00044304
		public virtual void SetFontAndSize(BaseFont bf, float size)
		{
			this.CheckWriter();
			if (size < 0.0001f && size > -0.0001f)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("font.size.too.small.1", size));
			}
			this.state.size = size;
			this.state.fontDetails = this.writer.AddSimple(bf);
			PageResources pageResources = this.PageResources;
			PdfName pdfName = this.state.fontDetails.FontName;
			pdfName = pageResources.AddFont(pdfName, this.state.fontDetails.IndirectReference);
			this.content.Append(pdfName.GetBytes()).Append(' ').Append(size).Append(" Tf").Append_i(this.separator);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000453C4 File Offset: 0x000443C4
		public void SetTextRenderingMode(int value)
		{
			this.content.Append(value).Append(" Tr").Append_i(this.separator);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000453E8 File Offset: 0x000443E8
		public void SetTextRise(float value)
		{
			this.content.Append(value).Append(" Ts").Append_i(this.separator);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0004540C File Offset: 0x0004440C
		private void ShowText2(string text)
		{
			if (this.state.fontDetails == null)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("font.and.size.must.be.set.before.writing.any.text"));
			}
			byte[] b = this.state.fontDetails.ConvertToBytes(text);
			PdfContentByte.EscapeString(b, this.content);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00045454 File Offset: 0x00044454
		public void ShowText(string text)
		{
			this.ShowText2(text);
			this.content.Append("Tj").Append_i(this.separator);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0004547C File Offset: 0x0004447C
		public static PdfTextArray GetKernArray(string text, BaseFont font)
		{
			PdfTextArray pdfTextArray = new PdfTextArray();
			StringBuilder stringBuilder = new StringBuilder();
			int num = text.Length - 1;
			char[] array = text.ToCharArray();
			if (num >= 0)
			{
				stringBuilder.Append(array, 0, 1);
			}
			for (int i = 0; i < num; i++)
			{
				char c = array[i + 1];
				int kerning = font.GetKerning((int)array[i], (int)c);
				if (kerning == 0)
				{
					stringBuilder.Append(c);
				}
				else
				{
					pdfTextArray.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
					stringBuilder.Append(array, i + 1, 1);
					pdfTextArray.Add((float)(-(float)kerning));
				}
			}
			pdfTextArray.Add(stringBuilder.ToString());
			return pdfTextArray;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00045520 File Offset: 0x00044520
		public void ShowTextKerned(string text)
		{
			if (this.state.fontDetails == null)
			{
				throw new ArgumentNullException(MessageLocalization.GetComposedMessage("font.and.size.must.be.set.before.writing.any.text"));
			}
			BaseFont baseFont = this.state.fontDetails.BaseFont;
			if (baseFont.HasKernPairs())
			{
				this.ShowText(PdfContentByte.GetKernArray(text, baseFont));
				return;
			}
			this.ShowText(text);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00045578 File Offset: 0x00044578
		public void NewlineShowText(string text)
		{
			this.state.yTLM -= this.state.leading;
			this.ShowText2(text);
			this.content.Append('\'').Append_i(this.separator);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000455B8 File Offset: 0x000445B8
		public void NewlineShowText(float wordSpacing, float charSpacing, string text)
		{
			this.state.yTLM -= this.state.leading;
			this.content.Append(wordSpacing).Append(' ').Append(charSpacing);
			this.ShowText2(text);
			this.content.Append("\"").Append_i(this.separator);
			this.state.charSpace = charSpacing;
			this.state.wordSpace = wordSpacing;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00045638 File Offset: 0x00044638
		public void SetTextMatrix(float a, float b, float c, float d, float x, float y)
		{
			this.state.xTLM = x;
			this.state.yTLM = y;
			this.content.Append(a).Append(' ').Append(b).Append_i(32).Append(c).Append_i(32).Append(d).Append_i(32).Append(x).Append_i(32).Append(y).Append(" Tm").Append_i(this.separator);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000456C8 File Offset: 0x000446C8
		public void SetTextMatrix(Matrix transform)
		{
			float[] elements = transform.Elements;
			this.SetTextMatrix(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000456F4 File Offset: 0x000446F4
		public void SetTextMatrix(float x, float y)
		{
			this.SetTextMatrix(1f, 0f, 0f, 1f, x, y);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00045714 File Offset: 0x00044714
		public void MoveText(float x, float y)
		{
			this.state.xTLM += x;
			this.state.yTLM += y;
			this.content.Append(x).Append(' ').Append(y).Append(" Td").Append_i(this.separator);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00045778 File Offset: 0x00044778
		public void MoveTextWithLeading(float x, float y)
		{
			this.state.xTLM += x;
			this.state.yTLM += y;
			this.state.leading = -y;
			this.content.Append(x).Append(' ').Append(y).Append(" TD").Append_i(this.separator);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000457E7 File Offset: 0x000447E7
		public void NewlineText()
		{
			this.state.yTLM -= this.state.leading;
			this.content.Append("T*").Append_i(this.separator);
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00045822 File Offset: 0x00044822
		internal int Size
		{
			get
			{
				return this.content.Size;
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00045830 File Offset: 0x00044830
		internal static byte[] EscapeString(byte[] b)
		{
			ByteBuffer byteBuffer = new ByteBuffer();
			PdfContentByte.EscapeString(b, byteBuffer);
			return byteBuffer.ToByteArray();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00045850 File Offset: 0x00044850
		internal static void EscapeString(byte[] b, ByteBuffer content)
		{
			content.Append_i(40);
			int i = 0;
			while (i < b.Length)
			{
				byte b2 = b[i];
				int num = (int)b2;
				switch (num)
				{
				case 8:
					content.Append("\\b");
					break;
				case 9:
					content.Append("\\t");
					break;
				case 10:
					content.Append("\\n");
					break;
				case 11:
					goto IL_A5;
				case 12:
					content.Append("\\f");
					break;
				case 13:
					content.Append("\\r");
					break;
				default:
					switch (num)
					{
					case 40:
					case 41:
						break;
					default:
						if (num != 92)
						{
							goto IL_A5;
						}
						break;
					}
					content.Append_i(92).Append_i((int)b2);
					break;
				}
				IL_AD:
				i++;
				continue;
				IL_A5:
				content.Append_i((int)b2);
				goto IL_AD;
			}
			content.Append(')');
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00045920 File Offset: 0x00044920
		public void AddOutline(PdfOutline outline, string name)
		{
			this.CheckWriter();
			this.pdf.AddOutline(outline, name);
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00045935 File Offset: 0x00044935
		public PdfOutline RootOutline
		{
			get
			{
				this.CheckWriter();
				return this.pdf.RootOutline;
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00045948 File Offset: 0x00044948
		public float GetEffectiveStringWidth(string text, bool kerned)
		{
			BaseFont baseFont = this.state.fontDetails.BaseFont;
			float num;
			if (kerned)
			{
				num = baseFont.GetWidthPointKerned(text, this.state.size);
			}
			else
			{
				num = baseFont.GetWidthPoint(text, this.state.size);
			}
			if (this.state.charSpace != 0f && text.Length > 1)
			{
				num += this.state.charSpace * (float)(text.Length - 1);
			}
			int fontType = baseFont.FontType;
			if (this.state.wordSpace != 0f && (fontType == 0 || fontType == 1 || fontType == 5))
			{
				for (int i = 0; i < text.Length - 1; i++)
				{
					if (text[i] == ' ')
					{
						num += this.state.wordSpace;
					}
				}
			}
			if ((double)this.state.scale != 100.0)
			{
				num = num * this.state.scale / 100f;
			}
			return num;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00045A40 File Offset: 0x00044A40
		public void ShowTextAligned(int alignment, string text, float x, float y, float rotation)
		{
			this.ShowTextAligned(alignment, text, x, y, rotation, false);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00045A50 File Offset: 0x00044A50
		private void ShowTextAligned(int alignment, string text, float x, float y, float rotation, bool kerned)
		{
			if (this.state.fontDetails == null)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("font.and.size.must.be.set.before.writing.any.text"));
			}
			if (rotation != 0f)
			{
				double num = (double)rotation * 3.141592653589793 / 180.0;
				float num2 = (float)Math.Cos(num);
				float num3 = (float)Math.Sin(num);
				switch (alignment)
				{
				case 1:
				{
					float num4 = this.GetEffectiveStringWidth(text, kerned) / 2f;
					x -= num4 * num2;
					y -= num4 * num3;
					break;
				}
				case 2:
				{
					float num4 = this.GetEffectiveStringWidth(text, kerned);
					x -= num4 * num2;
					y -= num4 * num3;
					break;
				}
				}
				this.SetTextMatrix(num2, num3, -num3, num2, x, y);
				if (kerned)
				{
					this.ShowTextKerned(text);
				}
				else
				{
					this.ShowText(text);
				}
				this.SetTextMatrix(0f, 0f);
				return;
			}
			switch (alignment)
			{
			case 1:
				x -= this.GetEffectiveStringWidth(text, kerned) / 2f;
				break;
			case 2:
				x -= this.GetEffectiveStringWidth(text, kerned);
				break;
			}
			this.SetTextMatrix(x, y);
			if (kerned)
			{
				this.ShowTextKerned(text);
				return;
			}
			this.ShowText(text);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00045B82 File Offset: 0x00044B82
		public void ShowTextAlignedKerned(int alignment, string text, float x, float y, float rotation)
		{
			this.ShowTextAligned(alignment, text, x, y, rotation, true);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00045B94 File Offset: 0x00044B94
		public void ConcatCTM(float a, float b, float c, float d, float e, float f)
		{
			this.content.Append(a).Append(' ').Append(b).Append(' ').Append(c).Append(' ');
			this.content.Append(d).Append(' ').Append(e).Append(' ').Append(f).Append(" cm").Append_i(this.separator);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00045C0E File Offset: 0x00044C0E
		public void ConcatCTM(Matrix transform)
		{
			this.Transform(transform);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00045C18 File Offset: 0x00044C18
		public static List<float[]> BezierArc(float x1, float y1, float x2, float y2, float startAng, float extent)
		{
			if (x1 > x2)
			{
				float num = x1;
				x1 = x2;
				x2 = num;
			}
			if (y2 > y1)
			{
				float num = y1;
				y1 = y2;
				y2 = num;
			}
			float num2;
			int num3;
			if (Math.Abs(extent) <= 90f)
			{
				num2 = extent;
				num3 = 1;
			}
			else
			{
				num3 = (int)Math.Ceiling((double)(Math.Abs(extent) / 90f));
				num2 = extent / (float)num3;
			}
			float num4 = (x1 + x2) / 2f;
			float num5 = (y1 + y2) / 2f;
			float num6 = (x2 - x1) / 2f;
			float num7 = (y2 - y1) / 2f;
			float num8 = (float)((double)num2 * 3.141592653589793 / 360.0);
			float num9 = (float)Math.Abs(1.3333333333333333 * (1.0 - Math.Cos((double)num8)) / Math.Sin((double)num8));
			List<float[]> list = new List<float[]>();
			for (int i = 0; i < num3; i++)
			{
				float num10 = (float)((double)(startAng + (float)i * num2) * 3.141592653589793 / 180.0);
				float num11 = (float)((double)(startAng + (float)(i + 1) * num2) * 3.141592653589793 / 180.0);
				float num12 = (float)Math.Cos((double)num10);
				float num13 = (float)Math.Cos((double)num11);
				float num14 = (float)Math.Sin((double)num10);
				float num15 = (float)Math.Sin((double)num11);
				if (num2 > 0f)
				{
					list.Add(new float[]
					{
						num4 + num6 * num12,
						num5 - num7 * num14,
						num4 + num6 * (num12 - num9 * num14),
						num5 - num7 * (num14 + num9 * num12),
						num4 + num6 * (num13 + num9 * num15),
						num5 - num7 * (num15 - num9 * num13),
						num4 + num6 * num13,
						num5 - num7 * num15
					});
				}
				else
				{
					list.Add(new float[]
					{
						num4 + num6 * num12,
						num5 - num7 * num14,
						num4 + num6 * (num12 + num9 * num14),
						num5 - num7 * (num14 - num9 * num12),
						num4 + num6 * (num13 - num9 * num15),
						num5 - num7 * (num15 + num9 * num13),
						num4 + num6 * num13,
						num5 - num7 * num15
					});
				}
			}
			return list;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00045E88 File Offset: 0x00044E88
		public void Arc(float x1, float y1, float x2, float y2, float startAng, float extent)
		{
			List<float[]> list = PdfContentByte.BezierArc(x1, y1, x2, y2, startAng, extent);
			if (list.Count == 0)
			{
				return;
			}
			float[] array = list[0];
			this.MoveTo(array[0], array[1]);
			for (int i = 0; i < list.Count; i++)
			{
				array = list[i];
				this.CurveTo(array[2], array[3], array[4], array[5], array[6], array[7]);
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00045EF2 File Offset: 0x00044EF2
		public void Ellipse(float x1, float y1, float x2, float y2)
		{
			this.Arc(x1, y1, x2, y2, 0f, 360f);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00045F0C File Offset: 0x00044F0C
		public PdfPatternPainter CreatePattern(float width, float height, float xstep, float ystep)
		{
			this.CheckWriter();
			if (xstep == 0f || ystep == 0f)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("xstep.or.ystep.can.not.be.zero"));
			}
			PdfPatternPainter pdfPatternPainter = new PdfPatternPainter(this.writer);
			pdfPatternPainter.Width = width;
			pdfPatternPainter.Height = height;
			pdfPatternPainter.XStep = xstep;
			pdfPatternPainter.YStep = ystep;
			this.writer.AddSimplePattern(pdfPatternPainter);
			return pdfPatternPainter;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00045F77 File Offset: 0x00044F77
		public PdfPatternPainter CreatePattern(float width, float height)
		{
			return this.CreatePattern(width, height, width, height);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00045F84 File Offset: 0x00044F84
		public PdfPatternPainter CreatePattern(float width, float height, float xstep, float ystep, BaseColor color)
		{
			this.CheckWriter();
			if (xstep == 0f || ystep == 0f)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("xstep.or.ystep.can.not.be.zero"));
			}
			PdfPatternPainter pdfPatternPainter = new PdfPatternPainter(this.writer, color);
			pdfPatternPainter.Width = width;
			pdfPatternPainter.Height = height;
			pdfPatternPainter.XStep = xstep;
			pdfPatternPainter.YStep = ystep;
			this.writer.AddSimplePattern(pdfPatternPainter);
			return pdfPatternPainter;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00045FF1 File Offset: 0x00044FF1
		public PdfPatternPainter CreatePattern(float width, float height, BaseColor color)
		{
			return this.CreatePattern(width, height, width, height, color);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00045FFE File Offset: 0x00044FFE
		public PdfTemplate CreateTemplate(float width, float height)
		{
			return this.CreateTemplate(width, height, null);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0004600C File Offset: 0x0004500C
		internal PdfTemplate CreateTemplate(float width, float height, PdfName forcedName)
		{
			this.CheckWriter();
			PdfTemplate pdfTemplate = new PdfTemplate(this.writer);
			pdfTemplate.Width = width;
			pdfTemplate.Height = height;
			this.writer.AddDirectTemplateSimple(pdfTemplate, forcedName);
			return pdfTemplate;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00046048 File Offset: 0x00045048
		public PdfAppearance CreateAppearance(float width, float height)
		{
			return this.CreateAppearance(width, height, null);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00046054 File Offset: 0x00045054
		internal PdfAppearance CreateAppearance(float width, float height, PdfName forcedName)
		{
			this.CheckWriter();
			PdfAppearance pdfAppearance = new PdfAppearance(this.writer);
			pdfAppearance.Width = width;
			pdfAppearance.Height = height;
			this.writer.AddDirectTemplateSimple(pdfAppearance, forcedName);
			return pdfAppearance;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00046090 File Offset: 0x00045090
		public void AddPSXObject(PdfPSXObject psobject)
		{
			this.CheckWriter();
			PdfName pdfName = this.writer.AddDirectTemplateSimple(psobject, null);
			PageResources pageResources = this.PageResources;
			pdfName = pageResources.AddXObject(pdfName, psobject.IndirectReference);
			this.content.Append(pdfName.GetBytes()).Append(" Do").Append_i(this.separator);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000460F0 File Offset: 0x000450F0
		public virtual void AddTemplate(PdfTemplate template, float a, float b, float c, float d, float e, float f)
		{
			this.CheckWriter();
			this.CheckNoPattern(template);
			PdfName pdfName = this.writer.AddDirectTemplateSimple(template, null);
			PageResources pageResources = this.PageResources;
			pdfName = pageResources.AddXObject(pdfName, template.IndirectReference);
			this.content.Append("q ");
			this.content.Append(a).Append(' ');
			this.content.Append(b).Append(' ');
			this.content.Append(c).Append(' ');
			this.content.Append(d).Append(' ');
			this.content.Append(e).Append(' ');
			this.content.Append(f).Append(" cm ");
			this.content.Append(pdfName.GetBytes()).Append(" Do Q").Append_i(this.separator);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000461E4 File Offset: 0x000451E4
		public void AddTemplate(PdfTemplate template, Matrix transform)
		{
			float[] elements = transform.Elements;
			this.AddTemplate(template, elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00046214 File Offset: 0x00045214
		internal void AddTemplateReference(PdfIndirectReference template, PdfName name, float a, float b, float c, float d, float e, float f)
		{
			this.CheckWriter();
			PageResources pageResources = this.PageResources;
			name = pageResources.AddXObject(name, template);
			this.content.Append("q ");
			this.content.Append(a).Append(' ');
			this.content.Append(b).Append(' ');
			this.content.Append(c).Append(' ');
			this.content.Append(d).Append(' ');
			this.content.Append(e).Append(' ');
			this.content.Append(f).Append(" cm ");
			this.content.Append(name.GetBytes()).Append(" Do Q").Append_i(this.separator);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000462F0 File Offset: 0x000452F0
		public void AddTemplate(PdfTemplate template, float x, float y)
		{
			this.AddTemplate(template, 1f, 0f, 0f, 1f, x, y);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00046310 File Offset: 0x00045310
		public virtual void SetCMYKColorFill(int cyan, int magenta, int yellow, int black)
		{
			this.content.Append((float)(cyan & 255) / 255f);
			this.content.Append(' ');
			this.content.Append((float)(magenta & 255) / 255f);
			this.content.Append(' ');
			this.content.Append((float)(yellow & 255) / 255f);
			this.content.Append(' ');
			this.content.Append((float)(black & 255) / 255f);
			this.content.Append(" k").Append_i(this.separator);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000463CC File Offset: 0x000453CC
		public virtual void SetCMYKColorStroke(int cyan, int magenta, int yellow, int black)
		{
			this.content.Append((float)(cyan & 255) / 255f);
			this.content.Append(' ');
			this.content.Append((float)(magenta & 255) / 255f);
			this.content.Append(' ');
			this.content.Append((float)(yellow & 255) / 255f);
			this.content.Append(' ');
			this.content.Append((float)(black & 255) / 255f);
			this.content.Append(" K").Append_i(this.separator);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00046488 File Offset: 0x00045488
		public virtual void SetRGBColorFill(int red, int green, int blue)
		{
			this.HelperRGB((float)(red & 255) / 255f, (float)(green & 255) / 255f, (float)(blue & 255) / 255f);
			this.content.Append(" rg").Append_i(this.separator);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000464E4 File Offset: 0x000454E4
		public virtual void SetRGBColorStroke(int red, int green, int blue)
		{
			this.HelperRGB((float)(red & 255) / 255f, (float)(green & 255) / 255f, (float)(blue & 255) / 255f);
			this.content.Append(" RG").Append_i(this.separator);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00046540 File Offset: 0x00045540
		public virtual void SetColorStroke(BaseColor value)
		{
			PdfXConformanceImp.CheckPDFXConformance(this.writer, 1, value);
			switch (ExtendedColor.GetType(value))
			{
			case 1:
				this.SetGrayStroke(((GrayColor)value).Gray);
				return;
			case 2:
			{
				CMYKColor cmykcolor = (CMYKColor)value;
				this.SetCMYKColorStrokeF(cmykcolor.Cyan, cmykcolor.Magenta, cmykcolor.Yellow, cmykcolor.Black);
				return;
			}
			case 3:
			{
				SpotColor spotColor = (SpotColor)value;
				this.SetColorStroke(spotColor.PdfSpotColor, spotColor.Tint);
				return;
			}
			case 4:
			{
				PatternColor patternColor = (PatternColor)value;
				this.SetPatternStroke(patternColor.Painter);
				return;
			}
			case 5:
			{
				ShadingColor shadingColor = (ShadingColor)value;
				this.SetShadingStroke(shadingColor.PdfShadingPattern);
				return;
			}
			default:
				this.SetRGBColorStroke(value.R, value.G, value.B);
				return;
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00046618 File Offset: 0x00045618
		public virtual void SetColorFill(BaseColor value)
		{
			PdfXConformanceImp.CheckPDFXConformance(this.writer, 1, value);
			switch (ExtendedColor.GetType(value))
			{
			case 1:
				this.SetGrayFill(((GrayColor)value).Gray);
				return;
			case 2:
			{
				CMYKColor cmykcolor = (CMYKColor)value;
				this.SetCMYKColorFillF(cmykcolor.Cyan, cmykcolor.Magenta, cmykcolor.Yellow, cmykcolor.Black);
				return;
			}
			case 3:
			{
				SpotColor spotColor = (SpotColor)value;
				this.SetColorFill(spotColor.PdfSpotColor, spotColor.Tint);
				return;
			}
			case 4:
			{
				PatternColor patternColor = (PatternColor)value;
				this.SetPatternFill(patternColor.Painter);
				return;
			}
			case 5:
			{
				ShadingColor shadingColor = (ShadingColor)value;
				this.SetShadingFill(shadingColor.PdfShadingPattern);
				return;
			}
			default:
				this.SetRGBColorFill(value.R, value.G, value.B);
				return;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x000466F0 File Offset: 0x000456F0
		public virtual void SetColorFill(PdfSpotColor sp, float tint)
		{
			this.CheckWriter();
			this.state.colorDetails = this.writer.AddSimple(sp);
			PageResources pageResources = this.PageResources;
			PdfName pdfName = this.state.colorDetails.ColorName;
			pdfName = pageResources.AddColor(pdfName, this.state.colorDetails.IndirectReference);
			this.content.Append(pdfName.GetBytes()).Append(" cs ").Append(tint).Append(" scn").Append_i(this.separator);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00046784 File Offset: 0x00045784
		public virtual void SetColorStroke(PdfSpotColor sp, float tint)
		{
			this.CheckWriter();
			this.state.colorDetails = this.writer.AddSimple(sp);
			PageResources pageResources = this.PageResources;
			PdfName pdfName = this.state.colorDetails.ColorName;
			pdfName = pageResources.AddColor(pdfName, this.state.colorDetails.IndirectReference);
			this.content.Append(pdfName.GetBytes()).Append(" CS ").Append(tint).Append(" SCN").Append_i(this.separator);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00046818 File Offset: 0x00045818
		public virtual void SetPatternFill(PdfPatternPainter p)
		{
			if (p.IsStencil())
			{
				this.SetPatternFill(p, p.DefaultColor);
				return;
			}
			this.CheckWriter();
			PageResources pageResources = this.PageResources;
			PdfName pdfName = this.writer.AddSimplePattern(p);
			pdfName = pageResources.AddPattern(pdfName, p.IndirectReference);
			this.content.Append(PdfName.PATTERN.GetBytes()).Append(" cs ").Append(pdfName.GetBytes()).Append(" scn").Append_i(this.separator);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000468A4 File Offset: 0x000458A4
		internal void OutputColorNumbers(BaseColor color, float tint)
		{
			PdfXConformanceImp.CheckPDFXConformance(this.writer, 1, color);
			switch (ExtendedColor.GetType(color))
			{
			case 0:
				this.content.Append((float)color.R / 255f);
				this.content.Append(' ');
				this.content.Append((float)color.G / 255f);
				this.content.Append(' ');
				this.content.Append((float)color.B / 255f);
				return;
			case 1:
				this.content.Append(((GrayColor)color).Gray);
				return;
			case 2:
			{
				CMYKColor cmykcolor = (CMYKColor)color;
				this.content.Append(cmykcolor.Cyan).Append(' ').Append(cmykcolor.Magenta);
				this.content.Append(' ').Append(cmykcolor.Yellow).Append(' ').Append(cmykcolor.Black);
				return;
			}
			case 3:
				this.content.Append(tint);
				return;
			default:
				throw new Exception(MessageLocalization.GetComposedMessage("invalid.color.type"));
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000469D6 File Offset: 0x000459D6
		public virtual void SetPatternFill(PdfPatternPainter p, BaseColor color)
		{
			if (ExtendedColor.GetType(color) == 3)
			{
				this.SetPatternFill(p, color, ((SpotColor)color).Tint);
				return;
			}
			this.SetPatternFill(p, color, 0f);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00046A04 File Offset: 0x00045A04
		public virtual void SetPatternFill(PdfPatternPainter p, BaseColor color, float tint)
		{
			this.CheckWriter();
			if (!p.IsStencil())
			{
				throw new Exception(MessageLocalization.GetComposedMessage("an.uncolored.pattern.was.expected"));
			}
			PageResources pageResources = this.PageResources;
			PdfName pdfName = this.writer.AddSimplePattern(p);
			pdfName = pageResources.AddPattern(pdfName, p.IndirectReference);
			ColorDetails colorDetails = this.writer.AddSimplePatternColorspace(color);
			PdfName pdfName2 = pageResources.AddColor(colorDetails.ColorName, colorDetails.IndirectReference);
			this.content.Append(pdfName2.GetBytes()).Append(" cs").Append_i(this.separator);
			this.OutputColorNumbers(color, tint);
			this.content.Append(' ').Append(pdfName.GetBytes()).Append(" scn").Append_i(this.separator);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00046ACE File Offset: 0x00045ACE
		public virtual void SetPatternStroke(PdfPatternPainter p, BaseColor color)
		{
			if (ExtendedColor.GetType(color) == 3)
			{
				this.SetPatternStroke(p, color, ((SpotColor)color).Tint);
				return;
			}
			this.SetPatternStroke(p, color, 0f);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00046AFC File Offset: 0x00045AFC
		public virtual void SetPatternStroke(PdfPatternPainter p, BaseColor color, float tint)
		{
			this.CheckWriter();
			if (!p.IsStencil())
			{
				throw new Exception(MessageLocalization.GetComposedMessage("an.uncolored.pattern.was.expected"));
			}
			PageResources pageResources = this.PageResources;
			PdfName pdfName = this.writer.AddSimplePattern(p);
			pdfName = pageResources.AddPattern(pdfName, p.IndirectReference);
			ColorDetails colorDetails = this.writer.AddSimplePatternColorspace(color);
			PdfName pdfName2 = pageResources.AddColor(colorDetails.ColorName, colorDetails.IndirectReference);
			this.content.Append(pdfName2.GetBytes()).Append(" CS").Append_i(this.separator);
			this.OutputColorNumbers(color, tint);
			this.content.Append(' ').Append(pdfName.GetBytes()).Append(" SCN").Append_i(this.separator);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00046BC8 File Offset: 0x00045BC8
		public virtual void SetPatternStroke(PdfPatternPainter p)
		{
			if (p.IsStencil())
			{
				this.SetPatternStroke(p, p.DefaultColor);
				return;
			}
			this.CheckWriter();
			PageResources pageResources = this.PageResources;
			PdfName pdfName = this.writer.AddSimplePattern(p);
			pdfName = pageResources.AddPattern(pdfName, p.IndirectReference);
			this.content.Append(PdfName.PATTERN.GetBytes()).Append(" CS ").Append(pdfName.GetBytes()).Append(" SCN").Append_i(this.separator);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00046C54 File Offset: 0x00045C54
		public virtual void PaintShading(PdfShading shading)
		{
			this.writer.AddSimpleShading(shading);
			PageResources pageResources = this.PageResources;
			PdfName pdfName = pageResources.AddShading(shading.ShadingName, shading.ShadingReference);
			this.content.Append(pdfName.GetBytes()).Append(" sh").Append_i(this.separator);
			ColorDetails colorDetails = shading.ColorDetails;
			if (colorDetails != null)
			{
				pageResources.AddColor(colorDetails.ColorName, colorDetails.IndirectReference);
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00046CCB File Offset: 0x00045CCB
		public virtual void PaintShading(PdfShadingPattern shading)
		{
			this.PaintShading(shading.Shading);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00046CDC File Offset: 0x00045CDC
		public virtual void SetShadingFill(PdfShadingPattern shading)
		{
			this.writer.AddSimpleShadingPattern(shading);
			PageResources pageResources = this.PageResources;
			PdfName pdfName = pageResources.AddPattern(shading.PatternName, shading.PatternReference);
			this.content.Append(PdfName.PATTERN.GetBytes()).Append(" cs ").Append(pdfName.GetBytes()).Append(" scn").Append_i(this.separator);
			ColorDetails colorDetails = shading.ColorDetails;
			if (colorDetails != null)
			{
				pageResources.AddColor(colorDetails.ColorName, colorDetails.IndirectReference);
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00046D6C File Offset: 0x00045D6C
		public virtual void SetShadingStroke(PdfShadingPattern shading)
		{
			this.writer.AddSimpleShadingPattern(shading);
			PageResources pageResources = this.PageResources;
			PdfName pdfName = pageResources.AddPattern(shading.PatternName, shading.PatternReference);
			this.content.Append(PdfName.PATTERN.GetBytes()).Append(" CS ").Append(pdfName.GetBytes()).Append(" SCN").Append_i(this.separator);
			ColorDetails colorDetails = shading.ColorDetails;
			if (colorDetails != null)
			{
				pageResources.AddColor(colorDetails.ColorName, colorDetails.IndirectReference);
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00046DFC File Offset: 0x00045DFC
		protected virtual void CheckWriter()
		{
			if (this.writer == null)
			{
				throw new ArgumentNullException(MessageLocalization.GetComposedMessage("the.writer.in.pdfcontentbyte.is.null"));
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00046E18 File Offset: 0x00045E18
		public void ShowText(PdfTextArray text)
		{
			if (this.state.fontDetails == null)
			{
				throw new ArgumentNullException(MessageLocalization.GetComposedMessage("font.and.size.must.be.set.before.writing.any.text"));
			}
			this.content.Append('[');
			List<object> arrayList = text.ArrayList;
			bool flag = false;
			foreach (object obj in arrayList)
			{
				if (obj is string)
				{
					this.ShowText2((string)obj);
					flag = false;
				}
				else
				{
					if (flag)
					{
						this.content.Append(' ');
					}
					else
					{
						flag = true;
					}
					this.content.Append((float)obj);
				}
			}
			this.content.Append("]TJ").Append_i(this.separator);
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00046EF0 File Offset: 0x00045EF0
		public PdfWriter PdfWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00046EF8 File Offset: 0x00045EF8
		public PdfDocument PdfDocument
		{
			get
			{
				return this.pdf;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00046F00 File Offset: 0x00045F00
		public void LocalGoto(string name, float llx, float lly, float urx, float ury)
		{
			this.pdf.LocalGoto(name, llx, lly, urx, ury);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00046F14 File Offset: 0x00045F14
		public bool LocalDestination(string name, PdfDestination destination)
		{
			return this.pdf.LocalDestination(name, destination);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00046F23 File Offset: 0x00045F23
		public virtual PdfContentByte Duplicate
		{
			get
			{
				return new PdfContentByte(this.writer);
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00046F30 File Offset: 0x00045F30
		public void RemoteGoto(string filename, string name, float llx, float lly, float urx, float ury)
		{
			this.RemoteGoto(filename, name, llx, lly, urx, ury);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00046F41 File Offset: 0x00045F41
		public void RemoteGoto(string filename, int page, float llx, float lly, float urx, float ury)
		{
			this.pdf.RemoteGoto(filename, page, llx, lly, urx, ury);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00046F58 File Offset: 0x00045F58
		public void RoundRectangle(float x, float y, float w, float h, float r)
		{
			if (w < 0f)
			{
				x += w;
				w = -w;
			}
			if (h < 0f)
			{
				y += h;
				h = -h;
			}
			if (r < 0f)
			{
				r = -r;
			}
			float num = 0.4477f;
			this.MoveTo(x + r, y);
			this.LineTo(x + w - r, y);
			this.CurveTo(x + w - r * num, y, x + w, y + r * num, x + w, y + r);
			this.LineTo(x + w, y + h - r);
			this.CurveTo(x + w, y + h - r * num, x + w - r * num, y + h, x + w - r, y + h);
			this.LineTo(x + r, y + h);
			this.CurveTo(x + r * num, y + h, x, y + h - r * num, x, y + h - r);
			this.LineTo(x, y + r);
			this.CurveTo(x, y + r * num, x + r * num, y, x + r, y);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00047061 File Offset: 0x00046061
		public virtual void SetAction(PdfAction action, float llx, float lly, float urx, float ury)
		{
			this.pdf.SetAction(action, llx, lly, urx, ury);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00047075 File Offset: 0x00046075
		public void SetLiteral(string s)
		{
			this.content.Append(s);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00047084 File Offset: 0x00046084
		public void SetLiteral(char c)
		{
			this.content.Append(c);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00047093 File Offset: 0x00046093
		public void SetLiteral(float n)
		{
			this.content.Append(n);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000470A2 File Offset: 0x000460A2
		internal void CheckNoPattern(PdfTemplate t)
		{
			if (t.Type == 3)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.use.of.a.pattern.a.template.was.expected"));
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000470C0 File Offset: 0x000460C0
		public void DrawRadioField(float llx, float lly, float urx, float ury, bool on)
		{
			if (llx > urx)
			{
				float num = llx;
				llx = urx;
				urx = num;
			}
			if (lly > ury)
			{
				float num2 = lly;
				lly = ury;
				ury = num2;
			}
			this.SetLineWidth(1f);
			this.SetLineCap(1);
			this.SetColorStroke(new BaseColor(192, 192, 192));
			this.Arc(llx + 1f, lly + 1f, urx - 1f, ury - 1f, 0f, 360f);
			this.Stroke();
			this.SetLineWidth(1f);
			this.SetLineCap(1);
			this.SetColorStroke(new BaseColor(160, 160, 160));
			this.Arc(llx + 0.5f, lly + 0.5f, urx - 0.5f, ury - 0.5f, 45f, 180f);
			this.Stroke();
			this.SetLineWidth(1f);
			this.SetLineCap(1);
			this.SetColorStroke(new BaseColor(0, 0, 0));
			this.Arc(llx + 1.5f, lly + 1.5f, urx - 1.5f, ury - 1.5f, 45f, 180f);
			this.Stroke();
			if (on)
			{
				this.SetLineWidth(1f);
				this.SetLineCap(1);
				this.SetColorFill(new BaseColor(0, 0, 0));
				this.Arc(llx + 4f, lly + 4f, urx - 4f, ury - 4f, 0f, 360f);
				this.Fill();
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00047250 File Offset: 0x00046250
		public void DrawTextField(float llx, float lly, float urx, float ury)
		{
			if (llx > urx)
			{
				float num = llx;
				llx = urx;
				urx = num;
			}
			if (lly > ury)
			{
				float num2 = lly;
				lly = ury;
				ury = num2;
			}
			this.SetColorStroke(new BaseColor(192, 192, 192));
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.Rectangle(llx, lly, urx - llx, ury - lly);
			this.Stroke();
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.SetColorFill(new BaseColor(255, 255, 255));
			this.Rectangle(llx + 0.5f, lly + 0.5f, urx - llx - 1f, ury - lly - 1f);
			this.Fill();
			this.SetColorStroke(new BaseColor(192, 192, 192));
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.MoveTo(llx + 1f, lly + 1.5f);
			this.LineTo(urx - 1.5f, lly + 1.5f);
			this.LineTo(urx - 1.5f, ury - 1f);
			this.Stroke();
			this.SetColorStroke(new BaseColor(160, 160, 160));
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.MoveTo(llx + 1f, lly + 1f);
			this.LineTo(llx + 1f, ury - 1f);
			this.LineTo(urx - 1f, ury - 1f);
			this.Stroke();
			this.SetColorStroke(new BaseColor(0, 0, 0));
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.MoveTo(llx + 2f, lly + 2f);
			this.LineTo(llx + 2f, ury - 2f);
			this.LineTo(urx - 2f, ury - 2f);
			this.Stroke();
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00047454 File Offset: 0x00046454
		public void DrawButton(float llx, float lly, float urx, float ury, string text, BaseFont bf, float size)
		{
			if (llx > urx)
			{
				float num = llx;
				llx = urx;
				urx = num;
			}
			if (lly > ury)
			{
				float num2 = lly;
				lly = ury;
				ury = num2;
			}
			this.SetColorStroke(new BaseColor(0, 0, 0));
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.Rectangle(llx, lly, urx - llx, ury - lly);
			this.Stroke();
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.SetColorFill(new BaseColor(192, 192, 192));
			this.Rectangle(llx + 0.5f, lly + 0.5f, urx - llx - 1f, ury - lly - 1f);
			this.Fill();
			this.SetColorStroke(new BaseColor(255, 255, 255));
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.MoveTo(llx + 1f, lly + 1f);
			this.LineTo(llx + 1f, ury - 1f);
			this.LineTo(urx - 1f, ury - 1f);
			this.Stroke();
			this.SetColorStroke(new BaseColor(160, 160, 160));
			this.SetLineWidth(1f);
			this.SetLineCap(0);
			this.MoveTo(llx + 1f, lly + 1f);
			this.LineTo(urx - 1f, lly + 1f);
			this.LineTo(urx - 1f, ury - 1f);
			this.Stroke();
			this.ResetRGBColorFill();
			this.BeginText();
			this.SetFontAndSize(bf, size);
			this.ShowTextAligned(1, text, llx + (urx - llx) / 2f, lly + (ury - lly - size) / 2f, 0f);
			this.EndText();
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0004762C File Offset: 0x0004662C
		internal virtual PageResources PageResources
		{
			get
			{
				return this.pdf.PageResources;
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0004763C File Offset: 0x0004663C
		public void SetGState(PdfGState gstate)
		{
			PdfObject[] array = this.writer.AddSimpleExtGState(gstate);
			PageResources pageResources = this.PageResources;
			PdfName pdfName = pageResources.AddExtGState((PdfName)array[0], (PdfIndirectReference)array[1]);
			this.content.Append(pdfName.GetBytes()).Append(" gs").Append_i(this.separator);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0004769C File Offset: 0x0004669C
		public void BeginLayer(IPdfOCG layer)
		{
			if (layer is PdfLayer && ((PdfLayer)layer).Title != null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("a.title.is.not.a.layer"));
			}
			if (this.layerDepth == null)
			{
				this.layerDepth = new List<int>();
			}
			if (layer is PdfLayerMembership)
			{
				this.layerDepth.Add(1);
				this.BeginLayer2(layer);
				return;
			}
			int num = 0;
			for (PdfLayer pdfLayer = (PdfLayer)layer; pdfLayer != null; pdfLayer = pdfLayer.Parent)
			{
				if (pdfLayer.Title == null)
				{
					this.BeginLayer2(pdfLayer);
					num++;
				}
			}
			this.layerDepth.Add(num);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00047734 File Offset: 0x00046734
		private void BeginLayer2(IPdfOCG layer)
		{
			PdfName pdfName = (PdfName)this.writer.AddSimpleProperty(layer, layer.Ref)[0];
			PageResources pageResources = this.PageResources;
			pdfName = pageResources.AddProperty(pdfName, layer.Ref);
			this.content.Append("/OC ").Append(pdfName.GetBytes()).Append(" BDC").Append_i(this.separator);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000477A4 File Offset: 0x000467A4
		public void EndLayer()
		{
			if (this.layerDepth != null && this.layerDepth.Count > 0)
			{
				int num = this.layerDepth[this.layerDepth.Count - 1];
				this.layerDepth.RemoveAt(this.layerDepth.Count - 1);
				while (num-- > 0)
				{
					this.content.Append("EMC").Append_i(this.separator);
				}
				return;
			}
			throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.layer.operators"));
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00047830 File Offset: 0x00046830
		internal virtual void AddAnnotation(PdfAnnotation annot)
		{
			this.writer.AddAnnotation(annot);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00047840 File Offset: 0x00046840
		public virtual void SetDefaultColorspace(PdfName name, PdfObject obj)
		{
			PageResources pageResources = this.PageResources;
			pageResources.AddDefaultColor(name, obj);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0004785C File Offset: 0x0004685C
		public void Transform(Matrix tx)
		{
			float[] elements = tx.Elements;
			this.ConcatCTM(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00047888 File Offset: 0x00046888
		public void BeginMarkedContentSequence(PdfStructureElement struc)
		{
			PdfObject pdfObject = struc.Get(PdfName.K);
			int markPoint = this.pdf.GetMarkPoint();
			if (pdfObject != null)
			{
				PdfArray pdfArray;
				if (pdfObject.IsNumber())
				{
					pdfArray = new PdfArray();
					pdfArray.Add(pdfObject);
					struc.Put(PdfName.K, pdfArray);
				}
				else
				{
					if (!pdfObject.IsArray())
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("unknown.object.at.k.1", pdfObject.GetType().ToString()));
					}
					pdfArray = (PdfArray)pdfObject;
					if (!pdfArray[0].IsNumber())
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("the.structure.has.kids"));
					}
				}
				PdfDictionary pdfDictionary = new PdfDictionary(PdfName.MCR);
				pdfDictionary.Put(PdfName.PG, this.writer.CurrentPage);
				pdfDictionary.Put(PdfName.MCID, new PdfNumber(markPoint));
				pdfArray.Add(pdfDictionary);
				struc.SetPageMark(this.writer.PageNumber - 1, -1);
			}
			else
			{
				struc.SetPageMark(this.writer.PageNumber - 1, markPoint);
				struc.Put(PdfName.PG, this.writer.CurrentPage);
			}
			this.pdf.IncMarkPoint();
			this.mcDepth++;
			this.content.Append(struc.Get(PdfName.S).GetBytes()).Append(" <</MCID ").Append(markPoint).Append(">> BDC").Append_i(this.separator);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000479F8 File Offset: 0x000469F8
		public void EndMarkedContentSequence()
		{
			if (this.mcDepth == 0)
			{
				throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.begin.end.marked.content.operators"));
			}
			this.mcDepth--;
			this.content.Append("EMC").Append_i(this.separator);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00047A48 File Offset: 0x00046A48
		public void BeginMarkedContentSequence(PdfName tag, PdfDictionary property, bool inline)
		{
			if (property == null)
			{
				this.content.Append(tag.GetBytes()).Append(" BMC").Append_i(this.separator);
				return;
			}
			this.content.Append(tag.GetBytes()).Append(' ');
			if (inline)
			{
				property.ToPdf(this.writer, this.content);
			}
			else
			{
				PdfObject[] array;
				if (this.writer.PropertyExists(property))
				{
					array = this.writer.AddSimpleProperty(property, null);
				}
				else
				{
					array = this.writer.AddSimpleProperty(property, this.writer.PdfIndirectReference);
				}
				PdfName pdfName = (PdfName)array[0];
				PageResources pageResources = this.PageResources;
				pdfName = pageResources.AddProperty(pdfName, (PdfIndirectReference)array[1]);
				this.content.Append(pdfName.GetBytes());
			}
			this.content.Append(" BDC").Append_i(this.separator);
			this.mcDepth++;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00047B42 File Offset: 0x00046B42
		public void BeginMarkedContentSequence(PdfName tag)
		{
			this.BeginMarkedContentSequence(tag, null, false);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00047B50 File Offset: 0x00046B50
		public void SanityCheck()
		{
			if (this.mcDepth != 0)
			{
				throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.marked.content.operators"));
			}
			if (this.inText)
			{
				throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.begin.end.text.operators"));
			}
			if (this.layerDepth != null && this.layerDepth.Count > 0)
			{
				throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.layer.operators"));
			}
			if (this.stateList.Count > 0)
			{
				throw new IllegalPdfSyntaxException(MessageLocalization.GetComposedMessage("unbalanced.save.restore.state.operators"));
			}
		}

		// Token: 0x040009A2 RID: 2466
		public const int ALIGN_CENTER = 1;

		// Token: 0x040009A3 RID: 2467
		public const int ALIGN_LEFT = 0;

		// Token: 0x040009A4 RID: 2468
		public const int ALIGN_RIGHT = 2;

		// Token: 0x040009A5 RID: 2469
		public const int LINE_CAP_BUTT = 0;

		// Token: 0x040009A6 RID: 2470
		public const int LINE_CAP_ROUND = 1;

		// Token: 0x040009A7 RID: 2471
		public const int LINE_CAP_PROJECTING_SQUARE = 2;

		// Token: 0x040009A8 RID: 2472
		public const int LINE_JOIN_MITER = 0;

		// Token: 0x040009A9 RID: 2473
		public const int LINE_JOIN_ROUND = 1;

		// Token: 0x040009AA RID: 2474
		public const int LINE_JOIN_BEVEL = 2;

		// Token: 0x040009AB RID: 2475
		public const int TEXT_RENDER_MODE_FILL = 0;

		// Token: 0x040009AC RID: 2476
		public const int TEXT_RENDER_MODE_STROKE = 1;

		// Token: 0x040009AD RID: 2477
		public const int TEXT_RENDER_MODE_FILL_STROKE = 2;

		// Token: 0x040009AE RID: 2478
		public const int TEXT_RENDER_MODE_INVISIBLE = 3;

		// Token: 0x040009AF RID: 2479
		public const int TEXT_RENDER_MODE_FILL_CLIP = 4;

		// Token: 0x040009B0 RID: 2480
		public const int TEXT_RENDER_MODE_STROKE_CLIP = 5;

		// Token: 0x040009B1 RID: 2481
		public const int TEXT_RENDER_MODE_FILL_STROKE_CLIP = 6;

		// Token: 0x040009B2 RID: 2482
		public const int TEXT_RENDER_MODE_CLIP = 7;

		// Token: 0x040009B3 RID: 2483
		private static float[] unitRect = new float[]
		{
			0f,
			0f,
			0f,
			1f,
			1f,
			0f,
			1f,
			1f
		};

		// Token: 0x040009B4 RID: 2484
		protected ByteBuffer content = new ByteBuffer();

		// Token: 0x040009B5 RID: 2485
		protected PdfWriter writer;

		// Token: 0x040009B6 RID: 2486
		protected PdfDocument pdf;

		// Token: 0x040009B7 RID: 2487
		protected PdfContentByte.GraphicState state = new PdfContentByte.GraphicState();

		// Token: 0x040009B8 RID: 2488
		protected List<int> layerDepth;

		// Token: 0x040009B9 RID: 2489
		protected List<PdfContentByte.GraphicState> stateList = new List<PdfContentByte.GraphicState>();

		// Token: 0x040009BA RID: 2490
		protected int separator = 10;

		// Token: 0x040009BB RID: 2491
		private int mcDepth;

		// Token: 0x040009BC RID: 2492
		private bool inText;

		// Token: 0x040009BD RID: 2493
		private static Dictionary<PdfName, string> abrev = new Dictionary<PdfName, string>();

		// Token: 0x0200015A RID: 346
		public class GraphicState
		{
			// Token: 0x06000CFE RID: 3326 RVA: 0x00047BD1 File Offset: 0x00046BD1
			internal GraphicState()
			{
			}

			// Token: 0x06000CFF RID: 3327 RVA: 0x00047BE4 File Offset: 0x00046BE4
			internal GraphicState(PdfContentByte.GraphicState cp)
			{
				this.fontDetails = cp.fontDetails;
				this.colorDetails = cp.colorDetails;
				this.size = cp.size;
				this.xTLM = cp.xTLM;
				this.yTLM = cp.yTLM;
				this.leading = cp.leading;
				this.scale = cp.scale;
				this.charSpace = cp.charSpace;
				this.wordSpace = cp.wordSpace;
			}

			// Token: 0x040009BE RID: 2494
			internal FontDetails fontDetails;

			// Token: 0x040009BF RID: 2495
			internal ColorDetails colorDetails;

			// Token: 0x040009C0 RID: 2496
			internal float size;

			// Token: 0x040009C1 RID: 2497
			protected internal float xTLM;

			// Token: 0x040009C2 RID: 2498
			protected internal float yTLM;

			// Token: 0x040009C3 RID: 2499
			protected internal float leading;

			// Token: 0x040009C4 RID: 2500
			protected internal float scale = 100f;

			// Token: 0x040009C5 RID: 2501
			protected internal float charSpace;

			// Token: 0x040009C6 RID: 2502
			protected internal float wordSpace;
		}
	}
}
