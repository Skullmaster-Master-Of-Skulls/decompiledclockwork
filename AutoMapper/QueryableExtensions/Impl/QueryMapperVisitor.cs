using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x0200006A RID: 106
	public class QueryMapperVisitor : ExpressionVisitor
	{
		// Token: 0x0600039D RID: 925 RVA: 0x000090A4 File Offset: 0x000072A4
		internal QueryMapperVisitor(Type sourceType, Type destinationType, IQueryable destQuery, IConfigurationProvider config)
		{
			this._sourceType = sourceType;
			this._destinationType = destinationType;
			this._destQuery = destQuery;
			this._instanceParameter = Expression.Parameter(destinationType, "dto");
			this._memberVisitor = new MemberAccessQueryMapperVisitor(this, config);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00009104 File Offset: 0x00007304
		public static IQueryable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> sourceQuery, IQueryable<TDestination> destQuery, IConfigurationProvider config)
		{
			Expression expression = new QueryMapperVisitor(typeof(TSource), typeof(TDestination), destQuery, config).Visit(sourceQuery.Expression);
			return destQuery.Provider.CreateQuery<TDestination>(expression);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009144 File Offset: 0x00007344
		public override Expression Visit(Expression node)
		{
			this._tree.Push(node);
			if (node != null && node.NodeType == (ExpressionType)10000)
			{
				return node;
			}
			Expression expression = base.Visit(node);
			this._newTree.Push(expression);
			return expression;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00009184 File Offset: 0x00007384
		protected override Expression VisitParameter(ParameterExpression node)
		{
			return this._instanceParameter;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000918C File Offset: 0x0000738C
		protected override Expression VisitConstant(ConstantExpression node)
		{
			IQueryable queryable = node.Value as IQueryable;
			if (queryable != null && queryable.ElementType == this._sourceType)
			{
				return this._destQuery.Expression;
			}
			return node;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000091C8 File Offset: 0x000073C8
		protected override Expression VisitBinary(BinaryExpression node)
		{
			Expression expression = this.Visit(node.Left);
			Expression expression2 = this.Visit(node.Right);
			if (expression.Type != expression2.Type && expression2.NodeType == ExpressionType.Constant)
			{
				expression2 = Expression.Constant(Convert.ChangeType(((ConstantExpression)expression2).Value, expression.Type, CultureInfo.CurrentCulture), expression.Type);
			}
			return Expression.MakeBinary(node.NodeType, expression, expression2);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00009240 File Offset: 0x00007440
		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			Expression expression = this.Visit(node.Body);
			IEnumerable<ParameterExpression> parameters = from p in node.Parameters
			select (ParameterExpression)this.Visit(p);
			return Expression.Lambda(this.ChangeLambdaArgTypeFormSourceToDest(node.Type, expression.Type), expression, parameters);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000928C File Offset: 0x0000748C
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node.Method.Name == "OrderBy" || node.Method.Name == "OrderByDescending" || node.Method.Name == "ThenBy" || node.Method.Name == "ThenByDescending")
			{
				return this.VisitOrderBy(node);
			}
			List<Expression> arguments = node.Arguments.Select(new Func<Expression, Expression>(this.Visit)).ToList<Expression>();
			Expression instance = this.Visit(node.Object);
			MethodInfo method = this.ChangeMethodArgTypeFormSourceToDest(node.Method);
			return Expression.Call(instance, method, arguments);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000933C File Offset: 0x0000753C
		private Expression VisitOrderBy(MethodCallExpression node)
		{
			Expression node2 = node.Arguments[0];
			Expression node3 = node.Arguments[1];
			Expression arg = this.Visit(node2);
			Expression arg2 = this.Visit(node3);
			Expression instance = this.Visit(node.Object);
			MethodInfo genericMethodDefinition = node.Method.GetGenericMethodDefinition();
			Type[] genericArguments = node.Method.GetGenericArguments();
			genericArguments[0] = genericArguments[0].ReplaceItemType(this._sourceType, this._destinationType);
			genericArguments[1] = genericArguments[1].ReplaceItemType(typeof(string), typeof(int));
			MethodInfo method = genericMethodDefinition.MakeGenericMethod(genericArguments);
			return Expression.Call(instance, method, arg, arg2);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000093E3 File Offset: 0x000075E3
		protected override Expression VisitMember(MemberExpression node)
		{
			return this._memberVisitor.Visit(node);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000093F4 File Offset: 0x000075F4
		private MethodInfo ChangeMethodArgTypeFormSourceToDest(MethodInfo mi)
		{
			if (!mi.IsGenericMethod)
			{
				return mi;
			}
			MethodInfo genericMethodDefinition = mi.GetGenericMethodDefinition();
			Type[] array = mi.GetGenericArguments();
			array = (from t in array
			select t.ReplaceItemType(this._sourceType, this._destinationType)).ToArray<Type>();
			return genericMethodDefinition.MakeGenericMethod(array);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00009438 File Offset: 0x00007638
		private Type ChangeLambdaArgTypeFormSourceToDest(Type lambdaType, Type returnType)
		{
			if (lambdaType.IsGenericType())
			{
				Type[] array = (from t in lambdaType.GetTypeInfo().GenericTypeArguments
				select t.ReplaceItemType(this._sourceType, this._destinationType)).ToArray<Type>();
				Type genericTypeDefinition = lambdaType.GetGenericTypeDefinition();
				if (genericTypeDefinition.FullName.StartsWith("System.Func"))
				{
					array[array.Length - 1] = returnType;
				}
				return genericTypeDefinition.MakeGenericType(array);
			}
			return lambdaType;
		}

		// Token: 0x040000B4 RID: 180
		private readonly IQueryable _destQuery;

		// Token: 0x040000B5 RID: 181
		private readonly ParameterExpression _instanceParameter;

		// Token: 0x040000B6 RID: 182
		private readonly Type _sourceType;

		// Token: 0x040000B7 RID: 183
		private readonly Type _destinationType;

		// Token: 0x040000B8 RID: 184
		private readonly Stack<object> _tree = new Stack<object>();

		// Token: 0x040000B9 RID: 185
		private readonly Stack<object> _newTree = new Stack<object>();

		// Token: 0x040000BA RID: 186
		private readonly MemberAccessQueryMapperVisitor _memberVisitor;
	}
}
