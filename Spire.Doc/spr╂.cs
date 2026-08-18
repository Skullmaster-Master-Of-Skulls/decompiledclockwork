using System;
using System.Collections.Generic;
using System.IO;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Core.Escher;
using Spire.Doc.Fields;

// Token: 0x02000174 RID: 372
internal class spr\u2542 : spr\u2192
{
	// Token: 0x06000CDD RID: 3293 RVA: 0x000D58DC File Offset: 0x000D48DC
	internal sprᵲ \u1714()
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

	// Token: 0x06000CDE RID: 3294 RVA: 0x000D5920 File Offset: 0x000D4920
	internal spr\u2542(Document A_0) : base(A_0)
	{
		this.ᜀ = new sprᵲ(A_0);
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x000D5940 File Offset: 0x000D4940
	internal spr\u2542(MSOFBT A_0, Document A_1) : base(A_1)
	{
		this.ᜀ = new sprᵲ(A_1);
		base.\u1717().ᜀ(true);
		base.\u1717().ᜀ(A_0);
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x000D5978 File Offset: 0x000D4978
	internal static bool ᜀ(spr\u2542 A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				flag = false;
				spr\u2192 spr_u = null;
				int num = 0;
				int count = A_0.\u1714().Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_D6;
					case 1:
						goto IL_F2;
					case 2:
						if (spr_u is spr\u2542)
						{
							num2 = 13;
							continue;
						}
						goto IL_129;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E6;
						default:
							goto IL_176;
						}
						break;
					case 4:
						num2 = 6;
						continue;
					case 5:
						goto IL_129;
					case 6:
						if ((spr_u as spr\u2459).ᜅ().ᜀ() == A_1)
						{
							num2 = 8;
							continue;
						}
						goto IL_129;
					case 7:
						if (num >= count)
						{
							goto IL_E6;
						}
						spr_u = (A_0.\u1714()[num] as spr\u2192);
						num2 = 10;
						continue;
					case 8:
						A_0.\u1714().Remove(spr_u);
						flag = true;
						num2 = 3;
						continue;
					case 9:
						if (spr_u is spr\u2459)
						{
							num2 = 4;
							continue;
						}
						num2 = 2;
						continue;
					case 10:
						if (!flag)
						{
							num2 = 12;
							continue;
						}
						return flag;
					case 11:
						goto IL_D6;
					case 12:
						num2 = 9;
						continue;
					case 13:
						if (true)
						{
						}
						flag = spr\u2542.ᜀ(spr_u as spr\u2542, A_1);
						num2 = 5;
						continue;
					}
					break;
					IL_D6:
					num2 = 7;
					continue;
					IL_E6:
					num2 = 1;
					continue;
					IL_129:
					num++;
					num2 = 11;
				}
			}
			IL_F2:
			return flag;
			IL_176:
			if (false)
			{
			}
			return flag;
		}
		}
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x000D5B34 File Offset: 0x000D4B34
	internal void ᜀ(TextBoxItemCollection A_0, ref int A_1, ref int A_2, ref int A_3, ref int A_4)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2192 spr_u = null;
				int num = 0;
				int count = this.\u1714().Count;
				int num2 = 12;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (spr_u is spr\u2402)
						{
							num2 = 4;
							continue;
						}
						goto IL_1A0;
					case 1:
						this.ᜀ(spr_u as spr\u22B7, ref A_3);
						num2 = 11;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F6;
						default:
							if (false)
							{
							}
							(spr_u as spr\u2542).ᜀ(A_0, ref A_1, ref A_2, ref A_3, ref A_4);
							num2 = 13;
							continue;
						}
						break;
					case 3:
						if (spr_u is spr\u22B7)
						{
							num2 = 1;
							continue;
						}
						goto IL_1CA;
					case 4:
					{
						spr\u2402 a_ = spr_u as spr\u2402;
						this.ᜀ(a_, A_0, ref A_1, ref A_2, ref A_4);
						num2 = 9;
						continue;
					}
					case 5:
						goto IL_166;
					case 6:
						return;
					case 7:
						if (num >= count)
						{
							num2 = 6;
							continue;
						}
						spr_u = (this.\u1714()[num] as spr\u2192);
						num2 = 0;
						continue;
					case 8:
						if (spr_u is spr\u2542)
						{
							num2 = 2;
							continue;
						}
						goto IL_FB;
					case 9:
						goto IL_F6;
					case 10:
						(spr_u as sprᥥ).ᜀ(A_3);
						num2 = 15;
						continue;
					case 11:
						goto IL_1CA;
					case 12:
						goto IL_166;
					case 13:
						goto IL_FB;
					case 14:
						if (spr_u is sprᥥ)
						{
							num2 = 10;
							continue;
						}
						goto IL_141;
					case 15:
						goto IL_141;
					}
					break;
					IL_FB:
					num++;
					num2 = 5;
					continue;
					IL_141:
					num2 = 8;
					continue;
					IL_166:
					num2 = 7;
					continue;
					IL_1A0:
					if (true)
					{
					}
					num2 = 3;
					continue;
					IL_F6:
					goto IL_1A0;
					IL_1CA:
					num2 = 14;
				}
			}
			return;
		}
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x000D5D34 File Offset: 0x000D4D34
	internal int \u1715()
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				spr\u2192 spr_u = null;
				int num2 = 0;
				int count = this.\u1714().Count;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_13E;
					case 1:
						return num;
					case 2:
						goto IL_CF;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return num;
						default:
							if (false)
							{
							}
							num3 = 4;
							continue;
						}
						break;
					case 4:
						if (spr_u is spr\u2402)
						{
							num3 = 7;
							continue;
						}
						num3 = 11;
						continue;
					case 5:
						if (num == 0)
						{
							num3 = 3;
							continue;
						}
						return num;
					case 6:
						goto IL_CF;
					case 7:
						num = (spr_u as spr\u2402).ᜀ();
						num3 = 9;
						continue;
					case 8:
						if (num2 >= count)
						{
							num3 = 1;
							continue;
						}
						spr_u = (this.\u1714()[num2] as spr\u2192);
						num3 = 5;
						continue;
					case 9:
						return num;
					case 10:
						num = (spr_u as spr\u2542).\u1715();
						num3 = 0;
						continue;
					case 11:
						if (spr_u is spr\u2542)
						{
							num3 = 10;
							continue;
						}
						goto IL_13E;
					}
					break;
					IL_CF:
					num3 = 8;
					continue;
					IL_13E:
					num2++;
					if (true)
					{
					}
					num3 = 6;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x000D5EB8 File Offset: 0x000D4EB8
	internal bool ᜄ(int A_0)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				flag = false;
				spr\u2192 spr_u = null;
				int num = 0;
				int count = this.\u1714().Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_D0;
					case 1:
						goto IL_D0;
					case 2:
						goto IL_147;
					case 3:
						if (!flag)
						{
							if (true)
							{
							}
							num2 = 8;
							continue;
						}
						return flag;
					case 4:
						if (num >= count)
						{
							num2 = 11;
							continue;
						}
						spr_u = (this.\u1714()[num] as spr\u2192);
						num2 = 3;
						continue;
					case 5:
						flag = (spr_u as spr\u2542).ᜄ(A_0);
						num2 = 2;
						continue;
					case 6:
						return flag;
					case 7:
						(spr_u as spr\u2402).ᜀ(A_0);
						flag = true;
						num2 = 6;
						continue;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return flag;
						default:
							if (false)
							{
							}
							num2 = 9;
							continue;
						}
						break;
					case 9:
						if (spr_u is spr\u2402)
						{
							num2 = 7;
							continue;
						}
						num2 = 10;
						continue;
					case 10:
						if (spr_u is spr\u2542)
						{
							num2 = 5;
							continue;
						}
						goto IL_147;
					case 11:
						return flag;
					}
					break;
					IL_D0:
					num2 = 4;
					continue;
					IL_147:
					num++;
					num2 = 1;
				}
			}
			return flag;
		}
		}
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x000D6040 File Offset: 0x000D5040
	internal spr\u2192 ᜀ(MSOFBT A_0)
	{
		spr\u2192 spr_u;
		for (;;)
		{
			IL_46:
			int num = 0;
			if (true)
			{
			}
			int num2 = 3;
			for (;;)
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
					switch (num2)
					{
					case 0:
						if (num >= this.ᜀ.Count)
						{
							num2 = 2;
							continue;
						}
						spr_u = (this.ᜀ[num] as spr\u2192);
						num2 = 5;
						continue;
					case 1:
						return spr_u;
					case 2:
						goto IL_C6;
					case 3:
						goto IL_A2;
					case 4:
						goto IL_66;
					case 5:
						if (spr_u.\u1717().ᜅ() == A_0)
						{
							num2 = 1;
							continue;
						}
						num++;
						num2 = 4;
						continue;
					}
					goto IL_46;
				}
				IL_A2:
				num2 = 0;
				continue;
				IL_66:
				goto IL_A2;
			}
		}
		return spr_u;
		IL_C6:
		return null;
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x000D6118 File Offset: 0x000D5118
	internal spr\u2192 ᜀ(Type A_0)
	{
		spr\u2192 spr_u;
		for (;;)
		{
			IL_46:
			if (true)
			{
			}
			int num = 0;
			int num2 = 1;
			for (;;)
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
					switch (num2)
					{
					case 0:
						if (num >= this.ᜀ.Count)
						{
							num2 = 5;
							continue;
						}
						spr_u = (this.ᜀ[num] as spr\u2192);
						num2 = 4;
						continue;
					case 1:
						goto IL_9D;
					case 2:
						goto IL_66;
					case 3:
						return spr_u;
					case 4:
						if (spr_u.GetType() == A_0)
						{
							num2 = 3;
							continue;
						}
						num++;
						num2 = 2;
						continue;
					case 5:
						goto IL_C1;
					}
					goto IL_46;
				}
				IL_9D:
				num2 = 0;
				continue;
				IL_66:
				goto IL_9D;
			}
		}
		return spr_u;
		IL_C1:
		return null;
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x000D61EC File Offset: 0x000D51EC
	internal spr\u2542 ᜀ(spr\u2542 A_0)
	{
		spr\u2542 spr_u2;
		for (;;)
		{
			IL_30:
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_62:
				num = 9;
				break;
			default:
				if (false)
				{
				}
				num2 = 0;
				num = 5;
				break;
			}
			spr\u2542 spr_u;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_106;
				case 1:
					if (spr_u != null)
					{
						num = 0;
						continue;
					}
					goto IL_D2;
				case 2:
					if (spr_u == A_0)
					{
						num = 7;
						continue;
					}
					num = 1;
					continue;
				case 3:
					return spr_u2;
				case 4:
					if (num2 >= this.ᜀ.Count)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					spr_u = (this.ᜀ[num2] as spr\u2542);
					num = 2;
					continue;
				case 5:
					goto IL_79;
				case 6:
					goto IL_9A;
				case 7:
					return this;
				case 8:
					goto IL_79;
				case 9:
					if (spr_u2 != null)
					{
						num = 3;
						continue;
					}
					goto IL_D2;
				}
				goto IL_30;
				IL_79:
				num = 4;
				continue;
				IL_D2:
				num2++;
				num = 8;
			}
			IL_106:
			spr_u2 = spr_u.ᜀ(A_0);
			goto IL_62;
		}
		return spr_u2;
		IL_9A:
		return null;
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x000D6308 File Offset: 0x000D5308
	protected override void ᜁ(Stream A_0)
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
		this.ᜀ.ᜀ(A_0, base.\u1717().ᜇ());
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x000D635C File Offset: 0x000D535C
	protected override void ᜀ(Stream A_0)
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
		this.ᜀ.ᜀ(A_0);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x000D63A4 File Offset: 0x000D53A4
	internal virtual spr\u2192 ᜂ()
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
			switch (0)
			{
			}
			break;
		}
		base.\u1717().ᜅ();
		spr\u2192 spr_u = base.\u1717().ᜄ();
		using (List<object>.Enumerator enumerator = this.\u1714().GetEnumerator())
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_101;
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					spr\u2192 spr_u2 = (spr\u2192)enumerator.Current;
					spr\u2192 spr_u3 = spr_u2.\u1717().ᜄ();
					spr_u3.ᜀ(spr_u2.\u1717().ᜆ());
					(spr_u as spr\u2542).\u1714().Add(spr_u2.ᜃ());
					num = 3;
					continue;
				}
				}
				IL_D5:
				num = 2;
				continue;
				goto IL_D5;
			}
			IL_101:;
		}
		spr_u.ᜁ = this.ᜁ;
		return spr_u;
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x000D64E0 File Offset: 0x000D54E0
	internal virtual void ᜀ(Document A_0)
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
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x000D651C File Offset: 0x000D551C
	internal void \u1716()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2192 spr_u = null;
				int num = 0;
				int count = this.\u1714().Count;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						spr\u2459 spr_u2 = spr_u as spr\u2459;
						spr_u2.ᜎ();
						num2 = 10;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num2 = 8;
							continue;
						}
						break;
					case 2:
						if (num >= count)
						{
							num2 = 4;
							continue;
						}
						spr_u = (this.\u1714()[num] as spr\u2192);
						num2 = 11;
						continue;
					case 3:
						goto IL_E4;
					case 4:
						return;
					case 5:
						if (spr_u is spr\u2542)
						{
							num2 = 9;
							continue;
						}
						goto IL_15B;
					case 6:
						goto IL_E4;
					case 7:
						goto IL_15B;
					case 8:
						if ((spr_u as spr\u2459).ᜅ().\u1712())
						{
							num2 = 0;
							continue;
						}
						goto IL_15B;
					case 9:
						(spr_u as spr\u2542).\u1716();
						num2 = 7;
						continue;
					case 10:
						goto IL_15B;
					case 11:
						if (spr_u is spr\u2459)
						{
							num2 = 1;
							continue;
						}
						if (true)
						{
						}
						num2 = 5;
						continue;
					}
					break;
					IL_E4:
					num2 = 2;
					continue;
					IL_15B:
					num++;
					num2 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x000D66B4 File Offset: 0x000D56B4
	private void ᜀ(spr\u22B7 A_0, ref int A_1)
	{
		for (;;)
		{
			int num = A_1;
			sprẖ sprẖ = null;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_4D;
				case 1:
					if (A_0.ᜅ().ContainsKey(138))
					{
						num2 = 2;
						continue;
					}
					return;
				case 2:
					sprẖ = (A_0.ᜅ()[138] as sprẖ);
					A_0.ᜅ().Remove(138);
					num2 = 5;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						if (false)
						{
						}
						num2 = 11;
						continue;
					}
					break;
				case 4:
					sprẖ = (A_0.ᜅ()[267] as sprẖ);
					num2 = 6;
					continue;
				case 5:
					return;
				case 6:
					if (num == A_1)
					{
						num2 = 12;
						continue;
					}
					goto IL_6C;
				case 7:
					if (true)
					{
					}
					goto IL_6C;
				case 8:
					goto IL_A0;
				case 9:
					if (A_0.ᜅ().ContainsKey(267))
					{
						num2 = 3;
						continue;
					}
					goto IL_15B;
				case 10:
					sprẖ = (A_0.ᜅ()[128] as sprẖ);
					A_1 += 65536;
					sprẖ.ᜀ((uint)A_1);
					num2 = 8;
					continue;
				case 11:
					if ((this as spr\u2459).ᜅ().ᜊ() == EscherShapeType.msosptHostControl)
					{
						num2 = 4;
						continue;
					}
					goto IL_15B;
				case 12:
					A_1 += 65536;
					num2 = 7;
					continue;
				case 13:
					goto IL_15B;
				}
				break;
				IL_4D:
				if (A_0.ᜅ().ContainsKey(128))
				{
					num2 = 10;
					continue;
				}
				goto IL_A0;
				IL_6C:
				sprẖ.ᜀ((uint)A_1);
				num2 = 13;
				continue;
				IL_A0:
				num2 = 9;
				continue;
				IL_15B:
				num2 = 1;
			}
		}
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x000D68C4 File Offset: 0x000D58C4
	private void ᜀ(spr\u2402 A_0, TextBoxItemCollection A_1, ref int A_2, ref int A_3, ref int A_4)
	{
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 6;
				continue;
			case 1:
				if (A_1.Count > 0)
				{
					num = 8;
					continue;
				}
				return;
			case 2:
				goto IL_B4;
			case 3:
				return;
			case 4:
				goto IL_159;
			case 5:
				num = 14;
				continue;
			case 6:
				if ((this as spr\u2459).ᜅ().ᜊ() != EscherShapeType.msosptHostControl)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B4;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 7:
				num = 10;
				continue;
			case 8:
				(A_1[A_4] as TextBox).Spid = A_0.ᜀ();
				A_4++;
				num = 3;
				continue;
			case 9:
				if (A_1 != null)
				{
					num = 7;
					continue;
				}
				return;
			case 10:
				if (this is spr\u2459)
				{
					num = 5;
					continue;
				}
				return;
			case 11:
				goto IL_61;
			case 12:
				if ((this as spr\u2459).ᜌ().ᜆ() == null)
				{
					num = 0;
					continue;
				}
				goto IL_159;
			case 14:
				if ((this as spr\u2459).ᜌ() != null)
				{
					num = 2;
					continue;
				}
				return;
			}
			if (A_0.ᜊ() == EscherShapeType.msosptPictureFrame)
			{
				num = 11;
				continue;
			}
			if (true)
			{
			}
			A_0.ᜀ(A_2);
			A_2++;
			num = 9;
			continue;
			IL_B4:
			num = 12;
			continue;
			IL_159:
			num = 1;
		}
		IL_61:
		A_0.ᜀ(A_3);
		A_3++;
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x000D6AA0 File Offset: 0x000D5AA0
	internal override void \u170D()
	{
		int num = 5;
		for (;;)
		{
			if (true)
			{
			}
			int num2;
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				goto IL_12D;
			case 2:
			{
				if (this.ᜀ.Count == 0)
				{
					num = 14;
					continue;
				}
				object obj = null;
				num2 = 0;
				int count = this.ᜀ.Count;
				num = 1;
				continue;
			}
			case 3:
			{
				object obj;
				if (obj is spr\u2542)
				{
					num = 11;
					continue;
				}
				num = 10;
				continue;
			}
			case 4:
				return;
			case 6:
				goto IL_C3;
			case 7:
				goto IL_C3;
			case 8:
			{
				object obj;
				(obj as spr\u23F8).\u170D();
				num = 7;
				continue;
			}
			case 9:
			{
				int count;
				if (num2 >= count)
				{
					num = 4;
					continue;
				}
				object obj = this.ᜀ[num2];
				num = 3;
				continue;
			}
			case 10:
			{
				object obj;
				if (obj is spr\u2192)
				{
					num = 16;
					continue;
				}
				num = 12;
				continue;
			}
			case 11:
			{
				object obj;
				(obj as spr\u2542).\u170D();
				num = 6;
				continue;
			}
			case 12:
			{
				object obj;
				if (obj is spr\u23F8)
				{
					num = 8;
					continue;
				}
				goto IL_C3;
			}
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_113;
				default:
					if (false)
					{
					}
					goto IL_12D;
				}
				break;
			case 14:
				goto IL_113;
			case 15:
				goto IL_C3;
			case 16:
			{
				object obj;
				(obj as spr\u2192).\u170D();
				num = 15;
				continue;
			}
			}
			if (this.ᜀ != null)
			{
				num = 0;
				continue;
			}
			break;
			IL_C3:
			num2++;
			num = 13;
			continue;
			IL_12D:
			num = 9;
		}
		IL_BB:
		this.ᜀ = null;
		return;
		IL_113:
		goto IL_BB;
	}

	// Token: 0x04001459 RID: 5209
	private new sprᵲ ᜀ;
}
