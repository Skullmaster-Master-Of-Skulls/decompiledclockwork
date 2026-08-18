using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x02000493 RID: 1171
	public class TextBodySelection
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x003B06B4 File Offset: 0x003AF6B4
		public Body TextBody
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x003B06F8 File Offset: 0x003AF6F8
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x003B073C File Offset: 0x003AF73C
		public int ItemStartIndex
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜁ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x003B0780 File Offset: 0x003AF780
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x003B07C4 File Offset: 0x003AF7C4
		public int ItemEndIndex
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜂ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x003B0808 File Offset: 0x003AF808
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x003B084C File Offset: 0x003AF84C
		public int ParagraphItemStartIndex
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜃ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x003B0890 File Offset: 0x003AF890
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x003B08D4 File Offset: 0x003AF8D4
		public int ParagraphItemEndIndex
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜄ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x003B0918 File Offset: 0x003AF918
		public TextBodySelection(ParagraphBase itemStart, ParagraphBase itemEnd)
		{
			int a_ = 1;
			this.ᜁ = -1;
			this.ᜂ = -1;
			this.ᜃ = -1;
			this.ᜄ = -1;
			base..ctor();
			Paragraph ownerParagraph = itemStart.OwnerParagraph;
			Paragraph ownerParagraph2 = itemEnd.OwnerParagraph;
			if (ownerParagraph.Owner != ownerParagraph2.Owner)
			{
				throw new ArgumentException(ClipboardData.b("๦ᵨ๪l㱮հቲݴͶ奸᩺፼᭾ꆀ캊놐ﺒ뮚ﾜ爵膠삢쪤즦\udda8쪪쒬솮풰ힲ閴\udeb6ힸ鮺튼톾꓀뇄ꋆ뇈뿊귎뻐럒곔", a_));
			}
			this.ᜀ = ownerParagraph.OwnerTextBody;
			this.ᜁ = ownerParagraph.ឯ();
			this.ᜂ = ownerParagraph2.ឯ();
			this.ᜃ = itemStart.ឯ();
			this.ᜄ = itemEnd.ឯ();
			this.ᜀ();
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x003B09C4 File Offset: 0x003AF9C4
		public TextBodySelection(IBody textBody, int itemStartIndex, int itemEndIndex, int pItemStartIndex, int pItemEndIndex)
		{
			int a_ = 19;
			this.ᜁ = -1;
			this.ᜂ = -1;
			this.ᜃ = -1;
			this.ᜄ = -1;
			base..ctor();
			if (textBody == null)
			{
				throw new ArgumentNullException(ClipboardData.b("൸Ṻռ୾쎀ﺆ", a_));
			}
			this.ᜀ = (Body)textBody;
			this.ᜁ = itemStartIndex;
			this.ᜂ = itemEndIndex;
			this.ᜃ = pItemStartIndex;
			this.ᜄ = pItemEndIndex;
			this.ᜀ();
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x003B0A48 File Offset: 0x003AFA48
		internal int ᜀ(int A_0, int A_1)
		{
			int num;
			for (;;)
			{
				num = this.ᜂ - this.ᜁ;
				this.ᜁ += num;
				this.ᜂ += A_0;
				num = this.ᜄ - this.ᜃ;
				this.ᜃ += num;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_AF;
					case 1:
						if (this.ᜁ == this.ᜂ)
						{
							goto IL_7A;
						}
						goto IL_AF;
					case 2:
						this.ᜃ++;
						this.ᜄ += A_1 + 1;
						num2 = 0;
						continue;
					}
					break;
					IL_7A:
					num2 = 2;
					continue;
					IL_AF:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7A;
					default:
						goto IL_C5;
					}
				}
			}
			IL_C5:
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ();
			return num;
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x003B0B30 File Offset: 0x003AFB30
		private void ᜀ()
		{
			int a_ = 3;
			int num = 4;
			Paragraph paragraph;
			Paragraph paragraph2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜄ >= 0)
					{
						num = 15;
						continue;
					}
					goto IL_19C;
				case 1:
					if (this.ᜂ >= this.ᜁ)
					{
						num = 12;
						continue;
					}
					goto IL_316;
				case 2:
					num = 10;
					continue;
				case 3:
					if (this.ᜁ >= this.ᜀ.Items.Count)
					{
						num = 17;
						continue;
					}
					num = 1;
					continue;
				case 5:
					if (this.ᜂ >= this.ᜀ.Items.Count)
					{
						num = 7;
						continue;
					}
					paragraph = (this.ᜀ.Items[this.ᜁ] as Paragraph);
					paragraph2 = (this.ᜀ.Items[this.ᜂ] as Paragraph);
					num = 16;
					continue;
				case 6:
					num = 0;
					continue;
				case 7:
					goto IL_279;
				case 8:
					if (paragraph2 != null)
					{
						num = 6;
						continue;
					}
					return;
				case 9:
					goto IL_2DB;
				case 10:
					if (this.ᜃ > paragraph.Items.Count)
					{
						num = 9;
						continue;
					}
					goto IL_8A;
				case 11:
					goto IL_127;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21C;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 13:
					num = 19;
					continue;
				case 14:
					if (this.ᜄ > paragraph2.Items.Count)
					{
						num = 11;
						continue;
					}
					return;
				case 15:
					num = 14;
					continue;
				case 16:
					if (paragraph != null)
					{
						goto IL_21C;
					}
					goto IL_8A;
				case 17:
					goto IL_311;
				case 18:
					num = 3;
					continue;
				case 19:
					if (this.ᜃ >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_165;
				}
				if (true)
				{
				}
				if (this.ᜁ >= 0)
				{
					num = 18;
					continue;
				}
				goto IL_129;
				IL_8A:
				num = 8;
				continue;
				IL_21C:
				num = 13;
			}
			IL_127:
			goto IL_19C;
			IL_129:
			throw new ArgumentOutOfRangeException(ClipboardData.b("Ѩ㑪Ѭ᭮ᑰṲ♴Ͷᡸॺॼ㙾ﾆ", a_), ClipboardData.b("Ѩ㑪Ѭ᭮ᑰṲ♴Ͷᡸॺॼ㙾ﾆꦈﺌ꾎﶐릘ﺞ쾠莢閤螦욨\ud9aa趬좮쎰횲풴쎶\udcb8즺鶼쮾꧀ꋂꯄ", a_) + this.ᜀ.Items.Count);
			IL_165:
			throw new ArgumentOutOfRangeException(ClipboardData.b("Ѩ㑪ᵬ♮հᙲᡴ⑶൸ོ᩺୾좀", a_), ClipboardData.b("Ѩ㑪ᵬ♮հᙲᡴ⑶൸ོ᩺୾좀ꮊﲎ놐ﾒ뮚삠춢薤鞦覨쒪\udfac辮횰솲킴횶춸\udeba쾼龾뗀ꯂ꓄꧆", a_) + paragraph.Items.Count);
			IL_19C:
			throw new ArgumentOutOfRangeException(ClipboardData.b("Ѩ㑪ᵬ♮հᙲᡴ㉶᝸ὺ㑼ᅾﶄ", a_), ClipboardData.b("Ѩ㑪ᵬ♮հᙲᡴ㉶᝸ὺ㑼ᅾﶄꞆ권랖ﲜ膠鎢薤좦\udba8讪쪬\uddae풰튲솴튶쮸鮺즼ힾꃀ귂", a_) + paragraph2.Items.Count);
			IL_279:
			goto IL_316;
			IL_2DB:
			goto IL_165;
			IL_311:
			goto IL_129;
			IL_316:
			throw new ArgumentOutOfRangeException(ClipboardData.b("Ѩ㑪Ѭ᭮ᑰṲぴ᥶ᵸ㉺፼᭾ﮂ", a_), string.Concat(new object[]
			{
				ClipboardData.b("Ѩ㑪Ѭ᭮ᑰṲぴ᥶ᵸ㉺፼᭾ﮂꖄ愈ꮊ떔漢뾞", a_),
				this.ᜁ,
				ClipboardData.b("䥨ѪὬ佮ᙰŲၴᙶ൸Ṻོ彾ꦈ", a_),
				this.ᜀ.Items.Count
			}));
		}

		// Token: 0x04002F8E RID: 12174
		private Body ᜀ;

		// Token: 0x04002F8F RID: 12175
		private byte[] \u2460\u00A1\u0093\u00A7;

		// Token: 0x04002F90 RID: 12176
		private float \u2593\u0080\u009A\u00A0;

		// Token: 0x04002F91 RID: 12177
		private int ᜁ;

		// Token: 0x04002F92 RID: 12178
		private int[] \u2593\u008D\u00A8\u00A6;

		// Token: 0x04002F93 RID: 12179
		private bool \u2609\u0089\u0082\u0083;

		// Token: 0x04002F94 RID: 12180
		private byte \u25D9\u00AF\u00AD\u0091;

		// Token: 0x04002F95 RID: 12181
		private int ᜂ;

		// Token: 0x04002F96 RID: 12182
		private int ᜃ;

		// Token: 0x04002F97 RID: 12183
		private int ᜄ;
	}
}
