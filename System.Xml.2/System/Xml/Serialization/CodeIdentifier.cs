using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	// Token: 0x02000138 RID: 312
	public class CodeIdentifier
	{
		// Token: 0x060016AC RID: 5804 RVA: 0x00063F59 File Offset: 0x00062159
		[Obsolete("This class should never get constructed as it contains only static methods.")]
		public CodeIdentifier()
		{
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00063F64 File Offset: 0x00062164
		public static string MakePascal(string identifier)
		{
			identifier = CodeIdentifier.MakeValid(identifier);
			if (identifier.Length <= 2)
			{
				return identifier.ToUpper(CultureInfo.InvariantCulture);
			}
			if (char.IsLower(identifier[0]))
			{
				return char.ToUpper(identifier[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + identifier.Substring(1);
			}
			return identifier;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00063FC8 File Offset: 0x000621C8
		public static string MakeCamel(string identifier)
		{
			identifier = CodeIdentifier.MakeValid(identifier);
			if (identifier.Length <= 2)
			{
				return identifier.ToLower(CultureInfo.InvariantCulture);
			}
			if (char.IsUpper(identifier[0]))
			{
				return char.ToLower(identifier[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + identifier.Substring(1);
			}
			return identifier;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0006402C File Offset: 0x0006222C
		public static string MakeValid(string identifier)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < identifier.Length && stringBuilder.Length < 511)
			{
				char c = identifier[num];
				if (CodeIdentifier.IsValid(c))
				{
					if (stringBuilder.Length == 0 && !CodeIdentifier.IsValidStart(c))
					{
						stringBuilder.Append("Item");
					}
					stringBuilder.Append(c);
				}
				num++;
			}
			if (stringBuilder.Length == 0)
			{
				return "Item";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000640A5 File Offset: 0x000622A5
		internal static string MakeValidInternal(string identifier)
		{
			if (identifier.Length > 30)
			{
				return "Item";
			}
			return CodeIdentifier.MakeValid(identifier);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000640BD File Offset: 0x000622BD
		private static bool IsValidStart(char c)
		{
			return char.GetUnicodeCategory(c) != UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000640CC File Offset: 0x000622CC
		private static bool IsValid(char c)
		{
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.ConnectorPunctuation:
				return true;
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.LetterNumber:
			case UnicodeCategory.OtherNumber:
			case UnicodeCategory.SpaceSeparator:
			case UnicodeCategory.LineSeparator:
			case UnicodeCategory.ParagraphSeparator:
			case UnicodeCategory.Control:
			case UnicodeCategory.Format:
			case UnicodeCategory.Surrogate:
			case UnicodeCategory.PrivateUse:
			case UnicodeCategory.DashPunctuation:
			case UnicodeCategory.OpenPunctuation:
			case UnicodeCategory.ClosePunctuation:
			case UnicodeCategory.InitialQuotePunctuation:
			case UnicodeCategory.FinalQuotePunctuation:
			case UnicodeCategory.OtherPunctuation:
			case UnicodeCategory.MathSymbol:
			case UnicodeCategory.CurrencySymbol:
			case UnicodeCategory.ModifierSymbol:
			case UnicodeCategory.OtherSymbol:
			case UnicodeCategory.OtherNotAssigned:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00064165 File Offset: 0x00062365
		internal static void CheckValidIdentifier(string ident)
		{
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(ident))
			{
				throw new ArgumentException(Res.GetString("XmlInvalidIdentifier", new object[]
				{
					ident
				}), "ident");
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0006418E File Offset: 0x0006238E
		internal static string GetCSharpName(string name)
		{
			return CodeIdentifier.EscapeKeywords(name.Replace('+', '.'), CodeIdentifier.csharp);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000641A4 File Offset: 0x000623A4
		private static int GetCSharpName(Type t, Type[] parameters, int index, StringBuilder sb)
		{
			if (t.DeclaringType != null && t.DeclaringType != t)
			{
				index = CodeIdentifier.GetCSharpName(t.DeclaringType, parameters, index, sb);
				sb.Append(".");
			}
			string name = t.Name;
			int num = name.IndexOf('`');
			if (num < 0)
			{
				num = name.IndexOf('!');
			}
			if (num > 0)
			{
				CodeIdentifier.EscapeKeywords(name.Substring(0, num), CodeIdentifier.csharp, sb);
				sb.Append("<");
				int num2 = int.Parse(name.Substring(num + 1), CultureInfo.InvariantCulture) + index;
				while (index < num2)
				{
					sb.Append(CodeIdentifier.GetCSharpName(parameters[index]));
					if (index < num2 - 1)
					{
						sb.Append(",");
					}
					index++;
				}
				sb.Append(">");
			}
			else
			{
				CodeIdentifier.EscapeKeywords(name, CodeIdentifier.csharp, sb);
			}
			return index;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00064288 File Offset: 0x00062488
		internal static string GetCSharpName(Type t)
		{
			int num = 0;
			while (t.IsArray)
			{
				t = t.GetElementType();
				num++;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("global::");
			string @namespace = t.Namespace;
			if (@namespace != null && @namespace.Length > 0)
			{
				string[] array = @namespace.Split(new char[]
				{
					'.'
				});
				for (int i = 0; i < array.Length; i++)
				{
					CodeIdentifier.EscapeKeywords(array[i], CodeIdentifier.csharp, stringBuilder);
					stringBuilder.Append(".");
				}
			}
			Type[] parameters = (t.IsGenericType || t.ContainsGenericParameters) ? t.GetGenericArguments() : new Type[0];
			CodeIdentifier.GetCSharpName(t, parameters, 0, stringBuilder);
			for (int j = 0; j < num; j++)
			{
				stringBuilder.Append("[]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00064360 File Offset: 0x00062560
		private static void EscapeKeywords(string identifier, CodeDomProvider codeProvider, StringBuilder sb)
		{
			if (identifier == null || identifier.Length == 0)
			{
				return;
			}
			int num = 0;
			while (identifier.EndsWith("[]", StringComparison.Ordinal))
			{
				num++;
				identifier = identifier.Substring(0, identifier.Length - 2);
			}
			if (identifier.Length > 0)
			{
				CodeIdentifier.CheckValidIdentifier(identifier);
				identifier = codeProvider.CreateEscapedIdentifier(identifier);
				sb.Append(identifier);
			}
			for (int i = 0; i < num; i++)
			{
				sb.Append("[]");
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000643DC File Offset: 0x000625DC
		private static string EscapeKeywords(string identifier, CodeDomProvider codeProvider)
		{
			if (identifier == null || identifier.Length == 0)
			{
				return identifier;
			}
			string[] array = identifier.Split(new char[]
			{
				'.',
				',',
				'<',
				'>'
			});
			StringBuilder stringBuilder = new StringBuilder();
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (num >= 0)
				{
					stringBuilder.Append(identifier.Substring(num, 1));
				}
				num++;
				num += array[i].Length;
				string identifier2 = array[i].Trim();
				CodeIdentifier.EscapeKeywords(identifier2, codeProvider, stringBuilder);
			}
			if (stringBuilder.Length != identifier.Length)
			{
				return stringBuilder.ToString();
			}
			return identifier;
		}

		// Token: 0x04000A98 RID: 2712
		internal static CodeDomProvider csharp = new CSharpCodeProvider();

		// Token: 0x04000A99 RID: 2713
		internal const int MaxIdentifierLength = 511;
	}
}
