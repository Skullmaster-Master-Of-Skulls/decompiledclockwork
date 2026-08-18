using System;
using System.Collections.Generic;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200095B RID: 2395
	internal abstract class SequenceRangeCollection
	{
		// Token: 0x17001631 RID: 5681
		public abstract SequenceRange this[int index]
		{
			get;
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06005CD3 RID: 23763
		public abstract int Count { get; }

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06005CD4 RID: 23764 RVA: 0x00156B3B File Offset: 0x00154D3B
		public static SequenceRangeCollection Empty
		{
			get
			{
				return SequenceRangeCollection.empty;
			}
		}

		// Token: 0x06005CD5 RID: 23765
		public abstract bool Contains(long number);

		// Token: 0x06005CD6 RID: 23766
		public abstract SequenceRangeCollection MergeWith(long number);

		// Token: 0x06005CD7 RID: 23767
		public abstract SequenceRangeCollection MergeWith(SequenceRange range);

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00156B42 File Offset: 0x00154D42
		private static SequenceRangeCollection GeneralCreate(SequenceRange[] sortedRanges)
		{
			if (sortedRanges.Length == 0)
			{
				return SequenceRangeCollection.empty;
			}
			if (sortedRanges.Length == 1)
			{
				return new SequenceRangeCollection.SingleItemRangeCollection(sortedRanges[0]);
			}
			return new SequenceRangeCollection.MultiItemRangeCollection(sortedRanges);
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00156B68 File Offset: 0x00154D68
		private static SequenceRangeCollection GeneralMerge(SequenceRange[] sortedRanges, SequenceRange range)
		{
			if (sortedRanges.Length == 0)
			{
				return new SequenceRangeCollection.SingleItemRangeCollection(range);
			}
			int num;
			if (sortedRanges.Length == 1)
			{
				if (range.Lower == sortedRanges[0].Upper)
				{
					num = 0;
				}
				else if (range.Lower < sortedRanges[0].Upper)
				{
					num = -1;
				}
				else
				{
					num = -2;
				}
			}
			else
			{
				num = Array.BinarySearch<SequenceRange>(sortedRanges, new SequenceRange(range.Lower), SequenceRangeCollection.upperComparer);
			}
			if (num < 0)
			{
				num = ~num;
				if (num > 0 && sortedRanges[num - 1].Upper == range.Lower - 1L)
				{
					num--;
				}
				if (num == sortedRanges.Length)
				{
					SequenceRange[] array = new SequenceRange[sortedRanges.Length + 1];
					Array.Copy(sortedRanges, array, sortedRanges.Length);
					array[sortedRanges.Length] = range;
					return SequenceRangeCollection.GeneralCreate(array);
				}
			}
			int num2;
			if (sortedRanges.Length == 1)
			{
				if (range.Upper == sortedRanges[0].Lower)
				{
					num2 = 0;
				}
				else if (range.Upper < sortedRanges[0].Lower)
				{
					num2 = -1;
				}
				else
				{
					num2 = -2;
				}
			}
			else
			{
				num2 = Array.BinarySearch<SequenceRange>(sortedRanges, new SequenceRange(range.Upper), SequenceRangeCollection.lowerComparer);
			}
			if (num2 < 0)
			{
				num2 = ~num2;
				if (num2 > 0)
				{
					if (num2 == sortedRanges.Length || sortedRanges[num2].Lower != range.Upper + 1L)
					{
						num2--;
					}
				}
				else if (sortedRanges[0].Lower > range.Upper + 1L)
				{
					SequenceRange[] array2 = new SequenceRange[sortedRanges.Length + 1];
					Array.Copy(sortedRanges, 0, array2, 1, sortedRanges.Length);
					array2[0] = range;
					return SequenceRangeCollection.GeneralCreate(array2);
				}
			}
			long lower = (range.Lower < sortedRanges[num].Lower) ? range.Lower : sortedRanges[num].Lower;
			long upper = (range.Upper > sortedRanges[num2].Upper) ? range.Upper : sortedRanges[num2].Upper;
			int num3 = num2 - num + 1;
			int num4 = sortedRanges.Length - num3 + 1;
			if (num4 == 1)
			{
				return new SequenceRangeCollection.SingleItemRangeCollection(lower, upper);
			}
			SequenceRange[] array3 = new SequenceRange[num4];
			Array.Copy(sortedRanges, array3, num);
			array3[num] = new SequenceRange(lower, upper);
			Array.Copy(sortedRanges, num2 + 1, array3, num + 1, sortedRanges.Length - num2 - 1);
			return SequenceRangeCollection.GeneralCreate(array3);
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x00156DA8 File Offset: 0x00154FA8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.Count; i++)
			{
				SequenceRange sequenceRange = this[i];
				if (i > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(sequenceRange.Lower);
				stringBuilder.Append('-');
				stringBuilder.Append(sequenceRange.Upper);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003754 RID: 14164
		private static SequenceRangeCollection.EmptyRangeCollection empty = new SequenceRangeCollection.EmptyRangeCollection();

		// Token: 0x04003755 RID: 14165
		private static SequenceRangeCollection.LowerComparer lowerComparer = new SequenceRangeCollection.LowerComparer();

		// Token: 0x04003756 RID: 14166
		private static SequenceRangeCollection.UpperComparer upperComparer = new SequenceRangeCollection.UpperComparer();

		// Token: 0x02000DDE RID: 3550
		private class EmptyRangeCollection : SequenceRangeCollection
		{
			// Token: 0x17001C70 RID: 7280
			public override SequenceRange this[int index]
			{
				get
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index"));
				}
			}

			// Token: 0x17001C71 RID: 7281
			// (get) Token: 0x06008072 RID: 32882 RVA: 0x001DDB8E File Offset: 0x001DBD8E
			public override int Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06008073 RID: 32883 RVA: 0x001DDB91 File Offset: 0x001DBD91
			public override bool Contains(long number)
			{
				return false;
			}

			// Token: 0x06008074 RID: 32884 RVA: 0x001DDB94 File Offset: 0x001DBD94
			public override SequenceRangeCollection MergeWith(long number)
			{
				return new SequenceRangeCollection.SingleItemRangeCollection(number, number);
			}

			// Token: 0x06008075 RID: 32885 RVA: 0x001DDB9D File Offset: 0x001DBD9D
			public override SequenceRangeCollection MergeWith(SequenceRange range)
			{
				return new SequenceRangeCollection.SingleItemRangeCollection(range);
			}
		}

		// Token: 0x02000DDF RID: 3551
		private class MultiItemRangeCollection : SequenceRangeCollection
		{
			// Token: 0x06008077 RID: 32887 RVA: 0x001DDBAD File Offset: 0x001DBDAD
			public MultiItemRangeCollection(SequenceRange[] sortedRanges)
			{
				this.ranges = sortedRanges;
			}

			// Token: 0x17001C72 RID: 7282
			public override SequenceRange this[int index]
			{
				get
				{
					if (index < 0 || index >= this.ranges.Length)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", index, SR.GetString("ValueMustBeInRange", new object[]
						{
							0,
							this.ranges.Length - 1
						})));
					}
					return this.ranges[index];
				}
			}

			// Token: 0x17001C73 RID: 7283
			// (get) Token: 0x06008079 RID: 32889 RVA: 0x001DDC2A File Offset: 0x001DBE2A
			public override int Count
			{
				get
				{
					return this.ranges.Length;
				}
			}

			// Token: 0x0600807A RID: 32890 RVA: 0x001DDC34 File Offset: 0x001DBE34
			public override bool Contains(long number)
			{
				if (this.ranges.Length == 0)
				{
					return false;
				}
				if (this.ranges.Length == 1)
				{
					return this.ranges[0].Contains(number);
				}
				SequenceRange value = new SequenceRange(number);
				int num = Array.BinarySearch<SequenceRange>(this.ranges, value, SequenceRangeCollection.lowerComparer);
				if (num >= 0)
				{
					return true;
				}
				num = ~num;
				return num != 0 && this.ranges[num - 1].Upper >= number;
			}

			// Token: 0x0600807B RID: 32891 RVA: 0x001DDCAB File Offset: 0x001DBEAB
			public override SequenceRangeCollection MergeWith(long number)
			{
				return this.MergeWith(new SequenceRange(number));
			}

			// Token: 0x0600807C RID: 32892 RVA: 0x001DDCB9 File Offset: 0x001DBEB9
			public override SequenceRangeCollection MergeWith(SequenceRange newRange)
			{
				return SequenceRangeCollection.GeneralMerge(this.ranges, newRange);
			}

			// Token: 0x0400496A RID: 18794
			private SequenceRange[] ranges;
		}

		// Token: 0x02000DE0 RID: 3552
		private class SingleItemRangeCollection : SequenceRangeCollection
		{
			// Token: 0x0600807D RID: 32893 RVA: 0x001DDCC7 File Offset: 0x001DBEC7
			public SingleItemRangeCollection(SequenceRange range)
			{
				this.range = range;
			}

			// Token: 0x0600807E RID: 32894 RVA: 0x001DDCD6 File Offset: 0x001DBED6
			public SingleItemRangeCollection(long lower, long upper)
			{
				this.range = new SequenceRange(lower, upper);
			}

			// Token: 0x17001C74 RID: 7284
			public override SequenceRange this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index"));
					}
					return this.range;
				}
			}

			// Token: 0x17001C75 RID: 7285
			// (get) Token: 0x06008080 RID: 32896 RVA: 0x001DDD0B File Offset: 0x001DBF0B
			public override int Count
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x06008081 RID: 32897 RVA: 0x001DDD0E File Offset: 0x001DBF0E
			public override bool Contains(long number)
			{
				return this.range.Contains(number);
			}

			// Token: 0x06008082 RID: 32898 RVA: 0x001DDD1C File Offset: 0x001DBF1C
			public override SequenceRangeCollection MergeWith(long number)
			{
				if (number == this.range.Upper + 1L)
				{
					return new SequenceRangeCollection.SingleItemRangeCollection(this.range.Lower, number);
				}
				return this.MergeWith(new SequenceRange(number));
			}

			// Token: 0x06008083 RID: 32899 RVA: 0x001DDD50 File Offset: 0x001DBF50
			public override SequenceRangeCollection MergeWith(SequenceRange newRange)
			{
				if (newRange.Lower == this.range.Upper + 1L)
				{
					return new SequenceRangeCollection.SingleItemRangeCollection(this.range.Lower, newRange.Upper);
				}
				if (this.range.Contains(newRange))
				{
					return this;
				}
				if (newRange.Contains(this.range))
				{
					return new SequenceRangeCollection.SingleItemRangeCollection(newRange);
				}
				if (newRange.Upper == this.range.Lower - 1L)
				{
					return new SequenceRangeCollection.SingleItemRangeCollection(newRange.Lower, this.range.Upper);
				}
				return SequenceRangeCollection.GeneralMerge(new SequenceRange[]
				{
					this.range
				}, newRange);
			}

			// Token: 0x0400496B RID: 18795
			private SequenceRange range;
		}

		// Token: 0x02000DE1 RID: 3553
		private class LowerComparer : IComparer<SequenceRange>
		{
			// Token: 0x06008084 RID: 32900 RVA: 0x001DDDFA File Offset: 0x001DBFFA
			public int Compare(SequenceRange x, SequenceRange y)
			{
				if (x.Lower < y.Lower)
				{
					return -1;
				}
				if (x.Lower > y.Lower)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x02000DE2 RID: 3554
		private class UpperComparer : IComparer<SequenceRange>
		{
			// Token: 0x06008086 RID: 32902 RVA: 0x001DDE29 File Offset: 0x001DC029
			public int Compare(SequenceRange x, SequenceRange y)
			{
				if (x.Upper < y.Upper)
				{
					return -1;
				}
				if (x.Upper > y.Upper)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
