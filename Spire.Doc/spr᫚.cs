using System;
using System.IO;

// Token: 0x020002B5 RID: 693
internal class spr\u1ADA : spr\u23F8
{
	// Token: 0x06002560 RID: 9568 RVA: 0x00257FB0 File Offset: 0x00256FB0
	public int ᜄ()
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

	// Token: 0x06002561 RID: 9569 RVA: 0x00257FF4 File Offset: 0x00256FF4
	public void ᜅ(int A_0)
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

	// Token: 0x06002562 RID: 9570 RVA: 0x00258038 File Offset: 0x00257038
	public int ᜆ()
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

	// Token: 0x06002563 RID: 9571 RVA: 0x0025807C File Offset: 0x0025707C
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
		this.ᜁ = A_0;
	}

	// Token: 0x06002564 RID: 9572 RVA: 0x002580C0 File Offset: 0x002570C0
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
		return this.ᜂ;
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x00258104 File Offset: 0x00257104
	public void ᜁ(int A_0)
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

	// Token: 0x06002566 RID: 9574 RVA: 0x00258148 File Offset: 0x00257148
	public int ᜁ()
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

	// Token: 0x06002567 RID: 9575 RVA: 0x0025818C File Offset: 0x0025718C
	public void ᜂ(int A_0)
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

	// Token: 0x06002568 RID: 9576 RVA: 0x002581D0 File Offset: 0x002571D0
	internal int ᜂ()
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
		return this.ᜄ;
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x00258214 File Offset: 0x00257214
	internal void ᜄ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x00258258 File Offset: 0x00257258
	internal int ᜀ()
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
		return this.ᜅ;
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x0025829C File Offset: 0x0025729C
	internal new void ᜃ(int A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x002582E0 File Offset: 0x002572E0
	public void ᜁ(Stream A_0)
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
		this.ᜀ = A_0.ReadByte();
		this.ᜂ = A_0.ReadByte();
		this.ᜁ = A_0.ReadByte();
		this.ᜃ = A_0.ReadByte();
		this.ᜄ(this.ᜁ - this.ᜀ);
		this.ᜃ(this.ᜃ - this.ᜂ);
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x00258374 File Offset: 0x00257374
	public void ᜀ(Stream A_0)
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
		A_0.WriteByte((byte)this.ᜀ);
		A_0.WriteByte((byte)this.ᜂ);
		spr\u23F8.ᜁ(A_0, this.ᜀ + this.ᜄ);
		spr\u23F8.ᜁ(A_0, this.ᜂ + this.ᜅ);
	}

	// Token: 0x040021EC RID: 8684
	private new int ᜀ;

	// Token: 0x040021ED RID: 8685
	private new int ᜁ;

	// Token: 0x040021EE RID: 8686
	private new int ᜂ;

	// Token: 0x040021EF RID: 8687
	private new int ᜃ;

	// Token: 0x040021F0 RID: 8688
	private new int ᜄ;

	// Token: 0x040021F1 RID: 8689
	private new int ᜅ;
}
