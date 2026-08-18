using System;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200004C RID: 76
	internal static class TextReaderExtensions
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000E086 File Offset: 0x0000C286
		public static string ReadUntil(this TextReader reader, char terminator)
		{
			return reader.ReadUntil(terminator, false);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		public static string ReadUntil(this TextReader reader, char terminator, bool inclusive)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return reader.ReadUntil((char c) => c == terminator, inclusive);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000E0DF File Offset: 0x0000C2DF
		public static string ReadUntil(this TextReader reader, params char[] terminators)
		{
			return reader.ReadUntil(false, terminators);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000E13C File Offset: 0x0000C33C
		public static string ReadUntil(this TextReader reader, bool inclusive, params char[] terminators)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (terminators == null)
			{
				throw new ArgumentNullException("terminators");
			}
			return reader.ReadUntil((char c) => terminators.Any((char tc) => tc == c), inclusive);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000E18A File Offset: 0x0000C38A
		public static string ReadUntil(this TextReader reader, Predicate<char> condition)
		{
			return reader.ReadUntil(condition, false);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000E194 File Offset: 0x0000C394
		public static string ReadUntil(this TextReader reader, Predicate<char> condition, bool inclusive)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = reader.Peek()) != -1 && !condition((char)num))
			{
				reader.Read();
				stringBuilder.Append((char)num);
			}
			if (inclusive && reader.Peek() != -1)
			{
				stringBuilder.Append((char)reader.Read());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000E20C File Offset: 0x0000C40C
		public static string ReadWhile(this TextReader reader, Predicate<char> condition)
		{
			return reader.ReadWhile(condition, false);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000E230 File Offset: 0x0000C430
		public static string ReadWhile(this TextReader reader, Predicate<char> condition, bool inclusive)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			return reader.ReadUntil((char ch) => !condition(ch), inclusive);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000E286 File Offset: 0x0000C486
		public static string ReadWhiteSpace(this TextReader reader)
		{
			return reader.ReadWhile((char c) => char.IsWhiteSpace(c));
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000E2B3 File Offset: 0x0000C4B3
		public static string ReadUntilWhiteSpace(this TextReader reader)
		{
			return reader.ReadUntil((char c) => char.IsWhiteSpace(c));
		}
	}
}
