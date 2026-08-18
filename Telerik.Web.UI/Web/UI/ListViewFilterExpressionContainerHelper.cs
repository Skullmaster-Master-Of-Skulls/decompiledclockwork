using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001972 RID: 6514
	[Serializable]
	internal class ListViewFilterExpressionContainerHelper : IRadListViewFilterExpressionContainer
	{
		// Token: 0x0600FC3C RID: 64572 RVA: 0x0038D310 File Offset: 0x0038B510
		public ListViewFilterExpressionContainerHelper(IRadListViewFilterExpressionContainer container)
		{
			this._container = container;
		}

		// Token: 0x0600FC3D RID: 64573 RVA: 0x0038D320 File Offset: 0x0038B520
		public RadListViewFilterExpression FindByFieldName(string fieldName)
		{
			if (string.IsNullOrEmpty(fieldName))
			{
				throw new ArgumentOutOfRangeException("fieldName", "FieldName cannot be null or empty string!");
			}
			Stack<RadListViewFilterExpression> stack = new Stack<RadListViewFilterExpression>();
			for (int i = this.Expressions.Count - 1; i >= 0; i--)
			{
				stack.Push(this.Expressions[i]);
			}
			while (stack.Count > 0)
			{
				RadListViewFilterExpression radListViewFilterExpression = stack.Pop();
				if (radListViewFilterExpression.FieldName == fieldName)
				{
					return radListViewFilterExpression;
				}
				IRadListViewFilterExpressionContainer radListViewFilterExpressionContainer = radListViewFilterExpression as IRadListViewFilterExpressionContainer;
				if (radListViewFilterExpressionContainer != null)
				{
					for (int j = radListViewFilterExpressionContainer.Expressions.Count - 1; j >= 0; j--)
					{
						stack.Push(radListViewFilterExpressionContainer.Expressions[j]);
					}
				}
			}
			return null;
		}

		// Token: 0x17004C2F RID: 19503
		// (get) Token: 0x0600FC3E RID: 64574 RVA: 0x0038D3D1 File Offset: 0x0038B5D1
		public IList<RadListViewFilterExpression> Expressions
		{
			get
			{
				return this._container.Expressions;
			}
		}

		// Token: 0x040047C0 RID: 18368
		private IRadListViewFilterExpressionContainer _container;
	}
}
