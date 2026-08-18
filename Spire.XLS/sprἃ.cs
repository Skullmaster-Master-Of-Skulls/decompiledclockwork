using System;
using System.IO;

// Token: 0x02000496 RID: 1174
internal class sprἃ : spr\u1FDC
{
	// Token: 0x06004848 RID: 18504 RVA: 0x002BA168 File Offset: 0x002B9168
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
		return this.ᜀ.CanRead;
	}

	// Token: 0x06004849 RID: 18505 RVA: 0x002BA1B0 File Offset: 0x002B91B0
	public virtual bool ᜁ()
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
		return this.ᜀ.CanSeek;
	}

	// Token: 0x0600484A RID: 18506 RVA: 0x002BA1F8 File Offset: 0x002B91F8
	public virtual bool ᜂ()
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
		return this.ᜀ.CanWrite;
	}

	// Token: 0x0600484B RID: 18507 RVA: 0x002BA240 File Offset: 0x002B9240
	public virtual long ᜅ()
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
		return this.ᜀ.Length;
	}

	// Token: 0x0600484C RID: 18508 RVA: 0x002BA288 File Offset: 0x002B9288
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
		return this.ᜀ.Position;
	}

	// Token: 0x0600484D RID: 18509 RVA: 0x002BA2D0 File Offset: 0x002B92D0
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
		this.ᜀ.Position = A_0;
	}

	// Token: 0x0600484E RID: 18510 RVA: 0x002BA318 File Offset: 0x002B9318
	public sprἃ(spr\u1FDC A_0) : base(A_0.ᜋ())
	{
		this.ᜀ = A_0;
	}

	// Token: 0x0600484F RID: 18511 RVA: 0x002BA338 File Offset: 0x002B9338
	public virtual void ᜄ()
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
		this.ᜀ.Flush();
	}

	// Token: 0x06004850 RID: 18512 RVA: 0x002BA380 File Offset: 0x002B9380
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
		return this.ᜀ.Read(A_0, A_1, A_2);
	}

	// Token: 0x06004851 RID: 18513 RVA: 0x002BA3CC File Offset: 0x002B93CC
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
		return this.ᜀ.Seek(A_0, A_1);
	}

	// Token: 0x06004852 RID: 18514 RVA: 0x002BA414 File Offset: 0x002B9414
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
		this.ᜀ.SetLength(A_0);
	}

	// Token: 0x06004853 RID: 18515 RVA: 0x002BA45C File Offset: 0x002B945C
	public virtual void ᜁ(byte[] A_0, int A_1, int A_2)
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
		this.ᜀ.Write(A_0, A_1, A_2);
	}

	// Token: 0x06004854 RID: 18516 RVA: 0x002BA4A8 File Offset: 0x002B94A8
	protected override void ᜀ(bool A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					base.Dispose(A_0);
					this.ᜀ = null;
					GC.SuppressFinalize(this);
					num = 1;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (this.ᜀ == null)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x040020DF RID: 8415
	private new spr\u1FDC ᜀ;
}
