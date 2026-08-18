using System;
using System.IO;
using System.Runtime.InteropServices;
using Spire.CompoundFile.Doc;

// Token: 0x020002DB RID: 731
internal class sprỷ : spr\u2578
{
	// Token: 0x060027E1 RID: 10209 RVA: 0x0027E4AC File Offset: 0x0027D4AC
	public sprỷ(sprḙ A_0, string A_1)
	{
		int a_ = 8;
		base..ctor(A_1);
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ᵭѯqᅳ᝵ᕷ", a_));
		}
		this.ᜀ = A_0;
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x0027E4EC File Offset: 0x0027D4EC
	public virtual int ᜁ(byte[] A_0, int A_1, int A_2)
	{
		int num2;
		for (;;)
		{
			this.ᜀ(A_0, A_1, A_2);
			int num = 0;
			for (;;)
			{
				byte[] array;
				byte[] array2;
				switch (num)
				{
				case 0:
					if (A_1 == 0)
					{
						num = 5;
						continue;
					}
					num = 2;
					continue;
				case 1:
					if (A_1 != 0)
					{
						num = 4;
						continue;
					}
					return num2;
				case 2:
					goto IL_DC;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DC;
					default:
						goto IL_80;
					}
					break;
				case 4:
					Buffer.BlockCopy(array, 0, A_0, A_1, num2);
					num = 3;
					continue;
				case 5:
					if (true)
					{
					}
					num = 6;
					continue;
				case 6:
					array2 = A_0;
					goto IL_88;
				}
				break;
				IL_88:
				array = array2;
				uint num3 = 0U;
				this.ᜀ.ᜀ(array, (uint)A_2, ref num3);
				num2 = (int)num3;
				this.ᜁ += (long)((ulong)num3);
				num = 1;
				continue;
				IL_DC:
				array2 = new byte[A_2];
				goto IL_88;
			}
		}
		IL_80:
		if (false)
		{
		}
		return num2;
	}

	// Token: 0x060027E3 RID: 10211 RVA: 0x0027E5E0 File Offset: 0x0027D5E0
	public virtual void ᜂ(byte[] A_0, int A_1, int A_2)
	{
		byte[] array;
		for (;;)
		{
			this.ᜀ(A_0, A_1, A_2);
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
						continue;
					default:
						goto IL_79;
					}
					break;
				case 1:
					array = A_0;
					num = 0;
					continue;
				case 2:
					if (true)
					{
					}
					if (A_1 == 0)
					{
						num = 1;
						continue;
					}
					array = new byte[A_2];
					Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
					num = 3;
					continue;
				case 3:
					goto IL_57;
				}
				break;
			}
		}
		IL_57:
		goto IL_8B;
		IL_79:
		if (false)
		{
		}
		IL_8B:
		uint num2 = 0U;
		this.ᜀ.ᜁ(array, (uint)A_2, ref num2);
		this.ᜁ += (long)((ulong)num2);
	}

	// Token: 0x060027E4 RID: 10212 RVA: 0x0027E69C File Offset: 0x0027D69C
	public virtual long ᜀ(long A_0, SeekOrigin A_1)
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
		long result;
		this.ᜀ.ᜀ(A_0, A_1, out result);
		this.ᜁ = result;
		return result;
	}

	// Token: 0x060027E5 RID: 10213 RVA: 0x0027E6F0 File Offset: 0x0027D6F0
	public virtual void ᜁ(long A_0)
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
		this.ᜀ.ᜀ((ulong)A_0);
	}

	// Token: 0x060027E6 RID: 10214 RVA: 0x0027E738 File Offset: 0x0027D738
	public virtual long ᜅ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				sprỷ.ᜂ = true;
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
					num = 1;
					continue;
				}
				break;
			case 1:
				goto IL_5F;
			}
			IL_1C:
			if (!sprỷ.ᜂ)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_1C;
		}
		IL_5F:
		long result;
		this.ᜀ.ᜀ(0L, SeekOrigin.End, out result);
		this.ᜀ.ᜀ(this.ᜁ, SeekOrigin.Begin, out this.ᜁ);
		return result;
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x0027E7DC File Offset: 0x0027D7DC
	public virtual long ᜃ()
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

	// Token: 0x060027E8 RID: 10216 RVA: 0x0027E820 File Offset: 0x0027D820
	public override void ᜀ(long A_0)
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
		this.ᜁ = this.Seek(A_0, SeekOrigin.Begin);
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x0027E86C File Offset: 0x0027D86C
	public virtual bool ᜀ()
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
		return true;
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x0027E8A8 File Offset: 0x0027D8A8
	public virtual bool ᜂ()
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
		return true;
	}

	// Token: 0x060027EB RID: 10219 RVA: 0x0027E8E4 File Offset: 0x0027D8E4
	public virtual bool ᜁ()
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
		return true;
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x0027E920 File Offset: 0x0027D920
	public virtual void ᜄ()
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
		this.ᜀ.ᜀ(0U);
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x0027E968 File Offset: 0x0027D968
	protected override void ᜀ(bool A_0)
	{
		for (;;)
		{
			base.Dispose(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3B;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					}
					goto Block_2;
				case 2:
					if (true)
					{
					}
					if (this.ᜀ != null)
					{
						num = 0;
						continue;
					}
					goto IL_8D;
				}
				break;
				IL_3B:
				this.ᜀ.ᜀ(0U);
				Marshal.FinalReleaseComObject(this.ᜀ);
				this.ᜀ = null;
				num = 1;
			}
		}
		Block_2:
		if (false)
		{
		}
		IL_8D:
		this.ᜁ = -1L;
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x0027EA0C File Offset: 0x0027DA0C
	private void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 3;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_1 + A_2 > A_0.Length)
				{
					num = 3;
					continue;
				}
				num = 6;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E0;
				default:
					goto IL_C6;
				}
				break;
			case 3:
				goto IL_9A;
			case 4:
				goto IL_80;
			case 5:
				if (A_2 < 0)
				{
					num = 7;
					continue;
				}
				return;
			case 6:
				if (A_1 < 0)
				{
					num = 4;
					continue;
				}
				goto IL_E0;
			case 7:
				goto IL_FA;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 1;
			continue;
			IL_E0:
			num = 5;
		}
		IL_80:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("٨൪୬ᱮᑰݲ", a_));
		IL_9A:
		throw new ArgumentOutOfRangeException(ClipboardData.b("⡨ᥪὬ๮ࡰ卲ٴṶ͸Ṻ兼彾ﾊ권ﾐ떔ﮖﲘ煮즠莢솤좦첨\ud8aa쎬袮얰鎲\ud8b4횶춸\ud8ba햼龾꓀ꋂꛄ꿆꓊만꟎듐ꇒ", a_));
		IL_C6:
		if (false)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("୨Ṫ୬८ᑰŲ", a_));
		IL_FA:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ը๪ͬ࡮հ᭲", a_));
	}

	// Token: 0x040022FC RID: 8956
	private new sprḙ ᜀ;

	// Token: 0x040022FD RID: 8957
	private long ᜁ;

	// Token: 0x040022FE RID: 8958
	private static bool ᜂ;
}
