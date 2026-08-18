using System;
using System.IO;
using System.Runtime.InteropServices;
using Spire.CompoundFile.Doc;
using Spire.CompoundFile.Doc.Native;

// Token: 0x0200016E RID: 366
internal class sprỻ : spr\u21F4
{
	// Token: 0x06000C97 RID: 3223 RVA: 0x000D2410 File Offset: 0x000D1410
	public spr\u222F ᜂ()
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

	// Token: 0x06000C98 RID: 3224 RVA: 0x000D2454 File Offset: 0x000D1454
	public sprỻ()
	{
		this.ᜀ();
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x000D2470 File Offset: 0x000D1470
	public sprỻ(Stream A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x000D248C File Offset: 0x000D148C
	public sprỻ(string A_0, STGM A_1)
	{
		int a_ = 14;
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
				Guid guid = new Guid(ClipboardData.b("䑳䙵䡷䩹䱻乽끿ꦃ뚅뢇몉벋ꎍꂏꊑ꒓ꚕ떗\ud999겛꺝邟辡钣隥颧骩鲫麭肯花蒳蚵買貹", a_));
				spr\u1CE7 a_2;
				int num = spr\u2443.StgCreateStorageEx(A_0, A_1, STGFMT.STGFMT_DOCFILE, 0, IntPtr.Zero, IntPtr.Zero, ref guid, out a_2);
				if (num != -2147287007)
				{
					if (num != -2147287008)
					{
						if (num != 0)
						{
							throw new ExternalException(ClipboardData.b("㝳᝵ᙷᑹ፻੽ꁿꪉﾋ揄ﾏﶗ뒙벛\ud89d즟캡솣蚥쮩솫쮭邯\udbb1잳貵颷", a_) + A_0, num);
						}
						this.ᜀ = new spr\u1DDA(a_2);
						return;
					}
				}
				throw new spr\u1D2E();
			}
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ታήᑷό㉻ώ", a_));
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x000D2598 File Offset: 0x000D1598
	public void ᜄ()
	{
		for (;;)
		{
			this.ᜀ.ᜃ();
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_61;
						}
					}
					IL_61:
					if (false)
					{
					}
					this.ᜁ.ᜀ();
					num = 2;
					continue;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x000D2624 File Offset: 0x000D1624
	private void ᜀ()
	{
		int a_ = 2;
		int num;
		spr\u1CE7 a_2;
		for (;;)
		{
			num = spr\u2443.CreateILockBytesOnHGlobal(IntPtr.Zero, true, out this.ᜁ);
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_62;
				case 1:
					goto IL_AE;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C5;
					default:
						if (false)
						{
						}
						if (num != 0)
						{
							num2 = 0;
							continue;
						}
						num = spr\u2443.StgCreateDocfileOnILockBytes(this.ᜁ, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE, 0, out a_2);
						num2 = 3;
						continue;
					}
					break;
				case 3:
					if (num != 0)
					{
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					goto IL_C5;
				}
				break;
			}
		}
		IL_62:
		throw new ExternalException(ClipboardData.b("⭧୩ɫ䥭ѯ剱ᝳѵᵷ᭹ࡻ᭽ꁿ캁좉揄몓", a_), num);
		IL_AE:
		throw new ExternalException(ClipboardData.b("⭧୩ɫ䥭ѯ剱ᝳѵᵷ᭹ࡻ᭽ꁿ慎낏﶑望뚕톗횙ﶝ쮟\udda3튥춧\ud9a9芫", a_), num);
		IL_C5:
		this.ᜀ = new spr\u1DDA(a_2);
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x000D270C File Offset: 0x000D170C
	public void ᜁ(Stream A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 4;
			int num4;
			for (;;)
			{
				uint num2;
				long num3;
				byte[] array;
				switch (num)
				{
				case 0:
					goto IL_9E;
				case 1:
					if (num2 < 32768U)
					{
						num = 2;
						continue;
					}
					num3 += 32768L;
					num = 8;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					}
					goto Block_5;
				case 3:
					goto IL_74;
				case 5:
					goto IL_C0;
				case 6:
					goto IL_5D;
				case 7:
					if (this.ᜁ == null)
					{
						num = 5;
						continue;
					}
					array = new byte[32768];
					num3 = 0L;
					if (true)
					{
					}
					num = 3;
					continue;
				case 8:
					goto IL_74;
				case 9:
					if (num4 != 0)
					{
						num = 0;
						continue;
					}
					A_0.Write(array, 0, (int)num2);
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
				IL_89:
				num = 9;
				continue;
				IL_74:
				num4 = this.ᜁ.ᜀ((ulong)num3, array, 32768U, out num2);
				goto IL_89;
			}
			IL_5D:
			throw new ArgumentNullException(ClipboardData.b("ٴͶ୸Ṻᱼቾ", a_));
			IL_9E:
			throw new ExternalException(ClipboardData.b("⁴᥶ᡸ᥺ᅼ᩾ꆀꞆﮈ놐ﲘ붜咽펠첢좤螦슬첮\udab0체쎶\udcb8좺", a_), num4);
			IL_C0:
			throw new ArgumentNullException(ClipboardData.b("ᡴ⡶ᕸᑺṼᑾ쎀廒愈", a_));
			Block_5:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x000D2898 File Offset: 0x000D1898
	private void ᜀ(Stream A_0)
	{
		int a_ = 0;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0E;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			spr\u2443.CreateILockBytesOnHGlobal(IntPtr.Zero, true, out this.ᜁ);
			int num = (int)(A_0.Length - A_0.Position);
			byte[] array = new byte[num];
			A_0.Read(array, 0, num);
			uint num2;
			this.ᜁ.ᜁ(0UL, array, (uint)array.Length, out num2);
			this.ᜁ.ᜀ();
			spr\u1CE7 a_2;
			spr\u2443.StgOpenStorageOnILockBytes(this.ᜁ, null, STGM.STGM_SHARE_DENY_NONE | STGM.STGM_DIRECT_SWMR, 0, 0, out a_2);
			this.ᜀ = new spr\u1DDA(a_2);
			return;
		}
		IL_0E:
		throw new ArgumentNullException(ClipboardData.b("ᕥᱧᡩ५཭ᵯ", a_));
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x000D296C File Offset: 0x000D196C
	public spr\u2547 ᜁ()
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

	// Token: 0x06000CA0 RID: 3232 RVA: 0x000D29B0 File Offset: 0x000D19B0
	public void ᜂ(Stream A_0)
	{
		int a_ = 2;
		this.ᜄ();
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
				throw new Exception(ClipboardData.b("㱧ɩ५乭ᵯ᝱sṵ᝷ṹ屻ᅽꊁ揄憐﶑望뚕벛쾟횡蒣쾥얧\udaa9삫쮭\uddafힱ\udab3습\uddb7\udeb9銻", a_));
			}
		}
		if (true)
		{
		}
		this.ᜁ(A_0);
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x000D2A20 File Offset: 0x000D1A20
	public void ᜀ(string A_0)
	{
		FileStream fileStream = new FileStream(A_0, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
		try
		{
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
					goto IL_70;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						if (false)
						{
						}
						((IDisposable)fileStream).Dispose();
						num = 1;
						continue;
					}
					break;
				}
				goto IL_31;
				IL_3C:
				num = 2;
				continue;
				IL_31:
				if (true)
				{
				}
				if (fileStream != null)
				{
					goto IL_3C;
				}
				break;
			}
			IL_70:;
		}
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x000D2ABC File Offset: 0x000D1ABC
	public void ᜃ()
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3D;
			case 1:
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
					if (this.ᜁ != null)
					{
						num = 3;
						continue;
					}
					goto IL_3D;
				}
				break;
			case 2:
				return;
			case 3:
				Marshal.FinalReleaseComObject(this.ᜁ);
				GC.SuppressFinalize(this.ᜁ);
				this.ᜁ = null;
				num = 0;
				continue;
			case 5:
				this.ᜀ.ᜂ();
				this.ᜀ = null;
				num = 1;
				continue;
			}
			if (this.ᜀ != null)
			{
				num = 5;
				continue;
			}
			break;
			IL_3D:
			GC.SuppressFinalize(this);
			num = 2;
		}
	}

	// Token: 0x04001433 RID: 5171
	private spr\u1DDA ᜀ;

	// Token: 0x04001434 RID: 5172
	private sprḂ ᜁ;

	// Token: 0x04001435 RID: 5173
	private spr\u222F ᜂ;
}
