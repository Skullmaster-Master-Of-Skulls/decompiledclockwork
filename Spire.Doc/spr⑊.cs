using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200035C RID: 860
internal class spr\u244A : spr\u2578, spr\u19AD
{
	// Token: 0x06002E29 RID: 11817 RVA: 0x002BFA68 File Offset: 0x002BEA68
	public spr\u2486 ᜈ()
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

	// Token: 0x06002E2A RID: 11818 RVA: 0x002BFAAC File Offset: 0x002BEAAC
	protected Stream ᜊ()
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

	// Token: 0x06002E2B RID: 11819 RVA: 0x002BFAF0 File Offset: 0x002BEAF0
	protected void ᜀ(Stream A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06002E2C RID: 11820 RVA: 0x002BFB34 File Offset: 0x002BEB34
	public spr\u20BF ᜅ()
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
		return this.ᜀ;
	}

	// Token: 0x06002E2D RID: 11821 RVA: 0x002BFB78 File Offset: 0x002BEB78
	public spr\u244A(spr\u20BF A_0, spr\u2486 A_1)
	{
		int a_ = 0;
		base..ctor(A_1.ᜀ());
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("eŧ٩५", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ͥ٧ṩṫ᝭", a_));
		}
		if (A_1.ᜄ() != spr\u2486.EntryType.Stream)
		{
			throw new ArgumentOutOfRangeException(ClipboardData.b("ͥ٧ṩṫ᝭", a_));
		}
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x06002E2E RID: 11822 RVA: 0x002BFBFC File Offset: 0x002BEBFC
	public virtual void ᜃ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜂ = this.ᜀ.ᜀ(this.ᜁ);
				num = 1;
				continue;
			case 1:
				if (true)
				{
				}
				goto IL_57;
			case 2:
				IL_08:
				break;
			}
			if (this.ᜂ == null)
			{
				num = 0;
				continue;
			}
			IL_57:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_6D;
			}
		}
		IL_6D:
		if (false)
		{
		}
	}

	// Token: 0x06002E2F RID: 11823 RVA: 0x002BFC88 File Offset: 0x002BEC88
	public virtual int ᜀ(byte[] A_0, int A_1, int A_2)
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
		return this.ᜂ.Read(A_0, A_1, A_2);
	}

	// Token: 0x06002E30 RID: 11824 RVA: 0x002BFCD4 File Offset: 0x002BECD4
	public virtual void ᜁ(byte[] A_0, int A_1, int A_2)
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
		this.ᜂ.Write(A_0, A_1, A_2);
	}

	// Token: 0x06002E31 RID: 11825 RVA: 0x002BFD20 File Offset: 0x002BED20
	public virtual long ᜀ(long A_0, SeekOrigin A_1)
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
		return this.ᜂ.Seek(A_0, A_1);
	}

	// Token: 0x06002E32 RID: 11826 RVA: 0x002BFD68 File Offset: 0x002BED68
	public virtual void ᜁ(long A_0)
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
		this.ᜂ.SetLength(A_0);
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x002BFDB0 File Offset: 0x002BEDB0
	public virtual void ᜄ()
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
		this.Flush();
		this.ᜂ.Dispose();
		this.ᜂ = null;
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x002BFE04 File Offset: 0x002BEE04
	public virtual long ᜂ()
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
			if (this.ᜂ == null)
			{
				return (long)((ulong)this.ᜁ.ᜌ());
			}
			break;
		}
		return this.ᜂ.Length;
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x002BFE64 File Offset: 0x002BEE64
	public virtual long ᜀ()
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
		return this.ᜂ.Position;
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x002BFEAC File Offset: 0x002BEEAC
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
		this.ᜂ.Position = A_0;
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x002BFEF4 File Offset: 0x002BEEF4
	public virtual void ᜁ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_57;
			case 1:
				IL_08:
				break;
			case 2:
				this.ᜀ.ᜂ(this.ᜁ, this.ᜂ);
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (this.ᜂ != null)
			{
				num = 2;
				continue;
			}
			IL_57:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_6D;
			}
		}
		IL_6D:
		if (false)
		{
		}
	}

	// Token: 0x06002E38 RID: 11832 RVA: 0x002BFF80 File Offset: 0x002BEF80
	public virtual bool ᜇ()
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

	// Token: 0x06002E39 RID: 11833 RVA: 0x002BFFBC File Offset: 0x002BEFBC
	public virtual bool ᜆ()
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
		return true;
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x002BFFF8 File Offset: 0x002BEFF8
	public virtual bool ᜉ()
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

	// Token: 0x06002E3B RID: 11835 RVA: 0x002C0034 File Offset: 0x002BF034
	protected override void ᜀ(bool A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				base.Dispose(A_0);
				this.ᜂ.Dispose();
				this.ᜂ = null;
				this.ᜀ = null;
				this.ᜁ = null;
				GC.SuppressFinalize(this);
				num = 2;
				continue;
			case 1:
				IL_08:
				break;
			case 2:
				goto IL_6D;
			}
			if (true)
			{
			}
			if (this.ᜂ != null)
			{
				num = 0;
				continue;
			}
			IL_6D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_83;
			}
		}
		IL_83:
		if (false)
		{
		}
	}

	// Token: 0x040026AD RID: 9901
	private new spr\u20BF ᜀ;

	// Token: 0x040026AE RID: 9902
	private spr\u2486 ᜁ;

	// Token: 0x040026AF RID: 9903
	private Stream ᜂ;
}
