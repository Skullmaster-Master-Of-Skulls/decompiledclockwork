using System;
using System.Drawing;
using System.Threading;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x02000589 RID: 1417
internal class sprឰ : CommonWrapper, IInterior, IOptimizedUpdate
{
	// Token: 0x06005596 RID: 21910 RVA: 0x0036B1B8 File Offset: 0x0036A1B8
	public sprឰ()
	{
	}

	// Token: 0x06005597 RID: 21911 RVA: 0x0036B1CC File Offset: 0x0036A1CC
	public sprឰ(spr\u192F A_0)
	{
		int a_ = 15;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("⍄⡆㭈♊ⱌ㭎", a_));
		}
		this.ᜀ = A_0;
		if (A_0.ᜤ() == ExcelPatternType.Gradient)
		{
			this.ᜀ();
		}
	}

	// Token: 0x06005598 RID: 21912 RVA: 0x0036B220 File Offset: 0x0036A220
	public ExcelColors ᜇ()
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
		return this.ᜀ.ᝆ();
	}

	// Token: 0x06005599 RID: 21913 RVA: 0x0036B268 File Offset: 0x0036A268
	public void ᜁ(ExcelColors A_0)
	{
		for (;;)
		{
			this.BeginUpdate();
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ != null)
					{
						num = 1;
						continue;
					}
					goto IL_73;
				case 1:
					for (;;)
					{
						this.ᜀ(ExcelPatternType.Solid);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_63;
						}
					}
					IL_63:
					if (false)
					{
					}
					num = 2;
					continue;
				case 2:
					goto IL_71;
				}
				break;
			}
		}
		IL_71:
		IL_73:
		this.ᜀ.ᜁ(A_0);
		this.EndUpdate();
	}

	// Token: 0x0600559A RID: 21914 RVA: 0x0036B2FC File Offset: 0x0036A2FC
	public Color ᜂ()
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
		return this.ᜀ.\u1732();
	}

	// Token: 0x0600559B RID: 21915 RVA: 0x0036B344 File Offset: 0x0036A344
	public void ᜁ(Color A_0)
	{
		for (;;)
		{
			this.BeginUpdate();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					for (;;)
					{
						this.ᜀ(ExcelPatternType.Solid);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_5B;
						}
					}
					IL_5B:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					if (this.ᜁ != null)
					{
						num = 1;
						continue;
					}
					goto IL_73;
				}
				break;
			}
		}
		IL_71:
		IL_73:
		this.ᜀ.ᜂ(A_0);
		this.EndUpdate();
	}

	// Token: 0x0600559C RID: 21916 RVA: 0x0036B3D8 File Offset: 0x0036A3D8
	public ExcelColors ᜄ()
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
		return this.ᜀ.ᜩ();
	}

	// Token: 0x0600559D RID: 21917 RVA: 0x0036B420 File Offset: 0x0036A420
	public void ᜀ(ExcelColors A_0)
	{
		for (;;)
		{
			this.BeginUpdate();
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					if (this.ᜁ != null)
					{
						num = 2;
						continue;
					}
					goto IL_73;
				case 2:
					for (;;)
					{
						this.ᜀ(ExcelPatternType.Solid);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_63;
						}
					}
					IL_63:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_71:
		IL_73:
		this.ᜀ.ᜂ(A_0);
		this.EndUpdate();
	}

	// Token: 0x0600559E RID: 21918 RVA: 0x0036B4B4 File Offset: 0x0036A4B4
	public Color ᜈ()
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
		return this.ᜀ.ᜰ();
	}

	// Token: 0x0600559F RID: 21919 RVA: 0x0036B4FC File Offset: 0x0036A4FC
	public void ᜀ(Color A_0)
	{
		for (;;)
		{
			this.BeginUpdate();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ != null)
					{
						num = 1;
						continue;
					}
					goto IL_73;
				case 1:
					for (;;)
					{
						this.ᜀ(ExcelPatternType.Solid);
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_63;
						}
					}
					IL_63:
					if (false)
					{
					}
					num = 2;
					continue;
				case 2:
					goto IL_71;
				}
				break;
			}
		}
		IL_71:
		IL_73:
		this.ᜀ.ᜃ(A_0);
		this.EndUpdate();
	}

	// Token: 0x060055A0 RID: 21920 RVA: 0x0036B590 File Offset: 0x0036A590
	public ExcelGradient ᜁ()
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
		return new ExcelGradient(this.ᜁ);
	}

	// Token: 0x060055A1 RID: 21921 RVA: 0x0036B5D8 File Offset: 0x0036A5D8
	public ExcelPatternType ᜅ()
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
		return this.ᜀ.ᜤ();
	}

	// Token: 0x060055A2 RID: 21922 RVA: 0x0036B620 File Offset: 0x0036A620
	public void ᜀ(ExcelPatternType A_0)
	{
		int a_ = 18;
		for (;;)
		{
			IL_39:
			XlsWorkbook xlsWorkbook = this.ᜀ.ᜎ();
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_69:
				if (xlsWorkbook.Version != ExcelVersion.Version97to2003)
				{
					goto IL_8E;
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				IL_0B:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0 == ExcelPatternType.Gradient)
					{
						num = 1;
						continue;
					}
					this.ᜁ = null;
					this.ᜀ.ᜀ(null);
					num = 4;
					continue;
				case 1:
					this.ᜀ();
					num = 6;
					continue;
				case 2:
					num = 5;
					continue;
				case 3:
					goto IL_69;
				case 4:
					goto IL_108;
				case 5:
					if (A_0 == ExcelPatternType.Gradient)
					{
						num = 7;
						continue;
					}
					goto IL_8E;
				case 6:
					goto IL_89;
				case 7:
					goto IL_E8;
				}
				goto IL_39;
			}
			IL_8E:
			this.BeginUpdate();
			this.ᜀ.ᜀ(A_0);
			num = 0;
			goto IL_0B;
		}
		IL_89:
		goto IL_11E;
		IL_E8:
		throw new ArgumentException(RecordTableEnumerator.b("ṇ⽉㹋㵍㥏㵑㩓潕潗⹙㍛汝偟剡坣䙥ṧཀྵṫᵭ᥯ᵱᩳ噵ᱷᕹ᥻ൽꁿꢇ黎曆ﺍ﶑뢗ﶙﾝ쒟쮡솣좥\udca7誩쪫잭\udcaf\udeb1钳습솷쪹\ud9bb邽", a_));
		IL_108:
		IL_11E:
		this.EndUpdate();
	}

	// Token: 0x060055A3 RID: 21923 RVA: 0x0036B754 File Offset: 0x0036A754
	public void ᜀ(EventHandler A_0)
	{
		for (;;)
		{
			EventHandler eventHandler = this.ᜂ;
			int num = 0;
			for (;;)
			{
				EventHandler eventHandler2;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_37;
				case 1:
					return;
				case 2:
					if (eventHandler != eventHandler2)
					{
						goto IL_37;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
				IL_54:
				num = 2;
				continue;
				IL_37:
				eventHandler2 = eventHandler;
				EventHandler value = (EventHandler)Delegate.Combine(eventHandler2, A_0);
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜂ, value, eventHandler2);
				goto IL_54;
			}
		}
	}

	// Token: 0x060055A4 RID: 21924 RVA: 0x0036B7EC File Offset: 0x0036A7EC
	public void ᜁ(EventHandler A_0)
	{
		for (;;)
		{
			EventHandler eventHandler = this.ᜂ;
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				EventHandler eventHandler2;
				switch (num)
				{
				case 0:
					if (eventHandler != eventHandler2)
					{
						goto IL_37;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				case 2:
					goto IL_37;
				}
				break;
				IL_54:
				num = 0;
				continue;
				IL_37:
				eventHandler2 = eventHandler;
				EventHandler value = (EventHandler)Delegate.Remove(eventHandler2, A_0);
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜂ, value, eventHandler2);
				goto IL_54;
			}
		}
	}

	// Token: 0x060055A5 RID: 21925 RVA: 0x0036B884 File Offset: 0x0036A884
	private void ᜀ(object A_0, EventArgs A_1)
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
		this.BeginUpdate();
		this.ᜀ.ᜀ(this.ᜁ.ᜂ());
		this.EndUpdate();
	}

	// Token: 0x060055A6 RID: 21926 RVA: 0x0036B8E4 File Offset: 0x0036A8E4
	private void ᜀ()
	{
		XlsShapeFill xlsShapeFill;
		for (;;)
		{
			XlsWorkbook xlsWorkbook = this.ᜀ.ᜎ();
			xlsShapeFill = (XlsShapeFill)this.ᜀ.ᝐ();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						if (true)
						{
						}
						xlsShapeFill = new XlsShapeFill(xlsWorkbook.AppImplementation, this.ᜀ);
						xlsShapeFill.FillType = ShapeFillType.Gradient;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_87;
						}
					}
					IL_87:
					if (false)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_98;
				case 2:
					if (xlsShapeFill == null)
					{
						num = 0;
						continue;
					}
					goto IL_9A;
				}
				break;
			}
		}
		IL_98:
		IL_9A:
		this.ᜁ = new sprᩐ(xlsShapeFill);
		this.ᜁ.ᜀ(new EventHandler(this.ᜀ));
		this.BeginUpdate();
		this.ᜀ.ᜀ(this.ᜁ.ᜂ());
		this.EndUpdate();
	}

	// Token: 0x060055A7 RID: 21927 RVA: 0x0036B9D0 File Offset: 0x0036A9D0
	public spr\u192F ᜉ()
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
		return this.ᜀ;
	}

	// Token: 0x060055A8 RID: 21928 RVA: 0x0036BA14 File Offset: 0x0036AA14
	public virtual void ᜆ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7A;
			case 2:
				for (;;)
				{
					this.ᜀ = (spr\u192F)this.ᜉ().\u1758();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_5A;
					}
				}
				IL_5A:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 0;
				continue;
			}
			if (base.BeginCallsCount != 0)
			{
				break;
			}
			num = 2;
		}
		IL_7A:
		base.BeginUpdate();
	}

	// Token: 0x060055A9 RID: 21929 RVA: 0x0036BAA4 File Offset: 0x0036AAA4
	public virtual void ᜃ()
	{
		for (;;)
		{
			base.EndUpdate();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜂ != null)
					{
						goto IL_A3;
					}
					return;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A3;
					default:
					{
						if (false)
						{
						}
						XlsWorkbook xlsWorkbook = this.ᜀ.ᜎ();
						xlsWorkbook.SetChanged();
						num = 0;
						continue;
					}
					}
					break;
				case 2:
					return;
				case 3:
					if (base.BeginCallsCount == 0)
					{
						num = 1;
						continue;
					}
					return;
				case 4:
					this.ᜂ(this, EventArgs.Empty);
					num = 2;
					continue;
				}
				break;
				IL_A3:
				num = 4;
			}
		}
	}

	// Token: 0x04002914 RID: 10516
	private spr\u192F ᜀ;

	// Token: 0x04002915 RID: 10517
	private sprᩐ ᜁ;

	// Token: 0x04002916 RID: 10518
	private EventHandler ᜂ;
}
