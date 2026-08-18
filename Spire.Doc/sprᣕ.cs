using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;

// Token: 0x020002DF RID: 735
internal class sprᣕ : spr\u2562
{
	// Token: 0x0600287A RID: 10362 RVA: 0x002855F8 File Offset: 0x002845F8
	internal override int ᜀ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_52:
			num = 0;
			break;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		for (;;)
		{
			IL_38:
			switch (num)
			{
			case 0:
				this.ᜆ = 20;
				num = 2;
				continue;
			case 1:
				goto IL_2E;
			case 2:
				goto IL_6C;
			}
			goto IL_4A;
		}
		IL_2E:
		if (true)
		{
		}
		IL_4A:
		if (this.ᜆ == 0)
		{
			goto IL_52;
		}
		IL_6C:
		return this.ᜆ;
		IL_20:
		if (false)
		{
		}
		num = 1;
		goto IL_38;
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x0028567C File Offset: 0x0028467C
	internal sprᣕ(spr\u2578 A_0)
	{
		byte[] array = new byte[A_0.Length];
		A_0.Read(array, 0, array.Length);
		this.ᜁ(array, 0);
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x002856BC File Offset: 0x002846BC
	internal sprᣕ(OleLinkType A_0, string A_1)
	{
		this.\u1718 = A_0;
		this.ᜇ = 33554433;
		this.ᜊ = 0;
		this.ᜋ = 0;
		if (A_0 == OleLinkType.Embed)
		{
			this.ᜈ = 8;
			return;
		}
		this.ᜈ = 1;
		this.\u1719 = A_1;
		this.ᜉ = 3;
	}

	// Token: 0x0600287D RID: 10365 RVA: 0x0028571C File Offset: 0x0028471C
	internal override void ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜆ = A_0.Length;
				this.ᜇ = spr\u2562.ᜃ(A_0, ref A_1);
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (this.ᜑ == -1)
						{
							num = 10;
							continue;
						}
						byte[] a_2 = spr\u2562.ᜀ(A_0, 16, ref A_1);
						this.\u1712 = new sprᣕ.ᜀ();
						this.\u1712.ᜁ(a_2, 0);
						this.\u1713 = spr\u2562.ᜃ(A_0, ref A_1);
						this.\u1714 = spr\u2562.ᜃ(A_0, ref A_1);
						this.\u1715 = spr\u2562.ᜃ(A_0, ref A_1);
						this.\u1716 = spr\u2562.ᜃ(A_0, ref A_1);
						this.\u1717 = spr\u2562.ᜃ(A_0, ref A_1);
						num = 15;
						continue;
					}
					case 1:
						if (this.ᜈ != 8)
						{
							num = 4;
							continue;
						}
						return;
					case 2:
						if (this.\u170D != 0)
						{
							num = 9;
							continue;
						}
						goto IL_2B3;
					case 3:
						goto IL_AA;
					case 4:
						this.ᜋ = spr\u2562.ᜃ(A_0, ref A_1);
						num = 14;
						continue;
					case 5:
					{
						byte[] a_3 = spr\u2562.ᜀ(A_0, this.ᜋ, ref A_1);
						this.ᜌ = new sprᣕ.ᜁ(this.\u1719);
						this.ᜌ.ᜁ(a_3, 0);
						num = 16;
						continue;
					}
					case 6:
						num = 1;
						continue;
					case 7:
						if (this.ᜊ != 0)
						{
							num = 8;
							continue;
						}
						num = 13;
						continue;
					case 8:
						goto IL_1A8;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_372;
						default:
						{
							if (false)
							{
							}
							byte[] a_4 = spr\u2562.ᜀ(A_0, this.\u170D, ref A_1);
							this.ᜎ = new sprᣕ.ᜁ(this.\u1719);
							this.ᜎ.ᜁ(a_4, 0);
							num = 11;
							continue;
						}
						}
						break;
					case 10:
						goto IL_320;
					case 11:
						goto IL_2B3;
					case 12:
						if (this.ᜇ != 33554433)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						this.ᜈ = spr\u2562.ᜃ(A_0, ref A_1);
						this.ᜉ = spr\u2562.ᜃ(A_0, ref A_1);
						this.ᜊ = spr\u2562.ᜃ(A_0, ref A_1);
						num = 7;
						continue;
					case 13:
						if (this.ᜈ != 0)
						{
							goto IL_372;
						}
						return;
					case 14:
						if (this.ᜋ != 0)
						{
							num = 5;
							continue;
						}
						goto IL_1AA;
					case 15:
						goto IL_271;
					case 16:
						goto IL_1AA;
					}
					break;
					IL_1AA:
					this.\u170D = spr\u2562.ᜃ(A_0, ref A_1);
					num = 2;
					continue;
					IL_2B3:
					this.ᜏ = spr\u2562.ᜃ(A_0, ref A_1);
					byte[] a_5 = spr\u2562.ᜀ(A_0, this.ᜏ, ref A_1);
					this.ᜐ = new sprᣕ.ᜁ(this.\u1719);
					this.ᜐ.ᜁ(a_5, 0);
					this.ᜑ = spr\u2562.ᜃ(A_0, ref A_1);
					num = 0;
					continue;
					IL_372:
					num = 6;
				}
			}
			IL_AA:
			throw new InvalidDataException(ClipboardData.b("㝷㙹㥻幽겋ﺏ늑望秊몙ﾝ첟쮡삣", a_));
			IL_1A8:
			throw new InvalidDataException(ClipboardData.b("㝷㙹㥻幽겋ﺏ늑望秊몙ﾝ첟쮡삣", a_));
			IL_271:
			return;
			IL_320:
			throw new InvalidDataException(ClipboardData.b("㝷㙹㥻幽겋ﺏ늑望秊몙ﾝ첟쮡삣", a_));
		}
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x00285AAC File Offset: 0x00284AAC
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		for (;;)
		{
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜇ);
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜈ);
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜉ);
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜊ);
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜎ.ᜀ(A_0, A_1);
					A_1 += this.ᜎ.ᜀ();
					num = 2;
					continue;
				case 1:
					if (this.ᜋ != 0)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					goto IL_7D;
				case 2:
					goto IL_10E;
				case 3:
					goto IL_7B;
				case 4:
					if (this.\u170D != 0)
					{
						num = 0;
						continue;
					}
					goto IL_14C;
				case 5:
					this.ᜌ.ᜀ(A_0, A_1);
					A_1 += this.ᜌ.ᜀ();
					num = 7;
					continue;
				case 6:
					if (this.ᜈ == 0)
					{
						num = 3;
						continue;
					}
					spr\u2562.ᜀ(A_0, ref A_1, this.ᜋ);
					num = 1;
					continue;
				case 7:
					goto IL_7D;
				}
				break;
				IL_7D:
				spr\u2562.ᜀ(A_0, ref A_1, this.\u170D);
				num = 4;
			}
		}
		IL_7B:
		IL_E2:
		return A_0.Length;
		IL_10E:
		IL_14C:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_E2;
		default:
			if (false)
			{
			}
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜏ);
			this.ᜐ.ᜀ(A_0, A_1);
			A_1 += this.ᜐ.ᜀ();
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜑ);
			this.\u1712.ᜀ(A_0, A_1);
			A_1 += this.\u1712.ᜀ();
			spr\u2562.ᜀ(A_0, ref A_1, this.\u1713);
			spr\u2562.ᜀ(A_0, ref A_1, this.\u1714);
			spr\u2562.ᜀ(A_0, ref A_1, this.\u1715);
			spr\u2562.ᜀ(A_0, ref A_1, this.\u1716);
			spr\u2562.ᜀ(A_0, ref A_1, this.\u1717);
			return A_0.Length;
		}
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x00285CC0 File Offset: 0x00284CC0
	internal void ᜀ(spr\u2578 A_0)
	{
		for (;;)
		{
			IL_00:
			for (;;)
			{
				IL_30:
				int num = 0;
				byte[] array = new byte[20];
				spr\u2562.ᜀ(array, ref num, this.ᜇ);
				spr\u2562.ᜀ(array, ref num, this.ᜈ);
				spr\u2562.ᜀ(array, ref num, this.ᜉ);
				spr\u2562.ᜀ(array, ref num, this.ᜊ);
				int num2 = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							spr\u2562.ᜀ(array, ref num, this.ᜋ);
							A_0.Write(array, 0, array.Length);
							if (true)
							{
							}
							num2 = 2;
							continue;
						case 1:
							if (this.\u1718 == OleLinkType.Embed)
							{
								num2 = 0;
								continue;
							}
							return;
						case 2:
							return;
						}
						goto IL_30;
					}
				}
			}
		}
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x00285D94 File Offset: 0x00284D94
	internal void ᜀ(sprᤘ A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_AF:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_3F;
			}
			break;
		}
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				return;
			case 1:
			{
				if (true)
				{
				}
				int a_ = 4;
				this.ᜀ(A_0, a_);
				string[] array = this.\u1719.Split(new char[]
				{
					'\\'
				});
				string a_2 = array[array.Length - 1];
				this.ᜎ = new sprᣕ.ᜁ(a_2);
				this.ᜎ.ᜀ(A_0, false);
				this.ᜐ = new sprᣕ.ᜁ(this.\u1719);
				this.ᜐ.ᜀ(A_0, true);
				this.ᜀ(A_0, 16);
				this.ᜀ(A_0, a_);
				this.ᜀ(A_0, a_);
				this.ᜀ(A_0, a_);
				this.ᜀ(A_0, a_);
				this.ᜀ(A_0, a_);
				num = 0;
				continue;
			}
			case 2:
				goto IL_A3;
			}
			goto IL_3F;
		}
		IL_A3:
		if (this.\u1718 == OleLinkType.Link)
		{
			goto IL_AF;
		}
		return;
		IL_3F:
		int num2 = 0;
		byte[] array2 = new byte[20];
		spr\u2562.ᜀ(array2, ref num2, this.ᜇ);
		spr\u2562.ᜀ(array2, ref num2, this.ᜈ);
		spr\u2562.ᜀ(array2, ref num2, this.ᜉ);
		spr\u2562.ᜀ(array2, ref num2, this.ᜊ);
		spr\u2562.ᜀ(array2, ref num2, this.ᜋ);
		A_0.Write(array2, 0, array2.Length);
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x00285F18 File Offset: 0x00284F18
	private void ᜀ(sprᤘ A_0, int A_1)
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

	// Token: 0x06002882 RID: 10370 RVA: 0x00285F64 File Offset: 0x00284F64
	private void ᜀ(sprᤘ A_0, string A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_47:
				byte[] array = new byte[4];
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				int num = 0;
				byte[] bytes = asciiencoding.GetBytes(A_1);
				spr\u2562.ᜀ(array, ref num, bytes.Length);
				A_0.Write(array, 0, array.Length);
				for (;;)
				{
					IL_74:
					int num2 = 0;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_74;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							switch (num2)
							{
							case 0:
								if (bytes.Length > 0)
								{
									num2 = 2;
									continue;
								}
								return;
							case 1:
								return;
							case 2:
								A_0.Write(bytes, 0, bytes.Length);
								num2 = 1;
								continue;
							}
							goto IL_47;
						}
					}
				}
			}
			return;
		}
	}

	// Token: 0x0400233C RID: 9020
	private new const int ᜀ = 33554433;

	// Token: 0x0400233D RID: 9021
	private new const int ᜁ = 0;

	// Token: 0x0400233E RID: 9022
	private new const int ᜂ = 20;

	// Token: 0x0400233F RID: 9023
	private new const int ᜃ = -1;

	// Token: 0x04002340 RID: 9024
	private new const int ᜄ = 8;

	// Token: 0x04002341 RID: 9025
	private const int ᜅ = 1;

	// Token: 0x04002342 RID: 9026
	private int ᜆ;

	// Token: 0x04002343 RID: 9027
	private int ᜇ;

	// Token: 0x04002344 RID: 9028
	private int ᜈ;

	// Token: 0x04002345 RID: 9029
	private int ᜉ;

	// Token: 0x04002346 RID: 9030
	private int ᜊ;

	// Token: 0x04002347 RID: 9031
	private int ᜋ;

	// Token: 0x04002348 RID: 9032
	private sprᣕ.ᜁ ᜌ;

	// Token: 0x04002349 RID: 9033
	private int \u170D;

	// Token: 0x0400234A RID: 9034
	private sprᣕ.ᜁ ᜎ;

	// Token: 0x0400234B RID: 9035
	private int ᜏ;

	// Token: 0x0400234C RID: 9036
	private sprᣕ.ᜁ ᜐ;

	// Token: 0x0400234D RID: 9037
	private int ᜑ;

	// Token: 0x0400234E RID: 9038
	private sprᣕ.ᜀ \u1712;

	// Token: 0x0400234F RID: 9039
	private int \u1713;

	// Token: 0x04002350 RID: 9040
	private int \u1714;

	// Token: 0x04002351 RID: 9041
	private int \u1715;

	// Token: 0x04002352 RID: 9042
	private int \u1716;

	// Token: 0x04002353 RID: 9043
	private int \u1717;

	// Token: 0x04002354 RID: 9044
	private OleLinkType \u1718;

	// Token: 0x04002355 RID: 9045
	private string \u1719 = string.Empty;

	// Token: 0x020002E0 RID: 736
	private new class ᜁ : spr\u2562
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x00286024 File Offset: 0x00285024
		internal override int ᜀ()
		{
			while (this.ᜄ != null)
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
					return this.ᜄ.Length + 16;
				}
			}
			return 0;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x00286078 File Offset: 0x00285078
		internal ᜁ(string A_0)
		{
			this.ᜅ = A_0;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x00286094 File Offset: 0x00285094
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
			this.ᜃ = new sprᣕ.ᜀ();
			this.ᜃ.ᜁ(A_0, A_1);
			A_1 += this.ᜃ.ᜀ();
			int a_ = A_0.Length - this.ᜃ.ᜀ();
			this.ᜄ = spr\u2562.ᜀ(A_0, a_, ref A_1);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x00286118 File Offset: 0x00285118
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
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜄ);
			return 0;
		}

		// Token: 0x06002887 RID: 10375
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)] string A_0, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder A_1, int A_2);

		// Token: 0x06002888 RID: 10376 RVA: 0x00286180 File Offset: 0x00285180
		internal void ᜀ(sprᤘ A_0, bool A_1)
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
					IL_27:
					this.ᜃ = new sprᣕ.ᜀ();
					array = new byte[16];
					this.ᜃ.ᜀ(array, 0);
					asciiencoding = new ASCIIEncoding();
					unicodeEncoding = new UnicodeEncoding();
					s = string.Empty;
					s2 = string.Empty;
					for (;;)
					{
						IL_61:
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								StringBuilder stringBuilder = new StringBuilder(255);
								sprᣕ.ᜁ.GetShortPathName(this.ᜅ, stringBuilder, stringBuilder.Capacity);
								string text = stringBuilder.ToString();
								s = text;
								s2 = text;
								if (true)
								{
								}
								num = 2;
								continue;
							}
							case 1:
								goto IL_B2;
							case 2:
								goto IL_F9;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_61;
								default:
									if (false)
									{
									}
									if (A_1)
									{
										num = 0;
										continue;
									}
									s = this.ᜅ;
									s2 = this.ᜅ;
									num = 1;
									continue;
								}
								break;
							}
							goto IL_27;
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

		// Token: 0x04002356 RID: 9046
		private new const int ᜀ = 0;

		// Token: 0x04002357 RID: 9047
		private new const int ᜁ = -559022081;

		// Token: 0x04002358 RID: 9048
		private new const int ᜂ = 4;

		// Token: 0x04002359 RID: 9049
		internal new sprᣕ.ᜀ ᜃ;

		// Token: 0x0400235A RID: 9050
		internal new byte[] ᜄ;

		// Token: 0x0400235B RID: 9051
		internal string ᜅ;
	}

	// Token: 0x020002E1 RID: 737
	private new class ᜀ : spr\u2562
	{
		// Token: 0x06002889 RID: 10377 RVA: 0x0028630C File Offset: 0x0028530C
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

		// Token: 0x0600288A RID: 10378 RVA: 0x0028634C File Offset: 0x0028534C
		internal ᜀ()
		{
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00286360 File Offset: 0x00285360
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
			this.ᜁ = spr\u2562.ᜃ(A_0, ref A_1);
			this.ᜂ = spr\u2562.ᜄ(A_0, ref A_1);
			this.ᜃ = spr\u2562.ᜄ(A_0, ref A_1);
			this.ᜄ = spr\u2562.ᜂ(A_0, ref A_1);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x002863D4 File Offset: 0x002853D4
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
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜁ);
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜂ);
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜃ);
			spr\u2562.ᜀ(A_0, ref A_1, this.ᜄ);
			return 16;
		}

		// Token: 0x0400235C RID: 9052
		internal new const int ᜀ = 16;

		// Token: 0x0400235D RID: 9053
		internal new int ᜁ;

		// Token: 0x0400235E RID: 9054
		internal new short ᜂ;

		// Token: 0x0400235F RID: 9055
		internal new short ᜃ;

		// Token: 0x04002360 RID: 9056
		internal new long ᜄ;
	}
}
