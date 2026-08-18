using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200196C RID: 6508
	internal abstract class ListViewFilterExpressionBuilder
	{
		// Token: 0x0600FC21 RID: 64545 RVA: 0x0038CF3D File Offset: 0x0038B13D
		public ListViewFilterExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions) : this(expressions, RadListViewGroupFilterOperator.And)
		{
		}

		// Token: 0x0600FC22 RID: 64546 RVA: 0x0038CF47 File Offset: 0x0038B147
		public ListViewFilterExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions, RadListViewGroupFilterOperator groupOperator)
		{
			this._expressions = expressions;
			this.GroupOperator = groupOperator;
		}

		// Token: 0x17004C29 RID: 19497
		// (get) Token: 0x0600FC23 RID: 64547 RVA: 0x0038CF5D File Offset: 0x0038B15D
		// (set) Token: 0x0600FC24 RID: 64548 RVA: 0x0038CF65 File Offset: 0x0038B165
		private protected virtual RadListViewGroupFilterOperator GroupOperator { protected get; private set; }

		// Token: 0x17004C2A RID: 19498
		// (get) Token: 0x0600FC25 RID: 64549
		protected abstract TFunc<RadListViewFilterExpression, string> Convertor { get; }

		// Token: 0x0600FC26 RID: 64550 RVA: 0x0038CF70 File Offset: 0x0038B170
		public string Convert()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RadListViewFilterExpression arg in this._expressions)
			{
				string text = this.Convertor(arg);
				if (!string.IsNullOrEmpty(text))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendFormat(" {0} ", this.GetLogicalOperatorAsString());
					}
					stringBuilder.Append(text.Trim());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600FC27 RID: 64551 RVA: 0x0038D000 File Offset: 0x0038B200
		protected virtual string GetLogicalOperatorAsString()
		{
			if (this.GroupOperator != RadListViewGroupFilterOperator.And)
			{
				return "OR";
			}
			return "AND";
		}

		// Token: 0x040047B9 RID: 18361
		private IEnumerable<RadListViewFilterExpression> _expressions;
	}
}
