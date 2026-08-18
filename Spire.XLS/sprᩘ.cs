using System;
using System.Threading;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000368 RID: 872
[CLSCompliant(false)]
internal class spr\u1A58
{
	// Token: 0x0600354F RID: 13647 RVA: 0x001E7370 File Offset: 0x001E6370
	public int ᜁ()
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
		return this.ᜅ - this.ᜃ;
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x001E73B8 File Offset: 0x001E63B8
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
		return this.ᜄ;
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x001E73FC File Offset: 0x001E63FC
	public void ᜂ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x001E7440 File Offset: 0x001E6440
	public int ᜃ()
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

	// Token: 0x06003553 RID: 13651 RVA: 0x001E7484 File Offset: 0x001E6484
	public void ᜃ(int A_0)
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

	// Token: 0x06003554 RID: 13652 RVA: 0x001E74C8 File Offset: 0x001E64C8
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
		return this.ᜅ;
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x001E750C File Offset: 0x001E650C
	public TBIFFRecord ᜈ()
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
		return this.ᜆ;
	}

	// Token: 0x06003556 RID: 13654 RVA: 0x001E7550 File Offset: 0x001E6550
	public void ᜀ(TBIFFRecord A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06003557 RID: 13655 RVA: 0x001E7594 File Offset: 0x001E6594
	public TBIFFRecord ᜄ()
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
		return this.ᜇ;
	}

	// Token: 0x06003558 RID: 13656 RVA: 0x001E75D8 File Offset: 0x001E65D8
	public void ᜁ(TBIFFRecord A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x001E761C File Offset: 0x001E661C
	public virtual int ᜀ()
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
		return 8224;
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x001E765C File Offset: 0x001E665C
	public void ᜁ(EventHandler A_0)
	{
		for (;;)
		{
			EventHandler eventHandler = this.ᜉ;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7C:
				num = 1;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				EventHandler eventHandler2;
				switch (num)
				{
				case 0:
					if (eventHandler == eventHandler2)
					{
						goto IL_7C;
					}
					goto IL_53;
				case 1:
					return;
				case 2:
					goto IL_53;
				}
				break;
				IL_53:
				eventHandler2 = eventHandler;
				EventHandler value = (EventHandler)Delegate.Combine(eventHandler2, A_0);
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜉ, value, eventHandler2);
				num = 0;
			}
		}
	}

	// Token: 0x0600355B RID: 13659 RVA: 0x001E76F4 File Offset: 0x001E66F4
	public void ᜀ(EventHandler A_0)
	{
		for (;;)
		{
			EventHandler eventHandler = this.ᜉ;
			if (true)
			{
			}
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7C:
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
				EventHandler eventHandler2;
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_53;
				case 2:
					if (eventHandler == eventHandler2)
					{
						goto IL_7C;
					}
					goto IL_53;
				}
				break;
				IL_53:
				eventHandler2 = eventHandler;
				EventHandler value = (EventHandler)Delegate.Remove(eventHandler2, A_0);
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜉ, value, eventHandler2);
				num = 2;
			}
		}
	}

	// Token: 0x0600355C RID: 13660 RVA: 0x001E778C File Offset: 0x001E678C
	public spr\u1A58(spr\u2453 A_0)
	{
		int a_ = 19;
		this.ᜂ = -1;
		this.ᜆ = TBIFFRecord.Continue;
		this.ᜇ = TBIFFRecord.Continue;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㥈⩊㽌⩎㽐❒", a_));
		}
		this.ᜀ = A_0;
		this.ᜅ = this.ᜀ.MaximumRecordSize;
		this.ᜃ = this.ᜀ.Length;
		this.ᜁ = this.ᜃ;
		this.ᜄ = this.ᜃ;
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x001E781C File Offset: 0x001E681C
	public void ᜀ(byte A_0)
	{
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_71;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜂ();
					this.ᜇ();
					num = 1;
					continue;
				case 1:
					goto IL_71;
				}
				if (!this.ᜀ(1))
				{
					goto IL_73;
				}
				num = 0;
				break;
			}
		}
		IL_71:
		IL_73:
		this.ᜀ.ᜀ(this.ᜁ, A_0);
		this.ᜁ(1);
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x001E78B8 File Offset: 0x001E68B8
	public virtual int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int num;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					num = 0;
					int num2 = 8;
					for (;;)
					{
						int num3;
						int num5;
						switch (num2)
						{
						case 0:
							goto IL_172;
						case 1:
							num2 = 3;
							continue;
						case 2:
							if (true)
							{
							}
							num2 = 7;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num3 = this.ᜅ;
								goto IL_13F;
							}
							break;
						case 4:
						{
							int num4;
							num3 = num4 - num5;
							goto IL_13F;
						}
						case 5:
							goto IL_172;
						case 6:
						{
							int num4;
							if (num4 - num5 >= this.ᜅ)
							{
								num2 = 1;
								continue;
							}
							num2 = 4;
							continue;
						}
						case 7:
							goto IL_DB;
						case 8:
							if (this.ᜀ(A_2))
							{
								num2 = 10;
								continue;
							}
							this.ᜀ.ᜀ(this.ᜁ, A_0, A_1, A_2);
							this.ᜁ(A_2);
							num2 = 9;
							continue;
						case 9:
							goto IL_C2;
						case 10:
						{
							int num4 = A_1 + A_2;
							num5 = A_1;
							num2 = 5;
							continue;
						}
						case 11:
						{
							int num4;
							if (num5 >= num4)
							{
								num2 = 2;
								continue;
							}
							this.ᜂ();
							this.ᜇ();
							num++;
							num2 = 6;
							continue;
						}
						}
						break;
						IL_13F:
						int num6 = num3;
						this.ᜀ.ᜀ(this.ᜁ, A_0, num5, num6);
						this.ᜁ(num6);
						num5 += this.ᜅ;
						num2 = 0;
						continue;
						IL_172:
						num2 = 11;
					}
				}
				break;
			}
		}
		IL_C2:
		IL_DB:
		this.ᜂ();
		return num;
	}

	// Token: 0x0600355F RID: 13663 RVA: 0x001E7A64 File Offset: 0x001E6A64
	public void ᜀ(ushort A_0)
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_73;
				default:
					if (false)
					{
					}
					this.ᜂ();
					this.ᜇ();
					num = 1;
					continue;
				}
				break;
			case 1:
				goto IL_71;
			}
			if (!this.ᜀ(2))
			{
				break;
			}
			num = 0;
		}
		IL_71:
		IL_73:
		this.ᜀ.ᜀ(this.ᜁ, A_0);
		this.ᜁ(2);
	}

	// Token: 0x06003560 RID: 13664 RVA: 0x001E7B00 File Offset: 0x001E6B00
	public bool ᜀ(int A_0)
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
		return this.ᜃ + A_0 > this.ᜅ;
	}

	// Token: 0x06003561 RID: 13665 RVA: 0x001E7B4C File Offset: 0x001E6B4C
	public void ᜇ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_59;
			case 2:
				goto IL_B7;
			case 3:
				goto IL_EA;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B7;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 5:
				if (this.ᜈ != 1)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 6:
				goto IL_46;
			}
			if (this.ᜉ != null)
			{
				num = 2;
				continue;
			}
			IL_59:
			this.ᜈ++;
			this.ᜀ.ᜂ.Add(this.ᜁ);
			num = 5;
			continue;
			IL_B7:
			this.ᜉ(this, EventArgs.Empty);
			num = 1;
		}
		IL_46:
		if (true)
		{
		}
		TBIFFRecord tbiffrecord = this.ᜄ();
		goto IL_F2;
		IL_EA:
		tbiffrecord = this.ᜈ();
		IL_F2:
		TBIFFRecord tbiffrecord2 = tbiffrecord;
		this.ᜀ.ᜀ(this.ᜁ, (ushort)tbiffrecord2);
		this.ᜁ += 2;
		this.ᜂ = this.ᜁ;
		this.ᜃ = 0;
		this.ᜀ.ᜀ(this.ᜁ, (ushort)this.ᜃ);
		this.ᜁ += 2;
		this.ᜄ += 4;
		this.ᜅ = this.ᜀ();
	}

	// Token: 0x06003562 RID: 13666 RVA: 0x001E7CC0 File Offset: 0x001E6CC0
	public void ᜂ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_75;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_77;
				default:
					if (false)
					{
					}
					this.ᜀ.ᜀ(this.ᜂ, (ushort)this.ᜃ);
					num = 0;
					continue;
				}
				break;
			}
			if (this.ᜂ < 0)
			{
				break;
			}
			num = 1;
		}
		IL_75:
		IL_77:
		if (true)
		{
		}
	}

	// Token: 0x06003563 RID: 13667 RVA: 0x001E7D4C File Offset: 0x001E6D4C
	protected void ᜁ(int A_0)
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
		this.ᜁ += A_0;
		this.ᜄ += A_0;
		this.ᜃ += A_0;
	}

	// Token: 0x0400173E RID: 5950
	protected spr\u2453 ᜀ;

	// Token: 0x0400173F RID: 5951
	protected int ᜁ;

	// Token: 0x04001740 RID: 5952
	private int ᜂ;

	// Token: 0x04001741 RID: 5953
	private int ᜃ;

	// Token: 0x04001742 RID: 5954
	private int ᜄ;

	// Token: 0x04001743 RID: 5955
	protected int ᜅ;

	// Token: 0x04001744 RID: 5956
	private TBIFFRecord ᜆ;

	// Token: 0x04001745 RID: 5957
	private TBIFFRecord ᜇ;

	// Token: 0x04001746 RID: 5958
	private int ᜈ;

	// Token: 0x04001747 RID: 5959
	private EventHandler ᜉ;
}
