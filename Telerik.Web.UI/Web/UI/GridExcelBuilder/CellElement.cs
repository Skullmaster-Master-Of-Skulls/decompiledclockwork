using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B13 RID: 6931
	public class CellElement : ElementBase
	{
		// Token: 0x1700519C RID: 20892
		// (get) Token: 0x06010C33 RID: 68659 RVA: 0x003B93E5 File Offset: 0x003B75E5
		// (set) Token: 0x06010C34 RID: 68660 RVA: 0x003B93ED File Offset: 0x003B75ED
		public string HRef { get; set; }

		// Token: 0x1700519D RID: 20893
		// (get) Token: 0x06010C35 RID: 68661 RVA: 0x003B93F6 File Offset: 0x003B75F6
		// (set) Token: 0x06010C36 RID: 68662 RVA: 0x003B93FE File Offset: 0x003B75FE
		public int MergeDown
		{
			get
			{
				return this._mergeDown;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MergeDown cannot be less then 0");
				}
				this._mergeDown = value;
			}
		}

		// Token: 0x1700519E RID: 20894
		// (get) Token: 0x06010C37 RID: 68663 RVA: 0x003B9416 File Offset: 0x003B7616
		// (set) Token: 0x06010C38 RID: 68664 RVA: 0x003B941E File Offset: 0x003B761E
		public int MergeAcross
		{
			get
			{
				return this._mergeAcross;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MergeAcross cannot be less then 0");
				}
				this._mergeAcross = value;
			}
		}

		// Token: 0x1700519F RID: 20895
		// (get) Token: 0x06010C39 RID: 68665 RVA: 0x003B9436 File Offset: 0x003B7636
		public virtual DataElement Data
		{
			get
			{
				if (this._dataElement == null)
				{
					this._dataElement = new DataElement();
				}
				return this._dataElement;
			}
		}

		// Token: 0x170051A0 RID: 20896
		// (get) Token: 0x06010C3A RID: 68666 RVA: 0x003B9451 File Offset: 0x003B7651
		// (set) Token: 0x06010C3B RID: 68667 RVA: 0x003B946C File Offset: 0x003B766C
		public virtual string StyleValue
		{
			get
			{
				if (this._styleValue == null)
				{
					this._styleValue = string.Empty;
				}
				return this._styleValue;
			}
			set
			{
				this._styleValue = value;
			}
		}

		// Token: 0x170051A1 RID: 20897
		// (get) Token: 0x06010C3C RID: 68668 RVA: 0x003B9475 File Offset: 0x003B7675
		protected override string StartTag
		{
			get
			{
				return "<Cell{0}>";
			}
		}

		// Token: 0x170051A2 RID: 20898
		// (get) Token: 0x06010C3D RID: 68669 RVA: 0x003B947C File Offset: 0x003B767C
		protected override string EndTag
		{
			get
			{
				return "</Cell>";
			}
		}

		// Token: 0x170051A3 RID: 20899
		// (get) Token: 0x06010C3E RID: 68670 RVA: 0x003B9483 File Offset: 0x003B7683
		// (set) Token: 0x06010C3F RID: 68671 RVA: 0x003B949E File Offset: 0x003B769E
		public string ColumnName
		{
			get
			{
				if (this._columnName == null)
				{
					this._columnName = string.Empty;
				}
				return this._columnName;
			}
			set
			{
				this._columnName = value;
			}
		}

		// Token: 0x06010C40 RID: 68672 RVA: 0x003B94A8 File Offset: 0x003B76A8
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.StyleValue.Trim().Length > 0)
			{
				base.Attributes.Add("ss:StyleID", this.StyleValue.Trim());
			}
			if (this.MergeAcross > 0)
			{
				base.Attributes.Add("ss:MergeAcross", this.MergeAcross.ToString());
			}
			if (this.MergeDown > 0)
			{
				base.Attributes.Add("ss:MergeDown", this.MergeDown.ToString());
			}
			if (this.HRef != null)
			{
				base.Attributes.Add("ss:HRef", this.HRef);
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x06010C41 RID: 68673 RVA: 0x003B9556 File Offset: 0x003B7756
		protected override void RenderChildElements(StringBuilder sb)
		{
			this.Data.Render(sb);
			base.RenderChildElements(sb);
		}

		// Token: 0x04004AD9 RID: 19161
		private int _mergeAcross;

		// Token: 0x04004ADA RID: 19162
		private int _mergeDown;

		// Token: 0x04004ADB RID: 19163
		private string _styleValue;

		// Token: 0x04004ADC RID: 19164
		private DataElement _dataElement;

		// Token: 0x04004ADD RID: 19165
		private string _columnName;
	}
}
