using System;
using System.Reflection;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Fields;

// Token: 0x02000196 RID: 406
[DefaultMember("Item")]
internal class spr\u2062 : CollectionEx
{
	// Token: 0x06000F82 RID: 3970 RVA: 0x000F2B1C File Offset: 0x000F1B1C
	internal Field ᜀ(string A_0)
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
		return this.ᜁ(A_0);
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x000F2B60 File Offset: 0x000F1B60
	internal Field ᜀ(int A_0)
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
		return base.InnerList[A_0] as Field;
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x000F2BAC File Offset: 0x000F1BAC
	internal spr\u2062(Document A_0) : base(A_0, A_0)
	{
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x000F2BC4 File Offset: 0x000F1BC4
	public Field ᜁ(string A_0)
	{
		Field field;
		for (;;)
		{
			A_0.Replace('-', '_');
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						goto IL_C6;
					}
					break;
				case 1:
					return field;
				case 2:
					goto IL_84;
				case 3:
					if (field.Value.Equals(A_0, StringComparison.CurrentCultureIgnoreCase))
					{
						num2 = 1;
						continue;
					}
					num++;
					goto IL_3B;
				case 4:
					if (num >= base.InnerList.Count)
					{
						num2 = 0;
						continue;
					}
					field = (base.InnerList[num] as Field);
					num2 = 3;
					continue;
				case 5:
					goto IL_84;
				}
				break;
				IL_3B:
				num2 = 5;
				continue;
				IL_84:
				num2 = 4;
			}
		}
		return field;
		IL_C6:
		if (false)
		{
		}
		return null;
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x000F2CA0 File Offset: 0x000F1CA0
	public void ᜁ(int A_0)
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
		Field a_ = base.InnerList[A_0] as Field;
		this.ᜀ(a_);
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x000F2CF4 File Offset: 0x000F1CF4
	public void ᜀ(Field A_0)
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
		base.InnerList.Remove(A_0);
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x000F2D3C File Offset: 0x000F1D3C
	public void ᜀ()
	{
		int num = 3;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 1:
			{
				while (base.InnerList.Count <= 0)
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
						num = 2;
						goto IL_0A;
					}
				}
				int a_ = base.InnerList.Count - 1;
				this.ᜁ(a_);
				num = 0;
				continue;
			}
			case 2:
				return;
			}
			IL_22:
			if (true)
			{
			}
			num = 1;
			continue;
			goto IL_22;
		}
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x000F2DDC File Offset: 0x000F1DDC
	internal void ᜁ(Field A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				base.InnerList.Add(A_0);
				num = 2;
				continue;
			case 2:
				goto IL_5D;
			}
			goto IL_1C;
			IL_3C:
			num = 0;
			continue;
			IL_1C:
			if (true)
			{
			}
			if (!base.InnerList.Contains(A_0))
			{
				goto IL_3C;
			}
			IL_5D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			default:
				goto IL_73;
			}
		}
		IL_73:
		if (false)
		{
		}
	}
}
