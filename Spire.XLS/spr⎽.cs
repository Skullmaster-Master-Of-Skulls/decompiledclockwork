using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000563 RID: 1379
internal class spr\u23BD : spr\u20AE
{
	// Token: 0x0600531B RID: 21275 RVA: 0x0033DD80 File Offset: 0x0033CD80
	internal override int ᜀ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜆ = 20;
				num = 2;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6C;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_6C;
			}
			if (this.ᜆ != 0)
			{
				break;
			}
			num = 0;
		}
		IL_6C:
		return this.ᜆ;
	}

	// Token: 0x0600531C RID: 21276 RVA: 0x0033DE04 File Offset: 0x0033CE04
	internal spr\u23BD(spr\u1FDC A_0)
	{
		byte[] array = new byte[A_0.Length];
		A_0.Read(array, 0, array.Length);
		this.ᜁ(array, 0);
	}

	// Token: 0x0600531D RID: 21277 RVA: 0x0033DE44 File Offset: 0x0033CE44
	internal spr\u23BD(string A_0)
	{
		this.ᜇ = 33554433;
		this.ᜊ = 0;
		this.ᜋ = 0;
		this.\u1718 = A_0;
		this.ᜉ = 3;
	}

	// Token: 0x0600531E RID: 21278 RVA: 0x0033DE8C File Offset: 0x0033CE8C
	internal override void ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜆ = A_0.Length;
				this.ᜇ = spr\u20AE.ᜃ(A_0, ref A_1);
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (this.ᜑ == -1)
						{
							num = 3;
							continue;
						}
						byte[] a_2 = spr\u20AE.ᜀ(A_0, 16, ref A_1);
						this.\u1712 = new spr\u23BD.ᜁ();
						this.\u1712.ᜁ(a_2, 0);
						this.\u1713 = spr\u20AE.ᜃ(A_0, ref A_1);
						this.\u1714 = spr\u20AE.ᜃ(A_0, ref A_1);
						this.\u1715 = spr\u20AE.ᜃ(A_0, ref A_1);
						this.\u1716 = spr\u20AE.ᜃ(A_0, ref A_1);
						this.\u1717 = spr\u20AE.ᜃ(A_0, ref A_1);
						num = 6;
						continue;
					}
					case 1:
					{
						byte[] a_3 = spr\u20AE.ᜀ(A_0, this.ᜋ, ref A_1);
						this.ᜌ = new spr\u23BD.ᜀ(this.\u1718);
						this.ᜌ.ᜁ(a_3, 0);
						num = 15;
						continue;
					}
					case 2:
						goto IL_BC;
					case 3:
						goto IL_31D;
					case 4:
						if (this.\u170D != 0)
						{
							num = 12;
							continue;
						}
						goto IL_2A6;
					case 5:
						if (this.ᜊ != 0)
						{
							num = 7;
							continue;
						}
						num = 14;
						continue;
					case 6:
						goto IL_264;
					case 7:
						goto IL_1B7;
					case 8:
						this.ᜋ = spr\u20AE.ᜃ(A_0, ref A_1);
						num = 11;
						continue;
					case 9:
						if (this.ᜈ != 8)
						{
							num = 8;
							continue;
						}
						return;
					case 10:
						num = 9;
						continue;
					case 11:
						if (this.ᜋ != 0)
						{
							num = 1;
							continue;
						}
						goto IL_1B9;
					case 12:
					{
						byte[] a_4 = spr\u20AE.ᜀ(A_0, this.\u170D, ref A_1);
						this.ᜎ = new spr\u23BD.ᜀ(this.\u1718);
						this.ᜎ.ᜁ(a_4, 0);
						num = 16;
						continue;
					}
					case 13:
						if (this.ᜇ == 33554433)
						{
							this.ᜈ = spr\u20AE.ᜃ(A_0, ref A_1);
							this.ᜉ = spr\u20AE.ᜃ(A_0, ref A_1);
							this.ᜊ = spr\u20AE.ᜃ(A_0, ref A_1);
							num = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36F;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 14:
						if (this.ᜈ != 0)
						{
							goto IL_36F;
						}
						return;
					case 15:
						if (true)
						{
						}
						goto IL_1B9;
					case 16:
						goto IL_2A6;
					}
					break;
					IL_1B9:
					this.\u170D = spr\u20AE.ᜃ(A_0, ref A_1);
					num = 4;
					continue;
					IL_2A6:
					this.ᜏ = spr\u20AE.ᜃ(A_0, ref A_1);
					byte[] a_5 = spr\u20AE.ᜀ(A_0, this.ᜏ, ref A_1);
					this.ᜐ = new spr\u23BD.ᜀ(this.\u1718);
					this.ᜐ.ᜁ(a_5, 0);
					this.ᜑ = spr\u20AE.ᜃ(A_0, ref A_1);
					num = 0;
					continue;
					IL_36F:
					num = 10;
				}
			}
			IL_BC:
			throw new Exception(RecordTableEnumerator.b("琺焼稾慀あㅄ㕆ⱈ⩊⁌潎㡐㵒畔㥖㙘⽚絜⥞`ར౤ͦ", a_));
			IL_1B7:
			throw new InvalidDataException(RecordTableEnumerator.b("琺焼稾慀あㅄ㕆ⱈ⩊⁌潎㡐㵒畔㥖㙘⽚絜⥞`ར౤ͦ", a_));
			IL_264:
			return;
			IL_31D:
			throw new InvalidDataException(RecordTableEnumerator.b("琺焼稾慀あㅄ㕆ⱈ⩊⁌潎㡐㵒畔㥖㙘⽚絜⥞`ར౤ͦ", a_));
		}
	}

	// Token: 0x0600531F RID: 21279 RVA: 0x0033E21C File Offset: 0x0033D21C
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		for (;;)
		{
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜇ);
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜈ);
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜉ);
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜊ);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜎ.ᜀ(A_0, A_1);
					A_1 += this.ᜎ.ᜀ();
					goto IL_125;
				case 1:
					goto IL_130;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_125;
					default:
						if (false)
						{
						}
						goto IL_80;
					}
					break;
				case 3:
					if (this.ᜈ == 0)
					{
						num = 7;
						continue;
					}
					if (true)
					{
					}
					spr\u20AE.ᜀ(A_0, ref A_1, this.ᜋ);
					num = 4;
					continue;
				case 4:
					if (this.ᜋ != 0)
					{
						num = 5;
						continue;
					}
					goto IL_80;
				case 5:
					this.ᜌ.ᜀ(A_0, A_1);
					A_1 += this.ᜌ.ᜀ();
					num = 2;
					continue;
				case 6:
					if (this.\u170D != 0)
					{
						num = 0;
						continue;
					}
					goto IL_16E;
				case 7:
					goto IL_7B;
				}
				break;
				IL_80:
				spr\u20AE.ᜀ(A_0, ref A_1, this.\u170D);
				num = 6;
				continue;
				IL_125:
				num = 1;
			}
		}
		IL_7B:
		return A_0.Length;
		IL_130:
		IL_16E:
		spr\u20AE.ᜀ(A_0, ref A_1, this.ᜏ);
		this.ᜐ.ᜀ(A_0, A_1);
		A_1 += this.ᜐ.ᜀ();
		spr\u20AE.ᜀ(A_0, ref A_1, this.ᜑ);
		this.\u1712.ᜀ(A_0, A_1);
		A_1 += this.\u1712.ᜀ();
		spr\u20AE.ᜀ(A_0, ref A_1, this.\u1713);
		spr\u20AE.ᜀ(A_0, ref A_1, this.\u1714);
		spr\u20AE.ᜀ(A_0, ref A_1, this.\u1715);
		spr\u20AE.ᜀ(A_0, ref A_1, this.\u1716);
		spr\u20AE.ᜀ(A_0, ref A_1, this.\u1717);
		return A_0.Length;
	}

	// Token: 0x06005320 RID: 21280 RVA: 0x0033E438 File Offset: 0x0033D438
	internal new void ᜀ(spr\u1FDC A_0)
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
		int num = 0;
		byte[] array = new byte[20];
		spr\u20AE.ᜀ(array, ref num, this.ᜇ);
		spr\u20AE.ᜀ(array, ref num, this.ᜈ);
		spr\u20AE.ᜀ(array, ref num, this.ᜉ);
		spr\u20AE.ᜀ(array, ref num, this.ᜊ);
		spr\u20AE.ᜀ(array, ref num, this.ᜋ);
		A_0.Write(array, 0, array.Length);
	}

	// Token: 0x06005321 RID: 21281 RVA: 0x0033E4D0 File Offset: 0x0033D4D0
	internal new void ᜀ(spr\u2399 A_0)
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
		int num = 0;
		byte[] array = new byte[20];
		spr\u20AE.ᜀ(array, ref num, this.ᜇ);
		spr\u20AE.ᜀ(array, ref num, this.ᜈ);
		spr\u20AE.ᜀ(array, ref num, this.ᜉ);
		spr\u20AE.ᜀ(array, ref num, this.ᜊ);
		spr\u20AE.ᜀ(array, ref num, this.ᜋ);
		A_0.Write(array, 0, array.Length);
	}

	// Token: 0x06005322 RID: 21282 RVA: 0x0033E568 File Offset: 0x0033D568
	private new void ᜀ(spr\u2399 A_0, int A_1)
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
		byte[] buffer = new byte[A_1];
		A_0.Write(buffer, 0, A_1);
	}

	// Token: 0x06005323 RID: 21283 RVA: 0x0033E5B4 File Offset: 0x0033D5B4
	private new void ᜀ(spr\u2399 A_0, string A_1)
	{
		int num;
		byte[] bytes;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_34:
				switch (num)
				{
				case 0:
					return;
				case 1:
					A_0.Write(bytes, 0, bytes.Length);
					num = 0;
					continue;
				case 2:
					if (bytes.Length > 0)
					{
						num = 1;
						continue;
					}
					return;
				}
				goto IL_47;
			}
			return;
		default:
			if (false)
			{
			}
			num = 0;
			switch (num)
			{
			default:
				if (true)
				{
				}
				break;
			}
			break;
		}
		IL_47:
		byte[] array = new byte[4];
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		int num2 = 0;
		bytes = asciiencoding.GetBytes(A_1);
		spr\u20AE.ᜀ(array, ref num2, bytes.Length);
		A_0.Write(array, 0, array.Length);
		num = 2;
		goto IL_34;
	}

	// Token: 0x040026D7 RID: 9943
	private new const int ᜀ = 33554433;

	// Token: 0x040026D8 RID: 9944
	private new const int ᜁ = 0;

	// Token: 0x040026D9 RID: 9945
	private new const int ᜂ = 20;

	// Token: 0x040026DA RID: 9946
	private new const int ᜃ = -1;

	// Token: 0x040026DB RID: 9947
	private new const int ᜄ = 8;

	// Token: 0x040026DC RID: 9948
	private const int ᜅ = 1;

	// Token: 0x040026DD RID: 9949
	private int ᜆ;

	// Token: 0x040026DE RID: 9950
	private int ᜇ;

	// Token: 0x040026DF RID: 9951
	private int ᜈ;

	// Token: 0x040026E0 RID: 9952
	private int ᜉ;

	// Token: 0x040026E1 RID: 9953
	private int ᜊ;

	// Token: 0x040026E2 RID: 9954
	private int ᜋ;

	// Token: 0x040026E3 RID: 9955
	private spr\u23BD.ᜀ ᜌ;

	// Token: 0x040026E4 RID: 9956
	private int \u170D;

	// Token: 0x040026E5 RID: 9957
	private spr\u23BD.ᜀ ᜎ;

	// Token: 0x040026E6 RID: 9958
	private int ᜏ;

	// Token: 0x040026E7 RID: 9959
	private spr\u23BD.ᜀ ᜐ;

	// Token: 0x040026E8 RID: 9960
	private int ᜑ;

	// Token: 0x040026E9 RID: 9961
	private spr\u23BD.ᜁ \u1712;

	// Token: 0x040026EA RID: 9962
	private int \u1713;

	// Token: 0x040026EB RID: 9963
	private int \u1714;

	// Token: 0x040026EC RID: 9964
	private int \u1715;

	// Token: 0x040026ED RID: 9965
	private int \u1716;

	// Token: 0x040026EE RID: 9966
	private int \u1717;

	// Token: 0x040026EF RID: 9967
	private string \u1718 = string.Empty;

	// Token: 0x02000564 RID: 1380
	private new class ᜀ : spr\u20AE
	{
		// Token: 0x06005324 RID: 21284 RVA: 0x0033E670 File Offset: 0x0033D670
		internal override int ᜀ()
		{
			if (this.ᜄ == null)
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
					return 0;
				}
			}
			if (true)
			{
			}
			return this.ᜄ.Length + 16;
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x0033E6C4 File Offset: 0x0033D6C4
		internal ᜀ(string A_0)
		{
			this.ᜅ = A_0;
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x0033E6E0 File Offset: 0x0033D6E0
		internal override void ᜁ(byte[] A_0, int A_1)
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
			this.ᜃ = new spr\u23BD.ᜁ();
			this.ᜃ.ᜁ(A_0, A_1);
			A_1 += this.ᜃ.ᜀ();
			int a_ = A_0.Length - this.ᜃ.ᜀ();
			this.ᜄ = spr\u20AE.ᜀ(A_0, a_, ref A_1);
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x0033E764 File Offset: 0x0033D764
		internal override int ᜀ(byte[] A_0, int A_1)
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
			this.ᜃ.ᜀ(A_0, A_1);
			A_1 += this.ᜃ.ᜀ();
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜄ);
			return 0;
		}

		// Token: 0x06005328 RID: 21288
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)] string A_0, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder A_1, int A_2);

		// Token: 0x06005329 RID: 21289 RVA: 0x0033E7CC File Offset: 0x0033D7CC
		internal new void ᜀ(spr\u2399 A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				ASCIIEncoding asciiencoding;
				UnicodeEncoding unicodeEncoding;
				string s;
				string s2;
				for (;;)
				{
					this.ᜃ = new spr\u23BD.ᜁ();
					array = new byte[16];
					this.ᜃ.ᜀ(array, 0);
					asciiencoding = new ASCIIEncoding();
					unicodeEncoding = new UnicodeEncoding();
					s = string.Empty;
					s2 = string.Empty;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B2;
					default:
					{
						if (false)
						{
						}
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (A_1)
								{
									num = 3;
									continue;
								}
								s = this.ᜅ;
								s2 = this.ᜅ;
								num = 2;
								continue;
							case 1:
								goto IL_F9;
							case 2:
								goto IL_B2;
							case 3:
							{
								StringBuilder stringBuilder = new StringBuilder(255);
								spr\u23BD.ᜀ.GetShortPathName(this.ᜅ, stringBuilder, stringBuilder.Capacity);
								string text = stringBuilder.ToString();
								s = text;
								s2 = text;
								if (true)
								{
								}
								num = 1;
								continue;
							}
							}
							break;
						}
						break;
					}
					}
				}
				IL_B2:
				IL_F9:
				byte[] bytes = asciiencoding.GetBytes(s);
				byte[] bytes2 = BitConverter.GetBytes(-559022081);
				byte[] bytes3 = unicodeEncoding.GetBytes(s2);
				int value = array.Length + bytes.Length + bytes2.Length + bytes3.Length;
				int count = 4;
				A_0.Write(BitConverter.GetBytes(value), 0, count);
				A_0.Write(array, 0, array.Length);
				A_0.Write(bytes, 0, bytes.Length);
				A_0.Write(bytes2, 0, bytes2.Length);
				A_0.Write(bytes3, 0, bytes3.Length);
				return;
			}
			}
		}

		// Token: 0x040026F0 RID: 9968
		private new const int ᜀ = 0;

		// Token: 0x040026F1 RID: 9969
		private new const int ᜁ = -559022081;

		// Token: 0x040026F2 RID: 9970
		private new const int ᜂ = 4;

		// Token: 0x040026F3 RID: 9971
		internal new spr\u23BD.ᜁ ᜃ;

		// Token: 0x040026F4 RID: 9972
		internal new byte[] ᜄ;

		// Token: 0x040026F5 RID: 9973
		internal string ᜅ;
	}

	// Token: 0x02000565 RID: 1381
	private new class ᜁ : spr\u20AE
	{
		// Token: 0x0600532A RID: 21290 RVA: 0x0033E958 File Offset: 0x0033D958
		internal override int ᜀ()
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
			return 16;
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x0033E998 File Offset: 0x0033D998
		internal ᜁ()
		{
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x0033E9AC File Offset: 0x0033D9AC
		internal override void ᜁ(byte[] A_0, int A_1)
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
			this.ᜁ = spr\u20AE.ᜃ(A_0, ref A_1);
			this.ᜂ = spr\u20AE.ᜄ(A_0, ref A_1);
			this.ᜃ = spr\u20AE.ᜄ(A_0, ref A_1);
			this.ᜄ = spr\u20AE.ᜂ(A_0, ref A_1);
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x0033EA20 File Offset: 0x0033DA20
		internal override int ᜀ(byte[] A_0, int A_1)
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
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜁ);
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜂ);
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜃ);
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜄ);
			return 16;
		}

		// Token: 0x040026F6 RID: 9974
		internal new const int ᜀ = 16;

		// Token: 0x040026F7 RID: 9975
		internal new int ᜁ;

		// Token: 0x040026F8 RID: 9976
		internal new short ᜂ;

		// Token: 0x040026F9 RID: 9977
		internal new short ᜃ;

		// Token: 0x040026FA RID: 9978
		internal new long ᜄ;
	}
}
