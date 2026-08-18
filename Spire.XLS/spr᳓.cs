using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003A0 RID: 928
internal sealed class spr\u1CD3
{
	// Token: 0x06003870 RID: 14448 RVA: 0x001F7F14 File Offset: 0x001F6F14
	public static int[] ᜀ(int[] A_0)
	{
		int num = 3;
		for (;;)
		{
			int[] array;
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_93;
			case 1:
				goto IL_57;
			case 2:
				goto IL_93;
			case 4:
				return array;
			case 5:
				if (num2 >= num3)
				{
					num = 4;
					continue;
				}
				array[num2] = A_0[num2];
				num2++;
				num = 2;
				continue;
			}
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
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				break;
			}
			num3 = A_0.Length;
			array = new int[num3];
			num2 = 0;
			num = 0;
			continue;
			IL_93:
			num = 5;
		}
		IL_57:
		return null;
	}

	// Token: 0x06003871 RID: 14449 RVA: 0x001F7FD4 File Offset: 0x001F6FD4
	[CLSCompliant(false)]
	public static ushort[] ᜀ(ushort[] A_0)
	{
		int num = 0;
		for (;;)
		{
			int num2;
			int num3;
			ushort[] array;
			switch (num)
			{
			case 1:
				if (num2 >= num3)
				{
					num = 2;
					continue;
				}
				array[num2] = A_0[num2];
				num2++;
				num = 4;
				continue;
			case 2:
				return array;
			case 3:
				goto IL_57;
			case 4:
				goto IL_93;
			case 5:
				goto IL_93;
			}
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
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				break;
			}
			num3 = A_0.Length;
			array = new ushort[num3];
			num2 = 0;
			num = 5;
			continue;
			IL_93:
			num = 1;
		}
		IL_57:
		return null;
	}

	// Token: 0x06003872 RID: 14450 RVA: 0x001F8094 File Offset: 0x001F7094
	public static string[] ᜀ(string[] A_0)
	{
		int num = 0;
		for (;;)
		{
			string[] array;
			int num2;
			int num3;
			switch (num)
			{
			case 1:
				goto IL_88;
			case 2:
				goto IL_4F;
			case 3:
				return array;
			case 4:
				if (true)
				{
				}
				if (num2 >= num3)
				{
					num = 3;
					continue;
				}
				array[num2] = A_0[num2];
				num2++;
				num = 5;
				continue;
			case 5:
				goto IL_88;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				break;
			}
			num3 = A_0.Length;
			array = new string[num3];
			num2 = 0;
			num = 1;
			continue;
			IL_88:
			num = 4;
		}
		IL_4F:
		return null;
	}

	// Token: 0x06003873 RID: 14451 RVA: 0x001F8150 File Offset: 0x001F7150
	public static object[] ᜀ(object[] A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				int num2;
				int num3;
				object obj;
				object[] array;
				switch (num)
				{
				case 0:
				{
					if (num2 >= num3)
					{
						num = 7;
						continue;
					}
					obj = A_0[num2];
					ICloneable cloneable = obj as ICloneable;
					num = 6;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						ICloneable cloneable;
						obj = cloneable.Clone();
						break;
					}
					}
					num = 3;
					continue;
				case 2:
					goto IL_95;
				case 3:
					goto IL_52;
				case 4:
					goto IL_95;
				case 5:
					goto IL_50;
				case 6:
				{
					ICloneable cloneable;
					if (cloneable != null)
					{
						num = 1;
						continue;
					}
					goto IL_52;
				}
				case 7:
					return array;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num3 = A_0.Length;
				array = new object[num3];
				num2 = 0;
				num = 4;
				continue;
				IL_52:
				array[num2] = obj;
				num2++;
				num = 2;
				continue;
				IL_95:
				num = 0;
			}
			IL_50:
			return null;
		}
		}
	}

	// Token: 0x06003874 RID: 14452 RVA: 0x001F826C File Offset: 0x001F726C
	public static List<ᜀ> ᜀ<ᜀ>(List<ᜀ> A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				int num2;
				int count;
				ᜀ ᜀ;
				List<ᜀ> list;
				switch (num)
				{
				case 0:
					goto IL_99;
				case 1:
					num = 6;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						ICloneable cloneable;
						if (cloneable != null)
						{
							num = 5;
							continue;
						}
						break;
					}
					}
					num = 1;
					continue;
				case 3:
					if (true)
					{
					}
					goto IL_99;
				case 4:
				{
					if (num2 >= count)
					{
						num = 9;
						continue;
					}
					ICloneable cloneable = A_0[num2] as ICloneable;
					num = 2;
					continue;
				}
				case 5:
				{
					ICloneable cloneable;
					ᜀ = (ᜀ)((object)cloneable.Clone());
					goto IL_C3;
				}
				case 6:
					ᜀ = A_0[num2];
					goto IL_C3;
				case 7:
					goto IL_54;
				case 9:
					return list;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				count = A_0.Count;
				list = new List<ᜀ>(count);
				num2 = 0;
				num = 0;
				continue;
				IL_99:
				num = 4;
				continue;
				IL_C3:
				ᜀ item = ᜀ;
				list.Add(item);
				num2++;
				num = 3;
			}
			IL_54:
			return null;
		}
		}
	}

	// Token: 0x06003875 RID: 14453 RVA: 0x001F83B4 File Offset: 0x001F73B4
	public static object ᜀ(ICloneable A_0)
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
			if (A_0 != null)
			{
				return A_0.Clone();
			}
			break;
		}
		return null;
	}

	// Token: 0x06003876 RID: 14454 RVA: 0x001F83FC File Offset: 0x001F73FC
	public static List<BiffRecordRaw> ᜀ(List<BiffRecordRaw> A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				int num2;
				int count;
				List<BiffRecordRaw> list;
				switch (num)
				{
				case 0:
					goto IL_B9;
				case 1:
					goto IL_B9;
				case 2:
				{
					if (num2 >= count)
					{
						goto IL_C9;
					}
					ICloneable a_ = A_0[num2];
					list.Add((BiffRecordRaw)spr\u1CD3.ᜀ(a_));
					num2++;
					num = 1;
					continue;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C9;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 4:
					goto IL_72;
				case 5:
					return list;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				count = A_0.Count;
				list = new List<BiffRecordRaw>(count);
				num2 = 0;
				num = 0;
				continue;
				IL_B9:
				num = 2;
				continue;
				IL_C9:
				num = 5;
			}
			IL_72:
			return null;
		}
		}
	}

	// Token: 0x06003877 RID: 14455 RVA: 0x001F84E4 File Offset: 0x001F74E4
	public static List<spr\u223A> ᜀ(List<spr\u223A> A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				List<spr\u223A> list;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_B4;
				case 1:
					return list;
				case 2:
					goto IL_B4;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C4;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 4:
					goto IL_6A;
				case 5:
				{
					if (num2 >= count)
					{
						goto IL_C4;
					}
					spr\u223A item = A_0[num2].\u170D();
					list.Add(item);
					num2++;
					num = 0;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				count = A_0.Count;
				list = new List<spr\u223A>(count);
				num2 = 0;
				num = 2;
				continue;
				IL_B4:
				num = 5;
				continue;
				IL_C4:
				num = 1;
			}
			IL_6A:
			return null;
		}
		}
	}

	// Token: 0x06003878 RID: 14456 RVA: 0x001F85C4 File Offset: 0x001F75C4
	public static SortedList<int, int> ᜀ(SortedList<int, int> A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				int num2;
				int count;
				SortedList<int, int> sortedList;
				IList<int> keys;
				IList<int> values;
				switch (num)
				{
				case 0:
					goto IL_CA;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					if (num2 >= count)
					{
						goto IL_DB;
					}
					sortedList.Add(keys[num2], values[num2]);
					num2++;
					num = 0;
					continue;
				case 3:
					return sortedList;
				case 4:
					goto IL_72;
				case 5:
					goto IL_CA;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				count = A_0.Count;
				sortedList = new SortedList<int, int>(count);
				keys = A_0.Keys;
				values = A_0.Values;
				num2 = 0;
				num = 5;
				continue;
				IL_CA:
				num = 2;
				continue;
				IL_DB:
				num = 3;
			}
			IL_72:
			return null;
		}
		}
	}

	// Token: 0x06003879 RID: 14457 RVA: 0x001F86BC File Offset: 0x001F76BC
	public static SortedList<ᜀ, ᜁ> ᜀ<ᜀ, ᜁ>(SortedList<ᜀ, ᜁ> A_0) where ᜀ : IComparable
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				IEnumerator<KeyValuePair<ᜀ, ᜁ>> enumerator;
				SortedList<ᜀ, ᜁ> sortedList;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					try
					{
						num = 5;
						for (;;)
						{
							ICloneable cloneable;
							ᜁ ᜁ;
							ᜀ ᜀ;
							switch (num)
							{
							case 0:
								goto IL_C1;
							case 1:
								goto IL_192;
							case 2:
								goto IL_E1;
							case 4:
								num = 1;
								continue;
							case 6:
								ᜁ = (ᜁ)((object)cloneable.Clone());
								num = 10;
								continue;
							case 7:
								if (cloneable != null)
								{
									num = 6;
									continue;
								}
								goto IL_FB;
							case 8:
								ᜀ = (ᜀ)((object)cloneable.Clone());
								num = 0;
								continue;
							case 9:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_E1;
								default:
									if (false)
									{
									}
									if (cloneable != null)
									{
										num = 8;
										continue;
									}
									goto IL_C1;
								}
								break;
							case 10:
								goto IL_FB;
							}
							goto IL_87;
							IL_E1:
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							KeyValuePair<ᜀ, ᜁ> keyValuePair = enumerator.Current;
							ᜁ = keyValuePair.Value;
							cloneable = (ᜁ as ICloneable);
							num = 7;
							continue;
							IL_C1:
							sortedList.Add(ᜀ, ᜁ);
							num = 3;
							continue;
							IL_D5:
							num = 2;
							continue;
							IL_87:
							goto IL_D5;
							IL_FB:
							ᜀ = keyValuePair.Key;
							cloneable = (ᜀ as ICloneable);
							num = 9;
						}
						IL_192:
						return sortedList;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1D4;
							case 2:
								enumerator.Dispose();
								num = 0;
								continue;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 2;
						}
						IL_1D4:;
					}
					goto IL_1D7;
				case 2:
					goto IL_44;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				IL_1D7:
				int count = A_0.Count;
				sortedList = new SortedList<ᜀ, ᜁ>(count);
				enumerator = A_0.GetEnumerator();
				num = 0;
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("唸刺丼䬾", a_));
		}
		}
	}

	// Token: 0x0600387A RID: 14458 RVA: 0x001F890C File Offset: 0x001F790C
	public static List<ᜀ> ᜀ<ᜀ>(IList<ᜀ> A_0, object A_1)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 5;
			for (;;)
			{
				int num2;
				int count;
				List<ᜀ> list;
				switch (num)
				{
				case 0:
					goto IL_C7;
				case 1:
				{
					if (num2 >= count)
					{
						goto IL_D7;
					}
					ICloneParent a_ = (ICloneParent)((object)A_0[num2]);
					list.Add((ᜀ)((object)spr\u1CD3.ᜀ(a_, A_1)));
					num2++;
					num = 4;
					continue;
				}
				case 2:
					goto IL_72;
				case 3:
					return list;
				case 4:
					goto IL_C7;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D7;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				count = A_0.Count;
				list = new List<ᜀ>(count);
				num2 = 0;
				num = 0;
				continue;
				IL_C7:
				num = 1;
				continue;
				IL_D7:
				num = 3;
			}
			IL_72:
			return null;
		}
		}
	}

	// Token: 0x0600387B RID: 14459 RVA: 0x001F8A00 File Offset: 0x001F7A00
	public static object ᜀ(ICloneParent A_0, object A_1)
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
			if (A_0 != null)
			{
				return A_0.Clone(A_1);
			}
			break;
		}
		return null;
	}

	// Token: 0x0600387C RID: 14460 RVA: 0x001F8A4C File Offset: 0x001F7A4C
	[CLSCompliant(false)]
	public static object ᜀ(spr\u1D3B A_0, spr\u1D3B A_1)
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
			if (A_0 != null)
			{
				return A_0.ᜁ(A_1);
			}
			break;
		}
		return null;
	}

	// Token: 0x0600387D RID: 14461 RVA: 0x001F8A98 File Offset: 0x001F7A98
	public static byte[] ᜀ(byte[] A_0)
	{
		int num = 3;
		for (;;)
		{
			byte[] array;
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_57;
			case 1:
				return array;
			case 2:
				goto IL_93;
			case 4:
				if (num2 >= num3)
				{
					num = 1;
					continue;
				}
				array[num2] = A_0[num2];
				num2++;
				num = 2;
				continue;
			case 5:
				goto IL_93;
			}
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
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				break;
			}
			num3 = A_0.Length;
			array = new byte[num3];
			num2 = 0;
			num = 5;
			continue;
			IL_93:
			num = 4;
		}
		IL_57:
		return null;
	}

	// Token: 0x0600387E RID: 14462 RVA: 0x001F8B58 File Offset: 0x001F7B58
	public static Ptg[] ᜀ(Ptg[] A_0)
	{
		int num = 5;
		for (;;)
		{
			int num2;
			int num3;
			Ptg[] array;
			switch (num)
			{
			case 0:
				goto IL_9D;
			case 1:
				if (num2 >= num3)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				array[num2] = (Ptg)((ICloneable)A_0[num2]).Clone();
				num2++;
				num = 2;
				continue;
			case 2:
				goto IL_9D;
			case 3:
				goto IL_4F;
			case 4:
				return array;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				break;
			}
			num3 = A_0.Length;
			array = new Ptg[num3];
			num2 = 0;
			num = 0;
			continue;
			IL_9D:
			num = 1;
		}
		IL_4F:
		return null;
	}

	// Token: 0x0600387F RID: 14463 RVA: 0x001F8C20 File Offset: 0x001F7C20
	[CLSCompliant(false)]
	public static spr\u216E[] ᜀ(spr\u216E[] A_0)
	{
		int num = 2;
		spr\u216E[] array;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				if (num2 >= num3)
				{
					num = 4;
					continue;
				}
				goto IL_3D;
			case 1:
				goto IL_7E;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				goto IL_7E;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3D;
				default:
					goto IL_AA;
				}
				break;
			case 5:
				goto IL_3B;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num3 = A_0.Length;
			array = new spr\u216E[num3];
			num2 = 0;
			num = 1;
			continue;
			IL_3D:
			array[num2] = (spr\u216E)spr\u1CD3.ᜀ(A_0[num2]);
			num2++;
			num = 3;
			continue;
			IL_7E:
			num = 0;
		}
		IL_3B:
		return null;
		IL_AA:
		if (false)
		{
		}
		return array;
	}

	// Token: 0x06003880 RID: 14464 RVA: 0x001F8CE0 File Offset: 0x001F7CE0
	public static Dictionary<ᜀ, ᜁ> ᜀ<ᜀ, ᜁ>(Dictionary<ᜀ, ᜁ> A_0)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				Dictionary<ᜀ, ᜁ>.Enumerator enumerator;
				Dictionary<ᜀ, ᜁ> dictionary;
				switch (num)
				{
				case 0:
					goto IL_44;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D5;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					try
					{
						num = 5;
						for (;;)
						{
							ICloneable cloneable;
							ᜁ ᜁ;
							KeyValuePair<ᜀ, ᜁ> keyValuePair;
							ᜀ ᜀ;
							switch (num)
							{
							case 0:
								if (cloneable != null)
								{
									num = 4;
									continue;
								}
								goto IL_F5;
							case 1:
								goto IL_F5;
							case 2:
								goto IL_170;
							case 3:
								num = 2;
								continue;
							case 4:
								ᜁ = (ᜁ)((object)cloneable.Clone());
								num = 1;
								continue;
							case 7:
								goto IL_BE;
							case 8:
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								keyValuePair = enumerator.Current;
								ᜁ = keyValuePair.Value;
								cloneable = (ᜁ as ICloneable);
								num = 0;
								continue;
							case 9:
								if (cloneable != null)
								{
									num = 10;
									continue;
								}
								goto IL_BE;
							case 10:
								ᜀ = (ᜀ)((object)cloneable.Clone());
								num = 7;
								continue;
							}
							goto IL_87;
							IL_BE:
							dictionary.Add(ᜀ, ᜁ);
							num = 6;
							continue;
							IL_D2:
							num = 8;
							continue;
							IL_87:
							goto IL_D2;
							IL_F5:
							ᜀ = keyValuePair.Key;
							cloneable = (ᜀ as ICloneable);
							num = 9;
						}
						IL_170:
						return dictionary;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_180;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				IL_180:
				int count = A_0.Count;
				dictionary = new Dictionary<ᜀ, ᜁ>(count);
				enumerator = A_0.GetEnumerator();
				num = 2;
			}
			IL_44:
			IL_1D5:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⅈ⩊㹌❎", a_));
		}
		}
	}

	// Token: 0x06003881 RID: 14465 RVA: 0x001F8EF4 File Offset: 0x001F7EF4
	public static Dictionary<spr\u223A, int> ᜀ(Dictionary<spr\u223A, int> A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				Dictionary<spr\u223A, int>.Enumerator enumerator;
				Dictionary<spr\u223A, int> dictionary;
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_121;
					default:
						if (false)
						{
						}
						try
						{
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 3;
									continue;
								case 1:
								{
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									KeyValuePair<spr\u223A, int> keyValuePair = enumerator.Current;
									spr\u223A key = keyValuePair.Key.\u170D();
									dictionary.Add(key, keyValuePair.Value);
									num = 2;
									continue;
								}
								case 3:
									goto IL_CD;
								}
								IL_A7:
								num = 1;
								continue;
								goto IL_A7;
							}
							IL_CD:
							return dictionary;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_DD;
					}
					break;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				IL_DD:
				dictionary = new Dictionary<spr\u223A, int>();
				enumerator = A_0.GetEnumerator();
				num = 2;
			}
			IL_4C:
			IL_121:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱃ❅㭇≉", a_));
		}
		}
	}

	// Token: 0x06003882 RID: 14466 RVA: 0x001F9048 File Offset: 0x001F8048
	public static Dictionary<object, int> ᜀ(Dictionary<object, int> A_0)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 2;
			Dictionary<object, int> dictionary;
			for (;;)
			{
				Dictionary<object, int>.Enumerator enumerator;
				switch (num)
				{
				case 0:
					goto IL_44;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15A;
					}
					if (false)
					{
					}
					try
					{
						num = 2;
						for (;;)
						{
							object obj;
							KeyValuePair<object, int> keyValuePair;
							switch (num)
							{
							case 0:
								goto IL_C8;
							case 1:
							{
								spr\u223A spr_u223A;
								obj = spr_u223A.ᜐ();
								num = 0;
								continue;
							}
							case 3:
							{
								spr\u223A spr_u223A;
								if (spr_u223A != null)
								{
									num = 1;
									continue;
								}
								goto IL_C8;
							}
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 6;
									continue;
								}
								keyValuePair = enumerator.Current;
								obj = keyValuePair.Key;
								spr\u223A spr_u223A = obj as spr\u223A;
								num = 3;
								continue;
							}
							case 5:
								goto IL_105;
							case 6:
								num = 5;
								continue;
							}
							IL_AB:
							num = 4;
							continue;
							goto IL_AB;
							IL_C8:
							dictionary.Add(obj, keyValuePair.Value);
							num = 7;
						}
						IL_105:
						goto IL_16E;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_115;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				IL_115:
				dictionary = new Dictionary<object, int>();
				enumerator = A_0.GetEnumerator();
				num = 1;
			}
			IL_44:
			IL_15A:
			throw new ArgumentNullException(RecordTableEnumerator.b("吻弽㌿⩁", a_));
			IL_16E:
			if (true)
			{
			}
			return dictionary;
		}
		}
	}

	// Token: 0x06003883 RID: 14467 RVA: 0x001F91DC File Offset: 0x001F81DC
	public static Dictionary<int, int> ᜀ(Dictionary<int, int> A_0)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			Dictionary<int, int>.Enumerator enumerator;
			Dictionary<int, int> dictionary;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BF;
				default:
					goto IL_F2;
				}
				break;
			case 1:
				try
				{
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_AF;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							KeyValuePair<int, int> keyValuePair = enumerator.Current;
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
							num = 4;
							continue;
						}
						}
						IL_8C:
						num = 2;
						continue;
						goto IL_8C;
					}
					IL_AF:
					return dictionary;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_BF;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_BF:
			dictionary = new Dictionary<int, int>();
			enumerator = A_0.GetEnumerator();
			num = 1;
		}
		IL_F2:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⽆⡈㡊╌", a_));
	}

	// Token: 0x06003884 RID: 14468 RVA: 0x001F9310 File Offset: 0x001F8310
	public static Dictionary<ᜀ, ᜁ> ᜀ<ᜀ, ᜁ>(Dictionary<ᜀ, ᜁ> A_0, object A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				Dictionary<ᜀ, ᜁ>.Enumerator enumerator;
				Dictionary<ᜀ, ᜁ> dictionary;
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D7;
					default:
						if (false)
						{
						}
						try
						{
							num = 7;
							for (;;)
							{
								ICloneParent cloneParent;
								ᜁ ᜁ;
								ᜀ ᜀ;
								KeyValuePair<ᜀ, ᜁ> keyValuePair;
								switch (num)
								{
								case 0:
									goto IL_BE;
								case 1:
									goto IL_F5;
								case 2:
									ᜁ = (ᜁ)((object)cloneParent.Clone(A_1));
									num = 1;
									continue;
								case 3:
									ᜀ = (ᜀ)((object)cloneParent.Clone(A_1));
									num = 0;
									continue;
								case 4:
									goto IL_172;
								case 5:
									num = 4;
									continue;
								case 6:
									if (cloneParent != null)
									{
										num = 3;
										continue;
									}
									goto IL_BE;
								case 8:
									if (!enumerator.MoveNext())
									{
										num = 5;
										continue;
									}
									keyValuePair = enumerator.Current;
									ᜁ = keyValuePair.Value;
									cloneParent = (ᜁ as ICloneParent);
									num = 10;
									continue;
								case 10:
									if (cloneParent != null)
									{
										num = 2;
										continue;
									}
									goto IL_F5;
								}
								goto IL_87;
								IL_BE:
								dictionary.Add(ᜀ, ᜁ);
								num = 9;
								continue;
								IL_D2:
								num = 8;
								continue;
								IL_87:
								goto IL_D2;
								IL_F5:
								ᜀ = keyValuePair.Key;
								cloneParent = (ᜀ as ICloneParent);
								num = 6;
							}
							IL_172:
							return dictionary;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_182;
					}
					break;
				case 2:
					goto IL_44;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				IL_182:
				int count = A_0.Count;
				dictionary = new Dictionary<ᜀ, ᜁ>(count);
				enumerator = A_0.GetEnumerator();
				if (true)
				{
				}
				num = 1;
			}
			IL_44:
			IL_1D7:
			throw new ArgumentNullException(RecordTableEnumerator.b("吻弽㌿⩁", a_));
		}
		}
	}

	// Token: 0x06003885 RID: 14469 RVA: 0x001F9528 File Offset: 0x001F8528
	public static Stream ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			MemoryStream memoryStream;
			long position;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EA;
				default:
				{
					if (false)
					{
					}
					byte[] buffer;
					switch (num)
					{
					case 0:
					{
						if (true)
						{
						}
						int count;
						if ((count = A_0.Read(buffer, 0, 32768)) == 0)
						{
							num = 4;
							continue;
						}
						memoryStream.Write(buffer, 0, count);
						num = 3;
						continue;
					}
					case 2:
						goto IL_AD;
					case 3:
						goto IL_AD;
					case 4:
						goto IL_E8;
					case 5:
						goto IL_60;
					}
					if (A_0 == null)
					{
						num = 5;
						break;
					}
					position = A_0.Position;
					memoryStream = new MemoryStream((int)A_0.Length);
					A_0.Position = 0L;
					buffer = new byte[32768];
					num = 2;
					break;
					IL_AD:
					num = 0;
					break;
				}
				}
			}
			IL_60:
			return null;
			IL_E8:
			IL_EA:
			A_0.Position = position;
			memoryStream.Position = position;
			return memoryStream;
		}
		}
	}

	// Token: 0x06003886 RID: 14470 RVA: 0x001F9630 File Offset: 0x001F8630
	public static bool[] ᜀ(bool[] A_0)
	{
		bool[] array;
		for (;;)
		{
			array = null;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
				{
					int num2;
					Buffer.BlockCopy(A_0, 0, array, 0, num2);
					num = 4;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return array;
					default:
					{
						if (false)
						{
						}
						int num2 = A_0.Length;
						array = new bool[num2];
						num = 3;
						continue;
					}
					}
					break;
				case 2:
					if (A_0 != null)
					{
						num = 1;
						continue;
					}
					return array;
				case 3:
				{
					int num2;
					if (num2 > 0)
					{
						num = 0;
						continue;
					}
					return array;
				}
				case 4:
					return array;
				}
				break;
			}
		}
		return array;
	}
}
