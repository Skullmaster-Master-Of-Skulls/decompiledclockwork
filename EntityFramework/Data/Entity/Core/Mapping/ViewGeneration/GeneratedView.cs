using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000433 RID: 1075
	internal sealed class GeneratedView : InternalBase
	{
		// Token: 0x0600277A RID: 10106 RVA: 0x000BFAF8 File Offset: 0x000BDCF8
		internal static GeneratedView CreateGeneratedView(EntitySetBase extent, EdmType type, DbQueryCommandTree commandTree, string eSQL, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config)
		{
			DiscriminatorMap discriminatorMap = null;
			if (commandTree != null)
			{
				commandTree = ViewSimplifier.SimplifyView(extent, commandTree);
				if (extent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
				{
					DiscriminatorMap.TryCreateDiscriminatorMap((EntitySet)extent, commandTree.Query, out discriminatorMap);
				}
			}
			return new GeneratedView(extent, type, commandTree, eSQL, discriminatorMap, mappingItemCollection, config);
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000BFB3F File Offset: 0x000BDD3F
		internal static GeneratedView CreateGeneratedViewForFKAssociationSet(EntitySetBase extent, EdmType type, DbQueryCommandTree commandTree, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config)
		{
			return new GeneratedView(extent, type, commandTree, null, null, mappingItemCollection, config);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000BFB50 File Offset: 0x000BDD50
		internal static bool TryParseUserSpecifiedView(EntitySetBaseMapping setMapping, EntityTypeBase type, string eSQL, bool includeSubtypes, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config, IList<EdmSchemaError> errors, out GeneratedView generatedView)
		{
			bool flag = false;
			DbQueryCommandTree dbQueryCommandTree;
			DiscriminatorMap discriminatorMap;
			Exception ex;
			if (!GeneratedView.TryParseView(eSQL, true, setMapping.Set, mappingItemCollection, config, out dbQueryCommandTree, out discriminatorMap, out ex))
			{
				EdmSchemaError item = new EdmSchemaError(Strings.Mapping_Invalid_QueryView2(setMapping.Set.Name, ex.Message), 2068, EdmSchemaErrorSeverity.Error, setMapping.EntityContainerMapping.SourceLocation, setMapping.StartLineNumber, setMapping.StartLinePosition, ex);
				errors.Add(item);
				flag = true;
			}
			else
			{
				foreach (EdmSchemaError item2 in ViewValidator.ValidateQueryView(dbQueryCommandTree, setMapping, type, includeSubtypes))
				{
					errors.Add(item2);
					flag = true;
				}
				CollectionType collectionType = dbQueryCommandTree.Query.ResultType.EdmType as CollectionType;
				if (collectionType == null || !setMapping.Set.ElementType.IsAssignableFrom(collectionType.TypeUsage.EdmType))
				{
					EdmSchemaError item3 = new EdmSchemaError(Strings.Mapping_Invalid_QueryView_Type(setMapping.Set.Name), 2069, EdmSchemaErrorSeverity.Error, setMapping.EntityContainerMapping.SourceLocation, setMapping.StartLineNumber, setMapping.StartLinePosition);
					errors.Add(item3);
					flag = true;
				}
			}
			if (!flag)
			{
				generatedView = new GeneratedView(setMapping.Set, type, dbQueryCommandTree, eSQL, discriminatorMap, mappingItemCollection, config);
				return true;
			}
			generatedView = null;
			return false;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000BFCA8 File Offset: 0x000BDEA8
		private GeneratedView(EntitySetBase extent, EdmType type, DbQueryCommandTree commandTree, string eSQL, DiscriminatorMap discriminatorMap, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config)
		{
			this.m_extent = extent;
			this.m_type = type;
			this.m_commandTree = commandTree;
			this.m_eSQL = eSQL;
			this.m_discriminatorMap = discriminatorMap;
			this.m_mappingItemCollection = mappingItemCollection;
			this.m_config = config;
			if (this.m_config.IsViewTracing)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				this.ToCompactString(stringBuilder);
				Helpers.FormatTraceLine("CQL view for {0}", new object[]
				{
					stringBuilder.ToString()
				});
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x000BFD2A File Offset: 0x000BDF2A
		internal string eSQL
		{
			get
			{
				return this.m_eSQL;
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000BFD34 File Offset: 0x000BDF34
		internal DbQueryCommandTree GetCommandTree()
		{
			if (this.m_commandTree != null)
			{
				return this.m_commandTree;
			}
			Exception ex;
			if (GeneratedView.TryParseView(this.m_eSQL, false, this.m_extent, this.m_mappingItemCollection, this.m_config, out this.m_commandTree, out this.m_discriminatorMap, out ex))
			{
				return this.m_commandTree;
			}
			throw new MappingException(Strings.Mapping_Invalid_QueryView(this.m_extent.Name, ex.Message));
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000BFDA0 File Offset: 0x000BDFA0
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "projectOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal Node GetInternalTree(Command targetIqtCommand)
		{
			if (this.m_internalTreeNode == null)
			{
				DbQueryCommandTree commandTree = this.GetCommandTree();
				Command command = ITreeGenerator.Generate(commandTree, this.m_discriminatorMap);
				PlanCompiler.Assert(command.Root.Op.OpType == OpType.PhysicalProject, "Expected a physical projectOp at the root of the tree - found " + command.Root.Op.OpType);
				command.DisableVarVecEnumCaching();
				this.m_internalTreeNode = command.Root.Child0;
			}
			return OpCopier.Copy(targetIqtCommand, this.m_internalTreeNode);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000BFE24 File Offset: 0x000BE024
		private static bool TryParseView(string eSQL, bool isUserSpecified, EntitySetBase extent, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config, out DbQueryCommandTree commandTree, out DiscriminatorMap discriminatorMap, out Exception parserException)
		{
			commandTree = null;
			discriminatorMap = null;
			parserException = null;
			config.StartSingleWatch(PerfType.ViewParsing);
			try
			{
				ParserOptions.CompilationMode compilationMode = ParserOptions.CompilationMode.RestrictedViewGenerationMode;
				if (isUserSpecified)
				{
					compilationMode = ParserOptions.CompilationMode.UserViewGenerationMode;
				}
				commandTree = (DbQueryCommandTree)ExternalCalls.CompileView(eSQL, mappingItemCollection, compilationMode);
				commandTree = ViewSimplifier.SimplifyView(extent, commandTree);
				if (extent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
				{
					DiscriminatorMap.TryCreateDiscriminatorMap((EntitySet)extent, commandTree.Query, out discriminatorMap);
				}
			}
			catch (Exception ex)
			{
				if (!ex.IsCatchableExceptionType())
				{
					throw;
				}
				parserException = ex;
			}
			finally
			{
				config.StopSingleWatch(PerfType.ViewParsing);
			}
			return parserException == null;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000BFECC File Offset: 0x000BE0CC
		internal override void ToCompactString(StringBuilder builder)
		{
			bool flag = this.m_type != this.m_extent.ElementType;
			if (flag)
			{
				builder.Append("OFTYPE(");
			}
			builder.AppendFormat("{0}.{1}", this.m_extent.EntityContainer.Name, this.m_extent.Name);
			if (flag)
			{
				builder.Append(", ").Append(this.m_type.Name).Append(')');
			}
			builder.AppendLine(" = ");
			if (!string.IsNullOrEmpty(this.m_eSQL))
			{
				builder.Append(this.m_eSQL);
				return;
			}
			builder.Append(this.m_commandTree.Print());
		}

		// Token: 0x04000EDB RID: 3803
		private readonly EntitySetBase m_extent;

		// Token: 0x04000EDC RID: 3804
		private readonly EdmType m_type;

		// Token: 0x04000EDD RID: 3805
		private DbQueryCommandTree m_commandTree;

		// Token: 0x04000EDE RID: 3806
		private readonly string m_eSQL;

		// Token: 0x04000EDF RID: 3807
		private Node m_internalTreeNode;

		// Token: 0x04000EE0 RID: 3808
		private DiscriminatorMap m_discriminatorMap;

		// Token: 0x04000EE1 RID: 3809
		private readonly StorageMappingItemCollection m_mappingItemCollection;

		// Token: 0x04000EE2 RID: 3810
		private readonly ConfigViewGenerator m_config;
	}
}
