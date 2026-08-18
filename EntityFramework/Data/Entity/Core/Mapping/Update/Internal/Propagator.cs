using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003FE RID: 1022
	internal class Propagator : UpdateExpressionVisitor<ChangeNode>
	{
		// Token: 0x060025BA RID: 9658 RVA: 0x000B38B0 File Offset: 0x000B1AB0
		private Propagator(UpdateTranslator parent, EntitySet table)
		{
			this.m_updateTranslator = parent;
			this.m_table = table;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000B38C6 File Offset: 0x000B1AC6
		internal UpdateTranslator UpdateTranslator
		{
			get
			{
				return this.m_updateTranslator;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000B38CE File Offset: 0x000B1ACE
		protected override string VisitorName
		{
			get
			{
				return Propagator._visitorName;
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000B38D8 File Offset: 0x000B1AD8
		internal static ChangeNode Propagate(UpdateTranslator parent, EntitySet table, DbQueryCommandTree umView)
		{
			DbExpressionVisitor<ChangeNode> visitor = new Propagator(parent, table);
			return umView.Query.Accept<ChangeNode>(visitor);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000B38FC File Offset: 0x000B1AFC
		private static ChangeNode BuildChangeNode(DbExpression node)
		{
			TypeUsage resultType = node.ResultType;
			TypeUsage elementType = MetadataHelper.GetElementType(resultType);
			return new ChangeNode(elementType);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000B391D File Offset: 0x000B1B1D
		public override ChangeNode Visit(DbCrossJoinExpression node)
		{
			Check.NotNull<DbCrossJoinExpression>(node, "node");
			throw new NotSupportedException(Strings.Update_UnsupportedJoinType(node.ExpressionKind));
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000B3940 File Offset: 0x000B1B40
		public override ChangeNode Visit(DbJoinExpression node)
		{
			Check.NotNull<DbJoinExpression>(node, "node");
			if (DbExpressionKind.InnerJoin != node.ExpressionKind && DbExpressionKind.LeftOuterJoin != node.ExpressionKind)
			{
				throw new NotSupportedException(Strings.Update_UnsupportedJoinType(node.ExpressionKind));
			}
			DbExpression expression = node.Left.Expression;
			DbExpression expression2 = node.Right.Expression;
			ChangeNode left = this.Visit(expression);
			ChangeNode right = this.Visit(expression2);
			Propagator.JoinPropagator joinPropagator = new Propagator.JoinPropagator(left, right, node, this);
			return joinPropagator.Propagate();
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000B39C4 File Offset: 0x000B1BC4
		public override ChangeNode Visit(DbUnionAllExpression node)
		{
			Check.NotNull<DbUnionAllExpression>(node, "node");
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

		// Token: 0x060025C2 RID: 9666 RVA: 0x000B3A50 File Offset: 0x000B1C50
		public override ChangeNode Visit(DbProjectExpression node)
		{
			Check.NotNull<DbProjectExpression>(node, "node");
			ChangeNode changeNode = Propagator.BuildChangeNode(node);
			ChangeNode changeNode2 = this.Visit(node.Input.Expression);
			foreach (PropagatorResult row in changeNode2.Inserted)
			{
				changeNode.Inserted.Add(Propagator.Project(node, row, changeNode.ElementType));
			}
			foreach (PropagatorResult row2 in changeNode2.Deleted)
			{
				changeNode.Deleted.Add(Propagator.Project(node, row2, changeNode.ElementType));
			}
			changeNode.Placeholder = Propagator.Project(node, changeNode2.Placeholder, changeNode.ElementType);
			return changeNode;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000B3B48 File Offset: 0x000B1D48
		private static PropagatorResult Project(DbProjectExpression node, PropagatorResult row, TypeUsage resultType)
		{
			DbNewInstanceExpression dbNewInstanceExpression = node.Projection as DbNewInstanceExpression;
			if (dbNewInstanceExpression == null)
			{
				throw new NotSupportedException(Strings.Update_UnsupportedProjection(node.Projection.ExpressionKind));
			}
			PropagatorResult[] array = new PropagatorResult[dbNewInstanceExpression.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Propagator.Evaluator.Evaluate(dbNewInstanceExpression.Arguments[i], row);
			}
			return PropagatorResult.CreateStructuralValue(array, (StructuralType)resultType.EdmType, false);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000B3BC8 File Offset: 0x000B1DC8
		public override ChangeNode Visit(DbFilterExpression node)
		{
			Check.NotNull<DbFilterExpression>(node, "node");
			ChangeNode changeNode = Propagator.BuildChangeNode(node);
			ChangeNode changeNode2 = this.Visit(node.Input.Expression);
			changeNode.Inserted.AddRange(Propagator.Evaluator.Filter(node.Predicate, changeNode2.Inserted));
			changeNode.Deleted.AddRange(Propagator.Evaluator.Filter(node.Predicate, changeNode2.Deleted));
			changeNode.Placeholder = changeNode2.Placeholder;
			return changeNode;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000B3C40 File Offset: 0x000B1E40
		public override ChangeNode Visit(DbScanExpression node)
		{
			Check.NotNull<DbScanExpression>(node, "node");
			EntitySetBase target = node.Target;
			ChangeNode extentModifications = this.UpdateTranslator.GetExtentModifications(target);
			if (extentModifications.Placeholder == null)
			{
				extentModifications.Placeholder = Propagator.ExtentPlaceholderCreator.CreatePlaceholder(target);
			}
			return extentModifications;
		}

		// Token: 0x04000E07 RID: 3591
		private readonly UpdateTranslator m_updateTranslator;

		// Token: 0x04000E08 RID: 3592
		private readonly EntitySet m_table;

		// Token: 0x04000E09 RID: 3593
		private static readonly string _visitorName = typeof(Propagator).FullName;

		// Token: 0x020003FF RID: 1023
		private class Evaluator : UpdateExpressionVisitor<PropagatorResult>
		{
			// Token: 0x060025C7 RID: 9671 RVA: 0x000B3C98 File Offset: 0x000B1E98
			private Evaluator(PropagatorResult row)
			{
				this.m_row = row;
			}

			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000B3CA7 File Offset: 0x000B1EA7
			protected override string VisitorName
			{
				get
				{
					return Propagator.Evaluator._visitorName;
				}
			}

			// Token: 0x060025C9 RID: 9673 RVA: 0x000B3E54 File Offset: 0x000B2054
			internal static IEnumerable<PropagatorResult> Filter(DbExpression predicate, IEnumerable<PropagatorResult> rows)
			{
				foreach (PropagatorResult row in rows)
				{
					if (Propagator.Evaluator.EvaluatePredicate(predicate, row))
					{
						yield return row;
					}
				}
				yield break;
			}

			// Token: 0x060025CA RID: 9674 RVA: 0x000B3E78 File Offset: 0x000B2078
			internal static bool EvaluatePredicate(DbExpression predicate, PropagatorResult row)
			{
				return Propagator.Evaluator.ConvertResultToBool(predicate.Accept<PropagatorResult>(new Propagator.Evaluator(row))) ?? false;
			}

			// Token: 0x060025CB RID: 9675 RVA: 0x000B3EB0 File Offset: 0x000B20B0
			internal static PropagatorResult Evaluate(DbExpression node, PropagatorResult row)
			{
				DbExpressionVisitor<PropagatorResult> visitor = new Propagator.Evaluator(row);
				return node.Accept<PropagatorResult>(visitor);
			}

			// Token: 0x060025CC RID: 9676 RVA: 0x000B3ECC File Offset: 0x000B20CC
			private static bool? ConvertResultToBool(PropagatorResult result)
			{
				if (result.IsNull)
				{
					return null;
				}
				return new bool?((bool)result.GetSimpleValue());
			}

			// Token: 0x060025CD RID: 9677 RVA: 0x000B3EFC File Offset: 0x000B20FC
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

			// Token: 0x060025CE RID: 9678 RVA: 0x000B3F34 File Offset: 0x000B2134
			public override PropagatorResult Visit(DbIsOfExpression predicate)
			{
				Check.NotNull<DbIsOfExpression>(predicate, "predicate");
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

			// Token: 0x060025CF RID: 9679 RVA: 0x000B3FA8 File Offset: 0x000B21A8
			public override PropagatorResult Visit(DbComparisonExpression predicate)
			{
				Check.NotNull<DbComparisonExpression>(predicate, "predicate");
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

			// Token: 0x060025D0 RID: 9680 RVA: 0x000B4044 File Offset: 0x000B2244
			public override PropagatorResult Visit(DbAndExpression predicate)
			{
				Check.NotNull<DbAndExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Left);
				PropagatorResult propagatorResult2 = this.Visit(predicate.Right);
				bool? left = Propagator.Evaluator.ConvertResultToBool(propagatorResult);
				bool? right = Propagator.Evaluator.ConvertResultToBool(propagatorResult2);
				if ((left != null && !left.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult)) || (right != null && !right.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult2)))
				{
					return Propagator.Evaluator.CreatePerservedAndKnownResult(false);
				}
				bool? booleanValue = left.And(right);
				return Propagator.Evaluator.ConvertBoolToResult(booleanValue, new PropagatorResult[]
				{
					propagatorResult,
					propagatorResult2
				});
			}

			// Token: 0x060025D1 RID: 9681 RVA: 0x000B40EC File Offset: 0x000B22EC
			public override PropagatorResult Visit(DbOrExpression predicate)
			{
				Check.NotNull<DbOrExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Left);
				PropagatorResult propagatorResult2 = this.Visit(predicate.Right);
				bool? left = Propagator.Evaluator.ConvertResultToBool(propagatorResult);
				bool? right = Propagator.Evaluator.ConvertResultToBool(propagatorResult2);
				if ((left != null && left.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult)) || (right != null && right.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult2)))
				{
					return Propagator.Evaluator.CreatePerservedAndKnownResult(true);
				}
				bool? booleanValue = left.Or(right);
				return Propagator.Evaluator.ConvertBoolToResult(booleanValue, new PropagatorResult[]
				{
					propagatorResult,
					propagatorResult2
				});
			}

			// Token: 0x060025D2 RID: 9682 RVA: 0x000B4191 File Offset: 0x000B2391
			private static PropagatorResult CreatePerservedAndKnownResult(object value)
			{
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, value);
			}

			// Token: 0x060025D3 RID: 9683 RVA: 0x000B419A File Offset: 0x000B239A
			private static bool PreservedAndKnown(PropagatorResult result)
			{
				return 1 == (byte)(result.PropagatorFlags & (PropagatorFlags.Preserve | PropagatorFlags.Unknown));
			}

			// Token: 0x060025D4 RID: 9684 RVA: 0x000B41AC File Offset: 0x000B23AC
			public override PropagatorResult Visit(DbNotExpression predicate)
			{
				Check.NotNull<DbNotExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Argument);
				bool? operand = Propagator.Evaluator.ConvertResultToBool(propagatorResult);
				bool? booleanValue = operand.Not();
				return Propagator.Evaluator.ConvertBoolToResult(booleanValue, new PropagatorResult[]
				{
					propagatorResult
				});
			}

			// Token: 0x060025D5 RID: 9685 RVA: 0x000B41F4 File Offset: 0x000B23F4
			public override PropagatorResult Visit(DbCaseExpression node)
			{
				Check.NotNull<DbCaseExpression>(node, "node");
				int num = -1;
				int num2 = 0;
				List<PropagatorResult> list = new List<PropagatorResult>();
				foreach (DbExpression expression in node.When)
				{
					PropagatorResult propagatorResult = this.Visit(expression);
					list.Add(propagatorResult);
					bool flag = Propagator.Evaluator.ConvertResultToBool(propagatorResult) ?? false;
					if (flag)
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

			// Token: 0x060025D6 RID: 9686 RVA: 0x000B42D8 File Offset: 0x000B24D8
			public override PropagatorResult Visit(DbVariableReferenceExpression node)
			{
				Check.NotNull<DbVariableReferenceExpression>(node, "node");
				return this.m_row;
			}

			// Token: 0x060025D7 RID: 9687 RVA: 0x000B42EC File Offset: 0x000B24EC
			public override PropagatorResult Visit(DbPropertyExpression node)
			{
				Check.NotNull<DbPropertyExpression>(node, "node");
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

			// Token: 0x060025D8 RID: 9688 RVA: 0x000B4338 File Offset: 0x000B2538
			public override PropagatorResult Visit(DbConstantExpression node)
			{
				Check.NotNull<DbConstantExpression>(node, "node");
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, node.Value);
			}

			// Token: 0x060025D9 RID: 9689 RVA: 0x000B4360 File Offset: 0x000B2560
			public override PropagatorResult Visit(DbRefKeyExpression node)
			{
				Check.NotNull<DbRefKeyExpression>(node, "node");
				return this.Visit(node.Argument);
			}

			// Token: 0x060025DA RID: 9690 RVA: 0x000B4388 File Offset: 0x000B2588
			public override PropagatorResult Visit(DbNullExpression node)
			{
				Check.NotNull<DbNullExpression>(node, "node");
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, null);
			}

			// Token: 0x060025DB RID: 9691 RVA: 0x000B43AC File Offset: 0x000B25AC
			public override PropagatorResult Visit(DbTreatExpression node)
			{
				Check.NotNull<DbTreatExpression>(node, "node");
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				TypeUsage resultType = node.ResultType;
				if (MetadataHelper.IsSuperTypeOf(resultType.EdmType, propagatorResult.StructuralType))
				{
					return propagatorResult;
				}
				return PropagatorResult.CreateSimpleValue(propagatorResult.PropagatorFlags, null);
			}

			// Token: 0x060025DC RID: 9692 RVA: 0x000B43FC File Offset: 0x000B25FC
			public override PropagatorResult Visit(DbCastExpression node)
			{
				Check.NotNull<DbCastExpression>(node, "node");
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				TypeUsage resultType = node.ResultType;
				if (!propagatorResult.IsSimple || BuiltInTypeKind.PrimitiveType != resultType.EdmType.BuiltInTypeKind)
				{
					throw new NotSupportedException(Strings.Update_UnsupportedCastArgument(resultType.EdmType.Name));
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

			// Token: 0x060025DD RID: 9693 RVA: 0x000B449C File Offset: 0x000B269C
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

			// Token: 0x060025DE RID: 9694 RVA: 0x000B450C File Offset: 0x000B270C
			public override PropagatorResult Visit(DbIsNullExpression node)
			{
				Check.NotNull<DbIsNullExpression>(node, "node");
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				bool isNull = propagatorResult.IsNull;
				return Propagator.Evaluator.ConvertBoolToResult(new bool?(isNull), new PropagatorResult[]
				{
					propagatorResult
				});
			}

			// Token: 0x060025DF RID: 9695 RVA: 0x000B4550 File Offset: 0x000B2750
			private static PropagatorFlags PropagateUnknownAndPreserveFlags(PropagatorResult result, IEnumerable<PropagatorResult> inputs)
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = true;
				foreach (PropagatorResult propagatorResult in inputs)
				{
					flag3 = false;
					PropagatorFlags propagatorFlags = propagatorResult.PropagatorFlags;
					if ((byte)(PropagatorFlags.Unknown & propagatorFlags) != 0)
					{
						flag = true;
					}
					if ((byte)(PropagatorFlags.Preserve & propagatorFlags) == 0)
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

			// Token: 0x04000E0A RID: 3594
			private readonly PropagatorResult m_row;

			// Token: 0x04000E0B RID: 3595
			private static readonly string _visitorName = typeof(Propagator.Evaluator).FullName;
		}

		// Token: 0x02000400 RID: 1024
		internal class ExtentPlaceholderCreator
		{
			// Token: 0x060025E1 RID: 9697 RVA: 0x000B4614 File Offset: 0x000B2814
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
				return dictionary;
			}

			// Token: 0x060025E2 RID: 9698 RVA: 0x000B4728 File Offset: 0x000B2928
			private static Dictionary<PrimitiveTypeKind, object> InitializeSpatialTypeDefaultMap()
			{
				Dictionary<PrimitiveTypeKind, object> dictionary = new Dictionary<PrimitiveTypeKind, object>(EqualityComparer<PrimitiveTypeKind>.Default);
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

			// Token: 0x060025E3 RID: 9699 RVA: 0x000B4864 File Offset: 0x000B2A64
			private static bool TryGetDefaultValue(PrimitiveType primitiveType, out object defaultValue)
			{
				PrimitiveTypeKind primitiveTypeKind = primitiveType.PrimitiveTypeKind;
				if (!Helper.IsSpatialType(primitiveType))
				{
					return Propagator.ExtentPlaceholderCreator._typeDefaultMap.TryGetValue(primitiveTypeKind, out defaultValue);
				}
				return Propagator.ExtentPlaceholderCreator._spatialTypeDefaultMap.Value.TryGetValue(primitiveTypeKind, out defaultValue);
			}

			// Token: 0x060025E4 RID: 9700 RVA: 0x000B48A0 File Offset: 0x000B2AA0
			internal static PropagatorResult CreatePlaceholder(EntitySetBase extent)
			{
				Propagator.ExtentPlaceholderCreator extentPlaceholderCreator = new Propagator.ExtentPlaceholderCreator();
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
				throw new NotSupportedException(Strings.Update_UnsupportedExtentType(extent.Name, extent.GetType().Name));
			}

			// Token: 0x060025E5 RID: 9701 RVA: 0x000B48F4 File Offset: 0x000B2AF4
			private PropagatorResult CreateEntitySetPlaceholder(EntitySet entitySet)
			{
				ReadOnlyMetadataCollection<EdmProperty> properties = entitySet.ElementType.Properties;
				PropagatorResult[] array = new PropagatorResult[properties.Count];
				for (int i = 0; i < properties.Count; i++)
				{
					PropagatorResult propagatorResult = this.CreateMemberPlaceholder(properties[i]);
					array[i] = propagatorResult;
				}
				return PropagatorResult.CreateStructuralValue(array, entitySet.ElementType, false);
			}

			// Token: 0x060025E6 RID: 9702 RVA: 0x000B4950 File Offset: 0x000B2B50
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
					RowType keyRowType = entityType.GetKeyRowType();
					PropagatorResult propagatorResult2 = PropagatorResult.CreateStructuralValue(array2, keyRowType, false);
					array[i] = propagatorResult2;
				}
				return PropagatorResult.CreateStructuralValue(array, associationSet.ElementType, false);
			}

			// Token: 0x060025E7 RID: 9703 RVA: 0x000B4A2A File Offset: 0x000B2C2A
			private PropagatorResult CreateMemberPlaceholder(EdmMember member)
			{
				return this.Visit(member);
			}

			// Token: 0x060025E8 RID: 9704 RVA: 0x000B4A34 File Offset: 0x000B2C34
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

			// Token: 0x060025E9 RID: 9705 RVA: 0x000B4ABC File Offset: 0x000B2CBC
			internal static void GetPropagatorResultForPrimitiveType(PrimitiveType primitiveType, out PropagatorResult result)
			{
				object value;
				if (!Propagator.ExtentPlaceholderCreator.TryGetDefaultValue(primitiveType, out value))
				{
					value = 0;
				}
				result = PropagatorResult.CreateSimpleValue(PropagatorFlags.NoFlags, value);
			}

			// Token: 0x04000E0C RID: 3596
			private static readonly Dictionary<PrimitiveTypeKind, object> _typeDefaultMap = Propagator.ExtentPlaceholderCreator.InitializeTypeDefaultMap();

			// Token: 0x04000E0D RID: 3597
			private static readonly Lazy<Dictionary<PrimitiveTypeKind, object>> _spatialTypeDefaultMap = new Lazy<Dictionary<PrimitiveTypeKind, object>>(new Func<Dictionary<PrimitiveTypeKind, object>>(Propagator.ExtentPlaceholderCreator.InitializeSpatialTypeDefaultMap));
		}

		// Token: 0x02000401 RID: 1025
		private class JoinPropagator
		{
			// Token: 0x060025EC RID: 9708 RVA: 0x000B4B10 File Offset: 0x000B2D10
			internal JoinPropagator(ChangeNode left, ChangeNode right, DbJoinExpression node, Propagator parent)
			{
				this.m_left = left;
				this.m_right = right;
				this.m_joinExpression = node;
				this.m_parent = parent;
				if (DbExpressionKind.InnerJoin == this.m_joinExpression.ExpressionKind)
				{
					this.m_insertRules = Propagator.JoinPropagator._innerJoinInsertRules;
					this.m_deleteRules = Propagator.JoinPropagator._innerJoinDeleteRules;
				}
				else
				{
					this.m_insertRules = Propagator.JoinPropagator._leftOuterJoinInsertRules;
					this.m_deleteRules = Propagator.JoinPropagator._leftOuterJoinDeleteRules;
				}
				Propagator.JoinPropagator.JoinConditionVisitor.GetKeySelectors(node.JoinCondition, out this.m_leftKeySelectors, out this.m_rightKeySelectors);
				this.m_leftPlaceholderKey = Propagator.JoinPropagator.ExtractKey(this.m_left.Placeholder, this.m_leftKeySelectors);
				this.m_rightPlaceholderKey = Propagator.JoinPropagator.ExtractKey(this.m_right.Placeholder, this.m_rightKeySelectors);
			}

			// Token: 0x060025ED RID: 9709 RVA: 0x000B4BCC File Offset: 0x000B2DCC
			[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
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

			// Token: 0x060025EE RID: 9710 RVA: 0x000B4D55 File Offset: 0x000B2F55
			private static void InitializeRule(Propagator.JoinPropagator.Ops input, Propagator.JoinPropagator.Ops joinInsert, Propagator.JoinPropagator.Ops joinDelete, Propagator.JoinPropagator.Ops lojInsert, Propagator.JoinPropagator.Ops lojDelete)
			{
				Propagator.JoinPropagator._innerJoinInsertRules.Add(input, joinInsert);
				Propagator.JoinPropagator._innerJoinDeleteRules.Add(input, joinDelete);
				Propagator.JoinPropagator._leftOuterJoinInsertRules.Add(input, lojInsert);
				Propagator.JoinPropagator._leftOuterJoinDeleteRules.Add(input, lojDelete);
			}

			// Token: 0x060025EF RID: 9711 RVA: 0x000B4D88 File Offset: 0x000B2F88
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

			// Token: 0x060025F0 RID: 9712 RVA: 0x000B4F00 File Offset: 0x000B3100
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
					throw new UpdateException(Strings.Update_InvalidChanges, null, stateEntries.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
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

			// Token: 0x060025F1 RID: 9713 RVA: 0x000B5128 File Offset: 0x000B3328
			private PropagatorResult CreateResultTuple(Tuple<CompositeKey, PropagatorResult> left, Tuple<CompositeKey, PropagatorResult> right, ChangeNode result)
			{
				CompositeKey item = left.Item1;
				CompositeKey item2 = right.Item1;
				Dictionary<PropagatorResult, PropagatorResult> map = null;
				if (!object.ReferenceEquals(null, item) && !object.ReferenceEquals(null, item2) && !object.ReferenceEquals(item, item2))
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

			// Token: 0x060025F2 RID: 9714 RVA: 0x000B5246 File Offset: 0x000B3446
			private PropagatorResult LeftPlaceholder(CompositeKey key, Propagator.JoinPropagator.PopulateMode mode)
			{
				return Propagator.JoinPropagator.PlaceholderPopulator.Populate(this.m_left.Placeholder, key, this.m_leftPlaceholderKey, mode);
			}

			// Token: 0x060025F3 RID: 9715 RVA: 0x000B5260 File Offset: 0x000B3460
			private PropagatorResult RightPlaceholder(CompositeKey key, Propagator.JoinPropagator.PopulateMode mode)
			{
				return Propagator.JoinPropagator.PlaceholderPopulator.Populate(this.m_right.Placeholder, key, this.m_rightPlaceholderKey, mode);
			}

			// Token: 0x060025F4 RID: 9716 RVA: 0x000B527C File Offset: 0x000B347C
			private Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> ProcessKeys(IEnumerable<PropagatorResult> instances, ReadOnlyCollection<DbExpression> keySelectors)
			{
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary = new Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>>(this.m_parent.UpdateTranslator.KeyComparer);
				foreach (PropagatorResult propagatorResult in instances)
				{
					CompositeKey compositeKey = Propagator.JoinPropagator.ExtractKey(propagatorResult, keySelectors);
					dictionary[compositeKey] = Tuple.Create<CompositeKey, PropagatorResult>(compositeKey, propagatorResult);
				}
				return dictionary;
			}

			// Token: 0x060025F5 RID: 9717 RVA: 0x000B52EC File Offset: 0x000B34EC
			private static CompositeKey ExtractKey(PropagatorResult change, ReadOnlyCollection<DbExpression> keySelectors)
			{
				PropagatorResult[] array = new PropagatorResult[keySelectors.Count];
				for (int i = 0; i < keySelectors.Count; i++)
				{
					PropagatorResult propagatorResult = Propagator.Evaluator.Evaluate(keySelectors[i], change);
					array[i] = propagatorResult;
				}
				return new CompositeKey(array);
			}

			// Token: 0x04000E0E RID: 3598
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> _innerJoinInsertRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04000E0F RID: 3599
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> _innerJoinDeleteRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04000E10 RID: 3600
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> _leftOuterJoinInsertRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04000E11 RID: 3601
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> _leftOuterJoinDeleteRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04000E12 RID: 3602
			private readonly DbJoinExpression m_joinExpression;

			// Token: 0x04000E13 RID: 3603
			private readonly Propagator m_parent;

			// Token: 0x04000E14 RID: 3604
			private readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> m_insertRules;

			// Token: 0x04000E15 RID: 3605
			private readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> m_deleteRules;

			// Token: 0x04000E16 RID: 3606
			private readonly ReadOnlyCollection<DbExpression> m_leftKeySelectors;

			// Token: 0x04000E17 RID: 3607
			private readonly ReadOnlyCollection<DbExpression> m_rightKeySelectors;

			// Token: 0x04000E18 RID: 3608
			private readonly ChangeNode m_left;

			// Token: 0x04000E19 RID: 3609
			private readonly ChangeNode m_right;

			// Token: 0x04000E1A RID: 3610
			private readonly CompositeKey m_leftPlaceholderKey;

			// Token: 0x04000E1B RID: 3611
			private readonly CompositeKey m_rightPlaceholderKey;

			// Token: 0x02000402 RID: 1026
			[Flags]
			private enum Ops : uint
			{
				// Token: 0x04000E1D RID: 3613
				Nothing = 0U,
				// Token: 0x04000E1E RID: 3614
				LeftInsert = 1U,
				// Token: 0x04000E1F RID: 3615
				LeftDelete = 2U,
				// Token: 0x04000E20 RID: 3616
				RightInsert = 4U,
				// Token: 0x04000E21 RID: 3617
				RightDelete = 8U,
				// Token: 0x04000E22 RID: 3618
				LeftUnknown = 32U,
				// Token: 0x04000E23 RID: 3619
				RightNullModified = 128U,
				// Token: 0x04000E24 RID: 3620
				RightNullPreserve = 256U,
				// Token: 0x04000E25 RID: 3621
				RightUnknown = 512U,
				// Token: 0x04000E26 RID: 3622
				LeftUpdate = 3U,
				// Token: 0x04000E27 RID: 3623
				RightUpdate = 12U,
				// Token: 0x04000E28 RID: 3624
				Unsupported = 4096U,
				// Token: 0x04000E29 RID: 3625
				LeftInsertJoinRightInsert = 5U,
				// Token: 0x04000E2A RID: 3626
				LeftDeleteJoinRightDelete = 10U,
				// Token: 0x04000E2B RID: 3627
				LeftInsertNullModifiedExtended = 129U,
				// Token: 0x04000E2C RID: 3628
				LeftInsertNullPreserveExtended = 257U,
				// Token: 0x04000E2D RID: 3629
				LeftInsertUnknownExtended = 513U,
				// Token: 0x04000E2E RID: 3630
				LeftDeleteNullModifiedExtended = 130U,
				// Token: 0x04000E2F RID: 3631
				LeftDeleteNullPreserveExtended = 258U,
				// Token: 0x04000E30 RID: 3632
				LeftDeleteUnknownExtended = 514U,
				// Token: 0x04000E31 RID: 3633
				LeftUnknownNullModifiedExtended = 160U,
				// Token: 0x04000E32 RID: 3634
				LeftUnknownNullPreserveExtended = 288U,
				// Token: 0x04000E33 RID: 3635
				RightInsertUnknownExtended = 36U,
				// Token: 0x04000E34 RID: 3636
				RightDeleteUnknownExtended = 40U
			}

			// Token: 0x02000403 RID: 1027
			private class JoinConditionVisitor : UpdateExpressionVisitor<object>
			{
				// Token: 0x060025F6 RID: 9718 RVA: 0x000B532E File Offset: 0x000B352E
				private JoinConditionVisitor()
				{
					this.m_leftKeySelectors = new List<DbExpression>();
					this.m_rightKeySelectors = new List<DbExpression>();
				}

				// Token: 0x1700052B RID: 1323
				// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000B534C File Offset: 0x000B354C
				protected override string VisitorName
				{
					get
					{
						return Propagator.JoinPropagator.JoinConditionVisitor._visitorName;
					}
				}

				// Token: 0x060025F8 RID: 9720 RVA: 0x000B5354 File Offset: 0x000B3554
				internal static void GetKeySelectors(DbExpression joinCondition, out ReadOnlyCollection<DbExpression> leftKeySelectors, out ReadOnlyCollection<DbExpression> rightKeySelectors)
				{
					Propagator.JoinPropagator.JoinConditionVisitor joinConditionVisitor = new Propagator.JoinPropagator.JoinConditionVisitor();
					joinCondition.Accept<object>(joinConditionVisitor);
					leftKeySelectors = new ReadOnlyCollection<DbExpression>(joinConditionVisitor.m_leftKeySelectors);
					rightKeySelectors = new ReadOnlyCollection<DbExpression>(joinConditionVisitor.m_rightKeySelectors);
				}

				// Token: 0x060025F9 RID: 9721 RVA: 0x000B5389 File Offset: 0x000B3589
				public override object Visit(DbAndExpression node)
				{
					Check.NotNull<DbAndExpression>(node, "node");
					this.Visit(node.Left);
					this.Visit(node.Right);
					return null;
				}

				// Token: 0x060025FA RID: 9722 RVA: 0x000B53B4 File Offset: 0x000B35B4
				public override object Visit(DbComparisonExpression node)
				{
					Check.NotNull<DbComparisonExpression>(node, "node");
					if (DbExpressionKind.Equals == node.ExpressionKind)
					{
						this.m_leftKeySelectors.Add(node.Left);
						this.m_rightKeySelectors.Add(node.Right);
						return null;
					}
					throw base.ConstructNotSupportedException(node);
				}

				// Token: 0x04000E35 RID: 3637
				private readonly List<DbExpression> m_leftKeySelectors;

				// Token: 0x04000E36 RID: 3638
				private readonly List<DbExpression> m_rightKeySelectors;

				// Token: 0x04000E37 RID: 3639
				private static readonly string _visitorName = typeof(Propagator.JoinPropagator.JoinConditionVisitor).FullName;
			}

			// Token: 0x02000404 RID: 1028
			private enum PopulateMode
			{
				// Token: 0x04000E39 RID: 3641
				NullModified,
				// Token: 0x04000E3A RID: 3642
				NullPreserve,
				// Token: 0x04000E3B RID: 3643
				Unknown
			}

			// Token: 0x02000405 RID: 1029
			private static class PlaceholderPopulator
			{
				// Token: 0x060025FC RID: 9724 RVA: 0x000B548C File Offset: 0x000B368C
				internal static PropagatorResult Populate(PropagatorResult placeholder, CompositeKey key, CompositeKey placeholderKey, Propagator.JoinPropagator.PopulateMode mode)
				{
					Propagator.JoinPropagator.PlaceholderPopulator.<>c__DisplayClassf CS$<>8__locals1 = new Propagator.JoinPropagator.PlaceholderPopulator.<>c__DisplayClassf();
					CS$<>8__locals1.key = key;
					CS$<>8__locals1.placeholderKey = placeholderKey;
					CS$<>8__locals1.isNull = (mode == Propagator.JoinPropagator.PopulateMode.NullModified || mode == Propagator.JoinPropagator.PopulateMode.NullPreserve);
					bool flag = mode == Propagator.JoinPropagator.PopulateMode.NullPreserve || mode == Propagator.JoinPropagator.PopulateMode.Unknown;
					CS$<>8__locals1.flags = PropagatorFlags.NoFlags;
					if (!CS$<>8__locals1.isNull)
					{
						Propagator.JoinPropagator.PlaceholderPopulator.<>c__DisplayClassf CS$<>8__locals2 = CS$<>8__locals1;
						CS$<>8__locals2.flags |= PropagatorFlags.Unknown;
					}
					if (flag)
					{
						Propagator.JoinPropagator.PlaceholderPopulator.<>c__DisplayClassf CS$<>8__locals3 = CS$<>8__locals1;
						CS$<>8__locals3.flags |= PropagatorFlags.Preserve;
					}
					return placeholder.Replace(delegate(PropagatorResult node)
					{
						int num = -1;
						for (int i = 0; i < CS$<>8__locals1.placeholderKey.KeyComponents.Length; i++)
						{
							if (CS$<>8__locals1.placeholderKey.KeyComponents[i] == node)
							{
								num = i;
								break;
							}
						}
						if (num != -1)
						{
							return CS$<>8__locals1.key.KeyComponents[num];
						}
						object value = CS$<>8__locals1.isNull ? null : node.GetSimpleValue();
						return PropagatorResult.CreateSimpleValue(CS$<>8__locals1.flags, value);
					});
				}
			}
		}
	}
}
