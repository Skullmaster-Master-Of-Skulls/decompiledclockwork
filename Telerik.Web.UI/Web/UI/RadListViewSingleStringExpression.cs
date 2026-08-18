using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200197A RID: 6522
	[Browsable(false)]
	public abstract class RadListViewSingleStringExpression : RadListViewSingleValueExpression<string>
	{
		// Token: 0x0600FC87 RID: 64647 RVA: 0x0038DC58 File Offset: 0x0038BE58
		internal RadListViewSingleStringExpression()
		{
		}

		// Token: 0x0600FC88 RID: 64648 RVA: 0x0038DC60 File Offset: 0x0038BE60
		public RadListViewSingleStringExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C44 RID: 19524
		// (get) Token: 0x0600FC89 RID: 64649 RVA: 0x0038DC69 File Offset: 0x0038BE69
		protected override RadListViewSingleValueExpression<string>.ValueFormatter EnitySqlFormatter
		{
			get
			{
				if (this._enitySqlFormatter == null)
				{
					this._enitySqlFormatter = new RadListViewSingleStringExpression.ContainsFormatter();
				}
				return this._enitySqlFormatter;
			}
		}

		// Token: 0x17004C45 RID: 19525
		// (get) Token: 0x0600FC8A RID: 64650 RVA: 0x0038DC84 File Offset: 0x0038BE84
		protected override RadListViewSingleValueExpression<string>.ValueFormatter OqlFormatter
		{
			get
			{
				if (this._oqlFormater == null)
				{
					this._oqlFormater = new RadListViewSingleStringExpression.OqlContainsFormatter();
				}
				return this._oqlFormater;
			}
		}

		// Token: 0x0600FC8B RID: 64651 RVA: 0x0038DC9F File Offset: 0x0038BE9F
		public override string ToDynamicLinq()
		{
			if (this.CurrentValue == null)
			{
				this.CurrentValue = "";
			}
			return base.ToDynamicLinq();
		}

		// Token: 0x0600FC8C RID: 64652 RVA: 0x0038DCBA File Offset: 0x0038BEBA
		public override string ToEntitySQL()
		{
			if (this.CurrentValue == null)
			{
				this.CurrentValue = "";
			}
			return base.ToEntitySQL();
		}

		// Token: 0x040047C7 RID: 18375
		private RadListViewSingleValueExpression<string>.ValueFormatter _enitySqlFormatter;

		// Token: 0x040047C8 RID: 18376
		private RadListViewSingleValueExpression<string>.ValueFormatter _oqlFormater;

		// Token: 0x0200197B RID: 6523
		private class ContainsFormatter : RadListViewSingleValueExpression<string>.EnitytSqlValueFormatter
		{
			// Token: 0x0600FC8D RID: 64653 RVA: 0x0038DCD5 File Offset: 0x0038BED5
			public override string PrepareValue(string value)
			{
				return value.Replace("'", "''");
			}
		}

		// Token: 0x0200197C RID: 6524
		private class OqlContainsFormatter : RadListViewSingleValueExpression<string>.OqlValueFormatter
		{
			// Token: 0x0600FC8F RID: 64655 RVA: 0x0038DCEF File Offset: 0x0038BEEF
			public override string PrepareValue(string value)
			{
				return value.Replace("'", "''");
			}
		}
	}
}
