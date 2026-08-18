using System;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200001F RID: 31
	internal static class CodeGeneratorPaddingHelper
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004438 File Offset: 0x00002638
		public static int PaddingCharCount(RazorEngineHost host, Span target, int generatedStart)
		{
			int num = CodeGeneratorPaddingHelper.CalculatePadding(host, target, generatedStart);
			if (host.DesignTimeMode && host.IsIndentingWithTabs)
			{
				int num3;
				int num2 = Math.DivRem(num, host.TabSize, out num3);
				return num2 + num3;
			}
			return num;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004474 File Offset: 0x00002674
		public static string PadStatement(RazorEngineHost host, string code, Span target, ref int startGeneratedCode, out int paddingCharCount)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			int num = CodeGeneratorPaddingHelper.CalculatePadding(host, target, 0);
			if (host.DesignTimeMode && num > 0 && target.Previous.Kind == SpanKind.Transition && string.Equals(target.Previous.Content, SyntaxConstants.TransitionString))
			{
				num--;
				startGeneratedCode--;
			}
			return CodeGeneratorPaddingHelper.PadInternal(host, code, num, out paddingCharCount);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000044EC File Offset: 0x000026EC
		public static string Pad(RazorEngineHost host, string code, Span target, out int paddingCharCount)
		{
			int padding = CodeGeneratorPaddingHelper.CalculatePadding(host, target, 0);
			return CodeGeneratorPaddingHelper.PadInternal(host, code, padding, out paddingCharCount);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000450C File Offset: 0x0000270C
		public static string Pad(RazorEngineHost host, string code, Span target, int generatedStart, out int paddingCharCount)
		{
			int padding = CodeGeneratorPaddingHelper.CalculatePadding(host, target, generatedStart);
			return CodeGeneratorPaddingHelper.PadInternal(host, code, padding, out paddingCharCount);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000452C File Offset: 0x0000272C
		internal static int CalculatePadding(RazorEngineHost host, Span target, int generatedStart)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			int num = CodeGeneratorPaddingHelper.CollectSpacesAndTabs(target, host.TabSize) - generatedStart;
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000456C File Offset: 0x0000276C
		private static string PadInternal(RazorEngineHost host, string code, int padding, out int paddingCharCount)
		{
			if (host.DesignTimeMode && host.IsIndentingWithTabs)
			{
				int num2;
				int num = Math.DivRem(padding, host.TabSize, out num2);
				paddingCharCount = num + num2;
				return new string('\t', num) + new string(' ', num2) + code;
			}
			paddingCharCount = padding;
			return code.PadLeft(padding + code.Length, ' ');
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000045C8 File Offset: 0x000027C8
		private static int CollectSpacesAndTabs(Span target, int tabSize)
		{
			Span span = target;
			string text = null;
			while (span.Previous != null)
			{
				string text2 = span.Previous.Content ?? string.Empty;
				int num = text2.LastIndexOfAny(CodeGeneratorPaddingHelper._newLineChars);
				if (num < 0)
				{
					span = span.Previous;
				}
				else
				{
					if (num != text2.Length - 1)
					{
						span = span.Previous;
						text = text2.Substring(num + 1);
						break;
					}
					break;
				}
			}
			Span span2 = span;
			if (text == null)
			{
				text = span2.Content;
			}
			int num2 = 0;
			while (span2 != target)
			{
				if (text != null)
				{
					for (int i = 0; i < text.Length; i++)
					{
						if (text[i] == '\t')
						{
							num2 += tabSize - num2 % tabSize;
						}
						else
						{
							num2++;
						}
					}
				}
				span2 = span2.Next;
				text = span2.Content;
			}
			return num2;
		}

		// Token: 0x04000041 RID: 65
		private static readonly char[] _newLineChars = new char[]
		{
			'\r',
			'\n'
		};
	}
}
