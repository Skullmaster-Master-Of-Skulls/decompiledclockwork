using System;
using System.Collections;
using System.Text;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x0200032A RID: 810
	public class TernaryTree : ICloneable
	{
		// Token: 0x06001D56 RID: 7510 RVA: 0x000B0411 File Offset: 0x000AF411
		internal TernaryTree()
		{
			this.Init();
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x000B0420 File Offset: 0x000AF420
		protected void Init()
		{
			this.root = '\0';
			this.freenode = '\u0001';
			this.length = 0;
			this.lo = new char[TernaryTree.BLOCK_SIZE];
			this.hi = new char[TernaryTree.BLOCK_SIZE];
			this.eq = new char[TernaryTree.BLOCK_SIZE];
			this.sc = new char[TernaryTree.BLOCK_SIZE];
			this.kv = new CharVector();
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000B0490 File Offset: 0x000AF490
		public void Insert(string key, char val)
		{
			int num = key.Length + 1;
			if ((int)this.freenode + num > this.eq.Length)
			{
				this.RedimNodeArrays(this.eq.Length + TernaryTree.BLOCK_SIZE);
			}
			char[] array = new char[num--];
			key.CopyTo(0, array, 0, num);
			array[num] = '\0';
			this.root = this.Insert(this.root, array, 0, val);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000B04FC File Offset: 0x000AF4FC
		public void Insert(char[] key, int start, char val)
		{
			int num = TernaryTree.Strlen(key) + 1;
			if ((int)this.freenode + num > this.eq.Length)
			{
				this.RedimNodeArrays(this.eq.Length + TernaryTree.BLOCK_SIZE);
			}
			this.root = this.Insert(this.root, key, start, val);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000B0550 File Offset: 0x000AF550
		private char Insert(char p, char[] key, int start, char val)
		{
			int num = TernaryTree.Strlen(key, start);
			if (p == '\0')
			{
				char c;
				this.freenode = (c = this.freenode) + '\u0001';
				p = c;
				this.eq[(int)p] = val;
				this.length++;
				this.hi[(int)p] = '\0';
				if (num > 0)
				{
					this.sc[(int)p] = char.MaxValue;
					this.lo[(int)p] = (char)this.kv.Alloc(num + 1);
					TernaryTree.Strcpy(this.kv.Arr, (int)this.lo[(int)p], key, start);
				}
				else
				{
					this.sc[(int)p] = '\0';
					this.lo[(int)p] = '\0';
				}
				return p;
			}
			if (this.sc[(int)p] == '￿')
			{
				char c2;
				this.freenode = (c2 = this.freenode) + '\u0001';
				char c3 = c2;
				this.lo[(int)c3] = this.lo[(int)p];
				this.eq[(int)c3] = this.eq[(int)p];
				this.lo[(int)p] = '\0';
				if (num <= 0)
				{
					this.sc[(int)c3] = char.MaxValue;
					this.hi[(int)p] = c3;
					this.sc[(int)p] = '\0';
					this.eq[(int)p] = val;
					this.length++;
					return p;
				}
				this.sc[(int)p] = this.kv[(int)this.lo[(int)c3]];
				this.eq[(int)p] = c3;
				char[] array = this.lo;
				char c4 = c3;
				array[(int)c4] = array[(int)c4] + '\u0001';
				if (this.kv[(int)this.lo[(int)c3]] == '\0')
				{
					this.lo[(int)c3] = '\0';
					this.sc[(int)c3] = '\0';
					this.hi[(int)c3] = '\0';
				}
				else
				{
					this.sc[(int)c3] = char.MaxValue;
				}
			}
			char c5 = key[start];
			if (c5 < this.sc[(int)p])
			{
				this.lo[(int)p] = this.Insert(this.lo[(int)p], key, start, val);
			}
			else if (c5 == this.sc[(int)p])
			{
				if (c5 != '\0')
				{
					this.eq[(int)p] = this.Insert(this.eq[(int)p], key, start + 1, val);
				}
				else
				{
					this.eq[(int)p] = val;
				}
			}
			else
			{
				this.hi[(int)p] = this.Insert(this.hi[(int)p], key, start, val);
			}
			return p;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000B0782 File Offset: 0x000AF782
		public static int Strcmp(char[] a, int startA, char[] b, int startB)
		{
			while (a[startA] == b[startB])
			{
				if (a[startA] == '\0')
				{
					return 0;
				}
				startA++;
				startB++;
			}
			return (int)(a[startA] - b[startB]);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x000B07A8 File Offset: 0x000AF7A8
		public static int Strcmp(string str, char[] a, int start)
		{
			int num = str.Length;
			int i;
			for (i = 0; i < num; i++)
			{
				int num2 = (int)(str[i] - a[start + i]);
				if (num2 != 0)
				{
					return num2;
				}
				if (a[start + i] == '\0')
				{
					return num2;
				}
			}
			if (a[start + i] != '\0')
			{
				return (int)(-(int)a[start + i]);
			}
			return 0;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000B07F3 File Offset: 0x000AF7F3
		public static void Strcpy(char[] dst, int di, char[] src, int si)
		{
			while (src[si] != '\0')
			{
				dst[di++] = src[si++];
			}
			dst[di] = '\0';
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000B0810 File Offset: 0x000AF810
		public static int Strlen(char[] a, int start)
		{
			int num = 0;
			int num2 = start;
			while (num2 < a.Length && a[num2] != '\0')
			{
				num++;
				num2++;
			}
			return num;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000B0837 File Offset: 0x000AF837
		public static int Strlen(char[] a)
		{
			return TernaryTree.Strlen(a, 0);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000B0840 File Offset: 0x000AF840
		public int Find(string key)
		{
			int num = key.Length;
			char[] array = new char[num + 1];
			key.CopyTo(0, array, 0, num);
			array[num] = '\0';
			return this.Find(array, 0);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000B0874 File Offset: 0x000AF874
		public int Find(char[] key, int start)
		{
			char c = this.root;
			int num = start;
			while (c != '\0')
			{
				if (this.sc[(int)c] == '￿')
				{
					if (TernaryTree.Strcmp(key, num, this.kv.Arr, (int)this.lo[(int)c]) == 0)
					{
						return (int)this.eq[(int)c];
					}
					return -1;
				}
				else
				{
					char c2 = key[num];
					int num2 = (int)(c2 - this.sc[(int)c]);
					if (num2 == 0)
					{
						if (c2 == '\0')
						{
							return (int)this.eq[(int)c];
						}
						num++;
						c = this.eq[(int)c];
					}
					else if (num2 < 0)
					{
						c = this.lo[(int)c];
					}
					else
					{
						c = this.hi[(int)c];
					}
				}
			}
			return -1;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000B090B File Offset: 0x000AF90B
		public bool Knows(string key)
		{
			return this.Find(key) >= 0;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000B091C File Offset: 0x000AF91C
		private void RedimNodeArrays(int newsize)
		{
			int num = (newsize < this.lo.Length) ? newsize : this.lo.Length;
			char[] destinationArray = new char[newsize];
			Array.Copy(this.lo, 0, destinationArray, 0, num);
			this.lo = destinationArray;
			destinationArray = new char[newsize];
			Array.Copy(this.hi, 0, destinationArray, 0, num);
			this.hi = destinationArray;
			destinationArray = new char[newsize];
			Array.Copy(this.eq, 0, destinationArray, 0, num);
			this.eq = destinationArray;
			destinationArray = new char[newsize];
			Array.Copy(this.sc, 0, destinationArray, 0, num);
			this.sc = destinationArray;
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x000B09B4 File Offset: 0x000AF9B4
		public int Size
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000B09BC File Offset: 0x000AF9BC
		public object Clone()
		{
			return new TernaryTree
			{
				lo = (char[])this.lo.Clone(),
				hi = (char[])this.hi.Clone(),
				eq = (char[])this.eq.Clone(),
				sc = (char[])this.sc.Clone(),
				kv = (CharVector)this.kv.Clone(),
				root = this.root,
				freenode = this.freenode,
				length = this.length
			};
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000B0A64 File Offset: 0x000AFA64
		protected void InsertBalanced(string[] k, char[] v, int offset, int n)
		{
			if (n < 1)
			{
				return;
			}
			int num = n >> 1;
			this.Insert(k[num + offset], v[num + offset]);
			this.InsertBalanced(k, v, offset, num);
			this.InsertBalanced(k, v, offset + num + 1, n - num - 1);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000B0AAC File Offset: 0x000AFAAC
		public void Balance()
		{
			int num = 0;
			int num2 = this.length;
			string[] array = new string[num2];
			char[] array2 = new char[num2];
			TernaryTree.Iterator iterator = new TernaryTree.Iterator(this);
			while (iterator.HasMoreElements())
			{
				array2[num] = iterator.Value;
				array[num++] = (string)iterator.NextElement();
			}
			this.Init();
			this.InsertBalanced(array, array2, 0, num2);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000B0B10 File Offset: 0x000AFB10
		public void TrimToSize()
		{
			this.Balance();
			this.RedimNodeArrays((int)this.freenode);
			CharVector charVector = new CharVector();
			charVector.Alloc(1);
			TernaryTree map = new TernaryTree();
			this.Compact(charVector, map, this.root);
			this.kv = charVector;
			this.kv.TrimToSize();
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000B0B64 File Offset: 0x000AFB64
		private void Compact(CharVector kx, TernaryTree map, char p)
		{
			if (p == '\0')
			{
				return;
			}
			if (this.sc[(int)p] == '￿')
			{
				int num = map.Find(this.kv.Arr, (int)this.lo[(int)p]);
				if (num < 0)
				{
					num = kx.Alloc(TernaryTree.Strlen(this.kv.Arr, (int)this.lo[(int)p]) + 1);
					TernaryTree.Strcpy(kx.Arr, num, this.kv.Arr, (int)this.lo[(int)p]);
					map.Insert(kx.Arr, num, (char)num);
				}
				this.lo[(int)p] = (char)num;
				return;
			}
			this.Compact(kx, map, this.lo[(int)p]);
			if (this.sc[(int)p] != '\0')
			{
				this.Compact(kx, map, this.eq[(int)p]);
			}
			this.Compact(kx, map, this.hi[(int)p]);
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x000B0C36 File Offset: 0x000AFC36
		public TernaryTree.Iterator Keys
		{
			get
			{
				return new TernaryTree.Iterator(this);
			}
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x000B0C40 File Offset: 0x000AFC40
		public virtual void PrintStats()
		{
			Console.Error.WriteLine("Number of keys = " + this.length.ToString());
			Console.Error.WriteLine("Node count = " + this.freenode.ToString());
			Console.Error.WriteLine("Key Array length = " + this.kv.Length.ToString());
		}

		// Token: 0x0400142F RID: 5167
		protected char[] lo;

		// Token: 0x04001430 RID: 5168
		protected char[] hi;

		// Token: 0x04001431 RID: 5169
		protected char[] eq;

		// Token: 0x04001432 RID: 5170
		protected char[] sc;

		// Token: 0x04001433 RID: 5171
		protected CharVector kv;

		// Token: 0x04001434 RID: 5172
		protected char root;

		// Token: 0x04001435 RID: 5173
		protected char freenode;

		// Token: 0x04001436 RID: 5174
		protected int length;

		// Token: 0x04001437 RID: 5175
		protected static int BLOCK_SIZE = 2048;

		// Token: 0x0200032B RID: 811
		public class Iterator
		{
			// Token: 0x06001D6D RID: 7533 RVA: 0x000B0CBE File Offset: 0x000AFCBE
			public Iterator(TernaryTree parent)
			{
				this.parent = parent;
				this.cur = -1;
				this.ns = new Stack();
				this.ks = new StringBuilder();
				this.Rewind();
			}

			// Token: 0x06001D6E RID: 7534 RVA: 0x000B0CF0 File Offset: 0x000AFCF0
			public void Rewind()
			{
				this.ns.Clear();
				this.ks.Length = 0;
				this.cur = (int)this.parent.root;
				this.Run();
			}

			// Token: 0x06001D6F RID: 7535 RVA: 0x000B0D24 File Offset: 0x000AFD24
			public object NextElement()
			{
				string result = this.curkey;
				this.cur = this.Up();
				this.Run();
				return result;
			}

			// Token: 0x1700052B RID: 1323
			// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000B0D4C File Offset: 0x000AFD4C
			public char Value
			{
				get
				{
					if (this.cur >= 0)
					{
						return this.parent.eq[this.cur];
					}
					return '\0';
				}
			}

			// Token: 0x06001D71 RID: 7537 RVA: 0x000B0D6B File Offset: 0x000AFD6B
			public bool HasMoreElements()
			{
				return this.cur != -1;
			}

			// Token: 0x06001D72 RID: 7538 RVA: 0x000B0D7C File Offset: 0x000AFD7C
			private int Up()
			{
				TernaryTree.Iterator.Item item = new TernaryTree.Iterator.Item();
				int result = 0;
				if (this.ns.Count == 0)
				{
					return -1;
				}
				if (this.cur != 0 && this.parent.sc[this.cur] == '\0')
				{
					return (int)this.parent.lo[this.cur];
				}
				bool flag = true;
				while (flag)
				{
					item = (TernaryTree.Iterator.Item)this.ns.Pop();
					TernaryTree.Iterator.Item item2 = item;
					item2.child += '\u0001';
					switch (item.child)
					{
					case '\u0001':
						if (this.parent.sc[(int)item.parent] != '\0')
						{
							result = (int)this.parent.eq[(int)item.parent];
							this.ns.Push(item.Clone());
							this.ks.Append(this.parent.sc[(int)item.parent]);
						}
						else
						{
							TernaryTree.Iterator.Item item3 = item;
							item3.child += '\u0001';
							this.ns.Push(item.Clone());
							result = (int)this.parent.hi[(int)item.parent];
						}
						flag = false;
						break;
					case '\u0002':
						result = (int)this.parent.hi[(int)item.parent];
						this.ns.Push(item.Clone());
						if (this.ks.Length > 0)
						{
							this.ks.Length = this.ks.Length - 1;
						}
						flag = false;
						break;
					default:
						if (this.ns.Count == 0)
						{
							return -1;
						}
						flag = true;
						break;
					}
				}
				return result;
			}

			// Token: 0x06001D73 RID: 7539 RVA: 0x000B0F08 File Offset: 0x000AFF08
			private int Run()
			{
				if (this.cur == -1)
				{
					return -1;
				}
				bool flag = false;
				do
				{
					if (this.cur != 0)
					{
						if (this.parent.sc[this.cur] == '￿')
						{
							flag = true;
						}
						else
						{
							this.ns.Push(new TernaryTree.Iterator.Item((char)this.cur, '\0'));
							if (this.parent.sc[this.cur] != '\0')
							{
								this.cur = (int)this.parent.lo[this.cur];
								continue;
							}
							flag = true;
						}
					}
					if (flag)
					{
						goto IL_96;
					}
					this.cur = this.Up();
				}
				while (this.cur != -1);
				return -1;
				IL_96:
				StringBuilder stringBuilder = new StringBuilder(this.ks.ToString());
				if (this.parent.sc[this.cur] == '￿')
				{
					int index = (int)this.parent.lo[this.cur];
					while (this.parent.kv[index] != '\0')
					{
						stringBuilder.Append(this.parent.kv[index++]);
					}
				}
				this.curkey = stringBuilder.ToString();
				return 0;
			}

			// Token: 0x04001438 RID: 5176
			private int cur;

			// Token: 0x04001439 RID: 5177
			private string curkey;

			// Token: 0x0400143A RID: 5178
			private TernaryTree parent;

			// Token: 0x0400143B RID: 5179
			private Stack ns;

			// Token: 0x0400143C RID: 5180
			private StringBuilder ks;

			// Token: 0x0200032C RID: 812
			private class Item : ICloneable
			{
				// Token: 0x06001D74 RID: 7540 RVA: 0x000B1026 File Offset: 0x000B0026
				public Item()
				{
					this.parent = '\0';
					this.child = '\0';
				}

				// Token: 0x06001D75 RID: 7541 RVA: 0x000B103C File Offset: 0x000B003C
				public Item(char p, char c)
				{
					this.parent = p;
					this.child = c;
				}

				// Token: 0x06001D76 RID: 7542 RVA: 0x000B1052 File Offset: 0x000B0052
				public object Clone()
				{
					return new TernaryTree.Iterator.Item(this.parent, this.child);
				}

				// Token: 0x0400143D RID: 5181
				internal char parent;

				// Token: 0x0400143E RID: 5182
				internal char child;
			}
		}
	}
}
