using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x02000128 RID: 296
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal sealed class ExpressionKeyGen : DbExpressionVisitor
	{
		// Token: 0x06000988 RID: 2440 RVA: 0x000305B0 File Offset: 0x0002E7B0
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

		// Token: 0x06000989 RID: 2441 RVA: 0x000305F4 File Offset: 0x0002E7F4
		internal ExpressionKeyGen()
		{
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00030608 File Offset: 0x0002E808
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000306C7 File Offset: 0x0002E8C7
		internal string Key
		{
			get
			{
				return this._key.ToString();
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000306D4 File Offset: 0x0002E8D4
		private void VisitVariableName(string varName)
		{
			this._key.Append('\'');
			this._key.Append(varName.Replace("'", "''"));
			this._key.Append('\'');
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00030710 File Offset: 0x0002E910
		private void VisitBinding(DbExpressionBinding binding)
		{
			this._key.Append("BV");
			this.VisitVariableName(binding.VariableName);
			this._key.Append("=(");
			binding.Expression.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00030768 File Offset: 0x0002E968
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

		// Token: 0x0600098F RID: 2447 RVA: 0x000307DC File Offset: 0x0002E9DC
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

		// Token: 0x06000990 RID: 2448 RVA: 0x00030884 File Offset: 0x0002EA84
		private void VisitExprKind(DbExpressionKind kind)
		{
			this._key.Append('[');
			this._key.Append(ExpressionKeyGen._exprKindNames[(int)kind]);
			this._key.Append(']');
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000308B5 File Offset: 0x0002EAB5
		private void VisitUnary(DbUnaryExpression expr)
		{
			this.VisitExprKind(expr.ExpressionKind);
			this._key.Append('(');
			expr.Argument.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000308EC File Offset: 0x0002EAEC
		private void VisitBinary(DbBinaryExpression expr)
		{
			this.VisitExprKind(expr.ExpressionKind);
			this._key.Append('(');
			expr.Left.Accept(this);
			this._key.Append(',');
			expr.Right.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00030948 File Offset: 0x0002EB48
		private void VisitCastOrTreat(DbUnaryExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.ResultType.Identity);
			this._key.Append(')');
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000309B1 File Offset: 0x0002EBB1
		public override void Visit(DbExpression e)
		{
			Check.NotNull<DbExpression>(e, "e");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000309D4 File Offset: 0x0002EBD4
		public override void Visit(DbConstantExpression e)
		{
			Check.NotNull<DbConstantExpression>(e, "e");
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

		// Token: 0x06000996 RID: 2454 RVA: 0x00030C6A File Offset: 0x0002EE6A
		public override void Visit(DbNullExpression e)
		{
			Check.NotNull<DbNullExpression>(e, "e");
			this._key.Append("NULL:");
			this._key.Append(e.ResultType.Identity);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00030CA0 File Offset: 0x0002EEA0
		public override void Visit(DbVariableReferenceExpression e)
		{
			Check.NotNull<DbVariableReferenceExpression>(e, "e");
			this._key.Append("Var(");
			this.VisitVariableName(e.VariableName);
			this._key.Append(")");
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00030CDC File Offset: 0x0002EEDC
		public override void Visit(DbParameterReferenceExpression e)
		{
			Check.NotNull<DbParameterReferenceExpression>(e, "e");
			this._key.Append("@");
			this._key.Append(e.ParameterName);
			this._key.Append(":");
			this._key.Append(e.ResultType.Identity);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00030D40 File Offset: 0x0002EF40
		public override void Visit(DbFunctionExpression e)
		{
			Check.NotNull<DbFunctionExpression>(e, "e");
			this.VisitFunction(e.Function, e.Arguments);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00030D60 File Offset: 0x0002EF60
		public override void Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
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

		// Token: 0x0600099B RID: 2459 RVA: 0x00030EB8 File Offset: 0x0002F0B8
		public override void Visit(DbPropertyExpression e)
		{
			Check.NotNull<DbPropertyExpression>(e, "e");
			e.Instance.Accept(this);
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append(e.Property.Name);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00030EF5 File Offset: 0x0002F0F5
		public override void Visit(DbComparisonExpression e)
		{
			Check.NotNull<DbComparisonExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00030F0C File Offset: 0x0002F10C
		public override void Visit(DbLikeExpression e)
		{
			Check.NotNull<DbLikeExpression>(e, "e");
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

		// Token: 0x0600099E RID: 2462 RVA: 0x00030FA8 File Offset: 0x0002F1A8
		public override void Visit(DbLimitExpression e)
		{
			Check.NotNull<DbLimitExpression>(e, "e");
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

		// Token: 0x0600099F RID: 2463 RVA: 0x0003102B File Offset: 0x0002F22B
		public override void Visit(DbIsNullExpression e)
		{
			Check.NotNull<DbIsNullExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00031040 File Offset: 0x0002F240
		public override void Visit(DbArithmeticExpression e)
		{
			Check.NotNull<DbArithmeticExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			foreach (DbExpression dbExpression in e.Arguments)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000310C4 File Offset: 0x0002F2C4
		public override void Visit(DbAndExpression e)
		{
			Check.NotNull<DbAndExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000310D9 File Offset: 0x0002F2D9
		public override void Visit(DbOrExpression e)
		{
			Check.NotNull<DbOrExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000310F0 File Offset: 0x0002F2F0
		public override void Visit(DbInExpression e)
		{
			Check.NotNull<DbInExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Item.Accept(this);
			this._key.Append(",(");
			bool flag = true;
			foreach (DbExpression dbExpression in e.List)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this._key.Append(',');
				}
				dbExpression.Accept(this);
			}
			this._key.Append("))");
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000311A8 File Offset: 0x0002F3A8
		public override void Visit(DbNotExpression e)
		{
			Check.NotNull<DbNotExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000311BD File Offset: 0x0002F3BD
		public override void Visit(DbDistinctExpression e)
		{
			Check.NotNull<DbDistinctExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000311D2 File Offset: 0x0002F3D2
		public override void Visit(DbElementExpression e)
		{
			Check.NotNull<DbElementExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000311E7 File Offset: 0x0002F3E7
		public override void Visit(DbIsEmptyExpression e)
		{
			Check.NotNull<DbIsEmptyExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000311FC File Offset: 0x0002F3FC
		public override void Visit(DbUnionAllExpression e)
		{
			Check.NotNull<DbUnionAllExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00031211 File Offset: 0x0002F411
		public override void Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00031226 File Offset: 0x0002F426
		public override void Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0003123B File Offset: 0x0002F43B
		public override void Visit(DbTreatExpression e)
		{
			Check.NotNull<DbTreatExpression>(e, "e");
			this.VisitCastOrTreat(e);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00031250 File Offset: 0x0002F450
		public override void Visit(DbCastExpression e)
		{
			Check.NotNull<DbCastExpression>(e, "e");
			this.VisitCastOrTreat(e);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00031268 File Offset: 0x0002F468
		public override void Visit(DbIsOfExpression e)
		{
			Check.NotNull<DbIsOfExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.OfType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000312E4 File Offset: 0x0002F4E4
		public override void Visit(DbOfTypeExpression e)
		{
			Check.NotNull<DbOfTypeExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.OfType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00031360 File Offset: 0x0002F560
		public override void Visit(DbCaseExpression e)
		{
			Check.NotNull<DbCaseExpression>(e, "e");
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

		// Token: 0x060009B0 RID: 2480 RVA: 0x00031420 File Offset: 0x0002F620
		public override void Visit(DbNewInstanceExpression e)
		{
			Check.NotNull<DbNewInstanceExpression>(e, "e");
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

		// Token: 0x060009B1 RID: 2481 RVA: 0x000315D8 File Offset: 0x0002F7D8
		public override void Visit(DbRefExpression e)
		{
			Check.NotNull<DbRefExpression>(e, "e");
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

		// Token: 0x060009B2 RID: 2482 RVA: 0x000316AC File Offset: 0x0002F8AC
		public override void Visit(DbRelationshipNavigationExpression e)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
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

		// Token: 0x060009B3 RID: 2483 RVA: 0x00031779 File Offset: 0x0002F979
		public override void Visit(DbDerefExpression e)
		{
			Check.NotNull<DbDerefExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0003178E File Offset: 0x0002F98E
		public override void Visit(DbRefKeyExpression e)
		{
			Check.NotNull<DbRefKeyExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000317A3 File Offset: 0x0002F9A3
		public override void Visit(DbEntityRefExpression e)
		{
			Check.NotNull<DbEntityRefExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000317B8 File Offset: 0x0002F9B8
		public override void Visit(DbScanExpression e)
		{
			Check.NotNull<DbScanExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this._key.Append(e.Target.EntityContainer.Name);
			this._key.Append('.');
			this._key.Append(e.Target.Name);
			this._key.Append(':');
			this._key.Append(e.ResultType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00031864 File Offset: 0x0002FA64
		public override void Visit(DbFilterExpression e)
		{
			Check.NotNull<DbFilterExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Predicate.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000318D0 File Offset: 0x0002FAD0
		public override void Visit(DbProjectExpression e)
		{
			Check.NotNull<DbProjectExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Projection.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0003193C File Offset: 0x0002FB3C
		public override void Visit(DbCrossJoinExpression e)
		{
			Check.NotNull<DbCrossJoinExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			foreach (DbExpressionBinding binding in e.Inputs)
			{
				this.VisitBinding(binding);
			}
			this._key.Append(')');
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000319C0 File Offset: 0x0002FBC0
		public override void Visit(DbJoinExpression e)
		{
			Check.NotNull<DbJoinExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Left);
			this.VisitBinding(e.Right);
			this._key.Append('(');
			e.JoinCondition.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00031A38 File Offset: 0x0002FC38
		public override void Visit(DbApplyExpression e)
		{
			Check.NotNull<DbApplyExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitBinding(e.Apply);
			this._key.Append(')');
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00031A94 File Offset: 0x0002FC94
		public override void Visit(DbGroupByExpression e)
		{
			Check.NotNull<DbGroupByExpression>(e, "e");
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

		// Token: 0x060009BD RID: 2493 RVA: 0x00031C08 File Offset: 0x0002FE08
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

		// Token: 0x060009BE RID: 2494 RVA: 0x00031CE4 File Offset: 0x0002FEE4
		public override void Visit(DbSkipExpression e)
		{
			Check.NotNull<DbSkipExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitSortOrder(e.SortOrder);
			this._key.Append('(');
			e.Count.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00031D5C File Offset: 0x0002FF5C
		public override void Visit(DbSortExpression e)
		{
			Check.NotNull<DbSortExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitSortOrder(e.SortOrder);
			this._key.Append(')');
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00031DB8 File Offset: 0x0002FFB8
		public override void Visit(DbQuantifierExpression e)
		{
			Check.NotNull<DbQuantifierExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Predicate.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x04000296 RID: 662
		private readonly StringBuilder _key = new StringBuilder();

		// Token: 0x04000297 RID: 663
		private static readonly string[] _exprKindNames = ExpressionKeyGen.InitializeExprKindNames();
	}
}
