using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02000F74 RID: 3956
	public class RadFilterExpressionPreviewProvider : RadFilterQueryProvider
	{
		// Token: 0x17002FDD RID: 12253
		// (get) Token: 0x0600978E RID: 38798 RVA: 0x0021FA5B File Offset: 0x0021DC5B
		public RadFilter OwnerFilter
		{
			get
			{
				return this._ownerFilter;
			}
		}

		// Token: 0x17002FDE RID: 12254
		// (get) Token: 0x0600978F RID: 38799 RVA: 0x0021FA63 File Offset: 0x0021DC63
		public FilterStrings Localization
		{
			get
			{
				return this.OwnerFilter.Localization;
			}
		}

		// Token: 0x06009790 RID: 38800 RVA: 0x0021FB90 File Offset: 0x0021DD90
		public RadFilterExpressionPreviewProvider(RadFilter ownerFilter)
		{
			this._ownerFilter = ownerFilter;
			this.Expression = new StringBuilder();
		}

		// Token: 0x06009791 RID: 38801 RVA: 0x0021FBFC File Offset: 0x0021DDFC
		private string ValueFormatter(RadFilterDataFieldEditor editor, ArrayList values)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object arg in values)
			{
				stringBuilder.AppendFormat(editor.PreviewDataFormat, arg);
				stringBuilder.Append(",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return string.Format("<span class=\"rfVal\">{0}</span>", stringBuilder);
		}

		// Token: 0x17002FDF RID: 12255
		// (get) Token: 0x06009792 RID: 38802 RVA: 0x0021FC80 File Offset: 0x0021DE80
		public override IList<RadFilterFunction> SupportedFilterFunctions
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002FE0 RID: 12256
		// (get) Token: 0x06009793 RID: 38803 RVA: 0x0021FC83 File Offset: 0x0021DE83
		public override IList<RadFilterGroupOperation> SupportedGroupOperations
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06009794 RID: 38804 RVA: 0x0021FC88 File Offset: 0x0021DE88
		public override void ProcessGroup(RadFilterGroupExpression rootGroup)
		{
			if (this.Expression != null && this.Expression.Length > 0)
			{
				this.Expression = new StringBuilder();
			}
			this.ProcessGroupInternal(rootGroup);
			this.Expression = this.Expression.Replace(")", "<span class=\"rfBr\">)</span>");
		}

		// Token: 0x06009795 RID: 38805 RVA: 0x0021FCD8 File Offset: 0x0021DED8
		protected override string PrepareQuery(RadFilterNonGroupExpression expression)
		{
			RadFilterDataFieldEditor editor = this.OwnerFilter.FieldEditors.FindEditorForFieldName(expression.FieldName);
			return this.PrepareQueryForEditor(editor, expression);
		}

		// Token: 0x06009796 RID: 38806 RVA: 0x0021FD04 File Offset: 0x0021DF04
		protected virtual string PrepareQueryForEditor(RadFilterDataFieldEditor editor, RadFilterNonGroupExpression expression)
		{
			RadFilterEvaluationData radFilterEvaluationData = new RadFilterEvaluationData();
			radFilterEvaluationData.Expression = expression;
			string value = this.FieldNameFormatter(editor);
			IRadFilterValueExpression radFilterValueExpression = expression as IRadFilterValueExpression;
			if (radFilterValueExpression == null)
			{
				radFilterEvaluationData.Values = new ArrayList
				{
					value,
					this.OperatorFormatter(expression.FilterFunction, this.Localization)
				};
				radFilterEvaluationData.ExpressionFormat = "{0} {1}";
			}
			else
			{
				radFilterEvaluationData.Values = new ArrayList
				{
					value,
					this.OperatorFormatter(expression.FilterFunction, this.Localization),
					this.ValueFormatter(editor, radFilterValueExpression.Values)
				};
				radFilterEvaluationData.ExpressionFormat = "{0} {1} {2}";
			}
			if (base.OnExpressionEvaluated != null)
			{
				base.OnExpressionEvaluated(radFilterEvaluationData);
			}
			return radFilterEvaluationData.Format();
		}

		// Token: 0x06009797 RID: 38807 RVA: 0x0021FDE6 File Offset: 0x0021DFE6
		protected override string ConvertInGroupOperatorToString(RadFilterGroupOperation groupOperation)
		{
			return string.Format("<em>{0}</em>", base.ConvertInGroupOperatorToString(groupOperation));
		}

		// Token: 0x06009798 RID: 38808 RVA: 0x0021FDFC File Offset: 0x0021DFFC
		protected override string ConvertStartGroupOperatorToString(RadFilterGroupOperation groupOperation)
		{
			string arg = string.Empty;
			if (groupOperation == RadFilterGroupOperation.NotAnd || groupOperation == RadFilterGroupOperation.NotOr)
			{
				arg = string.Format("<em>{0}</em>", "NOT");
			}
			return string.Format("{0}<span class=\"rfBr\">(</span>", arg);
		}

		// Token: 0x04002B4F RID: 11087
		private RadFilter _ownerFilter;

		// Token: 0x04002B50 RID: 11088
		private TFunc<RadFilterDataFieldEditor, string> FieldNameFormatter = (RadFilterDataFieldEditor editor) => string.Format("<strong>{0}</strong>", string.IsNullOrEmpty(editor.DisplayName) ? editor.FieldName : editor.DisplayName);

		// Token: 0x04002B51 RID: 11089
		private TFunc<RadFilterFunction, FilterStrings, string> OperatorFormatter = delegate(RadFilterFunction fn, FilterStrings localization)
		{
			string result;
			switch (fn)
			{
			case RadFilterFunction.Contains:
				result = localization.PreviewProviderContainsText;
				break;
			case RadFilterFunction.DoesNotContain:
				result = localization.PreviewProviderDoesNotContainText;
				break;
			case RadFilterFunction.StartsWith:
				result = localization.PreviewProviderStartsWithText;
				break;
			case RadFilterFunction.EndsWith:
				result = localization.PreviewProviderEndsWithText;
				break;
			case RadFilterFunction.EqualTo:
				result = localization.PreviewProviderEqualToText;
				break;
			case RadFilterFunction.NotEqualTo:
				result = localization.PreviewProviderNotEqualToText;
				break;
			case RadFilterFunction.GreaterThan:
				result = localization.PreviewProviderGreaterThanText;
				break;
			case RadFilterFunction.LessThan:
				result = localization.PreviewProviderLessThanText;
				break;
			case RadFilterFunction.GreaterThanOrEqualTo:
				result = localization.PreviewProviderGreaterThanOrEqualToText;
				break;
			case RadFilterFunction.LessThanOrEqualTo:
				result = localization.PreviewProviderLessThanOrEqualToText;
				break;
			case RadFilterFunction.Between:
				result = localization.PreviewProviderBetweenText;
				break;
			case RadFilterFunction.NotBetween:
				result = localization.PreviewProviderNotBetweenText;
				break;
			case RadFilterFunction.IsEmpty:
				result = localization.PreviewProviderIsEmptyText;
				break;
			case RadFilterFunction.NotIsEmpty:
				result = localization.PreviewProviderNotIsEmptyText;
				break;
			case RadFilterFunction.IsNull:
				result = localization.PreviewProviderIsNullText;
				break;
			case RadFilterFunction.NotIsNull:
				result = localization.PreviewProviderNotIsNullText;
				break;
			default:
				result = string.Empty;
				break;
			}
			return result;
		};
	}
}
