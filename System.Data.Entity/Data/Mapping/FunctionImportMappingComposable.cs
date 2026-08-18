using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Data.Query.PlanCompiler;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x02000221 RID: 545
	internal class FunctionImportMappingComposable : FunctionImportMapping
	{
		// Token: 0x06002392 RID: 9106 RVA: 0x0007FA28 File Offset: 0x0007DC28
		internal FunctionImportMappingComposable(EdmFunction functionImport, EdmFunction targetFunction, List<Tuple<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>>> structuralTypeMappings, EdmProperty[] targetFunctionKeys, StorageMappingItemCollection mappingItemCollection, string sourceLocation, LineInfo lineInfo) : base(functionImport, targetFunction)
		{
			EntityUtil.CheckArgumentNull<StorageMappingItemCollection>(mappingItemCollection, "mappingItemCollection");
			this.m_mappingItemCollection = mappingItemCollection;
			this.m_commandParameters = (from p in functionImport.Parameters
			select TypeHelpers.GetPrimitiveTypeUsageForScalar(p.TypeUsage).Parameter(p.Name)).ToArray<DbParameterReferenceExpression>();
			this.m_structuralTypeMappings = structuralTypeMappings;
			this.m_targetFunctionKeys = targetFunctionKeys;
			this.m_sourceLocation = sourceLocation;
			this.m_lineInfo = lineInfo;
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x0007FAA6 File Offset: 0x0007DCA6
		internal EdmProperty[] TvfKeys
		{
			get
			{
				return this.m_targetFunctionKeys;
			}
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0007FAB0 File Offset: 0x0007DCB0
		internal Node GetInternalTree(Command targetIqtCommand, IList<Node> targetIqtArguments)
		{
			if (this.m_internalTreeNode == null)
			{
				List<EdmSchemaError> list = new List<EdmSchemaError>();
				DiscriminatorMap discriminatorMap;
				DbQueryCommandTree ctree = this.GenerateFunctionView(list, out discriminatorMap);
				if (list.Count > 0)
				{
					throw new MappingException(Helper.CombineErrorMessage(list));
				}
				Command command = ITreeGenerator.Generate(ctree, discriminatorMap);
				Node root = command.Root;
				PlanCompiler.Assert(root.Op.OpType == OpType.PhysicalProject, "Expected a physical projectOp at the root of the tree - found " + root.Op.OpType.ToString());
				PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)root.Op;
				Node child = root.Child0;
				command.DisableVarVecEnumCaching();
				Node node = child;
				Var var = physicalProjectOp.Outputs[0];
				TypeUsage type = physicalProjectOp.ColumnMap.Type;
				if (!Command.EqualTypes(type, this.FunctionImport.ReturnParameter.TypeUsage))
				{
					CollectionType collectionType = (CollectionType)this.FunctionImport.ReturnParameter.TypeUsage.EdmType;
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

		// Token: 0x06002395 RID: 9109 RVA: 0x0007FCA0 File Offset: 0x0007DEA0
		internal DbQueryCommandTree GenerateFunctionView(IList<EdmSchemaError> errors, out DiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			DbExpression storeFunctionInvoke = this.TargetFunction.Invoke(this.GetParametersForTargetFunctionCall());
			DbExpression dbExpression;
			if (this.m_structuralTypeMappings != null)
			{
				dbExpression = this.GenerateStructuralTypeResultMappingView(storeFunctionInvoke, errors, out discriminatorMap);
			}
			else
			{
				dbExpression = this.GenerateScalarResultMappingView(storeFunctionInvoke);
			}
			if (dbExpression == null)
			{
				return null;
			}
			return DbQueryCommandTree.FromValidExpression(this.m_mappingItemCollection.Workspace, DataSpace.SSpace, dbExpression);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0007FCF5 File Offset: 0x0007DEF5
		private IEnumerable<DbExpression> GetParametersForTargetFunctionCall()
		{
			using (ReadOnlyMetadataCollection<FunctionParameter>.Enumerator enumerator = this.TargetFunction.Parameters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FunctionParameter targetParameter = enumerator.Current;
					FunctionParameter value = this.FunctionImport.Parameters.Single((FunctionParameter p) => p.Name == targetParameter.Name);
					yield return this.m_commandParameters[this.FunctionImport.Parameters.IndexOf(value)];
				}
			}
			ReadOnlyMetadataCollection<FunctionParameter>.Enumerator enumerator = default(ReadOnlyMetadataCollection<FunctionParameter>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0007FD08 File Offset: 0x0007DF08
		internal void ValidateFunctionView(IList<EdmSchemaError> errors)
		{
			DiscriminatorMap discriminatorMap;
			this.GenerateFunctionView(errors, out discriminatorMap);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x0007FD20 File Offset: 0x0007DF20
		private DbExpression GenerateStructuralTypeResultMappingView(DbExpression storeFunctionInvoke, IList<EdmSchemaError> errors, out DiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			DbExpression dbExpression = storeFunctionInvoke;
			if (this.m_structuralTypeMappings.Count == 1)
			{
				Tuple<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>> tuple = this.m_structuralTypeMappings[0];
				StructuralType item = tuple.Item1;
				List<StorageConditionPropertyMapping> conditions = tuple.Item2;
				List<StoragePropertyMapping> item2 = tuple.Item3;
				if (conditions.Count > 0)
				{
					dbExpression = from row in dbExpression
					where this.GenerateStructuralTypeConditionsPredicate(conditions, row)
					select row;
				}
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs("row");
				DbExpression dbExpression2 = this.GenerateStructuralTypeMappingView(item, item2, dbExpressionBinding.Variable, errors);
				if (dbExpression2 == null)
				{
					return null;
				}
				dbExpression = dbExpressionBinding.Project(dbExpression2);
			}
			else
			{
				DbExpressionBinding binding = dbExpression.BindAs("row");
				List<DbExpression> list = (from m in this.m_structuralTypeMappings
				select this.GenerateStructuralTypeConditionsPredicate(m.Item2, binding.Variable)).ToList<DbExpression>();
				dbExpression = binding.Filter(Helpers.BuildBalancedTreeInPlace<DbExpression>(list.ToArray(), (DbExpression prev, DbExpression next) => prev.Or(next)));
				binding = dbExpression.BindAs("row");
				List<DbExpression> list2 = new List<DbExpression>(this.m_structuralTypeMappings.Count);
				foreach (Tuple<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>> tuple2 in this.m_structuralTypeMappings)
				{
					StructuralType item3 = tuple2.Item1;
					List<StoragePropertyMapping> item4 = tuple2.Item3;
					DbExpression dbExpression3 = this.GenerateStructuralTypeMappingView(item3, item4, binding.Variable, errors);
					if (dbExpression3 != null)
					{
						list2.Add(dbExpression3);
					}
				}
				if (list2.Count != this.m_structuralTypeMappings.Count)
				{
					return null;
				}
				DbExpression projection = DbExpressionBuilder.Case(list.Take(this.m_structuralTypeMappings.Count - 1), list2.Take(this.m_structuralTypeMappings.Count - 1), list2[this.m_structuralTypeMappings.Count - 1]);
				dbExpression = binding.Project(projection);
				DiscriminatorMap.TryCreateDiscriminatorMap(this.FunctionImport.EntitySet, dbExpression, out discriminatorMap);
			}
			return dbExpression;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0007FF60 File Offset: 0x0007E160
		private DbExpression GenerateStructuralTypeMappingView(StructuralType structuralType, List<StoragePropertyMapping> propertyMappings, DbExpression row, IList<EdmSchemaError> errors)
		{
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(structuralType);
			List<DbExpression> list = new List<DbExpression>(allStructuralMembers.Count);
			for (int i = 0; i < propertyMappings.Count; i++)
			{
				StoragePropertyMapping storagePropertyMapping = propertyMappings[i];
				DbExpression dbExpression = this.GeneratePropertyMappingView(storagePropertyMapping, row, new List<string>
				{
					storagePropertyMapping.EdmProperty.Name
				}, errors);
				if (dbExpression != null)
				{
					list.Add(dbExpression);
				}
			}
			if (list.Count != propertyMappings.Count)
			{
				return null;
			}
			return TypeUsage.Create(structuralType).New(list);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0007FFE4 File Offset: 0x0007E1E4
		private DbExpression GenerateStructuralTypeConditionsPredicate(List<StorageConditionPropertyMapping> conditions, DbExpression row)
		{
			return Helpers.BuildBalancedTreeInPlace<DbExpression>((from c in conditions
			select this.GeneratePredicate(c, row)).ToArray<DbExpression>(), (DbExpression prev, DbExpression next) => prev.And(next));
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00080044 File Offset: 0x0007E244
		private DbExpression GeneratePredicate(StorageConditionPropertyMapping condition, DbExpression row)
		{
			DbExpression dbExpression = this.GenerateColumnRef(row, condition.ColumnProperty);
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

		// Token: 0x0600239C RID: 9116 RVA: 0x000800AC File Offset: 0x0007E2AC
		private DbExpression GeneratePropertyMappingView(StoragePropertyMapping mapping, DbExpression row, List<string> context, IList<EdmSchemaError> errors)
		{
			StorageScalarPropertyMapping storageScalarPropertyMapping = (StorageScalarPropertyMapping)mapping;
			return this.GenerateScalarPropertyMappingView(storageScalarPropertyMapping.EdmProperty, storageScalarPropertyMapping.ColumnProperty, row);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000800D4 File Offset: 0x0007E2D4
		private DbExpression GenerateScalarPropertyMappingView(EdmProperty edmProperty, EdmProperty columnProperty, DbExpression row)
		{
			DbExpression dbExpression = this.GenerateColumnRef(row, columnProperty);
			if (!TypeSemantics.IsEqual(dbExpression.ResultType, edmProperty.TypeUsage))
			{
				dbExpression = dbExpression.CastTo(edmProperty.TypeUsage);
			}
			return dbExpression;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0008010C File Offset: 0x0007E30C
		private DbExpression GenerateColumnRef(DbExpression row, EdmProperty column)
		{
			RowType rowType = (RowType)row.ResultType.EdmType;
			return row.Property(column.Name);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00080138 File Offset: 0x0007E338
		private DbExpression GenerateScalarResultMappingView(DbExpression storeFunctionInvoke)
		{
			CollectionType functionImportReturnType;
			MetadataHelper.TryGetFunctionImportReturnCollectionType(this.FunctionImport, 0, out functionImportReturnType);
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

		// Token: 0x04000FBD RID: 4029
		private readonly StorageMappingItemCollection m_mappingItemCollection;

		// Token: 0x04000FBE RID: 4030
		private readonly DbParameterReferenceExpression[] m_commandParameters;

		// Token: 0x04000FBF RID: 4031
		private readonly List<Tuple<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>>> m_structuralTypeMappings;

		// Token: 0x04000FC0 RID: 4032
		private readonly EdmProperty[] m_targetFunctionKeys;

		// Token: 0x04000FC1 RID: 4033
		private Node m_internalTreeNode;

		// Token: 0x04000FC2 RID: 4034
		private readonly string m_sourceLocation;

		// Token: 0x04000FC3 RID: 4035
		private readonly LineInfo m_lineInfo;

		// Token: 0x02000558 RID: 1368
		private sealed class FunctionViewOpCopier : OpCopier
		{
			// Token: 0x06003F09 RID: 16137 RVA: 0x000E990F File Offset: 0x000E7B0F
			private FunctionViewOpCopier(Command cmd, Dictionary<string, Node> viewArguments) : base(cmd)
			{
				this.m_viewArguments = viewArguments;
			}

			// Token: 0x06003F0A RID: 16138 RVA: 0x000E991F File Offset: 0x000E7B1F
			internal static Node Copy(Command cmd, Node viewNode, Dictionary<string, Node> viewArguments)
			{
				return new FunctionImportMappingComposable.FunctionViewOpCopier(cmd, viewArguments).CopyNode(viewNode);
			}

			// Token: 0x06003F0B RID: 16139 RVA: 0x000E9930 File Offset: 0x000E7B30
			public override Node Visit(VarRefOp op, Node n)
			{
				Node n2;
				if (op.Var.VarType == VarType.Parameter && this.m_viewArguments.TryGetValue(((ParameterVar)op.Var).ParameterName, out n2))
				{
					return OpCopier.Copy(this.m_destCmd, n2);
				}
				return base.Visit(op, n);
			}

			// Token: 0x04001C13 RID: 7187
			private Dictionary<string, Node> m_viewArguments;
		}
	}
}
