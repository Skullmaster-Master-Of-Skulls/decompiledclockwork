using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000436 RID: 1078
internal class spr\u1B4E : spr\u1FDC, spr\u2228
{
	// Token: 0x06004101 RID: 16641 RVA: 0x00245F50 File Offset: 0x00244F50
	public spr\u1DAB ᜈ()
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

	// Token: 0x06004102 RID: 16642 RVA: 0x00245F94 File Offset: 0x00244F94
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

	// Token: 0x06004103 RID: 16643 RVA: 0x00245FD8 File Offset: 0x00244FD8
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

	// Token: 0x06004104 RID: 16644 RVA: 0x0024601C File Offset: 0x0024501C
	public spr\u2604 ᜅ()
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

	// Token: 0x06004105 RID: 16645 RVA: 0x00246060 File Offset: 0x00245060
	public spr\u1B4E(spr\u2604 A_0, spr\u1DAB A_1)
	{
		int a_ = 0;
		base..ctor(A_1.ᜀ());
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("倵儷嘹夻", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("匵嘷丹主䜽", a_));
		}
		if (A_1.ᜄ() != spr\u1DAB.EntryType.Stream)
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匵嘷丹主䜽", a_));
		}
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x06004106 RID: 16646 RVA: 0x002460E4 File Offset: 0x002450E4
	public virtual void ᜃ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_38:
			if (this.ᜂ != null)
			{
				return;
			}
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				this.ᜂ = this.ᜀ.ᜀ(this.ᜁ);
				num = 2;
				continue;
			case 2:
				return;
			}
			break;
		}
		goto IL_38;
	}

	// Token: 0x06004107 RID: 16647 RVA: 0x00246170 File Offset: 0x00245170
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

	// Token: 0x06004108 RID: 16648 RVA: 0x002461BC File Offset: 0x002451BC
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

	// Token: 0x06004109 RID: 16649 RVA: 0x00246208 File Offset: 0x00245208
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

	// Token: 0x0600410A RID: 16650 RVA: 0x00246250 File Offset: 0x00245250
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

	// Token: 0x0600410B RID: 16651 RVA: 0x00246298 File Offset: 0x00245298
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

	// Token: 0x0600410C RID: 16652 RVA: 0x002462EC File Offset: 0x002452EC
	public virtual long ᜂ()
	{
		if (true)
		{
		}
		if (this.ᜂ != null)
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
				return this.ᜂ.Length;
			}
		}
		return (long)((ulong)this.ᜁ.ᜌ());
	}

	// Token: 0x0600410D RID: 16653 RVA: 0x0024634C File Offset: 0x0024534C
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

	// Token: 0x0600410E RID: 16654 RVA: 0x00246394 File Offset: 0x00245394
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

	// Token: 0x0600410F RID: 16655 RVA: 0x002463DC File Offset: 0x002453DC
	public virtual void ᜁ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_40:
			if (this.ᜂ == null)
			{
				return;
			}
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_24;
			case 1:
				return;
			case 2:
				this.ᜀ.ᜂ(this.ᜁ, this.ᜂ);
				num = 1;
				continue;
			}
			goto IL_40;
		}
		IL_24:
		if (true)
		{
		}
		goto IL_40;
	}

	// Token: 0x06004110 RID: 16656 RVA: 0x00246468 File Offset: 0x00245468
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

	// Token: 0x06004111 RID: 16657 RVA: 0x002464A4 File Offset: 0x002454A4
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

	// Token: 0x06004112 RID: 16658 RVA: 0x002464E0 File Offset: 0x002454E0
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

	// Token: 0x06004113 RID: 16659 RVA: 0x0024651C File Offset: 0x0024551C
	protected override void ᜀ(bool A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_40:
			if (this.ᜂ == null)
			{
				return;
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_24;
			case 1:
				base.Dispose(A_0);
				this.ᜂ.Dispose();
				this.ᜂ = null;
				this.ᜀ = null;
				this.ᜁ = null;
				GC.SuppressFinalize(this);
				num = 2;
				continue;
			case 2:
				return;
			}
			goto IL_40;
		}
		IL_24:
		if (true)
		{
		}
		goto IL_40;
	}

	// Token: 0x04001CFC RID: 7420
	private new spr\u2604 ᜀ;

	// Token: 0x04001CFD RID: 7421
	private spr\u1DAB ᜁ;

	// Token: 0x04001CFE RID: 7422
	private Stream ᜂ;
}
