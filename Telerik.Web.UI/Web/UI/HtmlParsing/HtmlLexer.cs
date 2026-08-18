using System;

namespace Telerik.Web.UI.HtmlParsing
{
	// Token: 0x020011DA RID: 4570
	internal class HtmlLexer
	{
		// Token: 0x0600BCB4 RID: 48308 RVA: 0x0029DCE4 File Offset: 0x0029BEE4
		internal HtmlLexer()
		{
		}

		// Token: 0x17003CEB RID: 15595
		// (set) Token: 0x0600BCB5 RID: 48309 RVA: 0x0029DCEC File Offset: 0x0029BEEC
		internal string Text
		{
			set
			{
				this.text = value;
			}
		}

		// Token: 0x0600BCB6 RID: 48310 RVA: 0x0029DCF5 File Offset: 0x0029BEF5
		internal bool HasTokens()
		{
			return this.currentPosition < this.text.Length || (this.thisTag != null && !string.IsNullOrEmpty(this.thisTag.Contents));
		}

		// Token: 0x0600BCB7 RID: 48311 RVA: 0x0029DD2C File Offset: 0x0029BF2C
		private Token Pending(Token current)
		{
			if (this.thisTag != null && !string.IsNullOrEmpty(this.thisTag.Contents))
			{
				Token result = this.thisTag;
				this.thisTag = null;
				return result;
			}
			return current;
		}

		// Token: 0x0600BCB8 RID: 48312 RVA: 0x0029DD68 File Offset: 0x0029BF68
		private Token TagStart(Token current)
		{
			if (this.text[this.currentPosition] == '<' && this.currentPosition + 2 < this.text.Length && (char.IsLetter(this.text[this.currentPosition + 1]) || this.text[this.currentPosition + 1] == '!' || this.text[this.currentPosition + 1] == '/' || this.text[this.currentPosition + 1] == '?'))
			{
				current.Type = TokenType.TagStart;
				this.thisTag = new Token();
				this.thisTag.Offset = this.currentPosition;
				if (this.text[this.currentPosition + 1] == '/')
				{
					this.thisTag.Type = TokenType.CloseTag;
				}
				else
				{
					this.thisTag.Type = TokenType.OpenTag;
				}
			}
			return current;
		}

		// Token: 0x0600BCB9 RID: 48313 RVA: 0x0029DE5C File Offset: 0x0029C05C
		private Token TagEnd(Token current)
		{
			if (this.text[this.currentPosition] == '>')
			{
				if (this.thisTag != null)
				{
					this.thisTag.Contents = this.text.Substring(this.thisTag.Offset, this.currentPosition - this.thisTag.Offset + 1);
				}
				current.Type = TokenType.TagEnd;
			}
			return current;
		}

		// Token: 0x0600BCBA RID: 48314 RVA: 0x0029DEC4 File Offset: 0x0029C0C4
		private Token HandleTags(Token current)
		{
			if (current == null)
			{
				current = new Token();
				current.Contents = this.text[this.currentPosition].ToString();
				current.Offset = this.currentPosition;
				current = this.TagStart(current);
				current = this.TagEnd(current);
			}
			return current;
		}

		// Token: 0x0600BCBB RID: 48315 RVA: 0x0029DF1C File Offset: 0x0029C11C
		internal Token NextToken()
		{
			Token token = null;
			token = this.Pending(token);
			if (token != null)
			{
				return token;
			}
			token = this.HandleEntity(this.currentPosition);
			token = this.HandleTags(token);
			this.currentPosition += token.Contents.Length;
			return token;
		}

		// Token: 0x0600BCBC RID: 48316 RVA: 0x0029DF68 File Offset: 0x0029C168
		internal Token HandleEntity(int position)
		{
			checked
			{
				if (this.text[position] == '&' && position + 3 < this.text.Length)
				{
					if (char.IsLetter(this.text[position + 1]))
					{
						return this.FinishEntity(position, position + 1, new HtmlLexer.CharacterCondition(char.IsLetter));
					}
					if (this.text[position + 1] == '#')
					{
						return this.FinishEntity(position, position + 2, new HtmlLexer.CharacterCondition(char.IsDigit));
					}
				}
				return null;
			}
		}

		// Token: 0x0600BCBD RID: 48317 RVA: 0x0029DFEC File Offset: 0x0029C1EC
		private Token FinishEntity(int position, int searchFrom, HtmlLexer.CharacterCondition condition)
		{
			int num = this.SkipNext(searchFrom, condition);
			if (this.AtEntityEnd(num))
			{
				return this.MarkEntity(position, num + 1);
			}
			return null;
		}

		// Token: 0x0600BCBE RID: 48318 RVA: 0x0029E018 File Offset: 0x0029C218
		internal Token MarkEntity(int start, int end)
		{
			string contents = this.text.Substring(start, end - start);
			return new Token
			{
				Type = TokenType.HtmlEntity,
				Contents = contents,
				Offset = start
			};
		}

		// Token: 0x0600BCBF RID: 48319 RVA: 0x0029E051 File Offset: 0x0029C251
		private bool AtEntityEnd(int position)
		{
			return position < this.text.Length && this.text[position] == ';';
		}

		// Token: 0x0600BCC0 RID: 48320 RVA: 0x0029E074 File Offset: 0x0029C274
		private int SkipNext(int start, HtmlLexer.CharacterCondition condition)
		{
			int num = start;
			while (num < this.text.Length && condition(this.text[num]))
			{
				num++;
			}
			return num;
		}

		// Token: 0x04003197 RID: 12695
		private int currentPosition;

		// Token: 0x04003198 RID: 12696
		private string text;

		// Token: 0x04003199 RID: 12697
		private Token thisTag;

		// Token: 0x020011DB RID: 4571
		// (Invoke) Token: 0x0600BCC2 RID: 48322
		private delegate bool CharacterCondition(char target);
	}
}
