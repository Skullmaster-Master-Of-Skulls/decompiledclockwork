using System;
using System.Drawing;
using Spire.DataExport.CollectionEditors;

// Token: 0x0200014F RID: 335
internal class spr\u2495 : ICloneable
{
	// Token: 0x0600083B RID: 2107 RVA: 0x00052AE0 File Offset: 0x00051AE0
	public spr\u2495(Color A_0)
	{
		this.ᜀ = A_0.R;
		this.ᜁ = A_0.G;
		this.ᜂ = A_0.B;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00052B1C File Offset: 0x00051B1C
	public spr\u2495(byte A_0, byte A_1, byte A_2)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00052B44 File Offset: 0x00051B44
	public object ᜀ()
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
		return new spr\u2495(this.ᜂ(), this.ᜃ(), this.ᜆ());
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00052B98 File Offset: 0x00051B98
	public byte ᜂ()
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

	// Token: 0x0600083F RID: 2111 RVA: 0x00052BDC File Offset: 0x00051BDC
	public void ᜂ(byte A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00052C20 File Offset: 0x00051C20
	public byte ᜃ()
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

	// Token: 0x06000841 RID: 2113 RVA: 0x00052C64 File Offset: 0x00051C64
	public void ᜁ(byte A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00052CA8 File Offset: 0x00051CA8
	public byte ᜆ()
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

	// Token: 0x06000843 RID: 2115 RVA: 0x00052CEC File Offset: 0x00051CEC
	public void ᜀ(byte A_0)
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

	// Token: 0x06000844 RID: 2116 RVA: 0x00052D30 File Offset: 0x00051D30
	public Color ᜄ()
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
		return Color.FromArgb((int)this.ᜀ, (int)this.ᜁ, (int)this.ᜂ);
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00052D84 File Offset: 0x00051D84
	public string ᜅ()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(HyperlinksCollectionEditor.b("䈝刟䜡䀣崥ᠧ圩瀫䤭䈯圱儳堵䌷ହ䄻戽∿⹁ㅃ⍅㍇硉ㅋ畍", a_), this.ᜀ, this.ᜁ, this.ᜂ);
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00052DFC File Offset: 0x00051DFC
	public int ᜁ()
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
		return this.ᜅ().Length;
	}

	// Token: 0x04000638 RID: 1592
	private byte ᜀ;

	// Token: 0x04000639 RID: 1593
	private byte ᜁ;

	// Token: 0x0400063A RID: 1594
	private byte ᜂ;
}
