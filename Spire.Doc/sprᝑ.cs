using System;
using System.IO;
using Spire.Doc;

// Token: 0x02000287 RID: 647
[CLSCompliant(false)]
internal class sprᝑ : spr\u23F8
{
	// Token: 0x0600225B RID: 8795 RVA: 0x00237114 File Offset: 0x00236114
	internal bool ᜂ()
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
		return (this.ᜂ & 128) != 0;
	}

	// Token: 0x0600225C RID: 8796 RVA: 0x00237164 File Offset: 0x00236164
	internal new void ᜃ(bool A_0)
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
		this.ᜂ = (byte)spr\u23F8.ᜀ((int)this.ᜂ, 7, A_0);
	}

	// Token: 0x0600225D RID: 8797 RVA: 0x002371B4 File Offset: 0x002361B4
	internal bool ᜆ()
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
		return (this.ᜂ & 4) != 0;
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x00237200 File Offset: 0x00236200
	internal void ᜂ(bool A_0)
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
		this.ᜂ = (byte)spr\u23F8.ᜀ((int)this.ᜂ, 2, A_0);
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x00237250 File Offset: 0x00236250
	internal bool ᜅ()
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
		return (this.ᜂ & 8) != 0;
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x0023729C File Offset: 0x0023629C
	internal void ᜀ(bool A_0)
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
		this.ᜂ = (byte)spr\u23F8.ᜀ((int)this.ᜂ, 3, A_0);
	}

	// Token: 0x06002261 RID: 8801 RVA: 0x002372EC File Offset: 0x002362EC
	internal bool ᜄ()
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
		return (this.ᜂ & 64) != 0;
	}

	// Token: 0x06002262 RID: 8802 RVA: 0x00237338 File Offset: 0x00236338
	internal void ᜁ(bool A_0)
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
		this.ᜂ = (byte)spr\u23F8.ᜀ((int)this.ᜂ, 6, A_0);
	}

	// Token: 0x06002263 RID: 8803 RVA: 0x00237388 File Offset: 0x00236388
	internal FieldType ᜈ()
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
		return (FieldType)this.ᜂ;
	}

	// Token: 0x06002264 RID: 8804 RVA: 0x002373CC File Offset: 0x002363CC
	internal void ᜀ(FieldType A_0)
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
		this.ᜂ = (byte)A_0;
	}

	// Token: 0x06002265 RID: 8805 RVA: 0x00237410 File Offset: 0x00236410
	internal byte ᜉ()
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
		return this.ᜀ;
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x00237454 File Offset: 0x00236454
	internal void ᜀ(byte A_0)
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

	// Token: 0x06002267 RID: 8807 RVA: 0x00237498 File Offset: 0x00236498
	internal sprᝑ(BinaryReader A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002268 RID: 8808 RVA: 0x002374B4 File Offset: 0x002364B4
	internal sprᝑ()
	{
	}

	// Token: 0x06002269 RID: 8809 RVA: 0x002374C8 File Offset: 0x002364C8
	internal void ᜀ(short A_0)
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
		byte[] bytes = BitConverter.GetBytes(A_0);
		this.ᜀ = (bytes[0] & 31);
		this.ᜁ = (bytes[0] & 224);
		this.ᜂ = bytes[1];
	}

	// Token: 0x0600226A RID: 8810 RVA: 0x00237530 File Offset: 0x00236530
	internal short ᜁ()
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
		return BitConverter.ToInt16(new byte[]
		{
			this.ᜀ | this.ᜁ,
			this.ᜂ
		}, 0);
	}

	// Token: 0x0600226B RID: 8811 RVA: 0x00237594 File Offset: 0x00236594
	internal sprᝑ ᜀ()
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
		sprᝑ sprᝑ = new sprᝑ();
		sprᝑ.ᜃ(this.ᜂ());
		sprᝑ.ᜁ(this.ᜄ());
		sprᝑ.ᜂ(this.ᜆ());
		sprᝑ.ᜀ(this.ᜅ());
		sprᝑ.ᜀ(this.ᜉ());
		sprᝑ.ᜀ(this.ᜈ());
		return sprᝑ;
	}

	// Token: 0x0600226C RID: 8812 RVA: 0x00237620 File Offset: 0x00236620
	internal void ᜀ(BinaryReader A_0)
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
		byte[] array = A_0.ReadBytes(2);
		this.ᜀ = (array[0] & 31);
		this.ᜁ = (array[0] & 224);
		this.ᜂ = array[1];
	}

	// Token: 0x0600226D RID: 8813 RVA: 0x0023768C File Offset: 0x0023668C
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
		A_0.WriteByte(this.ᜀ | this.ᜁ);
		A_0.WriteByte(this.ᜂ);
	}

	// Token: 0x0400210A RID: 8458
	private new byte ᜀ;

	// Token: 0x0400210B RID: 8459
	private new byte ᜁ;

	// Token: 0x0400210C RID: 8460
	private new byte ᜂ;
}
