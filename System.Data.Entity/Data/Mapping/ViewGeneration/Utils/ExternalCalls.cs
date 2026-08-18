using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping.ViewGeneration.Utils
{
	// Token: 0x02000270 RID: 624
	internal static class ExternalCalls
	{
		// Token: 0x06002627 RID: 9767 RVA: 0x0009196E File Offset: 0x0008FB6E
		internal static bool IsReservedKeyword(string name)
		{
			return CqlLexer.IsReservedKeyword(name);
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x00091978 File Offset: 0x0008FB78
		internal static DbCommandTree CompileView(string viewDef, StorageMappingItemCollection mappingItemCollection, ParserOptions.CompilationMode compilationMode)
		{
			Perspective perspective = new TargetPerspective(mappingItemCollection.Workspace);
			return CqlQuery.Compile(viewDef, perspective, new ParserOptions
			{
				ParserCompilationMode = compilationMode
			}, null).CommandTree;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000919B0 File Offset: 0x0008FBB0
		internal static DbExpression CompileFunctionView(string viewDef, StorageMappingItemCollection mappingItemCollection, ParserOptions.CompilationMode compilationMode, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			Perspective perspective = new TargetPerspective(mappingItemCollection.Workspace);
			ParserOptions parserOptions = new ParserOptions();
			parserOptions.ParserCompilationMode = compilationMode;
			DbLambda lambda = CqlQuery.CompileQueryCommandLambda(viewDef, perspective, parserOptions, null, from pInfo in parameters
			select pInfo.ResultType.Variable(pInfo.ParameterName));
			return lambda.Invoke(parameters);
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00091A10 File Offset: 0x0008FC10
		internal static DbLambda CompileFunctionDefinition(string functionFullName, string functionDefinition, IList<FunctionParameter> functionParameters, EdmItemCollection edmItemCollection)
		{
			MetadataWorkspace metadataWorkspace = new MetadataWorkspace();
			metadataWorkspace.RegisterItemCollection(edmItemCollection);
			Perspective perspective = new ModelPerspective(metadataWorkspace);
			return CqlQuery.CompileQueryCommandLambda(functionDefinition, perspective, null, null, from pInfo in functionParameters
			select pInfo.TypeUsage.Variable(pInfo.Name));
		}
	}
}
