using System;
using System.Collections;
using System.Collections.Generic;

namespace Facet.Combinatorics
{
	// Token: 0x02000004 RID: 4
	public class Permutations<T> : IMetaCollection<T>, IEnumerable<IList<T>>, IEnumerable
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		protected Permutations()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000205B File Offset: 0x0000025B
		public Permutations(IList<T> values)
		{
			this.Initialize(values, GenerateOption.WithoutRepetition, null);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000206F File Offset: 0x0000026F
		public Permutations(IList<T> values, GenerateOption type)
		{
			this.Initialize(values, type, null);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002083 File Offset: 0x00000283
		public Permutations(IList<T> values, IComparer<T> comparer)
		{
			this.Initialize(values, GenerateOption.WithoutRepetition, comparer);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002098 File Offset: 0x00000298
		public virtual IEnumerator GetEnumerator()
		{
			return new Permutations<T>.Enumerator(this);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020B0 File Offset: 0x000002B0
		IEnumerator<IList<T>> IEnumerable<IList<!0>>.GetEnumerator()
		{
			return new Permutations<T>.Enumerator(this);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020C8 File Offset: 0x000002C8
		public long Count
		{
			get
			{
				return this.myCount;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020E0 File Offset: 0x000002E0
		public GenerateOption Type
		{
			get
			{
				return this.myMetaCollectionType;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020F8 File Offset: 0x000002F8
		public int UpperIndex
		{
			get
			{
				return this.myValues.Count;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002118 File Offset: 0x00000318
		public int LowerIndex
		{
			get
			{
				return this.myValues.Count;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002138 File Offset: 0x00000338
		private void Initialize(IList<T> values, GenerateOption type, IComparer<T> comparer)
		{
			this.myMetaCollectionType = type;
			this.myValues = new List<T>(values.Count);
			this.myValues.AddRange(values);
			this.myLexicographicOrders = new int[values.Count];
			bool flag = type == GenerateOption.WithRepetition;
			if (flag)
			{
				for (int i = 0; i < this.myLexicographicOrders.Length; i++)
				{
					this.myLexicographicOrders[i] = i;
				}
			}
			else
			{
				bool flag2 = comparer == null;
				if (flag2)
				{
					comparer = new Permutations<T>.SelfComparer<T>();
				}
				this.myValues.Sort(comparer);
				int num = 1;
				bool flag3 = this.myLexicographicOrders.Length != 0;
				if (flag3)
				{
					this.myLexicographicOrders[0] = num;
				}
				for (int j = 1; j < this.myLexicographicOrders.Length; j++)
				{
					bool flag4 = comparer.Compare(this.myValues[j - 1], this.myValues[j]) != 0;
					if (flag4)
					{
						num++;
					}
					this.myLexicographicOrders[j] = num;
				}
			}
			this.myCount = this.GetCount();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002254 File Offset: 0x00000454
		private long GetCount()
		{
			int num = 1;
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			for (int i = 1; i < this.myLexicographicOrders.Length; i++)
			{
				list2.AddRange(SmallPrimeUtility.Factor(i + 1));
				bool flag = this.myLexicographicOrders[i] == this.myLexicographicOrders[i - 1];
				if (flag)
				{
					num++;
				}
				else
				{
					for (int j = 2; j <= num; j++)
					{
						list.AddRange(SmallPrimeUtility.Factor(j));
					}
					num = 1;
				}
			}
			for (int k = 2; k <= num; k++)
			{
				list.AddRange(SmallPrimeUtility.Factor(k));
			}
			return SmallPrimeUtility.EvaluatePrimeFactors(SmallPrimeUtility.DividePrimeFactors(list2, list));
		}

		// Token: 0x04000004 RID: 4
		private List<T> myValues;

		// Token: 0x04000005 RID: 5
		private int[] myLexicographicOrders;

		// Token: 0x04000006 RID: 6
		private long myCount;

		// Token: 0x04000007 RID: 7
		private GenerateOption myMetaCollectionType;

		// Token: 0x0200007F RID: 127
		public class Enumerator : IEnumerator<IList<T>>, IDisposable, IEnumerator
		{
			// Token: 0x0600064B RID: 1611 RVA: 0x0002AD5C File Offset: 0x00028F5C
			public Enumerator(Permutations<T> source)
			{
				this.myParent = source;
				this.myLexicographicalOrders = new int[source.myLexicographicOrders.Length];
				source.myLexicographicOrders.CopyTo(this.myLexicographicalOrders, 0);
				this.Reset();
			}

			// Token: 0x0600064C RID: 1612 RVA: 0x0002ADAC File Offset: 0x00028FAC
			public void Reset()
			{
				this.myPosition = Permutations<T>.Enumerator.Position.BeforeFirst;
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x0002ADB8 File Offset: 0x00028FB8
			public bool MoveNext()
			{
				bool flag = this.myPosition == Permutations<T>.Enumerator.Position.BeforeFirst;
				if (flag)
				{
					this.myValues = new List<T>(this.myParent.myValues.Count);
					this.myValues.AddRange(this.myParent.myValues);
					Array.Sort<int>(this.myLexicographicalOrders);
					this.myPosition = Permutations<T>.Enumerator.Position.InSet;
				}
				else
				{
					bool flag2 = this.myPosition == Permutations<T>.Enumerator.Position.InSet;
					if (flag2)
					{
						bool flag3 = this.myValues.Count < 2;
						if (flag3)
						{
							this.myPosition = Permutations<T>.Enumerator.Position.AfterLast;
						}
						else
						{
							bool flag4 = !this.NextPermutation();
							if (flag4)
							{
								this.myPosition = Permutations<T>.Enumerator.Position.AfterLast;
							}
						}
					}
				}
				return this.myPosition != Permutations<T>.Enumerator.Position.AfterLast;
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x0600064E RID: 1614 RVA: 0x0002AE70 File Offset: 0x00029070
			public object Current
			{
				get
				{
					bool flag = this.myPosition == Permutations<T>.Enumerator.Position.InSet;
					if (flag)
					{
						return new List<T>(this.myValues);
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x0600064F RID: 1615 RVA: 0x0002AEA4 File Offset: 0x000290A4
			IList<T> IEnumerator<IList<!0>>.Current
			{
				get
				{
					bool flag = this.myPosition == Permutations<T>.Enumerator.Position.InSet;
					if (flag)
					{
						return new List<T>(this.myValues);
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x0002AED6 File Offset: 0x000290D6
			public virtual void Dispose()
			{
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x0002AEDC File Offset: 0x000290DC
			private bool NextPermutation()
			{
				int i = this.myLexicographicalOrders.Length - 1;
				while (this.myLexicographicalOrders[i - 1] >= this.myLexicographicalOrders[i])
				{
					i--;
					bool flag = i == 0;
					if (flag)
					{
						return false;
					}
				}
				int num = this.myLexicographicalOrders.Length;
				while (this.myLexicographicalOrders[num - 1] <= this.myLexicographicalOrders[i - 1])
				{
					num--;
				}
				this.Swap(i - 1, num - 1);
				i++;
				num = this.myLexicographicalOrders.Length;
				while (i < num)
				{
					this.Swap(i - 1, num - 1);
					i++;
					num--;
				}
				return true;
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x0002AF9C File Offset: 0x0002919C
			private void Swap(int i, int j)
			{
				this.myTemp = this.myValues[i];
				this.myValues[i] = this.myValues[j];
				this.myValues[j] = this.myTemp;
				this.myKviTemp = this.myLexicographicalOrders[i];
				this.myLexicographicalOrders[i] = this.myLexicographicalOrders[j];
				this.myLexicographicalOrders[j] = this.myKviTemp;
			}

			// Token: 0x0400034E RID: 846
			private T myTemp;

			// Token: 0x0400034F RID: 847
			private int myKviTemp;

			// Token: 0x04000350 RID: 848
			private Permutations<T>.Enumerator.Position myPosition = Permutations<T>.Enumerator.Position.BeforeFirst;

			// Token: 0x04000351 RID: 849
			private int[] myLexicographicalOrders;

			// Token: 0x04000352 RID: 850
			private List<T> myValues;

			// Token: 0x04000353 RID: 851
			private Permutations<T> myParent;

			// Token: 0x020000B0 RID: 176
			private enum Position
			{
				// Token: 0x0400044A RID: 1098
				BeforeFirst,
				// Token: 0x0400044B RID: 1099
				InSet,
				// Token: 0x0400044C RID: 1100
				AfterLast
			}
		}

		// Token: 0x02000080 RID: 128
		private class SelfComparer<U> : IComparer<U>
		{
			// Token: 0x06000653 RID: 1619 RVA: 0x0002B014 File Offset: 0x00029214
			public int Compare(U x, U y)
			{
				return ((IComparable<U>)((object)x)).CompareTo(y);
			}
		}
	}
}
