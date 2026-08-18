using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x020000C5 RID: 197
	public class Rectangle : Element, IElement
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x0002154C File Offset: 0x0002054C
		public Rectangle(float llx, float lly, float urx, float ury)
		{
			this.llx = llx;
			this.lly = lly;
			this.urx = urx;
			this.ury = ury;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000215BA File Offset: 0x000205BA
		public Rectangle(float urx, float ury) : this(0f, 0f, urx, ury)
		{
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000215CE File Offset: 0x000205CE
		public Rectangle(Rectangle rect) : this(rect.llx, rect.lly, rect.urx, rect.ury)
		{
			this.CloneNonPositionParameters(rect);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000215F8 File Offset: 0x000205F8
		public virtual void CloneNonPositionParameters(Rectangle rect)
		{
			this.rotation = rect.rotation;
			this.border = rect.border;
			this.borderWidth = rect.borderWidth;
			this.borderColor = rect.borderColor;
			this.backgroundColor = rect.backgroundColor;
			this.borderColorLeft = rect.borderColorLeft;
			this.borderColorRight = rect.borderColorRight;
			this.borderColorTop = rect.borderColorTop;
			this.borderColorBottom = rect.borderColorBottom;
			this.borderWidthLeft = rect.borderWidthLeft;
			this.borderWidthRight = rect.borderWidthRight;
			this.borderWidthTop = rect.borderWidthTop;
			this.borderWidthBottom = rect.borderWidthBottom;
			this.useVariableBorders = rect.useVariableBorders;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000216B0 File Offset: 0x000206B0
		public virtual void SoftCloneNonPositionParameters(Rectangle rect)
		{
			if (rect.rotation != 0)
			{
				this.rotation = rect.rotation;
			}
			if (rect.border != -1)
			{
				this.border = rect.border;
			}
			if (rect.borderWidth != -1f)
			{
				this.borderWidth = rect.borderWidth;
			}
			if (rect.borderColor != null)
			{
				this.borderColor = rect.borderColor;
			}
			if (rect.backgroundColor != null)
			{
				this.backgroundColor = rect.backgroundColor;
			}
			if (rect.borderColorLeft != null)
			{
				this.borderColorLeft = rect.borderColorLeft;
			}
			if (rect.borderColorRight != null)
			{
				this.borderColorRight = rect.borderColorRight;
			}
			if (rect.borderColorTop != null)
			{
				this.borderColorTop = rect.borderColorTop;
			}
			if (rect.borderColorBottom != null)
			{
				this.borderColorBottom = rect.borderColorBottom;
			}
			if (rect.borderWidthLeft != -1f)
			{
				this.borderWidthLeft = rect.borderWidthLeft;
			}
			if (rect.borderWidthRight != -1f)
			{
				this.borderWidthRight = rect.borderWidthRight;
			}
			if (rect.borderWidthTop != -1f)
			{
				this.borderWidthTop = rect.borderWidthTop;
			}
			if (rect.borderWidthBottom != -1f)
			{
				this.borderWidthBottom = rect.borderWidthBottom;
			}
			if (this.useVariableBorders)
			{
				this.useVariableBorders = rect.useVariableBorders;
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000217F0 File Offset: 0x000207F0
		public virtual bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				result = listener.Add(this);
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00021820 File Offset: 0x00020820
		public virtual int Type
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00021824 File Offset: 0x00020824
		public virtual List<Chunk> Chunks
		{
			get
			{
				return new List<Chunk>();
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0002182B File Offset: 0x0002082B
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002182E File Offset: 0x0002082E
		public virtual bool IsNestable()
		{
			return false;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00021834 File Offset: 0x00020834
		public virtual void Normalize()
		{
			if (this.llx > this.urx)
			{
				float num = this.llx;
				this.llx = this.urx;
				this.urx = num;
			}
			if (this.lly > this.ury)
			{
				float num2 = this.lly;
				this.lly = this.ury;
				this.ury = num2;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00021894 File Offset: 0x00020894
		public Rectangle GetRectangle(float top, float bottom)
		{
			Rectangle rectangle = new Rectangle(this);
			if (this.Top > top)
			{
				rectangle.Top = top;
				rectangle.Border = this.border - (this.border & 1);
			}
			if (this.Bottom < bottom)
			{
				rectangle.Bottom = bottom;
				rectangle.Border = this.border - (this.border & 2);
			}
			return rectangle;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000218F4 File Offset: 0x000208F4
		public Rectangle Rotate()
		{
			Rectangle rectangle = new Rectangle(this.lly, this.llx, this.ury, this.urx);
			rectangle.rotation = this.rotation + 90;
			rectangle.rotation %= 360;
			return rectangle;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00021941 File Offset: 0x00020941
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x00021949 File Offset: 0x00020949
		public virtual float Top
		{
			get
			{
				return this.ury;
			}
			set
			{
				this.ury = value;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00021952 File Offset: 0x00020952
		public virtual void EnableBorderSide(int side)
		{
			if (this.border == -1)
			{
				this.border = 0;
			}
			this.border |= side;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00021972 File Offset: 0x00020972
		public virtual void DisableBorderSide(int side)
		{
			if (this.border == -1)
			{
				this.border = 0;
			}
			this.border &= ~side;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00021993 File Offset: 0x00020993
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0002199B File Offset: 0x0002099B
		public virtual int Border
		{
			get
			{
				return this.border;
			}
			set
			{
				this.border = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000219A4 File Offset: 0x000209A4
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x000219C9 File Offset: 0x000209C9
		public virtual float GrayFill
		{
			get
			{
				if (this.backgroundColor is GrayColor)
				{
					return ((GrayColor)this.backgroundColor).Gray;
				}
				return 0f;
			}
			set
			{
				this.backgroundColor = new GrayColor(value);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000219D7 File Offset: 0x000209D7
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x000219DF File Offset: 0x000209DF
		public virtual float Left
		{
			get
			{
				return this.llx;
			}
			set
			{
				this.llx = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000219E8 File Offset: 0x000209E8
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x000219F0 File Offset: 0x000209F0
		public virtual float Right
		{
			get
			{
				return this.urx;
			}
			set
			{
				this.urx = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000219F9 File Offset: 0x000209F9
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00021A01 File Offset: 0x00020A01
		public virtual float Bottom
		{
			get
			{
				return this.lly;
			}
			set
			{
				this.lly = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00021A0A File Offset: 0x00020A0A
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00021A21 File Offset: 0x00020A21
		public virtual BaseColor BorderColorBottom
		{
			get
			{
				if (this.borderColorBottom == null)
				{
					return this.borderColor;
				}
				return this.borderColorBottom;
			}
			set
			{
				this.borderColorBottom = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00021A2A File Offset: 0x00020A2A
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00021A41 File Offset: 0x00020A41
		public virtual BaseColor BorderColorTop
		{
			get
			{
				if (this.borderColorTop == null)
				{
					return this.borderColor;
				}
				return this.borderColorTop;
			}
			set
			{
				this.borderColorTop = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00021A4A File Offset: 0x00020A4A
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00021A61 File Offset: 0x00020A61
		public virtual BaseColor BorderColorLeft
		{
			get
			{
				if (this.borderColorLeft == null)
				{
					return this.borderColor;
				}
				return this.borderColorLeft;
			}
			set
			{
				this.borderColorLeft = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00021A6A File Offset: 0x00020A6A
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00021A81 File Offset: 0x00020A81
		public virtual BaseColor BorderColorRight
		{
			get
			{
				if (this.borderColorRight == null)
				{
					return this.borderColor;
				}
				return this.borderColorRight;
			}
			set
			{
				this.borderColorRight = value;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00021A8A File Offset: 0x00020A8A
		public virtual float GetLeft(float margin)
		{
			return this.llx + margin;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00021A94 File Offset: 0x00020A94
		public virtual float GetRight(float margin)
		{
			return this.urx - margin;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00021A9E File Offset: 0x00020A9E
		public virtual float GetTop(float margin)
		{
			return this.ury - margin;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00021AA8 File Offset: 0x00020AA8
		public virtual float GetBottom(float margin)
		{
			return this.lly + margin;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00021AB2 File Offset: 0x00020AB2
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00021AC1 File Offset: 0x00020AC1
		public virtual float Width
		{
			get
			{
				return this.urx - this.llx;
			}
			set
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("the.width.cannot.be.set"));
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00021AD2 File Offset: 0x00020AD2
		public float Height
		{
			get
			{
				return this.ury - this.lly;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00021AE4 File Offset: 0x00020AE4
		public bool HasBorders()
		{
			switch (this.border)
			{
			case -1:
			case 0:
				return false;
			default:
				return this.borderWidth > 0f || this.borderWidthLeft > 0f || this.borderWidthRight > 0f || this.borderWidthTop > 0f || this.borderWidthBottom > 0f;
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00021B4F File Offset: 0x00020B4F
		public bool HasBorder(int type)
		{
			return this.border != -1 && (this.border & type) == type;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00021B67 File Offset: 0x00020B67
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x00021B6F File Offset: 0x00020B6F
		public virtual float BorderWidth
		{
			get
			{
				return this.borderWidth;
			}
			set
			{
				this.borderWidth = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00021B78 File Offset: 0x00020B78
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x00021B80 File Offset: 0x00020B80
		public virtual BaseColor BorderColor
		{
			get
			{
				return this.borderColor;
			}
			set
			{
				this.borderColor = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00021B89 File Offset: 0x00020B89
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00021B91 File Offset: 0x00020B91
		public virtual BaseColor BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}
			set
			{
				this.backgroundColor = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00021B9A File Offset: 0x00020B9A
		public int Rotation
		{
			get
			{
				return this.rotation;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00021BA2 File Offset: 0x00020BA2
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00021BB1 File Offset: 0x00020BB1
		public virtual float BorderWidthLeft
		{
			get
			{
				return this.GetVariableBorderWidth(this.borderWidthLeft, 4);
			}
			set
			{
				this.borderWidthLeft = value;
				this.UpdateBorderBasedOnWidth(value, 4);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00021BC2 File Offset: 0x00020BC2
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00021BD1 File Offset: 0x00020BD1
		public virtual float BorderWidthRight
		{
			get
			{
				return this.GetVariableBorderWidth(this.borderWidthRight, 8);
			}
			set
			{
				this.borderWidthRight = value;
				this.UpdateBorderBasedOnWidth(value, 8);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00021BE2 File Offset: 0x00020BE2
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00021BF1 File Offset: 0x00020BF1
		public virtual float BorderWidthTop
		{
			get
			{
				return this.GetVariableBorderWidth(this.borderWidthTop, 1);
			}
			set
			{
				this.borderWidthTop = value;
				this.UpdateBorderBasedOnWidth(value, 1);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00021C02 File Offset: 0x00020C02
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00021C11 File Offset: 0x00020C11
		public virtual float BorderWidthBottom
		{
			get
			{
				return this.GetVariableBorderWidth(this.borderWidthBottom, 2);
			}
			set
			{
				this.borderWidthBottom = value;
				this.UpdateBorderBasedOnWidth(value, 2);
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00021C22 File Offset: 0x00020C22
		private void UpdateBorderBasedOnWidth(float width, int side)
		{
			this.useVariableBorders = true;
			if (width > 0f)
			{
				this.EnableBorderSide(side);
				return;
			}
			this.DisableBorderSide(side);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00021C42 File Offset: 0x00020C42
		private float GetVariableBorderWidth(float variableWidthValue, int side)
		{
			if ((this.border & side) == 0)
			{
				return 0f;
			}
			if (variableWidthValue == -1f)
			{
				return this.borderWidth;
			}
			return variableWidthValue;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00021C64 File Offset: 0x00020C64
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00021C6C File Offset: 0x00020C6C
		public virtual bool UseVariableBorders
		{
			get
			{
				return this.useVariableBorders;
			}
			set
			{
				this.useVariableBorders = value;
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00021C78 File Offset: 0x00020C78
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Rectangle: ");
			stringBuilder.Append(this.Width);
			stringBuilder.Append('x');
			stringBuilder.Append(this.Height);
			stringBuilder.Append(" (rot: ");
			stringBuilder.Append(this.rotation);
			stringBuilder.Append(" degrees)");
			return stringBuilder.ToString();
		}

		// Token: 0x0400034D RID: 845
		public const int UNDEFINED = -1;

		// Token: 0x0400034E RID: 846
		public const int TOP_BORDER = 1;

		// Token: 0x0400034F RID: 847
		public const int BOTTOM_BORDER = 2;

		// Token: 0x04000350 RID: 848
		public const int LEFT_BORDER = 4;

		// Token: 0x04000351 RID: 849
		public const int RIGHT_BORDER = 8;

		// Token: 0x04000352 RID: 850
		public const int NO_BORDER = 0;

		// Token: 0x04000353 RID: 851
		public const int BOX = 15;

		// Token: 0x04000354 RID: 852
		protected float llx;

		// Token: 0x04000355 RID: 853
		protected float lly;

		// Token: 0x04000356 RID: 854
		protected float urx;

		// Token: 0x04000357 RID: 855
		protected float ury;

		// Token: 0x04000358 RID: 856
		protected int border = -1;

		// Token: 0x04000359 RID: 857
		protected float borderWidth = -1f;

		// Token: 0x0400035A RID: 858
		protected BaseColor borderColor;

		// Token: 0x0400035B RID: 859
		protected BaseColor borderColorLeft;

		// Token: 0x0400035C RID: 860
		protected BaseColor borderColorRight;

		// Token: 0x0400035D RID: 861
		protected BaseColor borderColorTop;

		// Token: 0x0400035E RID: 862
		protected BaseColor borderColorBottom;

		// Token: 0x0400035F RID: 863
		protected float borderWidthLeft = -1f;

		// Token: 0x04000360 RID: 864
		protected float borderWidthRight = -1f;

		// Token: 0x04000361 RID: 865
		protected float borderWidthTop = -1f;

		// Token: 0x04000362 RID: 866
		protected float borderWidthBottom = -1f;

		// Token: 0x04000363 RID: 867
		protected bool useVariableBorders;

		// Token: 0x04000364 RID: 868
		protected BaseColor backgroundColor;

		// Token: 0x04000365 RID: 869
		protected int rotation;
	}
}
