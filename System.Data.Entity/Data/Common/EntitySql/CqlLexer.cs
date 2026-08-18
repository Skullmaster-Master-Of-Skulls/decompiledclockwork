using System;
using System.Collections.Generic;
using System.Data.Common.EntitySql.AST;
using System.Data.Entity;
using System.IO;
using System.Text.RegularExpressions;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200032F RID: 815
	internal sealed class CqlLexer
	{
		// Token: 0x06002FFF RID: 12287 RVA: 0x000B549D File Offset: 0x000B369D
		internal CqlLexer(TextReader reader) : this()
		{
			if (reader == null)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserInputError"));
			}
			this.yy_reader = reader;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000B54BF File Offset: 0x000B36BF
		internal CqlLexer(FileStream instream) : this()
		{
			if (instream == null)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserInputError"));
			}
			this.yy_reader = new StreamReader(instream);
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000B54E8 File Offset: 0x000B36E8
		private CqlLexer()
		{
			this.yy_buffer = new char[512];
			this.yy_buffer_read = 0;
			this.yy_buffer_index = 0;
			this.yy_buffer_start = 0;
			this.yy_buffer_end = 0;
			this.yychar = 0;
			this.yyline = 0;
			this.yy_at_bol = true;
			this.yy_lexical_state = 0;
			this.accept_dispatch = new CqlLexer.AcceptMethod[]
			{
				null,
				null,
				new CqlLexer.AcceptMethod(this.Accept_2),
				new CqlLexer.AcceptMethod(this.Accept_3),
				new CqlLexer.AcceptMethod(this.Accept_4),
				new CqlLexer.AcceptMethod(this.Accept_5),
				new CqlLexer.AcceptMethod(this.Accept_6),
				new CqlLexer.AcceptMethod(this.Accept_7),
				new CqlLexer.AcceptMethod(this.Accept_8),
				new CqlLexer.AcceptMethod(this.Accept_9),
				new CqlLexer.AcceptMethod(this.Accept_10),
				new CqlLexer.AcceptMethod(this.Accept_11),
				new CqlLexer.AcceptMethod(this.Accept_12),
				new CqlLexer.AcceptMethod(this.Accept_13),
				new CqlLexer.AcceptMethod(this.Accept_14),
				new CqlLexer.AcceptMethod(this.Accept_15),
				new CqlLexer.AcceptMethod(this.Accept_16),
				new CqlLexer.AcceptMethod(this.Accept_17),
				new CqlLexer.AcceptMethod(this.Accept_18),
				null,
				new CqlLexer.AcceptMethod(this.Accept_20),
				new CqlLexer.AcceptMethod(this.Accept_21),
				new CqlLexer.AcceptMethod(this.Accept_22),
				new CqlLexer.AcceptMethod(this.Accept_23),
				null,
				new CqlLexer.AcceptMethod(this.Accept_25),
				new CqlLexer.AcceptMethod(this.Accept_26),
				new CqlLexer.AcceptMethod(this.Accept_27),
				new CqlLexer.AcceptMethod(this.Accept_28),
				null,
				new CqlLexer.AcceptMethod(this.Accept_30),
				new CqlLexer.AcceptMethod(this.Accept_31),
				new CqlLexer.AcceptMethod(this.Accept_32),
				null,
				new CqlLexer.AcceptMethod(this.Accept_34),
				new CqlLexer.AcceptMethod(this.Accept_35),
				null,
				new CqlLexer.AcceptMethod(this.Accept_37),
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				new CqlLexer.AcceptMethod(this.Accept_53),
				new CqlLexer.AcceptMethod(this.Accept_54),
				new CqlLexer.AcceptMethod(this.Accept_55),
				new CqlLexer.AcceptMethod(this.Accept_56),
				new CqlLexer.AcceptMethod(this.Accept_57),
				new CqlLexer.AcceptMethod(this.Accept_58),
				new CqlLexer.AcceptMethod(this.Accept_59),
				new CqlLexer.AcceptMethod(this.Accept_60),
				new CqlLexer.AcceptMethod(this.Accept_61),
				new CqlLexer.AcceptMethod(this.Accept_62),
				new CqlLexer.AcceptMethod(this.Accept_63),
				new CqlLexer.AcceptMethod(this.Accept_64),
				new CqlLexer.AcceptMethod(this.Accept_65),
				new CqlLexer.AcceptMethod(this.Accept_66),
				new CqlLexer.AcceptMethod(this.Accept_67),
				new CqlLexer.AcceptMethod(this.Accept_68),
				new CqlLexer.AcceptMethod(this.Accept_69),
				new CqlLexer.AcceptMethod(this.Accept_70),
				new CqlLexer.AcceptMethod(this.Accept_71),
				new CqlLexer.AcceptMethod(this.Accept_72),
				new CqlLexer.AcceptMethod(this.Accept_73),
				new CqlLexer.AcceptMethod(this.Accept_74),
				new CqlLexer.AcceptMethod(this.Accept_75),
				new CqlLexer.AcceptMethod(this.Accept_76),
				new CqlLexer.AcceptMethod(this.Accept_77),
				new CqlLexer.AcceptMethod(this.Accept_78),
				new CqlLexer.AcceptMethod(this.Accept_79),
				new CqlLexer.AcceptMethod(this.Accept_80),
				new CqlLexer.AcceptMethod(this.Accept_81),
				new CqlLexer.AcceptMethod(this.Accept_82),
				new CqlLexer.AcceptMethod(this.Accept_83),
				new CqlLexer.AcceptMethod(this.Accept_84)
			};
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000B5939 File Offset: 0x000B3B39
		private CqlLexer.Token Accept_2()
		{
			return this.HandleEscapedIdentifiers();
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_3()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000B594F File Offset: 0x000B3B4F
		private CqlLexer.Token Accept_4()
		{
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000B5960 File Offset: 0x000B3B60
		private CqlLexer.Token Accept_5()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000B596F File Offset: 0x000B3B6F
		private CqlLexer.Token Accept_6()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000B597D File Offset: 0x000B3B7D
		private CqlLexer.Token Accept_7()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000B598B File Offset: 0x000B3B8B
		private CqlLexer.Token Accept_8()
		{
			this._lineNumber++;
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000B59AA File Offset: 0x000B3BAA
		private CqlLexer.Token Accept_9()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.String);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000B59B9 File Offset: 0x000B3BB9
		private CqlLexer.Token Accept_10()
		{
			return this.MapDoubleQuotedString(this.YYText);
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000B59C7 File Offset: 0x000B3BC7
		private CqlLexer.Token Accept_11()
		{
			return this.NewParameterToken(this.YYText);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000B59D5 File Offset: 0x000B3BD5
		private CqlLexer.Token Accept_12()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Binary);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000B598B File Offset: 0x000B3B8B
		private CqlLexer.Token Accept_13()
		{
			this._lineNumber++;
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000B59E4 File Offset: 0x000B3BE4
		private CqlLexer.Token Accept_14()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Boolean);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000B59F3 File Offset: 0x000B3BF3
		private CqlLexer.Token Accept_15()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Time);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000B5A02 File Offset: 0x000B3C02
		private CqlLexer.Token Accept_16()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Guid);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000B5A11 File Offset: 0x000B3C11
		private CqlLexer.Token Accept_17()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.DateTime);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000B5A20 File Offset: 0x000B3C20
		private CqlLexer.Token Accept_18()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.DateTimeOffset);
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_20()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000B5960 File Offset: 0x000B3B60
		private CqlLexer.Token Accept_21()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000B596F File Offset: 0x000B3B6F
		private CqlLexer.Token Accept_22()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000B597D File Offset: 0x000B3B7D
		private CqlLexer.Token Accept_23()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_25()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000B5960 File Offset: 0x000B3B60
		private CqlLexer.Token Accept_26()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000B596F File Offset: 0x000B3B6F
		private CqlLexer.Token Accept_27()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000B597D File Offset: 0x000B3B7D
		private CqlLexer.Token Accept_28()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_30()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000B5960 File Offset: 0x000B3B60
		private CqlLexer.Token Accept_31()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000B597D File Offset: 0x000B3B7D
		private CqlLexer.Token Accept_32()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_34()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000B5960 File Offset: 0x000B3B60
		private CqlLexer.Token Accept_35()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000B5960 File Offset: 0x000B3B60
		private CqlLexer.Token Accept_37()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_53()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_54()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_55()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_56()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_57()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_58()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_59()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_60()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_61()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_62()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_63()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_64()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_65()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_66()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_67()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_68()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_69()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_70()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_71()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_72()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_73()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_74()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_75()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_76()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_77()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_78()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_79()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_80()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_81()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_82()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_83()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000B5941 File Offset: 0x000B3B41
		private CqlLexer.Token Accept_84()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000B5A2F File Offset: 0x000B3C2F
		private void yybegin(int state)
		{
			this.yy_lexical_state = state;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000B5A38 File Offset: 0x000B3C38
		private char yy_advance()
		{
			int num;
			if (this.yy_buffer_index < this.yy_buffer_read)
			{
				char[] array = this.yy_buffer;
				num = this.yy_buffer_index;
				this.yy_buffer_index = num + 1;
				return CqlLexer.yy_translate.translate(array[num]);
			}
			if (this.yy_buffer_start != 0)
			{
				int i = this.yy_buffer_start;
				int num2 = 0;
				while (i < this.yy_buffer_read)
				{
					this.yy_buffer[num2] = this.yy_buffer[i];
					i++;
					num2++;
				}
				this.yy_buffer_end -= this.yy_buffer_start;
				this.yy_buffer_start = 0;
				this.yy_buffer_read = num2;
				this.yy_buffer_index = num2;
				int num3 = this.yy_reader.Read(this.yy_buffer, this.yy_buffer_read, this.yy_buffer.Length - this.yy_buffer_read);
				if (num3 <= 0)
				{
					return '\u0081';
				}
				this.yy_buffer_read += num3;
			}
			while (this.yy_buffer_index >= this.yy_buffer_read)
			{
				if (this.yy_buffer_index >= this.yy_buffer.Length)
				{
					this.yy_buffer = this.yy_double(this.yy_buffer);
				}
				int num3 = this.yy_reader.Read(this.yy_buffer, this.yy_buffer_read, this.yy_buffer.Length - this.yy_buffer_read);
				if (num3 <= 0)
				{
					return '\u0081';
				}
				this.yy_buffer_read += num3;
			}
			char[] array2 = this.yy_buffer;
			num = this.yy_buffer_index;
			this.yy_buffer_index = num + 1;
			return CqlLexer.yy_translate.translate(array2[num]);
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000B5BA0 File Offset: 0x000B3DA0
		private void yy_move_end()
		{
			if (this.yy_buffer_end > this.yy_buffer_start && '\n' == this.yy_buffer[this.yy_buffer_end - 1])
			{
				this.yy_buffer_end--;
			}
			if (this.yy_buffer_end > this.yy_buffer_start && '\r' == this.yy_buffer[this.yy_buffer_end - 1])
			{
				this.yy_buffer_end--;
			}
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000B5C0C File Offset: 0x000B3E0C
		private void yy_mark_start()
		{
			for (int i = this.yy_buffer_start; i < this.yy_buffer_index; i++)
			{
				if (this.yy_buffer[i] == '\n' && !this.yy_last_was_cr)
				{
					this.yyline++;
				}
				if (this.yy_buffer[i] == '\r')
				{
					this.yyline++;
					this.yy_last_was_cr = true;
				}
				else
				{
					this.yy_last_was_cr = false;
				}
			}
			this.yychar = this.yychar + this.yy_buffer_index - this.yy_buffer_start;
			this.yy_buffer_start = this.yy_buffer_index;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000B5CA1 File Offset: 0x000B3EA1
		private void yy_mark_end()
		{
			this.yy_buffer_end = this.yy_buffer_index;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000B5CB0 File Offset: 0x000B3EB0
		private void yy_to_mark()
		{
			this.yy_buffer_index = this.yy_buffer_end;
			this.yy_at_bol = (this.yy_buffer_end > this.yy_buffer_start && (this.yy_buffer[this.yy_buffer_end - 1] == '\r' || this.yy_buffer[this.yy_buffer_end - 1] == '\n'));
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000B5D09 File Offset: 0x000B3F09
		internal string yytext()
		{
			return new string(this.yy_buffer, this.yy_buffer_start, this.yy_buffer_end - this.yy_buffer_start);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000B5D29 File Offset: 0x000B3F29
		internal int yy_char()
		{
			return this.yychar;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000B5D31 File Offset: 0x000B3F31
		private int yylength()
		{
			return this.yy_buffer_end - this.yy_buffer_start;
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000B5D40 File Offset: 0x000B3F40
		private char[] yy_double(char[] buf)
		{
			char[] array = new char[2 * buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i];
			}
			return array;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000B5D6D File Offset: 0x000B3F6D
		private void yy_error(int code, bool fatal)
		{
			if (fatal)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserFatalError"));
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000B5D84 File Offset: 0x000B3F84
		internal CqlLexer.Token yylex()
		{
			int num = CqlLexer.yy_state_dtrans[this.yy_lexical_state];
			int num2 = -1;
			bool flag = true;
			this.yy_mark_start();
			int num3 = CqlLexer.yy_acpt[num];
			if (num3 != 0)
			{
				num2 = num;
				this.yy_mark_end();
			}
			for (;;)
			{
				char c;
				if (flag && this.yy_at_bol)
				{
					c = '\u0080';
				}
				else
				{
					c = this.yy_advance();
				}
				int num4 = CqlLexer.yy_nxt[CqlLexer.yy_rmap[num], CqlLexer.yy_cmap[(int)c]];
				if ('\u0081' == c && flag)
				{
					break;
				}
				if (-1 != num4)
				{
					num = num4;
					flag = false;
					num3 = CqlLexer.yy_acpt[num];
					if (num3 != 0)
					{
						num2 = num;
						this.yy_mark_end();
					}
				}
				else
				{
					if (-1 == num2)
					{
						goto Block_7;
					}
					int num5 = CqlLexer.yy_acpt[num2];
					if ((2 & num5) != 0)
					{
						this.yy_move_end();
					}
					this.yy_to_mark();
					if (num2 < 0)
					{
						if (num2 < 85)
						{
							this.yy_error(0, false);
						}
					}
					else
					{
						CqlLexer.AcceptMethod acceptMethod = this.accept_dispatch[num2];
						if (acceptMethod != null)
						{
							CqlLexer.Token token = acceptMethod();
							if (token != null)
							{
								return token;
							}
						}
					}
					flag = true;
					num = CqlLexer.yy_state_dtrans[this.yy_lexical_state];
					num2 = -1;
					this.yy_mark_start();
					num3 = CqlLexer.yy_acpt[num];
					if (num3 != 0)
					{
						num2 = num;
						this.yy_mark_end();
					}
				}
			}
			return null;
			Block_7:
			throw new EntitySqlException(EntitySqlException.GetGenericErrorMessage(this._query, this.yychar));
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000B5ECA File Offset: 0x000B40CA
		internal CqlLexer(string query, ParserOptions parserOptions) : this()
		{
			this._query = query;
			this._parserOptions = parserOptions;
			this.yy_reader = new StringReader(this._query);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000B5EF1 File Offset: 0x000B40F1
		internal static CqlLexer.Token NewToken(short tokenId, Node tokenvalue)
		{
			return new CqlLexer.Token(tokenId, tokenvalue);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000B5EFA File Offset: 0x000B40FA
		internal static CqlLexer.Token NewToken(short tokenId, CqlLexer.TerminalToken termToken)
		{
			return new CqlLexer.Token(tokenId, termToken);
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000B5F03 File Offset: 0x000B4103
		internal string YYText
		{
			get
			{
				return this.yytext();
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x000B5F0B File Offset: 0x000B410B
		internal int IPos
		{
			get
			{
				return this._iPos;
			}
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000B5F13 File Offset: 0x000B4113
		internal int AdvanceIPos()
		{
			this._iPos += this.YYText.Length;
			return this._iPos;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000B5F33 File Offset: 0x000B4133
		internal static bool IsReservedKeyword(string term)
		{
			return CqlLexer.InternalKeywordDictionary.ContainsKey(term);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000B5F40 File Offset: 0x000B4140
		internal CqlLexer.Token MapIdentifierOrKeyword(string symbol)
		{
			CqlLexer.Token result;
			if (this.IsEscapedIdentifier(symbol, out result))
			{
				return result;
			}
			if (this.IsKeyword(symbol, out result))
			{
				return result;
			}
			return this.MapUnescapedIdentifier(symbol);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000B5F70 File Offset: 0x000B4170
		private bool IsEscapedIdentifier(string symbol, out CqlLexer.Token identifierToken)
		{
			if (symbol.Length <= 1 || symbol[0] != '[')
			{
				identifierToken = null;
				return false;
			}
			if (symbol[symbol.Length - 1] == ']')
			{
				string name = symbol.Substring(1, symbol.Length - 2);
				Identifier identifier = new Identifier(name, true, this._query, this._iPos);
				identifier.ErrCtx.ErrorContextInfo = "CtxEscapedIdentifier";
				identifierToken = CqlLexer.NewToken(CqlParser.ESCAPED_IDENTIFIER, identifier);
				return true;
			}
			throw EntityUtil.EntitySqlError(this._query, Strings.InvalidEscapedIdentifier(symbol), this._iPos);
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000B6004 File Offset: 0x000B4204
		private bool IsKeyword(string symbol, out CqlLexer.Token terminalToken)
		{
			char lookAheadChar = this.GetLookAheadChar();
			if (!this.IsInSymbolAsIdentifierState(lookAheadChar) && !this.IsCanonicalFunctionCall(symbol, lookAheadChar) && CqlLexer.InternalKeywordDictionary.ContainsKey(symbol))
			{
				this.ResetSymbolAsIdentifierState(true);
				short num = CqlLexer.InternalKeywordDictionary[symbol];
				if (num == CqlParser.AS)
				{
					this._symbolAsAliasIdentifierState = true;
				}
				else if (num == CqlParser.FUNCTION)
				{
					this._symbolAsInlineFunctionNameState = true;
				}
				terminalToken = CqlLexer.NewToken(num, new CqlLexer.TerminalToken(symbol, this._iPos));
				return true;
			}
			terminalToken = null;
			return false;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000B6085 File Offset: 0x000B4285
		private bool IsCanonicalFunctionCall(string symbol, char lookAheadChar)
		{
			return lookAheadChar == '(' && CqlLexer.InternalCanonicalFunctionNames.Contains(symbol);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000B609C File Offset: 0x000B429C
		private CqlLexer.Token MapUnescapedIdentifier(string symbol)
		{
			bool flag = CqlLexer.InternalInvalidAliasNames.Contains(symbol);
			if (this._symbolAsInlineFunctionNameState)
			{
				flag |= CqlLexer.InternalInvalidInlineFunctionNames.Contains(symbol);
			}
			this.ResetSymbolAsIdentifierState(true);
			if (flag)
			{
				throw EntityUtil.EntitySqlError(this._query, Strings.InvalidAliasName(symbol), this._iPos);
			}
			Identifier identifier = new Identifier(symbol, false, this._query, this._iPos);
			identifier.ErrCtx.ErrorContextInfo = "CtxIdentifier";
			return CqlLexer.NewToken(CqlParser.IDENTIFIER, identifier);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000B611C File Offset: 0x000B431C
		private char GetLookAheadChar()
		{
			this.yy_mark_end();
			char c = this.yy_advance();
			while (c != '\u0081' && (char.IsWhiteSpace(c) || CqlLexer.IsNewLine(c)))
			{
				c = this.yy_advance();
			}
			this.yy_to_mark();
			return c;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000B615E File Offset: 0x000B435E
		private bool IsInSymbolAsIdentifierState(char lookAheadChar)
		{
			return this._symbolAsIdentifierState || this._symbolAsAliasIdentifierState || this._symbolAsInlineFunctionNameState || lookAheadChar == '.';
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000B617F File Offset: 0x000B437F
		private void ResetSymbolAsIdentifierState(bool significant)
		{
			this._symbolAsIdentifierState = false;
			if (significant)
			{
				this._symbolAsAliasIdentifierState = false;
				this._symbolAsInlineFunctionNameState = false;
			}
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000B619C File Offset: 0x000B439C
		internal CqlLexer.Token MapOperator(string oper)
		{
			if (CqlLexer.InternalOperatorDictionary.ContainsKey(oper))
			{
				return CqlLexer.NewToken(CqlLexer.InternalOperatorDictionary[oper], new CqlLexer.TerminalToken(oper, this._iPos));
			}
			throw EntityUtil.EntitySqlError(this._query, Strings.InvalidOperatorSymbol, this._iPos);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000B61EC File Offset: 0x000B43EC
		internal CqlLexer.Token MapPunctuator(string punct)
		{
			if (CqlLexer.InternalPunctuatorDictionary.ContainsKey(punct))
			{
				this.ResetSymbolAsIdentifierState(true);
				if (punct.Equals(".", StringComparison.OrdinalIgnoreCase))
				{
					this._symbolAsIdentifierState = true;
				}
				return CqlLexer.NewToken(CqlLexer.InternalPunctuatorDictionary[punct], new CqlLexer.TerminalToken(punct, this._iPos));
			}
			throw EntityUtil.EntitySqlError(this._query, Strings.InvalidPunctuatorSymbol, this._iPos);
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000B6255 File Offset: 0x000B4455
		internal CqlLexer.Token MapDoubleQuotedString(string symbol)
		{
			return this.NewLiteralToken(symbol, LiteralKind.String);
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000B6260 File Offset: 0x000B4460
		internal CqlLexer.Token NewLiteralToken(string literal, LiteralKind literalKind)
		{
			string text = literal;
			switch (literalKind)
			{
			case LiteralKind.String:
				if ('N' == literal[0])
				{
					literalKind = LiteralKind.UnicodeString;
				}
				break;
			case LiteralKind.Binary:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidBinaryValue(text))
				{
					throw EntityUtil.EntitySqlError(this._query, Strings.InvalidLiteralFormat("binary", text), this._iPos);
				}
				break;
			case LiteralKind.DateTime:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidDateTimeValue(text))
				{
					throw EntityUtil.EntitySqlError(this._query, Strings.InvalidLiteralFormat("datetime", text), this._iPos);
				}
				break;
			case LiteralKind.Time:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidTimeValue(text))
				{
					throw EntityUtil.EntitySqlError(this._query, Strings.InvalidLiteralFormat("time", text), this._iPos);
				}
				break;
			case LiteralKind.DateTimeOffset:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidDateTimeOffsetValue(text))
				{
					throw EntityUtil.EntitySqlError(this._query, Strings.InvalidLiteralFormat("datetimeoffset", text), this._iPos);
				}
				break;
			case LiteralKind.Guid:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidGuidValue(text))
				{
					throw EntityUtil.EntitySqlError(this._query, Strings.InvalidLiteralFormat("guid", text), this._iPos);
				}
				break;
			}
			return CqlLexer.NewToken(CqlParser.LITERAL, new Literal(text, literalKind, this._query, this._iPos));
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000B63B1 File Offset: 0x000B45B1
		internal CqlLexer.Token NewParameterToken(string param)
		{
			return CqlLexer.NewToken(CqlParser.PARAMETER, new QueryParameter(param, this._query, this._iPos));
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000B63D0 File Offset: 0x000B45D0
		internal CqlLexer.Token HandleEscapedIdentifiers()
		{
			for (char c = this.YYText[0]; c != '\u0081'; c = this.yy_advance())
			{
				if (c == ']')
				{
					this.yy_mark_end();
					c = this.yy_advance();
					if (c != ']')
					{
						this.yy_to_mark();
						this.ResetSymbolAsIdentifierState(true);
						return this.MapIdentifierOrKeyword(this.YYText.Replace("]]", "]"));
					}
				}
			}
			throw EntityUtil.EntitySqlError(this._query, Strings.InvalidEscapedIdentifierUnbalanced(this.YYText), this._iPos);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000B6458 File Offset: 0x000B4658
		internal static bool IsLetterOrDigitOrUnderscore(string symbol, out bool isIdentifierASCII)
		{
			isIdentifierASCII = true;
			for (int i = 0; i < symbol.Length; i++)
			{
				isIdentifierASCII = (isIdentifierASCII && symbol[i] < '\u0080');
				if (!isIdentifierASCII && !CqlLexer.IsLetter(symbol[i]) && !CqlLexer.IsDigit(symbol[i]) && symbol[i] != '_')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000B64BE File Offset: 0x000B46BE
		private static bool IsLetter(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000B64DB File Offset: 0x000B46DB
		private static bool IsDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000B64EC File Offset: 0x000B46EC
		private static bool isHexDigit(char c)
		{
			return CqlLexer.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000B6514 File Offset: 0x000B4714
		internal static bool IsNewLine(char c)
		{
			for (int i = 0; i < CqlLexer._newLineCharacters.Length; i++)
			{
				if (c == CqlLexer._newLineCharacters[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000B6540 File Offset: 0x000B4740
		private static string GetLiteralSingleQuotePayload(string literal)
		{
			if (literal.Split(new char[]
			{
				'\''
			}).Length != 3 || -1 == literal.IndexOf('\'') || -1 == literal.LastIndexOf('\''))
			{
				throw EntityUtil.EntitySqlError(Strings.MalformedSingleQuotePayload);
			}
			int num = literal.IndexOf('\'');
			string text = literal.Substring(num + 1, literal.Length - (num + 2));
			if (text.Split(new char[]
			{
				'\''
			}).Length != 1)
			{
				throw EntityUtil.EntitySqlError(Strings.MalformedSingleQuotePayload);
			}
			return text;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000B65C4 File Offset: 0x000B47C4
		private static bool IsValidGuidValue(string guidValue)
		{
			int num = 0;
			int num2 = guidValue.Length - 1;
			if (num2 - num + 1 != 36)
			{
				return false;
			}
			int num3 = 0;
			bool flag = true;
			while (flag && num3 < 36)
			{
				if (num3 == 8 || num3 == 13 || num3 == 18 || num3 == 23)
				{
					flag = (guidValue[num + num3] == '-');
				}
				else
				{
					flag = CqlLexer.isHexDigit(guidValue[num + num3]);
				}
				num3++;
			}
			return flag;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000B662C File Offset: 0x000B482C
		private static bool IsValidBinaryValue(string binaryValue)
		{
			if (string.IsNullOrEmpty(binaryValue))
			{
				return true;
			}
			int num = 0;
			bool flag;
			for (flag = (binaryValue.Length > 0); flag && num < binaryValue.Length; flag = CqlLexer.isHexDigit(binaryValue[num++]))
			{
			}
			return flag;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000B666F File Offset: 0x000B486F
		private static bool IsValidDateTimeValue(string datetimeValue)
		{
			if (CqlLexer._reDateTimeValue == null)
			{
				CqlLexer._reDateTimeValue = new Regex("^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reDateTimeValue.IsMatch(datetimeValue);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000B6697 File Offset: 0x000B4897
		private static bool IsValidTimeValue(string timeValue)
		{
			if (CqlLexer._reTimeValue == null)
			{
				CqlLexer._reTimeValue = new Regex("^[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reTimeValue.IsMatch(timeValue);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000B66BF File Offset: 0x000B48BF
		private static bool IsValidDateTimeOffsetValue(string datetimeOffsetValue)
		{
			if (CqlLexer._reDateTimeOffsetValue == null)
			{
				CqlLexer._reDateTimeOffsetValue = new Regex("^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?([ ])*[\\+-][0-9]{1,2}:[0-9]{1,2}$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reDateTimeOffsetValue.IsMatch(datetimeOffsetValue);
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x000B66E8 File Offset: 0x000B48E8
		private static Dictionary<string, short> InternalKeywordDictionary
		{
			get
			{
				if (CqlLexer._keywords == null)
				{
					CqlLexer._keywords = new Dictionary<string, short>(60, CqlLexer._stringComparer)
					{
						{
							"all",
							CqlParser.ALL
						},
						{
							"and",
							CqlParser.AND
						},
						{
							"anyelement",
							CqlParser.ANYELEMENT
						},
						{
							"apply",
							CqlParser.APPLY
						},
						{
							"as",
							CqlParser.AS
						},
						{
							"asc",
							CqlParser.ASC
						},
						{
							"between",
							CqlParser.BETWEEN
						},
						{
							"by",
							CqlParser.BY
						},
						{
							"case",
							CqlParser.CASE
						},
						{
							"cast",
							CqlParser.CAST
						},
						{
							"collate",
							CqlParser.COLLATE
						},
						{
							"collection",
							CqlParser.COLLECTION
						},
						{
							"createref",
							CqlParser.CREATEREF
						},
						{
							"cross",
							CqlParser.CROSS
						},
						{
							"deref",
							CqlParser.DEREF
						},
						{
							"desc",
							CqlParser.DESC
						},
						{
							"distinct",
							CqlParser.DISTINCT
						},
						{
							"element",
							CqlParser.ELEMENT
						},
						{
							"else",
							CqlParser.ELSE
						},
						{
							"end",
							CqlParser.END
						},
						{
							"escape",
							CqlParser.ESCAPE
						},
						{
							"except",
							CqlParser.EXCEPT
						},
						{
							"exists",
							CqlParser.EXISTS
						},
						{
							"false",
							CqlParser.LITERAL
						},
						{
							"flatten",
							CqlParser.FLATTEN
						},
						{
							"from",
							CqlParser.FROM
						},
						{
							"full",
							CqlParser.FULL
						},
						{
							"function",
							CqlParser.FUNCTION
						},
						{
							"group",
							CqlParser.GROUP
						},
						{
							"grouppartition",
							CqlParser.GROUPPARTITION
						},
						{
							"having",
							CqlParser.HAVING
						},
						{
							"in",
							CqlParser.IN
						},
						{
							"inner",
							CqlParser.INNER
						},
						{
							"intersect",
							CqlParser.INTERSECT
						},
						{
							"is",
							CqlParser.IS
						},
						{
							"join",
							CqlParser.JOIN
						},
						{
							"key",
							CqlParser.KEY
						},
						{
							"left",
							CqlParser.LEFT
						},
						{
							"like",
							CqlParser.LIKE
						},
						{
							"limit",
							CqlParser.LIMIT
						},
						{
							"multiset",
							CqlParser.MULTISET
						},
						{
							"navigate",
							CqlParser.NAVIGATE
						},
						{
							"not",
							CqlParser.NOT
						},
						{
							"null",
							CqlParser.NULL
						},
						{
							"of",
							CqlParser.OF
						},
						{
							"oftype",
							CqlParser.OFTYPE
						},
						{
							"on",
							CqlParser.ON
						},
						{
							"only",
							CqlParser.ONLY
						},
						{
							"or",
							CqlParser.OR
						},
						{
							"order",
							CqlParser.ORDER
						},
						{
							"outer",
							CqlParser.OUTER
						},
						{
							"overlaps",
							CqlParser.OVERLAPS
						},
						{
							"ref",
							CqlParser.REF
						},
						{
							"relationship",
							CqlParser.RELATIONSHIP
						},
						{
							"right",
							CqlParser.RIGHT
						},
						{
							"row",
							CqlParser.ROW
						},
						{
							"select",
							CqlParser.SELECT
						},
						{
							"set",
							CqlParser.SET
						},
						{
							"skip",
							CqlParser.SKIP
						},
						{
							"then",
							CqlParser.THEN
						},
						{
							"top",
							CqlParser.TOP
						},
						{
							"treat",
							CqlParser.TREAT
						},
						{
							"true",
							CqlParser.LITERAL
						},
						{
							"union",
							CqlParser.UNION
						},
						{
							"using",
							CqlParser.USING
						},
						{
							"value",
							CqlParser.VALUE
						},
						{
							"when",
							CqlParser.WHEN
						},
						{
							"where",
							CqlParser.WHERE
						},
						{
							"with",
							CqlParser.WITH
						}
					};
				}
				return CqlLexer._keywords;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000B6B68 File Offset: 0x000B4D68
		private static HashSet<string> InternalInvalidAliasNames
		{
			get
			{
				if (CqlLexer._invalidAliasNames == null)
				{
					CqlLexer._invalidAliasNames = new HashSet<string>(CqlLexer._stringComparer)
					{
						"all",
						"and",
						"apply",
						"as",
						"asc",
						"between",
						"by",
						"case",
						"cast",
						"collate",
						"createref",
						"deref",
						"desc",
						"distinct",
						"element",
						"else",
						"end",
						"escape",
						"except",
						"exists",
						"flatten",
						"from",
						"group",
						"having",
						"in",
						"inner",
						"intersect",
						"is",
						"join",
						"like",
						"multiset",
						"navigate",
						"not",
						"null",
						"of",
						"oftype",
						"on",
						"only",
						"or",
						"overlaps",
						"ref",
						"relationship",
						"select",
						"set",
						"then",
						"treat",
						"union",
						"using",
						"when",
						"where",
						"with"
					};
				}
				return CqlLexer._invalidAliasNames;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x000B6DFC File Offset: 0x000B4FFC
		private static HashSet<string> InternalInvalidInlineFunctionNames
		{
			get
			{
				if (CqlLexer._invalidInlineFunctionNames == null)
				{
					CqlLexer._invalidInlineFunctionNames = new HashSet<string>(CqlLexer._stringComparer)
					{
						"anyelement",
						"element",
						"function",
						"grouppartition",
						"key",
						"ref",
						"row",
						"skip",
						"top",
						"value"
					};
				}
				return CqlLexer._invalidInlineFunctionNames;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x000B6EA4 File Offset: 0x000B50A4
		private static Dictionary<string, short> InternalOperatorDictionary
		{
			get
			{
				if (CqlLexer._operators == null)
				{
					CqlLexer._operators = new Dictionary<string, short>(16, CqlLexer._stringComparer)
					{
						{
							"==",
							CqlParser.OP_EQ
						},
						{
							"!=",
							CqlParser.OP_NEQ
						},
						{
							"<>",
							CqlParser.OP_NEQ
						},
						{
							"<",
							CqlParser.OP_LT
						},
						{
							"<=",
							CqlParser.OP_LE
						},
						{
							">",
							CqlParser.OP_GT
						},
						{
							">=",
							CqlParser.OP_GE
						},
						{
							"&&",
							CqlParser.AND
						},
						{
							"||",
							CqlParser.OR
						},
						{
							"!",
							CqlParser.NOT
						},
						{
							"+",
							CqlParser.PLUS
						},
						{
							"-",
							CqlParser.MINUS
						},
						{
							"*",
							CqlParser.STAR
						},
						{
							"/",
							CqlParser.FSLASH
						},
						{
							"%",
							CqlParser.PERCENT
						}
					};
				}
				return CqlLexer._operators;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x000B6FC4 File Offset: 0x000B51C4
		private static Dictionary<string, short> InternalPunctuatorDictionary
		{
			get
			{
				if (CqlLexer._punctuators == null)
				{
					CqlLexer._punctuators = new Dictionary<string, short>(16, CqlLexer._stringComparer)
					{
						{
							",",
							CqlParser.COMMA
						},
						{
							":",
							CqlParser.COLON
						},
						{
							".",
							CqlParser.DOT
						},
						{
							"?",
							CqlParser.QMARK
						},
						{
							"(",
							CqlParser.L_PAREN
						},
						{
							")",
							CqlParser.R_PAREN
						},
						{
							"[",
							CqlParser.L_BRACE
						},
						{
							"]",
							CqlParser.R_BRACE
						},
						{
							"{",
							CqlParser.L_CURLY
						},
						{
							"}",
							CqlParser.R_CURLY
						},
						{
							";",
							CqlParser.SCOLON
						},
						{
							"=",
							CqlParser.EQUAL
						}
					};
				}
				return CqlLexer._punctuators;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x000B70B4 File Offset: 0x000B52B4
		private static HashSet<string> InternalCanonicalFunctionNames
		{
			get
			{
				if (CqlLexer._canonicalFunctionNames == null)
				{
					CqlLexer._canonicalFunctionNames = new HashSet<string>(CqlLexer._stringComparer)
					{
						"left",
						"right"
					};
				}
				return CqlLexer._canonicalFunctionNames;
			}
		}

		// Token: 0x04001484 RID: 5252
		private const int YY_BUFFER_SIZE = 512;

		// Token: 0x04001485 RID: 5253
		private const int YY_F = -1;

		// Token: 0x04001486 RID: 5254
		private const int YY_NO_STATE = -1;

		// Token: 0x04001487 RID: 5255
		private const int YY_NOT_ACCEPT = 0;

		// Token: 0x04001488 RID: 5256
		private const int YY_START = 1;

		// Token: 0x04001489 RID: 5257
		private const int YY_END = 2;

		// Token: 0x0400148A RID: 5258
		private const int YY_NO_ANCHOR = 4;

		// Token: 0x0400148B RID: 5259
		private CqlLexer.AcceptMethod[] accept_dispatch;

		// Token: 0x0400148C RID: 5260
		private const int YY_BOL = 128;

		// Token: 0x0400148D RID: 5261
		private const int YY_EOF = 129;

		// Token: 0x0400148E RID: 5262
		private TextReader yy_reader;

		// Token: 0x0400148F RID: 5263
		private int yy_buffer_index;

		// Token: 0x04001490 RID: 5264
		private int yy_buffer_read;

		// Token: 0x04001491 RID: 5265
		private int yy_buffer_start;

		// Token: 0x04001492 RID: 5266
		private int yy_buffer_end;

		// Token: 0x04001493 RID: 5267
		private char[] yy_buffer;

		// Token: 0x04001494 RID: 5268
		private int yychar;

		// Token: 0x04001495 RID: 5269
		private int yyline;

		// Token: 0x04001496 RID: 5270
		private bool yy_at_bol;

		// Token: 0x04001497 RID: 5271
		private int yy_lexical_state;

		// Token: 0x04001498 RID: 5272
		private const int YYINITIAL = 0;

		// Token: 0x04001499 RID: 5273
		private static int[] yy_state_dtrans = new int[1];

		// Token: 0x0400149A RID: 5274
		private bool yy_last_was_cr;

		// Token: 0x0400149B RID: 5275
		private const int YY_E_INTERNAL = 0;

		// Token: 0x0400149C RID: 5276
		private const int YY_E_MATCH = 1;

		// Token: 0x0400149D RID: 5277
		private static string[] yy_error_string = new string[]
		{
			"Error: Internal error.\n",
			"Error: Unmatched input.\n"
		};

		// Token: 0x0400149E RID: 5278
		private static int[] yy_acpt = new int[]
		{
			0,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			2,
			4,
			4,
			4,
			4,
			4,
			0,
			4,
			4,
			4,
			4,
			0,
			4,
			4,
			4,
			4,
			0,
			4,
			4,
			4,
			0,
			4,
			4,
			0,
			4,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4
		};

		// Token: 0x0400149F RID: 5279
		private static int[] yy_cmap = new int[]
		{
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			27,
			11,
			11,
			8,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			12,
			33,
			28,
			11,
			11,
			39,
			36,
			10,
			40,
			40,
			39,
			38,
			40,
			25,
			24,
			39,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			40,
			40,
			34,
			32,
			35,
			40,
			29,
			5,
			2,
			30,
			13,
			15,
			18,
			20,
			30,
			3,
			30,
			30,
			23,
			16,
			26,
			17,
			30,
			30,
			6,
			19,
			14,
			21,
			30,
			30,
			9,
			7,
			30,
			1,
			11,
			40,
			11,
			31,
			11,
			5,
			2,
			30,
			13,
			15,
			18,
			20,
			30,
			3,
			30,
			30,
			23,
			16,
			4,
			17,
			30,
			30,
			6,
			19,
			14,
			21,
			30,
			30,
			9,
			7,
			30,
			40,
			37,
			40,
			11,
			11,
			0,
			41
		};

		// Token: 0x040014A0 RID: 5280
		private static int[] yy_rmap = new int[]
		{
			0,
			1,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			1,
			1,
			11,
			1,
			1,
			1,
			1,
			12,
			13,
			1,
			14,
			14,
			15,
			16,
			17,
			1,
			18,
			10,
			19,
			20,
			1,
			21,
			22,
			23,
			24,
			25,
			26,
			27,
			5,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			62,
			63,
			11,
			64,
			65,
			66,
			67,
			68,
			11,
			69
		};

		// Token: 0x040014A1 RID: 5281
		private static int[,] yy_nxt = new int[,]
		{
			{
				1,
				2,
				3,
				83,
				83,
				83,
				83,
				83,
				4,
				20,
				19,
				-1,
				4,
				84,
				64,
				83,
				83,
				83,
				71,
				83,
				72,
				83,
				5,
				83,
				6,
				7,
				25,
				8,
				24,
				29,
				83,
				83,
				22,
				23,
				28,
				23,
				33,
				36,
				32,
				32,
				27,
				1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				76,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				4,
				-1,
				-1,
				-1,
				4,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				21,
				-1,
				39,
				21,
				-1,
				21,
				-1,
				-1,
				26,
				5,
				31,
				40,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				35,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				41,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				8,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				19,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				24,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				11,
				-1,
				-1,
				-1,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				11,
				-1,
				-1,
				11,
				-1,
				-1,
				-1,
				11,
				11,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				9,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				19,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				38,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				32,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				10,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				24,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				19,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				24,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				21,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				32,
				-1,
				-1,
				32,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				14,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				21,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				32,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				44,
				83,
				45,
				-1,
				44,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				21,
				-1,
				39,
				21,
				-1,
				21,
				-1,
				-1,
				-1,
				35,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				32,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				21,
				-1,
				-1,
				-1,
				-1,
				21,
				-1,
				-1,
				-1,
				37,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				-1,
				38,
				12,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				-1,
				-1,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				38,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				37,
				-1,
				-1,
				42,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				42,
				-1,
				-1,
				-1
			},
			{
				-1,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				43,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				13,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				41,
				13
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				37,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				13,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				44,
				-1,
				45,
				-1,
				44,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				-1,
				45,
				15,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				-1,
				-1,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				45,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				46,
				-1,
				47,
				-1,
				46,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				-1,
				47,
				16,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				-1,
				-1,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				47,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				48,
				-1,
				38,
				-1,
				48,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				49,
				-1,
				50,
				-1,
				49,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				-1,
				50,
				17,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				-1,
				-1,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				50,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				51,
				-1,
				52,
				-1,
				51,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				-1,
				52,
				18,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				-1,
				-1,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				52,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				30,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				46,
				83,
				47,
				-1,
				46,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				34,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				48,
				83,
				38,
				-1,
				48,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				30,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				49,
				83,
				50,
				-1,
				49,
				83,
				83,
				83,
				83,
				81,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				54,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				51,
				83,
				52,
				-1,
				51,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				56,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				58,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				60,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				65,
				83,
				83,
				53,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				55,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				57,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				59,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				61,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				62,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				63,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				66,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				67,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				68,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				69,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				70,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				73,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				73,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				79,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				80,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				74,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				82,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				75,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				-1,
				-1,
				83,
				83,
				83,
				78,
				83,
				83,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				83,
				77,
				83,
				-1,
				-1,
				83,
				-1,
				-1,
				-1,
				83,
				77,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			}
		};

		// Token: 0x040014A2 RID: 5282
		private static readonly StringComparer _stringComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x040014A3 RID: 5283
		private static Dictionary<string, short> _keywords;

		// Token: 0x040014A4 RID: 5284
		private static HashSet<string> _invalidAliasNames;

		// Token: 0x040014A5 RID: 5285
		private static HashSet<string> _invalidInlineFunctionNames;

		// Token: 0x040014A6 RID: 5286
		private static Dictionary<string, short> _operators;

		// Token: 0x040014A7 RID: 5287
		private static Dictionary<string, short> _punctuators;

		// Token: 0x040014A8 RID: 5288
		private static HashSet<string> _canonicalFunctionNames;

		// Token: 0x040014A9 RID: 5289
		private static Regex _reDateTimeValue;

		// Token: 0x040014AA RID: 5290
		private static Regex _reTimeValue;

		// Token: 0x040014AB RID: 5291
		private static Regex _reDateTimeOffsetValue;

		// Token: 0x040014AC RID: 5292
		private const string _datetimeValueRegularExpression = "^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$";

		// Token: 0x040014AD RID: 5293
		private const string _timeValueRegularExpression = "^[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$";

		// Token: 0x040014AE RID: 5294
		private const string _datetimeOffsetValueRegularExpression = "^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?([ ])*[\\+-][0-9]{1,2}:[0-9]{1,2}$";

		// Token: 0x040014AF RID: 5295
		private int _iPos;

		// Token: 0x040014B0 RID: 5296
		private int _lineNumber;

		// Token: 0x040014B1 RID: 5297
		private ParserOptions _parserOptions;

		// Token: 0x040014B2 RID: 5298
		private string _query;

		// Token: 0x040014B3 RID: 5299
		private bool _symbolAsIdentifierState;

		// Token: 0x040014B4 RID: 5300
		private bool _symbolAsAliasIdentifierState;

		// Token: 0x040014B5 RID: 5301
		private bool _symbolAsInlineFunctionNameState;

		// Token: 0x040014B6 RID: 5302
		private static readonly char[] _newLineCharacters = new char[]
		{
			'\n',
			'\u0085',
			'\v',
			'\u2028',
			'\u2029'
		};

		// Token: 0x02000646 RID: 1606
		// (Invoke) Token: 0x060043BF RID: 17343
		private delegate CqlLexer.Token AcceptMethod();

		// Token: 0x02000647 RID: 1607
		internal class Token
		{
			// Token: 0x060043C2 RID: 17346 RVA: 0x000F6230 File Offset: 0x000F4430
			internal Token(short tokenId, Node tokenValue)
			{
				this._tokenId = tokenId;
				this._tokenValue = tokenValue;
			}

			// Token: 0x060043C3 RID: 17347 RVA: 0x000F6230 File Offset: 0x000F4430
			internal Token(short tokenId, CqlLexer.TerminalToken terminal)
			{
				this._tokenId = tokenId;
				this._tokenValue = terminal;
			}

			// Token: 0x17000BA3 RID: 2979
			// (get) Token: 0x060043C4 RID: 17348 RVA: 0x000F6246 File Offset: 0x000F4446
			internal short TokenId
			{
				get
				{
					return this._tokenId;
				}
			}

			// Token: 0x17000BA4 RID: 2980
			// (get) Token: 0x060043C5 RID: 17349 RVA: 0x000F624E File Offset: 0x000F444E
			internal object Value
			{
				get
				{
					return this._tokenValue;
				}
			}

			// Token: 0x04001EDF RID: 7903
			private short _tokenId;

			// Token: 0x04001EE0 RID: 7904
			private object _tokenValue;
		}

		// Token: 0x02000648 RID: 1608
		internal class TerminalToken
		{
			// Token: 0x060043C6 RID: 17350 RVA: 0x000F6256 File Offset: 0x000F4456
			internal TerminalToken(string token, int iPos)
			{
				this._token = token;
				this._iPos = iPos;
			}

			// Token: 0x17000BA5 RID: 2981
			// (get) Token: 0x060043C7 RID: 17351 RVA: 0x000F626C File Offset: 0x000F446C
			internal int IPos
			{
				get
				{
					return this._iPos;
				}
			}

			// Token: 0x17000BA6 RID: 2982
			// (get) Token: 0x060043C8 RID: 17352 RVA: 0x000F6274 File Offset: 0x000F4474
			internal string Token
			{
				get
				{
					return this._token;
				}
			}

			// Token: 0x04001EE1 RID: 7905
			private string _token;

			// Token: 0x04001EE2 RID: 7906
			private int _iPos;
		}

		// Token: 0x02000649 RID: 1609
		internal static class yy_translate
		{
			// Token: 0x060043C9 RID: 17353 RVA: 0x000F627C File Offset: 0x000F447C
			internal static char translate(char c)
			{
				if (char.IsWhiteSpace(c) || char.IsControl(c))
				{
					if (CqlLexer.IsNewLine(c))
					{
						return '\n';
					}
					return ' ';
				}
				else
				{
					if (c < '\u007f')
					{
						return c;
					}
					if (char.IsLetter(c) || char.IsSymbol(c) || char.IsNumber(c))
					{
						return 'a';
					}
					return '`';
				}
			}
		}
	}
}
