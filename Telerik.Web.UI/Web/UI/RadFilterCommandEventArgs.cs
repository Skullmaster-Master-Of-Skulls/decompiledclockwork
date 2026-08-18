using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018A1 RID: 6305
	public class RadFilterCommandEventArgs : CommandEventArgs, IRadFilterCommandEvent
	{
		// Token: 0x1700497A RID: 18810
		// (get) Token: 0x0600F3DD RID: 62429 RVA: 0x003776A5 File Offset: 0x003758A5
		// (set) Token: 0x0600F3DE RID: 62430 RVA: 0x003776AD File Offset: 0x003758AD
		public virtual RadFilterExpressionItem ExpressionItem { get; set; }

		// Token: 0x1700497B RID: 18811
		// (get) Token: 0x0600F3DF RID: 62431 RVA: 0x003776B6 File Offset: 0x003758B6
		// (set) Token: 0x0600F3E0 RID: 62432 RVA: 0x003776BE File Offset: 0x003758BE
		public object EventSource { get; set; }

		// Token: 0x0600F3E1 RID: 62433 RVA: 0x003776C7 File Offset: 0x003758C7
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterCommandEventArgs(RadFilterExpressionItem expressionItem, object eventSource, CommandEventArgs args) : base(args)
		{
			this.ExpressionItem = expressionItem;
			this.EventSource = eventSource;
		}

		// Token: 0x0600F3E2 RID: 62434 RVA: 0x003776DE File Offset: 0x003758DE
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal RadFilterCommandEventArgs(RadFilterExpressionItem expressionItem, object eventSource, string name, object argument) : base(name, argument)
		{
			this.ExpressionItem = expressionItem;
			this.EventSource = eventSource;
		}

		// Token: 0x1700497C RID: 18812
		// (get) Token: 0x0600F3E3 RID: 62435 RVA: 0x003776F7 File Offset: 0x003758F7
		// (set) Token: 0x0600F3E4 RID: 62436 RVA: 0x003776FF File Offset: 0x003758FF
		public virtual bool Canceled { get; set; }

		// Token: 0x0600F3E5 RID: 62437 RVA: 0x00377708 File Offset: 0x00375908
		public void ExecuteCommand(object source)
		{
			RadFilter ownerFilter = this.ExpressionItem.OwnerFilter;
			if (string.Compare(base.CommandName, "AddGroup", true) == 0)
			{
				ownerFilter.AddChildExpression(this.ExpressionItem as RadFilterGroupExpressionItem, true);
			}
			if (string.Compare(base.CommandName, "AddExpression", true) == 0)
			{
				ownerFilter.AddChildExpression(this.ExpressionItem as RadFilterGroupExpressionItem, false);
			}
			if (string.Compare(base.CommandName, "RemoveExpression", true) == 0)
			{
				ownerFilter.RemoveFilterExpression(this.ExpressionItem as RadFilterSingleExpressionItem, true);
			}
			if (string.Compare(base.CommandName, "RemoveGroup", true) == 0)
			{
				ownerFilter.RemoveGroupFilterExpression(this.ExpressionItem as RadFilterGroupExpressionItem, true);
			}
			if (string.Compare(base.CommandName, "ChangeGroupOperator", true) == 0)
			{
				RadFilterGroupOperation radFilterGroupOperation = (RadFilterGroupOperation)Enum.Parse(typeof(RadFilterGroupOperation), (string)base.CommandArgument);
				if (!ownerFilter.isGroupSupported(radFilterGroupOperation))
				{
					return;
				}
				ownerFilter.ChangeGroupOperator(this.ExpressionItem as RadFilterGroupExpressionItem, radFilterGroupOperation, true);
			}
			if (string.Compare(base.CommandName, "ChangeFilterFunction", true) == 0)
			{
				RadFilterFunction function = (RadFilterFunction)Enum.Parse(typeof(RadFilterFunction), (string)base.CommandArgument);
				if (!ownerFilter.isFilterFunctionSupported(function))
				{
					return;
				}
				ownerFilter.ChangeFilterFunction(this.ExpressionItem as RadFilterSingleExpressionItem, function, true);
			}
			if (string.Compare(base.CommandName, "ChangeExpressionFieldName", true) == 0)
			{
				ownerFilter.ChangeExpressionFieldName(this.ExpressionItem as RadFilterSingleExpressionItem, (string)base.CommandArgument);
			}
		}
	}
}
