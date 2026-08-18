using System;
using System.Drawing;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B12 RID: 6930
	public class BorderStyles : ElementBase
	{
		// Token: 0x17005196 RID: 20886
		// (get) Token: 0x06010C26 RID: 68646 RVA: 0x003B9236 File Offset: 0x003B7436
		// (set) Token: 0x06010C27 RID: 68647 RVA: 0x003B923E File Offset: 0x003B743E
		public double Weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				if (value < 0.0 || value > 3.0)
				{
					throw new ArgumentOutOfRangeException("Weight must be between 0 and 3");
				}
				this._weight = value;
			}
		}

		// Token: 0x17005197 RID: 20887
		// (get) Token: 0x06010C28 RID: 68648 RVA: 0x003B926A File Offset: 0x003B746A
		// (set) Token: 0x06010C29 RID: 68649 RVA: 0x003B9272 File Offset: 0x003B7472
		public LineStyle LineStyle
		{
			get
			{
				return this._lineStyle;
			}
			set
			{
				this._lineStyle = value;
			}
		}

		// Token: 0x17005198 RID: 20888
		// (get) Token: 0x06010C2A RID: 68650 RVA: 0x003B927B File Offset: 0x003B747B
		// (set) Token: 0x06010C2B RID: 68651 RVA: 0x003B9283 File Offset: 0x003B7483
		public PositionType PositionType
		{
			get
			{
				return this._positionType;
			}
			set
			{
				this._positionType = value;
			}
		}

		// Token: 0x17005199 RID: 20889
		// (get) Token: 0x06010C2C RID: 68652 RVA: 0x003B928C File Offset: 0x003B748C
		// (set) Token: 0x06010C2D RID: 68653 RVA: 0x003B9294 File Offset: 0x003B7494
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x06010C2E RID: 68654 RVA: 0x003B929D File Offset: 0x003B749D
		public BorderStyles() : this(PositionType.None)
		{
		}

		// Token: 0x06010C2F RID: 68655 RVA: 0x003B92A6 File Offset: 0x003B74A6
		public BorderStyles(PositionType positionType)
		{
			this._positionType = positionType;
			this._lineStyle = LineStyle.None;
			this._weight = 0.0;
		}

		// Token: 0x1700519A RID: 20890
		// (get) Token: 0x06010C30 RID: 68656 RVA: 0x003B92D6 File Offset: 0x003B74D6
		protected override string StartTag
		{
			get
			{
				return "<Border{0}>";
			}
		}

		// Token: 0x1700519B RID: 20891
		// (get) Token: 0x06010C31 RID: 68657 RVA: 0x003B92DD File Offset: 0x003B74DD
		protected override string EndTag
		{
			get
			{
				return "</Border>";
			}
		}

		// Token: 0x06010C32 RID: 68658 RVA: 0x003B92E4 File Offset: 0x003B74E4
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.PositionType == PositionType.None && (!base.Attributes.Contains("ss:Position") || base.Attributes["ss:Position"].Trim().Length == 0))
			{
				throw new Exception("PositionType is required and cannot be blank or None");
			}
			if (this.PositionType != PositionType.None)
			{
				base.Attributes.Add("ss:Position", this.PositionType.ToString());
			}
			if (this.Color != Color.Empty)
			{
				base.Attributes.Add("ss:Color", Utils.ConvertColor(this.Color));
			}
			if (this.LineStyle != LineStyle.None)
			{
				base.Attributes.Add("ss:LineStyle", this.LineStyle.ToString());
			}
			if (this.Weight > 0.0)
			{
				base.Attributes.Add("ss:Weight", this.Weight.ToString());
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04004AD5 RID: 19157
		private PositionType _positionType;

		// Token: 0x04004AD6 RID: 19158
		private LineStyle _lineStyle;

		// Token: 0x04004AD7 RID: 19159
		private Color _color = Color.Empty;

		// Token: 0x04004AD8 RID: 19160
		private double _weight;
	}
}
