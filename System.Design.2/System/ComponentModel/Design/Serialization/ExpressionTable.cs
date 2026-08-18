using System;
using System.CodeDom;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001E2 RID: 482
	internal sealed class ExpressionTable
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00067D04 File Offset: 0x00065F04
		private Hashtable Expressions
		{
			get
			{
				if (this._expressions == null)
				{
					this._expressions = new Hashtable(new ExpressionTable.ReferenceComparer());
				}
				return this._expressions;
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00067D24 File Offset: 0x00065F24
		internal void SetExpression(object value, CodeExpression expression, bool isPreset)
		{
			this.Expressions[value] = new ExpressionTable.ExpressionInfo(expression, isPreset);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00067D3C File Offset: 0x00065F3C
		internal CodeExpression GetExpression(object value)
		{
			CodeExpression result = null;
			ExpressionTable.ExpressionInfo expressionInfo = this.Expressions[value] as ExpressionTable.ExpressionInfo;
			if (expressionInfo != null)
			{
				result = expressionInfo.Expression;
			}
			return result;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00067D68 File Offset: 0x00065F68
		internal bool ContainsPresetExpression(object value)
		{
			ExpressionTable.ExpressionInfo expressionInfo = this.Expressions[value] as ExpressionTable.ExpressionInfo;
			return expressionInfo != null && expressionInfo.IsPreset;
		}

		// Token: 0x040009F8 RID: 2552
		private Hashtable _expressions;

		// Token: 0x020004A7 RID: 1191
		private class ExpressionInfo
		{
			// Token: 0x06002BAC RID: 11180 RVA: 0x00104B58 File Offset: 0x00102D58
			internal ExpressionInfo(CodeExpression expression, bool isPreset)
			{
				this._expression = expression;
				this._isPreset = isPreset;
			}

			// Token: 0x1700093C RID: 2364
			// (get) Token: 0x06002BAD RID: 11181 RVA: 0x00104B6E File Offset: 0x00102D6E
			internal CodeExpression Expression
			{
				get
				{
					return this._expression;
				}
			}

			// Token: 0x1700093D RID: 2365
			// (get) Token: 0x06002BAE RID: 11182 RVA: 0x00104B76 File Offset: 0x00102D76
			internal bool IsPreset
			{
				get
				{
					return this._isPreset;
				}
			}

			// Token: 0x04001E53 RID: 7763
			private CodeExpression _expression;

			// Token: 0x04001E54 RID: 7764
			private bool _isPreset;
		}

		// Token: 0x020004A8 RID: 1192
		private class ReferenceComparer : IEqualityComparer
		{
			// Token: 0x06002BAF RID: 11183 RVA: 0x000EBFE3 File Offset: 0x000EA1E3
			bool IEqualityComparer.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06002BB0 RID: 11184 RVA: 0x000B900B File Offset: 0x000B720B
			int IEqualityComparer.GetHashCode(object x)
			{
				if (x != null)
				{
					return x.GetHashCode();
				}
				return 0;
			}
		}
	}
}
