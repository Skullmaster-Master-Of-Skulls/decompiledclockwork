using System;
using System.Text;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200005A RID: 90
	public class TreePatternLexer
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0000AE09 File Offset: 0x00009009
		public TreePatternLexer(string pattern)
		{
			this.pattern = pattern;
			this.n = pattern.Length;
			this.Consume();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000AE3C File Offset: 0x0000903C
		public virtual int NextToken()
		{
			this.sval.Length = 0;
			while (this.c != -1)
			{
				if (this.c == 32 || this.c == 10 || this.c == 13 || this.c == 9)
				{
					this.Consume();
				}
				else
				{
					if ((this.c >= 97 && this.c <= 122) || (this.c >= 65 && this.c <= 90) || this.c == 95)
					{
						this.sval.Append((char)this.c);
						this.Consume();
						while ((this.c >= 97 && this.c <= 122) || (this.c >= 65 && this.c <= 90) || (this.c >= 48 && this.c <= 57) || this.c == 95)
						{
							this.sval.Append((char)this.c);
							this.Consume();
						}
						return 3;
					}
					if (this.c == 40)
					{
						this.Consume();
						return 1;
					}
					if (this.c == 41)
					{
						this.Consume();
						return 2;
					}
					if (this.c == 37)
					{
						this.Consume();
						return 5;
					}
					if (this.c == 58)
					{
						this.Consume();
						return 6;
					}
					if (this.c == 46)
					{
						this.Consume();
						return 7;
					}
					if (this.c == 91)
					{
						this.Consume();
						while (this.c != 93)
						{
							if (this.c == 92)
							{
								this.Consume();
								if (this.c != 93)
								{
									this.sval.Append('\\');
								}
								this.sval.Append((char)this.c);
							}
							else
							{
								this.sval.Append((char)this.c);
							}
							this.Consume();
						}
						this.Consume();
						return 4;
					}
					this.Consume();
					this.error = true;
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000B02B File Offset: 0x0000922B
		protected virtual void Consume()
		{
			this.p++;
			if (this.p >= this.n)
			{
				this.c = -1;
				return;
			}
			this.c = (int)this.pattern[this.p];
		}

		// Token: 0x040000DC RID: 220
		public const int Begin = 1;

		// Token: 0x040000DD RID: 221
		public const int End = 2;

		// Token: 0x040000DE RID: 222
		public const int Id = 3;

		// Token: 0x040000DF RID: 223
		public const int Arg = 4;

		// Token: 0x040000E0 RID: 224
		public const int Percent = 5;

		// Token: 0x040000E1 RID: 225
		public const int Colon = 6;

		// Token: 0x040000E2 RID: 226
		public const int Dot = 7;

		// Token: 0x040000E3 RID: 227
		protected string pattern;

		// Token: 0x040000E4 RID: 228
		protected int p = -1;

		// Token: 0x040000E5 RID: 229
		protected int c;

		// Token: 0x040000E6 RID: 230
		protected int n;

		// Token: 0x040000E7 RID: 231
		public StringBuilder sval = new StringBuilder();

		// Token: 0x040000E8 RID: 232
		public bool error;
	}
}
