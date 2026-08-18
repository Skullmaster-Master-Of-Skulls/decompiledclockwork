using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Spatial;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C8 RID: 712
	internal class Propagator : UpdateExpressionVisitor<ChangeNode>
	{
		// Token: 0x060029F6 RID: 10742 RVA: 0x000A432C File Offset: 0x000A252C
		private Propagator(UpdateTranslator parent, EntitySet table)
		{
			EntityUtil.CheckArgumentNull<UpdateTranslator>(parent, "parent");
			EntityUtil.CheckArgumentNull<EntitySet>(table, "table");
			this.m_updateTranslator = parent;
			this.m_table = table;
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x000A435A File Offset: 0x000A255A
		internal UpdateTranslator UpdateTranslator
		{
			get
			{
				return this.m_updateTranslator;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000A4362 File Offset: 0x000A2562
		protected override string VisitorName
		{
			get
			{
				return Propagator.s_visitorName;
			}
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000A436C File Offset: 0x000A256C
		internal static ChangeNode Propagate(UpdateTranslator parent, EntitySet table, DbQueryCommandTree umView)
		{
			DbExpressionVisitor<ChangeNode> visitor = new Propagator(parent, table);
			return umView.Query.Accept<ChangeNode>(visitor);
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000A4390 File Offset: 0x000A2590
		private static ChangeNode BuildChangeNode(DbExpression node)
		{
			TypeUsage resultType = node.ResultType;
			TypeUsage elementType = MetadataHelper.GetElementType(resultType);
			return new ChangeNode(elementType);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000A43B1 File Offset: 0x000A25B1
		public override ChangeNode Visit(DbCrossJoinExpression node)
		{
			throw EntityUtil.NotSupported(Strings.Update_UnsupportedJoinType(node.ExpressionKind));
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000A43C8 File Offset: 0x000A25C8
		public override ChangeNode Visit(DbJoinExpression node)
		{
			EntityUtil.CheckArgumentNull<DbJoinExpression>(node, "node");
			if (DbExpressionKind.InnerJoin != node.ExpressionKind && DbExpressionKind.LeftOuterJoin != node.ExpressionKind)
			{
				throw EntityUtil.NotSupported(Strings.Update_UnsupportedJoinType(node.ExpressionKind));
			}
			DbExpression expression = node.Left.Expression;
			DbExpression expression2 = node.Right.Expression;
			ChangeNode left = this.Visit(expression);
			ChangeNode right = this.Visit(expression2);
			Propagator.JoinPropagator joinPropagator = new Propagator.JoinPropagator(left, right, node, this);
			return joinPropagator.Propagate();
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000A444C File Offset: 0x000A264C
		public override ChangeNode Visit(DbUnionAllExpression node)
		{
			EntityUtil.CheckArgumentNull<DbUnionAllExpression>(node, "node");
			ChangeNode changeNode = Propagator.BuildChangeNode(node);
			ChangeNode changeNode2 = this.Visit(node.Left);
			ChangeNode changeNode3 = this.Visit(node.Right);
			changeNode.Inserted.AddRange(changeNode2.Inserted);
			changeNode.Inserted.AddRange(changeNode3.Inserted);
			changeNode.Deleted.AddRange(changeNode2.Deleted);
			changeNode.Deleted.AddRange(changeNode3.Deleted);
			changeNode.Placeholder = changeNode2.Placeholder;
			return changeNode;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000A44D8 File Offset: 0x000A26D8
		public override ChangeNode Visit(DbProjectExpression node)
		{
			EntityUtil.CheckArgumentNull<DbProjectExpression>(node, "node");
			ChangeNode changeNode = Propagator.BuildChangeNode(node);
			ChangeNode changeNode2 = this.Visit(node.Input.Expression);
			foreach (PropagatorResult row in changeNode2.Inserted)
			{
				changeNode.Inserted.Add(this.Project(node, row, changeNode.ElementType));
			}
			foreach (PropagatorResult row2 in changeNode2.Deleted)
			{
				changeNode.Deleted.Add(this.Project(node, row2, changeNode.ElementType));
			}
			changeNode.Placeholder = this.Project(node, changeNode2.Placeholder, changeNode.ElementType);
			return changeNode;
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000A45D4 File Offset: 0x000A27D4
		private PropagatorResult Project(DbProjectExpression node, PropagatorResult row, TypeUsage resultType)
		{
			EntityUtil.CheckArgumentNull<DbProjectExpression>(node, "node");
			DbNewInstanceExpression dbNewInstanceExpression = node.Projection as DbNewInstanceExpression;
			if (dbNewInstanceExpression == null)
			{
				throw EntityUtil.NotSupported(Strings.Update_UnsupportedProjection(node.Projection.ExpressionKind));
			}
			PropagatorResult[] array = new PropagatorResult[dbNewInstanceExpression.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Propagator.Evaluator.Evaluate(dbNewInstanceExpression.Arguments[i], row, this);
			}
			return PropagatorResult.CreateStructuralValue(array, (StructuralType)resultType.EdmType, false);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000A4660 File Offset: 0x000A2860
		public override ChangeNode Visit(DbFilterExpression node)
		{
			EntityUtil.CheckArgumentNull<DbFilterExpression>(node, "node");
			ChangeNode changeNode = Propagator.BuildChangeNode(node);
			ChangeNode changeNode2 = this.Visit(node.Input.Expression);
			changeNode.Inserted.AddRange(Propagator.Evaluator.Filter(node.Predicate, changeNode2.Inserted, this));
			changeNode.Deleted.AddRange(Propagator.Evaluator.Filter(node.Predicate, changeNode2.Deleted, this));
			changeNode.Placeholder = changeNode2.Placeholder;
			return changeNode;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000A46DC File Offset: 0x000A28DC
		public override ChangeNode Visit(DbScanExpression node)
		{
			EntityUtil.CheckArgumentNull<DbScanExpression>(node, "node");
			EntitySetBase target = node.Target;
			ChangeNode extentModifications = this.UpdateTranslator.GetExtentModifications(target);
			if (extentModifications.Placeholder == null)
			{
				extentModifications.Placeholder = Propagator.ExtentPlaceholderCreator.CreatePlaceholder(target, this.UpdateTranslator);
			}
			return extentModifications;
		}

		// Token: 0x040012B6 RID: 4790
		private readonly UpdateTranslator m_updateTranslator;

		// Token: 0x040012B7 RID: 4791
		private readonly EntitySet m_table;

		// Token: 0x040012B8 RID: 4792
		private static readonly string s_visitorName = typeof(Propagator).FullName;

		// Token: 0x0200061D RID: 1565
		private class Evaluator : UpdateExpressionVisitor<PropagatorResult>
		{
			// Token: 0x060042C6 RID: 17094 RVA: 0x000F296B File Offset: 0x000F0B6B
			private Evaluator(PropagatorResult row, Propagator parent)
			{
				EntityUtil.CheckArgumentNull<PropagatorResult>(row, "row");
				EntityUtil.CheckArgumentNull<Propagator>(parent, "parent");
				this.m_row = row;
				this.m_parent = parent;
			}

			// Token: 0x17000B7B RID: 2939
			// (get) Token: 0x060042C7 RID: 17095 RVA: 0x000F2999 File Offset: 0x000F0B99
			protected override string VisitorName
			{
				get
				{
					return Propagator.Evaluator.s_visitorName;
				}
			}

			// Token: 0x060042C8 RID: 17096 RVA: 0x000F29A0 File Offset: 0x000F0BA0
			internal static IEnumerable<PropagatorResult> Filter(DbExpression predicate, IEnumerable<PropagatorResult> rows, Propagator parent)
			{
				foreach (PropagatorResult propagatorResult in rows)
				{
					if (Propagator.Evaluator.EvaluatePredicate(predicate, propagatorResult, parent))
					{
						yield return propagatorResult;
					}
				}
				IEnumerator<PropagatorResult> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060042C9 RID: 17097 RVA: 0x000F29C0 File Offset: 0x000F0BC0
			internal static bool EvaluatePredicate(DbExpression predicate, PropagatorResult row, Propagator parent)
			{
				Propagator.Evaluator visitor = new Propagator.Evaluator(row, parent);
				PropagatorResult result = predicate.Accept<PropagatorResult>(visitor);
				return Propagator.Evaluator.ConvertResultToBool(result).GetValueOrDefault();
			}

			// Token: 0x060042CA RID: 17098 RVA: 0x000F29EC File Offset: 0x000F0BEC
			internal static PropagatorResult Evaluate(DbExpression node, PropagatorResult row, Propagator parent)
			{
				DbExpressionVisitor<PropagatorResult> visitor = new Propagator.Evaluator(row, parent);
				return node.Accept<PropagatorResult>(visitor);
			}

			// Token: 0x060042CB RID: 17099 RVA: 0x000F2A08 File Offset: 0x000F0C08
			private static bool? ConvertResultToBool(PropagatorResult result)
			{
				if (result.IsNull)
				{
					return null;
				}
				return new bool?((bool)result.GetSimpleValue());
			}

			// Token: 0x060042CC RID: 17100 RVA: 0x000F2A38 File Offset: 0x000F0C38
			private static PropagatorResult ConvertBoolToResult(bool? booleanValue, params PropagatorResult[] inputs)
			{
				object value;
				if (booleanValue != null)
				{
					value = booleanValue.Value;
				}
				else
				{
					value = null;
				}
				PropagatorFlags flags = Propagator.Evaluator.PropagateUnknownAndPreserveFlags(null, inputs);
				return PropagatorResult.CreateSimpleValue(flags, value);
			}

			// Token: 0x060042CD RID: 17101 RVA: 0x000F2A70 File Offset: 0x000F0C70
			public override PropagatorResult Visit(DbIsOfExpression predicate)
			{
				EntityUtil.CheckArgumentNull<DbIsOfExpression>(predicate, "predicate");
				if (DbExpressionKind.IsOfOnly != predicate.ExpressionKind)
				{
					throw base.ConstructNotSupportedException(predicate);
				}
				PropagatorResult propagatorResult = this.Visit(predicate.Argument);
				bool value = !propagatorResult.IsNull && propagatorResult.StructuralType.EdmEquals(predicate.OfType.EdmType);
				return Propagator.Evaluator.ConvertBoolToResult(new bool?(value), new PropagatorResult[]
				{
					propagatorResult
				});
			}

			// Token: 0x060042CE RID: 17102 RVA: 0x000F2AE0 File Offset: 0x000F0CE0
			public override PropagatorResult Visit(DbComparisonExpression predicate)
			{
				EntityUtil.CheckArgumentNull<DbComparisonExpression>(predicate, "predicate");
				if (DbExpressionKind.Equals == predicate.ExpressionKind)
				{
					PropagatorResult propagatorResult = this.Visit(predicate.Left);
					PropagatorResult propagatorResult2 = this.Visit(predicate.Right);
					bool? booleanValue;
					if (propagatorResult.IsNull || propagatorResult2.IsNull)
					{
						booleanValue = null;
					}
					else
					{
						object simpleValue = propagatorResult.GetSimpleValue();
						object simpleValue2 = propagatorResult2.GetSimpleValue();
						booleanValue = new bool?(ByValueEqualityComparer.Default.Equals(simpleValue, simpleValue2));
					}
					return Propagator.Evaluator.ConvertBoolToResult(booleanValue, new PropagatorResult[]
					{
						propagatorResult,
						propagatorResult2
					});
				}
				throw base.ConstructNotSupportedException(predicate);
			}

			// Token: 0x060042CF RID: 17103 RVA: 0x000F2B78 File Offset: 0x000F0D78
			public override PropagatorResult Visit(DbAndExpression predicate)
			{
				EntityUtil.CheckArgumentNull<DbAndExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Left);
				PropagatorResult propagatorResult2 = this.Visit(predicate.Right);
				bool? left = Propagator.Evaluator.ConvertResultToBool(propagatorResult);
				bool? right = Propagator.Evaluator.ConvertResultToBool(propagatorResult2);
				if ((left != null && !left.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult)) || (right != null && !right.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult2)))
				{
					return Propagator.Evaluator.CreatePerservedAndKnownResult(false);
				}
				bool? booleanValue = EntityUtil.ThreeValuedAnd(left, right);
				return Propagator.Evaluator.ConvertBoolToResult(booleanValue, new PropagatorResult[]
				{
					propagatorResult,
					propagatorResult2
				});
			}

			// Token: 0x060042D0 RID: 17104 RVA: 0x000F2C18 File Offset: 0x000F0E18
			public override PropagatorResult Visit(DbOrExpression predicate)
			{
				EntityUtil.CheckArgumentNull<DbOrExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Left);
				PropagatorResult propagatorResult2 = this.Visit(predicate.Right);
				bool? left = Propagator.Evaluator.ConvertResultToBool(propagatorResult);
				bool? right = Propagator.Evaluator.ConvertResultToBool(propagatorResult2);
				if ((left != null && left.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult)) || (right != null && right.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult2)))
				{
					return Propagator.Evaluator.CreatePerservedAndKnownResult(true);
				}
				bool? booleanValue = EntityUtil.ThreeValuedOr(left, right);
				return Propagator.Evaluator.ConvertBoolToResult(booleanValue, new PropagatorResult[]
				{
					propagatorResult,
					propagatorResult2
				});
			}

			// Token: 0x060042D1 RID: 17105 RVA: 0x000F2CB7 File Offset: 0x000F0EB7
			private static PropagatorResult CreatePerservedAndKnownResult(object value)
			{
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, value);
			}

			// Token: 0x060042D2 RID: 17106 RVA: 0x000F2CC0 File Offset: 0x000F0EC0
			private static bool PreservedAndKnown(PropagatorResult result)
			{
				return PropagatorFlags.Preserve == (result.PropagatorFlags & (PropagatorFlags.Preserve | PropagatorFlags.Unknown));
			}

			// Token: 0x060042D3 RID: 17107 RVA: 0x000F2CD0 File Offset: 0x000F0ED0
			public override PropagatorResult Visit(DbNotExpression predicate)
			{
				EntityUtil.CheckArgumentNull<DbNotExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Argument);
				bool? operand = Propagator.Evaluator.ConvertResultToBool(propagatorResult);
				bool? booleanValue = EntityUtil.ThreeValuedNot(operand);
				return Propagator.Evaluator.ConvertBoolToResult(booleanValue, new PropagatorResult[]
				{
					propagatorResult
				});
			}

			// Token: 0x060042D4 RID: 17108 RVA: 0x000F2D14 File Offset: 0x000F0F14
			public override PropagatorResult Visit(DbCaseExpression node)
			{
				int num = -1;
				int num2 = 0;
				List<PropagatorResult> list = new List<PropagatorResult>();
				foreach (DbExpression expression in node.When)
				{
					PropagatorResult propagatorResult = this.Visit(expression);
					list.Add(propagatorResult);
					bool valueOrDefault = Propagator.Evaluator.ConvertResultToBool(propagatorResult).GetValueOrDefault();
					if (valueOrDefault)
					{
						num = num2;
						break;
					}
					num2++;
				}
				PropagatorResult propagatorResult2;
				if (-1 == num)
				{
					propagatorResult2 = this.Visit(node.Else);
				}
				else
				{
					propagatorResult2 = this.Visit(node.Then[num]);
				}
				list.Add(propagatorResult2);
				PropagatorFlags flags = Propagator.Evaluator.PropagateUnknownAndPreserveFlags(propagatorResult2, list);
				return propagatorResult2.ReplicateResultWithNewFlags(flags);
			}

			// Token: 0x060042D5 RID: 17109 RVA: 0x000F2DDC File Offset: 0x000F0FDC
			public override PropagatorResult Visit(DbVariableReferenceExpression node)
			{
				return this.m_row;
			}

			// Token: 0x060042D6 RID: 17110 RVA: 0x000F2DE4 File Offset: 0x000F0FE4
			public override PropagatorResult Visit(DbPropertyExpression node)
			{
				PropagatorResult propagatorResult = this.Visit(node.Instance);
				PropagatorResult result;
				if (propagatorResult.IsNull)
				{
					result = PropagatorResult.CreateSimpleValue(propagatorResult.PropagatorFlags, null);
				}
				else
				{
					result = propagatorResult.GetMemberValue(node.Property);
				}
				return result;
			}

			// Token: 0x060042D7 RID: 17111 RVA: 0x000F2E24 File Offset: 0x000F1024
			public override PropagatorResult Visit(DbConstantExpression node)
			{
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, node.Value);
			}

			// Token: 0x060042D8 RID: 17112 RVA: 0x000F2E40 File Offset: 0x000F1040
			public override PropagatorResult Visit(DbRefKeyExpression node)
			{
				return this.Visit(node.Argument);
			}

			// Token: 0x060042D9 RID: 17113 RVA: 0x000F2E5C File Offset: 0x000F105C
			public override PropagatorResult Visit(DbNullExpression node)
			{
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, null);
			}

			// Token: 0x060042DA RID: 17114 RVA: 0x000F2E74 File Offset: 0x000F1074
			public override PropagatorResult Visit(DbTreatExpression node)
			{
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				TypeUsage resultType = node.ResultType;
				if (MetadataHelper.IsSuperTypeOf(resultType.EdmType, propagatorResult.StructuralType))
				{
					return propagatorResult;
				}
				return PropagatorResult.CreateSimpleValue(propagatorResult.PropagatorFlags, null);
			}

			// Token: 0x060042DB RID: 17115 RVA: 0x000F2EB8 File Offset: 0x000F10B8
			public override PropagatorResult Visit(DbCastExpression node)
			{
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				TypeUsage resultType = node.ResultType;
				if (!propagatorResult.IsSimple || BuiltInTypeKind.PrimitiveType != resultType.EdmType.BuiltInTypeKind)
				{
					throw EntityUtil.NotSupported(Strings.Update_UnsupportedCastArgument(resultType.EdmType.Name));
				}
				object value;
				if (propagatorResult.IsNull)
				{
					value = null;
				}
				else
				{
					try
					{
						value = Propagator.Evaluator.Cast(propagatorResult.GetSimpleValue(), ((PrimitiveType)resultType.EdmType).ClrEquivalentType);
					}
					catch
					{
						throw;
					}
				}
				return propagatorResult.ReplicateResultWithNewValue(value);
			}

			// Token: 0x060042DC RID: 17116 RVA: 0x000F2F50 File Offset: 0x000F1150
			private static object Cast(object value, Type clrPrimitiveType)
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				if (value == null || value == DBNull.Value || value.GetType() == clrPrimitiveType)
				{
					return value;
				}
				if (value is DateTime && clrPrimitiveType == typeof(DateTimeOffset))
				{
					return new DateTimeOffset(((DateTime)value).Ticks, TimeSpan.Zero);
				}
				return Convert.ChangeType(value, clrPrimitiveType, invariantCulture);
			}

			// Token: 0x060042DD RID: 17117 RVA: 0x000F2FC0 File Offset: 0x000F11C0
			public override PropagatorResult Visit(DbIsNullExpression node)
			{
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				bool isNull = propagatorResult.IsNull;
				return Propagator.Evaluator.ConvertBoolToResult(new bool?(isNull), new PropagatorResult[]
				{
					propagatorResult
				});
			}

			// Token: 0x060042DE RID: 17118 RVA: 0x000F2FF8 File Offset: 0x000F11F8
			private static PropagatorFlags PropagateUnknownAndPreserveFlags(PropagatorResult result, IEnumerable<PropagatorResult> inputs)
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = true;
				foreach (PropagatorResult propagatorResult in inputs)
				{
					flag3 = false;
					PropagatorFlags propagatorFlags = propagatorResult.PropagatorFlags;
					if ((PropagatorFlags.Unknown & propagatorFlags) != PropagatorFlags.NoFlags)
					{
						flag = true;
					}
					if ((PropagatorFlags.Preserve & propagatorFlags) == PropagatorFlags.NoFlags)
					{
						flag2 = false;
					}
				}
				if (flag3)
				{
					flag2 = false;
				}
				if (result != null)
				{
					PropagatorFlags propagatorFlags2 = result.PropagatorFlags;
					if (flag)
					{
						propagatorFlags2 |= PropagatorFlags.Unknown;
					}
					if (!flag2)
					{
						propagatorFlags2 &= ~PropagatorFlags.Preserve;
					}
					return propagatorFlags2;
				}
				PropagatorFlags propagatorFlags3 = PropagatorFlags.NoFlags;
				if (flag)
				{
					propagatorFlags3 |= PropagatorFlags.Unknown;
				}
				if (flag2)
				{
					propagatorFlags3 |= PropagatorFlags.Preserve;
				}
				return propagatorFlags3;
			}

			// Token: 0x04001E52 RID: 7762
			private PropagatorResult m_row;

			// Token: 0x04001E53 RID: 7763
			private Propagator m_parent;

			// Token: 0x04001E54 RID: 7764
			private static readonly string s_visitorName = typeof(Propagator.Evaluator).FullName;
		}

		// Token: 0x0200061E RID: 1566
		private class ExtentPlaceholderCreator
		{
			// Token: 0x060042E0 RID: 17120 RVA: 0x000F30B2 File Offset: 0x000F12B2
			private ExtentPlaceholderCreator(UpdateTranslator parent)
			{
				EntityUtil.CheckArgumentNull<UpdateTranslator>(parent, "parent");
				this.m_parent = parent;
			}

			// Token: 0x060042E1 RID: 17121 RVA: 0x000F30D0 File Offset: 0x000F12D0
			private static Dictionary<PrimitiveTypeKind, object> InitializeTypeDefaultMap()
			{
				Dictionary<PrimitiveTypeKind, object> dictionary = new Dictionary<PrimitiveTypeKind, object>(EqualityComparer<PrimitiveTypeKind>.Default);
				dictionary[PrimitiveTypeKind.Binary] = new byte[0];
				dictionary[PrimitiveTypeKind.Boolean] = false;
				dictionary[PrimitiveTypeKind.Byte] = 0;
				dictionary[PrimitiveTypeKind.DateTime] = default(DateTime);
				dictionary[PrimitiveTypeKind.Time] = default(TimeSpan);
				dictionary[PrimitiveTypeKind.DateTimeOffset] = default(DateTimeOffset);
				dictionary[PrimitiveTypeKind.Decimal] = 0m;
				dictionary[PrimitiveTypeKind.Double] = 0.0;
				dictionary[PrimitiveTypeKind.Guid] = default(Guid);
				dictionary[PrimitiveTypeKind.Int16] = 0;
				dictionary[PrimitiveTypeKind.Int32] = 0;
				dictionary[PrimitiveTypeKind.Int64] = 0L;
				dictionary[PrimitiveTypeKind.Single] = 0f;
				dictionary[PrimitiveTypeKind.SByte] = 0;
				dictionary[PrimitiveTypeKind.String] = string.Empty;
				dictionary[PrimitiveTypeKind.Geometry] = DbGeometry.FromText("POINT EMPTY");
				dictionary[PrimitiveTypeKind.GeometryPoint] = DbGeometry.FromText("POINT EMPTY");
				dictionary[PrimitiveTypeKind.GeometryLineString] = DbGeometry.FromText("LINESTRING EMPTY");
				dictionary[PrimitiveTypeKind.GeometryPolygon] = DbGeometry.FromText("POLYGON EMPTY");
				dictionary[PrimitiveTypeKind.GeometryMultiPoint] = DbGeometry.FromText("MULTIPOINT EMPTY");
				dictionary[PrimitiveTypeKind.GeometryMultiLineString] = DbGeometry.FromText("MULTILINESTRING EMPTY");
				dictionary[PrimitiveTypeKind.GeometryMultiPolygon] = DbGeometry.FromText("MULTIPOLYGON EMPTY");
				dictionary[PrimitiveTypeKind.GeometryCollection] = DbGeometry.FromText("GEOMETRYCOLLECTION EMPTY");
				dictionary[PrimitiveTypeKind.Geography] = DbGeography.FromText("POINT EMPTY");
				dictionary[PrimitiveTypeKind.GeographyPoint] = DbGeography.FromText("POINT EMPTY");
				dictionary[PrimitiveTypeKind.GeographyLineString] = DbGeography.FromText("LINESTRING EMPTY");
				dictionary[PrimitiveTypeKind.GeographyPolygon] = DbGeography.FromText("POLYGON EMPTY");
				dictionary[PrimitiveTypeKind.GeographyMultiPoint] = DbGeography.FromText("MULTIPOINT EMPTY");
				dictionary[PrimitiveTypeKind.GeographyMultiLineString] = DbGeography.FromText("MULTILINESTRING EMPTY");
				dictionary[PrimitiveTypeKind.GeographyMultiPolygon] = DbGeography.FromText("MULTIPOLYGON EMPTY");
				dictionary[PrimitiveTypeKind.GeographyCollection] = DbGeography.FromText("GEOMETRYCOLLECTION EMPTY");
				return dictionary;
			}

			// Token: 0x060042E2 RID: 17122 RVA: 0x000F3304 File Offset: 0x000F1504
			internal static PropagatorResult CreatePlaceholder(EntitySetBase extent, UpdateTranslator parent)
			{
				EntityUtil.CheckArgumentNull<EntitySetBase>(extent, "extent");
				Propagator.ExtentPlaceholderCreator extentPlaceholderCreator = new Propagator.ExtentPlaceholderCreator(parent);
				AssociationSet associationSet = extent as AssociationSet;
				if (associationSet != null)
				{
					return extentPlaceholderCreator.CreateAssociationSetPlaceholder(associationSet);
				}
				EntitySet entitySet = extent as EntitySet;
				if (entitySet != null)
				{
					return extentPlaceholderCreator.CreateEntitySetPlaceholder(entitySet);
				}
				throw EntityUtil.NotSupported(Strings.Update_UnsupportedExtentType(extent.Name, extent.GetType().Name));
			}

			// Token: 0x060042E3 RID: 17123 RVA: 0x000F3364 File Offset: 0x000F1564
			private PropagatorResult CreateEntitySetPlaceholder(EntitySet entitySet)
			{
				EntityUtil.CheckArgumentNull<EntitySet>(entitySet, "entitySet");
				ReadOnlyMetadataCollection<EdmProperty> properties = entitySet.ElementType.Properties;
				PropagatorResult[] array = new PropagatorResult[properties.Count];
				for (int i = 0; i < properties.Count; i++)
				{
					PropagatorResult propagatorResult = this.CreateMemberPlaceholder(properties[i]);
					array[i] = propagatorResult;
				}
				return PropagatorResult.CreateStructuralValue(array, entitySet.ElementType, false);
			}

			// Token: 0x060042E4 RID: 17124 RVA: 0x000F33CC File Offset: 0x000F15CC
			private PropagatorResult CreateAssociationSetPlaceholder(AssociationSet associationSet)
			{
				ReadOnlyMetadataCollection<AssociationEndMember> associationEndMembers = associationSet.ElementType.AssociationEndMembers;
				PropagatorResult[] array = new PropagatorResult[associationEndMembers.Count];
				for (int i = 0; i < associationEndMembers.Count; i++)
				{
					AssociationEndMember associationEndMember = associationEndMembers[i];
					EntityType entityType = (EntityType)((RefType)associationEndMember.TypeUsage.EdmType).ElementType;
					PropagatorResult[] array2 = new PropagatorResult[entityType.KeyMembers.Count];
					for (int j = 0; j < entityType.KeyMembers.Count; j++)
					{
						EdmMember member = entityType.KeyMembers[j];
						PropagatorResult propagatorResult = this.CreateMemberPlaceholder(member);
						array2[j] = propagatorResult;
					}
					RowType keyRowType = entityType.GetKeyRowType(this.m_parent.MetadataWorkspace);
					PropagatorResult propagatorResult2 = PropagatorResult.CreateStructuralValue(array2, keyRowType, false);
					array[i] = propagatorResult2;
				}
				return PropagatorResult.CreateStructuralValue(array, associationSet.ElementType, false);
			}

			// Token: 0x060042E5 RID: 17125 RVA: 0x000F34B1 File Offset: 0x000F16B1
			private PropagatorResult CreateMemberPlaceholder(EdmMember member)
			{
				EntityUtil.CheckArgumentNull<EdmMember>(member, "member");
				return this.Visit(member);
			}

			// Token: 0x060042E6 RID: 17126 RVA: 0x000F34C8 File Offset: 0x000F16C8
			internal PropagatorResult Visit(EdmMember node)
			{
				TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(node);
				PropagatorResult result;
				if (Helper.IsScalarType(modelTypeUsage.EdmType))
				{
					Propagator.ExtentPlaceholderCreator.GetPropagatorResultForPrimitiveType(Helper.AsPrimitive(modelTypeUsage.EdmType), out result);
				}
				else
				{
					StructuralType structuralType = (StructuralType)modelTypeUsage.EdmType;
					IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(structuralType);
					PropagatorResult[] array = new PropagatorResult[allStructuralMembers.Count];
					for (int i = 0; i < allStructuralMembers.Count; i++)
					{
						array[i] = this.Visit(allStructuralMembers[i]);
					}
					result = PropagatorResult.CreateStructuralValue(array, structuralType, false);
				}
				return result;
			}

			// Token: 0x060042E7 RID: 17127 RVA: 0x000F3550 File Offset: 0x000F1750
			private static void GetPropagatorResultForPrimitiveType(PrimitiveType primitiveType, out PropagatorResult result)
			{
				PrimitiveTypeKind primitiveTypeKind = primitiveType.PrimitiveTypeKind;
				object value;
				if (!Propagator.ExtentPlaceholderCreator.s_typeDefaultMap.TryGetValue(primitiveTypeKind, out value))
				{
					value = 0;
				}
				result = PropagatorResult.CreateSimpleValue(PropagatorFlags.NoFlags, value);
			}

			// Token: 0x04001E55 RID: 7765
			private static Dictionary<PrimitiveTypeKind, object> s_typeDefaultMap = Propagator.ExtentPlaceholderCreator.InitializeTypeDefaultMap();

			// Token: 0x04001E56 RID: 7766
			private UpdateTranslator m_parent;
		}

		// Token: 0x0200061F RID: 1567
		private class JoinPropagator
		{
			// Token: 0x060042E9 RID: 17129 RVA: 0x000F3590 File Offset: 0x000F1790
			internal JoinPropagator(ChangeNode left, ChangeNode right, DbJoinExpression node, Propagator parent)
			{
				EntityUtil.CheckArgumentNull<ChangeNode>(left, "left");
				EntityUtil.CheckArgumentNull<ChangeNode>(right, "right");
				EntityUtil.CheckArgumentNull<DbJoinExpression>(node, "node");
				EntityUtil.CheckArgumentNull<Propagator>(parent, "parent");
				this.m_left = left;
				this.m_right = right;
				this.m_joinExpression = node;
				this.m_parent = parent;
				if (DbExpressionKind.InnerJoin == this.m_joinExpression.ExpressionKind)
				{
					this.m_insertRules = Propagator.JoinPropagator.s_innerJoinInsertRules;
					this.m_deleteRules = Propagator.JoinPropagator.s_innerJoinDeleteRules;
				}
				else
				{
					this.m_insertRules = Propagator.JoinPropagator.s_leftOuterJoinInsertRules;
					this.m_deleteRules = Propagator.JoinPropagator.s_leftOuterJoinDeleteRules;
				}
				Propagator.JoinPropagator.JoinConditionVisitor.GetKeySelectors(node.JoinCondition, out this.m_leftKeySelectors, out this.m_rightKeySelectors);
				this.m_leftPlaceholderKey = Propagator.JoinPropagator.ExtractKey(this.m_left.Placeholder, this.m_leftKeySelectors, this.m_parent);
				this.m_rightPlaceholderKey = Propagator.JoinPropagator.ExtractKey(this.m_right.Placeholder, this.m_rightKeySelectors, this.m_parent);
			}

			// Token: 0x060042EA RID: 17130 RVA: 0x000F368C File Offset: 0x000F188C
			static JoinPropagator()
			{
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightInsert | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete, Propagator.JoinPropagator.Ops.Nothing, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete, Propagator.JoinPropagator.Ops.Nothing, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.Nothing, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.Nothing);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftUpdate, Propagator.JoinPropagator.Ops.LeftInsertUnknownExtended, Propagator.JoinPropagator.Ops.LeftDeleteUnknownExtended, Propagator.JoinPropagator.Ops.LeftInsertUnknownExtended, Propagator.JoinPropagator.Ops.LeftDeleteUnknownExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.RightUpdate, Propagator.JoinPropagator.Ops.RightInsertUnknownExtended, Propagator.JoinPropagator.Ops.RightDeleteUnknownExtended, Propagator.JoinPropagator.Ops.RightInsertUnknownExtended, Propagator.JoinPropagator.Ops.RightDeleteUnknownExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.LeftInsertNullModifiedExtended, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightInsert, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.LeftDeleteNullModifiedExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Nothing, Propagator.JoinPropagator.Ops.LeftDeleteNullPreserveExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.LeftInsertNullModifiedExtended, Propagator.JoinPropagator.Ops.Nothing);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.LeftUnknownNullModifiedExtended, Propagator.JoinPropagator.Ops.RightDeleteUnknownExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.RightInsert, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.RightInsertUnknownExtended, Propagator.JoinPropagator.Ops.LeftUnknownNullModifiedExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightInsert | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightInsert, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.RightInsert | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported);
			}

			// Token: 0x060042EB RID: 17131 RVA: 0x000F3815 File Offset: 0x000F1A15
			private static void InitializeRule(Propagator.JoinPropagator.Ops input, Propagator.JoinPropagator.Ops joinInsert, Propagator.JoinPropagator.Ops joinDelete, Propagator.JoinPropagator.Ops lojInsert, Propagator.JoinPropagator.Ops lojDelete)
			{
				Propagator.JoinPropagator.s_innerJoinInsertRules.Add(input, joinInsert);
				Propagator.JoinPropagator.s_innerJoinDeleteRules.Add(input, joinDelete);
				Propagator.JoinPropagator.s_leftOuterJoinInsertRules.Add(input, lojInsert);
				Propagator.JoinPropagator.s_leftOuterJoinDeleteRules.Add(input, lojDelete);
			}

			// Token: 0x060042EC RID: 17132 RVA: 0x000F3848 File Offset: 0x000F1A48
			internal ChangeNode Propagate()
			{
				ChangeNode changeNode = Propagator.BuildChangeNode(this.m_joinExpression);
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary = this.ProcessKeys(this.m_left.Deleted, this.m_leftKeySelectors);
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary2 = this.ProcessKeys(this.m_left.Inserted, this.m_leftKeySelectors);
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary3 = this.ProcessKeys(this.m_right.Deleted, this.m_rightKeySelectors);
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary4 = this.ProcessKeys(this.m_right.Inserted, this.m_rightKeySelectors);
				IEnumerable<CompositeKey> enumerable = dictionary.Keys.Concat(dictionary2.Keys).Concat(dictionary3.Keys).Concat(dictionary4.Keys).Distinct(this.m_parent.UpdateTranslator.KeyComparer);
				foreach (CompositeKey key in enumerable)
				{
					this.Propagate(key, changeNode, dictionary, dictionary2, dictionary3, dictionary4);
				}
				changeNode.Placeholder = this.CreateResultTuple(Tuple.Create<CompositeKey, PropagatorResult>(null, this.m_left.Placeholder), Tuple.Create<CompositeKey, PropagatorResult>(null, this.m_right.Placeholder), changeNode);
				return changeNode;
			}

			// Token: 0x060042ED RID: 17133 RVA: 0x000F397C File Offset: 0x000F1B7C
			private void Propagate(CompositeKey key, ChangeNode result, Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> leftDeletes, Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> leftInserts, Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> rightDeletes, Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> rightInserts)
			{
				Tuple<CompositeKey, PropagatorResult> tuple = null;
				Tuple<CompositeKey, PropagatorResult> tuple2 = null;
				Tuple<CompositeKey, PropagatorResult> tuple3 = null;
				Tuple<CompositeKey, PropagatorResult> tuple4 = null;
				Propagator.JoinPropagator.Ops ops = Propagator.JoinPropagator.Ops.Nothing;
				if (leftInserts.TryGetValue(key, out tuple))
				{
					ops |= Propagator.JoinPropagator.Ops.LeftInsert;
				}
				if (leftDeletes.TryGetValue(key, out tuple2))
				{
					ops |= Propagator.JoinPropagator.Ops.LeftDelete;
				}
				if (rightInserts.TryGetValue(key, out tuple3))
				{
					ops |= Propagator.JoinPropagator.Ops.RightInsert;
				}
				if (rightDeletes.TryGetValue(key, out tuple4))
				{
					ops |= Propagator.JoinPropagator.Ops.RightDelete;
				}
				Propagator.JoinPropagator.Ops ops2 = this.m_insertRules[ops];
				Propagator.JoinPropagator.Ops ops3 = this.m_deleteRules[ops];
				if (Propagator.JoinPropagator.Ops.Unsupported == ops2 || Propagator.JoinPropagator.Ops.Unsupported == ops3)
				{
					List<IEntityStateEntry> stateEntries = new List<IEntityStateEntry>();
					Action<Tuple<CompositeKey, PropagatorResult>> action = delegate(Tuple<CompositeKey, PropagatorResult> r)
					{
						if (r != null)
						{
							stateEntries.AddRange(SourceInterpreter.GetAllStateEntries(r.Item2, this.m_parent.m_updateTranslator, this.m_parent.m_table));
						}
					};
					action(tuple);
					action(tuple2);
					action(tuple3);
					action(tuple4);
					throw EntityUtil.Update(Strings.Update_InvalidChanges, null, stateEntries);
				}
				if ((Propagator.JoinPropagator.Ops.LeftUnknown & ops2) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple = Tuple.Create<CompositeKey, PropagatorResult>(key, this.LeftPlaceholder(key, Propagator.JoinPropagator.PopulateMode.Unknown));
				}
				if ((Propagator.JoinPropagator.Ops.LeftUnknown & ops3) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple2 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.LeftPlaceholder(key, Propagator.JoinPropagator.PopulateMode.Unknown));
				}
				if ((Propagator.JoinPropagator.Ops.RightNullModified & ops2) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple3 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.NullModified));
				}
				else if ((Propagator.JoinPropagator.Ops.RightNullPreserve & ops2) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple3 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.NullPreserve));
				}
				else if ((Propagator.JoinPropagator.Ops.RightUnknown & ops2) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple3 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.Unknown));
				}
				if ((Propagator.JoinPropagator.Ops.RightNullModified & ops3) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple4 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.NullModified));
				}
				else if ((Propagator.JoinPropagator.Ops.RightNullPreserve & ops3) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple4 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.NullPreserve));
				}
				else if ((Propagator.JoinPropagator.Ops.RightUnknown & ops3) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple4 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.Unknown));
				}
				if (tuple != null && tuple3 != null)
				{
					result.Inserted.Add(this.CreateResultTuple(tuple, tuple3, result));
				}
				if (tuple2 != null && tuple4 != null)
				{
					result.Deleted.Add(this.CreateResultTuple(tuple2, tuple4, result));
				}
			}

			// Token: 0x060042EE RID: 17134 RVA: 0x000F3B68 File Offset: 0x000F1D68
			private PropagatorResult CreateResultTuple(Tuple<CompositeKey, PropagatorResult> left, Tuple<CompositeKey, PropagatorResult> right, ChangeNode result)
			{
				CompositeKey item = left.Item1;
				CompositeKey item2 = right.Item1;
				Dictionary<PropagatorResult, PropagatorResult> map = null;
				if (item != null && item2 != null && item != item2)
				{
					CompositeKey compositeKey = item.Merge(this.m_parent.m_updateTranslator.KeyManager, item2);
					map = new Dictionary<PropagatorResult, PropagatorResult>();
					for (int i = 0; i < item.KeyComponents.Length; i++)
					{
						map[item.KeyComponents[i]] = compositeKey.KeyComponents[i];
						map[item2.KeyComponents[i]] = compositeKey.KeyComponents[i];
					}
				}
				PropagatorResult propagatorResult = PropagatorResult.CreateStructuralValue(new PropagatorResult[]
				{
					left.Item2,
					right.Item2
				}, (StructuralType)result.ElementType.EdmType, false);
				if (map != null)
				{
					PropagatorResult replacement;
					propagatorResult = propagatorResult.Replace(delegate(PropagatorResult original)
					{
						if (!map.TryGetValue(original, out replacement))
						{
							return original;
						}
						return replacement;
					});
				}
				return propagatorResult;
			}

			// Token: 0x060042EF RID: 17135 RVA: 0x000F3C62 File Offset: 0x000F1E62
			private PropagatorResult LeftPlaceholder(CompositeKey key, Propagator.JoinPropagator.PopulateMode mode)
			{
				return Propagator.JoinPropagator.PlaceholderPopulator.Populate(this.m_left.Placeholder, key, this.m_leftPlaceholderKey, mode, this.m_parent.UpdateTranslator);
			}

			// Token: 0x060042F0 RID: 17136 RVA: 0x000F3C87 File Offset: 0x000F1E87
			private PropagatorResult RightPlaceholder(CompositeKey key, Propagator.JoinPropagator.PopulateMode mode)
			{
				return Propagator.JoinPropagator.PlaceholderPopulator.Populate(this.m_right.Placeholder, key, this.m_rightPlaceholderKey, mode, this.m_parent.UpdateTranslator);
			}

			// Token: 0x060042F1 RID: 17137 RVA: 0x000F3CAC File Offset: 0x000F1EAC
			private Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> ProcessKeys(IEnumerable<PropagatorResult> instances, ReadOnlyCollection<DbExpression> keySelectors)
			{
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary = new Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>>(this.m_parent.UpdateTranslator.KeyComparer);
				foreach (PropagatorResult propagatorResult in instances)
				{
					CompositeKey compositeKey = Propagator.JoinPropagator.ExtractKey(propagatorResult, keySelectors, this.m_parent);
					dictionary[compositeKey] = Tuple.Create<CompositeKey, PropagatorResult>(compositeKey, propagatorResult);
				}
				return dictionary;
			}

			// Token: 0x060042F2 RID: 17138 RVA: 0x000F3D20 File Offset: 0x000F1F20
			private static CompositeKey ExtractKey(PropagatorResult change, ReadOnlyCollection<DbExpression> keySelectors, Propagator parent)
			{
				PropagatorResult[] array = new PropagatorResult[keySelectors.Count];
				for (int i = 0; i < keySelectors.Count; i++)
				{
					PropagatorResult propagatorResult = Propagator.Evaluator.Evaluate(keySelectors[i], change, parent);
					array[i] = propagatorResult;
				}
				return new CompositeKey(array);
			}

			// Token: 0x04001E57 RID: 7767
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> s_innerJoinInsertRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04001E58 RID: 7768
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> s_innerJoinDeleteRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04001E59 RID: 7769
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> s_leftOuterJoinInsertRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04001E5A RID: 7770
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> s_leftOuterJoinDeleteRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04001E5B RID: 7771
			private readonly DbJoinExpression m_joinExpression;

			// Token: 0x04001E5C RID: 7772
			private readonly Propagator m_parent;

			// Token: 0x04001E5D RID: 7773
			private readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> m_insertRules;

			// Token: 0x04001E5E RID: 7774
			private readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> m_deleteRules;

			// Token: 0x04001E5F RID: 7775
			private readonly ReadOnlyCollection<DbExpression> m_leftKeySelectors;

			// Token: 0x04001E60 RID: 7776
			private readonly ReadOnlyCollection<DbExpression> m_rightKeySelectors;

			// Token: 0x04001E61 RID: 7777
			private readonly ChangeNode m_left;

			// Token: 0x04001E62 RID: 7778
			private readonly ChangeNode m_right;

			// Token: 0x04001E63 RID: 7779
			private readonly CompositeKey m_leftPlaceholderKey;

			// Token: 0x04001E64 RID: 7780
			private readonly CompositeKey m_rightPlaceholderKey;

			// Token: 0x02000779 RID: 1913
			[Flags]
			private enum Ops : uint
			{
				// Token: 0x04002183 RID: 8579
				Nothing = 0U,
				// Token: 0x04002184 RID: 8580
				LeftInsert = 1U,
				// Token: 0x04002185 RID: 8581
				LeftDelete = 2U,
				// Token: 0x04002186 RID: 8582
				RightInsert = 4U,
				// Token: 0x04002187 RID: 8583
				RightDelete = 8U,
				// Token: 0x04002188 RID: 8584
				LeftUnknown = 32U,
				// Token: 0x04002189 RID: 8585
				RightNullModified = 128U,
				// Token: 0x0400218A RID: 8586
				RightNullPreserve = 256U,
				// Token: 0x0400218B RID: 8587
				RightUnknown = 512U,
				// Token: 0x0400218C RID: 8588
				LeftUpdate = 3U,
				// Token: 0x0400218D RID: 8589
				RightUpdate = 12U,
				// Token: 0x0400218E RID: 8590
				Unsupported = 4096U,
				// Token: 0x0400218F RID: 8591
				LeftInsertJoinRightInsert = 5U,
				// Token: 0x04002190 RID: 8592
				LeftDeleteJoinRightDelete = 10U,
				// Token: 0x04002191 RID: 8593
				LeftInsertNullModifiedExtended = 129U,
				// Token: 0x04002192 RID: 8594
				LeftInsertNullPreserveExtended = 257U,
				// Token: 0x04002193 RID: 8595
				LeftInsertUnknownExtended = 513U,
				// Token: 0x04002194 RID: 8596
				LeftDeleteNullModifiedExtended = 130U,
				// Token: 0x04002195 RID: 8597
				LeftDeleteNullPreserveExtended = 258U,
				// Token: 0x04002196 RID: 8598
				LeftDeleteUnknownExtended = 514U,
				// Token: 0x04002197 RID: 8599
				LeftUnknownNullModifiedExtended = 160U,
				// Token: 0x04002198 RID: 8600
				LeftUnknownNullPreserveExtended = 288U,
				// Token: 0x04002199 RID: 8601
				RightInsertUnknownExtended = 36U,
				// Token: 0x0400219A RID: 8602
				RightDeleteUnknownExtended = 40U
			}

			// Token: 0x0200077A RID: 1914
			private class JoinConditionVisitor : UpdateExpressionVisitor<object>
			{
				// Token: 0x060048B2 RID: 18610 RVA: 0x00105A33 File Offset: 0x00103C33
				private JoinConditionVisitor()
				{
					this.m_leftKeySelectors = new List<DbExpression>();
					this.m_rightKeySelectors = new List<DbExpression>();
				}

				// Token: 0x17000C02 RID: 3074
				// (get) Token: 0x060048B3 RID: 18611 RVA: 0x00105A51 File Offset: 0x00103C51
				protected override string VisitorName
				{
					get
					{
						return Propagator.JoinPropagator.JoinConditionVisitor.s_visitorName;
					}
				}

				// Token: 0x060048B4 RID: 18612 RVA: 0x00105A58 File Offset: 0x00103C58
				internal static void GetKeySelectors(DbExpression joinCondition, out ReadOnlyCollection<DbExpression> leftKeySelectors, out ReadOnlyCollection<DbExpression> rightKeySelectors)
				{
					EntityUtil.CheckArgumentNull<DbExpression>(joinCondition, "joinCondition");
					Propagator.JoinPropagator.JoinConditionVisitor joinConditionVisitor = new Propagator.JoinPropagator.JoinConditionVisitor();
					joinCondition.Accept<object>(joinConditionVisitor);
					leftKeySelectors = joinConditionVisitor.m_leftKeySelectors.AsReadOnly();
					rightKeySelectors = joinConditionVisitor.m_rightKeySelectors.AsReadOnly();
				}

				// Token: 0x060048B5 RID: 18613 RVA: 0x00105A99 File Offset: 0x00103C99
				public override object Visit(DbAndExpression node)
				{
					EntityUtil.CheckArgumentNull<DbAndExpression>(node, "node");
					this.Visit(node.Left);
					this.Visit(node.Right);
					return null;
				}

				// Token: 0x060048B6 RID: 18614 RVA: 0x00105AC4 File Offset: 0x00103CC4
				public override object Visit(DbComparisonExpression node)
				{
					EntityUtil.CheckArgumentNull<DbComparisonExpression>(node, "node");
					if (DbExpressionKind.Equals == node.ExpressionKind)
					{
						this.m_leftKeySelectors.Add(node.Left);
						this.m_rightKeySelectors.Add(node.Right);
						return null;
					}
					throw base.ConstructNotSupportedException(node);
				}

				// Token: 0x0400219B RID: 8603
				private readonly List<DbExpression> m_leftKeySelectors;

				// Token: 0x0400219C RID: 8604
				private readonly List<DbExpression> m_rightKeySelectors;

				// Token: 0x0400219D RID: 8605
				private static readonly string s_visitorName = typeof(Propagator.JoinPropagator.JoinConditionVisitor).FullName;
			}

			// Token: 0x0200077B RID: 1915
			private enum PopulateMode
			{
				// Token: 0x0400219F RID: 8607
				NullModified,
				// Token: 0x040021A0 RID: 8608
				NullPreserve,
				// Token: 0x040021A1 RID: 8609
				Unknown
			}

			// Token: 0x0200077C RID: 1916
			private static class PlaceholderPopulator
			{
				// Token: 0x060048B8 RID: 18616 RVA: 0x00105B28 File Offset: 0x00103D28
				internal static PropagatorResult Populate(PropagatorResult placeholder, CompositeKey key, CompositeKey placeholderKey, Propagator.JoinPropagator.PopulateMode mode, UpdateTranslator translator)
				{
					EntityUtil.CheckArgumentNull<PropagatorResult>(placeholder, "placeholder");
					EntityUtil.CheckArgumentNull<CompositeKey>(key, "key");
					EntityUtil.CheckArgumentNull<CompositeKey>(placeholderKey, "placeholderKey");
					EntityUtil.CheckArgumentNull<UpdateTranslator>(translator, "translator");
					bool isNull = mode == Propagator.JoinPropagator.PopulateMode.NullModified || mode == Propagator.JoinPropagator.PopulateMode.NullPreserve;
					bool flag = mode == Propagator.JoinPropagator.PopulateMode.NullPreserve || mode == Propagator.JoinPropagator.PopulateMode.Unknown;
					PropagatorFlags flags = PropagatorFlags.NoFlags;
					if (!isNull)
					{
						flags |= PropagatorFlags.Unknown;
					}
					if (flag)
					{
						flags |= PropagatorFlags.Preserve;
					}
					return placeholder.Replace(delegate(PropagatorResult node)
					{
						int num = -1;
						for (int i = 0; i < placeholderKey.KeyComponents.Length; i++)
						{
							if (placeholderKey.KeyComponents[i] == node)
							{
								num = i;
								break;
							}
						}
						if (num != -1)
						{
							return key.KeyComponents[num];
						}
						object value = isNull ? null : node.GetSimpleValue();
						return PropagatorResult.CreateSimpleValue(flags, value);
					});
				}
			}
		}
	}
}
