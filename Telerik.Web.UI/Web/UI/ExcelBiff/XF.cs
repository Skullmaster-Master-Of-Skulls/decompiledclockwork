using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AF5 RID: 2805
	internal sealed class XF : BaseBiffRecord, IRecord
	{
		// Token: 0x0600695C RID: 26972 RVA: 0x0018C690 File Offset: 0x0018A890
		public XF(XF copyXF) : base(224)
		{
			base.Length = 20;
			this.ifnt = copyXF.ifnt;
			this.ifmt = copyXF.ifmt;
			this.bitLockedHiddenStyle = copyXF.bitLockedHiddenStyle;
			this.bitAlignWrapJustLastRotate = copyXF.bitAlignWrapJustLastRotate;
			this.bitIndentShrinkToFitMergeCell = copyXF.bitIndentShrinkToFitMergeCell;
			this.bitBorderLineStyle = copyXF.bitBorderLineStyle;
			this.bitIndexColorPaletteBorder = copyXF.bitIndexColorPaletteBorder;
			this.bitIndexColorPaletteTopBottomDiag = copyXF.bitIndexColorPaletteTopBottomDiag;
			this.bitIndexColorPaletteFill = copyXF.bitIndexColorPaletteFill;
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x0018C71C File Offset: 0x0018A91C
		public XF(ushort fontIndex, ushort formatIndex, bool locked, ushort indentShrinkToFitMergeCell) : base(224)
		{
			base.Length = 20;
			this.ifnt = fontIndex;
			this.ifmt = formatIndex;
			this.bitLockedHiddenStyle = 65525;
			this.bitAlignWrapJustLastRotate = 32;
			this.bitIndentShrinkToFitMergeCell = indentShrinkToFitMergeCell;
			this.bitBorderLineStyle = 0;
			this.bitIndexColorPaletteBorder = 0;
			this.bitIndexColorPaletteTopBottomDiag = 0U;
			this.bitIndexColorPaletteFill = 8384;
			if (locked)
			{
				this.bitLockedHiddenStyle = 1;
			}
			this.bitAlignWrapJustLastRotate |= 8;
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x0018C7A0 File Offset: 0x0018A9A0
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.ifnt);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.ifmt);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bitLockedHiddenStyle);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bitAlignWrapJustLastRotate);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bitIndentShrinkToFitMergeCell);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bitBorderLineStyle);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bitIndexColorPaletteBorder);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bitIndexColorPaletteTopBottomDiag);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bitIndexColorPaletteFill);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x0018C89C File Offset: 0x0018AA9C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[XF]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("ifnt={0};", this.ifnt);
			stringBuilder.AppendFormat("ifmt={0};", this.ifmt);
			stringBuilder.AppendFormat("bitLockedHiddenStyle=0x{0:x4};", this.bitLockedHiddenStyle);
			stringBuilder.AppendFormat("bitAlignWrapJustLastRotate=0x{0:x4};", this.bitAlignWrapJustLastRotate);
			stringBuilder.AppendFormat("bitIndentShrinkToFitMergeCell=0x{0:x4};", this.bitIndentShrinkToFitMergeCell);
			stringBuilder.AppendFormat("bitBorderLineStyle=0x{0:x4};", this.bitBorderLineStyle);
			stringBuilder.AppendFormat("bitIndexColorPaletteBorder=0x{0:x4};", this.bitIndexColorPaletteBorder);
			stringBuilder.AppendFormat("bitIndexColorPaletteTopBottomDiag=0x{0:x4};", this.bitIndexColorPaletteTopBottomDiag);
			stringBuilder.AppendFormat("bitIndexColorPaletteFill=0x{0:x4};", this.bitIndexColorPaletteFill);
			stringBuilder.Append("[/XF]");
			return stringBuilder.ToString();
		}

		// Token: 0x06006960 RID: 26976 RVA: 0x0018C9AC File Offset: 0x0018ABAC
		public void SetDiagonalBorder(BiffCell.DiagonalDirection direction, BorderStyle borderStyle)
		{
			this.bitIndexColorPaletteBorder &= 16383;
			this.bitIndexColorPaletteTopBottomDiag &= 4263510015U;
			this.bitIndexColorPaletteBorder = (ushort)((BiffCell.DiagonalDirection)this.bitIndexColorPaletteBorder | direction << 14);
			this.bitIndexColorPaletteTopBottomDiag |= (uint)((uint)borderStyle << 21);
			this.bitIndentShrinkToFitMergeCell |= 8192;
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x0018CA14 File Offset: 0x0018AC14
		public void SetIndentation(ushort indentLevel, BiffCell.HorizontalAlignments alignment)
		{
			if (indentLevel <= 15)
			{
				this.bitIndentShrinkToFitMergeCell &= 65520;
				this.bitIndentShrinkToFitMergeCell |= indentLevel;
				this.HorizontalAlignment = alignment;
			}
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x0018CA44 File Offset: 0x0018AC44
		public void SetTransparent()
		{
			this.bitIndexColorPaletteFill &= 65408;
			this.bitIndexColorPaletteFill |= 64;
			this.bitIndentShrinkToFitMergeCell |= 16384;
			this.bitIndexColorPaletteTopBottomDiag &= 67108863U;
		}

		// Token: 0x1700227B RID: 8827
		// (set) Token: 0x06006963 RID: 26979 RVA: 0x0018CA99 File Offset: 0x0018AC99
		public BorderStyle BottomBorder
		{
			set
			{
				this.bitBorderLineStyle &= 4095;
				this.bitBorderLineStyle = (ushort)((BorderStyle)this.bitBorderLineStyle | value << 12);
				this.bitIndentShrinkToFitMergeCell |= 8192;
			}
		}

		// Token: 0x1700227C RID: 8828
		// (set) Token: 0x06006964 RID: 26980 RVA: 0x0018CAD3 File Offset: 0x0018ACD3
		public ushort BottomBorderColor
		{
			set
			{
				this.bitIndexColorPaletteTopBottomDiag &= 4294951039U;
				this.bitIndexColorPaletteTopBottomDiag |= (uint)((uint)value << 7);
			}
		}

		// Token: 0x1700227D RID: 8829
		// (set) Token: 0x06006965 RID: 26981 RVA: 0x0018CAF8 File Offset: 0x0018ACF8
		public ushort CellColor
		{
			set
			{
				this.bitIndexColorPaletteFill &= 65408;
				this.bitIndexColorPaletteFill |= value;
				if (64 != value)
				{
					this.bitIndentShrinkToFitMergeCell |= 16384;
					this.bitIndexColorPaletteTopBottomDiag |= 67108864U;
				}
			}
		}

		// Token: 0x1700227E RID: 8830
		// (set) Token: 0x06006966 RID: 26982 RVA: 0x0018CB51 File Offset: 0x0018AD51
		public ushort DiagonalBorderColor
		{
			set
			{
				this.bitIndexColorPaletteTopBottomDiag &= 4292886527U;
				this.bitIndexColorPaletteTopBottomDiag |= (uint)((uint)value << 14);
			}
		}

		// Token: 0x1700227F RID: 8831
		// (set) Token: 0x06006967 RID: 26983 RVA: 0x0018CB78 File Offset: 0x0018AD78
		public bool RTL
		{
			set
			{
				ushort num = value ? 128 : 64;
				this.bitIndentShrinkToFitMergeCell |= num;
			}
		}

		// Token: 0x17002280 RID: 8832
		// (set) Token: 0x06006968 RID: 26984 RVA: 0x0018CBA1 File Offset: 0x0018ADA1
		public ushort FontIndex
		{
			set
			{
				this.ifnt = value;
				this.bitIndentShrinkToFitMergeCell |= 32768;
			}
		}

		// Token: 0x17002281 RID: 8833
		// (set) Token: 0x06006969 RID: 26985 RVA: 0x0018CBBD File Offset: 0x0018ADBD
		public ushort FormatIndex
		{
			set
			{
				this.ifmt = value;
				this.bitIndentShrinkToFitMergeCell |= 1024;
			}
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x0600696A RID: 26986 RVA: 0x0018CBD9 File Offset: 0x0018ADD9
		// (set) Token: 0x0600696B RID: 26987 RVA: 0x0018CBE3 File Offset: 0x0018ADE3
		public BiffCell.HorizontalAlignments HorizontalAlignment
		{
			get
			{
				return (BiffCell.HorizontalAlignments)(this.bitAlignWrapJustLastRotate & 7);
			}
			set
			{
				this.bitAlignWrapJustLastRotate &= 65528;
				this.bitAlignWrapJustLastRotate = (ushort)((BiffCell.HorizontalAlignments)this.bitAlignWrapJustLastRotate | value);
			}
		}

		// Token: 0x17002283 RID: 8835
		// (set) Token: 0x0600696C RID: 26988 RVA: 0x0018CC07 File Offset: 0x0018AE07
		public BorderStyle LeftBorder
		{
			set
			{
				this.bitBorderLineStyle &= 65520;
				this.bitBorderLineStyle = (ushort)((BorderStyle)this.bitBorderLineStyle | value);
				this.bitIndentShrinkToFitMergeCell |= 8192;
			}
		}

		// Token: 0x17002284 RID: 8836
		// (set) Token: 0x0600696D RID: 26989 RVA: 0x0018CC3E File Offset: 0x0018AE3E
		public ushort LeftBorderColor
		{
			set
			{
				this.bitIndexColorPaletteBorder &= 65408;
				this.bitIndexColorPaletteBorder |= value;
			}
		}

		// Token: 0x17002285 RID: 8837
		// (get) Token: 0x0600696E RID: 26990 RVA: 0x0018CC62 File Offset: 0x0018AE62
		// (set) Token: 0x0600696F RID: 26991 RVA: 0x0018CC70 File Offset: 0x0018AE70
		public BiffCell.ReadingOrder ReadingOrder
		{
			get
			{
				return (BiffCell.ReadingOrder)(this.bitIndentShrinkToFitMergeCell & 192);
			}
			set
			{
				this.bitIndentShrinkToFitMergeCell &= 65343;
				this.bitIndentShrinkToFitMergeCell |= (ushort)value;
			}
		}

		// Token: 0x17002286 RID: 8838
		// (set) Token: 0x06006970 RID: 26992 RVA: 0x0018CC95 File Offset: 0x0018AE95
		public BorderStyle RightBorder
		{
			set
			{
				this.bitBorderLineStyle &= 65295;
				this.bitBorderLineStyle = (ushort)((BorderStyle)this.bitBorderLineStyle | value << 4);
				this.bitIndentShrinkToFitMergeCell |= 8192;
			}
		}

		// Token: 0x17002287 RID: 8839
		// (set) Token: 0x06006971 RID: 26993 RVA: 0x0018CCCE File Offset: 0x0018AECE
		public ushort RightBorderColor
		{
			set
			{
				this.bitIndexColorPaletteBorder &= 49279;
				this.bitIndexColorPaletteBorder = (ushort)((int)this.bitIndexColorPaletteBorder | (int)value << 7);
			}
		}

		// Token: 0x17002288 RID: 8840
		// (get) Token: 0x06006972 RID: 26994 RVA: 0x0018CCF4 File Offset: 0x0018AEF4
		// (set) Token: 0x06006973 RID: 26995 RVA: 0x0018CD02 File Offset: 0x0018AF02
		public BiffCell.TextRotate TextRotate
		{
			get
			{
				return (BiffCell.TextRotate)(this.bitAlignWrapJustLastRotate & 65280);
			}
			set
			{
				this.bitAlignWrapJustLastRotate &= 255;
				this.bitAlignWrapJustLastRotate = (ushort)((BiffCell.TextRotate)this.bitAlignWrapJustLastRotate | value);
			}
		}

		// Token: 0x17002289 RID: 8841
		// (set) Token: 0x06006974 RID: 26996 RVA: 0x0018CD28 File Offset: 0x0018AF28
		public double RotationAngle
		{
			set
			{
				ushort num = XF.GetBiffAngle(value);
				num = (ushort)(num << 8);
				this.bitAlignWrapJustLastRotate &= 255;
				this.bitAlignWrapJustLastRotate |= num;
			}
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x0018CD64 File Offset: 0x0018AF64
		internal static ushort GetBiffAngle(double value)
		{
			double num = Math.Round(-value);
			num %= 360.0;
			if (num > 180.0)
			{
				num -= 360.0;
			}
			else if (num < -180.0)
			{
				num += 360.0;
			}
			if (num > 90.0)
			{
				num = 90.0;
			}
			else if (num < -90.0)
			{
				num = -90.0;
			}
			if (num < 0.0)
			{
				num = -num;
				num += 90.0;
			}
			return (ushort)num;
		}

		// Token: 0x1700228A RID: 8842
		// (set) Token: 0x06006976 RID: 26998 RVA: 0x0018CE04 File Offset: 0x0018B004
		public BorderStyle TopBorder
		{
			set
			{
				this.bitBorderLineStyle &= 61695;
				this.bitBorderLineStyle = (ushort)((BorderStyle)this.bitBorderLineStyle | value << 8);
				this.bitIndentShrinkToFitMergeCell |= 8192;
			}
		}

		// Token: 0x1700228B RID: 8843
		// (set) Token: 0x06006977 RID: 26999 RVA: 0x0018CE3D File Offset: 0x0018B03D
		public ushort TopBorderColor
		{
			set
			{
				this.bitIndexColorPaletteTopBottomDiag &= 4294967168U;
				this.bitIndexColorPaletteTopBottomDiag |= (uint)value;
			}
		}

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x06006978 RID: 27000 RVA: 0x0018CE5C File Offset: 0x0018B05C
		// (set) Token: 0x06006979 RID: 27001 RVA: 0x0018CE67 File Offset: 0x0018B067
		public BiffCell.VerticalAlignments VerticalAlignment
		{
			get
			{
				return (BiffCell.VerticalAlignments)(this.bitAlignWrapJustLastRotate & 112);
			}
			set
			{
				this.bitAlignWrapJustLastRotate &= 65423;
				this.bitAlignWrapJustLastRotate = (ushort)((BiffCell.VerticalAlignments)this.bitAlignWrapJustLastRotate | value);
			}
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x0600697A RID: 27002 RVA: 0x0018CE8B File Offset: 0x0018B08B
		// (set) Token: 0x0600697B RID: 27003 RVA: 0x0018CE9B File Offset: 0x0018B09B
		public bool WrapText
		{
			get
			{
				return (this.bitAlignWrapJustLastRotate & 8) == 1;
			}
			set
			{
				if (value)
				{
					this.bitAlignWrapJustLastRotate |= 8;
					return;
				}
				this.bitAlignWrapJustLastRotate &= 247;
			}
		}

		// Token: 0x04001C72 RID: 7282
		private const ushort type = 224;

		// Token: 0x04001C73 RID: 7283
		private const ushort length = 20;

		// Token: 0x04001C74 RID: 7284
		private const ushort RTLMask = 128;

		// Token: 0x04001C75 RID: 7285
		private const ushort LTRMask = 64;

		// Token: 0x04001C76 RID: 7286
		private ushort ifnt;

		// Token: 0x04001C77 RID: 7287
		private ushort ifmt;

		// Token: 0x04001C78 RID: 7288
		private ushort bitLockedHiddenStyle;

		// Token: 0x04001C79 RID: 7289
		private ushort bitAlignWrapJustLastRotate;

		// Token: 0x04001C7A RID: 7290
		private ushort bitIndentShrinkToFitMergeCell;

		// Token: 0x04001C7B RID: 7291
		private ushort bitBorderLineStyle;

		// Token: 0x04001C7C RID: 7292
		private ushort bitIndexColorPaletteBorder;

		// Token: 0x04001C7D RID: 7293
		private uint bitIndexColorPaletteTopBottomDiag;

		// Token: 0x04001C7E RID: 7294
		private ushort bitIndexColorPaletteFill;
	}
}
