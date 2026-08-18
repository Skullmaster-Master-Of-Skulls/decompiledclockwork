using System;
using System.Collections.Generic;
using System.IO;
using Spire.Doc.Collections;
using Spire.Doc.Core;

// Token: 0x020002B8 RID: 696
[CLSCompliant(false)]
internal class spr᭕ : spr\u23F8
{
	// Token: 0x0600257A RID: 9594 RVA: 0x0025865C File Offset: 0x0025765C
	internal SortedItemList<int, sprᨼ> ᜈ()
	{
		if (!this.ᜁ.ContainsKey(WordSubdocument.Main))
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
				return null;
			}
		}
		if (true)
		{
		}
		return this.ᜁ[WordSubdocument.Main];
	}

	// Token: 0x0600257B RID: 9595 RVA: 0x002586B8 File Offset: 0x002576B8
	internal SortedItemList<int, spr\u181A> ᜁ()
	{
		if (!this.ᜂ.ContainsKey(WordSubdocument.Main))
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
				return null;
			}
		}
		if (true)
		{
		}
		return this.ᜂ[WordSubdocument.Main];
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x00258714 File Offset: 0x00257714
	internal SortedItemList<int, spr\u208C> ᜅ()
	{
		if (!this.ᜃ.ContainsKey(WordSubdocument.Main))
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
				return null;
			}
		}
		return this.ᜃ[WordSubdocument.Main];
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x00258770 File Offset: 0x00257770
	internal SortedItemList<int, sprᨼ> ᜀ()
	{
		if (!this.ᜁ.ContainsKey(WordSubdocument.HeaderFooter))
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
				return null;
			}
		}
		return this.ᜁ[WordSubdocument.HeaderFooter];
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x002587CC File Offset: 0x002577CC
	internal SortedItemList<int, spr\u181A> ᜂ()
	{
		if (!this.ᜂ.ContainsKey(WordSubdocument.HeaderFooter))
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
				return null;
			}
		}
		return this.ᜂ[WordSubdocument.HeaderFooter];
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x00258828 File Offset: 0x00257828
	internal SortedItemList<int, spr\u208C> ᜆ()
	{
		if (!this.ᜃ.ContainsKey(WordSubdocument.HeaderFooter))
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
				return null;
			}
		}
		if (true)
		{
		}
		return this.ᜃ[WordSubdocument.HeaderFooter];
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x00258884 File Offset: 0x00257884
	internal int ᜄ()
	{
		int num = 2;
		int num2;
		for (;;)
		{
			int num3;
			switch (num)
			{
			case 0:
				num2 += ((this.ᜂ() == null) ? 0 : this.ᜂ().Count);
				num = 4;
				continue;
			case 1:
				if (true)
				{
				}
				num = 3;
				continue;
			case 3:
				num3 = this.ᜈ().Count;
				goto IL_8C;
			case 4:
				goto IL_E8;
			case 5:
				num2 += ((this.ᜀ() == null) ? 0 : this.ᜀ().Count);
				num = 0;
				continue;
			case 6:
				num3 = 0;
				goto IL_8C;
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
				if (this.ᜈ() != null)
				{
					num = 1;
					continue;
				}
				break;
			}
			num = 6;
			continue;
			IL_8C:
			num2 = num3;
			num = 5;
		}
		IL_E8:
		num2 += ((this.ᜁ() == null) ? 0 : this.ᜁ().Count);
		return num2;
	}

	// Token: 0x06002581 RID: 9601 RVA: 0x00258998 File Offset: 0x00257998
	internal spr᭕(sprᾱ A_0, Stream A_1) : this()
	{
		this.ᜀ(A_1, A_0);
	}

	// Token: 0x06002582 RID: 9602 RVA: 0x002589B4 File Offset: 0x002579B4
	internal spr᭕()
	{
		this.ᜁ = new SortedItemList<WordSubdocument, SortedItemList<int, sprᨼ>>();
		this.ᜃ = new SortedItemList<WordSubdocument, SortedItemList<int, spr\u208C>>();
		this.ᜂ = new SortedItemList<WordSubdocument, SortedItemList<int, spr\u181A>>();
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x002589E8 File Offset: 0x002579E8
	internal void ᜀ(sprᨼ A_0, WordSubdocument A_1, int A_2)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				SortedItemList<int, sprᨼ> value = new SortedItemList<int, sprᨼ>();
				this.ᜁ.Add(A_1, value);
				num = 1;
				continue;
			}
			case 1:
				goto IL_73;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_73;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (this.ᜁ.ContainsKey(A_1))
			{
				break;
			}
			if (true)
			{
			}
			num = 0;
		}
		IL_73:
		this.ᜁ[A_1].Add(A_2, A_0);
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x00258A88 File Offset: 0x00257A88
	internal void ᜀ(WordSubdocument A_0, spr\u181A A_1, spr\u208C A_2, int A_3)
	{
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				SortedItemList<int, spr\u208C> value = new SortedItemList<int, spr\u208C>();
				this.ᜃ.Add(A_0, value);
				num = 5;
				continue;
			}
			case 1:
				if (A_0 == WordSubdocument.Main)
				{
					num = 11;
					continue;
				}
				goto IL_13C;
			case 2:
				if (true)
				{
				}
				goto IL_13C;
			case 3:
			{
				SortedItemList<int, spr\u181A> value2 = new SortedItemList<int, spr\u181A>();
				this.ᜂ.Add(A_0, value2);
				num = 7;
				continue;
			}
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AA;
				default:
					if (false)
					{
					}
					this.ᜅ = A_3 + 3;
					num = 6;
					continue;
				}
				break;
			case 5:
				goto IL_7A;
			case 6:
				return;
			case 7:
				goto IL_AC;
			case 8:
				if (A_0 == WordSubdocument.HeaderFooter)
				{
					num = 4;
					continue;
				}
				return;
			case 10:
				if (!this.ᜃ.ContainsKey(A_0))
				{
					num = 0;
					continue;
				}
				goto IL_7A;
			case 11:
				goto IL_AA;
			}
			if (!this.ᜂ.ContainsKey(A_0))
			{
				num = 3;
				continue;
			}
			goto IL_AC;
			IL_7A:
			this.ᜃ[A_0].Add(A_3, A_2);
			num = 1;
			continue;
			IL_AA:
			this.ᜄ = A_3 + 3;
			num = 2;
			continue;
			IL_AC:
			this.ᜂ[A_0].Add(A_3, A_1);
			num = 10;
			continue;
			IL_13C:
			num = 8;
		}
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x00258C1C File Offset: 0x00257C1C
	internal void ᜀ(Stream A_0, sprᾱ A_1)
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
		this.ᜀ = A_0;
		this.ᜂ(WordSubdocument.Main, A_1.ᝇ(), A_1.ឨ());
		this.ᜂ(WordSubdocument.HeaderFooter, A_1.ឱ(), A_1.ᝎ());
		this.ᜁ(WordSubdocument.Main, A_1.ស(), A_1.ᝉ());
		this.ᜁ(WordSubdocument.HeaderFooter, A_1.ᝦ(), A_1.ᝆ());
		this.ᜀ(WordSubdocument.Main, A_1.\u17C5(), A_1.ᝬ());
		this.ᜀ(WordSubdocument.HeaderFooter, A_1.ᝍ(), A_1.ឣ());
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x00258CD0 File Offset: 0x00257CD0
	internal void ᜀ(Stream A_0, sprᾱ A_1, int A_2, int A_3)
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
		this.ᜀ = A_0;
		this.ᜀ(A_1, A_2, A_3);
		this.ᜀ(A_1, A_2);
		this.ᜀ(A_1);
	}

	// Token: 0x06002587 RID: 9607 RVA: 0x00258D2C File Offset: 0x00257D2C
	internal int ᜀ(bool A_0, int A_1)
	{
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				if (A_1 != this.ᜁ().Count)
				{
					num = 3;
					continue;
				}
				goto IL_D8;
			case 2:
				if (this.ᜁ() != null)
				{
					goto IL_A5;
				}
				return 0;
			case 3:
				goto IL_8A;
			case 4:
				num = 7;
				continue;
			case 5:
				num = 8;
				continue;
			case 7:
				if (A_1 != this.ᜂ().Count)
				{
					num = 9;
					continue;
				}
				goto IL_122;
			case 8:
				if (this.ᜂ() != null)
				{
					num = 4;
					continue;
				}
				goto IL_8F;
			case 9:
				goto IL_D6;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A5:
				num = 0;
				continue;
			default:
				if (false)
				{
				}
				if (A_0)
				{
					num = 5;
					continue;
				}
				break;
			}
			IL_8F:
			num = 2;
		}
		IL_8A:
		return this.ᜀ(this.ᜁ(), A_1);
		IL_D6:
		return this.ᜀ(this.ᜂ(), A_1);
		IL_D8:
		return this.ᜀ(this.ᜁ(), A_1 - 1) + 3;
		IL_122:
		return this.ᜀ(this.ᜂ(), A_1 - 1) + 3;
	}

	// Token: 0x06002588 RID: 9608 RVA: 0x00258E7C File Offset: 0x00257E7C
	internal int ᜁ(WordSubdocument A_0, int A_1)
	{
		int num = 1;
		spr\u181A spr_u181A;
		for (;;)
		{
			switch (num)
			{
			case 0:
				spr_u181A = this.ᜁ(this.ᜁ(), A_1);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8A;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 2:
				goto IL_4C;
			case 3:
				goto IL_8A;
			}
			if (true)
			{
			}
			if (A_0 == WordSubdocument.TextBox)
			{
				num = 0;
			}
			else
			{
				spr_u181A = this.ᜁ(this.ᜂ(), A_1);
				num = 2;
			}
		}
		IL_4C:
		IL_8A:
		return spr_u181A.ᜁ();
	}

	// Token: 0x06002589 RID: 9609 RVA: 0x00258F1C File Offset: 0x00257F1C
	internal sprᨼ ᜀ(WordSubdocument A_0, int A_1)
	{
		SortedItemList<int, sprᨼ> sortedItemList;
		for (;;)
		{
			sortedItemList = this.ᜁ[A_0];
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_66;
				case 1:
					if (true)
					{
					}
					if (sortedItemList != null)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 2:
					goto IL_81;
				case 3:
					goto IL_6F;
				}
				break;
				IL_6F:
				num = 2;
			}
		}
		IL_66:
		return sortedItemList[A_1];
		IL_81:
		return null;
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x00258FB0 File Offset: 0x00257FB0
	private void ᜂ(WordSubdocument A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜀ.Position = (long)A_1;
				SortedItemList<int, sprᨼ> sortedItemList = new SortedItemList<int, sprᨼ>();
				this.ᜁ[A_0] = sortedItemList;
				int[] array;
				int num;
				int num2;
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_8B:
					array = this.ᜀ(26, A_2);
					num = 0;
					num2 = array.Length - 1;
					num3 = 1;
					break;
				default:
					if (false)
					{
					}
					num3 = 2;
					break;
				}
				for (;;)
				{
					switch (num3)
					{
					case 0:
						return;
					case 1:
						goto IL_8D;
					case 2:
						if (A_2 != 0)
						{
							if (true)
							{
							}
							num3 = 5;
							continue;
						}
						return;
					case 3:
					{
						if (num >= num2)
						{
							num3 = 0;
							continue;
						}
						sprᨼ value = new sprᨼ(this.ᜀ);
						sortedItemList.Add(array[num], value);
						num++;
						num3 = 4;
						continue;
					}
					case 4:
						goto IL_8D;
					case 5:
						goto IL_8B;
					}
					break;
					IL_8D:
					num3 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x002590BC File Offset: 0x002580BC
	private void ᜁ(WordSubdocument A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				int num2;
				int[] array;
				SortedItemList<int, spr\u181A> sortedItemList;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
					{
						if (num2 >= array.Length - 1)
						{
							num = 2;
							continue;
						}
						spr\u181A value = new spr\u181A(this.ᜀ);
						sortedItemList.Add(array[num2], value);
						num2++;
						num = 1;
						continue;
					}
					case 1:
						goto IL_6E;
					case 2:
						return;
					case 4:
						goto IL_6E;
					case 5:
						goto IL_B2;
					}
					if (A_2 > 0)
					{
						num = 5;
						continue;
					}
					return;
					IL_6E:
					num = 0;
					continue;
				}
				IL_B2:
				sortedItemList = new SortedItemList<int, spr\u181A>();
				this.ᜂ[A_0] = sortedItemList;
				this.ᜀ.Position = (long)A_1;
				array = this.ᜀ(spr\u181A.ᜀ, A_2);
				num2 = 0;
				num = 4;
			}
			return;
		}
		}
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x002591C8 File Offset: 0x002581C8
	private void ᜀ(WordSubdocument A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				int[] array;
				SortedItemList<int, spr\u208C> sortedItemList;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 1:
						goto IL_AA;
					case 2:
						return;
					case 3:
						goto IL_66;
					case 4:
						goto IL_66;
					case 5:
					{
						if (num2 >= array.Length - 1)
						{
							num = 2;
							continue;
						}
						spr\u208C value = new spr\u208C(this.ᜀ);
						sortedItemList.Add(array[num2], value);
						num2++;
						num = 3;
						continue;
					}
					}
					if (A_2 > 0)
					{
						num = 1;
						continue;
					}
					return;
					IL_66:
					num = 5;
					continue;
				}
				IL_AA:
				if (true)
				{
				}
				sortedItemList = new SortedItemList<int, spr\u208C>();
				this.ᜃ[A_0] = sortedItemList;
				this.ᜀ.Position = (long)A_1;
				array = this.ᜀ(6, A_2);
				num2 = 0;
				num = 4;
			}
			return;
		}
		}
	}

	// Token: 0x0600258D RID: 9613 RVA: 0x002592D0 File Offset: 0x002582D0
	private int[] ᜀ(int A_0, int A_1)
	{
		int[] array;
		for (;;)
		{
			int num = (A_1 - 4) / (A_0 + 4) + 1;
			array = new int[num];
			int num2 = 0;
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_35;
				case 1:
					return array;
				case 2:
					if (num2 >= num)
					{
						num3 = 1;
						continue;
					}
					array[num2] = (int)spr\u23F8.ᜃ(this.ᜀ);
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_96;
					default:
						if (false)
						{
						}
						num3 = 3;
						continue;
					}
					break;
				case 3:
					goto IL_96;
				}
				break;
				IL_35:
				if (true)
				{
				}
				num3 = 2;
				continue;
				IL_96:
				goto IL_35;
			}
		}
		return array;
	}

	// Token: 0x0600258E RID: 9614 RVA: 0x00259378 File Offset: 0x00258378
	private void ᜀ(sprᾱ A_0, int A_1, int A_2)
	{
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜀ() != null)
				{
					num = 8;
					continue;
				}
				goto IL_16E;
			case 1:
				if (this.ᜀ() != null)
				{
					num = 2;
					continue;
				}
				goto IL_16E;
			case 2:
				goto IL_148;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_16E;
				default:
					if (false)
					{
					}
					A_0.ᝅ((int)this.ᜀ.Position);
					this.ᜀ(this.ᜈ(), A_1);
					A_0.\u177E((int)(this.ᜀ.Position - (long)A_0.ᝇ()));
					num = 5;
					continue;
				}
				break;
			case 4:
				goto IL_146;
			case 5:
				goto IL_50;
			case 6:
				if (this.ᜈ() != null)
				{
					num = 3;
					continue;
				}
				goto IL_50;
			case 7:
				num = 1;
				continue;
			case 8:
				A_0.ធ((int)this.ᜀ.Position);
				this.ᜀ(this.ᜀ(), A_2);
				A_0.\u176D((int)(this.ᜀ.Position - (long)A_0.ឱ()));
				num = 4;
				continue;
			}
			if (this.ᜈ() == null)
			{
				num = 7;
				continue;
			}
			goto IL_148;
			IL_50:
			num = 0;
			continue;
			IL_148:
			num = 6;
		}
		IL_146:
		IL_16E:
		if (true)
		{
		}
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x002594FC File Offset: 0x002584FC
	private void ᜀ(sprᾱ A_0, int A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.គ((int)this.ᜀ.Position);
				this.ᜂ(this.ᜂ(), A_1);
				A_0.ᝐ((int)(this.ᜀ.Position - (long)A_0.ᝦ()));
				num = 3;
				continue;
			case 2:
				if (this.ᜂ() != null)
				{
					num = 7;
					continue;
				}
				return;
			case 3:
				return;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					A_0.ᝃ((int)this.ᜀ.Position);
					this.ᜂ(this.ᜁ(), A_1);
					A_0.ᝥ((int)(this.ᜀ.Position - (long)A_0.ស()));
					num = 6;
					continue;
				}
				break;
			case 5:
				num = 2;
				continue;
			case 6:
				goto IL_62;
			case 7:
				goto IL_150;
			case 8:
				if (this.ᜁ() != null)
				{
					num = 4;
					continue;
				}
				goto IL_62;
			case 9:
				if (this.ᜂ() != null)
				{
					num = 0;
					continue;
				}
				return;
			}
			if (true)
			{
			}
			if (this.ᜁ() == null)
			{
				num = 5;
				continue;
			}
			goto IL_150;
			IL_62:
			num = 9;
			continue;
			IL_150:
			num = 8;
		}
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x00259680 File Offset: 0x00258680
	private void ᜀ(sprᾱ A_0)
	{
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜅ() != null)
				{
					num = 5;
					continue;
				}
				goto IL_50;
			case 1:
				return;
			case 2:
				goto IL_15A;
			case 3:
				A_0.\u1714((int)this.ᜀ.Position);
				this.ᜀ(this.ᜆ(), this.ᜅ);
				A_0.ᜏ((int)(this.ᜀ.Position - (long)A_0.ᝍ()));
				num = 1;
				continue;
			case 4:
				if (true)
				{
				}
				if (this.ᜆ() != null)
				{
					num = 2;
					continue;
				}
				return;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					A_0.ᜐ((int)this.ᜀ.Position);
					this.ᜀ(this.ᜅ(), this.ᜄ);
					A_0.ᜉ((int)(this.ᜀ.Position - (long)A_0.\u17C5()));
					num = 7;
					continue;
				}
				break;
			case 6:
				num = 4;
				continue;
			case 7:
				goto IL_50;
			case 9:
				if (this.ᜆ() != null)
				{
					num = 3;
					continue;
				}
				return;
			}
			if (this.ᜅ() == null)
			{
				num = 6;
				continue;
			}
			goto IL_15A;
			IL_50:
			num = 9;
			continue;
			IL_15A:
			num = 0;
		}
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x00259810 File Offset: 0x00258810
	private void ᜀ(SortedItemList<int, spr\u208C> A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			IEnumerator<int> enumerator = A_0.Keys.GetEnumerator();
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_17B;
					case 2:
						goto IL_172;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						int a_ = enumerator.Current;
						spr\u23F8.ᜁ(this.ᜀ, a_);
						num = 4;
						continue;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_172;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					IL_11C:
					num = 3;
					continue;
					goto IL_11C;
					IL_172:
					num = 1;
				}
				IL_17B:
				goto IL_CD;
			}
			finally
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_1C3;
					case 2:
						enumerator.Dispose();
						num = 1;
						continue;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 2;
				}
				IL_1C3:;
			}
			return;
			for (;;)
			{
				IL_CD:
				spr\u23F8.ᜁ(this.ᜀ, A_1);
				IEnumerator<spr\u208C> enumerator2 = A_0.Values.GetEnumerator();
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator2.MoveNext())
							{
								num = 2;
								continue;
							}
							spr\u208C spr_u208C = enumerator2.Current;
							spr_u208C.ᜀ(this.ᜀ);
							num = 3;
							continue;
						}
						case 2:
							num = 4;
							continue;
						case 4:
							goto IL_8A;
						}
						IL_47:
						num = 0;
						continue;
						goto IL_47;
					}
					IL_8A:
					break;
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							enumerator2.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_CA;
						}
						if (enumerator2 == null)
						{
							break;
						}
						num = 1;
					}
					IL_CA:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x00259A00 File Offset: 0x00258A00
	private void ᜂ(SortedItemList<int, spr\u181A> A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			IEnumerator<int> enumerator = A_0.Keys.GetEnumerator();
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						int a_ = enumerator.Current;
						spr\u23F8.ᜁ(this.ᜀ, a_);
						num = 4;
						continue;
					}
					case 2:
						goto IL_17B;
					case 3:
						goto IL_172;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_172;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					IL_11C:
					num = 1;
					continue;
					goto IL_11C;
					IL_172:
					num = 2;
				}
				IL_17B:
				goto IL_CD;
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_1BB;
					case 2:
						enumerator.Dispose();
						num = 1;
						continue;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 2;
				}
				IL_1BB:
				if (true)
				{
				}
			}
			return;
			for (;;)
			{
				IL_CD:
				spr\u23F8.ᜁ(this.ᜀ, A_1);
				IEnumerator<spr\u181A> enumerator2 = A_0.Values.GetEnumerator();
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 1:
							num = 3;
							continue;
						case 2:
						{
							if (!enumerator2.MoveNext())
							{
								num = 1;
								continue;
							}
							spr\u181A spr_u181A = enumerator2.Current;
							spr_u181A.ᜀ(this.ᜀ);
							num = 0;
							continue;
						}
						case 3:
							goto IL_8A;
						}
						IL_47:
						num = 2;
						continue;
						goto IL_47;
					}
					IL_8A:
					break;
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_CA;
						case 1:
							enumerator2.Dispose();
							num = 0;
							continue;
						}
						if (enumerator2 == null)
						{
							break;
						}
						num = 1;
					}
					IL_CA:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x00259BF0 File Offset: 0x00258BF0
	private void ᜀ(SortedItemList<int, sprᨼ> A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			IEnumerator<int> enumerator = A_0.Keys.GetEnumerator();
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						int a_ = enumerator.Current;
						spr\u23F8.ᜁ(this.ᜀ, a_);
						num = 3;
						continue;
					}
					case 2:
						goto IL_171;
					case 4:
						num = 2;
						continue;
					}
					IL_12E:
					num = 1;
					continue;
					goto IL_12E;
				}
				IL_171:
				goto IL_E9;
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						enumerator.Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_1B9;
					}
					if (true)
					{
					}
					if (enumerator == null)
					{
						break;
					}
					num = 1;
				}
				IL_1B9:;
			}
			return;
			for (;;)
			{
				IL_E9:
				spr\u23F8.ᜁ(this.ᜀ, A_1);
				IEnumerator<sprᨼ> enumerator2 = A_0.Values.GetEnumerator();
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 2:
							if (!enumerator2.MoveNext())
							{
								num = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								sprᨼ sprᨼ = enumerator2.Current;
								sprᨼ.ᜀ(this.ᜀ);
								break;
							}
							}
							num = 0;
							continue;
						case 3:
							num = 4;
							continue;
						case 4:
							goto IL_A6;
						}
						IL_47:
						num = 2;
						continue;
						goto IL_47;
					}
					IL_A6:
					break;
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E6;
						case 2:
							enumerator2.Dispose();
							num = 0;
							continue;
						}
						if (enumerator2 == null)
						{
							break;
						}
						num = 2;
					}
					IL_E6:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x00259DE0 File Offset: 0x00258DE0
	private spr\u181A ᜁ(SortedItemList<int, spr\u181A> A_0, int A_1)
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
					goto IL_71;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 1:
				if (A_1 < 0)
				{
					goto IL_71;
				}
				goto IL_7B;
			case 3:
				goto IL_79;
			}
			if (A_1 <= A_0.Count - 1)
			{
				num = 0;
				continue;
			}
			break;
			IL_71:
			num = 3;
		}
		IL_35:
		return null;
		IL_79:
		goto IL_35;
		IL_7B:
		return A_0.Values[A_1];
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x00259E74 File Offset: 0x00258E74
	private int ᜀ(SortedItemList<int, spr\u181A> A_0, int A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_79;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_69;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 3:
				if (A_1 < 0)
				{
					goto IL_69;
				}
				goto IL_7B;
			}
			if (A_1 <= A_0.Count - 1)
			{
				num = 1;
				continue;
			}
			break;
			IL_69:
			if (true)
			{
			}
			num = 0;
		}
		return -1;
		IL_79:
		return -1;
		IL_7B:
		return A_0.Keys[A_1];
	}

	// Token: 0x040021F5 RID: 8693
	private new Stream ᜀ;

	// Token: 0x040021F6 RID: 8694
	private new SortedItemList<WordSubdocument, SortedItemList<int, sprᨼ>> ᜁ;

	// Token: 0x040021F7 RID: 8695
	private new SortedItemList<WordSubdocument, SortedItemList<int, spr\u181A>> ᜂ;

	// Token: 0x040021F8 RID: 8696
	private new SortedItemList<WordSubdocument, SortedItemList<int, spr\u208C>> ᜃ;

	// Token: 0x040021F9 RID: 8697
	private new int ᜄ;

	// Token: 0x040021FA RID: 8698
	private new int ᜅ;
}
