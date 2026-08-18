using System;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;

// Token: 0x0200046F RID: 1135
internal class spr\u2181 : IDataBar, IOptimizedUpdate
{
	// Token: 0x06004580 RID: 17792 RVA: 0x002A6B5C File Offset: 0x002A5B5C
	public IConditionValue ᜀ()
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

	// Token: 0x06004581 RID: 17793 RVA: 0x002A6BA0 File Offset: 0x002A5BA0
	public IConditionValue ᜁ()
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

	// Token: 0x06004582 RID: 17794 RVA: 0x002A6BE4 File Offset: 0x002A5BE4
	public Color ᜄ()
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
		return this.ᜀ.ᜄ();
	}

	// Token: 0x06004583 RID: 17795 RVA: 0x002A6C2C File Offset: 0x002A5C2C
	public void ᜀ(Color A_0)
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
		this.ᜇ();
		this.ᜀ.ᜀ(A_0);
		this.ᜂ();
	}

	// Token: 0x06004584 RID: 17796 RVA: 0x002A6C80 File Offset: 0x002A5C80
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
		return this.ᜀ.ᜆ();
	}

	// Token: 0x06004585 RID: 17797 RVA: 0x002A6CC8 File Offset: 0x002A5CC8
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
		this.ᜇ();
		this.ᜀ.ᜁ(A_0);
		this.ᜂ();
	}

	// Token: 0x06004586 RID: 17798 RVA: 0x002A6D1C File Offset: 0x002A5D1C
	public int ᜆ()
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
		return this.ᜀ.ᜇ();
	}

	// Token: 0x06004587 RID: 17799 RVA: 0x002A6D64 File Offset: 0x002A5D64
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
		this.ᜇ();
		this.ᜀ.ᜀ(A_0);
		this.ᜂ();
	}

	// Token: 0x06004588 RID: 17800 RVA: 0x002A6DB8 File Offset: 0x002A5DB8
	public bool ᜃ()
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
		return this.ᜀ.ᜃ();
	}

	// Token: 0x06004589 RID: 17801 RVA: 0x002A6E00 File Offset: 0x002A5E00
	public void ᜀ(bool A_0)
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
		this.ᜇ();
		this.ᜀ.ᜀ(A_0);
		this.ᜂ();
	}

	// Token: 0x0600458A RID: 17802 RVA: 0x002A6E54 File Offset: 0x002A5E54
	public void ᜇ()
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
		this.ᜁ.BeginUpdate();
		this.ᜀ = (this.ᜁ.ᜁ().DataBar.Wrapped as spr\u24CD);
	}

	// Token: 0x0600458B RID: 17803 RVA: 0x002A6EBC File Offset: 0x002A5EBC
	public void ᜂ()
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
		this.ᜁ.EndUpdate();
	}

	// Token: 0x0600458C RID: 17804 RVA: 0x002A6F04 File Offset: 0x002A5F04
	public spr\u2181(spr\u24CD A_0, ConditionalFormatWrapper A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = new spr\u24ED(A_0.ᜀ(), A_1);
		this.ᜃ = new spr\u24ED(A_0.ᜁ(), A_1);
	}

	// Token: 0x04001FBC RID: 8124
	private spr\u24CD ᜀ;

	// Token: 0x04001FBD RID: 8125
	private ConditionalFormatWrapper ᜁ;

	// Token: 0x04001FBE RID: 8126
	private spr\u24ED ᜂ;

	// Token: 0x04001FBF RID: 8127
	private spr\u24ED ᜃ;
}
