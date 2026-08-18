using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020018A6 RID: 6310
	public class RadFilterItemBuilder
	{
		// Token: 0x0600F413 RID: 62483 RVA: 0x003782D8 File Offset: 0x003764D8
		public IEnumerable<RadFilterExpressionItem> BuildNextItem(RadFilterGroupExpression groupExpression, RadFilterGroupExpressionItem inContext)
		{
			if (this._currentItem == null)
			{
				this._currentItem = new RadFilterGroupExpressionItem(groupExpression, true);
				yield return this._currentItem;
			}
			else
			{
				this._currentItem = inContext;
			}
			RadFilterExpressionItem item = null;
			foreach (RadFilterExpression expression in groupExpression.Expressions)
			{
				if (expression.FilterFunction == RadFilterFunction.Group)
				{
					RadFilterGroupExpression groupExoression = (RadFilterGroupExpression)expression;
					item = new RadFilterGroupExpressionItem(groupExoression, false);
					yield return item;
					RadFilterGroupExpressionItem previousItemCreated = this._currentItem;
					foreach (RadFilterExpressionItem expressionItem in this.BuildNextItem(groupExoression, (RadFilterGroupExpressionItem)item))
					{
						yield return expressionItem;
					}
					this._currentItem = previousItemCreated;
				}
				else
				{
					item = new RadFilterSingleExpressionItem((RadFilterNonGroupExpression)expression);
					yield return item;
				}
			}
			if (item != null)
			{
				item.IsLastItem = true;
			}
			yield break;
		}

		// Token: 0x0600F414 RID: 62484 RVA: 0x00378303 File Offset: 0x00376503
		public void AddItem(RadFilterExpressionItem item)
		{
			this._currentItem.AddChildItem(item);
		}

		// Token: 0x04004605 RID: 17925
		private RadFilterGroupExpressionItem _currentItem;
	}
}
