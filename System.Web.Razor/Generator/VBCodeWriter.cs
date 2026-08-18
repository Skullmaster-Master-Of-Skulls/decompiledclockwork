using System;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000033 RID: 51
	internal class VBCodeWriter : BaseCodeWriter
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006F19 File Offset: 0x00005119
		public override bool SupportsMidStatementLinePragmas
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006F1C File Offset: 0x0000511C
		protected internal override void WriteStartGenerics()
		{
			base.InnerWriter.Write("(Of ");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006F2E File Offset: 0x0000512E
		protected internal override void WriteEndGenerics()
		{
			base.InnerWriter.Write(")");
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006F40 File Offset: 0x00005140
		public override void WriteLineContinuation()
		{
			base.InnerWriter.Write(" _");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006F54 File Offset: 0x00005154
		public override int WriteVariableDeclaration(string type, string name, string value)
		{
			base.InnerWriter.Write("Dim ");
			base.InnerWriter.Write(name);
			base.InnerWriter.Write(" As ");
			int length = base.InnerWriter.GetStringBuilder().Length;
			base.InnerWriter.Write(type);
			if (!string.IsNullOrEmpty(value))
			{
				base.InnerWriter.Write(" = ");
				base.InnerWriter.Write(value);
			}
			else
			{
				base.InnerWriter.Write(" = Nothing");
			}
			return length;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006FE4 File Offset: 0x000051E4
		public override void WriteStringLiteral(string literal)
		{
			bool flag = true;
			base.InnerWriter.Write("\"");
			int i = 0;
			while (i < literal.Length)
			{
				char c = literal[i];
				if (c <= '"')
				{
					if (c == '\0')
					{
						goto IL_83;
					}
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						goto IL_83;
					case '\v':
					case '\f':
						goto IL_D8;
					default:
						if (c != '"')
						{
							goto IL_D8;
						}
						goto IL_AA;
					}
				}
				else
				{
					switch (c)
					{
					case '“':
					case '”':
						goto IL_AA;
					default:
						switch (c)
						{
						case '\u2028':
						case '\u2029':
							goto IL_83;
						default:
							if (c != '＂')
							{
								goto IL_D8;
							}
							goto IL_AA;
						}
						break;
					}
				}
				IL_F2:
				if (i > 0 && i % 80 == 0)
				{
					if (char.IsHighSurrogate(literal[i]) && i < literal.Length - 1 && char.IsLowSurrogate(literal[i + 1]))
					{
						base.InnerWriter.Write(literal[++i]);
					}
					if (flag)
					{
						base.InnerWriter.Write("\"");
					}
					flag = true;
					base.InnerWriter.Write("& _ ");
					base.InnerWriter.Write(Environment.NewLine);
					base.InnerWriter.Write('"');
				}
				i++;
				continue;
				IL_83:
				this.EnsureOutOfQuotes(ref flag);
				base.InnerWriter.Write("&");
				this.WriteCharLiteral(literal[i]);
				goto IL_F2;
				IL_AA:
				this.EnsureInQuotes(ref flag);
				base.InnerWriter.Write(literal[i]);
				base.InnerWriter.Write(literal[i]);
				goto IL_F2;
				IL_D8:
				this.EnsureInQuotes(ref flag);
				base.InnerWriter.Write(literal[i]);
				goto IL_F2;
			}
			this.EnsureOutOfQuotes(ref flag);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000718C File Offset: 0x0000538C
		protected internal override void EmitStartLambdaExpression(string[] parameterNames)
		{
			base.InnerWriter.Write("Function (");
			base.WriteCommaSeparatedList<string>(parameterNames, new Action<string>(base.InnerWriter.Write));
			base.InnerWriter.Write(") ");
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000071C7 File Offset: 0x000053C7
		protected internal override void EmitStartConstructor(string typeName)
		{
			base.InnerWriter.Write("New ");
			base.InnerWriter.Write(typeName);
			base.InnerWriter.Write("(");
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000071F5 File Offset: 0x000053F5
		protected internal override void EmitStartLambdaDelegate(string[] parameterNames)
		{
			base.InnerWriter.Write("Sub (");
			base.WriteCommaSeparatedList<string>(parameterNames, new Action<string>(base.InnerWriter.Write));
			base.InnerWriter.WriteLine(")");
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007230 File Offset: 0x00005430
		protected internal override void EmitEndLambdaDelegate()
		{
			base.InnerWriter.Write("End Sub");
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007242 File Offset: 0x00005442
		private void WriteCharLiteral(char literal)
		{
			base.InnerWriter.Write("Global.Microsoft.VisualBasic.ChrW(");
			base.InnerWriter.Write((int)literal);
			base.InnerWriter.Write(")");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007270 File Offset: 0x00005470
		private void EnsureInQuotes(ref bool inQuotes)
		{
			if (!inQuotes)
			{
				base.InnerWriter.Write("&\"");
				inQuotes = true;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007289 File Offset: 0x00005489
		private void EnsureOutOfQuotes(ref bool inQuotes)
		{
			if (inQuotes)
			{
				base.InnerWriter.Write("\"");
				inQuotes = false;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000072A2 File Offset: 0x000054A2
		public override void WriteReturn()
		{
			base.InnerWriter.Write("Return ");
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000072B4 File Offset: 0x000054B4
		public override void WriteLinePragma(int? lineNumber, string fileName)
		{
			base.InnerWriter.WriteLine();
			if (lineNumber != null)
			{
				base.InnerWriter.Write("#ExternalSource(\"");
				base.InnerWriter.Write(fileName);
				base.InnerWriter.Write("\", ");
				base.InnerWriter.Write(lineNumber);
				base.InnerWriter.WriteLine(")");
				return;
			}
			base.InnerWriter.WriteLine("#End ExternalSource");
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007333 File Offset: 0x00005533
		public override void WriteHelperHeaderPrefix(string templateTypeName, bool isStatic)
		{
			base.InnerWriter.Write("Public ");
			if (isStatic)
			{
				base.InnerWriter.Write("Shared ");
			}
			base.InnerWriter.Write("Function ");
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007368 File Offset: 0x00005568
		public override void WriteHelperHeaderSuffix(string templateTypeName)
		{
			base.InnerWriter.Write(" As ");
			base.InnerWriter.WriteLine(templateTypeName);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007386 File Offset: 0x00005586
		public override void WriteHelperTrailer()
		{
			base.InnerWriter.WriteLine("End Function");
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007398 File Offset: 0x00005598
		public override void WriteEndStatement()
		{
			base.InnerWriter.WriteLine();
		}
	}
}
