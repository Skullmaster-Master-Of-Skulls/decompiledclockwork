using System;
using System.Text;

namespace System.Web.Razor.Text
{
	// Token: 0x02000068 RID: 104
	internal static class TextExtensions
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0001269D File Offset: 0x0001089D
		public static void Seek(this ITextBuffer self, int characters)
		{
			self.Position += characters;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000126B0 File Offset: 0x000108B0
		public static ITextDocument ToDocument(this ITextBuffer self)
		{
			ITextDocument textDocument = self as ITextDocument;
			if (textDocument == null)
			{
				textDocument = new SeekableTextReader(self);
			}
			return textDocument;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000126EC File Offset: 0x000108EC
		public static LookaheadToken BeginLookahead(this ITextBuffer self)
		{
			int start = self.Position;
			return new LookaheadToken(delegate()
			{
				self.Position = start;
			});
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00012728 File Offset: 0x00010928
		public static string ReadToEnd(this ITextBuffer self)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = self.Read()) != -1)
			{
				stringBuilder.Append((char)num);
			}
			return stringBuilder.ToString();
		}
	}
}
