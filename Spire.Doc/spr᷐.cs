using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200033E RID: 830
[CLSCompliant(false)]
internal class spr\u1DD0
{
	// Token: 0x06002C5B RID: 11355 RVA: 0x002AD5E4 File Offset: 0x002AC5E4
	internal spr\u1DD0(sprᾱ A_0, sprὀ A_1)
	{
		this.ᜁ = A_0;
		this.ᜂ = A_1;
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x002AD660 File Offset: 0x002AC660
	internal sprᾱ ᜁ()
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

	// Token: 0x06002C5D RID: 11357 RVA: 0x002AD6A4 File Offset: 0x002AC6A4
	internal long ᜃ()
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
		return this.ᜎ;
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x002AD6E8 File Offset: 0x002AC6E8
	internal int ᜂ()
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
		return this.ᜈ.Count;
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x002AD730 File Offset: 0x002AC730
	internal sprὀ ᜀ()
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

	// Token: 0x06002C60 RID: 11360 RVA: 0x002AD774 File Offset: 0x002AC774
	internal spr\u20CB ᜃ(int A_0)
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
		return this.\u170D[A_0];
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x002AD7B8 File Offset: 0x002AC7B8
	internal void ᜀ(uint A_0, spr\u24D2 A_1)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_39;
			case 2:
				goto IL_6E;
			case 3:
				A_1 = new spr\u24D2();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_70;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 4:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A3;
			}
			if (A_0 < 0U)
			{
				num = 0;
				continue;
			}
			IL_70:
			num = 4;
		}
		IL_39:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᩩͫᵭ", a_));
		IL_6E:
		IL_A3:
		this.ᜅ.Add(A_0);
		this.ᜆ.Add(A_1);
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x002AD880 File Offset: 0x002AC880
	internal void ᜀ(uint A_0, spr\u2618 A_1, MemoryStream A_2)
	{
		int a_ = 3;
		int num = 5;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_33;
			default:
				goto IL_33;
			}
			IL_5B:
			if (true)
			{
			}
			if (A_0 < 0U)
			{
				num = 6;
				continue;
			}
			num = 0;
			continue;
			goto IL_5B;
			IL_33:
			if (false)
			{
			}
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_71;
			case 1:
				A_1 = new spr\u2618();
				num = 2;
				continue;
			case 2:
				goto IL_71;
			case 3:
				goto IL_9D;
			case 4:
				if (A_1.ᜇ() < 485)
				{
					num = 3;
					continue;
				}
				goto IL_EF;
			case 6:
				goto IL_6F;
			}
			goto IL_5B;
			IL_71:
			this.ᜃ.Add(A_0);
			num = 4;
		}
		IL_6F:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᥨѪṬ", a_));
		IL_9D:
		this.ᜄ.Add(A_1);
		return;
		IL_EF:
		int a_2 = (int)A_2.Position;
		short value = (short)A_1.ᜁ().ᜇ();
		byte[] bytes = BitConverter.GetBytes(value);
		A_2.Write(bytes, 0, bytes.Length);
		A_1.ᜀ(A_2);
		A_1 = new spr\u2618();
		A_1.ᜁ().ᜁ(26182, a_2);
		this.ᜄ.Add(A_1);
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x002AD9D0 File Offset: 0x002AC9D0
	internal void ᜀ(int A_0, spr\u20CB A_1)
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
		this.ᜇ.Add(A_0);
		this.ᜈ.Add(A_1);
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x002ADA24 File Offset: 0x002ACA24
	internal void ᜀ(MemoryStream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				uint num;
				int num2;
				int num3;
				spr\u2572[] array;
				int num4;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_141:
					A_0.Position = (long)((ulong)num);
					this.\u170D[num2] = new spr\u20CB(A_0);
					num3 = 7;
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					this.ᜉ = this.ᜀ(A_0, this.ᜂ.\u1716());
					this.ᜌ = new spr\u19F9[this.ᜉ.Length];
					this.ᜊ = this.ᜀ(A_0, this.ᜂ.ᜈ());
					this.ᜋ = new spr\u1804[this.ᜊ.Length];
					array = this.ᜂ.ᜄ().ᜁ();
					this.\u170D = new spr\u20CB[array.Length];
					num2 = 0;
					num4 = array.Length;
					num3 = 6;
					break;
				}
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_141;
					case 1:
						if (num != 4294967295U)
						{
							num3 = 0;
							continue;
						}
						this.\u170D[num2] = new spr\u20CB(true);
						num3 = 2;
						continue;
					case 2:
						goto IL_EE;
					case 3:
						if (num2 >= num4)
						{
							num3 = 5;
							continue;
						}
						num = array[num2].ᜄ();
						num3 = 1;
						continue;
					case 4:
						goto IL_143;
					case 5:
						goto IL_15F;
					case 6:
						goto IL_143;
					case 7:
						goto IL_EE;
					}
					break;
					IL_EE:
					num2++;
					num3 = 4;
					continue;
					IL_143:
					num3 = 3;
				}
			}
			IL_15F:
			this.ᜎ = A_0.Position;
			return;
		}
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x002ADBC8 File Offset: 0x002ACBC8
	internal void ᜄ(Stream A_0)
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
		this.ᜂ(A_0);
		this.ᜃ(A_0);
		this.ᜁ(A_0);
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x002ADC18 File Offset: 0x002ACC18
	internal spr\u1804 ᜄ(int A_0)
	{
		if (true)
		{
		}
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_40:
			if (this.ᜋ[A_0] != null)
			{
				goto IL_73;
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜋ[A_0] = new spr\u1804(this.ᜊ[A_0]);
				num = 2;
				continue;
			case 2:
				goto IL_71;
			}
			break;
		}
		goto IL_40;
		IL_71:
		IL_73:
		return this.ᜋ[A_0];
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x002ADCAC File Offset: 0x002ACCAC
	internal spr\u19F9 ᜂ(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_40:
			if (this.ᜌ[A_0] != null)
			{
				goto IL_73;
			}
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_24;
			case 1:
				goto IL_71;
			case 2:
				this.ᜌ[A_0] = new spr\u19F9(this.ᜉ[A_0]);
				num = 1;
				continue;
			}
			goto IL_40;
		}
		IL_24:
		if (true)
		{
		}
		goto IL_40;
		IL_71:
		IL_73:
		return this.ᜌ[A_0];
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x002ADD40 File Offset: 0x002ACD40
	internal void ᜁ(uint A_0)
	{
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_45:
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (num2 >= 0)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
				{
					spr\u2618 value = this.ᜄ[num2];
					this.ᜄ[this.ᜄ.Count - 1] = value;
					num = 1;
					continue;
				}
				}
				goto IL_38;
			}
			return;
		}
		default:
			if (false)
			{
			}
			break;
		}
		IL_38:
		num2 = this.ᜃ.IndexOf(A_0);
		goto IL_45;
	}

	// Token: 0x06002C69 RID: 11369 RVA: 0x002ADDE4 File Offset: 0x002ACDE4
	internal void ᜀ(uint A_0)
	{
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_45:
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (num2 >= 0)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
				{
					spr\u24D2 value = this.ᜆ[num2];
					this.ᜆ[this.ᜆ.Count - 1] = value;
					num = 2;
					continue;
				}
				case 2:
					return;
				}
				goto IL_38;
			}
			return;
		}
		default:
			if (false)
			{
			}
			break;
		}
		IL_38:
		num2 = this.ᜅ.IndexOf(A_0);
		goto IL_45;
	}

	// Token: 0x06002C6A RID: 11370 RVA: 0x002ADE88 File Offset: 0x002ACE88
	internal spr\u193A[] ᜀ(MemoryStream A_0, spr\u2039 A_1)
	{
		switch (0)
		{
		default:
		{
			spr\u193A[] array;
			for (;;)
			{
				int num = A_1.ᜀ().Length;
				array = new spr\u193A[num];
				int num2 = 0;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							goto IL_BB;
						}
						break;
					case 1:
						if (true)
						{
						}
						if (num2 >= num)
						{
							num3 = 0;
							continue;
						}
						goto IL_6E;
					case 2:
						goto IL_4E;
					case 3:
						goto IL_4E;
					}
					break;
					IL_4E:
					num3 = 1;
					continue;
					IL_6E:
					int num4 = A_1.ᜀ()[num2].ᜀ();
					A_0.Position = (long)(num4 * 512);
					array[num2] = new spr\u193A(A_0);
					num2++;
					num3 = 2;
				}
			}
			IL_BB:
			if (false)
			{
			}
			return array;
		}
		}
	}

	// Token: 0x06002C6B RID: 11371 RVA: 0x002ADF58 File Offset: 0x002ACF58
	private void ᜃ(Stream A_0)
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				int count = this.ᜃ.Count;
				int num = 0;
				uint num2 = (uint)this.ᜁ.ញ();
				BinaryWriter a_ = new BinaryWriter(A_0);
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_65;
					case 1:
						if (num >= count)
						{
							num3 = 3;
							continue;
						}
						goto IL_7D;
					case 2:
						goto IL_65;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7D;
						default:
							goto IL_E2;
						}
						break;
					}
					break;
					IL_65:
					num3 = 1;
					continue;
					IL_7D:
					spr\u1804 spr_u = new spr\u1804();
					num = this.ᜀ(spr_u, num2, num);
					int a_2 = this.ᜀ(A_0);
					this.ᜂ.ᜁ(num2, a_2);
					spr_u.ᜀ(a_, A_0);
					num2 = this.ᜃ[num - 1];
					num3 = 2;
				}
			}
			IL_E2:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x002AE050 File Offset: 0x002AD050
	private int ᜀ(spr\u1804 A_0, uint A_1, int A_2)
	{
		for (;;)
		{
			A_0.ᜀ(this.ᜁ(A_2));
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D7;
				case 1:
				{
					if (A_0.ᜀ() == 0)
					{
						num = 5;
						continue;
					}
					A_0.ᜄ()[0] = A_1;
					int num2 = 0;
					int num3 = A_0.ᜀ();
					num = 0;
					continue;
				}
				case 2:
					return A_2;
				case 3:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 2;
						continue;
					}
					A_0.ᜅ()[num2] = this.ᜄ[A_2];
					A_0.ᜄ()[num2 + 1] = this.ᜃ[A_2];
					A_2++;
					num2++;
					num = 4;
					continue;
				}
				case 4:
					goto IL_D7;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_C6;
					}
					break;
				}
				break;
				IL_D7:
				if (true)
				{
				}
				num = 3;
			}
		}
		IL_C6:
		if (false)
		{
		}
		throw new Exception(string.Empty);
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x002AE15C File Offset: 0x002AD15C
	private int ᜁ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				for (;;)
				{
					int num = 0;
					int count = this.ᜃ.Count;
					num2 = A_0;
					int num3 = 0;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_54;
						case 1:
							goto IL_72;
						case 2:
						{
							spr\u2618 spr_u;
							num += spr_u.ᜇ() + 13 + 4;
							num2++;
							num3 = 4;
							continue;
						}
						case 3:
						{
							spr\u2618 spr_u;
							if (spr_u.ᜇ() + 13 + 8 + num <= 511)
							{
								num3 = 2;
								continue;
							}
							goto IL_E8;
						}
						case 4:
							goto IL_54;
						case 5:
							if (true)
							{
							}
							if (num2 >= count)
							{
								num3 = 1;
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
								spr\u2618 spr_u = this.ᜄ[num2];
								num3 = 3;
								continue;
							}
							}
							break;
						}
						break;
						IL_54:
						num3 = 5;
					}
				}
			}
			IL_72:
			IL_E8:
			return num2 - A_0;
		}
		}
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x002AE254 File Offset: 0x002AD254
	private void ᜂ(Stream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int count = this.ᜅ.Count;
				int num = 0;
				uint num2 = (uint)this.ᜁ.ញ();
				BinaryWriter a_ = new BinaryWriter(A_0);
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_5D;
					case 1:
						if (num >= count)
						{
							num3 = 3;
							continue;
						}
						goto IL_75;
					case 2:
						goto IL_5D;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_75;
						default:
							goto IL_DB;
						}
						break;
					}
					break;
					IL_5D:
					num3 = 1;
					continue;
					IL_75:
					spr\u19F9 spr_u19F = new spr\u19F9();
					num = this.ᜀ(spr_u19F, num2, num);
					int a_2 = this.ᜀ(A_0);
					this.ᜂ.ᜀ(num2, a_2);
					spr_u19F.ᜀ(a_, A_0);
					num2 = this.ᜅ[num - 1];
					num3 = 2;
				}
			}
			IL_DB:
			if (false)
			{
			}
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x002AE34C File Offset: 0x002AD34C
	private int ᜀ(spr\u19F9 A_0, uint A_1, int A_2)
	{
		for (;;)
		{
			if (true)
			{
			}
			A_0.ᜀ(this.ᜀ(A_2));
			A_0.ᜂ()[0] = A_1;
			int num = 0;
			int num2 = A_0.ᜁ();
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_49;
				case 1:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					A_0.ᜄ()[num] = this.ᜆ[A_2];
					A_0.ᜂ()[num + 1] = this.ᜅ[A_2];
					A_2++;
					num++;
					num3 = 2;
					continue;
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
						goto IL_49;
					}
					break;
				case 3:
					return A_2;
				}
				break;
				IL_49:
				num3 = 1;
			}
		}
		return A_2;
	}

	// Token: 0x06002C70 RID: 11376 RVA: 0x002AE420 File Offset: 0x002AD420
	private int ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				int num = 0;
				int count = this.ᜅ.Count;
				num2 = A_0;
				int num3 = 0;
				for (;;)
				{
					int num4;
					int num5;
					switch (num3)
					{
					case 0:
						goto IL_132;
					case 1:
						goto IL_156;
					case 2:
						goto IL_BC;
					case 3:
					{
						spr\u24D2 spr_u24D;
						if (spr_u24D.ᜇ() % 2 == 0)
						{
							num3 = 7;
							continue;
						}
						num3 = 10;
						continue;
					}
					case 4:
						if (this.ᜀ(A_0, num2))
						{
							num3 = 9;
							continue;
						}
						num3 = 6;
						continue;
					case 5:
						goto IL_132;
					case 6:
						goto IL_110;
					case 7:
						num3 = 8;
						continue;
					case 8:
					{
						spr\u24D2 spr_u24D;
						num4 = spr_u24D.ᜇ();
						goto IL_15B;
					}
					case 9:
						num3 = 11;
						continue;
					case 10:
					{
						spr\u24D2 spr_u24D;
						num4 = spr_u24D.ᜇ() + 1;
						goto IL_15B;
					}
					case 11:
						if (num + 8 + 1 >= 511)
						{
							goto IL_1DC;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_110;
						default:
							if (false)
							{
							}
							num3 = 15;
							continue;
						}
						break;
					case 12:
						num += num5 + 4 + 1;
						num3 = 13;
						continue;
					case 13:
						goto IL_BC;
					case 14:
					{
						if (num2 >= count)
						{
							if (true)
							{
							}
							num3 = 1;
							continue;
						}
						spr\u24D2 spr_u24D = this.ᜆ[num2];
						num3 = 3;
						continue;
					}
					case 15:
						num += 5;
						num3 = 2;
						continue;
					}
					break;
					IL_BC:
					num2++;
					num3 = 5;
					continue;
					IL_110:
					if (num + num5 + 8 + 1 < 511)
					{
						num3 = 12;
						continue;
					}
					goto IL_1DC;
					IL_132:
					num3 = 14;
					continue;
					IL_15B:
					num5 = num4;
					num3 = 4;
				}
			}
			IL_156:
			IL_1DC:
			return num2 - A_0;
		}
		}
	}

	// Token: 0x06002C71 RID: 11377 RVA: 0x002AE60C File Offset: 0x002AD60C
	internal bool ᜀ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				spr\u24D2 spr_u24D = this.ᜆ[A_1];
				result = false;
				int num = A_0;
				int num2 = 8;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_5E;
					case 1:
						num2 = 3;
						continue;
					case 2:
						goto IL_10C;
					case 3:
					{
						spr\u24D2 spr_u24D2;
						if (result = spr_u24D.ᜁ(spr_u24D2))
						{
							return result;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					}
					case 4:
						goto IL_F0;
					case 5:
					{
						if (num >= A_1)
						{
							num2 = 2;
							continue;
						}
						spr\u24D2 spr_u24D2 = this.ᜆ[num];
						num2 = 9;
						continue;
					}
					case 6:
						num2 = 7;
						continue;
					case 7:
					{
						spr\u24D2 spr_u24D2;
						if (spr_u24D.ᜌ() == spr_u24D2.ᜌ())
						{
							num2 = 1;
							continue;
						}
						goto IL_5E;
					}
					case 8:
						goto IL_F0;
					case 9:
					{
						spr\u24D2 spr_u24D2;
						if (spr_u24D.ᜇ() == spr_u24D2.ᜇ())
						{
							num2 = 6;
							continue;
						}
						goto IL_5E;
					}
					}
					break;
					IL_5E:
					num++;
					num2 = 4;
					continue;
					IL_F0:
					num2 = 5;
				}
			}
			IL_10C:
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06002C72 RID: 11378 RVA: 0x002AE760 File Offset: 0x002AD760
	private void ᜁ(Stream A_0)
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				this.\u170D = new spr\u20CB[this.ᜇ.Count];
				int num = 0;
				int count = this.ᜇ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_68;
					case 1:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						goto IL_80;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_80;
						default:
							goto IL_EA;
						}
						break;
					case 3:
						goto IL_68;
					}
					break;
					IL_68:
					num2 = 1;
					continue;
					IL_80:
					int a_ = this.ᜇ[num];
					spr\u20CB spr_u20CB = this.ᜈ[num];
					int a_2 = this.ᜀ(A_0);
					this.ᜂ.ᜀ(a_, a_2);
					this.\u170D[num] = spr_u20CB;
					spr_u20CB.ᜄ(A_0);
					num++;
					num2 = 0;
				}
			}
			IL_EA:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06002C73 RID: 11379 RVA: 0x002AE860 File Offset: 0x002AD860
	private int ᜀ(Stream A_0)
	{
		int num2;
		for (;;)
		{
			IL_24:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_44;
			default:
				goto IL_44;
			}
			int num;
			int num3;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					return num2;
				case 1:
					num2++;
					num = 6;
					continue;
				case 2:
					if (num3 % 512 != 0)
					{
						num = 1;
						continue;
					}
					goto IL_A4;
				case 3:
					goto IL_D2;
				case 4:
					goto IL_D2;
				case 5:
				{
					if (A_0.Position >= (long)num3)
					{
						num = 0;
						continue;
					}
					byte[] buffer = new byte[(long)num3 - A_0.Position];
					A_0.Write(buffer, 0, (int)((long)num3 - A_0.Position));
					num = 3;
					continue;
				}
				case 6:
					if (true)
					{
					}
					goto IL_A4;
				}
				goto IL_24;
				IL_A4:
				num3 = 512 * num2;
				num = 4;
				continue;
				IL_D2:
				num = 5;
			}
			IL_44:
			if (false)
			{
			}
			num3 = (int)A_0.Position;
			num2 = num3 / 512;
			num = 2;
			goto IL_02;
		}
		return num2;
	}

	// Token: 0x04002626 RID: 9766
	private const int ᜀ = 1;

	// Token: 0x04002627 RID: 9767
	private sprᾱ ᜁ;

	// Token: 0x04002628 RID: 9768
	private sprὀ ᜂ;

	// Token: 0x04002629 RID: 9769
	private List<uint> ᜃ = new List<uint>();

	// Token: 0x0400262A RID: 9770
	private List<spr\u2618> ᜄ = new List<spr\u2618>();

	// Token: 0x0400262B RID: 9771
	private List<uint> ᜅ = new List<uint>();

	// Token: 0x0400262C RID: 9772
	private List<spr\u24D2> ᜆ = new List<spr\u24D2>();

	// Token: 0x0400262D RID: 9773
	private List<int> ᜇ = new List<int>();

	// Token: 0x0400262E RID: 9774
	private List<spr\u20CB> ᜈ = new List<spr\u20CB>();

	// Token: 0x0400262F RID: 9775
	private spr\u193A[] ᜉ = new spr\u193A[0];

	// Token: 0x04002630 RID: 9776
	private spr\u193A[] ᜊ = new spr\u193A[0];

	// Token: 0x04002631 RID: 9777
	private spr\u1804[] ᜋ;

	// Token: 0x04002632 RID: 9778
	private spr\u19F9[] ᜌ;

	// Token: 0x04002633 RID: 9779
	private spr\u20CB[] \u170D;

	// Token: 0x04002634 RID: 9780
	private long ᜎ;
}
