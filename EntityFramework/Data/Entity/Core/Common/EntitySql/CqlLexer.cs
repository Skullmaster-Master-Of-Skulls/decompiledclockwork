using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Resources;
using System.IO;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000241 RID: 577
	internal sealed class CqlLexer
	{
		// Token: 0x060013F7 RID: 5111 RVA: 0x00051C56 File Offset: 0x0004FE56
		internal CqlLexer(TextReader reader) : this()
		{
			if (reader == null)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserInputError"));
			}
			this.yy_reader = reader;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00051C78 File Offset: 0x0004FE78
		internal CqlLexer(FileStream instream) : this()
		{
			if (instream == null)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserInputError"));
			}
			this.yy_reader = new StreamReader(instream);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00051CA0 File Offset: 0x0004FEA0
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

		// Token: 0x060013FA RID: 5114 RVA: 0x000520F3 File Offset: 0x000502F3
		private CqlLexer.Token Accept_2()
		{
			return this.HandleEscapedIdentifiers();
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000520FB File Offset: 0x000502FB
		private CqlLexer.Token Accept_3()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00052109 File Offset: 0x00050309
		private CqlLexer.Token Accept_4()
		{
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0005211A File Offset: 0x0005031A
		private CqlLexer.Token Accept_5()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00052129 File Offset: 0x00050329
		private CqlLexer.Token Accept_6()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00052137 File Offset: 0x00050337
		private CqlLexer.Token Accept_7()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00052145 File Offset: 0x00050345
		private CqlLexer.Token Accept_8()
		{
			this._lineNumber++;
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00052164 File Offset: 0x00050364
		private CqlLexer.Token Accept_9()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.String);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00052173 File Offset: 0x00050373
		private CqlLexer.Token Accept_10()
		{
			return this.MapDoubleQuotedString(this.YYText);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00052181 File Offset: 0x00050381
		private CqlLexer.Token Accept_11()
		{
			return this.NewParameterToken(this.YYText);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0005218F File Offset: 0x0005038F
		private CqlLexer.Token Accept_12()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Binary);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0005219E File Offset: 0x0005039E
		private CqlLexer.Token Accept_13()
		{
			this._lineNumber++;
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x000521BD File Offset: 0x000503BD
		private CqlLexer.Token Accept_14()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Boolean);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000521CC File Offset: 0x000503CC
		private CqlLexer.Token Accept_15()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Time);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000521DB File Offset: 0x000503DB
		private CqlLexer.Token Accept_16()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Guid);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x000521EA File Offset: 0x000503EA
		private CqlLexer.Token Accept_17()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.DateTime);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x000521F9 File Offset: 0x000503F9
		private CqlLexer.Token Accept_18()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.DateTimeOffset);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00052208 File Offset: 0x00050408
		private CqlLexer.Token Accept_20()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00052216 File Offset: 0x00050416
		private CqlLexer.Token Accept_21()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00052225 File Offset: 0x00050425
		private CqlLexer.Token Accept_22()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00052233 File Offset: 0x00050433
		private CqlLexer.Token Accept_23()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00052241 File Offset: 0x00050441
		private CqlLexer.Token Accept_25()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0005224F File Offset: 0x0005044F
		private CqlLexer.Token Accept_26()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0005225E File Offset: 0x0005045E
		private CqlLexer.Token Accept_27()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0005226C File Offset: 0x0005046C
		private CqlLexer.Token Accept_28()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0005227A File Offset: 0x0005047A
		private CqlLexer.Token Accept_30()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00052288 File Offset: 0x00050488
		private CqlLexer.Token Accept_31()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00052297 File Offset: 0x00050497
		private CqlLexer.Token Accept_32()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000522A5 File Offset: 0x000504A5
		private CqlLexer.Token Accept_34()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x000522B3 File Offset: 0x000504B3
		private CqlLexer.Token Accept_35()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x000522C2 File Offset: 0x000504C2
		private CqlLexer.Token Accept_37()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x000522D1 File Offset: 0x000504D1
		private CqlLexer.Token Accept_53()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000522DF File Offset: 0x000504DF
		private CqlLexer.Token Accept_54()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x000522ED File Offset: 0x000504ED
		private CqlLexer.Token Accept_55()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x000522FB File Offset: 0x000504FB
		private CqlLexer.Token Accept_56()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00052309 File Offset: 0x00050509
		private CqlLexer.Token Accept_57()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00052317 File Offset: 0x00050517
		private CqlLexer.Token Accept_58()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00052325 File Offset: 0x00050525
		private CqlLexer.Token Accept_59()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00052333 File Offset: 0x00050533
		private CqlLexer.Token Accept_60()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00052341 File Offset: 0x00050541
		private CqlLexer.Token Accept_61()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0005234F File Offset: 0x0005054F
		private CqlLexer.Token Accept_62()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0005235D File Offset: 0x0005055D
		private CqlLexer.Token Accept_63()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0005236B File Offset: 0x0005056B
		private CqlLexer.Token Accept_64()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00052379 File Offset: 0x00050579
		private CqlLexer.Token Accept_65()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00052387 File Offset: 0x00050587
		private CqlLexer.Token Accept_66()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00052395 File Offset: 0x00050595
		private CqlLexer.Token Accept_67()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x000523A3 File Offset: 0x000505A3
		private CqlLexer.Token Accept_68()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x000523B1 File Offset: 0x000505B1
		private CqlLexer.Token Accept_69()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x000523BF File Offset: 0x000505BF
		private CqlLexer.Token Accept_70()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x000523CD File Offset: 0x000505CD
		private CqlLexer.Token Accept_71()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x000523DB File Offset: 0x000505DB
		private CqlLexer.Token Accept_72()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000523E9 File Offset: 0x000505E9
		private CqlLexer.Token Accept_73()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x000523F7 File Offset: 0x000505F7
		private CqlLexer.Token Accept_74()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00052405 File Offset: 0x00050605
		private CqlLexer.Token Accept_75()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00052413 File Offset: 0x00050613
		private CqlLexer.Token Accept_76()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00052421 File Offset: 0x00050621
		private CqlLexer.Token Accept_77()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0005242F File Offset: 0x0005062F
		private CqlLexer.Token Accept_78()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0005243D File Offset: 0x0005063D
		private CqlLexer.Token Accept_79()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0005244B File Offset: 0x0005064B
		private CqlLexer.Token Accept_80()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00052459 File Offset: 0x00050659
		private CqlLexer.Token Accept_81()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00052467 File Offset: 0x00050667
		private CqlLexer.Token Accept_82()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00052475 File Offset: 0x00050675
		private CqlLexer.Token Accept_83()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00052483 File Offset: 0x00050683
		private CqlLexer.Token Accept_84()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00052491 File Offset: 0x00050691
		private void yybegin(int state)
		{
			this.yy_lexical_state = state;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0005249C File Offset: 0x0005069C
		private char yy_advance()
		{
			if (this.yy_buffer_index < this.yy_buffer_read)
			{
				return CqlLexer.yy_translate.translate(this.yy_buffer[this.yy_buffer_index++]);
			}
			if (this.yy_buffer_start != 0)
			{
				int i = this.yy_buffer_start;
				int num = 0;
				while (i < this.yy_buffer_read)
				{
					this.yy_buffer[num] = this.yy_buffer[i];
					i++;
					num++;
				}
				this.yy_buffer_end -= this.yy_buffer_start;
				this.yy_buffer_start = 0;
				this.yy_buffer_read = num;
				this.yy_buffer_index = num;
				int num2 = this.yy_reader.Read(this.yy_buffer, this.yy_buffer_read, this.yy_buffer.Length - this.yy_buffer_read);
				if (num2 <= 0)
				{
					return '\u0081';
				}
				this.yy_buffer_read += num2;
			}
			while (this.yy_buffer_index >= this.yy_buffer_read)
			{
				if (this.yy_buffer_index >= this.yy_buffer.Length)
				{
					this.yy_buffer = this.yy_double(this.yy_buffer);
				}
				int num2 = this.yy_reader.Read(this.yy_buffer, this.yy_buffer_read, this.yy_buffer.Length - this.yy_buffer_read);
				if (num2 <= 0)
				{
					return '\u0081';
				}
				this.yy_buffer_read += num2;
			}
			return CqlLexer.yy_translate.translate(this.yy_buffer[this.yy_buffer_index++]);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00052604 File Offset: 0x00050804
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

		// Token: 0x0600143C RID: 5180 RVA: 0x00052670 File Offset: 0x00050870
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

		// Token: 0x0600143D RID: 5181 RVA: 0x00052705 File Offset: 0x00050905
		private void yy_mark_end()
		{
			this.yy_buffer_end = this.yy_buffer_index;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00052714 File Offset: 0x00050914
		private void yy_to_mark()
		{
			this.yy_buffer_index = this.yy_buffer_end;
			this.yy_at_bol = (this.yy_buffer_end > this.yy_buffer_start && (this.yy_buffer[this.yy_buffer_end - 1] == '\r' || this.yy_buffer[this.yy_buffer_end - 1] == '\n'));
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0005276D File Offset: 0x0005096D
		internal string yytext()
		{
			return new string(this.yy_buffer, this.yy_buffer_start, this.yy_buffer_end - this.yy_buffer_start);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0005278D File Offset: 0x0005098D
		internal int yy_char()
		{
			return this.yychar;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00052795 File Offset: 0x00050995
		private int yylength()
		{
			return this.yy_buffer_end - this.yy_buffer_start;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000527A4 File Offset: 0x000509A4
		private char[] yy_double(char[] buf)
		{
			char[] array = new char[2 * buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i];
			}
			return array;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000527D1 File Offset: 0x000509D1
		private void yy_error(int code, bool fatal)
		{
			if (fatal)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserFatalError"));
			}
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x000527E8 File Offset: 0x000509E8
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
						goto Block_8;
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
			Block_8:
			throw new EntitySqlException(EntitySqlException.GetGenericErrorMessage(this._query, this.yychar));
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0005292D File Offset: 0x00050B2D
		internal CqlLexer(string query, ParserOptions parserOptions) : this()
		{
			this._query = query;
			this._parserOptions = parserOptions;
			this.yy_reader = new StringReader(this._query);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00052954 File Offset: 0x00050B54
		internal static CqlLexer.Token NewToken(short tokenId, Node tokenvalue)
		{
			return new CqlLexer.Token(tokenId, tokenvalue);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0005295D File Offset: 0x00050B5D
		internal static CqlLexer.Token NewToken(short tokenId, CqlLexer.TerminalToken termToken)
		{
			return new CqlLexer.Token(tokenId, termToken);
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00052966 File Offset: 0x00050B66
		internal string YYText
		{
			get
			{
				return this.yytext();
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0005296E File Offset: 0x00050B6E
		internal int IPos
		{
			get
			{
				return this._iPos;
			}
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00052976 File Offset: 0x00050B76
		internal int AdvanceIPos()
		{
			this._iPos += this.YYText.Length;
			return this._iPos;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00052996 File Offset: 0x00050B96
		internal static bool IsReservedKeyword(string term)
		{
			return CqlLexer.InternalKeywordDictionary.ContainsKey(term);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000529A4 File Offset: 0x00050BA4
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

		// Token: 0x0600144D RID: 5197 RVA: 0x000529D4 File Offset: 0x00050BD4
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
			string errorDescription = Strings.InvalidEscapedIdentifier(symbol);
			throw EntitySqlException.Create(this._query, errorDescription, this._iPos, null, false, null);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00052A6C File Offset: 0x00050C6C
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

		// Token: 0x0600144F RID: 5199 RVA: 0x00052AED File Offset: 0x00050CED
		private bool IsCanonicalFunctionCall(string symbol, char lookAheadChar)
		{
			return lookAheadChar == '(' && CqlLexer.InternalCanonicalFunctionNames.Contains(symbol);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00052B04 File Offset: 0x00050D04
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
				string errorDescription = Strings.InvalidAliasName(symbol);
				throw EntitySqlException.Create(this._query, errorDescription, this._iPos, null, false, null);
			}
			Identifier identifier = new Identifier(symbol, false, this._query, this._iPos);
			identifier.ErrCtx.ErrorContextInfo = "CtxIdentifier";
			return CqlLexer.NewToken(CqlParser.IDENTIFIER, identifier);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00052B8C File Offset: 0x00050D8C
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

		// Token: 0x06001452 RID: 5202 RVA: 0x00052BCE File Offset: 0x00050DCE
		private bool IsInSymbolAsIdentifierState(char lookAheadChar)
		{
			return this._symbolAsIdentifierState || this._symbolAsAliasIdentifierState || this._symbolAsInlineFunctionNameState || lookAheadChar == '.';
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00052BEF File Offset: 0x00050DEF
		private void ResetSymbolAsIdentifierState(bool significant)
		{
			this._symbolAsIdentifierState = false;
			if (significant)
			{
				this._symbolAsAliasIdentifierState = false;
				this._symbolAsInlineFunctionNameState = false;
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00052C0C File Offset: 0x00050E0C
		internal CqlLexer.Token MapOperator(string oper)
		{
			if (CqlLexer.InternalOperatorDictionary.ContainsKey(oper))
			{
				return CqlLexer.NewToken(CqlLexer.InternalOperatorDictionary[oper], new CqlLexer.TerminalToken(oper, this._iPos));
			}
			string invalidOperatorSymbol = Strings.InvalidOperatorSymbol;
			throw EntitySqlException.Create(this._query, invalidOperatorSymbol, this._iPos, null, false, null);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00052C60 File Offset: 0x00050E60
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
			string invalidPunctuatorSymbol = Strings.InvalidPunctuatorSymbol;
			throw EntitySqlException.Create(this._query, invalidPunctuatorSymbol, this._iPos, null, false, null);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00052CCE File Offset: 0x00050ECE
		internal CqlLexer.Token MapDoubleQuotedString(string symbol)
		{
			return this.NewLiteralToken(symbol, LiteralKind.String);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00052CD8 File Offset: 0x00050ED8
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
					string errorDescription = Strings.InvalidLiteralFormat("binary", text);
					throw EntitySqlException.Create(this._query, errorDescription, this._iPos, null, false, null);
				}
				break;
			case LiteralKind.DateTime:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidDateTimeValue(text))
				{
					string errorDescription2 = Strings.InvalidLiteralFormat("datetime", text);
					throw EntitySqlException.Create(this._query, errorDescription2, this._iPos, null, false, null);
				}
				break;
			case LiteralKind.Time:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidTimeValue(text))
				{
					string errorDescription3 = Strings.InvalidLiteralFormat("time", text);
					throw EntitySqlException.Create(this._query, errorDescription3, this._iPos, null, false, null);
				}
				break;
			case LiteralKind.DateTimeOffset:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidDateTimeOffsetValue(text))
				{
					string errorDescription4 = Strings.InvalidLiteralFormat("datetimeoffset", text);
					throw EntitySqlException.Create(this._query, errorDescription4, this._iPos, null, false, null);
				}
				break;
			case LiteralKind.Guid:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidGuidValue(text))
				{
					string errorDescription5 = Strings.InvalidLiteralFormat("guid", text);
					throw EntitySqlException.Create(this._query, errorDescription5, this._iPos, null, false, null);
				}
				break;
			}
			return CqlLexer.NewToken(CqlParser.LITERAL, new Literal(text, literalKind, this._query, this._iPos));
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00052E4D File Offset: 0x0005104D
		internal CqlLexer.Token NewParameterToken(string param)
		{
			return CqlLexer.NewToken(CqlParser.PARAMETER, new QueryParameter(param, this._query, this._iPos));
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00052E6C File Offset: 0x0005106C
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
			string errorDescription = Strings.InvalidEscapedIdentifierUnbalanced(this.YYText);
			throw EntitySqlException.Create(this._query, errorDescription, this._iPos, null, false, null);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00052EF8 File Offset: 0x000510F8
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

		// Token: 0x0600145B RID: 5211 RVA: 0x00052F5E File Offset: 0x0005115E
		private static bool IsLetter(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00052F7B File Offset: 0x0005117B
		private static bool IsDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00052F8C File Offset: 0x0005118C
		private static bool isHexDigit(char c)
		{
			return CqlLexer.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00052FB4 File Offset: 0x000511B4
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

		// Token: 0x0600145F RID: 5215 RVA: 0x00052FE0 File Offset: 0x000511E0
		private static string GetLiteralSingleQuotePayload(string literal)
		{
			if (literal.Split(new char[]
			{
				'\''
			}).Length != 3 || -1 == literal.IndexOf('\'') || -1 == literal.LastIndexOf('\''))
			{
				string malformedSingleQuotePayload = Strings.MalformedSingleQuotePayload;
				throw new EntitySqlException(malformedSingleQuotePayload);
			}
			int num = literal.IndexOf('\'');
			string text = literal.Substring(num + 1, literal.Length - (num + 2));
			if (text.Split(new char[]
			{
				'\''
			}).Length != 1)
			{
				string malformedSingleQuotePayload2 = Strings.MalformedSingleQuotePayload;
				throw new EntitySqlException(malformedSingleQuotePayload2);
			}
			return text;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00053074 File Offset: 0x00051274
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

		// Token: 0x06001461 RID: 5217 RVA: 0x000530DC File Offset: 0x000512DC
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

		// Token: 0x06001462 RID: 5218 RVA: 0x0005311F File Offset: 0x0005131F
		private static bool IsValidDateTimeValue(string datetimeValue)
		{
			if (CqlLexer._reDateTimeValue == null)
			{
				CqlLexer._reDateTimeValue = new Regex("^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reDateTimeValue.IsMatch(datetimeValue);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00053147 File Offset: 0x00051347
		private static bool IsValidTimeValue(string timeValue)
		{
			if (CqlLexer._reTimeValue == null)
			{
				CqlLexer._reTimeValue = new Regex("^[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reTimeValue.IsMatch(timeValue);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0005316F File Offset: 0x0005136F
		private static bool IsValidDateTimeOffsetValue(string datetimeOffsetValue)
		{
			if (CqlLexer._reDateTimeOffsetValue == null)
			{
				CqlLexer._reDateTimeOffsetValue = new Regex("^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?([ ])*[\\+-][0-9]{1,2}:[0-9]{1,2}$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reDateTimeOffsetValue.IsMatch(datetimeOffsetValue);
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00053198 File Offset: 0x00051398
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

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00053618 File Offset: 0x00051818
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

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x000538AC File Offset: 0x00051AAC
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00053954 File Offset: 0x00051B54
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

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00053A74 File Offset: 0x00051C74
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

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00053B64 File Offset: 0x00051D64
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

		// Token: 0x0600146B RID: 5227 RVA: 0x00056E5C File Offset: 0x0005505C
		// Note: this type is marked as 'beforefieldinit'.
		static CqlLexer()
		{
			int[] array = new int[1];
			CqlLexer.yy_state_dtrans = array;
			CqlLexer.yy_error_string = new string[]
			{
				"Error: Internal error.\n",
				"Error: Unmatched input.\n"
			};
			CqlLexer.yy_acpt = new int[]
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
			CqlLexer.yy_cmap = new int[]
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
			CqlLexer.yy_rmap = new int[]
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
			CqlLexer.yy_nxt = new int[,]
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
			CqlLexer._stringComparer = StringComparer.OrdinalIgnoreCase;
			CqlLexer._newLineCharacters = new char[]
			{
				'\n',
				'\u0085',
				'\v',
				'\u2028',
				'\u2029'
			};
		}

		// Token: 0x04000648 RID: 1608
		private const int YY_BUFFER_SIZE = 512;

		// Token: 0x04000649 RID: 1609
		private const int YY_F = -1;

		// Token: 0x0400064A RID: 1610
		private const int YY_NO_STATE = -1;

		// Token: 0x0400064B RID: 1611
		private const int YY_NOT_ACCEPT = 0;

		// Token: 0x0400064C RID: 1612
		private const int YY_START = 1;

		// Token: 0x0400064D RID: 1613
		private const int YY_END = 2;

		// Token: 0x0400064E RID: 1614
		private const int YY_NO_ANCHOR = 4;

		// Token: 0x0400064F RID: 1615
		private const int YY_BOL = 128;

		// Token: 0x04000650 RID: 1616
		private const int YY_EOF = 129;

		// Token: 0x04000651 RID: 1617
		private const int YYINITIAL = 0;

		// Token: 0x04000652 RID: 1618
		private const int YY_E_INTERNAL = 0;

		// Token: 0x04000653 RID: 1619
		private const int YY_E_MATCH = 1;

		// Token: 0x04000654 RID: 1620
		private const string _datetimeValueRegularExpression = "^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$";

		// Token: 0x04000655 RID: 1621
		private const string _timeValueRegularExpression = "^[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$";

		// Token: 0x04000656 RID: 1622
		private const string _datetimeOffsetValueRegularExpression = "^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?([ ])*[\\+-][0-9]{1,2}:[0-9]{1,2}$";

		// Token: 0x04000657 RID: 1623
		private readonly CqlLexer.AcceptMethod[] accept_dispatch;

		// Token: 0x04000658 RID: 1624
		private readonly TextReader yy_reader;

		// Token: 0x04000659 RID: 1625
		private int yy_buffer_index;

		// Token: 0x0400065A RID: 1626
		private int yy_buffer_read;

		// Token: 0x0400065B RID: 1627
		private int yy_buffer_start;

		// Token: 0x0400065C RID: 1628
		private int yy_buffer_end;

		// Token: 0x0400065D RID: 1629
		private char[] yy_buffer;

		// Token: 0x0400065E RID: 1630
		private int yychar;

		// Token: 0x0400065F RID: 1631
		private int yyline;

		// Token: 0x04000660 RID: 1632
		private bool yy_at_bol;

		// Token: 0x04000661 RID: 1633
		private int yy_lexical_state;

		// Token: 0x04000662 RID: 1634
		private static readonly int[] yy_state_dtrans;

		// Token: 0x04000663 RID: 1635
		private bool yy_last_was_cr;

		// Token: 0x04000664 RID: 1636
		private static string[] yy_error_string;

		// Token: 0x04000665 RID: 1637
		private static readonly int[] yy_acpt;

		// Token: 0x04000666 RID: 1638
		private static readonly int[] yy_cmap;

		// Token: 0x04000667 RID: 1639
		private static readonly int[] yy_rmap;

		// Token: 0x04000668 RID: 1640
		private static readonly int[,] yy_nxt;

		// Token: 0x04000669 RID: 1641
		private static readonly StringComparer _stringComparer;

		// Token: 0x0400066A RID: 1642
		private static Dictionary<string, short> _keywords;

		// Token: 0x0400066B RID: 1643
		private static HashSet<string> _invalidAliasNames;

		// Token: 0x0400066C RID: 1644
		private static HashSet<string> _invalidInlineFunctionNames;

		// Token: 0x0400066D RID: 1645
		private static Dictionary<string, short> _operators;

		// Token: 0x0400066E RID: 1646
		private static Dictionary<string, short> _punctuators;

		// Token: 0x0400066F RID: 1647
		private static HashSet<string> _canonicalFunctionNames;

		// Token: 0x04000670 RID: 1648
		private static Regex _reDateTimeValue;

		// Token: 0x04000671 RID: 1649
		private static Regex _reTimeValue;

		// Token: 0x04000672 RID: 1650
		private static Regex _reDateTimeOffsetValue;

		// Token: 0x04000673 RID: 1651
		private int _iPos;

		// Token: 0x04000674 RID: 1652
		private int _lineNumber;

		// Token: 0x04000675 RID: 1653
		private ParserOptions _parserOptions;

		// Token: 0x04000676 RID: 1654
		private readonly string _query;

		// Token: 0x04000677 RID: 1655
		private bool _symbolAsIdentifierState;

		// Token: 0x04000678 RID: 1656
		private bool _symbolAsAliasIdentifierState;

		// Token: 0x04000679 RID: 1657
		private bool _symbolAsInlineFunctionNameState;

		// Token: 0x0400067A RID: 1658
		private static readonly char[] _newLineCharacters;

		// Token: 0x02000242 RID: 578
		// (Invoke) Token: 0x0600146D RID: 5229
		private delegate CqlLexer.Token AcceptMethod();

		// Token: 0x02000243 RID: 579
		internal class Token
		{
			// Token: 0x06001470 RID: 5232 RVA: 0x00056F14 File Offset: 0x00055114
			internal Token(short tokenId, Node tokenValue)
			{
				this._tokenId = tokenId;
				this._tokenValue = tokenValue;
			}

			// Token: 0x06001471 RID: 5233 RVA: 0x00056F2A File Offset: 0x0005512A
			internal Token(short tokenId, CqlLexer.TerminalToken terminal)
			{
				this._tokenId = tokenId;
				this._tokenValue = terminal;
			}

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06001472 RID: 5234 RVA: 0x00056F40 File Offset: 0x00055140
			internal short TokenId
			{
				get
				{
					return this._tokenId;
				}
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06001473 RID: 5235 RVA: 0x00056F48 File Offset: 0x00055148
			internal object Value
			{
				get
				{
					return this._tokenValue;
				}
			}

			// Token: 0x0400067B RID: 1659
			private readonly short _tokenId;

			// Token: 0x0400067C RID: 1660
			private readonly object _tokenValue;
		}

		// Token: 0x02000244 RID: 580
		internal class TerminalToken
		{
			// Token: 0x06001474 RID: 5236 RVA: 0x00056F50 File Offset: 0x00055150
			internal TerminalToken(string token, int iPos)
			{
				this._token = token;
				this._iPos = iPos;
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06001475 RID: 5237 RVA: 0x00056F66 File Offset: 0x00055166
			internal int IPos
			{
				get
				{
					return this._iPos;
				}
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06001476 RID: 5238 RVA: 0x00056F6E File Offset: 0x0005516E
			internal string Token
			{
				get
				{
					return this._token;
				}
			}

			// Token: 0x0400067D RID: 1661
			private readonly string _token;

			// Token: 0x0400067E RID: 1662
			private readonly int _iPos;
		}

		// Token: 0x02000245 RID: 581
		internal static class yy_translate
		{
			// Token: 0x06001477 RID: 5239 RVA: 0x00056F78 File Offset: 0x00055178
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
