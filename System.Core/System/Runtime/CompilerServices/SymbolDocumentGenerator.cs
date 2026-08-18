using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000143 RID: 323
	internal sealed class SymbolDocumentGenerator : DebugInfoGenerator
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x00025C10 File Offset: 0x00023E10
		private ISymbolDocumentWriter GetSymbolWriter(MethodBuilder method, SymbolDocumentInfo document)
		{
			if (this._symbolWriters == null)
			{
				this._symbolWriters = new Dictionary<SymbolDocumentInfo, ISymbolDocumentWriter>();
			}
			ISymbolDocumentWriter symbolDocumentWriter;
			if (!this._symbolWriters.TryGetValue(document, out symbolDocumentWriter))
			{
				symbolDocumentWriter = ((ModuleBuilder)method.Module).DefineDocument(document.FileName, document.Language, document.LanguageVendor, SymbolGuids.DocumentType_Text);
				this._symbolWriters.Add(document, symbolDocumentWriter);
			}
			return symbolDocumentWriter;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00025C78 File Offset: 0x00023E78
		internal override void MarkSequencePoint(LambdaExpression method, MethodBase methodBase, ILGenerator ilg, DebugInfoExpression sequencePoint)
		{
			MethodBuilder methodBuilder = methodBase as MethodBuilder;
			if (methodBuilder != null)
			{
				ilg.MarkSequencePoint(this.GetSymbolWriter(methodBuilder, sequencePoint.Document), sequencePoint.StartLine, sequencePoint.StartColumn, sequencePoint.EndLine, sequencePoint.EndColumn);
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00025CC5 File Offset: 0x00023EC5
		public override void MarkSequencePoint(LambdaExpression method, int ilOffset, DebugInfoExpression sequencePoint)
		{
			throw Error.PdbGeneratorNeedsExpressionCompiler();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00025CCC File Offset: 0x00023ECC
		internal override void SetLocalName(LocalBuilder localBuilder, string name)
		{
			localBuilder.SetLocalSymInfo(name);
		}

		// Token: 0x04000773 RID: 1907
		private Dictionary<SymbolDocumentInfo, ISymbolDocumentWriter> _symbolWriters;
	}
}
