using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003B6 RID: 950
	public sealed class FunctionImportMappingComposable : FunctionImportMapping
	{
		// Token: 0x06002283 RID: 8835 RVA: 0x000A1124 File Offset: 0x0009F324
		public FunctionImportMappingComposable(EdmFunction functionImport, EdmFunction targetFunction, FunctionImportResultMapping resultMapping, EntityContainerMapping containerMapping) : base(Check.NotNull<EdmFunction>(functionImport, "functionImport"), Check.NotNull<EdmFunction>(targetFunction, "targetFunction"))
		{
			Check.NotNull<FunctionImportResultMapping>(resultMapping, "resultMapping");
			Check.NotNull<EntityContainerMapping>(containerMapping, "containerMapping");
			if (!functionImport.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.NonComposableFunctionCannotBeMappedAsComposable("functionImport"));
			}
			if (!targetFunction.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.NonComposableFunctionCannotBeMappedAsComposable("targetFunction"));
			}
			EdmType edmType;
			if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, 0, out edmType))
			{
				throw new ArgumentException(Strings.InvalidReturnTypeForComposableFunction);
			}
			EdmFunction edmFunction = (containerMapping.StorageMappingItemCollection != null) ? containerMapping.StorageMappingItemCollection.StoreItemCollection.ConvertToCTypeFunction(targetFunction) : StoreItemCollection.ConvertFunctionSignatureToCType(targetFunction);
			RowType tvfReturnType = TypeHelpers.GetTvfReturnType(edmFunction);
			RowType tvfReturnType2 = TypeHelpers.GetTvfReturnType(targetFunction);
			if (tvfReturnType == null)
			{
				throw new ArgumentException(Strings.Mapping_FunctionImport_ResultMapping_InvalidSType(functionImport.Identity), "functionImport");
			}
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			FunctionImportMappingComposableHelper functionImportMappingComposableHelper = new FunctionImportMappingComposableHelper(containerMapping, string.Empty, list);
			FunctionImportMappingComposable functionImportMappingComposable;
			if (Helper.IsStructuralType(edmType))
			{
				functionImportMappingComposableHelper.TryCreateFunctionImportMappingComposableWithStructuralResult(functionImport, edmFunction, resultMapping.SourceList, tvfReturnType, tvfReturnType2, LineInfo.Empty, out functionImportMappingComposable);
			}
			else
			{
				functionImportMappingComposableHelper.TryCreateFunctionImportMappingComposableWithScalarResult(functionImport, edmFunction, targetFunction, edmType, tvfReturnType, LineInfo.Empty, out functionImportMappingComposable);
			}
			if (functionImportMappingComposable == null)
			{
				throw new InvalidOperationException((list.Count > 0) ? list[0].Message : string.Empty);
			}
			this._containerMapping = functionImportMappingComposable._containerMapping;
			this.m_commandParameters = functionImportMappingComposable.m_commandParameters;
			this.m_structuralTypeMappings = functionImportMappingComposable.m_structuralTypeMappings;
			this.m_targetFunctionKeys = functionImportMappingComposable.m_targetFunctionKeys;
			this._resultMapping = resultMapping;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000A12A8 File Offset: 0x0009F4A8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		internal FunctionImportMappingComposable(EdmFunction functionImport, EdmFunction targetFunction, List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> structuralTypeMappings) : base(functionImport, targetFunction)
		{
			if (!functionImport.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.NonComposableFunctionCannotBeMappedAsComposable("functionImport"));
			}
			if (!targetFunction.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.NonComposableFunctionCannotBeMappedAsComposable("targetFunction"));
			}
			EdmType type;
			if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, 0, out type))
			{
				throw new ArgumentException(Strings.InvalidReturnTypeForComposableFunction);
			}
			if (!TypeSemantics.IsScalarType(type) && (structuralTypeMappings == null || structuralTypeMappings.Count == 0))
			{
				throw new ArgumentException(Strings.StructuralTypeMappingsMustNotBeNullForFunctionImportsReturingNonScalarValues);
			}
			this.m_structuralTypeMappings = structuralTypeMappings;
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000A1340 File Offset: 0x0009F540
		internal FunctionImportMappingComposable(EdmFunction functionImport, EdmFunction targetFunction, List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> structuralTypeMappings, EdmProperty[] targetFunctionKeys, EntityContainerMapping containerMapping) : base(functionImport, targetFunction)
		{
			this._containerMapping = containerMapping;
			this.m_commandParameters = (from p in functionImport.Parameters
			select TypeHelpers.GetPrimitiveTypeUsageForScalar(p.TypeUsage).Parameter(p.Name)).ToArray<DbParameterReferenceExpression>();
			this.m_structuralTypeMappings = structuralTypeMappings;
			this.m_targetFunctionKeys = targetFunctionKeys;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x000A139F File Offset: 0x0009F59F
		public FunctionImportResultMapping ResultMapping
		{
			get
			{
				return this._resultMapping;
			}
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000A13A7 File Offset: 0x0009F5A7
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._resultMapping);
			base.SetReadOnly();
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x000A13BA File Offset: 0x0009F5BA
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		internal ReadOnlyCollection<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> StructuralTypeMappings
		{
			get
			{
				if (this.m_structuralTypeMappings != null)
				{
					return new ReadOnlyCollection<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>>(this.m_structuralTypeMappings);
				}
				return null;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x000A13D1 File Offset: 0x0009F5D1
		internal EdmProperty[] TvfKeys
		{
			get
			{
				return this.m_targetFunctionKeys;
			}
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000A13DC File Offset: 0x0009F5DC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "projectOp")]
		internal Node GetInternalTree(Command targetIqtCommand, IList<Node> targetIqtArguments)
		{
			if (this.m_internalTreeNode == null)
			{
				DiscriminatorMap discriminatorMap;
				DbQueryCommandTree ctree = this.GenerateFunctionView(out discriminatorMap);
				Command command = ITreeGenerator.Generate(ctree, discriminatorMap);
				Node root = command.Root;
				PlanCompiler.Assert(root.Op.OpType == OpType.PhysicalProject, "Expected a physical projectOp at the root of the tree - found " + root.Op.OpType);
				PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)root.Op;
				Node child = root.Child0;
				command.DisableVarVecEnumCaching();
				Node node = child;
				Var var = physicalProjectOp.Outputs[0];
				TypeUsage type = physicalProjectOp.ColumnMap.Type;
				if (!Command.EqualTypes(type, base.FunctionImport.ReturnParameter.TypeUsage))
				{
					CollectionType collectionType = (CollectionType)base.FunctionImport.ReturnParameter.TypeUsage.EdmType;
					TypeUsage typeUsage = collectionType.TypeUsage;
					Node arg = command.CreateNode(command.CreateVarRefOp(var));
					Node definingExpr = command.CreateNode(command.CreateSoftCastOp(typeUsage), arg);
					Node arg2 = command.CreateVarDefListNode(definingExpr, out var);
					ProjectOp op = command.CreateProjectOp(var);
					node = command.CreateNode(op, node, arg2);
				}
				this.m_internalTreeNode = command.BuildCollect(node, var);
			}
			Dictionary<string, Node> dictionary = new Dictionary<string, Node>(this.m_commandParameters.Length);
			for (int i = 0; i < this.m_commandParameters.Length; i++)
			{
				DbParameterReferenceExpression dbParameterReferenceExpression = this.m_commandParameters[i];
				Node node2 = targetIqtArguments[i];
				if (TypeSemantics.IsEnumerationType(node2.Op.Type))
				{
					node2 = targetIqtCommand.CreateNode(targetIqtCommand.CreateSoftCastOp(TypeHelpers.CreateEnumUnderlyingTypeUsage(node2.Op.Type)), node2);
				}
				dictionary.Add(dbParameterReferenceExpression.ParameterName, node2);
			}
			return FunctionImportMappingComposable.FunctionViewOpCopier.Copy(targetIqtCommand, this.m_internalTreeNode, dictionary);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000A1598 File Offset: 0x0009F798
		internal DbQueryCommandTree GenerateFunctionView(out DiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			DbExpression storeFunctionInvoke = base.TargetFunction.Invoke(this.GetParametersForTargetFunctionCall());
			DbExpression query;
			if (this.m_structuralTypeMappings != null)
			{
				query = this.GenerateStructuralTypeResultMappingView(storeFunctionInvoke, out discriminatorMap);
			}
			else
			{
				query = this.GenerateScalarResultMappingView(storeFunctionInvoke);
			}
			return DbQueryCommandTree.FromValidExpression(this._containerMapping.StorageMappingItemCollection.Workspace, DataSpace.SSpace, query, true);
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000A1828 File Offset: 0x0009FA28
		private IEnumerable<DbExpression> GetParametersForTargetFunctionCall()
		{
			using (ReadOnlyMetadataCollection<FunctionParameter>.Enumerator enumerator = base.TargetFunction.Parameters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FunctionParameter targetParameter = enumerator.Current;
					FunctionParameter functionImportParameter = base.FunctionImport.Parameters.Single((FunctionParameter p) => p.Name == targetParameter.Name);
					yield return this.m_commandParameters[base.FunctionImport.Parameters.IndexOf(functionImportParameter)];
				}
			}
			yield break;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000A1884 File Offset: 0x0009FA84
		private DbExpression GenerateStructuralTypeResultMappingView(DbExpression storeFunctionInvoke, out DiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			DbExpression dbExpression = storeFunctionInvoke;
			if (this.m_structuralTypeMappings.Count == 1)
			{
				Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>> tuple = this.m_structuralTypeMappings[0];
				StructuralType item = tuple.Item1;
				List<ConditionPropertyMapping> conditions = tuple.Item2;
				List<PropertyMapping> item2 = tuple.Item3;
				if (conditions.Count > 0)
				{
					dbExpression = from row in dbExpression
					where FunctionImportMappingComposable.GenerateStructuralTypeConditionsPredicate(conditions, row)
					select row;
				}
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs("row");
				DbExpression projection = FunctionImportMappingComposable.GenerateStructuralTypeMappingView(item, item2, dbExpressionBinding.Variable);
				dbExpression = dbExpressionBinding.Project(projection);
			}
			else
			{
				DbExpressionBinding binding = dbExpression.BindAs("row");
				List<DbExpression> list = (from m in this.m_structuralTypeMappings
				select FunctionImportMappingComposable.GenerateStructuralTypeConditionsPredicate(m.Item2, binding.Variable)).ToList<DbExpression>();
				dbExpression = binding.Filter(Helpers.BuildBalancedTreeInPlace<DbExpression>(list.ToArray(), (DbExpression prev, DbExpression next) => prev.Or(next)));
				binding = dbExpression.BindAs("row");
				List<DbExpression> list2 = new List<DbExpression>(this.m_structuralTypeMappings.Count);
				foreach (Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>> tuple2 in this.m_structuralTypeMappings)
				{
					StructuralType item3 = tuple2.Item1;
					List<PropertyMapping> item4 = tuple2.Item3;
					list2.Add(FunctionImportMappingComposable.GenerateStructuralTypeMappingView(item3, item4, binding.Variable));
				}
				DbExpression projection2 = DbExpressionBuilder.Case(list.Take(this.m_structuralTypeMappings.Count - 1), list2.Take(this.m_structuralTypeMappings.Count - 1), list2[this.m_structuralTypeMappings.Count - 1]);
				dbExpression = binding.Project(projection2);
				DiscriminatorMap.TryCreateDiscriminatorMap(base.FunctionImport.EntitySet, dbExpression, out discriminatorMap);
			}
			return dbExpression;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000A1A98 File Offset: 0x0009FC98
		private static DbExpression GenerateStructuralTypeMappingView(StructuralType structuralType, List<PropertyMapping> propertyMappings, DbExpression row)
		{
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(structuralType);
			List<DbExpression> list = new List<DbExpression>(allStructuralMembers.Count);
			for (int i = 0; i < propertyMappings.Count; i++)
			{
				PropertyMapping mapping = propertyMappings[i];
				list.Add(FunctionImportMappingComposable.GeneratePropertyMappingView(mapping, row));
			}
			return TypeUsage.Create(structuralType).New(list);
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000A1B0C File Offset: 0x0009FD0C
		private static DbExpression GenerateStructuralTypeConditionsPredicate(List<ConditionPropertyMapping> conditions, DbExpression row)
		{
			return Helpers.BuildBalancedTreeInPlace<DbExpression>((from c in conditions
			select FunctionImportMappingComposable.GeneratePredicate(c, row)).ToArray<DbExpression>(), (DbExpression prev, DbExpression next) => prev.And(next));
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000A1B64 File Offset: 0x0009FD64
		private static DbExpression GeneratePredicate(ConditionPropertyMapping condition, DbExpression row)
		{
			DbExpression dbExpression = FunctionImportMappingComposable.GenerateColumnRef(row, condition.Column);
			if (condition.IsNull == null)
			{
				return dbExpression.Equal(dbExpression.ResultType.Constant(condition.Value));
			}
			if (!condition.IsNull.Value)
			{
				return dbExpression.IsNull().Not();
			}
			return dbExpression.IsNull();
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000A1BC8 File Offset: 0x0009FDC8
		private static DbExpression GeneratePropertyMappingView(PropertyMapping mapping, DbExpression row)
		{
			ScalarPropertyMapping scalarPropertyMapping = (ScalarPropertyMapping)mapping;
			return FunctionImportMappingComposable.GenerateScalarPropertyMappingView(scalarPropertyMapping.Property, scalarPropertyMapping.Column, row);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000A1BF0 File Offset: 0x0009FDF0
		private static DbExpression GenerateScalarPropertyMappingView(EdmProperty edmProperty, EdmProperty columnProperty, DbExpression row)
		{
			DbExpression dbExpression = FunctionImportMappingComposable.GenerateColumnRef(row, columnProperty);
			if (!TypeSemantics.IsEqual(dbExpression.ResultType, edmProperty.TypeUsage))
			{
				dbExpression = dbExpression.CastTo(edmProperty.TypeUsage);
			}
			return dbExpression;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000A1C26 File Offset: 0x0009FE26
		private static DbExpression GenerateColumnRef(DbExpression row, EdmProperty column)
		{
			RowType rowType = (RowType)row.ResultType.EdmType;
			return row.Property(column.Name);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000A1CA8 File Offset: 0x0009FEA8
		private DbExpression GenerateScalarResultMappingView(DbExpression storeFunctionInvoke)
		{
			CollectionType functionImportReturnType;
			MetadataHelper.TryGetFunctionImportReturnCollectionType(base.FunctionImport, 0, out functionImportReturnType);
			CollectionType collectionType = (CollectionType)storeFunctionInvoke.ResultType.EdmType;
			RowType rowType = (RowType)collectionType.TypeUsage.EdmType;
			EdmProperty column = rowType.Properties[0];
			Func<DbExpression, DbExpression> scalarView = delegate(DbExpression row)
			{
				DbPropertyExpression dbPropertyExpression = row.Property(column);
				if (TypeSemantics.IsEqual(functionImportReturnType.TypeUsage, column.TypeUsage))
				{
					return dbPropertyExpression;
				}
				return dbPropertyExpression.CastTo(functionImportReturnType.TypeUsage);
			};
			return from row in storeFunctionInvoke
			select scalarView(row);
		}

		// Token: 0x04000C2F RID: 3119
		private readonly FunctionImportResultMapping _resultMapping;

		// Token: 0x04000C30 RID: 3120
		private readonly EntityContainerMapping _containerMapping;

		// Token: 0x04000C31 RID: 3121
		private readonly DbParameterReferenceExpression[] m_commandParameters;

		// Token: 0x04000C32 RID: 3122
		private readonly List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> m_structuralTypeMappings;

		// Token: 0x04000C33 RID: 3123
		private readonly EdmProperty[] m_targetFunctionKeys;

		// Token: 0x04000C34 RID: 3124
		private Node m_internalTreeNode;

		// Token: 0x020003B8 RID: 952
		private sealed class FunctionViewOpCopier : OpCopier
		{
			// Token: 0x060022E7 RID: 8935 RVA: 0x000A2D2E File Offset: 0x000A0F2E
			private FunctionViewOpCopier(Command cmd, Dictionary<string, Node> viewArguments) : base(cmd)
			{
				this.m_viewArguments = viewArguments;
			}

			// Token: 0x060022E8 RID: 8936 RVA: 0x000A2D3E File Offset: 0x000A0F3E
			internal static Node Copy(Command cmd, Node viewNode, Dictionary<string, Node> viewArguments)
			{
				return new FunctionImportMappingComposable.FunctionViewOpCopier(cmd, viewArguments).CopyNode(viewNode);
			}

			// Token: 0x060022E9 RID: 8937 RVA: 0x000A2D50 File Offset: 0x000A0F50
			public override Node Visit(VarRefOp op, Node n)
			{
				Node n2;
				if (op.Var.VarType == VarType.Parameter && this.m_viewArguments.TryGetValue(((ParameterVar)op.Var).ParameterName, out n2))
				{
					return OpCopier.Copy(this.m_destCmd, n2);
				}
				return base.Visit(op, n);
			}

			// Token: 0x04000C3B RID: 3131
			private readonly Dictionary<string, Node> m_viewArguments;
		}
	}
}
