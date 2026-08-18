using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B09 RID: 6921
	public class AutoFilterColumnElement : ElementBase
	{
		// Token: 0x17005170 RID: 20848
		// (get) Token: 0x06010BD7 RID: 68567 RVA: 0x003B8B88 File Offset: 0x003B6D88
		public virtual AutoFilterOrElement AutoFilterOr
		{
			get
			{
				if (this._autoFilterOr == null)
				{
					this._autoFilterOr = new AutoFilterOrElement();
				}
				return this._autoFilterOr;
			}
		}

		// Token: 0x17005171 RID: 20849
		// (get) Token: 0x06010BD8 RID: 68568 RVA: 0x003B8BA3 File Offset: 0x003B6DA3
		public virtual AutoFilterAndElement AutoFilterAnd
		{
			get
			{
				if (this._autoFilterAnd == null)
				{
					this._autoFilterAnd = new AutoFilterAndElement();
				}
				return this._autoFilterAnd;
			}
		}

		// Token: 0x17005172 RID: 20850
		// (get) Token: 0x06010BD9 RID: 68569 RVA: 0x003B8BBE File Offset: 0x003B6DBE
		public AutoFilterConditionElement ConditionElement
		{
			get
			{
				if (this._conditionElement == null)
				{
					this._conditionElement = new AutoFilterConditionElement();
				}
				return this._conditionElement;
			}
		}

		// Token: 0x17005173 RID: 20851
		// (get) Token: 0x06010BDA RID: 68570 RVA: 0x003B8BD9 File Offset: 0x003B6DD9
		// (set) Token: 0x06010BDB RID: 68571 RVA: 0x003B8BE1 File Offset: 0x003B6DE1
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x17005174 RID: 20852
		// (get) Token: 0x06010BDC RID: 68572 RVA: 0x003B8BEA File Offset: 0x003B6DEA
		// (set) Token: 0x06010BDD RID: 68573 RVA: 0x003B8BF2 File Offset: 0x003B6DF2
		public double Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17005175 RID: 20853
		// (get) Token: 0x06010BDE RID: 68574 RVA: 0x003B8BFB File Offset: 0x003B6DFB
		// (set) Token: 0x06010BDF RID: 68575 RVA: 0x003B8C03 File Offset: 0x003B6E03
		public AutoFilterOptions FilterType
		{
			get
			{
				return this._filterType;
			}
			set
			{
				this._filterType = value;
			}
		}

		// Token: 0x17005176 RID: 20854
		// (get) Token: 0x06010BE0 RID: 68576 RVA: 0x003B8C0C File Offset: 0x003B6E0C
		// (set) Token: 0x06010BE1 RID: 68577 RVA: 0x003B8C14 File Offset: 0x003B6E14
		public bool Hidden
		{
			get
			{
				return this._hidden;
			}
			set
			{
				this._hidden = value;
			}
		}

		// Token: 0x17005177 RID: 20855
		// (get) Token: 0x06010BE2 RID: 68578 RVA: 0x003B8C1D File Offset: 0x003B6E1D
		protected override string StartTag
		{
			get
			{
				return "<AutoFilterColumn{0}>";
			}
		}

		// Token: 0x17005178 RID: 20856
		// (get) Token: 0x06010BE3 RID: 68579 RVA: 0x003B8C24 File Offset: 0x003B6E24
		protected override string EndTag
		{
			get
			{
				return "</AutoFilterColumn>";
			}
		}

		// Token: 0x06010BE4 RID: 68580 RVA: 0x003B8C2C File Offset: 0x003B6E2C
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.Hidden)
			{
				base.Attributes.Add("x:Hidden", "1");
			}
			if (this.Index > 0)
			{
				base.Attributes.Add("x:Index", this.Index.ToString());
			}
			base.Attributes.Add("x:Type", Convert.ToString(this.FilterType));
			if ((this.FilterType == AutoFilterOptions.Top || this.FilterType == AutoFilterOptions.TopPercent || this.FilterType == AutoFilterOptions.Bottom || this.FilterType == AutoFilterOptions.BottomPercent) && this.Value == 0.0 && !base.Attributes.Contains("x:Value"))
			{
				throw new Exception("For this FilterType Value is required!");
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x06010BE5 RID: 68581 RVA: 0x003B8CF8 File Offset: 0x003B6EF8
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (!this.ConditionElement.IsEmpty)
			{
				((IElement)this.ConditionElement).Render(sb);
			}
			if (this.FilterType == AutoFilterOptions.Custom)
			{
				if (!this.AutoFilterOr.FirstFilterCondition.IsEmpty && !this.AutoFilterAnd.FilterCondition.IsEmpty)
				{
					throw new Exception("AutoFilterOr and AutoFilterAnd cannot be both set.");
				}
				if (!this.AutoFilterOr.FirstFilterCondition.IsEmpty)
				{
					((IElement)this.AutoFilterOr).Render(sb);
				}
				if (!this.AutoFilterAnd.FilterCondition.IsEmpty)
				{
					((IElement)this.AutoFilterAnd).Render(sb);
				}
			}
			base.RenderChildElements(sb);
		}

		// Token: 0x04004AB1 RID: 19121
		private bool _hidden;

		// Token: 0x04004AB2 RID: 19122
		private double _value;

		// Token: 0x04004AB3 RID: 19123
		private int _index;

		// Token: 0x04004AB4 RID: 19124
		private AutoFilterOptions _filterType;

		// Token: 0x04004AB5 RID: 19125
		private AutoFilterOrElement _autoFilterOr;

		// Token: 0x04004AB6 RID: 19126
		private AutoFilterAndElement _autoFilterAnd;

		// Token: 0x04004AB7 RID: 19127
		private AutoFilterConditionElement _conditionElement;
	}
}
