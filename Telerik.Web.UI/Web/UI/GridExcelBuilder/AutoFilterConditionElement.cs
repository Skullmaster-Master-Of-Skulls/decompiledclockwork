using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B0B RID: 6923
	public class AutoFilterConditionElement : ElementBase
	{
		// Token: 0x06010BE7 RID: 68583 RVA: 0x003B8DA1 File Offset: 0x003B6FA1
		public AutoFilterConditionElement(string value)
		{
			this._value = value;
		}

		// Token: 0x06010BE8 RID: 68584 RVA: 0x003B8DB0 File Offset: 0x003B6FB0
		public AutoFilterConditionElement() : this(string.Empty)
		{
		}

		// Token: 0x17005179 RID: 20857
		// (get) Token: 0x06010BE9 RID: 68585 RVA: 0x003B8DBD File Offset: 0x003B6FBD
		// (set) Token: 0x06010BEA RID: 68586 RVA: 0x003B8DC5 File Offset: 0x003B6FC5
		public FilterConditionOperator Operator
		{
			get
			{
				return this._operator;
			}
			set
			{
				this._operator = value;
			}
		}

		// Token: 0x1700517A RID: 20858
		// (get) Token: 0x06010BEB RID: 68587 RVA: 0x003B8DCE File Offset: 0x003B6FCE
		// (set) Token: 0x06010BEC RID: 68588 RVA: 0x003B8DE9 File Offset: 0x003B6FE9
		public virtual string Value
		{
			get
			{
				if (this._value == null)
				{
					this._value = string.Empty;
				}
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x1700517B RID: 20859
		// (get) Token: 0x06010BED RID: 68589 RVA: 0x003B8DF2 File Offset: 0x003B6FF2
		protected override string StartTag
		{
			get
			{
				return "<AutoFilterCondition{0}>";
			}
		}

		// Token: 0x1700517C RID: 20860
		// (get) Token: 0x06010BEE RID: 68590 RVA: 0x003B8DF9 File Offset: 0x003B6FF9
		protected override string EndTag
		{
			get
			{
				return "</AutoFilterCondition>";
			}
		}

		// Token: 0x1700517D RID: 20861
		// (get) Token: 0x06010BEF RID: 68591 RVA: 0x003B8E00 File Offset: 0x003B7000
		public virtual bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.Value.Trim()) && base.Attributes.Contains("x:Value");
			}
		}

		// Token: 0x1700517E RID: 20862
		// (get) Token: 0x06010BF0 RID: 68592 RVA: 0x003B8E26 File Offset: 0x003B7026
		public override IElementsCollection InnerElements
		{
			get
			{
				return new ElementsCollection();
			}
		}

		// Token: 0x06010BF1 RID: 68593 RVA: 0x003B8E30 File Offset: 0x003B7030
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.IsEmpty)
			{
				throw new Exception("Value cannot be blank");
			}
			if (string.IsNullOrEmpty(this.Value.Trim()))
			{
				base.Attributes.Add("x:Value", this.Value.Trim());
			}
			base.Attributes.Add("x:Operator", Convert.ToString(this.Operator));
			base.AppendAttributes(sb);
		}

		// Token: 0x04004ABF RID: 19135
		private FilterConditionOperator _operator;

		// Token: 0x04004AC0 RID: 19136
		private string _value;
	}
}
