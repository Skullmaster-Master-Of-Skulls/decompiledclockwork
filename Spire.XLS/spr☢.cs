using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200031A RID: 794
internal class spr\u2622 : CollectionExtended<spr\u1F7E>
{
	// Token: 0x06003121 RID: 12577 RVA: 0x001C68D0 File Offset: 0x001C58D0
	public spr\u2622(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003122 RID: 12578 RVA: 0x001C68F0 File Offset: 0x001C58F0
	public new spr\u1F7E ᜀ(spr\u1F7E A_0)
	{
		int a_ = 8;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				spr\u1F7E spr_u1F7E;
				if (this.ᜀ.TryGetValue(A_0.ᜁ(), out spr_u1F7E))
				{
					num = 3;
					continue;
				}
				base.Add(A_0);
				IgnoreErrorType key;
				this.ᜀ.Add(key, A_0);
				num = 7;
				continue;
			}
			case 1:
				return A_0;
			case 2:
				goto IL_46;
			case 3:
			{
				spr\u1F7E spr_u1F7E;
				spr_u1F7E.ᜀ(A_0);
				num = 2;
				continue;
			}
			case 4:
			{
				if (true)
				{
				}
				spr\u1F7E spr_u1F7E;
				if (spr_u1F7E == null)
				{
					num = 1;
					continue;
				}
				return spr_u1F7E;
			}
			case 5:
				goto IL_44;
			case 7:
				goto IL_46;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_44;
			default:
			{
				if (false)
				{
				}
				IgnoreErrorType key = A_0.ᜁ();
				this.ᜁ(A_0.ᜂ().ToArray());
				num = 0;
				continue;
			}
			}
			IL_46:
			num = 4;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬽㈿ぁ⭃㑅Ň⑉⡋❍㍏㍑⁓㥕⩗", a_));
	}

	// Token: 0x06003123 RID: 12579 RVA: 0x001C6A20 File Offset: 0x001C5A20
	public new spr\u1F7E ᜀ(Rectangle[] A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					goto IL_A7;
				case 1:
					goto IL_A5;
				case 2:
					goto IL_54;
				case 3:
				{
					spr\u1F7E spr_u1F7E;
					return spr_u1F7E;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
					{
						if (false)
						{
						}
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						spr\u1F7E spr_u1F7E = base[num2];
						num = 6;
						continue;
					}
					}
					break;
				case 6:
				{
					if (true)
					{
					}
					spr\u1F7E spr_u1F7E;
					if (spr_u1F7E.ᜀ(A_0, 0))
					{
						goto IL_7E;
					}
					int num2;
					num2++;
					num = 0;
					continue;
				}
				case 7:
					goto IL_A7;
				case 8:
				{
					if (num3 == 0)
					{
						num = 1;
						continue;
					}
					int num2 = 0;
					int count = base.Count;
					num = 7;
					continue;
				}
				case 9:
					goto IL_EC;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num3 = A_0.Length;
				num = 8;
				continue;
				IL_7E:
				num = 3;
				continue;
				IL_A7:
				num = 4;
			}
			IL_54:
			return null;
			IL_A5:
			return null;
			IL_EC:
			return null;
		}
		}
	}

	// Token: 0x06003124 RID: 12580 RVA: 0x001C6B4C File Offset: 0x001C5B4C
	public new void ᜁ(Rectangle[] A_0)
	{
		using (Dictionary<IgnoreErrorType, spr\u1F7E>.Enumerator enumerator = this.ᜀ.GetEnumerator())
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_8E;
						}
					}
					IL_8E:
					if (false)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_9C;
				case 3:
				{
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					KeyValuePair<IgnoreErrorType, spr\u1F7E> keyValuePair = enumerator.Current;
					keyValuePair.Value.ᜀ(A_0);
					num = 2;
					continue;
				}
				}
				IL_5D:
				num = 3;
				continue;
				goto IL_5D;
			}
			IL_9C:;
		}
		if (true)
		{
		}
	}

	// Token: 0x06003125 RID: 12581 RVA: 0x001C6C20 File Offset: 0x001C5C20
	public virtual object ᜀ(object A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u2622 spr_u = (spr\u2622)base.Clone(A_0);
			Dictionary<IgnoreErrorType, spr\u1F7E> dictionary = new Dictionary<IgnoreErrorType, spr\u1F7E>();
			using (Dictionary<IgnoreErrorType, spr\u1F7E>.Enumerator enumerator = this.ᜀ.GetEnumerator())
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						KeyValuePair<IgnoreErrorType, spr\u1F7E> keyValuePair = enumerator.Current;
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						num = 0;
						continue;
					}
					case 3:
						goto IL_C3;
					case 4:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_B1;
							}
						}
						IL_B1:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					IL_7E:
					num = 1;
					continue;
					goto IL_7E;
				}
				IL_C3:;
			}
			if (true)
			{
			}
			spr_u.ᜀ = dictionary;
			return spr_u;
		}
		}
	}

	// Token: 0x040015B1 RID: 5553
	private new Dictionary<IgnoreErrorType, spr\u1F7E> ᜀ = new Dictionary<IgnoreErrorType, spr\u1F7E>();
}
