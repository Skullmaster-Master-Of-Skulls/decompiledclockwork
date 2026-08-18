using System;
using Spire.XLS.File;

// Token: 0x02000107 RID: 263
internal class spr\u2416 : spr\u1DEE
{
	// Token: 0x060005B7 RID: 1463 RVA: 0x000374F8 File Offset: 0x000364F8
	public spr\u2416(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00037510 File Offset: 0x00036510
	protected override BiffCellType ᜂ()
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
		return BiffCellType.Numeric;
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0003754C File Offset: 0x0003654C
	protected override double ᜄ()
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
		double value = 0.0;
		byte[] bytes = BitConverter.GetBytes(value);
		Array.Copy(base.ᜢ(), 6, bytes, 0, bytes.Length);
		return BitConverter.ToDouble(bytes, 0);
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x000375B0 File Offset: 0x000365B0
	protected override void ᜀ(double A_0)
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
		byte[] bytes = BitConverter.GetBytes(A_0);
		Array.Copy(bytes, 0, base.ᜢ(), 6, bytes.Length);
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00037608 File Offset: 0x00036608
	protected override object ᜀ()
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
		return this.ᜄ();
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00037650 File Offset: 0x00036650
	protected override void ᜀ(object A_0)
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
		this.ᜀ((double)A_0);
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00037698 File Offset: 0x00036698
	protected override string ᜁ()
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
		return this.ᜄ().ToString();
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x000376E4 File Offset: 0x000366E4
	protected override void ᜀ(string A_0)
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
		double a_ = double.Parse(A_0);
		this.ᜀ(a_);
	}
}
