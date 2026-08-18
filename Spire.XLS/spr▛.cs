using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls.Core;

// Token: 0x02000388 RID: 904
[DefaultMember("Item")]
internal class spr\u259B<ᜀ> : List<ᜀ>, ICloneParent where ᜀ : class
{
	// Token: 0x060036D2 RID: 14034 RVA: 0x001EED18 File Offset: 0x001EDD18
	public spr\u259B()
	{
	}

	// Token: 0x060036D3 RID: 14035 RVA: 0x001EED2C File Offset: 0x001EDD2C
	public spr\u259B(ICollection<ᜀ> A_0) : base(A_0)
	{
	}

	// Token: 0x060036D4 RID: 14036 RVA: 0x001EED40 File Offset: 0x001EDD40
	public object ᜀ()
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
			switch (0)
			{
			}
			break;
		}
		spr\u259B<ᜀ> spr_u259B = new spr\u259B<ᜀ>();
		using (List<ᜀ>.Enumerator enumerator = base.GetEnumerator())
		{
			int num = 0;
			for (;;)
			{
				ᜀ ᜀ;
				switch (num)
				{
				case 1:
				{
					ᜀ ᜀ2;
					ᜀ = ᜀ2;
					goto IL_D5;
				}
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 6;
						continue;
					}
					ᜀ ᜀ2 = enumerator.Current;
					num = 8;
					continue;
				}
				case 3:
				{
					ᜀ ᜀ2;
					ᜀ = (ᜀ)((object)((ICloneable)((object)ᜀ2)).Clone());
					goto IL_D5;
				}
				case 4:
					num = 1;
					continue;
				case 5:
					goto IL_11A;
				case 6:
					num = 5;
					continue;
				case 8:
				{
					ᜀ ᜀ2;
					if (!(ᜀ2 is ICloneable))
					{
						num = 4;
						continue;
					}
					num = 3;
					continue;
				}
				}
				IL_8F:
				num = 2;
				continue;
				goto IL_8F;
				IL_D5:
				ᜀ item = ᜀ;
				spr_u259B.Add(item);
				num = 7;
			}
			IL_11A:;
		}
		return spr_u259B;
	}

	// Token: 0x060036D5 RID: 14037 RVA: 0x001EEE88 File Offset: 0x001EDE88
	public object ᜀ(object A_0)
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
			switch (0)
			{
			}
			break;
		}
		spr\u259B<ᜀ> spr_u259B = new spr\u259B<ᜀ>();
		using (List<ᜀ>.Enumerator enumerator = base.GetEnumerator())
		{
			int num = 5;
			for (;;)
			{
				ᜀ ᜀ2;
				switch (num)
				{
				case 0:
					goto IL_E5;
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_106;
				case 3:
				{
					if (!enumerator.MoveNext())
					{
						num = 1;
						continue;
					}
					ᜀ ᜀ = enumerator.Current;
					ᜀ2 = ᜀ;
					ICloneParent cloneParent = ᜀ2 as ICloneParent;
					num = 4;
					continue;
				}
				case 4:
				{
					ICloneParent cloneParent;
					if (cloneParent != null)
					{
						num = 6;
						continue;
					}
					goto IL_E5;
				}
				case 6:
				{
					ICloneParent cloneParent;
					ᜀ2 = (ᜀ)((object)cloneParent.Clone(A_0));
					num = 0;
					continue;
				}
				}
				IL_AD:
				num = 3;
				continue;
				goto IL_AD;
				IL_E5:
				spr_u259B.Add(ᜀ2);
				num = 7;
			}
			IL_106:;
		}
		return spr_u259B;
	}

	// Token: 0x060036D6 RID: 14038 RVA: 0x001EEFBC File Offset: 0x001EDFBC
	public ᜀ ᜀ(int A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_7D;
			case 2:
				if (A_0 >= 0)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_7F;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			}
			IL_20:
			if (A_0 < base.Count)
			{
				num = 3;
				continue;
			}
			goto IL_7F;
			goto IL_20;
		}
		IL_7D:
		return base[A_0];
		IL_7F:
		return default(ᜀ);
	}

	// Token: 0x060036D7 RID: 14039 RVA: 0x001EF054 File Offset: 0x001EE054
	public void ᜀ(int A_0, ᜀ A_1)
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
		this.ᜁ(A_0 + 1);
		base[A_0] = A_1;
	}

	// Token: 0x060036D8 RID: 14040 RVA: 0x001EF0A0 File Offset: 0x001EE0A0
	public void ᜁ(int A_0)
	{
		for (;;)
		{
			for (;;)
			{
				int count = base.Count;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
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
							base.AddRange(new ᜀ[A_0 - count]);
							num = 2;
							continue;
						}
						break;
					case 1:
						if (count < A_0)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						return;
					}
					break;
				}
			}
		}
	}
}
