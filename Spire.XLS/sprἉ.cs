using System;
using System.Threading;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000258 RID: 600
internal class sprἉ : XlsObject, spr\u1AE6, ICloneParent
{
	// Token: 0x060023E8 RID: 9192 RVA: 0x0014ED2C File Offset: 0x0014DD2C
	internal sprἉ(spr\u1DF5 A_0, object A_1, spr\u2141 A_2, int A_3) : base(A_0, A_1)
	{
		this.ᜀ = A_2;
		this.ᜁ = A_3;
		this.ᜀ();
	}

	// Token: 0x060023E9 RID: 9193 RVA: 0x0014ED58 File Offset: 0x0014DD58
	private void ᜀ()
	{
		int a_ = 1;
		for (;;)
		{
			this.ᜂ = (base.FindParent(typeof(XlsExternWorkbook)) as XlsExternWorkbook);
			if (this.ᜂ == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_6A;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("父䄸伺堼䴾⽀捂㉄⡆㭈⁊⽌⁎㹐㡒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴", a_));
		IL_6A:
		if (false)
		{
		}
	}

	// Token: 0x060023EA RID: 9194 RVA: 0x0014EDD8 File Offset: 0x0014DDD8
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
		return this.ᜁ;
	}

	// Token: 0x060023EB RID: 9195 RVA: 0x0014EE1C File Offset: 0x0014DE1C
	public void ᜀ(int A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				int oldIndex = this.ᜁ;
				this.ᜁ = A_0;
				NameIndexChangedEventArgs a_ = new NameIndexChangedEventArgs(oldIndex, this.ᜁ);
				this.ᜀ(a_);
				if (true)
				{
				}
				num = 2;
				continue;
			}
			case 2:
				return;
			}
			IL_1C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1C;
			default:
				if (false)
				{
				}
				if (A_0 == this.ᜁ)
				{
					return;
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x060023EC RID: 9196 RVA: 0x0014EEB4 File Offset: 0x0014DEB4
	public string ᜃ()
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
		return this.ᜀ.ᜌ();
	}

	// Token: 0x060023ED RID: 9197 RVA: 0x0014EEFC File Offset: 0x0014DEFC
	public int ᜂ()
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
		return this.ᜂ.Index;
	}

	// Token: 0x060023EE RID: 9198 RVA: 0x0014EF44 File Offset: 0x0014DF44
	internal spr\u2141 ᜄ()
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

	// Token: 0x060023EF RID: 9199 RVA: 0x0014EF88 File Offset: 0x0014DF88
	private void ᜀ(NameIndexChangedEventArgs A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				this.ᜃ(this, A_0);
				num = 0;
				continue;
			}
			IL_1C:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1C;
			default:
				if (false)
				{
				}
				if (this.ᜃ == null)
				{
					return;
				}
				num = 2;
				break;
			}
		}
	}

	// Token: 0x060023F0 RID: 9200 RVA: 0x0014F008 File Offset: 0x0014E008
	internal void ᜀ(RecordArrayList A_0)
	{
		int a_ = 10;
		while (A_0 != null)
		{
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
				A_0.ᜀ(this.ᜀ);
				return;
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁❃⥅㩇⹉㽋", a_));
	}

	// Token: 0x060023F1 RID: 9201 RVA: 0x0014F074 File Offset: 0x0014E074
	public object ᜀ(object A_0)
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
		sprἉ sprἉ = (sprἉ)base.MemberwiseClone();
		sprἉ.SetParent(A_0);
		sprἉ.ᜀ();
		this.ᜀ = (spr\u2141)spr\u1CD3.ᜀ(this.ᜀ);
		return sprἉ;
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x0014F0E0 File Offset: 0x0014E0E0
	public void ᜁ(XlsName.NameIndexChangedEventHandler A_0)
	{
		for (;;)
		{
			IL_3A:
			XlsName.NameIndexChangedEventHandler nameIndexChangedEventHandler = this.ᜃ;
			int num = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					XlsName.NameIndexChangedEventHandler nameIndexChangedEventHandler2;
					switch (num)
					{
					case 0:
						if (nameIndexChangedEventHandler == nameIndexChangedEventHandler2)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_4B;
					case 1:
						return;
					case 2:
						goto IL_4B;
					}
					goto IL_3A;
					IL_4B:
					nameIndexChangedEventHandler2 = nameIndexChangedEventHandler;
					XlsName.NameIndexChangedEventHandler value = (XlsName.NameIndexChangedEventHandler)Delegate.Combine(nameIndexChangedEventHandler2, A_0);
					nameIndexChangedEventHandler = Interlocked.CompareExchange<XlsName.NameIndexChangedEventHandler>(ref this.ᜃ, value, nameIndexChangedEventHandler2);
					num = 0;
					break;
				}
				}
			}
		}
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x0014F178 File Offset: 0x0014E178
	public void ᜀ(XlsName.NameIndexChangedEventHandler A_0)
	{
		for (;;)
		{
			IL_42:
			XlsName.NameIndexChangedEventHandler nameIndexChangedEventHandler = this.ᜃ;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					XlsName.NameIndexChangedEventHandler nameIndexChangedEventHandler2;
					switch (num)
					{
					case 0:
						goto IL_53;
					case 1:
						if (nameIndexChangedEventHandler == nameIndexChangedEventHandler2)
						{
							num = 2;
							continue;
						}
						goto IL_53;
					case 2:
						return;
					}
					goto IL_42;
					IL_53:
					nameIndexChangedEventHandler2 = nameIndexChangedEventHandler;
					XlsName.NameIndexChangedEventHandler value = (XlsName.NameIndexChangedEventHandler)Delegate.Remove(nameIndexChangedEventHandler2, A_0);
					nameIndexChangedEventHandler = Interlocked.CompareExchange<XlsName.NameIndexChangedEventHandler>(ref this.ᜃ, value, nameIndexChangedEventHandler2);
					num = 1;
					break;
				}
				}
			}
		}
	}

	// Token: 0x04001261 RID: 4705
	private spr\u2141 ᜀ;

	// Token: 0x04001262 RID: 4706
	private int ᜁ;

	// Token: 0x04001263 RID: 4707
	private XlsExternWorkbook ᜂ;

	// Token: 0x04001264 RID: 4708
	private XlsName.NameIndexChangedEventHandler ᜃ;
}
