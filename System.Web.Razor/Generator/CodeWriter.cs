using System;
using System.CodeDom;
using System.Globalization;
using System.IO;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000022 RID: 34
	internal abstract class CodeWriter : IDisposable
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004FE3 File Offset: 0x000031E3
		public string Content
		{
			get
			{
				return this.InnerWriter.ToString();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004FF0 File Offset: 0x000031F0
		public StringWriter InnerWriter
		{
			get
			{
				if (this._writer == null)
				{
					this._writer = new StringWriter(CultureInfo.InvariantCulture);
				}
				return this._writer;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005010 File Offset: 0x00003210
		public virtual bool SupportsMidStatementLinePragmas
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600011E RID: 286
		public abstract void WriteParameterSeparator();

		// Token: 0x0600011F RID: 287
		public abstract void WriteReturn();

		// Token: 0x06000120 RID: 288
		public abstract void WriteLinePragma(int? lineNumber, string fileName);

		// Token: 0x06000121 RID: 289
		public abstract void WriteHelperHeaderPrefix(string templateTypeName, bool isStatic);

		// Token: 0x06000122 RID: 290
		public abstract void WriteSnippet(string snippet);

		// Token: 0x06000123 RID: 291
		public abstract void WriteStringLiteral(string literal);

		// Token: 0x06000124 RID: 292
		public abstract int WriteVariableDeclaration(string type, string name, string value);

		// Token: 0x06000125 RID: 293 RVA: 0x00005013 File Offset: 0x00003213
		public virtual void WriteLinePragma()
		{
			this.WriteLinePragma(null);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000501C File Offset: 0x0000321C
		public virtual void WriteLinePragma(CodeLinePragma pragma)
		{
			if (pragma == null)
			{
				this.WriteLinePragma(null, null);
				return;
			}
			this.WriteLinePragma(new int?(pragma.LineNumber), pragma.FileName);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005054 File Offset: 0x00003254
		public virtual void WriteHiddenLinePragma()
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005056 File Offset: 0x00003256
		public virtual void WriteDisableUnusedFieldWarningPragma()
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005058 File Offset: 0x00003258
		public virtual void WriteRestoreUnusedFieldWarningPragma()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000505A File Offset: 0x0000325A
		public virtual void WriteIdentifier(string identifier)
		{
			this.InnerWriter.Write(identifier);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005068 File Offset: 0x00003268
		public virtual void WriteHelperHeaderSuffix(string templateTypeName)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000506A File Offset: 0x0000326A
		public virtual void WriteHelperTrailer()
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000506C File Offset: 0x0000326C
		public void WriteStartMethodInvoke(string methodName)
		{
			this.EmitStartMethodInvoke(methodName);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005075 File Offset: 0x00003275
		public void WriteStartMethodInvoke(string methodName, params string[] genericArguments)
		{
			this.EmitStartMethodInvoke(methodName, genericArguments);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000507F File Offset: 0x0000327F
		public void WriteEndMethodInvoke()
		{
			this.EmitEndMethodInvoke();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005087 File Offset: 0x00003287
		public virtual void WriteEndStatement()
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005089 File Offset: 0x00003289
		public virtual void WriteStartAssignment(string variableName)
		{
			this.InnerWriter.Write(variableName);
			this.InnerWriter.Write(" = ");
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000050A7 File Offset: 0x000032A7
		public void WriteStartLambdaExpression(params string[] parameterNames)
		{
			this.EmitStartLambdaExpression(parameterNames);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000050B0 File Offset: 0x000032B0
		public void WriteStartConstructor(string typeName)
		{
			this.EmitStartConstructor(typeName);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000050B9 File Offset: 0x000032B9
		public void WriteStartLambdaDelegate(params string[] parameterNames)
		{
			this.EmitStartLambdaDelegate(parameterNames);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000050C2 File Offset: 0x000032C2
		public void WriteEndLambdaExpression()
		{
			this.EmitEndLambdaExpression();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000050CA File Offset: 0x000032CA
		public void WriteEndConstructor()
		{
			this.EmitEndConstructor();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000050D2 File Offset: 0x000032D2
		public void WriteEndLambdaDelegate()
		{
			this.EmitEndLambdaDelegate();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000050DA File Offset: 0x000032DA
		public virtual void WriteLineContinuation()
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000050DC File Offset: 0x000032DC
		public virtual void WriteBooleanLiteral(bool value)
		{
			this.WriteSnippet(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000050F0 File Offset: 0x000032F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000050FF File Offset: 0x000032FF
		public void Clear()
		{
			if (this.InnerWriter != null)
			{
				this.InnerWriter.GetStringBuilder().Clear();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000511A File Offset: 0x0000331A
		public CodeSnippetStatement ToStatement()
		{
			return new CodeSnippetStatement(this.Content);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005127 File Offset: 0x00003327
		public CodeSnippetTypeMember ToTypeMember()
		{
			return new CodeSnippetTypeMember(this.Content);
		}

		// Token: 0x0600013E RID: 318
		protected internal abstract void EmitStartLambdaDelegate(string[] parameterNames);

		// Token: 0x0600013F RID: 319
		protected internal abstract void EmitStartLambdaExpression(string[] parameterNames);

		// Token: 0x06000140 RID: 320
		protected internal abstract void EmitStartConstructor(string typeName);

		// Token: 0x06000141 RID: 321
		protected internal abstract void EmitStartMethodInvoke(string methodName);

		// Token: 0x06000142 RID: 322 RVA: 0x00005134 File Offset: 0x00003334
		protected internal virtual void EmitStartMethodInvoke(string methodName, params string[] genericArguments)
		{
			this.EmitStartMethodInvoke(methodName);
		}

		// Token: 0x06000143 RID: 323
		protected internal abstract void EmitEndLambdaDelegate();

		// Token: 0x06000144 RID: 324
		protected internal abstract void EmitEndLambdaExpression();

		// Token: 0x06000145 RID: 325
		protected internal abstract void EmitEndConstructor();

		// Token: 0x06000146 RID: 326
		protected internal abstract void EmitEndMethodInvoke();

		// Token: 0x06000147 RID: 327 RVA: 0x0000513D File Offset: 0x0000333D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._writer != null)
			{
				this._writer.Dispose();
			}
		}

		// Token: 0x04000059 RID: 89
		private StringWriter _writer;

		// Token: 0x02000023 RID: 35
		private enum WriterMode
		{
			// Token: 0x0400005B RID: 91
			Constructor,
			// Token: 0x0400005C RID: 92
			MethodCall,
			// Token: 0x0400005D RID: 93
			LambdaDelegate,
			// Token: 0x0400005E RID: 94
			LambdaExpression
		}
	}
}
