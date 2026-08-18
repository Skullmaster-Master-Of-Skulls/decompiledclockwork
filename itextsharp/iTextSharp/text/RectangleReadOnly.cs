using System;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text
{
	// Token: 0x020005C5 RID: 1477
	public class RectangleReadOnly : Rectangle
	{
		// Token: 0x060032B3 RID: 12979 RVA: 0x0013C015 File Offset: 0x0013B015
		public RectangleReadOnly(float llx, float lly, float urx, float ury) : base(llx, lly, urx, ury)
		{
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x0013C022 File Offset: 0x0013B022
		public RectangleReadOnly(float urx, float ury) : base(0f, 0f, urx, ury)
		{
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x0013C036 File Offset: 0x0013B036
		public RectangleReadOnly(Rectangle rect) : base(rect.Left, rect.Bottom, rect.Right, rect.Top)
		{
			base.CloneNonPositionParameters(rect);
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x0013C05D File Offset: 0x0013B05D
		public override void CloneNonPositionParameters(Rectangle rect)
		{
			this.ThrowReadOnlyError();
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x0013C065 File Offset: 0x0013B065
		private void ThrowReadOnlyError()
		{
			throw new InvalidOperationException(MessageLocalization.GetComposedMessage("rectanglereadonly.this.rectangle.is.read.only"));
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x0013C076 File Offset: 0x0013B076
		public override void SoftCloneNonPositionParameters(Rectangle rect)
		{
			this.ThrowReadOnlyError();
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x0013C07E File Offset: 0x0013B07E
		public override void Normalize()
		{
			this.ThrowReadOnlyError();
		}

		// Token: 0x170008AF RID: 2223
		// (set) Token: 0x060032BA RID: 12986 RVA: 0x0013C086 File Offset: 0x0013B086
		public override float Top
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x0013C08E File Offset: 0x0013B08E
		public override void EnableBorderSide(int side)
		{
			this.ThrowReadOnlyError();
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x0013C096 File Offset: 0x0013B096
		public override void DisableBorderSide(int side)
		{
			this.ThrowReadOnlyError();
		}

		// Token: 0x170008B0 RID: 2224
		// (set) Token: 0x060032BD RID: 12989 RVA: 0x0013C09E File Offset: 0x0013B09E
		public override int Border
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (set) Token: 0x060032BE RID: 12990 RVA: 0x0013C0A6 File Offset: 0x0013B0A6
		public override float GrayFill
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (set) Token: 0x060032BF RID: 12991 RVA: 0x0013C0AE File Offset: 0x0013B0AE
		public override float Left
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (set) Token: 0x060032C0 RID: 12992 RVA: 0x0013C0B6 File Offset: 0x0013B0B6
		public override float Right
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x0013C0BE File Offset: 0x0013B0BE
		public override float Bottom
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (set) Token: 0x060032C2 RID: 12994 RVA: 0x0013C0C6 File Offset: 0x0013B0C6
		public override BaseColor BorderColorBottom
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x0013C0CE File Offset: 0x0013B0CE
		public override BaseColor BorderColorTop
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (set) Token: 0x060032C4 RID: 12996 RVA: 0x0013C0D6 File Offset: 0x0013B0D6
		public override BaseColor BorderColorLeft
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x0013C0DE File Offset: 0x0013B0DE
		public override BaseColor BorderColorRight
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (set) Token: 0x060032C6 RID: 12998 RVA: 0x0013C0E6 File Offset: 0x0013B0E6
		public override float BorderWidth
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008BA RID: 2234
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x0013C0EE File Offset: 0x0013B0EE
		public override BaseColor BorderColor
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008BB RID: 2235
		// (set) Token: 0x060032C8 RID: 13000 RVA: 0x0013C0F6 File Offset: 0x0013B0F6
		public override BaseColor BackgroundColor
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008BC RID: 2236
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x0013C0FE File Offset: 0x0013B0FE
		public override float BorderWidthLeft
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008BD RID: 2237
		// (set) Token: 0x060032CA RID: 13002 RVA: 0x0013C106 File Offset: 0x0013B106
		public override float BorderWidthRight
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008BE RID: 2238
		// (set) Token: 0x060032CB RID: 13003 RVA: 0x0013C10E File Offset: 0x0013B10E
		public override float BorderWidthTop
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008BF RID: 2239
		// (set) Token: 0x060032CC RID: 13004 RVA: 0x0013C116 File Offset: 0x0013B116
		public override float BorderWidthBottom
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (set) Token: 0x060032CD RID: 13005 RVA: 0x0013C11E File Offset: 0x0013B11E
		public override bool UseVariableBorders
		{
			set
			{
				this.ThrowReadOnlyError();
			}
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x0013C128 File Offset: 0x0013B128
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("RectangleReadOnly: ");
			stringBuilder.Append(this.Width);
			stringBuilder.Append('x');
			stringBuilder.Append(base.Height);
			stringBuilder.Append(" (rot: ");
			stringBuilder.Append(this.rotation);
			stringBuilder.Append(" degrees)");
			return stringBuilder.ToString();
		}
	}
}
