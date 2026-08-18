using System;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200051E RID: 1310
internal class spr\u230D : spr\u1772
{
	// Token: 0x06004F6A RID: 20330 RVA: 0x0030132C File Offset: 0x0030032C
	public spr\u230D(XlsChartSerieDataFormat A_0)
	{
		int a_ = 18;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱇ⭉㡋⽍ᙏ㵑♓㭕㥗⹙", a_));
		}
		this.ᜀ = A_0;
	}

	// Token: 0x06004F6B RID: 20331 RVA: 0x00301368 File Offset: 0x00300368
	public bool ᜄ()
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
		return this.ᜀ.IsBorderSupported;
	}

	// Token: 0x06004F6C RID: 20332 RVA: 0x003013B0 File Offset: 0x003003B0
	public XlsChartBorder ᜃ()
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
		this.ᜀ.HasLineProperties = true;
		return this.ᜀ.LineProperties;
	}

	// Token: 0x06004F6D RID: 20333 RVA: 0x00301404 File Offset: 0x00300404
	public XlsChartInterior ᜁ()
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
		this.ᜀ.HasInterior = true;
		return this.ᜀ.Interior as XlsChartInterior;
	}

	// Token: 0x06004F6E RID: 20334 RVA: 0x0030145C File Offset: 0x0030045C
	public spr\u1C26 ᜂ()
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
		this.ᜀ.HasInterior = true;
		return this.ᜀ.Fill as spr\u1C26;
	}

	// Token: 0x06004F6F RID: 20335 RVA: 0x003014B4 File Offset: 0x003004B4
	public ChartShadow ᜀ()
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
		return this.ᜀ.Shadow;
	}

	// Token: 0x06004F70 RID: 20336 RVA: 0x003014FC File Offset: 0x003004FC
	public Format3D ᜅ()
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
		return this.ᜀ.Format3D;
	}

	// Token: 0x040023F6 RID: 9206
	private XlsChartSerieDataFormat ᜀ;
}
