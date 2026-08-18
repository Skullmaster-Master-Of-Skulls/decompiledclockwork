using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002DB RID: 731
	internal class CoordinatorScratchpad
	{
		// Token: 0x0600199D RID: 6557 RVA: 0x0007FB08 File Offset: 0x0007DD08
		internal CoordinatorScratchpad(Type elementType)
		{
			this._elementType = elementType;
			this._nestedCoordinatorScratchpads = new List<CoordinatorScratchpad>();
			this._expressionWithErrorHandlingMap = new Dictionary<Expression, Expression>();
			this._inlineDelegates = new HashSet<LambdaExpression>();
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0007FB38 File Offset: 0x0007DD38
		internal CoordinatorScratchpad Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0007FB40 File Offset: 0x0007DD40
		// (set) Token: 0x060019A0 RID: 6560 RVA: 0x0007FB48 File Offset: 0x0007DD48
		internal Expression SetKeys { get; set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0007FB51 File Offset: 0x0007DD51
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x0007FB59 File Offset: 0x0007DD59
		internal Expression CheckKeys { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x0007FB62 File Offset: 0x0007DD62
		// (set) Token: 0x060019A4 RID: 6564 RVA: 0x0007FB6A File Offset: 0x0007DD6A
		internal Expression HasData { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x0007FB73 File Offset: 0x0007DD73
		// (set) Token: 0x060019A6 RID: 6566 RVA: 0x0007FB7B File Offset: 0x0007DD7B
		internal Expression Element { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0007FB84 File Offset: 0x0007DD84
		// (set) Token: 0x060019A8 RID: 6568 RVA: 0x0007FB8C File Offset: 0x0007DD8C
		internal Expression InitializeCollection { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0007FB95 File Offset: 0x0007DD95
		// (set) Token: 0x060019AA RID: 6570 RVA: 0x0007FB9D File Offset: 0x0007DD9D
		internal int StateSlotNumber { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0007FBA6 File Offset: 0x0007DDA6
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x0007FBAE File Offset: 0x0007DDAE
		internal int Depth { get; set; }

		// Token: 0x060019AD RID: 6573 RVA: 0x0007FBB7 File Offset: 0x0007DDB7
		internal void AddExpressionWithErrorHandling(Expression expression, Expression expressionWithErrorHandling)
		{
			this._expressionWithErrorHandlingMap[expression] = expressionWithErrorHandling;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0007FBC6 File Offset: 0x0007DDC6
		internal void AddInlineDelegate(LambdaExpression expression)
		{
			this._inlineDelegates.Add(expression);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0007FBD5 File Offset: 0x0007DDD5
		internal void AddNestedCoordinator(CoordinatorScratchpad nested)
		{
			nested._parent = this;
			this._nestedCoordinatorScratchpads.Add(nested);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0007FBEC File Offset: 0x0007DDEC
		internal CoordinatorFactory Compile()
		{
			RecordStateFactory[] array;
			if (this._recordStateScratchpads != null)
			{
				array = new RecordStateFactory[this._recordStateScratchpads.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._recordStateScratchpads[i].Compile();
				}
			}
			else
			{
				array = new RecordStateFactory[0];
			}
			CoordinatorFactory[] array2 = new CoordinatorFactory[this._nestedCoordinatorScratchpads.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = this._nestedCoordinatorScratchpads[j].Compile();
			}
			CoordinatorScratchpad.ReplacementExpressionVisitor replacementExpressionVisitor = new CoordinatorScratchpad.ReplacementExpressionVisitor(null, this._inlineDelegates);
			Expression expression = replacementExpressionVisitor.Visit(this.Element);
			replacementExpressionVisitor = new CoordinatorScratchpad.ReplacementExpressionVisitor(this._expressionWithErrorHandlingMap, this._inlineDelegates);
			Expression expression2 = replacementExpressionVisitor.Visit(this.Element);
			return (CoordinatorFactory)Activator.CreateInstance(typeof(CoordinatorFactory<>).MakeGenericType(new Type[]
			{
				this._elementType
			}), new object[]
			{
				this.Depth,
				this.StateSlotNumber,
				this.HasData,
				this.SetKeys,
				this.CheckKeys,
				array2,
				expression,
				expression2,
				this.InitializeCollection,
				array
			});
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0007FD48 File Offset: 0x0007DF48
		internal RecordStateScratchpad CreateRecordStateScratchpad()
		{
			RecordStateScratchpad recordStateScratchpad = new RecordStateScratchpad();
			if (this._recordStateScratchpads == null)
			{
				this._recordStateScratchpads = new List<RecordStateScratchpad>();
			}
			this._recordStateScratchpads.Add(recordStateScratchpad);
			return recordStateScratchpad;
		}

		// Token: 0x040008D1 RID: 2257
		private readonly Type _elementType;

		// Token: 0x040008D2 RID: 2258
		private CoordinatorScratchpad _parent;

		// Token: 0x040008D3 RID: 2259
		private readonly List<CoordinatorScratchpad> _nestedCoordinatorScratchpads;

		// Token: 0x040008D4 RID: 2260
		private readonly Dictionary<Expression, Expression> _expressionWithErrorHandlingMap;

		// Token: 0x040008D5 RID: 2261
		private readonly HashSet<LambdaExpression> _inlineDelegates;

		// Token: 0x040008D6 RID: 2262
		private List<RecordStateScratchpad> _recordStateScratchpads;

		// Token: 0x020002DE RID: 734
		private class ReplacementExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x060019D4 RID: 6612 RVA: 0x0008086C File Offset: 0x0007EA6C
			internal ReplacementExpressionVisitor(Dictionary<Expression, Expression> replacementDictionary, HashSet<LambdaExpression> inlineDelegates)
			{
				this._replacementDictionary = replacementDictionary;
				this._inlineDelegates = inlineDelegates;
			}

			// Token: 0x060019D5 RID: 6613 RVA: 0x00080884 File Offset: 0x0007EA84
			internal override Expression Visit(Expression expression)
			{
				if (expression == null)
				{
					return expression;
				}
				Expression expression2;
				Expression result;
				if (this._replacementDictionary != null && this._replacementDictionary.TryGetValue(expression, out expression2))
				{
					result = expression2;
				}
				else
				{
					bool flag = false;
					LambdaExpression lambdaExpression = null;
					if (expression.NodeType == ExpressionType.Lambda && this._inlineDelegates != null)
					{
						lambdaExpression = (LambdaExpression)expression;
						flag = this._inlineDelegates.Contains(lambdaExpression);
					}
					if (flag)
					{
						Expression expression3 = this.Visit(lambdaExpression.Body);
						result = Expression.Constant(CodeGenEmitter.Compile(expression3.Type, expression3));
					}
					else
					{
						result = base.Visit(expression);
					}
				}
				return result;
			}

			// Token: 0x040008E1 RID: 2273
			private readonly Dictionary<Expression, Expression> _replacementDictionary;

			// Token: 0x040008E2 RID: 2274
			private readonly HashSet<LambdaExpression> _inlineDelegates;
		}
	}
}
