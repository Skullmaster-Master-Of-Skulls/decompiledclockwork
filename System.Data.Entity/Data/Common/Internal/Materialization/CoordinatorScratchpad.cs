using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects.ELinq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003CA RID: 970
	internal class CoordinatorScratchpad
	{
		// Token: 0x06003460 RID: 13408 RVA: 0x000CA5CF File Offset: 0x000C87CF
		internal CoordinatorScratchpad(Type elementType)
		{
			this._elementType = elementType;
			this._nestedCoordinatorScratchpads = new List<CoordinatorScratchpad>();
			this._expressionWithErrorHandlingMap = new Dictionary<Expression, Expression>();
			this._inlineDelegates = new HashSet<LambdaExpression>();
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000CA5FF File Offset: 0x000C87FF
		internal CoordinatorScratchpad Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000CA607 File Offset: 0x000C8807
		// (set) Token: 0x06003463 RID: 13411 RVA: 0x000CA60F File Offset: 0x000C880F
		internal Expression SetKeys { get; set; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000CA618 File Offset: 0x000C8818
		// (set) Token: 0x06003465 RID: 13413 RVA: 0x000CA620 File Offset: 0x000C8820
		internal Expression CheckKeys { get; set; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000CA629 File Offset: 0x000C8829
		// (set) Token: 0x06003467 RID: 13415 RVA: 0x000CA631 File Offset: 0x000C8831
		internal Expression HasData { get; set; }

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000CA63A File Offset: 0x000C883A
		// (set) Token: 0x06003469 RID: 13417 RVA: 0x000CA642 File Offset: 0x000C8842
		internal Expression Element { get; set; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000CA64B File Offset: 0x000C884B
		// (set) Token: 0x0600346B RID: 13419 RVA: 0x000CA653 File Offset: 0x000C8853
		internal Expression InitializeCollection { get; set; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000CA65C File Offset: 0x000C885C
		// (set) Token: 0x0600346D RID: 13421 RVA: 0x000CA664 File Offset: 0x000C8864
		internal int StateSlotNumber { get; set; }

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000CA66D File Offset: 0x000C886D
		// (set) Token: 0x0600346F RID: 13423 RVA: 0x000CA675 File Offset: 0x000C8875
		internal int Depth { get; set; }

		// Token: 0x06003470 RID: 13424 RVA: 0x000CA67E File Offset: 0x000C887E
		internal void AddExpressionWithErrorHandling(Expression expression, Expression expressionWithErrorHandling)
		{
			this._expressionWithErrorHandlingMap[expression] = expressionWithErrorHandling;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000CA68D File Offset: 0x000C888D
		internal void AddInlineDelegate(LambdaExpression expression)
		{
			this._inlineDelegates.Add(expression);
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000CA69C File Offset: 0x000C889C
		internal void AddNestedCoordinator(CoordinatorScratchpad nested)
		{
			nested._parent = this;
			this._nestedCoordinatorScratchpads.Add(nested);
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000CA6B4 File Offset: 0x000C88B4
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
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
			Expression expression = new CoordinatorScratchpad.SecurityBoundaryExpressionVisitor().Visit(replacementExpressionVisitor.Visit(this.Element));
			replacementExpressionVisitor = new CoordinatorScratchpad.ReplacementExpressionVisitor(this._expressionWithErrorHandlingMap, this._inlineDelegates);
			Expression expression2 = new CoordinatorScratchpad.SecurityBoundaryExpressionVisitor().Visit(replacementExpressionVisitor.Visit(this.Element));
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

		// Token: 0x06003474 RID: 13428 RVA: 0x000CA818 File Offset: 0x000C8A18
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

		// Token: 0x040016D9 RID: 5849
		private readonly Type _elementType;

		// Token: 0x040016DA RID: 5850
		private CoordinatorScratchpad _parent;

		// Token: 0x040016DB RID: 5851
		private readonly List<CoordinatorScratchpad> _nestedCoordinatorScratchpads;

		// Token: 0x040016DC RID: 5852
		private readonly Dictionary<Expression, Expression> _expressionWithErrorHandlingMap;

		// Token: 0x040016DD RID: 5853
		private readonly HashSet<LambdaExpression> _inlineDelegates;

		// Token: 0x040016E5 RID: 5861
		private List<RecordStateScratchpad> _recordStateScratchpads;

		// Token: 0x02000692 RID: 1682
		private class ReplacementExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x06004534 RID: 17716 RVA: 0x000F928F File Offset: 0x000F748F
			internal ReplacementExpressionVisitor(Dictionary<Expression, Expression> replacementDictionary, HashSet<LambdaExpression> inlineDelegates)
			{
				this._replacementDictionary = replacementDictionary;
				this._inlineDelegates = inlineDelegates;
			}

			// Token: 0x06004535 RID: 17717 RVA: 0x000F92A8 File Offset: 0x000F74A8
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
						result = Expression.Constant(Translator.Compile(expression3.Type, expression3));
					}
					else
					{
						result = base.Visit(expression);
					}
				}
				return result;
			}

			// Token: 0x04001FF3 RID: 8179
			private readonly Dictionary<Expression, Expression> _replacementDictionary;

			// Token: 0x04001FF4 RID: 8180
			private readonly HashSet<LambdaExpression> _inlineDelegates;
		}

		// Token: 0x02000693 RID: 1683
		private sealed class SecurityBoundaryExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x06004536 RID: 17718 RVA: 0x000F9330 File Offset: 0x000F7530
			internal override Expression Visit(Expression exp)
			{
				if (exp == null)
				{
					return exp;
				}
				NewExpression newExpression = exp as NewExpression;
				if (newExpression == null || this._userExpressionDepth < 1)
				{
					return base.Visit(exp);
				}
				if (this._userArgumentType != null && !newExpression.Type.IsPublic && newExpression.Type.Assembly == typeof(CoordinatorScratchpad.SecurityBoundaryExpressionVisitor).Assembly)
				{
					return this.CreateInitializationArgumentReplacement(newExpression, this._userArgumentType);
				}
				ParameterInfo[] parameters = newExpression.Constructor.GetParameters();
				ReadOnlyCollection<Expression> arguments = newExpression.Arguments;
				List<Expression> list = new List<Expression>();
				for (int i = 0; i < arguments.Count; i++)
				{
					Expression expression = arguments[i];
					this._userArgumentType = parameters[i].ParameterType;
					Expression expression2 = this.Visit(expression);
					if (expression2 == expression)
					{
						Expression item = this.CreateInitializationArgumentReplacement(expression);
						list.Add(item);
					}
					else
					{
						list.Add(expression2);
					}
				}
				newExpression = Expression.New(newExpression.Constructor, list);
				if (this._userExpressionDepth == 1)
				{
					Func<DbDataReader, object[], object> value = Expression.Lambda<Func<DbDataReader, object[], object>>(newExpression, new ParameterExpression[]
					{
						this._reader,
						this._values
					}).Compile();
					return Expression.Convert(Expression.Call(Expression.Constant(value), CoordinatorScratchpad.SecurityBoundaryExpressionVisitor.s_userMaterializationFuncInvokeMethod, Translator.Shaper_Reader, Expression.NewArrayInit(typeof(object), this._initializationArguments)), newExpression.Type);
				}
				return newExpression;
			}

			// Token: 0x06004537 RID: 17719 RVA: 0x000F9494 File Offset: 0x000F7694
			internal override Expression VisitConditional(ConditionalExpression c)
			{
				if (this._userExpressionDepth < 1 || !(this._userArgumentType != null))
				{
					return base.VisitConditional(c);
				}
				MethodCallExpression methodCallExpression = c.Test as MethodCallExpression;
				MethodCallExpression methodCallExpression2 = c.IfFalse as MethodCallExpression;
				if (methodCallExpression != null && methodCallExpression.Object != null && typeof(DbDataReader).IsAssignableFrom(methodCallExpression.Object.Type) && methodCallExpression.Method.Name == "IsDBNull" && methodCallExpression2 != null && ((methodCallExpression2.Object != null && typeof(DbDataReader).IsAssignableFrom(methodCallExpression2.Object.Type)) || CoordinatorScratchpad.SecurityBoundaryExpressionVisitor.IsUserExpressionMethod(methodCallExpression2.Method)))
				{
					return base.VisitConditional(c);
				}
				return this.CreateInitializationArgumentReplacement(c);
			}

			// Token: 0x06004538 RID: 17720 RVA: 0x000F9560 File Offset: 0x000F7760
			internal override Expression VisitMemberAccess(MemberExpression m)
			{
				if (this._userExpressionDepth >= 1 && typeof(DbDataReader).IsAssignableFrom(m.Type))
				{
					ParameterExpression parameterExpression = m.Expression as ParameterExpression;
					if (parameterExpression != null && parameterExpression == Translator.Shaper_Parameter)
					{
						return this._reader;
					}
				}
				return base.VisitMemberAccess(m);
			}

			// Token: 0x06004539 RID: 17721 RVA: 0x000F95B4 File Offset: 0x000F77B4
			internal override Expression VisitMemberInit(MemberInitExpression init)
			{
				if (this._userExpressionDepth < 1)
				{
					return base.VisitMemberInit(init);
				}
				Expression expression = base.VisitMemberInit(init);
				if (expression != init && this._userExpressionDepth == 1)
				{
					Func<DbDataReader, object[], object> value = Expression.Lambda<Func<DbDataReader, object[], object>>(expression, new ParameterExpression[]
					{
						this._reader,
						this._values
					}).Compile();
					return Expression.Convert(Expression.Call(Expression.Constant(value), CoordinatorScratchpad.SecurityBoundaryExpressionVisitor.s_userMaterializationFuncInvokeMethod, Translator.Shaper_Reader, Expression.NewArrayInit(typeof(object), this._initializationArguments)), init.Type);
				}
				return expression;
			}

			// Token: 0x0600453A RID: 17722 RVA: 0x000F9644 File Offset: 0x000F7844
			internal override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
			{
				if (this._userExpressionDepth >= 1)
				{
					FieldInfo fieldInfo = assignment.Member as FieldInfo;
					PropertyInfo propertyInfo = assignment.Member as PropertyInfo;
					if (fieldInfo != null)
					{
						this._userArgumentType = fieldInfo.FieldType;
					}
					else if (propertyInfo != null)
					{
						this._userArgumentType = propertyInfo.PropertyType;
					}
				}
				return base.VisitMemberAssignment(assignment);
			}

			// Token: 0x0600453B RID: 17723 RVA: 0x000F96A8 File Offset: 0x000F78A8
			internal override Expression VisitMethodCall(MethodCallExpression m)
			{
				MethodInfo method = m.Method;
				if (CoordinatorScratchpad.SecurityBoundaryExpressionVisitor.IsUserExpressionMethod(method))
				{
					try
					{
						this._userArgumentType = null;
						this._userExpressionDepth++;
						return this.Visit(m.Arguments[0]);
					}
					finally
					{
						this._userExpressionDepth--;
					}
				}
				if (this._userExpressionDepth < 1)
				{
					return base.VisitMethodCall(m);
				}
				if (m.Object != null && typeof(DbDataReader).IsAssignableFrom(m.Object.Type))
				{
					return base.VisitMethodCall(m);
				}
				return this.CreateInitializationArgumentReplacement(m);
			}

			// Token: 0x0600453C RID: 17724 RVA: 0x000F9754 File Offset: 0x000F7954
			private Expression CreateInitializationArgumentReplacement(Expression original)
			{
				return this.CreateInitializationArgumentReplacement(original, original.Type);
			}

			// Token: 0x0600453D RID: 17725 RVA: 0x000F9764 File Offset: 0x000F7964
			private Expression CreateInitializationArgumentReplacement(Expression original, Type expressionType)
			{
				this._initializationArguments.Add(Expression.Convert(original, typeof(object)));
				return Expression.Convert(Expression.MakeBinary(ExpressionType.ArrayIndex, this._values, Expression.Constant(this._initializationArguments.Count - 1)), expressionType);
			}

			// Token: 0x0600453E RID: 17726 RVA: 0x000F97B5 File Offset: 0x000F79B5
			private static bool IsUserExpressionMethod(MethodInfo method)
			{
				return method.IsGenericMethod && method.GetGenericMethodDefinition() == InitializerMetadata.UserExpressionMarker;
			}

			// Token: 0x04001FF5 RID: 8181
			private static readonly MethodInfo s_userMaterializationFuncInvokeMethod = typeof(Func<DbDataReader, object[], object>).GetMethod("Invoke");

			// Token: 0x04001FF6 RID: 8182
			private ParameterExpression _values = Expression.Parameter(typeof(object[]), "values");

			// Token: 0x04001FF7 RID: 8183
			private ParameterExpression _reader = Expression.Parameter(typeof(DbDataReader), "reader");

			// Token: 0x04001FF8 RID: 8184
			private List<Expression> _initializationArguments = new List<Expression>();

			// Token: 0x04001FF9 RID: 8185
			private int _userExpressionDepth;

			// Token: 0x04001FFA RID: 8186
			private Type _userArgumentType;
		}
	}
}
