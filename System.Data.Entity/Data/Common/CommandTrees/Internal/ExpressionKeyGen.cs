using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Spatial;
using System.Globalization;
using System.Text;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x0200042D RID: 1069
	internal sealed class ExpressionKeyGen : DbExpressionVisitor
	{
		// Token: 0x06003943 RID: 14659 RVA: 0x000D94C0 File Offset: 0x000D76C0
		internal static bool TryGenerateKey(DbExpression tree, out string key)
		{
			ExpressionKeyGen expressionKeyGen = new ExpressionKeyGen();
			bool result;
			try
			{
				tree.Accept(expressionKeyGen);
				key = expressionKeyGen._key.ToString();
				result = true;
			}
			catch (NotSupportedException)
			{
				key = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000D9504 File Offset: 0x000D7704
		private ExpressionKeyGen()
		{
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000D9518 File Offset: 0x000D7718
		private static string[] InitializeExprKindNames()
		{
			string[] names = Enum.GetNames(typeof(DbExpressionKind));
			names[10] = "/";
			names[33] = "%";
			names[34] = "*";
			names[44] = "+";
			names[32] = "-";
			names[54] = "-";
			names[13] = "=";
			names[28] = "<";
			names[29] = "<=";
			names[18] = ">";
			names[19] = ">=";
			names[37] = "<>";
			names[46] = ".";
			names[21] = "IJ";
			names[16] = "FOJ";
			names[27] = "LOJ";
			names[6] = "CA";
			names[42] = "OA";
			return names;
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000D95D7 File Offset: 0x000D77D7
		private void VisitVariableName(string varName)
		{
			this._key.Append('\'');
			this._key.Append(varName.Replace("'", "''"));
			this._key.Append('\'');
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000D9614 File Offset: 0x000D7814
		private void VisitBinding(DbExpressionBinding binding)
		{
			this._key.Append("BV");
			this.VisitVariableName(binding.VariableName);
			this._key.Append("=(");
			binding.Expression.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000D966C File Offset: 0x000D786C
		private void VisitGroupBinding(DbGroupExpressionBinding groupBinding)
		{
			this._key.Append("GBVV");
			this.VisitVariableName(groupBinding.VariableName);
			this._key.Append(",");
			this.VisitVariableName(groupBinding.GroupVariableName);
			this._key.Append("=(");
			groupBinding.Expression.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000D96E0 File Offset: 0x000D78E0
		private void VisitFunction(EdmFunction func, IList<DbExpression> args)
		{
			this._key.Append("FUNC<");
			this._key.Append(func.Identity);
			this._key.Append(">:ARGS(");
			foreach (DbExpression dbExpression in args)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
			this._key.Append(')');
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000D9788 File Offset: 0x000D7988
		private void VisitExprKind(DbExpressionKind kind)
		{
			this._key.Append('[');
			this._key.Append(ExpressionKeyGen._exprKindNames[(int)kind]);
			this._key.Append(']');
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000D97B9 File Offset: 0x000D79B9
		private void VisitUnary(DbUnaryExpression expr)
		{
			this.VisitExprKind(expr.ExpressionKind);
			this._key.Append('(');
			expr.Argument.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000D97F0 File Offset: 0x000D79F0
		private void VisitBinary(DbBinaryExpression expr)
		{
			this.VisitExprKind(expr.ExpressionKind);
			this._key.Append('(');
			expr.Left.Accept(this);
			this._key.Append(',');
			expr.Right.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000D984C File Offset: 0x000D7A4C
		private void VisitCastOrTreat(DbUnaryExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.ResultType.Identity);
			this._key.Append(')');
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x00017364 File Offset: 0x00015564
		public override void Visit(DbExpression e)
		{
			throw EntityUtil.NotSupported(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000D98B8 File Offset: 0x000D7AB8
		public override void Visit(DbConstantExpression e)
		{
			TypeUsage primitiveTypeUsageForScalar = TypeHelpers.GetPrimitiveTypeUsageForScalar(e.ResultType);
			switch (((PrimitiveType)primitiveTypeUsageForScalar.EdmType).PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
			{
				byte[] array = e.Value as byte[];
				if (array == null)
				{
					throw new NotSupportedException();
				}
				this._key.Append("'");
				foreach (byte b in array)
				{
					this._key.AppendFormat("{0:X2}", b);
				}
				this._key.Append("'");
				break;
			}
			case PrimitiveTypeKind.Boolean:
			case PrimitiveTypeKind.Byte:
			case PrimitiveTypeKind.Decimal:
			case PrimitiveTypeKind.Double:
			case PrimitiveTypeKind.Guid:
			case PrimitiveTypeKind.Single:
			case PrimitiveTypeKind.SByte:
			case PrimitiveTypeKind.Int16:
			case PrimitiveTypeKind.Int32:
			case PrimitiveTypeKind.Int64:
			case PrimitiveTypeKind.Time:
				this._key.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					e.Value
				});
				break;
			case PrimitiveTypeKind.DateTime:
				this._key.Append(((DateTime)e.Value).ToString("o", CultureInfo.InvariantCulture));
				break;
			case PrimitiveTypeKind.String:
			{
				string text = e.Value as string;
				if (text == null)
				{
					throw new NotSupportedException();
				}
				this._key.Append("'");
				this._key.Append(text.Replace("'", "''"));
				this._key.Append("'");
				break;
			}
			case PrimitiveTypeKind.DateTimeOffset:
				this._key.Append(((DateTimeOffset)e.Value).ToString("o", CultureInfo.InvariantCulture));
				break;
			case PrimitiveTypeKind.Geometry:
			case PrimitiveTypeKind.GeometryPoint:
			case PrimitiveTypeKind.GeometryLineString:
			case PrimitiveTypeKind.GeometryPolygon:
			case PrimitiveTypeKind.GeometryMultiPoint:
			case PrimitiveTypeKind.GeometryMultiLineString:
			case PrimitiveTypeKind.GeometryMultiPolygon:
			case PrimitiveTypeKind.GeometryCollection:
			{
				DbGeometry dbGeometry = e.Value as DbGeometry;
				if (dbGeometry == null)
				{
					throw new NotSupportedException();
				}
				this._key.Append(dbGeometry.AsText());
				break;
			}
			case PrimitiveTypeKind.Geography:
			case PrimitiveTypeKind.GeographyPoint:
			case PrimitiveTypeKind.GeographyLineString:
			case PrimitiveTypeKind.GeographyPolygon:
			case PrimitiveTypeKind.GeographyMultiPoint:
			case PrimitiveTypeKind.GeographyMultiLineString:
			case PrimitiveTypeKind.GeographyMultiPolygon:
			case PrimitiveTypeKind.GeographyCollection:
			{
				DbGeography dbGeography = e.Value as DbGeography;
				if (dbGeography == null)
				{
					throw new NotSupportedException();
				}
				this._key.Append(dbGeography.AsText());
				break;
			}
			default:
				throw new NotSupportedException();
			}
			this._key.Append(":");
			this._key.Append(e.ResultType.Identity);
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000D9B3C File Offset: 0x000D7D3C
		public override void Visit(DbNullExpression e)
		{
			this._key.Append("NULL:");
			this._key.Append(e.ResultType.Identity);
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000D9B66 File Offset: 0x000D7D66
		public override void Visit(DbVariableReferenceExpression e)
		{
			this._key.Append("Var(");
			this.VisitVariableName(e.VariableName);
			this._key.Append(")");
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000D9B98 File Offset: 0x000D7D98
		public override void Visit(DbParameterReferenceExpression e)
		{
			this._key.Append("@");
			this._key.Append(e.ParameterName);
			this._key.Append(":");
			this._key.Append(e.ResultType.Identity);
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000D9BF0 File Offset: 0x000D7DF0
		public override void Visit(DbFunctionExpression e)
		{
			this.VisitFunction(e.Function, e.Arguments);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000D9C04 File Offset: 0x000D7E04
		public override void Visit(DbLambdaExpression expression)
		{
			this._key.Append("Lambda(");
			foreach (DbVariableReferenceExpression dbVariableReferenceExpression in expression.Lambda.Variables)
			{
				this._key.Append("(V");
				this.VisitVariableName(dbVariableReferenceExpression.VariableName);
				this._key.Append(":");
				this._key.Append(dbVariableReferenceExpression.ResultType.Identity);
				this._key.Append(')');
			}
			this._key.Append("=");
			foreach (DbExpression dbExpression in expression.Arguments)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
			this._key.Append(")Body(");
			expression.Lambda.Body.Accept(this);
			this._key.Append(")");
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000D9D50 File Offset: 0x000D7F50
		public override void Visit(DbPropertyExpression e)
		{
			e.Instance.Accept(this);
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append(e.Property.Name);
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000D9D81 File Offset: 0x000D7F81
		public override void Visit(DbComparisonExpression e)
		{
			this.VisitBinary(e);
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000D9D8C File Offset: 0x000D7F8C
		public override void Visit(DbLikeExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(")(");
			e.Pattern.Accept(this);
			this._key.Append(")(");
			if (e.Escape != null)
			{
				e.Escape.Accept(this);
			}
			e.Argument.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000D9E1C File Offset: 0x000D801C
		public override void Visit(DbLimitExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			if (e.WithTies)
			{
				this._key.Append("WithTies");
			}
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(")(");
			e.Limit.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000D9E93 File Offset: 0x000D8093
		public override void Visit(DbIsNullExpression e)
		{
			this.VisitUnary(e);
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000D9E9C File Offset: 0x000D809C
		public override void Visit(DbArithmeticExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			foreach (DbExpression dbExpression in e.Arguments)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000D9D81 File Offset: 0x000D7F81
		public override void Visit(DbAndExpression e)
		{
			this.VisitBinary(e);
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000D9D81 File Offset: 0x000D7F81
		public override void Visit(DbOrExpression e)
		{
			this.VisitBinary(e);
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000D9E93 File Offset: 0x000D8093
		public override void Visit(DbNotExpression e)
		{
			this.VisitUnary(e);
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000D9E93 File Offset: 0x000D8093
		public override void Visit(DbDistinctExpression e)
		{
			this.VisitUnary(e);
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000D9E93 File Offset: 0x000D8093
		public override void Visit(DbElementExpression e)
		{
			this.VisitUnary(e);
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000D9E93 File Offset: 0x000D8093
		public override void Visit(DbIsEmptyExpression e)
		{
			this.VisitUnary(e);
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000D9D81 File Offset: 0x000D7F81
		public override void Visit(DbUnionAllExpression e)
		{
			this.VisitBinary(e);
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000D9D81 File Offset: 0x000D7F81
		public override void Visit(DbIntersectExpression e)
		{
			this.VisitBinary(e);
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000D9D81 File Offset: 0x000D7F81
		public override void Visit(DbExceptExpression e)
		{
			this.VisitBinary(e);
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000D9F14 File Offset: 0x000D8114
		public override void Visit(DbTreatExpression e)
		{
			this.VisitCastOrTreat(e);
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000D9F14 File Offset: 0x000D8114
		public override void Visit(DbCastExpression e)
		{
			this.VisitCastOrTreat(e);
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000D9F20 File Offset: 0x000D8120
		public override void Visit(DbIsOfExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.OfType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000D9F90 File Offset: 0x000D8190
		public override void Visit(DbOfTypeExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.OfType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000DA000 File Offset: 0x000D8200
		public override void Visit(DbCaseExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			for (int i = 0; i < e.When.Count; i++)
			{
				this._key.Append("WHEN:(");
				e.When[i].Accept(this);
				this._key.Append(")THEN:(");
				e.Then[i].Accept(this);
			}
			this._key.Append("ELSE:(");
			e.Else.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000DA0B4 File Offset: 0x000D82B4
		public override void Visit(DbNewInstanceExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append(':');
			this._key.Append(e.ResultType.EdmType.Identity);
			this._key.Append('(');
			foreach (DbExpression dbExpression in e.Arguments)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
			if (e.HasRelatedEntityReferences)
			{
				foreach (DbRelatedEntityRef dbRelatedEntityRef in e.RelatedEntityReferences)
				{
					this._key.Append("RE(A(");
					this._key.Append(dbRelatedEntityRef.SourceEnd.DeclaringType.Identity);
					this._key.Append(")(");
					this._key.Append(dbRelatedEntityRef.SourceEnd.Name);
					this._key.Append("->");
					this._key.Append(dbRelatedEntityRef.TargetEnd.Name);
					this._key.Append(")(");
					dbRelatedEntityRef.TargetEntityReference.Accept(this);
					this._key.Append("))");
				}
			}
			this._key.Append(')');
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000DA260 File Offset: 0x000D8460
		public override void Visit(DbRefExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append("(ESET(");
			this._key.Append(e.EntitySet.EntityContainer.Name);
			this._key.Append('.');
			this._key.Append(e.EntitySet.Name);
			this._key.Append(")T(");
			this._key.Append(TypeHelpers.GetEdmType<RefType>(e.ResultType).ElementType.FullName);
			this._key.Append(")(");
			e.Argument.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000DA328 File Offset: 0x000D8528
		public override void Visit(DbRelationshipNavigationExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.NavigationSource.Accept(this);
			this._key.Append(")A(");
			this._key.Append(e.NavigateFrom.DeclaringType.Identity);
			this._key.Append(")(");
			this._key.Append(e.NavigateFrom.Name);
			this._key.Append("->");
			this._key.Append(e.NavigateTo.Name);
			this._key.Append("))");
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000D9E93 File Offset: 0x000D8093
		public override void Visit(DbDerefExpression e)
		{
			this.VisitUnary(e);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000D9E93 File Offset: 0x000D8093
		public override void Visit(DbRefKeyExpression e)
		{
			this.VisitUnary(e);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000D9E93 File Offset: 0x000D8093
		public override void Visit(DbEntityRefExpression e)
		{
			this.VisitUnary(e);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000DA3EC File Offset: 0x000D85EC
		public override void Visit(DbScanExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this._key.Append(e.Target.EntityContainer.Name);
			this._key.Append('.');
			this._key.Append(e.Target.Name);
			this._key.Append(':');
			this._key.Append(e.ResultType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000DA48C File Offset: 0x000D868C
		public override void Visit(DbFilterExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Predicate.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000DA4EC File Offset: 0x000D86EC
		public override void Visit(DbProjectExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Projection.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000DA54C File Offset: 0x000D874C
		public override void Visit(DbCrossJoinExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			foreach (DbExpressionBinding binding in e.Inputs)
			{
				this.VisitBinding(binding);
			}
			this._key.Append(')');
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000DA5C4 File Offset: 0x000D87C4
		public override void Visit(DbJoinExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Left);
			this.VisitBinding(e.Right);
			this._key.Append('(');
			e.JoinCondition.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000DA630 File Offset: 0x000D8830
		public override void Visit(DbApplyExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitBinding(e.Apply);
			this._key.Append(')');
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000DA680 File Offset: 0x000D8880
		public override void Visit(DbGroupByExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitGroupBinding(e.Input);
			foreach (DbExpression dbExpression in e.Keys)
			{
				this._key.Append("K(");
				dbExpression.Accept(this);
				this._key.Append(')');
			}
			foreach (DbAggregate dbAggregate in e.Aggregates)
			{
				DbGroupAggregate dbGroupAggregate = dbAggregate as DbGroupAggregate;
				if (dbGroupAggregate != null)
				{
					this._key.Append("GA(");
					dbGroupAggregate.Arguments[0].Accept(this);
					this._key.Append(')');
				}
				else
				{
					this._key.Append("A:");
					DbFunctionAggregate dbFunctionAggregate = (DbFunctionAggregate)dbAggregate;
					if (dbFunctionAggregate.Distinct)
					{
						this._key.Append("D:");
					}
					this.VisitFunction(dbFunctionAggregate.Function, dbFunctionAggregate.Arguments);
				}
			}
			this._key.Append(')');
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000DA7E4 File Offset: 0x000D89E4
		private void VisitSortOrder(IList<DbSortClause> sortOrder)
		{
			this._key.Append("SO(");
			foreach (DbSortClause dbSortClause in sortOrder)
			{
				this._key.Append(dbSortClause.Ascending ? "ASC(" : "DESC(");
				dbSortClause.Expression.Accept(this);
				this._key.Append(')');
				if (!string.IsNullOrEmpty(dbSortClause.Collation))
				{
					this._key.Append(":(");
					this._key.Append(dbSortClause.Collation);
					this._key.Append(')');
				}
			}
			this._key.Append(')');
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000DA8C0 File Offset: 0x000D8AC0
		public override void Visit(DbSkipExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitSortOrder(e.SortOrder);
			this._key.Append('(');
			e.Count.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000DA92C File Offset: 0x000D8B2C
		public override void Visit(DbSortExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitSortOrder(e.SortOrder);
			this._key.Append(')');
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000DA97C File Offset: 0x000D8B7C
		public override void Visit(DbQuantifierExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Predicate.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x04001859 RID: 6233
		private readonly StringBuilder _key = new StringBuilder();

		// Token: 0x0400185A RID: 6234
		private static string[] _exprKindNames = ExpressionKeyGen.InitializeExprKindNames();
	}
}
