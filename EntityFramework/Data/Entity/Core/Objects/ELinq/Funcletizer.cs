using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200055E RID: 1374
	internal sealed class Funcletizer
	{
		// Token: 0x06003525 RID: 13605 RVA: 0x000FAF48 File Offset: 0x000F9148
		private Funcletizer(Funcletizer.Mode mode, ObjectContext rootContext, ParameterExpression rootContextParameter, ReadOnlyCollection<ParameterExpression> compiledQueryParameters)
		{
			this._mode = mode;
			this._rootContext = rootContext;
			this._rootContextParameter = rootContextParameter;
			this._compiledQueryParameters = compiledQueryParameters;
			if (this._rootContextParameter != null && this._rootContext != null)
			{
				this._rootContextExpression = Expression.Constant(this._rootContext);
			}
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000FAFA4 File Offset: 0x000F91A4
		internal static Funcletizer CreateCompiledQueryEvaluationFuncletizer(ObjectContext rootContext, ParameterExpression rootContextParameter, ReadOnlyCollection<ParameterExpression> compiledQueryParameters)
		{
			return new Funcletizer(Funcletizer.Mode.CompiledQueryEvaluation, rootContext, rootContextParameter, compiledQueryParameters);
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000FAFAF File Offset: 0x000F91AF
		internal static Funcletizer CreateCompiledQueryLockdownFuncletizer()
		{
			return new Funcletizer(Funcletizer.Mode.CompiledQueryLockdown, null, null, null);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000FAFBA File Offset: 0x000F91BA
		internal static Funcletizer CreateQueryFuncletizer(ObjectContext rootContext)
		{
			return new Funcletizer(Funcletizer.Mode.ConventionalQuery, rootContext, null, null);
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000FAFC5 File Offset: 0x000F91C5
		internal ObjectContext RootContext
		{
			get
			{
				return this._rootContext;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000FAFCD File Offset: 0x000F91CD
		internal ParameterExpression RootContextParameter
		{
			get
			{
				return this._rootContextParameter;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000FAFD5 File Offset: 0x000F91D5
		internal ConstantExpression RootContextExpression
		{
			get
			{
				return this._rootContextExpression;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x000FAFDD File Offset: 0x000F91DD
		internal bool IsCompiledQuery
		{
			get
			{
				return this._mode == Funcletizer.Mode.CompiledQueryEvaluation || this._mode == Funcletizer.Mode.CompiledQueryLockdown;
			}
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000FAFF8 File Offset: 0x000F91F8
		internal Expression Funcletize(Expression expression, out Func<bool> recompileRequired)
		{
			expression = this.ReplaceRootContextParameter(expression);
			Func<Expression, bool> isClientConstant;
			Func<Expression, bool> isClientVariable;
			if (this._mode == Funcletizer.Mode.CompiledQueryEvaluation)
			{
				isClientConstant = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
				isClientVariable = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsCompiledQueryParameterVariable));
			}
			else if (this._mode == Funcletizer.Mode.CompiledQueryLockdown)
			{
				isClientConstant = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
				isClientVariable = ((Expression exp) => false);
			}
			else
			{
				isClientConstant = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsImmutable));
				isClientVariable = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
			}
			Funcletizer.FuncletizingVisitor funcletizingVisitor = new Funcletizer.FuncletizingVisitor(this, isClientConstant, isClientVariable);
			Expression result = funcletizingVisitor.Visit(expression);
			recompileRequired = funcletizingVisitor.GetRecompileRequiredFunction();
			return result;
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000FB0D4 File Offset: 0x000F92D4
		private Expression ReplaceRootContextParameter(Expression expression)
		{
			if (this._rootContextExpression != null)
			{
				return EntityExpressionVisitor.Visit(expression, delegate(Expression exp, Func<Expression, Expression> baseVisit)
				{
					if (exp != this._rootContextParameter)
					{
						return baseVisit(exp);
					}
					return this._rootContextExpression;
				});
			}
			return expression;
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000FB170 File Offset: 0x000F9370
		private static Func<Expression, bool> Nominate(Expression expression, Func<Expression, bool> localCriterion)
		{
			HashSet<Expression> candidates = new HashSet<Expression>();
			bool cannotBeNominated = false;
			Func<Expression, Func<Expression, Expression>, Expression> visit = delegate(Expression exp, Func<Expression, Expression> baseVisit)
			{
				if (exp != null)
				{
					bool cannotBeNominated = cannotBeNominated;
					cannotBeNominated = false;
					baseVisit(exp);
					if (!cannotBeNominated)
					{
						if (localCriterion(exp))
						{
							candidates.Add(exp);
						}
						else
						{
							cannotBeNominated = true;
						}
					}
					cannotBeNominated = (cannotBeNominated || cannotBeNominated);
				}
				return exp;
			};
			EntityExpressionVisitor.Visit(expression, visit);
			return new Func<Expression, bool>(candidates.Contains);
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000FB1C4 File Offset: 0x000F93C4
		private bool IsImmutable(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			ExpressionType nodeType = expression.NodeType;
			switch (nodeType)
			{
			case ExpressionType.Constant:
				return true;
			case ExpressionType.Convert:
				return true;
			default:
				switch (nodeType)
				{
				case ExpressionType.New:
				{
					PrimitiveType primitiveType;
					return ClrProviderManifest.Instance.TryGetPrimitiveType(TypeSystem.GetNonNullableType(expression.Type), out primitiveType);
				}
				case ExpressionType.NewArrayInit:
					return typeof(byte[]) == expression.Type;
				default:
					return false;
				}
				break;
			}
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000FB23C File Offset: 0x000F943C
		private bool IsClosureExpression(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			if (this.IsImmutable(expression))
			{
				return true;
			}
			if (ExpressionType.MemberAccess == expression.NodeType)
			{
				MemberExpression memberExpression = (MemberExpression)expression;
				return memberExpression.Member.MemberType != MemberTypes.Property || ExpressionConverter.CanFuncletizePropertyInfo((PropertyInfo)memberExpression.Member);
			}
			return false;
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000FB290 File Offset: 0x000F9490
		private bool IsCompiledQueryParameterVariable(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			if (this.IsClosureExpression(expression))
			{
				return true;
			}
			if (ExpressionType.Parameter == expression.NodeType)
			{
				ParameterExpression value = (ParameterExpression)expression;
				return this._compiledQueryParameters.Contains(value);
			}
			return false;
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000FB2CC File Offset: 0x000F94CC
		private bool TryGetTypeUsageForTerminal(Expression expression, out TypeUsage typeUsage)
		{
			Type type = expression.Type;
			if (this._rootContext.Perspective.TryGetTypeByName(TypeSystem.GetNonNullableType(type).FullNameWithNesting(), false, out typeUsage) && TypeSemantics.IsScalarType(typeUsage))
			{
				if (expression.NodeType == ExpressionType.Convert)
				{
					type = ((UnaryExpression)expression).Operand.Type;
				}
				if (type.IsValueType && Nullable.GetUnderlyingType(type) == null && TypeSemantics.IsNullable(typeUsage))
				{
					typeUsage = typeUsage.ShallowCopy(new FacetValues
					{
						Nullable = new bool?(false)
					});
				}
				return true;
			}
			typeUsage = null;
			return false;
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000FB36C File Offset: 0x000F956C
		internal string GenerateParameterName()
		{
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string format = "{0}{1}";
			object[] array = new object[2];
			array[0] = "p__linq__";
			object[] array2 = array;
			int num = 1;
			long parameterNumber;
			this._parameterNumber = (parameterNumber = this._parameterNumber) + 1L;
			array2[num] = parameterNumber;
			return string.Format(invariantCulture, format, array);
		}

		// Token: 0x040013E2 RID: 5090
		private const string s_parameterPrefix = "p__linq__";

		// Token: 0x040013E3 RID: 5091
		private readonly ParameterExpression _rootContextParameter;

		// Token: 0x040013E4 RID: 5092
		private readonly ObjectContext _rootContext;

		// Token: 0x040013E5 RID: 5093
		private readonly ConstantExpression _rootContextExpression;

		// Token: 0x040013E6 RID: 5094
		private readonly ReadOnlyCollection<ParameterExpression> _compiledQueryParameters;

		// Token: 0x040013E7 RID: 5095
		private readonly Funcletizer.Mode _mode;

		// Token: 0x040013E8 RID: 5096
		private readonly HashSet<Expression> _linqExpressionStack = new HashSet<Expression>();

		// Token: 0x040013E9 RID: 5097
		private long _parameterNumber;

		// Token: 0x0200055F RID: 1375
		private enum Mode
		{
			// Token: 0x040013EC RID: 5100
			CompiledQueryLockdown,
			// Token: 0x040013ED RID: 5101
			CompiledQueryEvaluation,
			// Token: 0x040013EE RID: 5102
			ConventionalQuery
		}

		// Token: 0x02000560 RID: 1376
		private sealed class FuncletizingVisitor : EntityExpressionVisitor
		{
			// Token: 0x06003537 RID: 13623 RVA: 0x000FB3B2 File Offset: 0x000F95B2
			internal FuncletizingVisitor(Funcletizer funcletizer, Func<Expression, bool> isClientConstant, Func<Expression, bool> isClientVariable)
			{
				this._funcletizer = funcletizer;
				this._isClientConstant = isClientConstant;
				this._isClientVariable = isClientVariable;
			}

			// Token: 0x06003538 RID: 13624 RVA: 0x000FB414 File Offset: 0x000F9614
			internal Func<bool> GetRecompileRequiredFunction()
			{
				ReadOnlyCollection<Func<bool>> recompileRequiredDelegates = new ReadOnlyCollection<Func<bool>>(this._recompileRequiredDelegates);
				return () => recompileRequiredDelegates.Any((Func<bool> d) => d());
			}

			// Token: 0x06003539 RID: 13625 RVA: 0x000FB444 File Offset: 0x000F9644
			internal override Expression Visit(Expression exp)
			{
				if (exp != null)
				{
					if (!this._funcletizer._linqExpressionStack.Add(exp))
					{
						throw new InvalidOperationException(Strings.ELinq_CycleDetected);
					}
					try
					{
						if (this._isClientConstant(exp))
						{
							return this.InlineValue(exp, false);
						}
						if (!this._isClientVariable(exp))
						{
							return base.Visit(exp);
						}
						TypeUsage type;
						if (this._funcletizer.TryGetTypeUsageForTerminal(exp, out type))
						{
							DbParameterReferenceExpression parameterReference = type.Parameter(this._funcletizer.GenerateParameterName());
							return new QueryParameterExpression(parameterReference, exp, this._funcletizer._compiledQueryParameters);
						}
						if (this._funcletizer.IsCompiledQuery)
						{
							throw Funcletizer.FuncletizingVisitor.InvalidCompiledQueryParameterException(exp);
						}
						return this.InlineValue(exp, true);
					}
					finally
					{
						this._funcletizer._linqExpressionStack.Remove(exp);
					}
				}
				return base.Visit(exp);
			}

			// Token: 0x0600353A RID: 13626 RVA: 0x000FB55C File Offset: 0x000F975C
			private static NotSupportedException InvalidCompiledQueryParameterException(Expression expression)
			{
				ParameterExpression parameterExpression;
				if (expression.NodeType == ExpressionType.Parameter)
				{
					parameterExpression = (ParameterExpression)expression;
				}
				else
				{
					HashSet<ParameterExpression> parameters = new HashSet<ParameterExpression>();
					EntityExpressionVisitor.Visit(expression, delegate(Expression exp, Func<Expression, Expression> baseVisit)
					{
						if (exp != null && exp.NodeType == ExpressionType.Parameter)
						{
							parameters.Add((ParameterExpression)exp);
						}
						return baseVisit(exp);
					});
					if (parameters.Count != 1)
					{
						return new NotSupportedException(Strings.CompiledELinq_UnsupportedParameterTypes(expression.Type.FullName));
					}
					parameterExpression = parameters.Single<ParameterExpression>();
				}
				if (parameterExpression.Type.Equals(expression.Type))
				{
					return new NotSupportedException(Strings.CompiledELinq_UnsupportedNamedParameterType(parameterExpression.Name, parameterExpression.Type.FullName));
				}
				return new NotSupportedException(Strings.CompiledELinq_UnsupportedNamedParameterUseAsType(parameterExpression.Name, expression.Type.FullName));
			}

			// Token: 0x0600353B RID: 13627 RVA: 0x000FB61C File Offset: 0x000F981C
			private static Func<object> CompileExpression(Expression expression)
			{
				return Expression.Lambda<Func<object>>(TypeSystem.EnsureType(expression, typeof(object)), new ParameterExpression[0]).Compile();
			}

			// Token: 0x0600353C RID: 13628 RVA: 0x000FB64C File Offset: 0x000F984C
			private Expression InlineValue(Expression expression, bool recompileOnChange)
			{
				Func<object> func = null;
				object obj = null;
				if (expression.NodeType == ExpressionType.Constant)
				{
					obj = ((ConstantExpression)expression).Value;
				}
				else
				{
					bool flag = false;
					if (expression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)expression;
						if (!recompileOnChange && unaryExpression.Operand.NodeType == ExpressionType.Constant && typeof(IQueryable).IsAssignableFrom(unaryExpression.Operand.Type))
						{
							obj = ((ConstantExpression)unaryExpression.Operand).Value;
							flag = true;
						}
					}
					if (!flag)
					{
						func = Funcletizer.FuncletizingVisitor.CompileExpression(expression);
						obj = func();
					}
				}
				ObjectQuery objectQuery = (obj as IQueryable).TryGetObjectQuery();
				Expression result;
				if (objectQuery != null)
				{
					result = this.InlineObjectQuery(objectQuery, objectQuery.GetType());
				}
				else
				{
					LambdaExpression lambdaExpression = obj as LambdaExpression;
					if (lambdaExpression != null)
					{
						result = this.InlineExpression(Expression.Quote(lambdaExpression));
					}
					else
					{
						result = ((expression.NodeType == ExpressionType.Constant) ? expression : Expression.Constant(obj, expression.Type));
					}
				}
				if (recompileOnChange)
				{
					this.AddRecompileRequiredDelegates(func, obj);
				}
				return result;
			}

			// Token: 0x0600353D RID: 13629 RVA: 0x000FB838 File Offset: 0x000F9A38
			private void AddRecompileRequiredDelegates(Func<object> getValue, object value)
			{
				Funcletizer.FuncletizingVisitor.<>c__DisplayClass14 CS$<>8__locals1 = new Funcletizer.FuncletizingVisitor.<>c__DisplayClass14();
				CS$<>8__locals1.getValue = getValue;
				CS$<>8__locals1.value = value;
				CS$<>8__locals1.originalQuery = (CS$<>8__locals1.value as IQueryable).TryGetObjectQuery();
				if (CS$<>8__locals1.originalQuery == null)
				{
					if (CS$<>8__locals1.getValue != null)
					{
						this._recompileRequiredDelegates.Add(() => !object.ReferenceEquals(CS$<>8__locals1.value, CS$<>8__locals1.getValue()));
					}
					return;
				}
				MergeOption? originalMergeOption = CS$<>8__locals1.originalQuery.QueryState.UserSpecifiedMergeOption;
				if (CS$<>8__locals1.getValue == null)
				{
					this._recompileRequiredDelegates.Add(() => CS$<>8__locals1.originalQuery.QueryState.UserSpecifiedMergeOption != originalMergeOption);
					return;
				}
				this._recompileRequiredDelegates.Add(delegate
				{
					ObjectQuery objectQuery = (CS$<>8__locals1.getValue() as IQueryable).TryGetObjectQuery();
					return !object.ReferenceEquals(CS$<>8__locals1.originalQuery, objectQuery) || objectQuery.QueryState.UserSpecifiedMergeOption != originalMergeOption;
				});
			}

			// Token: 0x0600353E RID: 13630 RVA: 0x000FB914 File Offset: 0x000F9B14
			private Expression InlineObjectQuery(ObjectQuery inlineQuery, Type expressionType)
			{
				Expression expression;
				if (this._funcletizer._mode == Funcletizer.Mode.CompiledQueryLockdown)
				{
					expression = Expression.Constant(inlineQuery, expressionType);
				}
				else
				{
					if (!object.ReferenceEquals(this._funcletizer._rootContext, inlineQuery.QueryState.ObjectContext))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedDifferentContexts);
					}
					expression = inlineQuery.GetExpression();
					if (!(inlineQuery.QueryState is EntitySqlQueryState))
					{
						expression = this.InlineExpression(expression);
					}
					expression = TypeSystem.EnsureType(expression, expressionType);
				}
				return expression;
			}

			// Token: 0x0600353F RID: 13631 RVA: 0x000FB988 File Offset: 0x000F9B88
			private Expression InlineExpression(Expression exp)
			{
				Func<bool> item;
				exp = this._funcletizer.Funcletize(exp, out item);
				if (!this._funcletizer.IsCompiledQuery)
				{
					this._recompileRequiredDelegates.Add(item);
				}
				return exp;
			}

			// Token: 0x040013EF RID: 5103
			private readonly Funcletizer _funcletizer;

			// Token: 0x040013F0 RID: 5104
			private readonly Func<Expression, bool> _isClientConstant;

			// Token: 0x040013F1 RID: 5105
			private readonly Func<Expression, bool> _isClientVariable;

			// Token: 0x040013F2 RID: 5106
			private readonly List<Func<bool>> _recompileRequiredDelegates = new List<Func<bool>>();
		}
	}
}
