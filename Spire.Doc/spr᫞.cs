using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Spire.CompoundFile.Doc;

// Token: 0x02000169 RID: 361
internal class spr\u1ADE : spr\u2097, IComparable
{
	// Token: 0x06000C32 RID: 3122 RVA: 0x000CE128 File Offset: 0x000CD128
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

	// Token: 0x06000C33 RID: 3123 RVA: 0x000CE178 File Offset: 0x000CD178
	public int ᜁ()
	{
		if (!this.ᜂ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3F;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜅ();
		}
		IL_3F:
		return this.ᜅ() - 16777216;
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x000CE1D0 File Offset: 0x000CD1D0
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

	// Token: 0x06000C35 RID: 3125 RVA: 0x000CE214 File Offset: 0x000CD214
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

	// Token: 0x06000C36 RID: 3126 RVA: 0x000CE258 File Offset: 0x000CD258
	internal spr\u1ADE()
	{
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x000CE26C File Offset: 0x000CD26C
	public spr\u1ADE(int A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x000CE288 File Offset: 0x000CD288
	public void ᜂ(Stream A_0, int A_1)
	{
		for (;;)
		{
			byte[] a_ = new byte[4];
			this.ᜄ = (PropertyType)sprữ.ᜁ(A_0, a_);
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_58;
				case 1:
					this.ᜅ = this.ᜁ(A_0, A_1);
					num = 5;
					continue;
				case 2:
					this.ᜄ &= (PropertyType)(-31);
					this.ᜄ |= PropertyType.String;
					num = 3;
					continue;
				case 3:
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if ((this.ᜄ & PropertyType.AsciiString) == PropertyType.AsciiString)
						{
							num = 2;
							continue;
						}
						return;
					}
					break;
				case 5:
					goto IL_58;
				case 6:
					if ((this.ᜄ & PropertyType.Vector) != PropertyType.Empty)
					{
						num = 1;
						continue;
					}
					this.ᜅ = this.ᜀ(this.ᜄ, A_0, A_1);
					num = 0;
					continue;
				}
				break;
				IL_58:
				num = 4;
			}
		}
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x000CE3AC File Offset: 0x000CD3AC
	private IList ᜁ(Stream A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num2;
			int num3;
			IList list;
			PropertyType a_;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_73:
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (num2 >= num3)
						{
							num = 3;
							continue;
						}
						list[num2] = this.ᜀ(a_, A_0, A_1 - 4);
						num2++;
						num = 2;
						continue;
					case 1:
						goto IL_7E;
					case 2:
						goto IL_7E;
					case 3:
						return list;
					}
					goto IL_4B;
					IL_7E:
					num = 0;
				}
				return list;
			}
			default:
				if (false)
				{
				}
				break;
			}
			IL_4B:
			byte[] a_2 = new byte[4];
			num3 = sprữ.ᜁ(A_0, a_2);
			a_ = (this.ᜄ & ~PropertyType.Vector);
			list = this.ᜀ(a_, num3);
			num2 = 0;
			goto IL_73;
		}
		}
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x000CE484 File Offset: 0x000CD484
	private IList ᜀ(PropertyType A_0, int A_1)
	{
		for (;;)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != PropertyType.Int32)
					{
						num = 10;
						continue;
					}
					goto IL_B9;
				case 1:
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					switch (A_0)
					{
					case PropertyType.AsciiString:
					case PropertyType.String:
						goto IL_DC;
					default:
						num = 3;
						continue;
					}
					break;
				case 3:
					num = 5;
					continue;
				case 4:
					goto IL_12A;
				case 5:
					goto IL_5C;
				case 6:
					goto IL_52;
				case 7:
					if (A_0 <= PropertyType.Object)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 8:
					if (A_0 != PropertyType.Int)
					{
						num = 1;
						continue;
					}
					goto IL_B9;
				case 9:
					if (A_0 != PropertyType.Object)
					{
						num = 11;
						continue;
					}
					goto IL_12C;
				case 10:
					num = 9;
					continue;
				case 11:
					num = 4;
					continue;
				}
				break;
				IL_52:
				num = 0;
			}
		}
		IL_5C:
		goto IL_12C;
		IL_B9:
		return new int[A_1];
		IL_DC:
		return new string[A_1];
		IL_12A:
		IL_12C:
		return new object[A_1];
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x000CE5C4 File Offset: 0x000CD5C4
	private object ᜀ(PropertyType A_0, Stream A_1, int A_2)
	{
		object result;
		for (;;)
		{
			byte[] a_ = new byte[8];
			result = null;
			int num = 20;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					goto IL_184;
				case 2:
					goto IL_169;
				case 3:
					switch (A_0)
					{
					case PropertyType.AsciiString:
						result = sprữ.ᜁ(A_1, A_2 - 4);
						num = 0;
						continue;
					case PropertyType.String:
						result = sprữ.ᜀ(A_1, A_2 - 4);
						num = 14;
						continue;
					default:
						num = 7;
						continue;
					}
					break;
				case 4:
					goto IL_284;
				case 5:
					if (A_0 != PropertyType.ClipboardData)
					{
						num = 23;
						continue;
					}
					result = this.ᜂ(A_1, a_);
					num = 12;
					continue;
				case 6:
					num = 18;
					continue;
				case 7:
					num = 8;
					continue;
				case 8:
					switch (A_0)
					{
					case PropertyType.DateTime:
						result = this.ᜁ(A_1, a_);
						num = 13;
						continue;
					case PropertyType.Blob:
						result = this.ᜃ(A_1, a_);
						num = 9;
						continue;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_16E;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						break;
					}
					break;
				case 9:
					return result;
				case 10:
					num = 5;
					continue;
				case 11:
					num = 22;
					continue;
				case 12:
					goto IL_19D;
				case 13:
					goto IL_D4;
				case 14:
					goto IL_11A;
				case 15:
					return result;
				case 16:
					return result;
				case 17:
					switch (A_0)
					{
					case PropertyType.Empty:
					case PropertyType.Null:
						result = null;
						num = 24;
						continue;
					case PropertyType.Int16:
						result = sprữ.ᜂ(A_1, a_);
						A_1.Position += 2L;
						num = 2;
						continue;
					case PropertyType.Int32:
						goto IL_2E6;
					case (PropertyType)4:
					case (PropertyType)6:
					case (PropertyType)7:
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)10:
						goto IL_206;
					case PropertyType.Double:
						result = sprữ.ᜀ(A_1, a_);
						num = 15;
						continue;
					case PropertyType.Bool:
						result = (sprữ.ᜁ(A_1, a_) != 0);
						num = 27;
						continue;
					case PropertyType.Object:
						goto IL_16E;
					default:
						num = 6;
						continue;
					}
					break;
				case 18:
					if (A_0 != PropertyType.UInt32)
					{
						num = 11;
						continue;
					}
					result = (uint)sprữ.ᜂ(A_1, a_);
					num = 25;
					continue;
				case 19:
					num = 4;
					continue;
				case 20:
					if (A_0 <= PropertyType.Int)
					{
						num = 21;
						continue;
					}
					num = 3;
					continue;
				case 21:
					if (true)
					{
					}
					num = 17;
					continue;
				case 22:
					if (A_0 != PropertyType.Int)
					{
						num = 19;
						continue;
					}
					goto IL_2E6;
				case 23:
					num = 26;
					continue;
				case 24:
					goto IL_201;
				case 25:
					goto IL_BB;
				case 26:
					goto IL_217;
				case 27:
					return result;
				}
				break;
				IL_16E:
				result = this.ᜀ(A_1, A_2 - 4);
				num = 1;
				continue;
				IL_2E6:
				result = sprữ.ᜁ(A_1, a_);
				num = 16;
			}
		}
		IL_BB:
		IL_D4:
		IL_11A:
		IL_169:
		IL_184:
		IL_19D:
		IL_201:
		return result;
		IL_206:
		throw new NotImplementedException();
		IL_217:
		IL_284:
		goto IL_206;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x000CE928 File Offset: 0x000CD928
	private object ᜃ(Stream A_0, byte[] A_1)
	{
		int num = sprữ.ᜁ(A_0, A_1);
		byte[] array = new byte[num];
		if (A_0.Read(array, 0, num) != num)
		{
			if (true)
			{
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
				throw new Exception();
			}
		}
		return array;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x000CE988 File Offset: 0x000CD988
	private object ᜂ(Stream A_0, byte[] A_1)
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
		ClipboardData clipboardData = new ClipboardData();
		clipboardData.Parse(A_0);
		return clipboardData;
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x000CE9D4 File Offset: 0x000CD9D4
	private object ᜁ(Stream A_0, byte[] A_1)
	{
		DateTime dateTime;
		for (;;)
		{
			IL_1C:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_78:
				num = 0;
				break;
			default:
			{
				if (false)
				{
				}
				A_0.Read(A_1, 0, 8);
				long ticks = BitConverter.ToInt64(A_1, 0) + 504911232000000000L;
				dateTime = new DateTime(ticks);
				num = 1;
				break;
			}
			}
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					dateTime = dateTime.ToLocalTime();
					num = 2;
					continue;
				case 1:
					goto IL_64;
				case 2:
					goto IL_95;
				}
				goto IL_1C;
			}
			IL_64:
			if (this.ᜅ() != 10)
			{
				goto IL_78;
			}
			break;
		}
		IL_95:
		return dateTime;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x000CEA80 File Offset: 0x000CDA80
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
		PropertyType a_2 = (PropertyType)sprữ.ᜁ(A_0, a_);
		return this.ᜀ(a_2, A_0, A_1 - 4);
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x000CEAD8 File Offset: 0x000CDAD8
	private int ᜀ(Stream A_0, object A_1)
	{
		PropertyType propertyType;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_9A:
			propertyType = PropertyType.Bool;
			num = 8;
			break;
		default:
			if (false)
			{
			}
			num = 7;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 is double)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
			case 1:
				propertyType = PropertyType.Double;
				num = 10;
				continue;
			case 2:
				propertyType = PropertyType.String;
				num = 4;
				continue;
			case 3:
				if (A_1 is string)
				{
					num = 2;
					continue;
				}
				goto IL_C7;
			case 4:
				goto IL_EA;
			case 5:
				if (A_1 is bool)
				{
					num = 11;
					continue;
				}
				if (true)
				{
				}
				num = 3;
				continue;
			case 6:
				propertyType = PropertyType.Int32;
				num = 9;
				continue;
			case 8:
				goto IL_A5;
			case 9:
				goto IL_DA;
			case 10:
				goto IL_11C;
			case 11:
				goto IL_10D;
			}
			if (A_1 is int)
			{
				num = 6;
			}
			else
			{
				num = 0;
			}
		}
		IL_A5:
		goto IL_11E;
		IL_C7:
		throw new NotImplementedException();
		IL_DA:
		IL_EA:
		goto IL_11E;
		IL_10D:
		goto IL_9A;
		IL_11C:
		IL_11E:
		sprữ.ᜂ(A_0, (int)propertyType);
		return this.ᜀ(A_0, A_1, propertyType) + 4;
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x000CEC18 File Offset: 0x000CDC18
	public int ᜀ(Stream A_0)
	{
		int num;
		for (;;)
		{
			for (;;)
			{
				num = sprữ.ᜂ(A_0, (int)this.ᜄ);
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_EA;
					case 1:
						goto IL_A2;
					case 2:
						if (this.ᜅ() == 0)
						{
							num2 = 8;
							continue;
						}
						num += this.ᜀ(A_0, this.ᜅ, this.ᜄ);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num2 = 7;
							continue;
						}
						break;
					case 3:
						if (this.ᜅ != null)
						{
							num2 = 6;
							continue;
						}
						goto IL_14E;
					case 4:
						num += this.ᜀ(A_0, (IList)this.ᜅ);
						num2 = 0;
						continue;
					case 5:
						if ((this.ᜄ & PropertyType.Vector) == PropertyType.Vector)
						{
							num2 = 4;
							continue;
						}
						num2 = 2;
						continue;
					case 6:
						A_0.Position -= 4L;
						num += this.ᜀ(A_0, (Dictionary<int, string>)this.ᜅ);
						if (true)
						{
						}
						num2 = 1;
						continue;
					case 7:
						goto IL_129;
					case 8:
						num2 = 3;
						continue;
					}
					break;
				}
			}
		}
		IL_A2:
		IL_EA:
		IL_129:
		IL_14E:
		sprữ.ᜀ(A_0, ref num);
		return num;
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x000CED7C File Offset: 0x000CDD7C
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
		num += sprữ.ᜂ(A_0, count);
		using (Dictionary<int, string>.Enumerator enumerator = A_1.GetEnumerator())
		{
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num2 = 3;
					continue;
				case 3:
					goto IL_D9;
				case 4:
				{
					if (!enumerator.MoveNext())
					{
						num2 = 0;
						continue;
					}
					KeyValuePair<int, string> keyValuePair = enumerator.Current;
					num += sprữ.ᜂ(A_0, keyValuePair.Key);
					num += sprữ.ᜁ(A_0, keyValuePair.Value);
					num2 = 1;
					continue;
				}
				}
				IL_B3:
				num2 = 4;
				continue;
				goto IL_B3;
			}
			IL_D9:;
		}
		return num;
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x000CEE84 File Offset: 0x000CDE84
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
				sprữ.ᜂ(A_0, count);
				num = 4;
				PropertyType a_ = this.ᜄ & ~PropertyType.Vector;
				int num2 = 0;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (num2 < count)
							{
								num += this.ᜀ(A_0, A_1[num2], a_);
								num2++;
								num3 = 2;
								continue;
							}
							break;
						}
						num3 = 1;
						continue;
					case 1:
						return num;
					case 2:
						goto IL_5A;
					case 3:
						goto IL_5A;
					}
					break;
					IL_5A:
					num3 = 0;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x000CEF50 File Offset: 0x000CDF50
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
				int num2 = 6;
				for (;;)
				{
					DateTime dateTime;
					switch (num2)
					{
					case 0:
						num2 = 25;
						continue;
					case 1:
						goto IL_425;
					case 2:
						if (true)
						{
						}
						goto IL_425;
					case 3:
						goto IL_425;
					case 4:
						goto IL_32B;
					case 5:
						dateTime = DateTime.FromBinary(((TimeSpan)A_1).Ticks);
						goto IL_2BA;
					case 6:
						if (A_2 <= PropertyType.UInt32)
						{
							num2 = 30;
							continue;
						}
						num2 = 8;
						continue;
					case 7:
						goto IL_425;
					case 8:
						if (A_2 != PropertyType.Int)
						{
							num2 = 0;
							continue;
						}
						goto IL_2EB;
					case 9:
						if (A_1 is TimeSpan)
						{
							num2 = 5;
							continue;
						}
						dateTime = (DateTime)A_1;
						num2 = 23;
						continue;
					case 10:
						if (A_2 != PropertyType.UInt32)
						{
							num2 = 13;
							continue;
						}
						num += sprữ.ᜂ(A_0, (int)((uint)A_1));
						num2 = 16;
						continue;
					case 11:
						goto IL_425;
					case 12:
						goto IL_425;
					case 13:
						num2 = 20;
						continue;
					case 14:
						switch (A_2)
						{
						case PropertyType.DateTime:
							num2 = 9;
							continue;
						case PropertyType.Blob:
						{
							byte[] a_ = (byte[])A_1;
							num += this.ᜀ(A_0, a_);
							num2 = 29;
							continue;
						}
						default:
							num2 = 22;
							continue;
						}
						break;
					case 15:
					{
						bool flag;
						num += sprữ.ᜂ(A_0, flag ? 1 : 0);
						num2 = 28;
						continue;
					}
					case 16:
						goto IL_425;
					case 17:
						switch (A_2)
						{
						case PropertyType.Empty:
						case PropertyType.Null:
							goto IL_425;
						case PropertyType.Int16:
							num += sprữ.ᜀ(A_0, (short)A_1);
							num2 = 1;
							continue;
						case PropertyType.Int32:
							goto IL_2EB;
						case (PropertyType)4:
						case (PropertyType)6:
						case (PropertyType)7:
						case (PropertyType)8:
						case (PropertyType)9:
						case (PropertyType)10:
							goto IL_29F;
						case PropertyType.Double:
							num += sprữ.ᜀ(A_0, (double)A_1);
							num2 = 3;
							continue;
						case PropertyType.Bool:
						{
							bool flag = (bool)A_1;
							num2 = 15;
							continue;
						}
						case PropertyType.Object:
							num += this.ᜀ(A_0, A_1);
							num2 = 11;
							continue;
						default:
							num2 = 18;
							continue;
						}
						break;
					case 18:
						num2 = 10;
						continue;
					case 19:
						dateTime = dateTime.ToUniversalTime();
						num2 = 4;
						continue;
					case 20:
						goto IL_1CE;
					case 21:
						goto IL_3CC;
					case 22:
						num2 = 27;
						continue;
					case 23:
						goto IL_3CC;
					case 24:
						num2 = 14;
						continue;
					case 25:
						switch (A_2)
						{
						case PropertyType.AsciiString:
							num += sprữ.ᜁ(A_0, (string)A_1);
							num2 = 7;
							continue;
						case PropertyType.String:
							num += sprữ.ᜀ(A_0, (string)A_1);
							num2 = 12;
							continue;
						default:
							num2 = 24;
							continue;
						}
						break;
					case 26:
						if (this.ᜅ() != 10)
						{
							num2 = 19;
							continue;
						}
						goto IL_32B;
					case 27:
						goto IL_172;
					case 28:
						goto IL_425;
					case 29:
						goto IL_425;
					case 30:
						num2 = 17;
						continue;
					case 31:
						goto IL_425;
					}
					break;
					IL_2BA:
					num2 = 21;
					continue;
					IL_425:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2BA;
					default:
						goto IL_43B;
					}
					IL_2EB:
					num += sprữ.ᜂ(A_0, (int)A_1);
					num2 = 31;
					continue;
					IL_32B:
					ulong value = (ulong)(dateTime.Ticks - 504911232000000000L);
					byte[] bytes = BitConverter.GetBytes(value);
					A_0.Write(bytes, 0, bytes.Length);
					num += bytes.Length;
					num2 = 2;
					continue;
					IL_3CC:
					num2 = 26;
				}
			}
			IL_172:
			IL_1CE:
			IL_29F:
			throw new NotImplementedException();
			IL_43B:
			if (false)
			{
			}
			return num;
		}
		}
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x000CF3A0 File Offset: 0x000CE3A0
	private int ᜀ(Stream A_0, byte[] A_1)
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
		int num = 0;
		int num2 = A_1.Length;
		num += sprữ.ᜂ(A_0, num2);
		A_0.Write(A_1, 0, num2);
		return num + num2;
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x000CF3FC File Offset: 0x000CE3FC
	public bool ᜀ(object A_0, PropertyType A_1)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_1)
					{
					case PropertyType.AsciiString:
					case PropertyType.String:
						goto IL_1C7;
					default:
						num = 14;
						continue;
					}
					break;
				case 1:
					switch (A_1)
					{
					case PropertyType.AsciiStringArray:
					case PropertyType.StringArray:
						goto IL_1C7;
					default:
						num = 6;
						continue;
					}
					break;
				case 2:
					num = 10;
					continue;
				case 3:
					switch (A_1)
					{
					case PropertyType.Empty:
					case PropertyType.Null:
					case PropertyType.Int16:
					case PropertyType.Int32:
					case PropertyType.Double:
					case PropertyType.Bool:
					case PropertyType.Object:
						goto IL_1C7;
					case (PropertyType)4:
					case (PropertyType)6:
					case (PropertyType)7:
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)10:
						goto IL_2CC;
					default:
						num = 9;
						continue;
					}
					break;
				case 4:
					num = 19;
					continue;
				case 5:
					if (A_1 != PropertyType.Vector)
					{
						num = 24;
						continue;
					}
					goto IL_1C7;
				case 6:
					num = 12;
					continue;
				case 7:
					switch (A_1)
					{
					case PropertyType.DateTime:
					case PropertyType.Blob:
						goto IL_1C7;
					default:
						num = 17;
						continue;
					}
					break;
				case 8:
					num = 7;
					continue;
				case 9:
					num = 15;
					continue;
				case 10:
					if (A_1 <= PropertyType.UInt32)
					{
						num = 18;
						continue;
					}
					num = 23;
					continue;
				case 11:
					num = 0;
					continue;
				case 12:
					goto IL_212;
				case 13:
					if (A_1 <= PropertyType.String)
					{
						num = 2;
						continue;
					}
					num = 22;
					continue;
				case 14:
					num = 21;
					continue;
				case 15:
					if (A_1 != PropertyType.UInt32)
					{
						goto IL_110;
					}
					goto IL_1C7;
				case 16:
					goto IL_1E2;
				case 17:
					num = 5;
					continue;
				case 18:
					num = 3;
					continue;
				case 19:
					goto IL_202;
				case 20:
					if (A_1 != PropertyType.ObjectArray)
					{
						num = 26;
						continue;
					}
					goto IL_1C7;
				case 21:
					goto IL_1F2;
				case 22:
					if (A_1 <= PropertyType.Vector)
					{
						num = 8;
						continue;
					}
					num = 20;
					continue;
				case 23:
					if (A_1 != PropertyType.Int)
					{
						num = 11;
						continue;
					}
					goto IL_1C7;
				case 24:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_110;
					default:
						if (false)
						{
						}
						num = 25;
						continue;
					}
					break;
				case 25:
					goto IL_CC;
				case 26:
					num = 1;
					continue;
				}
				break;
				IL_110:
				num = 4;
				continue;
				IL_1C7:
				this.ᜀ(A_0);
				this.ᜀ((VarEnum)A_1);
				result = true;
				num = 16;
			}
		}
		IL_CC:
		IL_1E2:
		IL_1F2:
		IL_202:
		IL_212:
		IL_2CC:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x000CF6E0 File Offset: 0x000CE6E0
	public object ᜃ()
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
		return this.ᜅ;
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x000CF724 File Offset: 0x000CE724
	public void ᜀ(object A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x000CF768 File Offset: 0x000CE768
	public VarEnum ᜄ()
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
		return (VarEnum)this.ᜄ;
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x000CF7AC File Offset: 0x000CE7AC
	public void ᜀ(VarEnum A_0)
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
		this.ᜄ = (PropertyType)A_0;
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x000CF7F0 File Offset: 0x000CE7F0
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

	// Token: 0x06000C4C RID: 3148 RVA: 0x000CF834 File Offset: 0x000CE834
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

	// Token: 0x06000C4D RID: 3149 RVA: 0x000CF878 File Offset: 0x000CE878
	public int ᜁ(object A_0)
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
		spr\u1ADE spr_u1ADE = (spr\u1ADE)A_0;
		return this.ᜅ() - spr_u1ADE.ᜅ();
	}

	// Token: 0x04001416 RID: 5142
	private const int ᜀ = 16777216;

	// Token: 0x04001417 RID: 5143
	private const int ᜁ = 0;

	// Token: 0x04001418 RID: 5144
	private int ᜂ;

	// Token: 0x04001419 RID: 5145
	private string ᜃ;

	// Token: 0x0400141A RID: 5146
	public PropertyType ᜄ;

	// Token: 0x0400141B RID: 5147
	public object ᜅ;
}
