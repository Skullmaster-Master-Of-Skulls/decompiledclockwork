using System;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;

// Token: 0x0200043C RID: 1084
internal class sprᡦ : IBorder, ICloneable
{
	// Token: 0x0600413A RID: 16698 RVA: 0x00248328 File Offset: 0x00247328
	public ExcelColors ᜃ()
	{
		if (this.ᜀ.ColorType != ColorType.Known)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_37;
				}
			}
			IL_37:
			if (false)
			{
			}
			return ExcelColors.Black;
		}
		return (ExcelColors)this.ᜀ.Value;
	}

	// Token: 0x0600413B RID: 16699 RVA: 0x00248380 File Offset: 0x00247380
	public void ᜀ(ExcelColors A_0)
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
		this.ᜀ.SetKnownColor(A_0);
	}

	// Token: 0x0600413C RID: 16700 RVA: 0x002483C8 File Offset: 0x002473C8
	public OColor ᜅ()
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

	// Token: 0x0600413D RID: 16701 RVA: 0x0024840C File Offset: 0x0024740C
	public Color ᜆ()
	{
		if (this.ᜀ.ColorType != ColorType.RGB)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_30;
				}
			}
			IL_30:
			if (false)
			{
			}
			if (true)
			{
			}
			return spr\u1D39.ᜂ;
		}
		return spr\u1D39.ᜀ(this.ᜀ.Value);
	}

	// Token: 0x0600413E RID: 16702 RVA: 0x00248470 File Offset: 0x00247470
	public void ᜀ(Color A_0)
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
		this.ᜀ.ᜀ(A_0);
	}

	// Token: 0x0600413F RID: 16703 RVA: 0x002484B8 File Offset: 0x002474B8
	public LineStyleType ᜂ()
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

	// Token: 0x06004140 RID: 16704 RVA: 0x002484FC File Offset: 0x002474FC
	public void ᜀ(LineStyleType A_0)
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

	// Token: 0x06004141 RID: 16705 RVA: 0x00248540 File Offset: 0x00247540
	public bool ᜁ()
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

	// Token: 0x06004142 RID: 16706 RVA: 0x00248584 File Offset: 0x00247584
	public void ᜀ(bool A_0)
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

	// Token: 0x06004143 RID: 16707 RVA: 0x002485C8 File Offset: 0x002475C8
	public spr\u1DF5 ᜀ()
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
		throw new NotImplementedException();
	}

	// Token: 0x06004144 RID: 16708 RVA: 0x00248608 File Offset: 0x00247608
	public object ᜇ()
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
		throw new NotImplementedException();
	}

	// Token: 0x06004145 RID: 16709 RVA: 0x00248648 File Offset: 0x00247648
	public object ᜄ()
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

	// Token: 0x04001D07 RID: 7431
	private OColor ᜀ = new OColor(ExcelColors.Black);

	// Token: 0x04001D08 RID: 7432
	private LineStyleType ᜁ;

	// Token: 0x04001D09 RID: 7433
	private bool ᜂ;
}
