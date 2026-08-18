using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Spire.CompoundFile.XLS;
using Spire.CompoundFile.XLS.Net;

// Token: 0x020002C6 RID: 710
internal class spr\u2129 : IPropertyData, IComparable
{
	// Token: 0x06002B09 RID: 11017 RVA: 0x0017F2D0 File Offset: 0x0017E2D0
	public bool ᜂ()
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
		return (this.ᜅ() & 16777216) != 0;
	}

	// Token: 0x06002B0A RID: 11018 RVA: 0x0017F320 File Offset: 0x0017E320
	public int ᜁ()
	{
		while (!this.ᜂ())
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
				return this.ᜅ();
			}
		}
		return this.ᜅ() - 16777216;
	}

	// Token: 0x06002B0B RID: 11019 RVA: 0x0017F378 File Offset: 0x0017E378
	public int ᜅ()
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
		return this.ᜂ;
	}

	// Token: 0x06002B0C RID: 11020 RVA: 0x0017F3BC File Offset: 0x0017E3BC
	public void ᜀ(int A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06002B0D RID: 11021 RVA: 0x0017F400 File Offset: 0x0017E400
	internal spr\u2129()
	{
	}

	// Token: 0x06002B0E RID: 11022 RVA: 0x0017F414 File Offset: 0x0017E414
	public spr\u2129(int A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002B0F RID: 11023 RVA: 0x0017F430 File Offset: 0x0017E430
	public void ᜂ(Stream A_0, int A_1)
	{
		for (;;)
		{
			byte[] a_ = new byte[4];
			this.ᜄ = (PropertyType)spr\u23D6.ᜁ(A_0, a_);
			if ((this.ᜄ & PropertyType.Vector) == PropertyType.Empty)
			{
				goto IL_61;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_42;
			}
		}
		IL_42:
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜅ = this.ᜁ(A_0, A_1);
		return;
		IL_61:
		this.ᜅ = this.ᜀ(this.ᜄ, A_0, A_1);
	}

	// Token: 0x06002B10 RID: 11024 RVA: 0x0017F4B4 File Offset: 0x0017E4B4
	private IList ᜁ(Stream A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			IList list;
			for (;;)
			{
				byte[] a_ = new byte[4];
				int num = spr\u23D6.ᜁ(A_0, a_);
				PropertyType a_2 = this.ᜄ & ~PropertyType.Vector;
				list = this.ᜀ(a_2, num);
				int num2 = 0;
				if (true)
				{
				}
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (num2 >= num)
						{
							num3 = 1;
							continue;
						}
						list[num2] = this.ᜀ(a_2, A_0, A_1 - 4);
						num2++;
						goto IL_C0;
					case 1:
						return list;
					case 2:
						goto IL_62;
					case 3:
						goto IL_62;
					}
					break;
					IL_62:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_C0:
						num3 = 3;
						break;
					default:
						if (false)
						{
						}
						num3 = 0;
						break;
					}
				}
			}
			return list;
		}
		}
	}

	// Token: 0x06002B11 RID: 11025 RVA: 0x0017F590 File Offset: 0x0017E590
	private IList ᜀ(PropertyType A_0, int A_1)
	{
		for (;;)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != PropertyType.Object)
					{
						goto IL_A5;
					}
					goto IL_12F;
				case 1:
					if (true)
					{
					}
					num = 8;
					continue;
				case 2:
					num = 4;
					continue;
				case 3:
					num = 7;
					continue;
				case 4:
					goto IL_5F;
				case 5:
					if (A_0 != PropertyType.Int32)
					{
						num = 10;
						continue;
					}
					goto IL_B2;
				case 6:
					if (A_0 <= PropertyType.Object)
					{
						num = 9;
						continue;
					}
					num = 11;
					continue;
				case 7:
					goto IL_12D;
				case 8:
					switch (A_0)
					{
					case PropertyType.AsciiString:
					case PropertyType.String:
						goto IL_FB;
					default:
						num = 2;
						continue;
					}
					break;
				case 9:
					num = 5;
					continue;
				case 10:
					num = 0;
					continue;
				case 11:
					if (A_0 != PropertyType.Int)
					{
						num = 1;
						continue;
					}
					goto IL_B2;
				}
				break;
				IL_A5:
				num = 3;
				continue;
				IL_B2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A5;
				default:
					goto IL_C8;
				}
			}
		}
		IL_5F:
		goto IL_12F;
		IL_C8:
		if (false)
		{
		}
		return new int[A_1];
		IL_FB:
		return new string[A_1];
		IL_12D:
		IL_12F:
		return new object[A_1];
	}

	// Token: 0x06002B12 RID: 11026 RVA: 0x0017F6D4 File Offset: 0x0017E6D4
	private object ᜀ(PropertyType A_0, Stream A_1, int A_2)
	{
		object result;
		for (;;)
		{
			byte[] a_ = new byte[8];
			result = null;
			int num = 26;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
					goto IL_11A;
				case 2:
					switch (A_0)
					{
					case PropertyType.Empty:
					case PropertyType.Null:
						result = null;
						num = 9;
						continue;
					case PropertyType.Int16:
						result = spr\u23D6.ᜂ(A_1, a_);
						A_1.Position += 2L;
						num = 21;
						continue;
					case PropertyType.Int32:
						goto IL_2DC;
					case (PropertyType)4:
					case (PropertyType)6:
					case (PropertyType)7:
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)10:
						goto IL_1FC;
					case PropertyType.Double:
						result = spr\u23D6.ᜀ(A_1, a_);
						num = 11;
						continue;
					case PropertyType.Bool:
						result = (spr\u23D6.ᜁ(A_1, a_) != 0);
						num = 5;
						continue;
					case PropertyType.Object:
						result = this.ᜀ(A_1, A_2 - 4);
						num = 17;
						continue;
					default:
						num = 4;
						continue;
					}
					break;
				case 3:
					return result;
				case 4:
					num = 10;
					continue;
				case 5:
					return result;
				case 6:
					if (A_0 != PropertyType.Int)
					{
						num = 22;
						continue;
					}
					goto IL_2DC;
				case 7:
					if (A_0 != PropertyType.ClipboardData)
					{
						num = 18;
						continue;
					}
					result = this.ᜁ(A_1, a_);
					num = 12;
					continue;
				case 8:
					return result;
				case 9:
					goto IL_1F7;
				case 10:
					if (A_0 != PropertyType.UInt32)
					{
						num = 0;
						continue;
					}
					result = (uint)spr\u23D6.ᜂ(A_1, a_);
					num = 19;
					continue;
				case 11:
					return result;
				case 12:
					goto IL_1B9;
				case 13:
					goto IL_20D;
				case 14:
					goto IL_D4;
				case 15:
					switch (A_0)
					{
					case PropertyType.DateTime:
						result = this.ᜃ(A_1, a_);
						goto IL_C9;
					case PropertyType.Blob:
						result = this.ᜂ(A_1, a_);
						num = 8;
						continue;
					default:
						num = 20;
						continue;
					}
					break;
				case 16:
					goto IL_27A;
				case 17:
					goto IL_1A0;
				case 18:
					num = 13;
					continue;
				case 19:
					goto IL_BB;
				case 20:
					num = 7;
					continue;
				case 21:
					goto IL_185;
				case 22:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C9;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 23:
					return result;
				case 24:
					num = 15;
					continue;
				case 25:
					switch (A_0)
					{
					case PropertyType.AsciiString:
						result = spr\u23D6.ᜁ(A_1, A_2 - 4);
						num = 3;
						continue;
					case PropertyType.String:
						result = spr\u23D6.ᜀ(A_1, A_2 - 4);
						num = 1;
						continue;
					default:
						num = 24;
						continue;
					}
					break;
				case 26:
					if (A_0 <= PropertyType.Int)
					{
						num = 27;
						continue;
					}
					num = 25;
					continue;
				case 27:
					if (true)
					{
					}
					num = 2;
					continue;
				}
				break;
				IL_C9:
				num = 14;
				continue;
				IL_2DC:
				result = spr\u23D6.ᜁ(A_1, a_);
				num = 23;
			}
		}
		IL_BB:
		IL_D4:
		IL_11A:
		IL_185:
		IL_1A0:
		IL_1B9:
		IL_1F7:
		return result;
		IL_1FC:
		throw new NotImplementedException();
		IL_20D:
		IL_27A:
		goto IL_1FC;
	}

	// Token: 0x06002B13 RID: 11027 RVA: 0x0017FA38 File Offset: 0x0017EA38
	private object ᜃ(Stream A_0, byte[] A_1)
	{
		DateTime dateTime;
		for (;;)
		{
			if (true)
			{
			}
			A_0.Read(A_1, 0, 8);
			long ticks = BitConverter.ToInt64(A_1, 0) + 504911232000000000L;
			dateTime = new DateTime(ticks);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					dateTime = dateTime.ToLocalTime();
					num = 1;
					continue;
				case 1:
					goto IL_78;
				case 2:
					IL_48:
					if (this.ᜅ() != 10)
					{
						num = 0;
						continue;
					}
					goto IL_78;
				}
				break;
				IL_78:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				default:
					goto IL_8E;
				}
			}
		}
		IL_8E:
		if (false)
		{
		}
		return dateTime;
	}

	// Token: 0x06002B14 RID: 11028 RVA: 0x0017FAE0 File Offset: 0x0017EAE0
	private object ᜂ(Stream A_0, byte[] A_1)
	{
		for (;;)
		{
			int num = spr\u23D6.ᜁ(A_0, A_1);
			byte[] array = new byte[num];
			if (A_0.Read(array, 0, num) == num)
			{
				return array;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_3B;
			}
		}
		IL_3B:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new Exception();
	}

	// Token: 0x06002B15 RID: 11029 RVA: 0x0017FB40 File Offset: 0x0017EB40
	private object ᜁ(Stream A_0, byte[] A_1)
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
		ClipboardData clipboardData = new ClipboardData();
		clipboardData.Parse(A_0);
		return clipboardData;
	}

	// Token: 0x06002B16 RID: 11030 RVA: 0x0017FB8C File Offset: 0x0017EB8C
	private object ᜀ(Stream A_0, int A_1)
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
		byte[] a_ = new byte[4];
		PropertyType a_2 = (PropertyType)spr\u23D6.ᜁ(A_0, a_);
		return this.ᜀ(a_2, A_0, A_1 - 4);
	}

	// Token: 0x06002B17 RID: 11031 RVA: 0x0017FBE4 File Offset: 0x0017EBE4
	private int ᜀ(Stream A_0, object A_1)
	{
		PropertyType propertyType;
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
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_ED;
				case 1:
					propertyType = PropertyType.String;
					num = 0;
					continue;
				case 2:
					goto IL_DD;
				case 4:
					if (A_1 is string)
					{
						num = 1;
						continue;
					}
					goto IL_CA;
				case 5:
					if (A_1 is double)
					{
						num = 8;
						continue;
					}
					num = 10;
					continue;
				case 6:
					propertyType = PropertyType.Int32;
					num = 2;
					continue;
				case 7:
					goto IL_9B;
				case 8:
					propertyType = PropertyType.Double;
					num = 11;
					continue;
				case 9:
					propertyType = PropertyType.Bool;
					num = 7;
					continue;
				case 10:
					if (A_1 is bool)
					{
						num = 9;
						continue;
					}
					if (true)
					{
					}
					num = 4;
					continue;
				case 11:
					goto IL_122;
				}
				if (A_1 is int)
				{
					num = 6;
				}
				else
				{
					num = 5;
				}
			}
			IL_9B:
			break;
			IL_CA:
			throw new NotImplementedException();
			IL_ED:
			IL_122:
			break;
		}
		}
		IL_DD:
		spr\u23D6.ᜂ(A_0, (int)propertyType);
		return this.ᜀ(A_0, A_1, propertyType) + 4;
	}

	// Token: 0x06002B18 RID: 11032 RVA: 0x0017FD28 File Offset: 0x0017ED28
	public int ᜀ(Stream A_0)
	{
		int num;
		for (;;)
		{
			IL_4C:
			num = spr\u23D6.ᜂ(A_0, (int)this.ᜄ);
			int num2 = 8;
			for (;;)
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
					switch (num2)
					{
					case 0:
						goto IL_89;
					case 1:
						spr\u23D6.ᜀ(A_0, ref num);
						num2 = 2;
						continue;
					case 2:
						return num;
					case 3:
						if (true)
						{
						}
						num += this.ᜀ(A_0, (IList)this.ᜅ);
						num2 = 0;
						continue;
					case 4:
						A_0.Position -= 4L;
						num += this.ᜀ(A_0, (Dictionary<int, string>)this.ᜅ);
						goto IL_D2;
					case 5:
						if (this.ᜅ() == 0)
						{
							num2 = 4;
							continue;
						}
						num += this.ᜀ(A_0, this.ᜅ, this.ᜄ);
						num2 = 7;
						continue;
					case 6:
						if (this.ᜄ != PropertyType.AsciiString)
						{
							num2 = 1;
							continue;
						}
						return num;
					case 7:
						goto IL_89;
					case 8:
						if ((this.ᜄ & PropertyType.Vector) == PropertyType.Vector)
						{
							num2 = 3;
							continue;
						}
						num2 = 5;
						continue;
					case 9:
						goto IL_89;
					}
					goto IL_4C;
					IL_89:
					num2 = 6;
					continue;
				}
				IL_D2:
				num2 = 9;
			}
		}
		return num;
	}

	// Token: 0x06002B19 RID: 11033 RVA: 0x0017FEA0 File Offset: 0x0017EEA0
	private int ᜀ(Stream A_0, Dictionary<int, string> A_1)
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
			switch (0)
			{
			}
			break;
		}
		int num = 0;
		int count = A_1.Count;
		num += spr\u23D6.ᜂ(A_0, count);
		using (Dictionary<int, string>.Enumerator enumerator = A_1.GetEnumerator())
		{
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num2 = 3;
					continue;
				case 1:
				{
					if (!enumerator.MoveNext())
					{
						num2 = 0;
						continue;
					}
					KeyValuePair<int, string> keyValuePair = enumerator.Current;
					num += spr\u23D6.ᜂ(A_0, keyValuePair.Key);
					num += spr\u23D6.ᜀ(A_0, keyValuePair.Value, false);
					num2 = 2;
					continue;
				}
				case 3:
					goto IL_DA;
				}
				IL_B4:
				num2 = 1;
				continue;
				goto IL_B4;
			}
			IL_DA:;
		}
		return num;
	}

	// Token: 0x06002B1A RID: 11034 RVA: 0x0017FFA8 File Offset: 0x0017EFA8
	private int ᜀ(Stream A_0, IList A_1)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num;
			for (;;)
			{
				int count = A_1.Count;
				spr\u23D6.ᜂ(A_0, count);
				num = 4;
				PropertyType a_ = this.ᜄ & ~PropertyType.Vector;
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_5A;
					case 1:
						if (num2 >= count)
						{
							num3 = 3;
							continue;
						}
						for (;;)
						{
							num += this.ᜀ(A_0, A_1[num2], a_);
							num2++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_A8;
							}
						}
						IL_A8:
						if (false)
						{
						}
						num3 = 2;
						continue;
					case 2:
						goto IL_5A;
					case 3:
						return num;
					}
					break;
					IL_5A:
					num3 = 1;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06002B1B RID: 11035 RVA: 0x00180074 File Offset: 0x0017F074
	private int ᜀ(Stream A_0, object A_1, PropertyType A_2)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 3;
				for (;;)
				{
					DateTime dateTime;
					switch (num2)
					{
					case 0:
						num2 = 25;
						continue;
					case 1:
						num2 = 20;
						continue;
					case 2:
						if (this.ᜅ() != 10)
						{
							num2 = 5;
							continue;
						}
						goto IL_1D6;
					case 3:
						if (A_2 <= PropertyType.Int)
						{
							num2 = 17;
							continue;
						}
						num2 = 4;
						continue;
					case 4:
						switch (A_2)
						{
						case PropertyType.AsciiString:
							num += spr\u23D6.ᜀ(A_0, (string)A_1, false);
							num2 = 10;
							continue;
						case PropertyType.String:
							num += spr\u23D6.ᜀ(A_0, (string)A_1);
							num2 = 18;
							continue;
						default:
							num2 = 0;
							continue;
						}
						break;
					case 5:
						dateTime = dateTime.ToUniversalTime();
						num2 = 29;
						continue;
					case 6:
						goto IL_F8;
					case 7:
						goto IL_1D1;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C5;
						default:
							goto IL_29A;
						}
						break;
					case 9:
						num2 = 13;
						continue;
					case 10:
						goto IL_17D;
					case 11:
					{
						bool flag;
						num += spr\u23D6.ᜂ(A_0, flag ? 1 : 0);
						num2 = 12;
						continue;
					}
					case 12:
						goto IL_22A;
					case 13:
					{
						if (A_2 != PropertyType.ClipboardData)
						{
							num2 = 1;
							continue;
						}
						ClipboardData a_ = (ClipboardData)A_1;
						num += this.ᜀ(A_0, a_);
						num2 = 22;
						continue;
					}
					case 14:
						goto IL_23B;
					case 15:
						goto IL_212;
					case 16:
						switch (A_2)
						{
						case PropertyType.Empty:
						case PropertyType.Null:
							return num;
						case PropertyType.Int16:
							num += spr\u23D6.ᜀ(A_0, (short)A_1);
							num2 = 26;
							continue;
						case PropertyType.Int32:
							goto IL_2A5;
						case (PropertyType)4:
						case (PropertyType)6:
						case (PropertyType)7:
						case (PropertyType)8:
						case (PropertyType)9:
						case (PropertyType)10:
							goto IL_3CF;
						case PropertyType.Double:
							num += spr\u23D6.ᜀ(A_0, (double)A_1);
							num2 = 30;
							continue;
						case PropertyType.Bool:
						{
							bool flag = (bool)A_1;
							num2 = 11;
							continue;
						}
						case PropertyType.Object:
							num += this.ᜀ(A_0, A_1);
							num2 = 6;
							continue;
						default:
							num2 = 21;
							continue;
						}
						break;
					case 17:
						goto IL_2C5;
					case 18:
						goto IL_19D;
					case 19:
						if (A_2 != PropertyType.UInt32)
						{
							num2 = 23;
							continue;
						}
						num += spr\u23D6.ᜂ(A_0, (int)((uint)A_1));
						num2 = 7;
						continue;
					case 20:
						goto IL_1AE;
					case 21:
						num2 = 19;
						continue;
					case 22:
						goto IL_3C4;
					case 23:
						num2 = 24;
						continue;
					case 24:
						if (true)
						{
						}
						if (A_2 != PropertyType.Int)
						{
							num2 = 27;
							continue;
						}
						goto IL_2A5;
					case 25:
						switch (A_2)
						{
						case PropertyType.DateTime:
							dateTime = (DateTime)A_1;
							num2 = 2;
							continue;
						case PropertyType.Blob:
						{
							byte[] a_2 = (byte[])A_1;
							num += this.ᜀ(A_0, a_2);
							num2 = 8;
							continue;
						}
						default:
							num2 = 9;
							continue;
						}
						break;
					case 26:
						goto IL_DC;
					case 27:
						num2 = 14;
						continue;
					case 28:
						goto IL_2C0;
					case 29:
						goto IL_1D6;
					case 30:
						return num;
					}
					break;
					IL_1D6:
					ulong value = (ulong)(dateTime.Ticks - 504911232000000000L);
					byte[] bytes = BitConverter.GetBytes(value);
					A_0.Write(bytes, 0, bytes.Length);
					num += bytes.Length;
					num2 = 15;
					continue;
					IL_2A5:
					num += spr\u23D6.ᜂ(A_0, (int)A_1);
					num2 = 28;
					continue;
					IL_2C5:
					num2 = 16;
				}
			}
			IL_DC:
			IL_F8:
			IL_17D:
			IL_19D:
			return num;
			IL_1AE:
			goto IL_3CF;
			IL_1D1:
			IL_212:
			IL_22A:
			return num;
			IL_23B:
			goto IL_3CF;
			IL_29A:
			if (false)
			{
			}
			IL_2C0:
			IL_3C4:
			return num;
			IL_3CF:
			throw new NotImplementedException();
		}
		}
	}

	// Token: 0x06002B1C RID: 11036 RVA: 0x001804A8 File Offset: 0x0017F4A8
	private int ᜀ(Stream A_0, ClipboardData A_1)
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
		return A_1.Serialize(A_0);
	}

	// Token: 0x06002B1D RID: 11037 RVA: 0x001804EC File Offset: 0x0017F4EC
	private int ᜀ(Stream A_0, byte[] A_1)
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
		int num = 0;
		int num2 = A_1.Length;
		num += spr\u23D6.ᜂ(A_0, num2);
		A_0.Write(A_1, 0, num2);
		return num + num2;
	}

	// Token: 0x06002B1E RID: 11038 RVA: 0x00180548 File Offset: 0x0017F548
	public bool ᜀ(object A_0, PropertyType A_1)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					return result;
				case 2:
					switch (A_1)
					{
					case PropertyType.AsciiStringArray:
					case PropertyType.StringArray:
						goto IL_1C2;
					default:
						num = 13;
						continue;
					}
					break;
				case 3:
					if (A_1 != PropertyType.Vector)
					{
						num = 26;
						continue;
					}
					goto IL_1C2;
				case 4:
					if (A_1 != PropertyType.Int)
					{
						num = 14;
						continue;
					}
					goto IL_1C2;
				case 5:
					return result;
				case 6:
					return result;
				case 7:
					if (A_1 != PropertyType.ObjectArray)
					{
						num = 0;
						continue;
					}
					goto IL_1C2;
				case 8:
					num = 16;
					continue;
				case 9:
					if (A_1 <= PropertyType.String)
					{
						num = 19;
						continue;
					}
					num = 17;
					continue;
				case 10:
					switch (A_1)
					{
					case PropertyType.AsciiString:
					case PropertyType.String:
						goto IL_1C2;
					default:
						num = 24;
						continue;
					}
					break;
				case 11:
					return result;
				case 12:
					if (A_1 != PropertyType.UInt32)
					{
						num = 15;
						continue;
					}
					goto IL_1C2;
				case 13:
					num = 1;
					continue;
				case 14:
					num = 10;
					continue;
				case 15:
					num = 11;
					continue;
				case 16:
					switch (A_1)
					{
					case PropertyType.Empty:
					case PropertyType.Null:
					case PropertyType.Int16:
					case PropertyType.Int32:
					case PropertyType.Double:
					case PropertyType.Bool:
					case PropertyType.Object:
						goto IL_1C2;
					case (PropertyType)4:
					case (PropertyType)6:
					case (PropertyType)7:
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)10:
						return result;
					default:
						num = 18;
						continue;
					}
					break;
				case 17:
					goto IL_2B6;
				case 18:
					num = 12;
					continue;
				case 19:
					num = 21;
					continue;
				case 20:
					switch (A_1)
					{
					case PropertyType.DateTime:
					case PropertyType.Blob:
						goto IL_1C2;
					default:
						num = 23;
						continue;
					}
					break;
				case 21:
					if (A_1 <= PropertyType.UInt32)
					{
						num = 8;
						continue;
					}
					num = 4;
					continue;
				case 22:
					goto IL_A3;
				case 23:
					num = 3;
					continue;
				case 24:
					num = 5;
					continue;
				case 25:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B6;
					default:
						if (false)
						{
						}
						num = 20;
						continue;
					}
					break;
				case 26:
					num = 22;
					continue;
				}
				break;
				IL_1C2:
				this.ᜀ(A_0);
				this.ᜀ((VarEnum)A_1);
				result = true;
				num = 6;
				continue;
				IL_2B6:
				if (A_1 <= PropertyType.Vector)
				{
					num = 25;
				}
				else
				{
					num = 7;
				}
			}
		}
		IL_A3:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06002B1F RID: 11039 RVA: 0x00180828 File Offset: 0x0017F828
	public object ᜃ()
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

	// Token: 0x06002B20 RID: 11040 RVA: 0x0018086C File Offset: 0x0017F86C
	public void ᜀ(object A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06002B21 RID: 11041 RVA: 0x001808B0 File Offset: 0x0017F8B0
	public VarEnum ᜄ()
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
		return (VarEnum)this.ᜄ;
	}

	// Token: 0x06002B22 RID: 11042 RVA: 0x001808F4 File Offset: 0x0017F8F4
	public void ᜀ(VarEnum A_0)
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
		this.ᜄ = (PropertyType)A_0;
	}

	// Token: 0x06002B23 RID: 11043 RVA: 0x00180938 File Offset: 0x0017F938
	public string ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x06002B24 RID: 11044 RVA: 0x0018097C File Offset: 0x0017F97C
	public void ᜀ(string A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x001809C0 File Offset: 0x0017F9C0
	public int ᜁ(object A_0)
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
		spr\u2129 spr_u = (spr\u2129)A_0;
		return this.ᜅ() - spr_u.ᜅ();
	}

	// Token: 0x04001433 RID: 5171
	private const int ᜀ = 16777216;

	// Token: 0x04001434 RID: 5172
	private const int ᜁ = 0;

	// Token: 0x04001435 RID: 5173
	private int ᜂ;

	// Token: 0x04001436 RID: 5174
	private string ᜃ;

	// Token: 0x04001437 RID: 5175
	public PropertyType ᜄ;

	// Token: 0x04001438 RID: 5176
	public object ᜅ;
}
