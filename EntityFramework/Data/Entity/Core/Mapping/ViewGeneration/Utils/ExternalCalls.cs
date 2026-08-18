using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Utils
{
	// Token: 0x0200048C RID: 1164
	internal static class ExternalCalls
	{
		// Token: 0x06002B16 RID: 11030 RVA: 0x000D07FE File Offset: 0x000CE9FE
		internal static bool IsReservedKeyword(string name)
		{
			return CqlLexer.IsReservedKeyword(name);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000D0808 File Offset: 0x000CEA08
		internal static DbCommandTree CompileView(string viewDef, StorageMappingItemCollection mappingItemCollection, ParserOptions.CompilationMode compilationMode)
		{
			Perspective perspective = new TargetPerspective(mappingItemCollection.Workspace);
			return CqlQuery.Compile(viewDef, perspective, new ParserOptions
			{
				ParserCompilationMode = compilationMode
			}, null).CommandTree;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000D0854 File Offset: 0x000CEA54
		internal static DbExpression CompileFunctionView(string viewDef, StorageMappingItemCollection mappingItemCollection, ParserOptions.CompilationMode compilationMode, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			Perspective perspective = new TargetPerspective(mappingItemCollection.Workspace);
			ParserOptions parserOptions = new ParserOptions();
			parserOptions.ParserCompilationMode = compilationMode;
			DbLambda lambda = CqlQuery.CompileQueryCommandLambda(viewDef, perspective, parserOptions, null, from pInfo in parameters
			select pInfo.ResultType.Variable(pInfo.ParameterName));
			return lambda.Invoke(parameters);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000D08DC File Offset: 0x000CEADC
		internal static DbLambda CompileFunctionDefinition(string functionDefinition, IList<FunctionParameter> functionParameters, EdmItemCollection edmItemCollection)
		{
			ModelPerspective perspective = new ModelPerspective(new MetadataWorkspace(() => edmItemCollection, () => null, () => null));
			return CqlQuery.CompileQueryCommandLambda(functionDefinition, perspective, null, null, from pInfo in functionParameters
			select pInfo.TypeUsage.Variable(pInfo.Name));
		}
	}
}
