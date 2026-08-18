using System;
using System.Diagnostics;
using Spire.CompoundFile.Doc;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x0200001A RID: 26
	[DebuggerStepThrough]
	public class SortedIntegerList
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00005C60 File Offset: 0x00004C60
		public SortedIntegerList()
		{
			this.ᜁ = new int[16];
			this.ᜂ = new object[16];
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00005C90 File Offset: 0x00004C90
		public SortedIntegerList(int initialCapacity)
		{
			int a_ = 15;
			base..ctor();
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException(ClipboardData.b("ᱴ᥶ၸེᑼṾ삂ﮎ", a_));
			}
			this.ᜁ = new int[initialCapacity];
			this.ᜂ = new object[initialCapacity];
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00005CE0 File Offset: 0x00004CE0
		public static int BinarySearch(int[] array, int index, int length, int value)
		{
			switch (0)
			{
			default:
			{
				int num;
				int num5;
				for (;;)
				{
					for (;;)
					{
						num = index;
						int num2 = index + length - 1;
						int num3 = 4;
						for (;;)
						{
							switch (num3)
							{
							case 0:
							{
								int num4;
								if (num4 == value)
								{
									num3 = 1;
									continue;
								}
								num3 = 8;
								continue;
							}
							case 1:
								return num5;
							case 2:
								goto IL_D4;
							case 3:
							{
								if (num > num2)
								{
									num3 = 6;
									continue;
								}
								num5 = num + num2 >> 1;
								int num4 = array[num5];
								num3 = 0;
								continue;
							}
							case 4:
								goto IL_D4;
							case 5:
								goto IL_D4;
							case 6:
								goto IL_F0;
							case 7:
								num = num5 + 1;
								num3 = 2;
								continue;
							case 8:
							{
								int num4;
								if (num4 < value)
								{
									num3 = 7;
									continue;
								}
								num2 = num5 - 1;
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
									num3 = 5;
									continue;
								}
								break;
							}
							}
							break;
							IL_D4:
							num3 = 3;
						}
					}
				}
				return num5;
				IL_F0:
				return ~num;
			}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00005DF8 File Offset: 0x00004DF8
		public void Add(int key, object value)
		{
			int a_ = 1;
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
				int num = SortedIntegerList.BinarySearch(this.ᜁ, 0, this.ᜃ, key);
				if (num < 0)
				{
					this.ᜀ(~num, key, value);
					return;
				}
				break;
			}
			}
			throw new ArgumentException(ClipboardData.b("ͦᱨ᭪ŬٮተቲŴቶ", a_));
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00005E74 File Offset: 0x00004E74
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00005EB8 File Offset: 0x00004EB8
		public int Capacity
		{
			get
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
				return this.ᜁ.Length;
			}
			set
			{
				int a_ = 8;
				int num = 8;
				int[] destinationArray;
				object[] destinationArray2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 5;
						continue;
					case 1:
						goto IL_8C;
					case 2:
						if (this.ᜃ > 0)
						{
							num = 4;
							continue;
						}
						goto IL_13B;
					case 3:
						goto IL_118;
					case 4:
						Array.Copy(this.ᜁ, 0, destinationArray, 0, this.ᜃ);
						Array.Copy(this.ᜂ, 0, destinationArray2, 0, this.ᜃ);
						num = 1;
						continue;
					case 5:
						if (value < this.ᜃ)
						{
							num = 9;
							continue;
						}
						if (true)
						{
						}
						num = 7;
						continue;
					case 6:
						destinationArray = new int[value];
						destinationArray2 = new object[value];
						num = 2;
						continue;
					case 7:
						if (value > 0)
						{
							num = 6;
							continue;
						}
						this.ᜁ = new int[16];
						this.ᜂ = new object[16];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 9:
						goto IL_139;
					}
					if (value == this.ᜁ.Length)
					{
						return;
					}
					num = 0;
				}
				IL_8C:
				goto IL_13B;
				IL_118:
				return;
				IL_139:
				throw new ArgumentOutOfRangeException(ClipboardData.b("ᡭᅯṱų፵", a_));
				IL_13B:
				this.ᜁ = destinationArray;
				this.ᜂ = destinationArray2;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00006044 File Offset: 0x00005044
		public int Count
		{
			get
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
				return this.ᜃ;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00006088 File Offset: 0x00005088
		public void Clear()
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
			this.ᜃ = 0;
			this.ᜁ = new int[16];
			this.ᜂ = new object[16];
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000060E4 File Offset: 0x000050E4
		protected SortedIntegerList CreateEmptyCopy()
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
			SortedIntegerList sortedIntegerList = (SortedIntegerList)base.MemberwiseClone();
			sortedIntegerList.ᜃ = 0;
			sortedIntegerList.ᜁ = new int[this.ᜁ.Length];
			sortedIntegerList.ᜂ = new object[this.ᜂ.Length];
			return sortedIntegerList;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000615C File Offset: 0x0000515C
		public bool Contains(int key)
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
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000061A4 File Offset: 0x000051A4
		public bool ContainsKey(int key)
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
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000061EC File Offset: 0x000051EC
		public bool ContainsValue(object value)
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
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00006234 File Offset: 0x00005234
		private void ᜀ(int A_0)
		{
			int num = 6;
			int num2;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					goto IL_B3;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B3;
					default:
						if (false)
						{
						}
						if (num2 < A_0)
						{
							num = 4;
							continue;
						}
						goto IL_B7;
					}
					break;
				case 2:
					num = 5;
					continue;
				case 3:
					goto IL_70;
				case 4:
					num2 = A_0;
					num = 3;
					continue;
				case 5:
					num3 = this.ᜁ.Length * 2;
					goto IL_72;
				case 6:
					if (true)
					{
					}
					break;
				}
				if (this.ᜁ.Length != 0)
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
				IL_72:
				num2 = num3;
				num = 1;
				continue;
				IL_B3:
				num3 = 16;
				goto IL_72;
			}
			IL_70:
			IL_B7:
			this.Capacity = num2;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00006300 File Offset: 0x00005300
		public object GetByIndex(int index)
		{
			int a_ = 15;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (index >= this.ᜃ)
					{
						num = 3;
						continue;
					}
					goto IL_94;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 3:
					goto IL_92;
				}
				IL_29:
				if (index >= 0)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				break;
				goto IL_29;
			}
			IL_5B:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᱴ᥶ᵸṺռ", a_));
			IL_92:
			goto IL_5B;
			IL_94:
			return this.ᜂ[index];
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000063AC File Offset: 0x000053AC
		public int GetKey(int index)
		{
			int a_ = 4;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_92;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					if (index >= this.ᜃ)
					{
						num = 1;
						continue;
					}
					goto IL_94;
				}
				IL_29:
				if (true)
				{
				}
				if (index >= 0)
				{
					num = 2;
					continue;
				}
				break;
				goto IL_29;
			}
			IL_5B:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ͩɫ੭ᕯੱ", a_));
			IL_92:
			goto IL_5B;
			IL_94:
			return this.ᜁ[index];
		}

		// Token: 0x17000006 RID: 6
		public object this[int key]
		{
			get
			{
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
					int num = this.IndexOfKey(key);
					if (num >= 0)
					{
						return this.ᜂ[num];
					}
					break;
				}
				}
				return null;
			}
			set
			{
				int num;
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
					num = sprὊ.ᜀ(this.ᜁ, 0, this.ᜃ, key);
					if (num < 0)
					{
						this.ᜀ(~num, key, value);
						return;
					}
					break;
				}
				this.ᜂ[num] = value;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00006518 File Offset: 0x00005518
		public int IndexOfKey(int key)
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
			{
				if (false)
				{
				}
				int num = sprὊ.ᜀ(this.ᜁ, 0, this.ᜃ, key);
				if (num >= 0)
				{
					return num;
				}
				break;
			}
			}
			return -1;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00006570 File Offset: 0x00005570
		public int IndexOfValue(object value)
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
			return Array.IndexOf<object>(this.ᜂ, value, 0, this.ᜃ);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000065C0 File Offset: 0x000055C0
		private void ᜀ(int A_0, int A_1, object A_2)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9E;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_38;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						Array.Copy(this.ᜁ, A_0, this.ᜁ, A_0 + 1, this.ᜃ - A_0);
						Array.Copy(this.ᜂ, A_0, this.ᜂ, A_0 + 1, this.ᜃ - A_0);
						num = 3;
						continue;
					}
					break;
				case 2:
					if (A_0 < this.ᜃ)
					{
						num = 1;
						continue;
					}
					goto IL_F9;
				case 3:
					goto IL_9C;
				case 5:
					this.ᜀ(this.ᜃ + 1);
					num = 0;
					continue;
				}
				goto IL_28;
				IL_38:
				num = 5;
				continue;
				IL_28:
				if (this.ᜃ == this.ᜁ.Length)
				{
					goto IL_38;
				}
				IL_9E:
				num = 2;
			}
			IL_9C:
			IL_F9:
			this.ᜁ[A_0] = A_1;
			this.ᜂ[A_0] = A_2;
			this.ᜃ++;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000066E8 File Offset: 0x000056E8
		public void RemoveAt(int index)
		{
			int a_ = 9;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C8;
				case 1:
					goto IL_10F;
				case 2:
					num = 6;
					continue;
				case 4:
					Array.Copy(this.ᜁ, index + 1, this.ᜁ, index, this.ᜃ - index);
					Array.Copy(this.ᜂ, index + 1, this.ᜂ, index, this.ᜃ - index);
					num = 1;
					continue;
				case 5:
					if (index < this.ᜃ)
					{
						num = 4;
						continue;
					}
					goto IL_111;
				case 6:
					if (index < this.ᜃ)
					{
						this.ᜃ--;
						num = 5;
						continue;
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
						num = 0;
						continue;
					}
					break;
				}
				if (index < 0)
				{
					break;
				}
				num = 2;
			}
			IL_79:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ٮὰᝲၴྲྀ", a_));
			IL_C8:
			goto IL_79;
			IL_10F:
			IL_111:
			if (true)
			{
			}
			this.ᜁ[this.ᜃ] = 0;
			this.ᜂ[this.ᜃ] = null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000682C File Offset: 0x0000582C
		public void Remove(int key)
		{
			for (;;)
			{
				IL_00:
				for (;;)
				{
					IL_42:
					int num = this.IndexOfKey(key);
					int num2 = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							switch (num2)
							{
							case 0:
								this.RemoveAt(num);
								num2 = 2;
								continue;
							case 1:
								if (num >= 0)
								{
									num2 = 0;
									continue;
								}
								return;
							case 2:
								return;
							}
							goto IL_42;
						}
					}
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000068AC File Offset: 0x000058AC
		public void SetByIndex(int index, object value)
		{
			int a_ = 7;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_92;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					if (index >= this.ᜃ)
					{
						num = 0;
						continue;
					}
					goto IL_94;
				}
				IL_29:
				if (index >= 0)
				{
					num = 1;
					continue;
				}
				break;
				goto IL_29;
			}
			IL_53:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(ClipboardData.b("ѬŮᕰᙲ൴", a_));
			IL_92:
			goto IL_53;
			IL_94:
			this.ᜂ[index] = value;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00006958 File Offset: 0x00005958
		public void TrimToSize()
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
			this.Capacity = this.ᜃ;
		}

		// Token: 0x040000C6 RID: 198
		private const int ᜀ = 16;

		// Token: 0x040000C7 RID: 199
		private float[] \u2593\u00AE\u00A2\u0082;

		// Token: 0x040000C8 RID: 200
		private string \u2593\u00A0\u00AD\u00A4;

		// Token: 0x040000C9 RID: 201
		private int[] ᜁ;

		// Token: 0x040000CA RID: 202
		private int \u25D9\u0080\u0095\u0085;

		// Token: 0x040000CB RID: 203
		private string \u25D9\u00A5\u0099\u008B;

		// Token: 0x040000CC RID: 204
		private float[] \u25D8\u00A6\u008B\u0090;

		// Token: 0x040000CD RID: 205
		private object[] ᜂ;

		// Token: 0x040000CE RID: 206
		private long \u25D8\u0084\u0096ª;

		// Token: 0x040000CF RID: 207
		private int ᜃ;
	}
}
