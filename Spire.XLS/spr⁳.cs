using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;

// Token: 0x0200024D RID: 589
internal class spr\u2073 : IColorScale, IOptimizedUpdate
{
	// Token: 0x06002392 RID: 9106 RVA: 0x0014CFB4 File Offset: 0x0014BFB4
	public IList<IColorConditionValue> ᜂ()
	{
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_75;
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
					goto IL_75;
				case 1:
					this.ᜂ = this.ᜁ.AsReadOnly();
					num = 0;
					continue;
				}
				if (this.ᜂ != null)
				{
					goto IL_77;
				}
				num = 1;
				break;
			}
		}
		IL_75:
		IL_77:
		return this.ᜂ;
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x0014D040 File Offset: 0x0014C040
	public void ᜂ(int A_0)
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
		this.ᜁ();
		this.ᜀ().ᜁ(A_0);
		this.ᜃ();
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x0014D094 File Offset: 0x0014C094
	public void ᜁ()
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
		this.ᜀ.BeginUpdate();
		this.ᜀ(this.ᜀ().ᜀ(), this);
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x0014D0EC File Offset: 0x0014C0EC
	public void ᜃ()
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
		this.ᜀ.EndUpdate();
		this.ᜀ(this.ᜀ().ᜀ(), this);
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x0014D144 File Offset: 0x0014C144
	public spr\u2073(ConditionalFormatWrapper A_0)
	{
		this.ᜀ = A_0;
		this.ᜀ(this.ᜀ().ᜀ(), this);
	}

	// Token: 0x06002397 RID: 9111 RVA: 0x0014D17C File Offset: 0x0014C17C
	private void ᜀ(IList<IColorConditionValue> A_0, IOptimizedUpdate A_1)
	{
		int count;
		int num2;
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
				{
					if (false)
					{
					}
					int num3;
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						goto IL_D8;
					case 2:
						this.ᜀ(count - num2);
						num = 1;
						continue;
					case 4:
						if (num2 > count)
						{
							num = 9;
							continue;
						}
						num = 7;
						continue;
					case 5:
						num3 = A_0.Count;
						goto IL_7C;
					case 6:
						num3 = 0;
						goto IL_7C;
					case 7:
						if (count > num2)
						{
							num = 2;
							continue;
						}
						goto IL_100;
					case 8:
						goto IL_C2;
					case 9:
						this.ᜀ(num2 - count, A_0);
						if (true)
						{
						}
						num = 8;
						continue;
					}
					if (A_0 == null)
					{
						num = 0;
						break;
					}
					num = 5;
					break;
					IL_7C:
					num2 = num3;
					count = this.ᜁ.Count;
					num = 4;
					break;
				}
				}
			}
		}
		IL_C2:
		IL_D8:
		IL_100:
		this.ᜁ(Math.Min(num2, count));
	}

	// Token: 0x06002398 RID: 9112 RVA: 0x0014D298 File Offset: 0x0014C298
	private void ᜀ(int A_0, IList<IColorConditionValue> A_1)
	{
		for (;;)
		{
			int count = this.ᜁ.Count;
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (false)
						{
						}
						goto IL_38;
					}
					break;
				case 1:
				{
					if (num >= A_0)
					{
						num2 = 2;
						continue;
					}
					spr\u24ED spr_u24ED = new sprự(A_1[num], this);
					this.ᜁ.Add(spr_u24ED as IColorConditionValue);
					num++;
					goto IL_7B;
				}
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 1;
				continue;
				IL_7B:
				num2 = 0;
			}
		}
	}

	// Token: 0x06002399 RID: 9113 RVA: 0x0014D34C File Offset: 0x0014C34C
	private void ᜁ(int A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				sprᝠ sprᝠ = this.ᜀ();
				IList<IColorConditionValue> list = sprᝠ.ᜀ();
				int num = 0;
				if (true)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= A_0)
						{
							num2 = 1;
							continue;
						}
						spr\u24ED spr_u24ED = this.ᜁ[num] as spr\u24ED;
						spr_u24ED.ᜀ(list[num]);
						num++;
						num2 = 3;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B7;
						default:
							goto IL_80;
						}
						break;
					case 2:
						goto IL_4A;
					case 3:
						goto IL_B7;
					}
					break;
					IL_4A:
					num2 = 0;
					continue;
					IL_B7:
					goto IL_4A;
				}
			}
			IL_80:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x0014D414 File Offset: 0x0014C414
	private void ᜀ(int A_0)
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
		this.ᜁ.RemoveRange(this.ᜁ.Count - A_0, A_0);
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x0014D468 File Offset: 0x0014C468
	private sprᝠ ᜀ()
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
		return this.ᜀ.ᜁ().ColorScale.Wrapped;
	}

	// Token: 0x04001239 RID: 4665
	private ConditionalFormatWrapper ᜀ;

	// Token: 0x0400123A RID: 4666
	private List<IColorConditionValue> ᜁ = new List<IColorConditionValue>();

	// Token: 0x0400123B RID: 4667
	private IList<IColorConditionValue> ᜂ;
}
