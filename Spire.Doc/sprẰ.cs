using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x02000280 RID: 640
[CLSCompliant(false)]
internal class sprẰ
{
	// Token: 0x06002213 RID: 8723 RVA: 0x00235090 File Offset: 0x00234090
	internal byte[] ᜁ()
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

	// Token: 0x06002214 RID: 8724 RVA: 0x002350D4 File Offset: 0x002340D4
	internal void ᜁ(byte[] A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x00235118 File Offset: 0x00234118
	internal byte[] ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x0023515C File Offset: 0x0023415C
	internal void ᜂ(byte[] A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06002217 RID: 8727 RVA: 0x002351A0 File Offset: 0x002341A0
	internal byte[] ᜂ()
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

	// Token: 0x06002218 RID: 8728 RVA: 0x002351E4 File Offset: 0x002341E4
	internal void ᜀ(byte[] A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x00235228 File Offset: 0x00234228
	internal int ᜃ()
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
		return this.ᜃ;
	}

	// Token: 0x0600221A RID: 8730 RVA: 0x0023526C File Offset: 0x0023426C
	internal void ᜀ(int A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x0600221B RID: 8731 RVA: 0x002352B0 File Offset: 0x002342B0
	internal sprẰ()
	{
	}

	// Token: 0x0600221C RID: 8732 RVA: 0x002352DC File Offset: 0x002342DC
	internal sprẰ(Stream A_0)
	{
		this.ᜁ(A_0);
	}

	// Token: 0x0600221D RID: 8733 RVA: 0x00235310 File Offset: 0x00234310
	internal void ᜁ(Stream A_0)
	{
		int a_ = 17;
		while (A_0 == null)
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
				throw new ArgumentNullException(ClipboardData.b("Ѷ൸ॺ᡼Ṿ", a_));
			}
		}
		byte[] a_2 = new byte[4];
		int num = this.ᜄ.ᜀ(A_0, a_2);
		this.ᜀ = new byte[num];
		A_0.Read(this.ᜀ, 0, num);
		A_0.Read(this.ᜁ, 0, this.ᜁ.Length);
		this.ᜃ = this.ᜄ.ᜀ(A_0, a_2);
		int num2 = (int)(A_0.Length - A_0.Position);
		this.ᜂ = new byte[num2];
		A_0.Read(this.ᜂ, 0, num2);
	}

	// Token: 0x0600221E RID: 8734 RVA: 0x002353F0 File Offset: 0x002343F0
	internal void ᜀ(Stream A_0)
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
		int num = this.ᜀ.Length;
		this.ᜄ.ᜀ(A_0, num);
		A_0.Write(this.ᜀ, 0, num);
		A_0.Write(this.ᜁ, 0, this.ᜁ.Length);
		this.ᜄ.ᜀ(A_0, this.ᜃ);
		int count = this.ᜂ.Length;
		A_0.Write(this.ᜂ, 0, count);
	}

	// Token: 0x040020D0 RID: 8400
	private byte[] ᜀ;

	// Token: 0x040020D1 RID: 8401
	private byte[] ᜁ = new byte[16];

	// Token: 0x040020D2 RID: 8402
	private byte[] ᜂ;

	// Token: 0x040020D3 RID: 8403
	private int ᜃ;

	// Token: 0x040020D4 RID: 8404
	private spr\u1AED ᜄ = new spr\u1AED();
}
