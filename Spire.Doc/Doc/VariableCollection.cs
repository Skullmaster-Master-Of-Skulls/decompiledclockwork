using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;

namespace Spire.Doc
{
	// Token: 0x020000D8 RID: 216
	public class VariableCollection
	{
		// Token: 0x170000D7 RID: 215
		public string this[string name]
		{
			get
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
					if (!this.ᜀ.ContainsKey(name))
					{
						return null;
					}
					break;
				}
				return this.ᜀ[name];
			}
			set
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
				this.ᜀ[name] = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00019F6C File Offset: 0x00018F6C
		public int Count
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
				return this.ᜀ.Count;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00019FB4 File Offset: 0x00018FB4
		internal Dictionary<string, string> Items
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
				return this.ᜀ;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00019FF8 File Offset: 0x00018FF8
		public VariableCollection()
		{
			this.ᜀ = new Dictionary<string, string>();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001A018 File Offset: 0x00019018
		public void Add(string name, string value)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					value = string.Empty;
					num = 1;
					continue;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_60;
					}
					break;
				}
				IL_26:
				if (value == null)
				{
					num = 0;
					continue;
				}
				goto IL_68;
				goto IL_26;
			}
			IL_60:
			if (false)
			{
			}
			IL_68:
			this.ᜀ.Add(name, value);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0001A09C File Offset: 0x0001909C
		public string GetNameByIndex(int index)
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
			this.ᜀ(index);
			return this.ᜀ(index, true);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001A0E8 File Offset: 0x000190E8
		public string GetValueByIndex(int index)
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
			this.ᜀ(index);
			return this.ᜀ(index, false);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0001A134 File Offset: 0x00019134
		public void Remove(string name)
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
			this.ᜀ.Remove(name);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0001A17C File Offset: 0x0001917C
		internal void ᜀ(byte[] A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					MemoryStream input = new MemoryStream(A_0);
					BinaryReader binaryReader = new BinaryReader(input, Encoding.Unicode);
					binaryReader.ReadInt16();
					int num = (int)binaryReader.ReadInt16();
					binaryReader.ReadInt16();
					string[] array = new string[num];
					int num2 = 0;
					int num3 = 5;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							if (num2 >= num)
							{
								num3 = 4;
								continue;
							}
							int count = (int)binaryReader.ReadUInt16();
							char[] value = binaryReader.ReadChars(count);
							array[num2] = new string(value);
							binaryReader.ReadInt32();
							num2++;
							num3 = 3;
							continue;
						}
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_76;
							default:
							{
								if (false)
								{
								}
								int num4;
								if (num4 >= num)
								{
									num3 = 7;
									continue;
								}
								if (true)
								{
								}
								int count = (int)binaryReader.ReadInt16();
								char[] value2 = binaryReader.ReadChars(count);
								this.ᜀ.Add(array[num4], new string(value2));
								num4++;
								num3 = 6;
								continue;
							}
							}
							break;
						case 2:
							goto IL_112;
						case 3:
							goto IL_150;
						case 4:
						{
							int num4 = 0;
							num3 = 2;
							continue;
						}
						case 5:
							goto IL_76;
						case 6:
							goto IL_112;
						case 7:
							return;
						}
						break;
						IL_112:
						num3 = 1;
						continue;
						IL_150:
						num3 = 0;
						continue;
						IL_76:
						goto IL_150;
					}
				}
				return;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001A300 File Offset: 0x00019300
		internal byte[] ᜀ()
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				MemoryStream memoryStream;
				for (;;)
				{
					BinaryWriter binaryWriter;
					string[] array;
					int num4;
					string[] array2;
					Spire.Doc.Collections.SortedDictionary<string, string> sortedDictionary;
					Dictionary<string, string>.KeyCollection.Enumerator enumerator2;
					int num5;
					switch (num)
					{
					case 0:
						goto IL_69;
					case 1:
						goto IL_2DE;
					case 2:
						goto IL_21C;
					case 3:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2ED;
						default:
							if (false)
							{
							}
							goto IL_6E;
						}
						break;
					case 4:
						goto IL_6E;
					case 6:
						goto IL_1FB;
					case 7:
					{
						int num2;
						short num3;
						if (num2 >= (int)num3)
						{
							num = 1;
							continue;
						}
						binaryWriter.Write((short)array[num2].Length);
						binaryWriter.Write(array[num2].ToCharArray());
						binaryWriter.Write(int.MaxValue);
						num2++;
						num = 4;
						continue;
					}
					case 8:
					{
						try
						{
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_29C;
								case 1:
								{
									IEnumerator<string> enumerator;
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									string text = enumerator.Current;
									array[num4] = text;
									array2[num4] = sortedDictionary[text];
									num4++;
									num = 2;
									continue;
								}
								case 3:
									num = 0;
									continue;
								}
								IL_276:
								num = 1;
								continue;
								goto IL_276;
							}
							IL_29C:
							goto IL_2F2;
						}
						finally
						{
							num = 1;
							for (;;)
							{
								IEnumerator<string> enumerator;
								switch (num)
								{
								case 0:
									goto IL_2DB;
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
							IL_2DB:;
						}
						goto IL_2DE;
						IL_2F2:
						binaryWriter.Write(byte.MaxValue);
						binaryWriter.Write(byte.MaxValue);
						short num3 = (short)this.ᜀ.Count;
						binaryWriter.Write(num3);
						binaryWriter.Write(4);
						int num2 = 0;
						num = 3;
						continue;
					}
					case 9:
					{
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									if (!enumerator2.MoveNext())
									{
										num = 4;
										continue;
									}
									string key = enumerator2.Current;
									sortedDictionary.Add(key, this.ᜀ[key]);
									num = 1;
									continue;
								}
								case 2:
									goto IL_106;
								case 4:
									num = 2;
									continue;
								}
								IL_E0:
								num = 0;
								continue;
								goto IL_E0;
							}
							IL_106:
							goto IL_35F;
						}
						finally
						{
							((IDisposable)enumerator2).Dispose();
						}
						goto IL_119;
						IL_35F:
						IEnumerator<string> enumerator = sortedDictionary.Keys.GetEnumerator();
						num = 8;
						continue;
					}
					case 10:
					{
						short num3;
						if (num5 >= (int)num3)
						{
							num = 2;
							continue;
						}
						goto IL_119;
					}
					case 11:
						goto IL_2ED;
					}
					if (this.ᜀ.Count == 0)
					{
						num = 0;
						continue;
					}
					memoryStream = new MemoryStream();
					binaryWriter = new BinaryWriter(memoryStream, Encoding.Unicode);
					array = new string[this.ᜀ.Count];
					array2 = new string[this.ᜀ.Count];
					num4 = 0;
					sortedDictionary = new Spire.Doc.Collections.SortedDictionary<string, string>();
					enumerator2 = this.ᜀ.Keys.GetEnumerator();
					num = 9;
					continue;
					IL_6E:
					num = 7;
					continue;
					IL_119:
					binaryWriter.Write((short)array2[num5].Length);
					binaryWriter.Write(array2[num5].ToCharArray());
					num5++;
					num = 6;
					continue;
					IL_1FB:
					num = 10;
					continue;
					IL_2ED:
					goto IL_1FB;
					IL_2DE:
					num5 = 0;
					num = 11;
				}
				IL_69:
				return null;
				IL_21C:
				return memoryStream.ToArray();
			}
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0001A6B0 File Offset: 0x000196B0
		private string ᜀ(int A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				DictionaryEntry entry;
				DictionaryEntry entry2;
				for (;;)
				{
					IDictionaryEnumerator dictionaryEnumerator = this.ᜀ.GetEnumerator();
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_EE;
						case 1:
							if (num > A_0)
							{
								num2 = 3;
								continue;
							}
							dictionaryEnumerator.MoveNext();
							num++;
							num2 = 2;
							continue;
						case 2:
							goto IL_EE;
						case 3:
							num2 = 5;
							continue;
						case 4:
							goto IL_E5;
						case 5:
							if (!A_1)
							{
								num2 = 7;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_60;
							default:
								if (false)
								{
								}
								entry = dictionaryEnumerator.Entry;
								num2 = 4;
								continue;
							}
							break;
						case 6:
							goto IL_70;
						case 7:
							goto IL_60;
						}
						break;
						IL_60:
						entry2 = dictionaryEnumerator.Entry;
						num2 = 6;
						continue;
						IL_EE:
						num2 = 1;
					}
				}
				IL_70:
				object obj = entry2.Value;
				goto IL_10F;
				IL_E5:
				obj = entry.Key;
				IL_10F:
				return (string)obj;
			}
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0001A7D4 File Offset: 0x000197D4
		private void ᜀ(int A_0)
		{
			int a_ = 7;
			for (;;)
			{
				IL_09:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (A_0 < this.ᜀ.Count)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_A8;
					case 3:
						num = 1;
						continue;
					}
					if (A_0 < 0)
					{
						goto IL_49;
					}
					if (true)
					{
					}
					num = 3;
				}
			}
			IL_49:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ѬŮᕰᙲ൴", a_), ClipboardData.b("⑬Ůᕰᙲ൴坶ᑸ๺๼୾ꆀꞆﾌ떔漢뾞醠莢쒤즦춨讪솬삮우횲잴鞶춸펺\udcbc톾귂냄꫆ꯈ껊뿌뻐뗒ꇖ룘꧚드뻞菠迢胤铦짨苪菬쿮藰鯲郴ퟶ鷸铺黼諾氀昂欄猆", a_));
			IL_A8:
			goto IL_49;
		}

		// Token: 0x04000C6A RID: 3178
		private float \u25D9\u0098\u00B0\u00AF;

		// Token: 0x04000C6B RID: 3179
		private int \u25D8\u0095\u008F\u0084;

		// Token: 0x04000C6C RID: 3180
		private float \u2593\u008B\u0080\u0088;

		// Token: 0x04000C6D RID: 3181
		private long[] \u2593\u0095\u007F\u00A6;

		// Token: 0x04000C6E RID: 3182
		private string \u2460\u00A2\u00A7\u00A7;

		// Token: 0x04000C6F RID: 3183
		private float \u2460\u00AE\u0096\u009B;

		// Token: 0x04000C70 RID: 3184
		private Dictionary<string, string> ᜀ;
	}
}
