using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003C9 RID: 969
[sprᵴ(MsoRecords.msofbtOPT)]
[CLSCompliant(false)]
[DefaultMember("Item")]
internal class spr\u23E7 : spr\u1D3B, ICloneable, sprᡍ
{
	// Token: 0x06003AE0 RID: 15072 RVA: 0x002103A8 File Offset: 0x0020F3A8
	public spr\u23E7(spr\u1D3B A_0) : base(A_0)
	{
	}

	// Token: 0x06003AE1 RID: 15073 RVA: 0x002103C8 File Offset: 0x0020F3C8
	public spr\u23E7(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003AE2 RID: 15074 RVA: 0x002103EC File Offset: 0x0020F3EC
	public override void ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = this.m_iLength;
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						int num4;
						int count;
						if (num4 >= count)
						{
							num3 = 2;
							continue;
						}
						spr\u23E7.ᜀ ᜀ = this.ᜁ[num4];
						ᜀ.ᜀ(A_0);
						num4++;
						num3 = 10;
						continue;
					}
					case 1:
					{
						spr\u23E7.ᜀ ᜀ2;
						if (ᜀ2.ᜂ())
						{
							num3 = 8;
							continue;
						}
						goto IL_12F;
					}
					case 2:
						return;
					case 3:
						goto IL_7B;
					case 4:
						goto IL_B5;
					case 5:
					{
						if (num >= num2)
						{
							num3 = 9;
							continue;
						}
						spr\u23E7.ᜀ ᜀ2 = new spr\u23E7.ᜀ(A_0);
						this.ᜃ(ᜀ2);
						num3 = 1;
						continue;
					}
					case 6:
						goto IL_12F;
					case 7:
						IL_5F:
						goto IL_7B;
					case 8:
					{
						spr\u23E7.ᜀ ᜀ2;
						num2 -= (int)ᜀ2.ᜆ();
						num3 = 6;
						continue;
					}
					case 9:
					{
						int num4 = 0;
						int count = this.ᜁ.Count;
						num3 = 4;
						continue;
					}
					case 10:
						goto IL_B5;
					}
					break;
					IL_7B:
					if (true)
					{
					}
					num3 = 5;
					continue;
					IL_B5:
					num3 = 0;
					continue;
					IL_12F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5F;
					default:
						if (false)
						{
						}
						num += spr\u23E7.ᜀ.ᜀ();
						num3 = 3;
						break;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06003AE3 RID: 15075 RVA: 0x00210560 File Offset: 0x0020F560
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				int count;
				int num6;
				int num8;
				int num9;
				int num10;
				switch (num)
				{
				case 0:
					if (num2 > 10)
					{
						num = 36;
						continue;
					}
					goto IL_171;
				case 1:
					num = 2;
					continue;
				case 2:
					if (num2 != this.ᜁ.Count)
					{
						if (true)
						{
						}
						num = 20;
						continue;
					}
					goto IL_171;
				case 3:
					goto IL_266;
				case 5:
					if (num3 <= 0)
					{
						num = 8;
						continue;
					}
					num = 30;
					continue;
				case 6:
					goto IL_244;
				case 7:
					goto IL_266;
				case 8:
					num = 21;
					continue;
				case 9:
					return;
				case 10:
					num2--;
					num = 23;
					continue;
				case 11:
				{
					if (num4 >= count)
					{
						num = 9;
						continue;
					}
					spr\u23E7.ᜀ ᜀ = this.ᜁ[num4];
					num = 32;
					continue;
				}
				case 12:
					num3 = (int)this.ᜁ[0].ᜈ();
					num = 5;
					continue;
				case 13:
					goto IL_244;
				case 14:
					if (count > 0)
					{
						num = 12;
						continue;
					}
					goto IL_DB;
				case 15:
					goto IL_44F;
				case 16:
					num4 = 0;
					num = 13;
					continue;
				case 17:
					goto IL_189;
				case 18:
				{
					int num5;
					if (num5 > 100)
					{
						num = 10;
						continue;
					}
					goto IL_171;
				}
				case 19:
				{
					if (num6 >= count)
					{
						num = 16;
						continue;
					}
					spr\u23E7.ᜀ ᜀ2 = this.ᜁ[num6];
					byte[] array = ᜀ2.ᜃ();
					int num7 = array.Length;
					A_0.Write(array, 0, num7);
					this.m_iLength += num7;
					num6++;
					num = 27;
					continue;
				}
				case 20:
					goto IL_F6;
				case 21:
					num8 = 0;
					goto IL_2A4;
				case 22:
				{
					int num5;
					if (num5 <= 1000)
					{
						num = 1;
						continue;
					}
					goto IL_F6;
				}
				case 23:
					goto IL_171;
				case 24:
					goto IL_2C7;
				case 25:
					this.ᜄ = 51;
					num = 37;
					continue;
				case 26:
					goto IL_DB;
				case 27:
					goto IL_44F;
				case 28:
				{
					int count2 = this.ᜁ.Count;
					num = 29;
					continue;
				}
				case 29:
					if (num9 <= 4)
					{
						num = 38;
						continue;
					}
					goto IL_171;
				case 30:
					num8 = 1;
					goto IL_2A4;
				case 31:
				{
					spr\u23E7.ᜀ ᜀ3;
					if (ᜀ3.ᜈ() > (MsoOptions)num3)
					{
						num = 33;
						continue;
					}
					goto IL_2C7;
				}
				case 32:
				{
					spr\u23E7.ᜀ ᜀ;
					if (ᜀ.ᜄ() != null)
					{
						num = 35;
						continue;
					}
					goto IL_189;
				}
				case 33:
				{
					spr\u23E7.ᜀ ᜀ3;
					num3 = (int)ᜀ3.ᜈ();
					num2++;
					num = 24;
					continue;
				}
				case 34:
				{
					if (num10 >= count)
					{
						num = 28;
						continue;
					}
					spr\u23E7.ᜀ ᜀ3 = this.ᜁ[num10];
					num = 31;
					continue;
				}
				case 35:
				{
					spr\u23E7.ᜀ ᜀ;
					byte[] array = ᜀ.ᜄ();
					int num11 = array.Length;
					A_0.Write(array, 0, num11);
					this.m_iLength += num11;
					num = 17;
					continue;
				}
				case 36:
					num = 18;
					continue;
				case 37:
					goto IL_403;
				case 38:
				{
					int count2;
					int num5 = (int)this.ᜁ[count2 - 1].ᜈ();
					num = 22;
					continue;
				}
				}
				if (base.\u1714() == 0)
				{
					num = 25;
					continue;
				}
				goto IL_403;
				IL_DB:
				this.m_iLength = 0;
				num6 = 0;
				num = 15;
				continue;
				IL_F6:
				num = 0;
				continue;
				IL_171:
				base.ᜈ(num2);
				num = 26;
				continue;
				IL_189:
				num4++;
				num = 6;
				continue;
				IL_244:
				num = 11;
				continue;
				IL_266:
				num = 34;
				continue;
				IL_2A4:
				num2 = num8;
				num9 = num3;
				num10 = 1;
				num = 3;
				continue;
				IL_2C7:
				num10++;
				num = 7;
				continue;
				IL_403:
				count = this.ᜁ.Count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 14;
					continue;
				}
				IL_44F:
				num = 19;
			}
			return;
		}
		}
	}

	// Token: 0x06003AE4 RID: 15076 RVA: 0x00210A14 File Offset: 0x0020FA14
	public override object ᜑ()
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
		return this.ᜅ();
	}

	// Token: 0x06003AE5 RID: 15077 RVA: 0x00210A58 File Offset: 0x0020FA58
	protected override object ᜅ()
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
		spr\u23E7 spr_u23E = (spr\u23E7)base.ᜅ();
		spr_u23E.ᜁ = spr\u1CD3.ᜀ<spr\u23E7.ᜀ>(this.ᜁ);
		return spr_u23E;
	}

	// Token: 0x06003AE6 RID: 15078 RVA: 0x00210AB4 File Offset: 0x0020FAB4
	public new spr\u23E7.ᜀ[] ᜀ()
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
		return this.ᜁ.ToArray();
	}

	// Token: 0x06003AE7 RID: 15079 RVA: 0x00210AFC File Offset: 0x0020FAFC
	public spr\u23E7.ᜀ ᜁ(int A_0)
	{
		int a_ = 15;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A8;
			case 1:
				if (A_0 >= this.ᜁ.Count)
				{
					num = 0;
					continue;
				}
				goto IL_AA;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_29;
				}
				if (false)
				{
				}
				num = 1;
				continue;
			}
			IL_29:
			if (A_0 < 0)
			{
				break;
			}
			if (true)
			{
			}
			num = 2;
		}
		IL_65:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱄ⥆ⵈ⹊㕌", a_), RecordTableEnumerator.b("ፄ♆╈㹊⡌潎㉐㉒㭔㥖㙘⽚絜㵞Ѡ䍢।ɦᩨᡪ䵬᭮ᥰቲ᭴坶䥸孺ᱼᅾꎂ歷뎒ﾖ붜즠슢쮤螦쒪\ud8ac솮얰鎲颴鞶袸閺", a_));
		IL_A8:
		goto IL_65;
		IL_AA:
		return this.ᜁ[A_0];
	}

	// Token: 0x06003AE8 RID: 15080 RVA: 0x00210BC0 File Offset: 0x0020FBC0
	public IList<spr\u23E7.ᜀ> ᜁ()
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

	// Token: 0x06003AE9 RID: 15081 RVA: 0x00210C04 File Offset: 0x0020FC04
	public new void ᜃ(spr\u23E7.ᜀ A_0)
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
		this.ᜁ.Add(A_0);
	}

	// Token: 0x06003AEA RID: 15082 RVA: 0x00210C4C File Offset: 0x0020FC4C
	public new void ᜀ(ICollection A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			IEnumerator enumerator = A_0.GetEnumerator();
			try
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						spr\u23E7.ᜀ a_ = (spr\u23E7.ᜀ)enumerator.Current;
						this.ᜃ(a_);
						num = 2;
						continue;
					}
					case 1:
						num = 4;
						continue;
					case 4:
						goto IL_9C;
					}
					IL_7A:
					num = 0;
					continue;
					goto IL_7A;
				}
				IL_9C:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_DC;
						case 1:
							disposable.Dispose();
							num = 0;
							continue;
						case 2:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_DE;
						}
						break;
					}
				}
				IL_DC:
				IL_DE:;
			}
			break;
		}
		}
	}

	// Token: 0x06003AEB RID: 15083 RVA: 0x00210D48 File Offset: 0x0020FD48
	public void ᜁ(spr\u23E7.ᜀ A_0)
	{
		int num;
		for (;;)
		{
			num = this.ᜀ(A_0);
			if (num != this.ᜁ.Count)
			{
				goto IL_53;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2E;
			}
		}
		IL_2E:
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜁ.Add(A_0);
		return;
		IL_53:
		this.ᜁ[num] = A_0;
	}

	// Token: 0x06003AEC RID: 15084 RVA: 0x00210DB8 File Offset: 0x0020FDB8
	public void ᜂ(spr\u23E7.ᜀ A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int count = this.ᜁ.Count;
				MsoOptions msoOptions = A_0.ᜈ();
				int num2 = count;
				int num3 = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num3)
					{
					case 0:
					{
						spr\u23E7.ᜀ ᜀ = this.ᜁ[num];
						num3 = 6;
						continue;
					}
					case 1:
						if (num >= num2)
						{
							num3 = 7;
							continue;
						}
						goto IL_105;
					case 2:
						if (num < count)
						{
							num3 = 0;
							continue;
						}
						goto IL_165;
					case 3:
						goto IL_144;
					case 4:
						num++;
						num3 = 9;
						continue;
					case 5:
						if (this.ᜁ[num].ᜈ() < msoOptions)
						{
							num3 = 4;
							continue;
						}
						goto IL_9E;
					case 6:
					{
						spr\u23E7.ᜀ ᜀ;
						if (ᜀ.ᜈ() == msoOptions)
						{
							num3 = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_105;
						default:
							goto IL_F1;
						}
						break;
					}
					case 7:
						goto IL_9E;
					case 8:
						goto IL_99;
					case 9:
						goto IL_144;
					}
					break;
					IL_9E:
					num3 = 2;
					continue;
					IL_105:
					num3 = 5;
					continue;
					IL_144:
					num3 = 1;
				}
			}
			IL_99:
			this.ᜁ[num] = A_0;
			return;
			IL_F1:
			if (false)
			{
			}
			this.ᜁ.Insert(num, A_0);
			return;
			IL_165:
			this.ᜁ.Add(A_0);
			return;
		}
		}
	}

	// Token: 0x06003AED RID: 15085 RVA: 0x00210F38 File Offset: 0x0020FF38
	private new int ᜀ(spr\u23E7.ᜀ A_0)
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
		return this.ᜀ(A_0.ᜈ());
	}

	// Token: 0x06003AEE RID: 15086 RVA: 0x00210F80 File Offset: 0x0020FF80
	public new void ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int count = this.ᜁ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_AC;
				case 1:
					return;
				case 2:
					goto IL_AA;
				case 3:
				{
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					spr\u23E7.ᜀ ᜀ = this.ᜁ[num];
					num2 = 5;
					continue;
				}
				case 4:
					goto IL_AC;
				case 5:
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
						spr\u23E7.ᜀ ᜀ;
						if (ᜀ.ᜈ() != (MsoOptions)A_0)
						{
							if (true)
							{
							}
							num++;
							num2 = 4;
							continue;
						}
						break;
					}
					}
					num2 = 2;
					continue;
				}
				break;
				IL_AC:
				num2 = 3;
			}
		}
		IL_AA:
		this.ᜁ.RemoveAt(num);
	}

	// Token: 0x06003AEF RID: 15087 RVA: 0x00211058 File Offset: 0x00210058
	public new int ᜀ(MsoOptions A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int count = this.ᜁ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_38;
				case 1:
					if (this.ᜁ[num].ᜈ() != A_0)
					{
						num2 = 3;
						continue;
					}
					return num;
				case 2:
					if (num >= count)
					{
						num2 = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 3:
					num++;
					num2 = 5;
					continue;
				case 4:
					return num;
				case 5:
					goto IL_38;
				}
				break;
				IL_38:
				if (true)
				{
				}
				num2 = 2;
			}
		}
		return num;
	}

	// Token: 0x040019A6 RID: 6566
	private new const int ᜀ = 127;

	// Token: 0x040019A7 RID: 6567
	private new List<spr\u23E7.ᜀ> ᜁ = new List<spr\u23E7.ᜀ>();

	// Token: 0x020003CA RID: 970
	internal new class ᜀ : ICloneable
	{
		// Token: 0x06003AF0 RID: 15088 RVA: 0x0021111C File Offset: 0x0021011C
		public MsoOptions ᜈ()
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
			return (MsoOptions)this.ᜄ;
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x00211160 File Offset: 0x00210160
		public void ᜀ(MsoOptions A_0)
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
			this.ᜄ = (ushort)A_0;
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x002111A4 File Offset: 0x002101A4
		public bool ᜁ()
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
			return this.ᜅ;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x002111E8 File Offset: 0x002101E8
		public void ᜀ(bool A_0)
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
			this.ᜅ = A_0;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x0021122C File Offset: 0x0021022C
		public bool ᜂ()
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
			return this.ᜆ;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x00211270 File Offset: 0x00210270
		public void ᜁ(bool A_0)
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
			this.ᜆ = A_0;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x002112B4 File Offset: 0x002102B4
		public uint ᜆ()
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
			return this.ᜇ;
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x002112F8 File Offset: 0x002102F8
		public void ᜀ(uint A_0)
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
			this.ᜇ = A_0;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x0021133C File Offset: 0x0021033C
		public int ᜅ()
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
			return (int)this.ᜇ;
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x00211380 File Offset: 0x00210380
		public void ᜀ(int A_0)
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
			this.ᜇ = (uint)A_0;
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x002113C4 File Offset: 0x002103C4
		public byte[] ᜄ()
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
			return this.ᜈ;
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x00211408 File Offset: 0x00210408
		public void ᜀ(byte[] A_0)
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
			this.ᜈ = A_0;
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x0021144C File Offset: 0x0021044C
		public byte[] ᜃ()
		{
			byte[] array;
			ushort num;
			for (;;)
			{
				array = new byte[spr\u23E7.ᜀ.ᜀ()];
				num = (this.ᜄ & 16383);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_77;
					case 1:
						num += 16384;
						num2 = 5;
						continue;
					case 2:
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
							if (this.ᜅ)
							{
								num2 = 1;
								continue;
							}
							goto IL_8D;
						}
						break;
					case 3:
						if (this.ᜆ)
						{
							num2 = 0;
							continue;
						}
						goto IL_CD;
					case 4:
						goto IL_8B;
					case 5:
						goto IL_8D;
					}
					break;
					IL_77:
					num += 32768;
					num2 = 4;
					continue;
					IL_8D:
					num2 = 3;
				}
			}
			IL_8B:
			IL_CD:
			BitConverter.GetBytes(num).CopyTo(array, 0);
			BitConverter.GetBytes(this.ᜇ).CopyTo(array, 2);
			return array;
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00211548 File Offset: 0x00210548
		public static int ᜀ()
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
			return 6;
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00211584 File Offset: 0x00210584
		public ᜀ()
		{
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x00211598 File Offset: 0x00210598
		public ᜀ(byte[] A_0, ref int A_1)
		{
			ushort num = BitConverter.ToUInt16(A_0, A_1);
			this.ᜄ = (num & 16383);
			this.ᜅ = ((num & 16384) != 0);
			this.ᜆ = ((num & 32768) != 0);
			A_1 += 2;
			this.ᜇ = BitConverter.ToUInt32(A_0, A_1);
			A_1 += 4;
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00211604 File Offset: 0x00210604
		public ᜀ(Stream A_0)
		{
			ushort num = spr\u1D3B.ᜁ(A_0);
			this.ᜄ = (num & 16383);
			this.ᜅ = ((num & 16384) != 0);
			this.ᜆ = ((num & 32768) != 0);
			this.ᜇ = spr\u1D3B.ᜃ(A_0);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x00211660 File Offset: 0x00210660
		public void ᜀ(byte[] A_0, ref int A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_72;
				case 2:
					if (true)
					{
					}
					this.ᜈ = new byte[this.ᜆ()];
					Array.Copy(A_0, A_1, this.ᜈ, 0, (int)this.ᜆ());
					A_1 += (int)this.ᜆ();
					num = 0;
					continue;
				}
				IL_1C:
				if (this.ᜂ())
				{
					num = 2;
					continue;
				}
				IL_72:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1C;
				}
				break;
			}
			if (false)
			{
			}
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00211708 File Offset: 0x00210708
		public void ᜀ(Stream A_0)
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_62;
				case 1:
				{
					int num2 = (int)this.ᜆ();
					this.ᜈ = new byte[num2];
					A_0.Read(this.ᜈ, 0, num2);
					num = 0;
					continue;
				}
				}
				IL_24:
				if (this.ᜂ())
				{
					num = 1;
					continue;
				}
				IL_62:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_24;
				}
				break;
			}
			if (false)
			{
			}
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x002117A0 File Offset: 0x002107A0
		public object ᜇ()
		{
			spr\u23E7.ᜀ ᜀ;
			for (;;)
			{
				ᜀ = (spr\u23E7.ᜀ)base.MemberwiseClone();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						ᜀ.ᜈ = spr\u1CD3.ᜀ(this.ᜈ);
						if (true)
						{
						}
						num = 2;
						continue;
					case 1:
						if (this.ᜈ == null)
						{
							return ᜀ;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return ᜀ;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
						return ᜀ;
					}
					break;
				}
			}
			return ᜀ;
		}

		// Token: 0x040019A8 RID: 6568
		private const ushort ᜀ = 16383;

		// Token: 0x040019A9 RID: 6569
		private const ushort ᜁ = 16384;

		// Token: 0x040019AA RID: 6570
		private const ushort ᜂ = 32768;

		// Token: 0x040019AB RID: 6571
		private const int ᜃ = 6;

		// Token: 0x040019AC RID: 6572
		private ushort ᜄ;

		// Token: 0x040019AD RID: 6573
		private bool ᜅ;

		// Token: 0x040019AE RID: 6574
		private bool ᜆ;

		// Token: 0x040019AF RID: 6575
		private uint ᜇ;

		// Token: 0x040019B0 RID: 6576
		private byte[] ᜈ;
	}
}
