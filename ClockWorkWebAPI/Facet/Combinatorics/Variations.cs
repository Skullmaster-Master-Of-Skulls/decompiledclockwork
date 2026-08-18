using System;
using System.Collections;
using System.Collections.Generic;

namespace Facet.Combinatorics
{
	// Token: 0x02000006 RID: 6
	public class Variations<T> : IMetaCollection<T>, IEnumerable<IList<!0>>, IEnumerable
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002050 File Offset: 0x00000250
		protected Variations()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025EB File Offset: 0x000007EB
		public Variations(IList<T> values, int lowerIndex)
		{
			this.Initialize(values, lowerIndex, GenerateOption.WithoutRepetition);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025FF File Offset: 0x000007FF
		public Variations(IList<T> values, int lowerIndex, GenerateOption type)
		{
			this.Initialize(values, lowerIndex, type);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002614 File Offset: 0x00000814
		public IEnumerator<IList<T>> GetEnumerator()
		{
			bool flag = this.Type == GenerateOption.WithRepetition;
			IEnumerator<IList<T>> result;
			if (flag)
			{
				result = new Variations<T>.EnumeratorWithRepetition(this);
			}
			else
			{
				result = new Variations<T>.EnumeratorWithoutRepetition(this);
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002644 File Offset: 0x00000844
		IEnumerator IEnumerable.GetEnumerator()
		{
			bool flag = this.Type == GenerateOption.WithRepetition;
			IEnumerator result;
			if (flag)
			{
				result = new Variations<T>.EnumeratorWithRepetition(this);
			}
			else
			{
				result = new Variations<T>.EnumeratorWithoutRepetition(this);
			}
			return result;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002674 File Offset: 0x00000874
		public long Count
		{
			get
			{
				bool flag = this.Type == GenerateOption.WithoutRepetition;
				long result;
				if (flag)
				{
					result = this.myPermutations.Count;
				}
				else
				{
					result = (long)Math.Pow((double)this.UpperIndex, (double)this.LowerIndex);
				}
				return result;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000026B8 File Offset: 0x000008B8
		public GenerateOption Type
		{
			get
			{
				return this.myMetaCollectionType;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000026D0 File Offset: 0x000008D0
		public int UpperIndex
		{
			get
			{
				return this.myValues.Count;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000026F0 File Offset: 0x000008F0
		public int LowerIndex
		{
			get
			{
				return this.myLowerIndex;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002708 File Offset: 0x00000908
		private void Initialize(IList<T> values, int lowerIndex, GenerateOption type)
		{
			this.myMetaCollectionType = type;
			this.myLowerIndex = lowerIndex;
			this.myValues = new List<T>();
			this.myValues.AddRange(values);
			bool flag = type == GenerateOption.WithoutRepetition;
			if (flag)
			{
				List<int> list = new List<int>();
				int num = 0;
				for (int i = 0; i < this.myValues.Count; i++)
				{
					bool flag2 = i >= this.myValues.Count - this.myLowerIndex;
					if (flag2)
					{
						list.Add(num++);
					}
					else
					{
						list.Add(int.MaxValue);
					}
				}
				this.myPermutations = new Permutations<int>(list);
			}
		}

		// Token: 0x04000009 RID: 9
		private List<T> myValues;

		// Token: 0x0400000A RID: 10
		private Permutations<int> myPermutations;

		// Token: 0x0400000B RID: 11
		private GenerateOption myMetaCollectionType;

		// Token: 0x0400000C RID: 12
		private int myLowerIndex;

		// Token: 0x02000081 RID: 129
		public class EnumeratorWithRepetition : IEnumerator<IList<!0>>, IDisposable, IEnumerator
		{
			// Token: 0x06000655 RID: 1621 RVA: 0x0002B037 File Offset: 0x00029237
			public EnumeratorWithRepetition(Variations<T> source)
			{
				this.myParent = source;
				this.Reset();
			}

			// Token: 0x06000656 RID: 1622 RVA: 0x0002B04F File Offset: 0x0002924F
			public void Reset()
			{
				this.myCurrentList = null;
				this.myListIndexes = null;
			}

			// Token: 0x06000657 RID: 1623 RVA: 0x0002B060 File Offset: 0x00029260
			public bool MoveNext()
			{
				int num = 1;
				bool flag = this.myListIndexes == null;
				if (flag)
				{
					this.myListIndexes = new List<int>();
					for (int i = 0; i < this.myParent.LowerIndex; i++)
					{
						this.myListIndexes.Add(0);
					}
					num = 0;
				}
				else
				{
					int num2 = this.myListIndexes.Count - 1;
					while (num2 >= 0 && num > 0)
					{
						List<int> list = this.myListIndexes;
						int index = num2;
						list[index] += num;
						num = 0;
						bool flag2 = this.myListIndexes[num2] >= this.myParent.UpperIndex;
						if (flag2)
						{
							this.myListIndexes[num2] = 0;
							num = 1;
						}
						num2--;
					}
				}
				this.myCurrentList = null;
				return num != 1;
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x06000658 RID: 1624 RVA: 0x0002B154 File Offset: 0x00029354
			public IList<T> Current
			{
				get
				{
					this.ComputeCurrent();
					return this.myCurrentList;
				}
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x06000659 RID: 1625 RVA: 0x0002B174 File Offset: 0x00029374
			object IEnumerator.Current
			{
				get
				{
					this.ComputeCurrent();
					return this.myCurrentList;
				}
			}

			// Token: 0x0600065A RID: 1626 RVA: 0x0002AED6 File Offset: 0x000290D6
			public void Dispose()
			{
			}

			// Token: 0x0600065B RID: 1627 RVA: 0x0002B194 File Offset: 0x00029394
			private void ComputeCurrent()
			{
				bool flag = this.myCurrentList == null;
				if (flag)
				{
					this.myCurrentList = new List<T>();
					foreach (int index in this.myListIndexes)
					{
						this.myCurrentList.Add(this.myParent.myValues[index]);
					}
				}
			}

			// Token: 0x04000354 RID: 852
			private Variations<T> myParent;

			// Token: 0x04000355 RID: 853
			private List<T> myCurrentList;

			// Token: 0x04000356 RID: 854
			private List<int> myListIndexes;
		}

		// Token: 0x02000082 RID: 130
		public class EnumeratorWithoutRepetition : IEnumerator<IList<!0>>, IDisposable, IEnumerator
		{
			// Token: 0x0600065C RID: 1628 RVA: 0x0002B21C File Offset: 0x0002941C
			public EnumeratorWithoutRepetition(Variations<T> source)
			{
				this.myParent = source;
				this.myPermutationsEnumerator = (Permutations<int>.Enumerator)this.myParent.myPermutations.GetEnumerator();
			}

			// Token: 0x0600065D RID: 1629 RVA: 0x0002B248 File Offset: 0x00029448
			public void Reset()
			{
				this.myPermutationsEnumerator.Reset();
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x0002B258 File Offset: 0x00029458
			public bool MoveNext()
			{
				bool result = this.myPermutationsEnumerator.MoveNext();
				this.myCurrentList = null;
				return result;
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x0600065F RID: 1631 RVA: 0x0002B280 File Offset: 0x00029480
			public IList<T> Current
			{
				get
				{
					this.ComputeCurrent();
					return this.myCurrentList;
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000660 RID: 1632 RVA: 0x0002B2A0 File Offset: 0x000294A0
			object IEnumerator.Current
			{
				get
				{
					this.ComputeCurrent();
					return this.myCurrentList;
				}
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x0002AED6 File Offset: 0x000290D6
			public void Dispose()
			{
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x0002B2C0 File Offset: 0x000294C0
			private void ComputeCurrent()
			{
				bool flag = this.myCurrentList == null;
				if (flag)
				{
					this.myCurrentList = new List<T>();
					int num = 0;
					IList<int> list = (IList<int>)this.myPermutationsEnumerator.Current;
					for (int i = 0; i < this.myParent.LowerIndex; i++)
					{
						this.myCurrentList.Add(this.myParent.myValues[0]);
					}
					for (int j = 0; j < list.Count; j++)
					{
						int num2 = list[j];
						bool flag2 = num2 != int.MaxValue;
						if (flag2)
						{
							this.myCurrentList[num2] = this.myParent.myValues[num];
							bool flag3 = this.myParent.Type == GenerateOption.WithoutRepetition;
							if (flag3)
							{
								num++;
							}
						}
						else
						{
							num++;
						}
					}
				}
			}

			// Token: 0x04000357 RID: 855
			private Variations<T> myParent;

			// Token: 0x04000358 RID: 856
			private List<T> myCurrentList;

			// Token: 0x04000359 RID: 857
			private Permutations<int>.Enumerator myPermutationsEnumerator;
		}
	}
}
