using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200009F RID: 159
	internal sealed class JSKeyword
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x0002AE52 File Offset: 0x00029052
		private JSKeyword(JSToken token, string name) : this(token, name, null)
		{
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002AE5D File Offset: 0x0002905D
		private JSKeyword(JSToken token, string name, JSKeyword next)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_length = this.m_name.Length;
			this.m_next = next;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002AE8C File Offset: 0x0002908C
		internal static string CanBeIdentifier(JSToken keyword)
		{
			switch (keyword)
			{
			case JSToken.Super:
				return "super";
			case JSToken.Module:
				return "module";
			case JSToken.Let:
				return "let";
			case JSToken.Implements:
				return "implements";
			case JSToken.Interface:
				return "interface";
			case JSToken.Package:
				return "package";
			case JSToken.Private:
				return "private";
			case JSToken.Protected:
				return "protected";
			case JSToken.Public:
				return "public";
			case JSToken.Static:
				return "static";
			case JSToken.Yield:
				return "yield";
			case JSToken.Native:
				return "native";
			case JSToken.Get:
				return "get";
			case JSToken.Set:
				return "set";
			}
			return null;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002AF44 File Offset: 0x00029144
		internal JSToken GetKeyword(string source, int startPosition, int wordLength)
		{
			for (JSKeyword jskeyword = this; jskeyword != null; jskeyword = jskeyword.m_next)
			{
				if (wordLength == jskeyword.m_length)
				{
					int num = string.CompareOrdinal(jskeyword.m_name, 0, source, startPosition, wordLength);
					if (num == 0)
					{
						return jskeyword.m_token;
					}
					if (num > 0)
					{
						return JSToken.Identifier;
					}
				}
				else if (wordLength < jskeyword.m_length)
				{
					return JSToken.Identifier;
				}
			}
			return JSToken.Identifier;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002AF98 File Offset: 0x00029198
		internal static JSKeyword[] InitKeywords()
		{
			JSKeyword[] array = new JSKeyword[26];
			array[1] = new JSKeyword(JSToken.Break, "break");
			array[2] = new JSKeyword(JSToken.Case, "case", new JSKeyword(JSToken.Catch, "catch", new JSKeyword(JSToken.Class, "class", new JSKeyword(JSToken.Const, "const", new JSKeyword(JSToken.Continue, "continue")))));
			array[3] = new JSKeyword(JSToken.Do, "do", new JSKeyword(JSToken.FirstOperator, "delete", new JSKeyword(JSToken.Default, "default", new JSKeyword(JSToken.Debugger, "debugger"))));
			array[4] = new JSKeyword(JSToken.Else, "else", new JSKeyword(JSToken.Enum, "enum", new JSKeyword(JSToken.Export, "export", new JSKeyword(JSToken.Extends, "extends"))));
			array[5] = new JSKeyword(JSToken.For, "for", new JSKeyword(JSToken.False, "false", new JSKeyword(JSToken.Finally, "finally", new JSKeyword(JSToken.Function, "function"))));
			array[6] = new JSKeyword(JSToken.Get, "get");
			array[8] = new JSKeyword(JSToken.If, "if", new JSKeyword(JSToken.In, "in", new JSKeyword(JSToken.Import, "import", new JSKeyword(JSToken.Interface, "interface", new JSKeyword(JSToken.Implements, "implements", new JSKeyword(JSToken.InstanceOf, "instanceof"))))));
			array[11] = new JSKeyword(JSToken.Let, "let");
			array[13] = new JSKeyword(JSToken.New, "new", new JSKeyword(JSToken.Null, "null", new JSKeyword(JSToken.Native, "native")));
			array[15] = new JSKeyword(JSToken.Public, "public", new JSKeyword(JSToken.Package, "package", new JSKeyword(JSToken.Private, "private", new JSKeyword(JSToken.Protected, "protected"))));
			array[17] = new JSKeyword(JSToken.Return, "return");
			array[18] = new JSKeyword(JSToken.Set, "set", new JSKeyword(JSToken.Super, "super", new JSKeyword(JSToken.Static, "static", new JSKeyword(JSToken.Switch, "switch"))));
			array[19] = new JSKeyword(JSToken.Try, "try", new JSKeyword(JSToken.This, "this", new JSKeyword(JSToken.True, "true", new JSKeyword(JSToken.Throw, "throw", new JSKeyword(JSToken.TypeOf, "typeof")))));
			array[21] = new JSKeyword(JSToken.Var, "var", new JSKeyword(JSToken.Void, "void"));
			array[22] = new JSKeyword(JSToken.With, "with", new JSKeyword(JSToken.While, "while"));
			array[24] = new JSKeyword(JSToken.Yield, "yield");
			return array;
		}

		// Token: 0x040003CB RID: 971
		private JSKeyword m_next;

		// Token: 0x040003CC RID: 972
		private JSToken m_token;

		// Token: 0x040003CD RID: 973
		private string m_name;

		// Token: 0x040003CE RID: 974
		private int m_length;
	}
}
