using System;
using System.Globalization;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000026 RID: 38
	internal class CSharpCodeWriter : BaseCodeWriter
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00005395 File Offset: 0x00003595
		protected internal override void WriteStartGenerics()
		{
			base.InnerWriter.Write("<");
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000053A7 File Offset: 0x000035A7
		protected internal override void WriteEndGenerics()
		{
			base.InnerWriter.Write(">");
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000053BC File Offset: 0x000035BC
		public override int WriteVariableDeclaration(string type, string name, string value)
		{
			base.InnerWriter.Write(type);
			base.InnerWriter.Write(" ");
			base.InnerWriter.Write(name);
			if (!string.IsNullOrEmpty(value))
			{
				base.InnerWriter.Write(" = ");
				base.InnerWriter.Write(value);
			}
			else
			{
				base.InnerWriter.Write(" = null");
			}
			return 0;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005428 File Offset: 0x00003628
		public override void WriteDisableUnusedFieldWarningPragma()
		{
			base.InnerWriter.Write("#pragma warning disable 219");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000543A File Offset: 0x0000363A
		public override void WriteRestoreUnusedFieldWarningPragma()
		{
			base.InnerWriter.Write("#pragma warning restore 219");
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000544C File Offset: 0x0000364C
		public override void WriteStringLiteral(string literal)
		{
			if (literal == null)
			{
				throw new ArgumentNullException("literal");
			}
			if (literal.Length >= 256 && literal.Length <= 1500 && literal.IndexOf('\0') == -1)
			{
				this.WriteVerbatimStringLiteral(literal);
				return;
			}
			this.WriteCStyleStringLiteral(literal);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000549C File Offset: 0x0000369C
		private void WriteVerbatimStringLiteral(string literal)
		{
			base.InnerWriter.Write("@\"");
			for (int i = 0; i < literal.Length; i++)
			{
				if (literal[i] == '"')
				{
					base.InnerWriter.Write("\"\"");
				}
				else
				{
					base.InnerWriter.Write(literal[i]);
				}
			}
			base.InnerWriter.Write("\"");
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000550C File Offset: 0x0000370C
		private void WriteCStyleStringLiteral(string literal)
		{
			base.InnerWriter.Write("\"");
			int i = 0;
			while (i < literal.Length)
			{
				char c = literal[i];
				if (c <= '"')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							base.InnerWriter.Write("\\t");
							break;
						case '\n':
							base.InnerWriter.Write("\\n");
							break;
						case '\v':
						case '\f':
							goto IL_132;
						case '\r':
							base.InnerWriter.Write("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_132;
							}
							base.InnerWriter.Write("\\\"");
							break;
						}
					}
					else
					{
						base.InnerWriter.Write("\\\0");
					}
				}
				else if (c != '\'')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case '\u2028':
						case '\u2029':
							base.InnerWriter.Write("\\u");
							base.InnerWriter.Write(((int)literal[i]).ToString("X4", CultureInfo.InvariantCulture));
							break;
						default:
							goto IL_132;
						}
					}
					else
					{
						base.InnerWriter.Write("\\\\");
					}
				}
				else
				{
					base.InnerWriter.Write("\\'");
				}
				IL_144:
				if (i > 0 && i % 80 == 0)
				{
					if (char.IsHighSurrogate(literal[i]) && i < literal.Length - 1 && char.IsLowSurrogate(literal[i + 1]))
					{
						base.InnerWriter.Write(literal[++i]);
					}
					base.InnerWriter.Write("\" +");
					base.InnerWriter.Write(Environment.NewLine);
					base.InnerWriter.Write('"');
				}
				i++;
				continue;
				IL_132:
				base.InnerWriter.Write(literal[i]);
				goto IL_144;
			}
			base.InnerWriter.Write("\"");
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000056F3 File Offset: 0x000038F3
		public override void WriteEndStatement()
		{
			base.InnerWriter.WriteLine(";");
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005705 File Offset: 0x00003905
		public override void WriteIdentifier(string identifier)
		{
			base.InnerWriter.Write("@" + identifier);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000571D File Offset: 0x0000391D
		public override void WriteBooleanLiteral(bool value)
		{
			this.WriteSnippet(value.ToString().ToLowerInvariant());
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005734 File Offset: 0x00003934
		protected internal override void EmitStartLambdaExpression(string[] parameterNames)
		{
			if (parameterNames == null)
			{
				throw new ArgumentNullException("parameterNames");
			}
			if (parameterNames.Length == 0 || parameterNames.Length > 1)
			{
				base.InnerWriter.Write("(");
			}
			base.WriteCommaSeparatedList<string>(parameterNames, new Action<string>(base.InnerWriter.Write));
			if (parameterNames.Length == 0 || parameterNames.Length > 1)
			{
				base.InnerWriter.Write(")");
			}
			base.InnerWriter.Write(" => ");
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000057AE File Offset: 0x000039AE
		protected internal override void EmitStartLambdaDelegate(string[] parameterNames)
		{
			if (parameterNames == null)
			{
				throw new ArgumentNullException("parameterNames");
			}
			this.EmitStartLambdaExpression(parameterNames);
			base.InnerWriter.WriteLine("{");
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000057D5 File Offset: 0x000039D5
		protected internal override void EmitEndLambdaDelegate()
		{
			base.InnerWriter.Write("}");
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000057E7 File Offset: 0x000039E7
		protected internal override void EmitStartConstructor(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			base.InnerWriter.Write("new ");
			base.InnerWriter.Write(typeName);
			base.InnerWriter.Write("(");
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005823 File Offset: 0x00003A23
		public override void WriteReturn()
		{
			base.InnerWriter.Write("return ");
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005838 File Offset: 0x00003A38
		public override void WriteLinePragma(int? lineNumber, string fileName)
		{
			base.InnerWriter.WriteLine();
			if (lineNumber != null)
			{
				base.InnerWriter.Write("#line ");
				base.InnerWriter.Write(lineNumber);
				base.InnerWriter.Write(" \"");
				base.InnerWriter.Write(fileName);
				base.InnerWriter.Write("\"");
				base.InnerWriter.WriteLine();
				return;
			}
			base.InnerWriter.WriteLine("#line default");
			base.InnerWriter.WriteLine("#line hidden");
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000058D2 File Offset: 0x00003AD2
		public override void WriteHiddenLinePragma()
		{
			base.InnerWriter.WriteLine("#line hidden");
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000058E4 File Offset: 0x00003AE4
		public override void WriteHelperHeaderPrefix(string templateTypeName, bool isStatic)
		{
			base.InnerWriter.Write("public ");
			if (isStatic)
			{
				base.InnerWriter.Write("static ");
			}
			base.InnerWriter.Write(templateTypeName);
			base.InnerWriter.Write(" ");
		}
	}
}
