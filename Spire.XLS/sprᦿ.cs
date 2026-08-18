using System;
using System.IO;
using System.Runtime.InteropServices;
using Spire.CompoundFile.XLS.Native;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000549 RID: 1353
internal class sprᦿ : spr\u2496
{
	// Token: 0x06005229 RID: 21033 RVA: 0x00332E64 File Offset: 0x00331E64
	public sprᦿ()
	{
		this.ᜀ();
	}

	// Token: 0x0600522A RID: 21034 RVA: 0x00332E80 File Offset: 0x00331E80
	public sprᦿ(Stream A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x0600522B RID: 21035 RVA: 0x00332E9C File Offset: 0x00331E9C
	public sprᦿ(string A_0, STGM A_1)
	{
		int a_ = 0;
		base..ctor();
		if (A_0 != null)
		{
			while (A_0.Length != 0)
			{
				if ((A_1 & STGM.STGM_CREATE) == STGM.STGM_READ)
				{
					using (FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						this.ᜀ(fileStream);
						return;
					}
					continue;
				}
				Guid guid = new Guid(RecordTableEnumerator.b("ص࠷ਹ఻฽瀿牁♃歅硇穉籋繍絏扑摓晕桗睙Ὓ湝偟剡䥣噥塧婩屫幭䁯䉱䑳䙵䡷乹䩻", a_));
				spr\u1ADF a_2;
				int num = spr\u2019.StgCreateStorageEx(A_0, A_1, STGFMT.STGFMT_DOCFILE, 0, IntPtr.Zero, IntPtr.Zero, ref guid, out a_2);
				if (num != -2147287007)
				{
					if (num != -2147287008)
					{
						if (num != 0)
						{
							throw new ExternalException(RecordTableEnumerator.b("电夷吹刻儽㐿扁⭃㙅ⵇ⑉汋㵍⑏㵑♓㝕㽗㽙牛繝♟ୡࡣͥ䡧⑩൫ͭᕯ剱ᵳյ䉷婹", a_) + A_0, num);
						}
						this.ᜀ = new spr\u24E8(a_2);
						return;
					}
				}
				throw new spr\u2551();
			}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倵儷嘹夻瀽ℿ⽁⅃", a_));
	}

	// Token: 0x0600522C RID: 21036 RVA: 0x00332FA8 File Offset: 0x00331FA8
	public void ᜁ()
	{
		for (;;)
		{
			this.ᜀ.ᜃ();
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (this.ᜁ != null)
					{
						goto IL_41;
					}
					return;
				case 2:
					this.ᜁ.ᜀ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_41;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
				IL_41:
				num = 2;
			}
		}
	}

	// Token: 0x0600522D RID: 21037 RVA: 0x00333034 File Offset: 0x00332034
	private void ᜀ()
	{
		int a_ = 13;
		int num;
		spr\u1ADF a_2;
		for (;;)
		{
			num = spr\u2019.CreateILockBytesOnHGlobal(IntPtr.Zero, true, out this.ᜁ);
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num == 0)
					{
						num = spr\u2019.StgCreateDocfileOnILockBytes(this.ᜁ, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE, 0, out a_2);
						num2 = 3;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AA;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_74;
				case 2:
					goto IL_B8;
				case 3:
					goto IL_AA;
				}
				break;
				IL_AA:
				if (num == 0)
				{
					goto IL_CF;
				}
				num2 = 2;
			}
		}
		IL_74:
		throw new ExternalException(RecordTableEnumerator.b("B⑄⥆湈㽊浌ⱎ⍐㙒㑔⍖㱘筚ᅜぞɠࡢ❤Ṧᵨ๪Ṭ䅮", a_), num);
		IL_B8:
		throw new ExternalException(RecordTableEnumerator.b("B⑄⥆湈㽊浌ⱎ⍐㙒㑔⍖㱘筚⹜⭞๠ᅢѤf౨䭪ɬŮ兰㩲㥴ᡶ᩸ၺ㽼پꦆ", a_), num);
		IL_CF:
		this.ᜀ = new spr\u24E8(a_2);
	}

	// Token: 0x0600522E RID: 21038 RVA: 0x0033311C File Offset: 0x0033211C
	public void ᜁ(Stream A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 1;
			int num4;
			for (;;)
			{
				byte[] array;
				long num2;
				uint num3;
				switch (num)
				{
				case 0:
					if (this.ᜁ == null)
					{
						num = 3;
						continue;
					}
					array = new byte[32768];
					num2 = 0L;
					num = 4;
					continue;
				case 2:
					goto IL_74;
				case 3:
					goto IL_CD;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_74;
					}
					break;
				case 5:
					goto IL_AB;
				case 6:
					if (num3 < 32768U)
					{
						num = 8;
						continue;
					}
					num2 += 32768L;
					num = 2;
					continue;
				case 7:
					goto IL_5D;
				case 8:
					return;
				case 9:
					if (num4 != 0)
					{
						num = 5;
						continue;
					}
					A_0.Write(array, 0, (int)num3);
					num = 6;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 0;
				continue;
				IL_74:
				num4 = this.ᜁ.ᜀ((ulong)num2, array, 32768U, out num3);
				num = 9;
			}
			IL_5D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀ㅂ⁄♆⑈", a_));
			IL_AB:
			throw new ExternalException(RecordTableEnumerator.b("樾⽀≂❄⭆ⱈ歊㥌⁎煐⅒ご㙖㵘筚㽜♞ᕠ٢ᙤ䝦ཨᥪɬɮ兰㩲㥴ᡶ᩸ၺ㽼پ", a_), num4);
			IL_CD:
			throw new ArgumentNullException(RecordTableEnumerator.b("刾Ṁ⽂⩄⑆≈ॊ㑌㭎㑐⁒", a_));
		}
		}
	}

	// Token: 0x0600522F RID: 21039 RVA: 0x003332AC File Offset: 0x003322AC
	private void ᜀ(Stream A_0)
	{
		int a_ = 9;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0C;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			spr\u2019.CreateILockBytesOnHGlobal(IntPtr.Zero, true, out this.ᜁ);
			int num = (int)(A_0.Length - A_0.Position);
			byte[] array = new byte[num];
			A_0.Read(array, 0, num);
			uint num2;
			this.ᜁ.ᜁ(0UL, array, (uint)array.Length, out num2);
			this.ᜁ.ᜀ();
			spr\u1ADF a_2;
			spr\u2019.StgOpenStorageOnILockBytes(this.ᜁ, null, STGM.STGM_SHARE_DENY_NONE | STGM.STGM_DIRECT_SWMR, 0, 0, out a_2);
			this.ᜀ = new spr\u24E8(a_2);
			return;
		}
		IL_0C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀ㅂ⁄♆⑈", a_));
	}

	// Token: 0x06005230 RID: 21040 RVA: 0x00333380 File Offset: 0x00332380
	public spr\u20C3 ᜃ()
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

	// Token: 0x06005231 RID: 21041 RVA: 0x003333C4 File Offset: 0x003323C4
	public void ᜂ(Stream A_0)
	{
		int a_ = 4;
		this.ᜁ();
		if (this.ᜁ == null)
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
				throw new Exception(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㽏⁑瑓㥕⡗㽙⹛㽝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵ᅷ᝹౻ችﲇꂍ", a_));
			}
		}
		if (true)
		{
		}
		this.ᜁ(A_0);
	}

	// Token: 0x06005232 RID: 21042 RVA: 0x00333434 File Offset: 0x00332434
	public void ᜀ(string A_0)
	{
		FileStream fileStream = new FileStream(A_0, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
		try
		{
			if (true)
			{
			}
			this.ᜂ(fileStream);
		}
		finally
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					((IDisposable)fileStream).Dispose();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_70;
				}
				IL_39:
				if (fileStream != null)
				{
					num = 1;
					continue;
				}
				break;
				goto IL_39;
			}
			IL_70:;
		}
	}

	// Token: 0x06005233 RID: 21043 RVA: 0x003334D0 File Offset: 0x003324D0
	public void ᜂ()
	{
		int num = 3;
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
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					Marshal.ReleaseComObject(this.ᜁ);
					GC.SuppressFinalize(this.ᜁ);
					this.ᜁ = null;
					goto IL_96;
				case 1:
					this.ᜀ.ᜂ();
					this.ᜀ = null;
					num = 4;
					continue;
				case 2:
					goto IL_68;
				case 4:
					if (this.ᜁ != null)
					{
						num = 0;
						continue;
					}
					goto IL_68;
				case 5:
					return;
				}
				if (this.ᜀ != null)
				{
					num = 1;
					continue;
				}
				return;
				IL_68:
				GC.SuppressFinalize(this);
				num = 5;
				continue;
			}
			IL_96:
			num = 2;
		}
	}

	// Token: 0x040024A3 RID: 9379
	private spr\u24E8 ᜀ;

	// Token: 0x040024A4 RID: 9380
	private sprᥖ ᜁ;
}
