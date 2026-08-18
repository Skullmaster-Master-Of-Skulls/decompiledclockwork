using System;
using System.Collections;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.XLS;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000057 RID: 87
internal class sprᲺ
{
	// Token: 0x060002CD RID: 717 RVA: 0x0001A280 File Offset: 0x00019280
	internal object ᜅ()
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
		return this.ᜂ();
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0001A2C4 File Offset: 0x000192C4
	internal void ᜁ(object A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0001A308 File Offset: 0x00019308
	internal string ᜃ()
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜁ = HyperlinksCollectionEditor.b("ጭ", a_) + this.ᜁ(null);
				num = 1;
				continue;
			case 1:
				goto IL_5F;
			}
			if (this.ᜁ != null)
			{
				goto IL_8F;
			}
			num = 0;
		}
		IL_5F:
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
			break;
		}
		IL_8F:
		return this.ᜁ;
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0001A3AC File Offset: 0x000193AC
	internal void ᜀ(string A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜁ = A_0;
				this.ᜀ(A_0, this.ᜂ);
				num = 2;
				continue;
			case 2:
				goto IL_50;
			}
			if (!(this.ᜁ != A_0))
			{
				return;
			}
			num = 1;
		}
		IL_50:
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
			break;
		}
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0001A43C File Offset: 0x0001943C
	internal sprᲺ(WorkSheet A_0, object[] A_1, FormulaOptions A_2, object[] A_3)
	{
		this.ᜂ = A_0;
		this.ᜅ = A_1;
		this.ᜄ = A_2;
		this.ᜇ = A_3;
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0001A46C File Offset: 0x0001946C
	internal sprᲺ(string A_0, WorkSheet A_1)
	{
		this.ᜁ = A_0;
		this.ᜂ = A_1;
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0001A498 File Offset: 0x00019498
	internal void ᜄ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_48;
			case 1:
				this.ᜇ = this.ᜀ(this.ᜆ);
				num = 0;
				continue;
			}
			if (this.ᜆ == null)
			{
				return;
			}
			num = 1;
		}
		IL_48:
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		}
		if (false)
		{
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0001A520 File Offset: 0x00019520
	private void ᜀ(object A_0)
	{
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 is bool))
				{
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_71;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 1:
				return;
			case 2:
				num = 5;
				continue;
			case 3:
				if (A_0 is string)
				{
					num = 2;
					continue;
				}
				return;
			case 4:
				return;
			case 5:
				if (spr\u22D3.ᜂ.IndexOf(A_0) != -1)
				{
					num = 6;
					continue;
				}
				this.ᜈ = (string)A_0;
				num = 1;
				continue;
			case 6:
				this.ᜅ[2] = (byte)spr\u22D3.ᜁ[A_0];
				num = 4;
				continue;
			case 8:
				goto IL_71;
			case 9:
				goto IL_81;
			case 10:
				goto IL_4C;
			}
			if (A_0 is double)
			{
				num = 10;
				continue;
			}
			num = 0;
			continue;
			IL_71:
			if (true)
			{
			}
			num = 9;
		}
		IL_4C:
		BitConverter.GetBytes((double)A_0).CopyTo(this.ᜅ, 0);
		return;
		IL_81:
		this.ᜅ[2] = (((bool)A_0) ? 0 : 1);
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0001A694 File Offset: 0x00019694
	private object ᜂ()
	{
		int num = 2;
		for (;;)
		{
			byte b;
			switch (num)
			{
			case 0:
				if ((byte)this.ᜅ[7] == 255)
				{
					num = 6;
					continue;
				}
				goto IL_130;
			case 1:
				num = 5;
				continue;
			case 3:
				switch (b)
				{
				case 0:
					goto IL_11C;
				case 1:
					goto IL_7B;
				case 2:
					goto IL_109;
				case 3:
					goto IL_93;
				default:
					num = 1;
					continue;
				}
				break;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_95;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 5:
				goto IL_12E;
			case 6:
				goto IL_95;
			}
			if ((byte)this.ᜅ[6] == 255)
			{
				num = 4;
				continue;
			}
			goto IL_130;
			IL_95:
			b = (byte)this.ᜅ[0];
			num = 3;
		}
		IL_7B:
		return sprᲺ.ᜀ((byte)this.ᜅ[2]);
		IL_93:
		return null;
		IL_109:
		return sprᲺ.ᜁ((byte)this.ᜅ[2]);
		IL_11C:
		return this.ᜈ;
		IL_12E:
		return this.ᜀ();
		IL_130:
		return null;
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x0001A7D4 File Offset: 0x000197D4
	private string ᜁ(sprạ[] A_0)
	{
		Stack stack;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_83:
				this.ᜀ(stack);
				num = 3;
				break;
			default:
				if (false)
				{
				}
				stack = new Stack();
				num = 5;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (stack.Count != 0)
					{
						num = 2;
						continue;
					}
					goto IL_B9;
				case 1:
					this.ᜀ(stack, A_0);
					num = 4;
					continue;
				case 2:
					goto IL_79;
				case 3:
					goto IL_61;
				case 4:
					goto IL_61;
				case 5:
					if (A_0 != null)
					{
						num = 1;
						continue;
					}
					goto IL_83;
				}
				break;
				IL_61:
				num = 0;
			}
		}
		IL_79:
		if (true)
		{
		}
		return stack.Peek().ToString();
		IL_B9:
		return string.Empty;
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x0001A8A0 File Offset: 0x000198A0
	private void ᜀ(Stack A_0, sprạ[] A_1)
	{
		for (;;)
		{
			int num = 0;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_36;
				case 1:
					goto IL_36;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						if (num >= A_1.Length)
						{
							num2 = 3;
							continue;
						}
						sprᲺ.ᜄ(A_1[num], A_0);
						num++;
						num2 = 0;
						continue;
					}
					break;
				case 3:
					return;
				}
				break;
				IL_36:
				num2 = 2;
			}
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0001A930 File Offset: 0x00019930
	private bool ᜀ(Stack A_0)
	{
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				IL_3F:
				byte[] array = new byte[this.ᜇ.Length];
				Array.Copy(this.ᜇ, 0, array, 0, this.ᜇ.Length);
				int num = 0;
				bool flag = false;
				result = false;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_9B:
					goto IL_9D;
				default:
					if (false)
					{
					}
					num2 = 0;
					break;
				}
				for (;;)
				{
					IL_10:
					sprạ sprạ;
					switch (num2)
					{
					case 0:
						goto IL_9B;
					case 1:
						return result;
					case 2:
						goto IL_C2;
					case 3:
						goto IL_C2;
					case 4:
						if (sprạ.ᜎ().ᜁ())
						{
							num2 = 6;
							continue;
						}
						flag = true;
						num2 = 2;
						continue;
					case 5:
						if (true)
						{
						}
						if (!flag)
						{
							num2 = 1;
							continue;
						}
						return false;
					case 6:
						result = true;
						num2 = 3;
						continue;
					case 7:
						num2 = 5;
						continue;
					case 8:
						goto IL_E0;
					case 9:
						if (num >= this.ᜇ.Length)
						{
							num2 = 7;
							continue;
						}
						sprạ = spr\u1C33.ᜀ(this.ᜂ, array, num);
						num2 = 4;
						continue;
					}
					goto IL_3F;
					IL_C2:
					sprᲺ.ᜄ(sprạ, A_0);
					num += sprạ.ᜄ();
					num2 = 8;
				}
				IL_E0:
				IL_9D:
				num2 = 9;
				goto IL_10;
			}
			return result;
		}
		}
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0001AAA4 File Offset: 0x00019AA4
	private static void ᜄ(sprạ A_0, Stack A_1)
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_150;
			case 1:
				goto IL_5E;
			case 2:
				if (A_0.ᜎ().ᜁ())
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_68;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 4:
				if (A_0.ᜎ().ᜂ())
				{
					num = 7;
					continue;
				}
				num = 6;
				continue;
			case 5:
				goto IL_104;
			case 6:
				if (A_0.ᜎ().ᜀ())
				{
					num = 0;
					continue;
				}
				num = 9;
				continue;
			case 7:
				goto IL_90;
			case 8:
				goto IL_D7;
			case 9:
				if (A_0.ᜎ().ᜅ())
				{
					num = 5;
					continue;
				}
				num = 2;
				continue;
			}
			if (A_0.ᜎ().ᜃ())
			{
				num = 1;
			}
			else
			{
				num = 4;
			}
		}
		IL_5E:
		sprᲺ.ᜃ(A_0, A_1);
		return;
		IL_68:
		sprᲺ.ᜀ(A_0, A_1);
		return;
		IL_90:
		sprᲺ.ᜂ(A_0, A_1);
		return;
		IL_D7:
		throw new ArgumentException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("崛氝䜟儡笣漥䘧尩䴫䈭夯嘱怳夵匷弹刻", a_)), A_0));
		IL_104:
		sprᲺ.ᜁ(A_0, A_1);
		return;
		IL_150:
		goto IL_68;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0001AC18 File Offset: 0x00019C18
	private void ᜀ(string A_0, WorkSheet A_1)
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
		sprạ[] array = new spr\u21BC(A_1).\u1712(A_0);
		this.ᜆ = array;
		this.ᜁ();
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0001AC70 File Offset: 0x00019C70
	private object[] ᜀ(sprạ[] A_0)
	{
		switch (0)
		{
		default:
		{
			object[] array2;
			for (;;)
			{
				ArrayList arrayList = new ArrayList();
				int num = 0;
				int num2 = 0;
				int num3 = 5;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_122;
					case 1:
					{
						int num4;
						if (num4 >= arrayList.Count)
						{
							num3 = 2;
							continue;
						}
						if (true)
						{
						}
						byte[] array = arrayList[num4] as byte[];
						int num5;
						Array.Copy(array, 0, array2, num5, array.Length);
						num5 += array.Length;
						num4++;
						num3 = 4;
						continue;
					}
					case 2:
						return array2;
					case 3:
					{
						array2 = new object[num];
						int num5 = 0;
						int num4 = 0;
						num3 = 7;
						continue;
					}
					case 4:
						goto IL_FB;
					case 5:
						goto IL_4A;
					case 6:
					{
						if (num2 >= A_0.Length)
						{
							num3 = 3;
							continue;
						}
						byte[] array3 = A_0[num2].ᜁ();
						arrayList.Add(array3);
						num += array3.Length;
						num2++;
						num3 = 0;
						continue;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4A;
						default:
							if (false)
							{
							}
							goto IL_FB;
						}
						break;
					}
					break;
					IL_FB:
					num3 = 1;
					continue;
					IL_122:
					num3 = 6;
					continue;
					IL_4A:
					goto IL_122;
				}
			}
			return array2;
		}
		}
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0001ADC4 File Offset: 0x00019DC4
	private void ᜁ()
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
		this.ᜁ(0);
		byte b = 0;
		this.ᜅ = new object[]
		{
			b,
			b,
			b,
			b,
			b,
			b,
			b,
			b
		};
		this.ᜄ = FormulaOptions.CalculateOnLoad;
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0001AE6C File Offset: 0x00019E6C
	private static void ᜃ(sprạ A_0, Stack A_1)
	{
		int a_ = 4;
		string text;
		for (;;)
		{
			IL_47:
			text = (A_1.Pop() as string);
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_77;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (A_0.\u170D() == 20)
						{
							num = 2;
							continue;
						}
						num = 1;
						continue;
					case 1:
						if (A_0.\u170D() == 21)
						{
							num = 3;
							continue;
						}
						goto IL_D5;
					case 2:
						goto IL_75;
					case 3:
						goto IL_C0;
					}
					goto IL_47;
				}
			}
		}
		IL_75:
		A_1.Push(text + A_0.ToString());
		return;
		IL_77:
		A_1.Push(HyperlinksCollectionEditor.b("ࠟ", a_) + text + HyperlinksCollectionEditor.b("ट", a_));
		return;
		IL_C0:
		goto IL_77;
		IL_D5:
		A_1.Push(A_0.ToString() + text);
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0001AF60 File Offset: 0x00019F60
	private static void ᜂ(sprạ A_0, Stack A_1)
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
		string str = A_1.Pop() as string;
		string str2 = A_1.Pop() as string;
		A_1.Push(str2 + A_0.ToString() + str);
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0001AFC8 File Offset: 0x00019FC8
	private static void ᜁ(sprạ A_0, Stack A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 10;
			StringBuilder stringBuilder;
			for (;;)
			{
				byte b;
				byte b2;
				byte b3;
				string[] array;
				byte b4;
				switch (num)
				{
				case 0:
					if (b != 1)
					{
						num = 6;
						continue;
					}
					goto IL_EA;
				case 1:
					b2 = (A_0 as spr\u2341).ᜂ();
					goto IL_198;
				case 2:
					goto IL_1E7;
				case 3:
					b = b3;
					num = 12;
					continue;
				case 4:
					b2 = (A_0 as sprᮺ).ᜂ();
					goto IL_198;
				case 5:
					num = 4;
					continue;
				case 6:
					stringBuilder.Append(HyperlinksCollectionEditor.b("ഠ", a_));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FB;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 7:
					goto IL_1E7;
				case 8:
					goto IL_EA;
				case 9:
					goto IL_172;
				case 11:
					goto IL_155;
				case 12:
					goto IL_155;
				case 13:
				{
					if (b <= 0)
					{
						num = 9;
						continue;
					}
					string value = array[(int)(b - 1)];
					stringBuilder.Append(value);
					num = 0;
					continue;
				}
				case 14:
					if (b4 >= b3)
					{
						num = 3;
						continue;
					}
					array[(int)b4] = (A_1.Pop() as string);
					b4 += 1;
					num = 2;
					continue;
				}
				if (A_0 is sprᮺ)
				{
					num = 5;
					continue;
				}
				num = 1;
				continue;
				IL_FB:
				num = 11;
				continue;
				IL_EA:
				b -= 1;
				goto IL_FB;
				IL_155:
				num = 13;
				continue;
				IL_198:
				b3 = b2;
				sprᮺ sprᮺ = A_0 as sprᮺ;
				stringBuilder = new StringBuilder();
				stringBuilder.Append(A_0.ToString());
				stringBuilder.Append(HyperlinksCollectionEditor.b("ठ", a_));
				array = new string[(int)b3];
				b4 = 0;
				if (true)
				{
				}
				num = 7;
				continue;
				IL_1E7:
				num = 14;
			}
			IL_172:
			stringBuilder.Append(HyperlinksCollectionEditor.b("ࠠ", a_));
			A_1.Push(stringBuilder.ToString());
			return;
		}
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0001B200 File Offset: 0x0001A200
	private static void ᜀ(sprạ A_0, Stack A_1)
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
		A_1.Push(A_0.ToString());
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0001B248 File Offset: 0x0001A248
	private double ᜀ()
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
		byte[] array = new byte[8];
		Array.Copy(this.ᜅ, 0, array, 0, 8);
		return BitConverter.ToDouble(array, 0);
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0001B2A0 File Offset: 0x0001A2A0
	internal static string ᜁ(byte A_0)
	{
		int a_ = 10;
		for (;;)
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BB;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_183;
					default:
						goto IL_1D7;
					}
					break;
				case 2:
					if (A_0 == 0)
					{
						num = 16;
						continue;
					}
					num = 14;
					continue;
				case 3:
					goto IL_F9;
				case 4:
					goto IL_20A;
				case 5:
					if (true)
					{
					}
					num = 2;
					continue;
				case 6:
					if (A_0 == 36)
					{
						num = 8;
						continue;
					}
					num = 11;
					continue;
				case 7:
					if (A_0 <= 29)
					{
						num = 17;
						continue;
					}
					num = 6;
					continue;
				case 8:
					goto IL_17E;
				case 9:
					if (A_0 <= 15)
					{
						num = 5;
						continue;
					}
					num = 7;
					continue;
				case 10:
					if (A_0 == 29)
					{
						num = 3;
						continue;
					}
					goto IL_20F;
				case 11:
					if (A_0 == 42)
					{
						num = 13;
						continue;
					}
					goto IL_20F;
				case 12:
					if (A_0 == 23)
					{
						num = 4;
						continue;
					}
					num = 10;
					continue;
				case 13:
					goto IL_161;
				case 14:
					if (A_0 == 7)
					{
						num = 0;
						continue;
					}
					goto IL_183;
				case 15:
					if (A_0 == 15)
					{
						num = 1;
						continue;
					}
					goto IL_20F;
				case 16:
					goto IL_1BC;
				case 17:
					num = 12;
					continue;
				}
				break;
				IL_183:
				num = 15;
			}
		}
		IL_BB:
		return HyperlinksCollectionEditor.b("ԥ氧挩稫ĭ/ጱ", a_);
		IL_F9:
		return HyperlinksCollectionEditor.b("ԥ昧欩愫欭༯", a_);
		IL_161:
		return HyperlinksCollectionEditor.b("ԥ昧ԩ洫༭", a_);
		IL_17E:
		return HyperlinksCollectionEditor.b("ԥ昧缩愫༭", a_);
		IL_1BC:
		return HyperlinksCollectionEditor.b("ԥ昧缩怫戭ᄯ", a_);
		IL_1D7:
		if (false)
		{
		}
		return HyperlinksCollectionEditor.b("ԥ縧欩怫笭甯ጱ", a_);
		IL_20A:
		return HyperlinksCollectionEditor.b("ԥ稧漩樫༭", a_);
		IL_20F:
		return HyperlinksCollectionEditor.b("ԥ洧砩縫愭戯ጱ", a_);
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0001B4CC File Offset: 0x0001A4CC
	internal static bool ᜀ(byte A_0)
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
		return A_0 == 1;
	}

	// Token: 0x040000CA RID: 202
	private const int ᜀ = 16;

	// Token: 0x040000CB RID: 203
	private string ᜁ;

	// Token: 0x040000CC RID: 204
	private WorkSheet ᜂ;

	// Token: 0x040000CD RID: 205
	internal ArrayList ᜃ;

	// Token: 0x040000CE RID: 206
	internal FormulaOptions ᜄ;

	// Token: 0x040000CF RID: 207
	internal object[] ᜅ;

	// Token: 0x040000D0 RID: 208
	private sprạ[] ᜆ;

	// Token: 0x040000D1 RID: 209
	internal object[] ᜇ;

	// Token: 0x040000D2 RID: 210
	private string ᜈ;
}
