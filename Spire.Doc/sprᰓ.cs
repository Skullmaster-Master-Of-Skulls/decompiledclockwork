using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200033B RID: 827
internal sealed class sprᰓ
{
	// Token: 0x06002C2C RID: 11308 RVA: 0x002ABD54 File Offset: 0x002AAD54
	public static int[] ᜀ(int[] A_0)
	{
		int num = 3;
		for (;;)
		{
			int num2;
			int num3;
			int[] array;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					if (num2 >= num3)
					{
						num = 5;
						continue;
					}
					array[num2] = A_0[num2];
					num2++;
					num = 4;
					continue;
				case 1:
					goto IL_93;
				case 2:
					goto IL_61;
				case 4:
					goto IL_93;
				case 5:
					return array;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				break;
				IL_93:
				num = 0;
				continue;
			}
			num3 = A_0.Length;
			array = new int[num3];
			num2 = 0;
			num = 1;
		}
		IL_61:
		return null;
	}

	// Token: 0x06002C2D RID: 11309 RVA: 0x002ABE14 File Offset: 0x002AAE14
	public static ushort[] ᜀ(ushort[] A_0)
	{
		int num = 2;
		for (;;)
		{
			int num2;
			int num3;
			ushort[] array;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_93;
				case 1:
					if (num2 >= num3)
					{
						num = 3;
						continue;
					}
					array[num2] = A_0[num2];
					num2++;
					num = 0;
					continue;
				case 3:
					return array;
				case 4:
					goto IL_57;
				case 5:
					goto IL_93;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				break;
				IL_93:
				num = 1;
				continue;
			}
			num3 = A_0.Length;
			array = new ushort[num3];
			num2 = 0;
			num = 5;
		}
		IL_57:
		return null;
	}

	// Token: 0x06002C2E RID: 11310 RVA: 0x002ABED4 File Offset: 0x002AAED4
	public static string[] ᜀ(string[] A_0)
	{
		string[] array;
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
			int num = 5;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_88;
				case 1:
					goto IL_4F;
				case 2:
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
					num = 0;
					continue;
				case 3:
					goto IL_A4;
				case 4:
					goto IL_88;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num3 = A_0.Length;
				array = new string[num3];
				num2 = 0;
				num = 4;
				continue;
				IL_88:
				num = 2;
			}
			IL_4F:
			return null;
			IL_A4:
			break;
		}
		}
		return array;
	}

	// Token: 0x06002C2F RID: 11311 RVA: 0x002ABF88 File Offset: 0x002AAF88
	public static object[] ᜀ(object[] A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				object[] array;
				int num2;
				int num3;
				object obj;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_B4;
				case 1:
					return array;
				case 2:
				{
					if (num2 >= num3)
					{
						num = 1;
						continue;
					}
					obj = A_0[num2];
					ICloneable cloneable = obj as ICloneable;
					num = 7;
					continue;
				}
				case 3:
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
						break;
					}
					ICloneable cloneable;
					obj = cloneable.Clone();
					num = 8;
					continue;
				}
				case 5:
					goto IL_53;
				case 6:
					goto IL_B4;
				case 7:
				{
					ICloneable cloneable;
					if (cloneable != null)
					{
						num = 3;
						continue;
					}
					goto IL_55;
				}
				case 8:
					goto IL_55;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num3 = A_0.Length;
				array = new object[num3];
				num2 = 0;
				num = 0;
				continue;
				IL_55:
				array[num2] = obj;
				num2++;
				num = 6;
				continue;
				IL_B4:
				num = 2;
			}
			IL_53:
			return null;
		}
		}
	}

	// Token: 0x06002C30 RID: 11312 RVA: 0x002AC0A4 File Offset: 0x002AB0A4
	public static object ᜀ(ICloneable A_0)
	{
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
				return null;
			}
		}
		return A_0.Clone();
	}

	// Token: 0x06002C31 RID: 11313 RVA: 0x002AC0EC File Offset: 0x002AB0EC
	public static SortedList<int, int> ᜀ(SortedList<int, int> A_0)
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					SortedList<int, int> sortedList;
					int num2;
					int count;
					IList<int> keys;
					IList<int> values;
					switch (num)
					{
					case 0:
						return sortedList;
					case 1:
						goto IL_CA;
					case 2:
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						sortedList.Add(keys[num2], values[num2]);
						num2++;
						num = 3;
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
							goto IL_CA;
						}
						break;
					case 5:
						goto IL_4E;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					count = A_0.Count;
					sortedList = new SortedList<int, int>(count);
					keys = A_0.Keys;
					values = A_0.Values;
					num2 = 0;
					num = 1;
					continue;
					IL_CA:
					num = 2;
				}
				break;
			}
			}
		}
		IL_4E:
		if (true)
		{
		}
		return null;
	}

	// Token: 0x06002C32 RID: 11314 RVA: 0x002AC1E8 File Offset: 0x002AB1E8
	public static byte[] ᜀ(byte[] A_0)
	{
		byte[] array;
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
			int num = 5;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_90;
				case 1:
					goto IL_A4;
				case 2:
					goto IL_57;
				case 3:
					goto IL_90;
				case 4:
					if (num2 >= num3)
					{
						num = 1;
						continue;
					}
					array[num2] = A_0[num2];
					num2++;
					num = 0;
					continue;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num3 = A_0.Length;
				array = new byte[num3];
				num2 = 0;
				num = 3;
				continue;
				IL_90:
				num = 4;
			}
			IL_57:
			return null;
			IL_A4:
			break;
		}
		}
		return array;
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x002AC29C File Offset: 0x002AB29C
	public static Dictionary<ᜀ, ᜁ> ᜀ<ᜀ, ᜁ>(Dictionary<ᜀ, ᜁ> A_0)
	{
		int a_ = 9;
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
					try
					{
						num = 7;
						for (;;)
						{
							KeyValuePair<ᜀ, ᜁ> keyValuePair;
							ᜁ ᜁ;
							ICloneable cloneable;
							ᜀ ᜀ;
							switch (num)
							{
							case 0:
								goto IL_1A1;
							case 1:
								if (!enumerator.MoveNext())
								{
									num = 5;
									continue;
								}
								keyValuePair = enumerator.Current;
								ᜁ = keyValuePair.Value;
								cloneable = (ᜁ as ICloneable);
								num = 10;
								continue;
							case 2:
								ᜁ = (ᜁ)((object)cloneable.Clone());
								num = 8;
								continue;
							case 4:
								if (cloneable != null)
								{
									num = 6;
									continue;
								}
								goto IL_EC;
							case 5:
								num = 0;
								continue;
							case 6:
								ᜀ = (ᜀ)((object)cloneable.Clone());
								num = 9;
								continue;
							case 8:
								goto IL_126;
							case 9:
								goto IL_EC;
							case 10:
								if (true)
								{
								}
								if (cloneable != null)
								{
									num = 2;
									continue;
								}
								goto IL_126;
							}
							goto IL_AD;
							IL_EC:
							dictionary.Add(ᜀ, ᜁ);
							num = 3;
							continue;
							IL_103:
							num = 1;
							continue;
							IL_AD:
							goto IL_103;
							IL_126:
							ᜀ = keyValuePair.Key;
							cloneable = (ᜀ as ICloneable);
							num = 4;
						}
						IL_1A1:
						return dictionary;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_1B1;
				case 2:
					goto IL_6A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1D8;
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
				IL_1B1:
				int count = A_0.Count;
				dictionary = new Dictionary<ᜀ, ᜁ>(count);
				enumerator = A_0.GetEnumerator();
				num = 1;
			}
			IL_6A:
			IL_1D8:
			throw new ArgumentNullException(ClipboardData.b("ݮၰrᵴ", a_));
		}
		}
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x002AC4B4 File Offset: 0x002AB4B4
	public static Dictionary<int, int> ᜀ(Dictionary<int, int> A_0)
	{
		int a_ = 4;
		int num = 0;
		for (;;)
		{
			Dictionary<int, int>.Enumerator enumerator;
			Dictionary<int, int> dictionary;
			switch (num)
			{
			case 1:
				try
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 2:
							goto IL_B1;
						case 3:
							num = 2;
							continue;
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							KeyValuePair<int, int> keyValuePair = enumerator.Current;
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
							num = 0;
							continue;
						}
						}
						IL_8E:
						num = 4;
						continue;
						goto IL_8E;
					}
					IL_B1:
					return dictionary;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_C1;
			case 2:
				goto IL_3D;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_C1:
			dictionary = new Dictionary<int, int>();
			enumerator = A_0.GetEnumerator();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3D;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 1;
				break;
			}
		}
		IL_3D:
		throw new ArgumentNullException(ClipboardData.b("ɩ൫ᵭᡯ", a_));
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x002AC5E8 File Offset: 0x002AB5E8
	public static Stream ᜀ(Stream A_0)
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
		return sprℭ.ᜀ(A_0);
	}
}
