using System;
using System.Collections;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;

namespace Spire.Doc.Documents
{
	// Token: 0x02000170 RID: 368
	public class TextSelection : IEnumerable
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000D2BBC File Offset: 0x000D1BBC
		public string SelectedText
		{
			get
			{
				int a_ = 14;
				int num = 0;
				int num3;
				int num4;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 1:
						num = 12;
						continue;
					case 2:
						num = 11;
						continue;
					case 3:
						goto IL_EE;
					case 4:
						num2 = this.ᜂ.StartPos + this.ᜅ - num3;
						goto IL_D0;
					case 5:
						if (this.ᜅ >= 0)
						{
							num = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_155;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 6:
						goto IL_10E;
					case 7:
						goto IL_155;
					case 8:
						if (this.ᜄ != 0)
						{
							num = 10;
							continue;
						}
						goto IL_88;
					case 9:
						if (num4 < 0)
						{
							num = 3;
							continue;
						}
						goto IL_1C4;
					case 10:
						num = 7;
						continue;
					case 11:
						if (this.ᜂ == null)
						{
							num = 6;
							continue;
						}
						num3 = this.ᜁ.StartPos;
						num = 8;
						continue;
					case 12:
						num2 = this.ᜂ.StartPos + this.ᜂ.TextLength - num3;
						goto IL_D0;
					case 13:
						num3 += this.ᜄ;
						num = 14;
						continue;
					case 14:
						goto IL_88;
					}
					if (this.ᜁ != null)
					{
						num = 2;
						continue;
					}
					break;
					IL_88:
					num = 5;
					continue;
					IL_155:
					if (this.ᜁ.SafeText)
					{
						num = 13;
						continue;
					}
					goto IL_88;
					IL_D0:
					num4 = num2;
					num = 9;
				}
				IL_CA:
				return string.Empty;
				IL_EE:
				throw new Exception(ClipboardData.b("⁳፵w๹屻ൽﲇ낏뢗瞧즟쒡춣쎥첧蒩貫節\ud8af\udbb1잳隵\udbb7햹즻튽꒿ꛃꏅ껉ꏋꃍ뗏ꏓ뻕뇗뛙맛ﻝ跟跡胣迥軧菩迫迭蓯鯱鯳飵\ud8f7闹髻\udefd珿洁焃琅欇漉Ⰻ樍缏焑愓笕紗琙栛〝", a_));
				IL_10E:
				goto IL_CA;
				IL_1C4:
				return this.OwnerParagraph.Text.Substring(num3, num4);
			}
		}

		// Token: 0x17000249 RID: 585
		public string this[int index]
		{
			get
			{
				string text;
				for (;;)
				{
					TextRange textRange = this.ᜃ[index];
					text = textRange.Text;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (index != 0)
							{
								goto IL_AE;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E1;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num = 6;
								continue;
							}
							break;
						case 1:
							text = text.Substring(0, this.ᜅ - this.ᜄ);
							num = 7;
							continue;
						case 2:
							if (this.ᜅ != -1)
							{
								num = 1;
								continue;
							}
							return text;
						case 3:
							num = 2;
							continue;
						case 4:
							goto IL_E1;
						case 5:
							if (index == this.ᜃ.Count - 1)
							{
								num = 3;
								continue;
							}
							return text;
						case 6:
							num = 4;
							continue;
						case 7:
							return text;
						case 8:
							text = text.Substring(this.ᜄ);
							num = 9;
							continue;
						case 9:
							goto IL_AE;
						}
						break;
						IL_AE:
						num = 5;
						continue;
						IL_E1:
						if (this.ᜄ <= 0)
						{
							goto IL_AE;
						}
						num = 8;
					}
				}
				return text;
			}
			set
			{
				TextRange textRange;
				string text;
				for (;;)
				{
					textRange = this.ᜃ[index];
					text = value;
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							goto IL_110;
						case 2:
							if (this.ᜅ != -1)
							{
								num = 8;
								continue;
							}
							goto IL_13B;
						case 3:
							goto IL_9B;
						case 4:
							goto IL_CF;
						case 5:
							num = 4;
							continue;
						case 6:
							text = textRange.Text.Substring(0, this.ᜄ) + text;
							num = 3;
							continue;
						case 7:
							if (index == this.ᜃ.Count)
							{
								num = 0;
								continue;
							}
							goto IL_13B;
						case 8:
							text += textRange.Text.Substring(this.ᜅ);
							if (true)
							{
							}
							num = 1;
							continue;
						case 9:
							if (index != 0)
							{
								goto IL_9B;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CF;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						}
						break;
						IL_9B:
						num = 7;
						continue;
						IL_CF:
						if (this.ᜄ <= 0)
						{
							goto IL_9B;
						}
						num = 6;
					}
				}
				IL_110:
				IL_13B:
				textRange.Text = text;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x000D3034 File Offset: 0x000D2034
		public int Count
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
				return this.ᜃ.Count;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x000D307C File Offset: 0x000D207C
		internal Paragraph OwnerParagraph
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_77;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜀ = this.ᜁ.OwnerParagraph;
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_75;
					}
					if (this.ᜁ == null)
					{
						break;
					}
					num = 0;
				}
				IL_75:
				IL_77:
				return this.ᜀ;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000D3108 File Offset: 0x000D2108
		internal TextRange StartTextRange
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
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000D314C File Offset: 0x000D214C
		internal TextRange EndTextRange
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
				return this.ᜂ;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000D3190 File Offset: 0x000D2190
		public TextSelection(Paragraph para, int startCharPos, int endCharPos)
		{
			this.ᜀ = para;
			if (this.ᜀ.Items.Count == 0)
			{
				return;
			}
			TextRange textRange;
			this.ᜆ = spr\u1AB5.ᜀ(this.ᜀ, startCharPos + 1, out textRange);
			if (textRange == null)
			{
				return;
			}
			this.ᜄ = startCharPos - textRange.StartPos;
			this.ᜁ = textRange;
			this.ᜇ = spr\u1AB5.ᜀ(this.ᜀ, endCharPos, out textRange);
			if (this.ᜇ < this.ᜆ)
			{
				goto IL_108;
			}
			if (textRange == null)
			{
				goto IL_108;
			}
			this.ᜅ = endCharPos - textRange.StartPos;
			IL_A9:
			this.ᜂ = textRange;
			if (this.ᜅ == textRange.TextLength)
			{
				this.ᜅ = -1;
			}
			for (int i = this.ᜆ; i <= this.ᜇ; i++)
			{
				textRange = (this.ᜀ.Items[i] as TextRange);
				if (textRange != null)
				{
					this.ᜃ.Add(textRange);
				}
			}
			return;
			IL_108:
			for (int j = para.Items.Count; j > 0; j--)
			{
				ParagraphBase paragraphBase = para[j - 1];
				if (paragraphBase is TextRange)
				{
					textRange = (paragraphBase as TextRange);
					IL_3E:
					this.ᜅ = endCharPos - textRange.StartPos - 1;
					goto IL_A9;
				}
			}
			goto IL_3E;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000D3308 File Offset: 0x000D2308
		public TextRange[] GetRanges()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					goto IL_BA;
				case 2:
					goto IL_50;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_97;
					default:
						if (false)
						{
						}
						if (this.ᜅ != -1)
						{
							num = 1;
							continue;
						}
						goto IL_CD;
					}
					break;
				case 5:
					if (this.ᜄ <= 0)
					{
						num = 0;
						continue;
					}
					goto IL_BA;
				case 6:
					goto IL_CB;
				}
				if (this.OwnerParagraph.Items.Count == 0)
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				this.ᜀ();
				IL_97:
				num = 5;
				continue;
				IL_BA:
				this.ᜆ();
				num = 6;
			}
			IL_50:
			return null;
			IL_CB:
			IL_CD:
			return this.ᜃ.ToArray();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000D33F0 File Offset: 0x000D23F0
		public TextRange GetAsOneRange()
		{
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜅ != -1)
					{
						num = 9;
						continue;
					}
					goto IL_94;
				case 1:
					goto IL_65;
				case 2:
					goto IL_140;
				case 3:
					num = 0;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_140;
					default:
						if (false)
						{
						}
						if (this.Count > 1)
						{
							num = 11;
							continue;
						}
						goto IL_1AB;
					}
					break;
				case 5:
					goto IL_165;
				case 6:
					goto IL_94;
				case 7:
					if (this.ᜃ.Count <= 1)
					{
						num = 13;
						continue;
					}
					this.ᜃ[1].RemoveSelf();
					this.ᜃ.RemoveAt(1);
					num = 8;
					continue;
				case 8:
					goto IL_103;
				case 9:
					goto IL_167;
				case 11:
				{
					string selectedText = this.SelectedText;
					num = 2;
					continue;
				}
				case 12:
					if (this.ᜄ <= 0)
					{
						num = 3;
						continue;
					}
					goto IL_167;
				case 13:
				{
					string selectedText;
					this.ᜁ.Text = selectedText;
					this.ᜂ = this.ᜁ;
					num = 5;
					continue;
				}
				}
				if (this.OwnerParagraph.Items.Count == 0)
				{
					num = 1;
					continue;
				}
				this.ᜀ();
				num = 12;
				continue;
				IL_94:
				if (true)
				{
				}
				num = 4;
				continue;
				IL_103:
				num = 7;
				continue;
				IL_140:
				goto IL_103;
				IL_167:
				this.ᜆ();
				num = 6;
			}
			IL_65:
			return null;
			IL_165:
			IL_1AB:
			return this.ᜃ[0];
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000D35B4 File Offset: 0x000D25B4
		internal int ᜄ()
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜅ != -1)
					{
						num = 8;
						continue;
					}
					goto IL_91;
				case 1:
					goto IL_15E;
				case 2:
					num = 9;
					continue;
				case 3:
					return 0;
				case 4:
					num = 0;
					continue;
				case 5:
					if (this.ᜄ <= 0)
					{
						num = 4;
						continue;
					}
					goto IL_160;
				case 7:
					this.ᜁ = null;
					this.ᜂ = this.ᜁ;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return 0;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 8:
					goto IL_160;
				case 9:
					goto IL_EC;
				case 10:
					if (this.Count > 0)
					{
						num = 2;
						continue;
					}
					goto IL_1A2;
				case 11:
					goto IL_EC;
				case 12:
					if (this.ᜃ.Count <= 0)
					{
						num = 7;
						continue;
					}
					this.ᜃ[0].RemoveSelf();
					this.ᜃ.RemoveAt(0);
					num = 11;
					continue;
				case 13:
					goto IL_91;
				}
				if (this.OwnerParagraph.Items.Count == 0)
				{
					num = 3;
					continue;
				}
				this.ᜀ();
				num = 5;
				continue;
				IL_91:
				num = 10;
				continue;
				IL_EC:
				num = 12;
				continue;
				IL_160:
				if (true)
				{
				}
				this.ᜆ();
				num = 13;
			}
			return 0;
			IL_15E:
			IL_1A2:
			return this.ᜆ;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000D376C File Offset: 0x000D276C
		internal void ᜂ()
		{
			int num = 5;
			for (;;)
			{
				IL_12:
				switch (num)
				{
				case 0:
					goto IL_CC;
				case 1:
					return;
				case 2:
					goto IL_CC;
				case 3:
				{
					TextRange[] ranges = this.GetRanges();
					num = 6;
					continue;
				}
				case 4:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 1;
						continue;
					}
					TextRange[] ranges;
					this.ᜈ[num2] = (TextRange)ranges[num2].Clone();
					num2++;
					num = 0;
					continue;
				}
				case 5:
					if (true)
					{
					}
					break;
				case 6:
				{
					TextRange[] ranges;
					while (ranges == null)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 7;
							goto IL_12;
						}
					}
					this.ᜈ = new TextRange[ranges.Length];
					int num2 = 0;
					int num3 = ranges.Length;
					num = 2;
					continue;
				}
				case 7:
					return;
				}
				if (this.ᜈ == null)
				{
					num = 3;
					continue;
				}
				return;
				IL_CC:
				num = 4;
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000D3870 File Offset: 0x000D2870
		internal void ᜀ(Paragraph A_0, int A_1, bool A_2, CharacterFormat A_3)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜂ();
					TextRange[] array = this.ᜈ;
					int num = 0;
					int num2 = 4;
					for (;;)
					{
						TextRange textRange2;
						switch (num2)
						{
						case 0:
							goto IL_EF;
						case 1:
							goto IL_ED;
						case 2:
						{
							if (num >= array.Length)
							{
								num2 = 5;
								continue;
							}
							TextRange textRange = array[num];
							textRange2 = (TextRange)textRange.Clone();
							num2 = 3;
							continue;
						}
						case 3:
							if (A_2)
							{
								num2 = 1;
								continue;
							}
							goto IL_7B;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_ED;
							default:
								if (false)
								{
								}
								goto IL_EF;
							}
							break;
						case 5:
							return;
						case 6:
							textRange2.CharacterFormat.ImportContainer(A_3);
							num2 = 8;
							continue;
						case 7:
							if (A_3 != null)
							{
								num2 = 6;
								continue;
							}
							goto IL_7B;
						case 8:
							goto IL_7B;
						}
						break;
						IL_7B:
						if (true)
						{
						}
						A_0.Items.Insert(A_1, textRange2);
						A_1++;
						num++;
						num2 = 0;
						continue;
						IL_ED:
						num2 = 7;
						continue;
						IL_EF:
						num2 = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000D39B0 File Offset: 0x000D29B0
		public IEnumerator GetEnumerator()
		{
			string[] array;
			for (;;)
			{
				array = new string[this.Count];
				int num = 0;
				int count = this.Count;
				if (true)
				{
				}
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_49;
					case 1:
						goto IL_79;
					case 2:
						goto IL_49;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							break;
						}
						array[num] = this[num];
						num++;
						num2 = 2;
						continue;
					}
					break;
					IL_49:
					num2 = 3;
				}
			}
			IL_79:
			return array.GetEnumerator();
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x000D3A5C File Offset: 0x000D2A5C
		private void ᜀ()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int count;
					if (this.ᜇ < count)
					{
						num = 6;
						continue;
					}
					goto IL_B6;
				}
				case 1:
					num = 8;
					continue;
				case 2:
					num = 7;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1AA;
					default:
						if (false)
						{
						}
						goto IL_B6;
					}
					break;
				case 5:
					goto IL_D2;
				case 6:
					num = 12;
					continue;
				case 7:
				{
					if (this.ᜂ.Owner != this.OwnerParagraph)
					{
						num = 13;
						continue;
					}
					int count = this.OwnerParagraph.Items.Count;
					goto IL_1AA;
				}
				case 8:
					if (this.ᜁ != this.OwnerParagraph.Items[this.ᜆ])
					{
						num = 9;
						continue;
					}
					goto IL_65;
				case 9:
					goto IL_8A;
				case 10:
				{
					int count;
					if (this.ᜆ < count)
					{
						num = 1;
						continue;
					}
					goto IL_8A;
				}
				case 11:
					goto IL_65;
				case 12:
					if (this.ᜂ != this.OwnerParagraph.Items[this.ᜇ])
					{
						num = 3;
						continue;
					}
					return;
				case 13:
					goto IL_103;
				}
				if (this.ᜁ.Owner == this.OwnerParagraph)
				{
					num = 2;
					continue;
				}
				break;
				IL_65:
				num = 0;
				continue;
				IL_8A:
				this.ᜆ = this.ᜁ.ឯ();
				if (true)
				{
				}
				num = 11;
				continue;
				IL_B6:
				this.ᜇ = this.ᜂ.ឯ();
				num = 5;
				continue;
				IL_1AA:
				num = 10;
			}
			IL_B0:
			throw new InvalidOperationException();
			IL_D2:
			return;
			IL_103:
			goto IL_B0;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x000D3C38 File Offset: 0x000D2C38
		internal void ᜆ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.Count == 1)
					{
						num = 2;
						continue;
					}
					goto IL_10E;
				case 2:
					num = 7;
					continue;
				case 3:
					goto IL_29B;
				case 4:
					if (this.ᜉ != null)
					{
						num = 9;
						continue;
					}
					goto IL_29B;
				case 5:
					this.ᜀ(false);
					if (true)
					{
					}
					num = 13;
					continue;
				case 6:
				{
					TextRange textRange = new TextRange(this.OwnerParagraph.Document);
					textRange.Text = this.ᜂ.Text.Substring(this.ᜅ);
					textRange.CharacterFormat.ImportContainer(this.ᜂ.CharacterFormat);
					this.ᜂ.Text = this.ᜂ.Text.Substring(0, this.ᜅ);
					this.OwnerParagraph.Items.Insert(this.ᜇ + 1, textRange);
					num = 8;
					continue;
				}
				case 7:
					if (this.ᜅ >= 0)
					{
						num = 11;
						continue;
					}
					goto IL_10E;
				case 8:
					if (this.ᜉ != null)
					{
						num = 5;
						continue;
					}
					goto IL_263;
				case 9:
					this.ᜀ(true);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17C;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 10:
					return;
				case 11:
					this.ᜅ -= this.ᜄ;
					num = 15;
					continue;
				case 12:
					goto IL_23C;
				case 13:
					goto IL_17C;
				case 14:
					if (this.ᜅ > 0)
					{
						num = 6;
						continue;
					}
					return;
				case 15:
					goto IL_10E;
				case 16:
				{
					TextRange textRange2 = new TextRange(this.OwnerParagraph.Document);
					textRange2.Text = this.ᜁ.Text.Substring(0, this.ᜄ);
					textRange2.CharacterFormat.ImportContainer(this.ᜁ.CharacterFormat);
					this.ᜁ.Text = this.ᜁ.Text.Substring(this.ᜄ);
					this.OwnerParagraph.Items.Insert(this.ᜆ, textRange2);
					this.ᜆ++;
					this.ᜇ++;
					num = 4;
					continue;
				}
				}
				if (this.ᜄ > 0)
				{
					num = 16;
					continue;
				}
				goto IL_23C;
				IL_10E:
				this.ᜄ = 0;
				num = 12;
				continue;
				IL_23C:
				num = 14;
				continue;
				IL_263:
				this.ᜅ = -1;
				num = 10;
				continue;
				IL_17C:
				goto IL_263;
				IL_29B:
				num = 1;
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000D3F28 File Offset: 0x000D2F28
		private void ᜀ(bool A_0)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch (0)
				{
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_36;
					}
					break;
				}
			}
			IL_36:
			if (false)
			{
			}
			using (List<TextSelection>.Enumerator enumerator = this.ᜉ.GetEnumerator())
			{
				int num = 23;
				for (;;)
				{
					TextSelection textSelection;
					int num2;
					TextRange textRange;
					int num3;
					TextRange textRange2;
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 1:
						textSelection.ᜂ = (TextRange)this.ᜂ.NextSibling;
						textSelection.ᜃ[textSelection.ᜃ.Count - 1] = textSelection.ᜂ;
						num = 6;
						continue;
					case 2:
						num = 10;
						continue;
					case 3:
						num2 = this.ᜄ;
						goto IL_2A9;
					case 4:
						num = 14;
						continue;
					case 5:
						goto IL_144;
					case 6:
						goto IL_1E6;
					case 8:
						if (!A_0)
						{
							num = 24;
							continue;
						}
						num = 9;
						continue;
					case 9:
						textRange = this.ᜁ;
						goto IL_1C5;
					case 10:
						if (!A_0)
						{
							num = 1;
							continue;
						}
						goto IL_1E6;
					case 11:
						textRange = this.ᜂ;
						goto IL_1C5;
					case 12:
						num2 = this.ᜅ;
						goto IL_2A9;
					case 13:
						if (textSelection.ᜅ >= 0)
						{
							num = 17;
							continue;
						}
						break;
					case 14:
						goto IL_33E;
					case 15:
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						textSelection = enumerator.Current;
						num = 19;
						continue;
					case 16:
						textSelection.ᜁ = (TextRange)this.ᜂ.NextSibling;
						textSelection.ᜃ[0] = textSelection.ᜁ;
						num = 21;
						continue;
					case 17:
						textSelection.ᜅ -= num3;
						num = 7;
						continue;
					case 18:
						num = 26;
						continue;
					case 19:
						if (textSelection != this)
						{
							num = 25;
							continue;
						}
						break;
					case 20:
						if (textSelection.ᜁ == textRange2)
						{
							num = 18;
							continue;
						}
						goto IL_144;
					case 21:
						goto IL_2F3;
					case 22:
						if (textSelection.ᜂ == textRange2)
						{
							num = 2;
							continue;
						}
						break;
					case 24:
						num = 11;
						continue;
					case 25:
						num = 8;
						continue;
					case 26:
						if (!A_0)
						{
							num = 16;
							continue;
						}
						goto IL_2F3;
					case 27:
						if (!A_0)
						{
							num = 0;
							continue;
						}
						num = 3;
						continue;
					}
					goto IL_CC;
					IL_144:
					num = 22;
					continue;
					IL_1C5:
					textRange2 = textRange;
					num = 27;
					continue;
					IL_1E6:
					num = 13;
					continue;
					IL_20C:
					num = 15;
					continue;
					IL_CC:
					goto IL_20C;
					IL_2A9:
					num3 = num2;
					num = 20;
					continue;
					IL_2F3:
					textSelection.ᜄ -= num3;
					num = 5;
				}
				IL_33E:;
			}
		}

		// Token: 0x04001436 RID: 5174
		private Paragraph ᜀ;

		// Token: 0x04001437 RID: 5175
		private TextRange ᜁ;

		// Token: 0x04001438 RID: 5176
		private TextRange ᜂ;

		// Token: 0x04001439 RID: 5177
		private byte[] \u2593\u0086\u00AF\u00AD;

		// Token: 0x0400143A RID: 5178
		private List<TextRange> ᜃ = new List<TextRange>();

		// Token: 0x0400143B RID: 5179
		private float[] \u2593\u0084\u009B\u0096;

		// Token: 0x0400143C RID: 5180
		private int ᜄ;

		// Token: 0x0400143D RID: 5181
		private int ᜅ;

		// Token: 0x0400143E RID: 5182
		private byte \u25D9\u00AD\u00AC\u009D;

		// Token: 0x0400143F RID: 5183
		private int ᜆ;

		// Token: 0x04001440 RID: 5184
		private byte \u2593\u00A5\u009E\u008A;

		// Token: 0x04001441 RID: 5185
		private int ᜇ;

		// Token: 0x04001442 RID: 5186
		private TextRange[] ᜈ;

		// Token: 0x04001443 RID: 5187
		private long[] \u25D9\u009D\u0085\u0086;

		// Token: 0x04001444 RID: 5188
		private float[] \u2593\u00A8\u0089\u008E;

		// Token: 0x04001445 RID: 5189
		internal spr\u226E ᜉ;
	}
}
