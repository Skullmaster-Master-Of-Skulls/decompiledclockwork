using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000329 RID: 809
[CLSCompliant(false)]
internal abstract class spr\u2396 : spr\u1D3B
{
	// Token: 0x060031F4 RID: 12788 RVA: 0x001CD4CC File Offset: 0x001CC4CC
	public spr\u2396(spr\u1D3B A_0) : base(A_0)
	{
	}

	// Token: 0x060031F5 RID: 12789 RVA: 0x001CD4EC File Offset: 0x001CC4EC
	public spr\u2396(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060031F6 RID: 12790 RVA: 0x001CD510 File Offset: 0x001CC510
	public spr\u2396(spr\u1D3B A_0, byte[] A_1, int A_2, spr\u24C9 A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060031F7 RID: 12791 RVA: 0x001CD534 File Offset: 0x001CC534
	private new void ᜀ(Stream A_0, int A_1)
	{
		for (;;)
		{
			spr\u1D3B item;
			int num;
			long num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7B:
				item = spr\u231F.ᜀ(this, A_0);
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num2 = A_0.Position + (long)this.m_iLength;
				if (true)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					item = spr\u231F.ᜀ(this, A_0, base.\u1716());
					num = 4;
					continue;
				case 1:
					if (num2 <= A_0.Position)
					{
						num = 5;
						continue;
					}
					num = 7;
					continue;
				case 2:
					goto IL_65;
				case 3:
					goto IL_BA;
				case 4:
					goto IL_65;
				case 5:
					return;
				case 6:
					goto IL_BA;
				case 7:
					if (base.\u1716() != null)
					{
						num = 0;
						continue;
					}
					goto IL_7B;
				}
				break;
				IL_65:
				this.ᜀ.Add(item);
				num = 6;
				continue;
				IL_BA:
				num = 1;
			}
		}
	}

	// Token: 0x060031F8 RID: 12792 RVA: 0x001CD63C File Offset: 0x001CC63C
	public new void ᜀ(spr\u1D3B A_0)
	{
		int a_ = 3;
		if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("倸伺堼刾ᕀⱂф⍆ⵈ", a_));
			}
		}
		this.ᜀ.Add(A_0);
	}

	// Token: 0x060031F9 RID: 12793 RVA: 0x001CD6A8 File Offset: 0x001CC6A8
	public new void ᜀ(ICollection<spr\u1D3B> A_0)
	{
		int a_ = 12;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⭁ぃ⍅╇㥉", a_));
			}
		}
		this.ᜀ.AddRange(A_0);
	}

	// Token: 0x060031FA RID: 12794 RVA: 0x001CD714 File Offset: 0x001CC714
	public spr\u1D3B[] ᜁ()
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
		return this.ᜀ.ToArray();
	}

	// Token: 0x060031FB RID: 12795 RVA: 0x001CD75C File Offset: 0x001CC75C
	internal new List<spr\u1D3B> ᜀ()
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

	// Token: 0x060031FC RID: 12796 RVA: 0x001CD7A0 File Offset: 0x001CC7A0
	public override void ᜀ(Stream A_0)
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
		this.ᜀ(A_0, 0);
	}

	// Token: 0x060031FD RID: 12797 RVA: 0x001CD7E4 File Offset: 0x001CC7E4
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		switch (0)
		{
		default:
		{
			long position;
			for (;;)
			{
				position = A_0.Position;
				int num = 0;
				int count = this.ᜀ.Count;
				if (true)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_59;
					case 1:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						spr\u1D3B spr_u1D3B = this.ᜀ[num];
						spr_u1D3B.ᜁ(A_0, (int)A_0.Position, A_2, A_3);
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_59;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					}
					case 2:
						goto IL_59;
					case 3:
						goto IL_6F;
					}
					break;
					IL_59:
					num2 = 1;
				}
			}
			IL_6F:
			this.m_iLength = (int)(A_0.Position - position);
			return;
		}
		}
	}

	// Token: 0x060031FE RID: 12798 RVA: 0x001CD8C0 File Offset: 0x001CC8C0
	protected override object ᜅ()
	{
		switch (0)
		{
		default:
		{
			spr\u2396 spr_u;
			for (;;)
			{
				spr_u = (spr\u2396)base.ᜅ();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 5;
							continue;
						}
						spr\u1D3B item = this.ᜀ[num2].ᜁ(spr_u);
						List<spr\u1D3B> list;
						list.Add(item);
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E7;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					case 1:
					{
						int count = this.ᜀ.Count;
						List<spr\u1D3B> list = new List<spr\u1D3B>(count);
						int num2 = 0;
						goto IL_E7;
					}
					case 2:
						if (spr_u.ᜀ != null)
						{
							num = 1;
							continue;
						}
						return spr_u;
					case 3:
						goto IL_5E;
					case 4:
						goto IL_5E;
					case 5:
					{
						List<spr\u1D3B> list;
						spr_u.ᜀ = list;
						num = 6;
						continue;
					}
					case 6:
						return spr_u;
					}
					break;
					IL_5E:
					num = 0;
					continue;
					IL_E7:
					num = 4;
				}
			}
			return spr_u;
		}
		}
	}

	// Token: 0x040015F0 RID: 5616
	private new List<spr\u1D3B> ᜀ = new List<spr\u1D3B>();
}
