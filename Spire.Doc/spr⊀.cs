using System;
using System.Collections.Generic;
using System.IO;
using Spire.Doc.Collections;
using Spire.Doc.Core;

// Token: 0x0200020C RID: 524
[CLSCompliant(false)]
internal class spr\u2280 : spr\u23F8
{
	// Token: 0x0600189E RID: 6302 RVA: 0x00177EE0 File Offset: 0x00176EE0
	internal SortedItemList<int, sprᝑ> ᜆ()
	{
		if (this.ᜄ.ContainsKey(WordSubdocument.Main))
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return this.ᜄ[WordSubdocument.Main];
			}
		}
		return null;
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x00177F3C File Offset: 0x00176F3C
	internal SortedItemList<int, sprᝑ> ᜀ()
	{
		if (this.ᜄ.ContainsKey(WordSubdocument.HeaderFooter))
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return this.ᜄ[WordSubdocument.HeaderFooter];
			}
		}
		return null;
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x00177F98 File Offset: 0x00176F98
	internal SortedItemList<int, sprᝑ> ᜂ()
	{
		if (this.ᜄ.ContainsKey(WordSubdocument.Footnote))
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
				if (true)
				{
				}
				return this.ᜄ[WordSubdocument.Footnote];
			}
		}
		return null;
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x00177FF4 File Offset: 0x00176FF4
	internal SortedItemList<int, sprᝑ> ᜄ()
	{
		if (this.ᜄ.ContainsKey(WordSubdocument.Annotation))
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
				if (true)
				{
				}
				return this.ᜄ[WordSubdocument.Annotation];
			}
		}
		return null;
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x00178050 File Offset: 0x00177050
	internal SortedItemList<int, sprᝑ> ᜅ()
	{
		if (this.ᜄ.ContainsKey(WordSubdocument.Endnote))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜄ[WordSubdocument.Endnote];
			}
		}
		return null;
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x001780AC File Offset: 0x001770AC
	internal SortedItemList<int, sprᝑ> ᜁ()
	{
		if (this.ᜄ.ContainsKey(WordSubdocument.TextBox))
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return this.ᜄ[WordSubdocument.TextBox];
			}
		}
		return null;
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x00178108 File Offset: 0x00177108
	internal SortedItemList<int, sprᝑ> ᜈ()
	{
		if (this.ᜄ.ContainsKey(WordSubdocument.HeaderTextBox))
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return this.ᜄ[WordSubdocument.HeaderTextBox];
			}
		}
		return null;
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x00178164 File Offset: 0x00177164
	internal spr\u2280(sprᾱ A_0, BinaryReader A_1)
	{
		this.ᜄ = new Dictionary<WordSubdocument, SortedItemList<int, sprᝑ>>();
		this.ᜂ = A_1;
		this.ᜀ(WordSubdocument.Main, A_0.អ(), A_0.\u1734());
		this.ᜀ(WordSubdocument.HeaderFooter, A_0.\u171C(), A_0.\u17CA());
		this.ᜀ(WordSubdocument.Footnote, A_0.ᜤ(), A_0.ᜋ());
		this.ᜀ(WordSubdocument.Annotation, A_0.ᜪ(), A_0.\u17D0());
		this.ᜀ(WordSubdocument.Endnote, A_0.\u17CC(), A_0.\u175E());
		this.ᜀ(WordSubdocument.TextBox, A_0.\u173F(), A_0.\u171D());
		this.ᜀ(WordSubdocument.HeaderTextBox, A_0.ᝊ(), A_0.ᝁ());
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x00178220 File Offset: 0x00177220
	internal spr\u2280()
	{
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x00178248 File Offset: 0x00177248
	internal void ᜀ(WordSubdocument A_0, sprᝑ A_1, int A_2)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_75;
				}
				if (false)
				{
				}
				break;
			case 1:
			{
				SortedItemList<int, sprᝑ> value = new SortedItemList<int, sprᝑ>();
				this.ᜄ.Add(A_0, value);
				goto IL_75;
			}
			case 2:
				goto IL_7D;
			}
			if (!this.ᜄ.ContainsKey(A_0))
			{
				if (true)
				{
				}
				num = 1;
				continue;
			}
			break;
			IL_75:
			num = 2;
		}
		IL_7D:
		this.ᜄ[A_0].Add(A_2, A_1);
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x001782E8 File Offset: 0x001772E8
	internal SortedItemList<int, sprᝑ> ᜀ(WordSubdocument A_0)
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
		return this.ᜄ[A_0];
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x00178330 File Offset: 0x00177330
	internal void ᜀ(Stream A_0, sprᾱ A_1, int A_2)
	{
		for (;;)
		{
			this.ᜁ = A_0;
			this.ᜅ = A_2;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_329;
				case 1:
					goto IL_135;
				case 2:
					A_1.ᝊ((int)this.ᜁ.Position);
					this.ᜀ(this.ᜄ());
					A_1.ᜰ((int)(this.ᜁ.Position - (long)A_1.ᜪ()));
					num = 3;
					continue;
				case 3:
					goto IL_2E7;
				case 4:
					goto IL_1EB;
				case 5:
					A_1.ថ((int)this.ᜁ.Position);
					this.ᜀ(this.ᜈ());
					A_1.\u1718((int)(this.ᜁ.Position - (long)A_1.ᝊ()));
					num = 22;
					continue;
				case 6:
					if (this.ᜄ() != null)
					{
						num = 2;
						continue;
					}
					goto IL_2E7;
				case 7:
					if (this.ᜀ() != null)
					{
						num = 9;
						continue;
					}
					goto IL_329;
				case 8:
					A_1.ᝡ((int)this.ᜁ.Position);
					this.ᜀ(this.ᜅ());
					A_1.\u173A((int)(this.ᜁ.Position - (long)A_1.\u17CC()));
					num = 4;
					continue;
				case 9:
					A_1.ᜑ((int)this.ᜁ.Position);
					this.ᜀ(this.ᜀ());
					A_1.ᜡ((int)(this.ᜁ.Position - (long)A_1.\u171C()));
					num = 0;
					continue;
				case 10:
					if (this.ᜄ.Count > 0)
					{
						num = 18;
						continue;
					}
					return;
				case 11:
					goto IL_259;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (this.ᜅ() != null)
						{
							num = 8;
							continue;
						}
						goto IL_1EB;
					}
					break;
				case 13:
					if (this.ᜁ() != null)
					{
						num = 19;
						continue;
					}
					goto IL_2C1;
				case 14:
					A_1.\u1755((int)this.ᜁ.Position);
					this.ᜀ(this.ᜂ());
					A_1.ᝆ((int)(this.ᜁ.Position - (long)A_1.ᜤ()));
					num = 11;
					continue;
				case 15:
					if (this.ᜂ() != null)
					{
						num = 14;
						continue;
					}
					goto IL_259;
				case 16:
					goto IL_2C1;
				case 17:
					if (this.ᜈ() != null)
					{
						num = 5;
						continue;
					}
					return;
				case 18:
					num = 20;
					continue;
				case 19:
					A_1.\u1772((int)this.ᜁ.Position);
					this.ᜀ(this.ᜁ());
					A_1.\u1735((int)(this.ᜁ.Position - (long)A_1.\u173F()));
					num = 16;
					continue;
				case 20:
					if (this.ᜆ() != null)
					{
						num = 21;
						continue;
					}
					goto IL_135;
				case 21:
					A_1.ᝦ((int)this.ᜁ.Position);
					this.ᜀ(this.ᜆ());
					A_1.\u175E((int)(this.ᜁ.Position - (long)A_1.អ()));
					num = 1;
					continue;
				case 22:
					return;
				}
				break;
				IL_135:
				num = 7;
				continue;
				IL_1EB:
				num = 13;
				continue;
				IL_259:
				num = 6;
				continue;
				IL_2C1:
				num = 17;
				continue;
				IL_2E7:
				num = 12;
				continue;
				IL_329:
				if (true)
				{
				}
				num = 15;
			}
		}
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x00178700 File Offset: 0x00177700
	internal sprᝑ ᜀ(WordSubdocument A_0, int A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		SortedItemList<int, sprᝑ> sortedItemList = this.ᜄ[A_0];
		return sortedItemList[A_1];
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x00178750 File Offset: 0x00177750
	private void ᜀ(BinaryReader A_0, int A_1, int A_2)
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
		sprᝑ value = new sprᝑ(A_0);
		this.ᜃ[A_1] = value;
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x001787A0 File Offset: 0x001777A0
	private void ᜀ(WordSubdocument A_0, int A_1, int A_2)
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
		this.ᜃ = new SortedItemList<int, sprᝑ>();
		this.ᜄ[A_0] = this.ᜃ;
		this.ᜂ.BaseStream.Position = (long)A_1;
		spr\u2432.ᜀ(this.ᜂ, A_2, this.ᜀ, new spr\u1ACD(this.ᜀ));
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x00178828 File Offset: 0x00177828
	private void ᜀ(SortedItemList<int, sprᝑ> A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int count = A_0.Count;
				int num2 = 0;
				for (;;)
				{
					IEnumerator<int> enumerator;
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_16C;
					case 1:
						try
						{
							num2 = 3;
							for (;;)
							{
								switch (num2)
								{
								case 1:
								{
									if (!enumerator.MoveNext())
									{
										num2 = 2;
										continue;
									}
									int key = enumerator.Current;
									A_0[key].ᜀ(this.ᜁ);
									num2 = 0;
									continue;
								}
								case 2:
									num2 = 4;
									continue;
								case 4:
									goto IL_BB;
								}
								IL_72:
								num2 = 1;
								continue;
								goto IL_72;
							}
							IL_BB:
							return;
						}
						finally
						{
							num2 = 1;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_117;
								case 1:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_10E;
									default:
										if (false)
										{
										}
										break;
									}
									break;
								case 2:
									enumerator.Dispose();
									goto IL_10E;
								}
								if (enumerator != null)
								{
									num2 = 2;
									continue;
								}
								break;
								IL_10E:
								num2 = 0;
							}
							IL_117:;
						}
						goto IL_11A;
					case 2:
						if (num >= count)
						{
							num2 = 4;
							continue;
						}
						spr\u23F8.ᜁ(this.ᜁ, A_0.GetKey(num));
						num++;
						num2 = 3;
						continue;
					case 3:
						goto IL_16C;
					case 4:
						goto IL_11A;
					}
					break;
					IL_11A:
					spr\u23F8.ᜁ(this.ᜁ, this.ᜅ);
					enumerator = A_0.Keys.GetEnumerator();
					num2 = 1;
					continue;
					IL_16C:
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x04001CC0 RID: 7360
	internal new int ᜀ = 2;

	// Token: 0x04001CC1 RID: 7361
	private new Stream ᜁ;

	// Token: 0x04001CC2 RID: 7362
	private new BinaryReader ᜂ;

	// Token: 0x04001CC3 RID: 7363
	private new SortedItemList<int, sprᝑ> ᜃ;

	// Token: 0x04001CC4 RID: 7364
	private new Dictionary<WordSubdocument, SortedItemList<int, sprᝑ>> ᜄ = new Dictionary<WordSubdocument, SortedItemList<int, sprᝑ>>();

	// Token: 0x04001CC5 RID: 7365
	private new int ᜅ;
}
