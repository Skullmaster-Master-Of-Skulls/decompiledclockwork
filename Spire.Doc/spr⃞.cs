using System;
using System.IO;

// Token: 0x02000260 RID: 608
internal class spr\u20DE : spr\u2578
{
	// Token: 0x06001FE2 RID: 8162 RVA: 0x0021EA9C File Offset: 0x0021DA9C
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

	// Token: 0x06001FE3 RID: 8163 RVA: 0x0021EAE4 File Offset: 0x0021DAE4
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

	// Token: 0x06001FE4 RID: 8164 RVA: 0x0021EB2C File Offset: 0x0021DB2C
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

	// Token: 0x06001FE5 RID: 8165 RVA: 0x0021EB74 File Offset: 0x0021DB74
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

	// Token: 0x06001FE6 RID: 8166 RVA: 0x0021EBBC File Offset: 0x0021DBBC
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

	// Token: 0x06001FE7 RID: 8167 RVA: 0x0021EC04 File Offset: 0x0021DC04
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

	// Token: 0x06001FE8 RID: 8168 RVA: 0x0021EC4C File Offset: 0x0021DC4C
	public spr\u20DE(spr\u2578 A_0) : base(A_0.ᜋ())
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x0021EC6C File Offset: 0x0021DC6C
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

	// Token: 0x06001FEA RID: 8170 RVA: 0x0021ECB4 File Offset: 0x0021DCB4
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

	// Token: 0x06001FEB RID: 8171 RVA: 0x0021ED00 File Offset: 0x0021DD00
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

	// Token: 0x06001FEC RID: 8172 RVA: 0x0021ED48 File Offset: 0x0021DD48
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

	// Token: 0x06001FED RID: 8173 RVA: 0x0021ED90 File Offset: 0x0021DD90
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

	// Token: 0x06001FEE RID: 8174 RVA: 0x0021EDDC File Offset: 0x0021DDDC
	protected override void ᜀ(bool A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				base.Dispose(A_0);
				this.ᜀ = null;
				GC.SuppressFinalize(this);
				num = 2;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			case 2:
				return;
			}
			if (this.ᜀ == null)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x04001FEB RID: 8171
	private new spr\u2578 ᜀ;
}
