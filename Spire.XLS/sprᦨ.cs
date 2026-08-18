using System;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200043B RID: 1083
internal class sprᦨ : ICloneable
{
	// Token: 0x06004132 RID: 16690 RVA: 0x0024816C File Offset: 0x0024716C
	private sprᦨ()
	{
	}

	// Token: 0x06004133 RID: 16691 RVA: 0x00248180 File Offset: 0x00247180
	public sprᦨ(string A_0, string A_1)
	{
		int a_ = 14;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("ぃ❅㩇ⵉ⥋㩍", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("ぃ㽅㡇⽉", a_));
		}
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x06004134 RID: 16692 RVA: 0x002481DC File Offset: 0x002471DC
	public sprᦨ(string A_0, string A_1, bool A_2) : this(A_0, A_1)
	{
		this.ᜂ = A_2;
	}

	// Token: 0x06004135 RID: 16693 RVA: 0x002481F8 File Offset: 0x002471F8
	public string ᜂ()
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

	// Token: 0x06004136 RID: 16694 RVA: 0x0024823C File Offset: 0x0024723C
	public string ᜃ()
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

	// Token: 0x06004137 RID: 16695 RVA: 0x00248280 File Offset: 0x00247280
	public bool ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x06004138 RID: 16696 RVA: 0x002482C4 File Offset: 0x002472C4
	public object ᜁ()
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
		return base.MemberwiseClone();
	}

	// Token: 0x04001D04 RID: 7428
	private string ᜀ;

	// Token: 0x04001D05 RID: 7429
	private string ᜁ;

	// Token: 0x04001D06 RID: 7430
	private bool ᜂ;
}
