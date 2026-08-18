using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000566 RID: 1382
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.Unknown)]
internal class sprᱬ : spr\u251F, ICloneable
{
	// Token: 0x0600532E RID: 21294 RVA: 0x0033EA98 File Offset: 0x0033DA98
	public new static BiffRecordRaw ᜀ()
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
		return sprᱬ.ᜀ;
	}

	// Token: 0x0600532F RID: 21295 RVA: 0x0033EAD8 File Offset: 0x0033DAD8
	public sprᱬ()
	{
	}

	// Token: 0x06005330 RID: 21296 RVA: 0x0033EAEC File Offset: 0x0033DAEC
	public sprᱬ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005331 RID: 21297 RVA: 0x0033EB04 File Offset: 0x0033DB04
	public sprᱬ(BinaryReader A_0, out int A_1)
	{
		this.m_iCode = (int)A_0.ReadInt16();
		this.m_iLength = (int)A_0.ReadInt16();
		this.ᜀ = new byte[this.m_iLength];
		A_0.BaseStream.Read(this.ᜀ, 0, this.m_iLength);
		A_1 = this.m_iLength;
	}

	// Token: 0x06005332 RID: 21298 RVA: 0x0033EB64 File Offset: 0x0033DB64
	public sprᱬ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005333 RID: 21299 RVA: 0x0033EB78 File Offset: 0x0033DB78
	public virtual bool ᜃ()
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

	// Token: 0x06005334 RID: 21300 RVA: 0x0033EBB4 File Offset: 0x0033DBB4
	public int ᜄ()
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
		return base.RecordCode;
	}

	// Token: 0x06005335 RID: 21301 RVA: 0x0033EBF8 File Offset: 0x0033DBF8
	public new void ᜀ(int A_0)
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
		this.m_iCode = A_0;
	}

	// Token: 0x06005336 RID: 21302 RVA: 0x0033EC3C File Offset: 0x0033DC3C
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
		return this.m_iLength;
	}

	// Token: 0x06005337 RID: 21303 RVA: 0x0033EC80 File Offset: 0x0033DC80
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
		this.m_iLength = A_0;
	}

	// Token: 0x06005338 RID: 21304 RVA: 0x0033ECC4 File Offset: 0x0033DCC4
	public override void ᜂ()
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
		this.ᜁ = new byte[this.ᜀ.Length];
		this.ᜀ.CopyTo(this.ᜁ, 0);
	}

	// Token: 0x06005339 RID: 21305 RVA: 0x0033ED24 File Offset: 0x0033DD24
	public override void ᜀ(ExcelVersion A_0)
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
	}

	// Token: 0x0600533A RID: 21306 RVA: 0x0033ED60 File Offset: 0x0033DD60
	public override int ᜁ(ExcelVersion A_0)
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
		return this.m_iLength;
	}

	// Token: 0x0600533B RID: 21307 RVA: 0x0033EDA4 File Offset: 0x0033DDA4
	public object ᜁ()
	{
		sprᱬ sprᱬ;
		for (;;)
		{
			sprᱬ = (sprᱬ)base.Clone();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return sprᱬ;
				case 1:
					if (this.ᜁ == null)
					{
						return sprᱬ;
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
						num = 2;
						continue;
					}
					break;
				case 2:
					sprᱬ.ᜁ = spr\u1CD3.ᜀ(this.ᜁ);
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
		}
		return sprᱬ;
	}

	// Token: 0x0600533C RID: 21308 RVA: 0x0033EE38 File Offset: 0x0033DE38
	// Note: this type is marked as 'beforefieldinit'.
	static sprᱬ()
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
		sprᱬ.ᜀ = new sprᱬ();
	}

	// Token: 0x040026FB RID: 9979
	private new static sprᱬ ᜀ;

	// Token: 0x040026FC RID: 9980
	private new byte[] ᜁ;
}
