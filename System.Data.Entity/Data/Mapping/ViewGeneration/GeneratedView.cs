using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Common.EntitySql;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Entity.Util;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Data.Query.PlanCompiler;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x02000269 RID: 617
	internal sealed class GeneratedView : InternalBase
	{
		// Token: 0x060025EA RID: 9706 RVA: 0x0008F2D4 File Offset: 0x0008D4D4
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

		// Token: 0x060025EB RID: 9707 RVA: 0x0008F31B File Offset: 0x0008D51B
		internal static GeneratedView CreateGeneratedViewForFKAssociationSet(EntitySetBase extent, EdmType type, DbQueryCommandTree commandTree, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config)
		{
			return new GeneratedView(extent, type, commandTree, null, null, mappingItemCollection, config);
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0008F32C File Offset: 0x0008D52C
		internal static bool TryParseUserSpecifiedView(StorageSetMapping setMapping, EntityTypeBase type, string eSQL, bool includeSubtypes, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config, IList<EdmSchemaError> errors, out GeneratedView generatedView)
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

		// Token: 0x060025ED RID: 9709 RVA: 0x0008F484 File Offset: 0x0008D684
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

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x0008F504 File Offset: 0x0008D704
		internal string eSQL
		{
			get
			{
				return this.m_eSQL;
			}
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0008F50C File Offset: 0x0008D70C
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

		// Token: 0x060025F0 RID: 9712 RVA: 0x0008F578 File Offset: 0x0008D778
		internal Node GetInternalTree(Command targetIqtCommand)
		{
			if (this.m_internalTreeNode == null)
			{
				DbQueryCommandTree commandTree = this.GetCommandTree();
				Command command = ITreeGenerator.Generate(commandTree, this.m_discriminatorMap);
				PlanCompiler.Assert(command.Root.Op.OpType == OpType.PhysicalProject, "Expected a physical projectOp at the root of the tree - found " + command.Root.Op.OpType.ToString());
				command.DisableVarVecEnumCaching();
				this.m_internalTreeNode = command.Root.Child0;
			}
			return OpCopier.Copy(targetIqtCommand, this.m_internalTreeNode);
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x0008F608 File Offset: 0x0008D808
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
				if (!isUserSpecified || AppSettings.SimplifyUserSpecifiedViews)
				{
					commandTree = ViewSimplifier.SimplifyView(extent, commandTree);
				}
				if (extent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
				{
					DiscriminatorMap.TryCreateDiscriminatorMap((EntitySet)extent, commandTree.Query, out discriminatorMap);
				}
			}
			catch (Exception ex)
			{
				if (!EntityUtil.IsCatchableExceptionType(ex))
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

		// Token: 0x060025F2 RID: 9714 RVA: 0x0008F6B8 File Offset: 0x0008D8B8
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

		// Token: 0x0400117D RID: 4477
		private readonly EntitySetBase m_extent;

		// Token: 0x0400117E RID: 4478
		private readonly EdmType m_type;

		// Token: 0x0400117F RID: 4479
		private DbQueryCommandTree m_commandTree;

		// Token: 0x04001180 RID: 4480
		private readonly string m_eSQL;

		// Token: 0x04001181 RID: 4481
		private Node m_internalTreeNode;

		// Token: 0x04001182 RID: 4482
		private DiscriminatorMap m_discriminatorMap;

		// Token: 0x04001183 RID: 4483
		private readonly StorageMappingItemCollection m_mappingItemCollection;

		// Token: 0x04001184 RID: 4484
		private readonly ConfigViewGenerator m_config;
	}
}
