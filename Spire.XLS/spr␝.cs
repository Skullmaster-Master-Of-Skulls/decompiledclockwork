using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200030D RID: 781
internal class spr\u241D
{
	// Token: 0x06003003 RID: 12291 RVA: 0x001B5E14 File Offset: 0x001B4E14
	public byte[] ᜁ()
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

	// Token: 0x06003004 RID: 12292 RVA: 0x001B5E58 File Offset: 0x001B4E58
	public void ᜁ(byte[] A_0)
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

	// Token: 0x06003005 RID: 12293 RVA: 0x001B5E9C File Offset: 0x001B4E9C
	public byte[] ᜀ()
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

	// Token: 0x06003006 RID: 12294 RVA: 0x001B5EE0 File Offset: 0x001B4EE0
	public void ᜂ(byte[] A_0)
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

	// Token: 0x06003007 RID: 12295 RVA: 0x001B5F24 File Offset: 0x001B4F24
	public byte[] ᜂ()
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

	// Token: 0x06003008 RID: 12296 RVA: 0x001B5F68 File Offset: 0x001B4F68
	public void ᜀ(byte[] A_0)
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

	// Token: 0x06003009 RID: 12297 RVA: 0x001B5FAC File Offset: 0x001B4FAC
	public int ᜃ()
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

	// Token: 0x0600300A RID: 12298 RVA: 0x001B5FF0 File Offset: 0x001B4FF0
	public void ᜀ(int A_0)
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

	// Token: 0x0600300B RID: 12299 RVA: 0x001B6034 File Offset: 0x001B5034
	public spr\u241D()
	{
	}

	// Token: 0x0600300C RID: 12300 RVA: 0x001B6054 File Offset: 0x001B5054
	public spr\u241D(Stream A_0)
	{
		this.ᜁ(A_0);
	}

	// Token: 0x0600300D RID: 12301 RVA: 0x001B607C File Offset: 0x001B507C
	public void ᜁ(Stream A_0)
	{
		int a_ = 4;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 != null)
			{
				byte[] a_2 = new byte[4];
				int num = sprṯ.ᜀ(A_0, a_2);
				this.ᜀ = new byte[num];
				A_0.Read(this.ᜀ, 0, num);
				A_0.Read(this.ᜁ, 0, this.ᜁ.Length);
				this.ᜃ = sprṯ.ᜀ(A_0, a_2);
				int num2 = (int)(A_0.Length - A_0.Position);
				this.ᜂ = new byte[num2];
				A_0.Read(this.ᜂ, 0, num2);
				return;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹䠻䰽┿⍁⥃", a_));
	}

	// Token: 0x0600300E RID: 12302 RVA: 0x001B6150 File Offset: 0x001B5150
	public void ᜀ(Stream A_0)
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
		sprṯ.ᜀ(A_0, num);
		A_0.Write(this.ᜀ, 0, num);
		A_0.Write(this.ᜁ, 0, this.ᜁ.Length);
		sprṯ.ᜀ(A_0, this.ᜃ);
		int count = this.ᜂ.Length;
		A_0.Write(this.ᜂ, 0, count);
	}

	// Token: 0x0400155D RID: 5469
	private byte[] ᜀ;

	// Token: 0x0400155E RID: 5470
	private byte[] ᜁ = new byte[16];

	// Token: 0x0400155F RID: 5471
	private byte[] ᜂ;

	// Token: 0x04001560 RID: 5472
	private int ᜃ;
}
