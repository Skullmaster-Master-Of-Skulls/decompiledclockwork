using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;

// Token: 0x02000265 RID: 613
internal abstract class spr\u25AD : ICloneable
{
	// Token: 0x060024C4 RID: 9412 RVA: 0x00155AB0 File Offset: 0x00154AB0
	public TObjSubRecordType ᜏ()
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

	// Token: 0x060024C5 RID: 9413 RVA: 0x00155AF4 File Offset: 0x00154AF4
	public ushort ᜎ()
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

	// Token: 0x060024C6 RID: 9414 RVA: 0x00155B38 File Offset: 0x00154B38
	protected void ᜂ(ushort A_0)
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

	// Token: 0x060024C7 RID: 9415 RVA: 0x00155B7C File Offset: 0x00154B7C
	private spr\u25AD()
	{
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x00155B90 File Offset: 0x00154B90
	protected spr\u25AD(TObjSubRecordType A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x060024C9 RID: 9417 RVA: 0x00155BAC File Offset: 0x00154BAC
	[CLSCompliant(false)]
	protected spr\u25AD(TObjSubRecordType A_0, ushort A_1, byte[] A_2)
	{
		this.ᜁ = A_0;
		this.ᜂ = A_1;
		this.ᜀ(A_2);
	}

	// Token: 0x060024CA RID: 9418
	protected abstract void ᜀ(byte[] A_0);

	// Token: 0x060024CB RID: 9419 RVA: 0x00155BD4 File Offset: 0x00154BD4
	public virtual void ᜀ(DataProvider A_0, int A_1)
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
		A_0.WriteInt16(A_1, (short)this.ᜏ());
		A_1 += 2;
		ushort value = (ushort)(this.ᜀ(ExcelVersion.Version97to2003) - 4);
		A_0.WriteUInt16(A_1, value);
		A_1 += 2;
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x060024CC RID: 9420 RVA: 0x00155C44 File Offset: 0x00154C44
	protected virtual void ᜁ(DataProvider A_0, int A_1)
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

	// Token: 0x060024CD RID: 9421
	public abstract int ᜀ(ExcelVersion A_0);

	// Token: 0x060024CE RID: 9422 RVA: 0x00155C80 File Offset: 0x00154C80
	public virtual object ᜁ()
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
		return base.MemberwiseClone();
	}

	// Token: 0x04001292 RID: 4754
	protected const int ᜀ = 4;

	// Token: 0x04001293 RID: 4755
	private TObjSubRecordType ᜁ;

	// Token: 0x04001294 RID: 4756
	private ushort ᜂ;
}
