using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CDB RID: 3291
	internal class IndexToValueTable<T> : IEnumerable<Range<T>>, IEnumerable
	{
		// Token: 0x06007ADD RID: 31453 RVA: 0x001C2F9A File Offset: 0x001C119A
		public IndexToValueTable()
		{
			this.list = new List<Range<T>>();
		}

		// Token: 0x1700275F RID: 10079
		// (get) Token: 0x06007ADE RID: 31454 RVA: 0x001C2FB0 File Offset: 0x001C11B0
		public int IndexCount
		{
			get
			{
				int num = 0;
				foreach (Range<T> range in this.list)
				{
					num += range.Count;
				}
				return num;
			}
		}

		// Token: 0x17002760 RID: 10080
		// (get) Token: 0x06007ADF RID: 31455 RVA: 0x001C3008 File Offset: 0x001C1208
		public bool IsEmpty
		{
			get
			{
				return this.list.Count == 0;
			}
		}

		// Token: 0x17002761 RID: 10081
		// (get) Token: 0x06007AE0 RID: 31456 RVA: 0x001C3018 File Offset: 0x001C1218
		public int RangeCount
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x001C3025 File Offset: 0x001C1225
		public void AddValue(int index, T value)
		{
			this.AddValues(index, 1, value);
		}

		// Token: 0x06007AE2 RID: 31458 RVA: 0x001C3030 File Offset: 0x001C1230
		public void AddValues(int startIndex, int count, T value)
		{
			this.AddValuesPrivate(startIndex, count, value, null);
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x001C304F File Offset: 0x001C124F
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06007AE4 RID: 31460 RVA: 0x001C305C File Offset: 0x001C125C
		public bool Contains(int index)
		{
			return this.IsCorrectRangeIndex(this.FindRangeIndex(index), index);
		}

		// Token: 0x06007AE5 RID: 31461 RVA: 0x001C306C File Offset: 0x001C126C
		public bool ContainsAll(int startIndex, int endIndex)
		{
			int num = -1;
			int num2 = -1;
			foreach (Range<T> range in this.list)
			{
				if (num == -1 && range.UpperBound >= startIndex)
				{
					if (startIndex < range.LowerBound)
					{
						return false;
					}
					num = startIndex;
					num2 = range.UpperBound;
					if (num2 >= endIndex)
					{
						return true;
					}
				}
				else if (num != -1)
				{
					if (range.LowerBound > num2 + 1)
					{
						return false;
					}
					num2 = range.UpperBound;
					if (num2 >= endIndex)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007AE6 RID: 31462 RVA: 0x001C3110 File Offset: 0x001C1310
		public bool ContainsIndexAndValue(int index, T value)
		{
			int num = this.FindRangeIndex(index);
			return this.IsCorrectRangeIndex(num, index) && this.list[num].ContainsValue(value);
		}

		// Token: 0x06007AE7 RID: 31463 RVA: 0x001C3148 File Offset: 0x001C1348
		public IndexToValueTable<T> Copy()
		{
			IndexToValueTable<T> indexToValueTable = new IndexToValueTable<T>();
			foreach (Range<T> range in this.list)
			{
				indexToValueTable.list.Add(range.Copy());
			}
			return indexToValueTable;
		}

		// Token: 0x06007AE8 RID: 31464 RVA: 0x001C31AC File Offset: 0x001C13AC
		public int CountNextNotIncludedIndexes(int index, int count)
		{
			int num = index;
			int num2 = 0;
			int i = this.FindRangeIndex(index);
			if (this.list.Count > 0)
			{
				if (i >= 0)
				{
					Range<T> range = this.list[i++];
					if (range.ContainsIndex(index))
					{
						num = range.UpperBound + 1;
					}
				}
				else
				{
					i++;
				}
				while (i < this.list.Count)
				{
					Range<T> range = this.list[i++];
					if (range.LowerBound - 1 - num >= count - num2)
					{
						return num + count - num2;
					}
					num2 += range.LowerBound - num;
					num = range.UpperBound + 1;
				}
				return num + (count - num2);
			}
			return index + count;
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x001C3254 File Offset: 0x001C1454
		public int CountPreviousNotIncludedIndexes(int index, int count)
		{
			int num = index;
			int num2 = 0;
			int i = this.FindRangeIndex(index);
			if (this.list.Count > 0)
			{
				if (i >= 0)
				{
					Range<T> range = this.list[i];
					if (range.ContainsIndex(index))
					{
						num = range.LowerBound - 1;
					}
				}
				else
				{
					i--;
				}
				while (i >= 0)
				{
					Range<T> range = this.list[i--];
					if (num - (range.UpperBound + 1) >= count - num2)
					{
						return num - (count - num2);
					}
					num2 += num - range.UpperBound;
					num = range.LowerBound - 1;
				}
				return num - (count - num2);
			}
			return index - count;
		}

		// Token: 0x06007AEA RID: 31466 RVA: 0x001C32F0 File Offset: 0x001C14F0
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		public int GetNextGap(int index)
		{
			int num = index + 1;
			int num2 = this.FindRangeIndex(num);
			if (this.IsCorrectRangeIndex(num2, num))
			{
				while (num2 < this.list.Count - 1 && this.list[num2].UpperBound == this.list[num2 + 1].LowerBound - 1)
				{
					num2++;
				}
				return this.list[num2].UpperBound + 1;
			}
			return num;
		}

		// Token: 0x06007AEB RID: 31467 RVA: 0x001C3368 File Offset: 0x001C1568
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		public int GetNextIndex(int index)
		{
			int num = index + 1;
			int num2 = this.FindRangeIndex(num);
			if (this.IsCorrectRangeIndex(num2, num))
			{
				return num;
			}
			num2++;
			if (num2 >= this.list.Count)
			{
				return -1;
			}
			return this.list[num2].LowerBound;
		}

		// Token: 0x06007AEC RID: 31468 RVA: 0x001C33B4 File Offset: 0x001C15B4
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		public int GetPreviousGap(int index)
		{
			int num = index - 1;
			int num2 = this.FindRangeIndex(num);
			if (this.IsCorrectRangeIndex(num2, num))
			{
				while (num2 > 0 && this.list[num2].LowerBound == this.list[num2 - 1].UpperBound + 1)
				{
					num2--;
				}
				return this.list[num2].LowerBound - 1;
			}
			return num;
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x001C3420 File Offset: 0x001C1620
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		public int GetPreviousIndex(int index)
		{
			int num = index - 1;
			int num2 = this.FindRangeIndex(num);
			if (this.IsCorrectRangeIndex(num2, num))
			{
				return num;
			}
			if (num2 < 0 || num2 >= this.list.Count)
			{
				return -1;
			}
			return this.list[num2].UpperBound;
		}

		// Token: 0x06007AEE RID: 31470 RVA: 0x001C346C File Offset: 0x001C166C
		public int GetIndexCount(int lowerBound, int upperBound, T value)
		{
			if (this.list.Count == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = this.FindRangeIndex(lowerBound);
			if (this.IsCorrectRangeIndex(num2, lowerBound) && this.list[num2].ContainsValue(value))
			{
				num += this.list[num2].UpperBound - lowerBound + 1;
			}
			num2++;
			while (num2 < this.list.Count && this.list[num2].UpperBound <= upperBound)
			{
				if (this.list[num2].ContainsValue(value))
				{
					num += this.list[num2].Count;
				}
				num2++;
			}
			if (num2 < this.list.Count && this.IsCorrectRangeIndex(num2, upperBound) && this.list[num2].ContainsValue(value))
			{
				num += upperBound - this.list[num2].LowerBound;
			}
			return num;
		}

		// Token: 0x06007AEF RID: 31471 RVA: 0x001C3570 File Offset: 0x001C1770
		public int GetIndexCount(int lowerBound, int upperBound)
		{
			if (upperBound < lowerBound || this.list.Count == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = this.FindRangeIndex(lowerBound);
			if (this.IsCorrectRangeIndex(num2, lowerBound))
			{
				num += this.list[num2].UpperBound - lowerBound + 1;
			}
			num2++;
			while (num2 < this.list.Count && this.list[num2].UpperBound <= upperBound)
			{
				num += this.list[num2].Count;
				num2++;
			}
			if (num2 < this.list.Count && this.IsCorrectRangeIndex(num2, upperBound))
			{
				num += upperBound - this.list[num2].LowerBound;
			}
			return num;
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x001C362C File Offset: 0x001C182C
		public int GetIndexCountBeforeGap(int startingIndex, int gapSize)
		{
			if (this.list.Count == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = startingIndex;
			int num3 = 0;
			int num4 = 0;
			while (num4 <= gapSize && num3 < this.list.Count)
			{
				num4 += this.list[num3].LowerBound - num2;
				if (num4 <= gapSize)
				{
					num += this.list[num3].UpperBound - this.list[num3].LowerBound + 1;
					num2 = this.list[num3].UpperBound + 1;
					num3++;
				}
			}
			return num;
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x001C3884 File Offset: 0x001C1A84
		public IEnumerable<int> GetIndexes()
		{
			foreach (Range<T> range in this.list)
			{
				for (int i = range.LowerBound; i <= range.UpperBound; i++)
				{
					yield return i;
				}
			}
			yield break;
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x001C3A40 File Offset: 0x001C1C40
		public IEnumerable<int> GetIndexes(int startIndex)
		{
			int rangeIndex = this.FindRangeIndex(startIndex);
			if (rangeIndex == -1)
			{
				rangeIndex++;
			}
			while (rangeIndex < this.list.Count)
			{
				for (int i = this.list[rangeIndex].LowerBound; i <= this.list[rangeIndex].UpperBound; i++)
				{
					if (i >= startIndex)
					{
						yield return i;
					}
				}
				rangeIndex++;
			}
			yield break;
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x001C3A64 File Offset: 0x001C1C64
		public int GetNthIndex(int n)
		{
			int num = 0;
			foreach (Range<T> range in this.list)
			{
				if (num + range.Count > n)
				{
					return range.LowerBound + n - num;
				}
				num += range.Count;
			}
			return -1;
		}

		// Token: 0x06007AF4 RID: 31476 RVA: 0x001C3AD8 File Offset: 0x001C1CD8
		public T GetValueAt(int index)
		{
			bool flag;
			return this.GetValueAt(index, out flag);
		}

		// Token: 0x06007AF5 RID: 31477 RVA: 0x001C3AF0 File Offset: 0x001C1CF0
		public T GetValueAt(int index, out bool found)
		{
			int num = this.FindRangeIndex(index);
			if (this.IsCorrectRangeIndex(num, index))
			{
				found = true;
				return this.list[num].Value;
			}
			found = false;
			return default(T);
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x001C3B30 File Offset: 0x001C1D30
		public int IndexOf(int index)
		{
			int num = 0;
			foreach (Range<T> range in this.list)
			{
				if (range.UpperBound >= index)
				{
					num += index - range.LowerBound;
					break;
				}
				num += range.Count;
			}
			return num;
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x001C3BA0 File Offset: 0x001C1DA0
		public void InsertIndex(int index)
		{
			this.InsertIndexes(index, 1);
		}

		// Token: 0x06007AF8 RID: 31480 RVA: 0x001C3BAA File Offset: 0x001C1DAA
		public void InsertIndexAndValue(int index, T value)
		{
			this.InsertIndexesAndValues(index, 1, value);
		}

		// Token: 0x06007AF9 RID: 31481 RVA: 0x001C3BB5 File Offset: 0x001C1DB5
		public void InsertIndexes(int startIndex, int count)
		{
			this.InsertIndexesPrivate(startIndex, count, this.FindRangeIndex(startIndex));
		}

		// Token: 0x06007AFA RID: 31482 RVA: 0x001C3BC8 File Offset: 0x001C1DC8
		public void InsertIndexesAndValues(int startIndex, int count, T value)
		{
			int num = this.FindRangeIndex(startIndex);
			this.InsertIndexesPrivate(startIndex, count, num);
			if (num >= 0 && this.list[num].LowerBound > startIndex)
			{
				num--;
			}
			this.AddValuesPrivate(startIndex, count, value, new int?(num));
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x001C3C11 File Offset: 0x001C1E11
		public void RemoveIndex(int index)
		{
			this.RemoveIndexes(index, 1);
		}

		// Token: 0x06007AFC RID: 31484 RVA: 0x001C3C1B File Offset: 0x001C1E1B
		public void RemoveIndexAndValue(int index)
		{
			this.RemoveIndexesAndValues(index, 1);
		}

		// Token: 0x06007AFD RID: 31485 RVA: 0x001C3C28 File Offset: 0x001C1E28
		public void RemoveIndexes(int startIndex, int count)
		{
			int num = this.FindRangeIndex(startIndex);
			if (num < 0)
			{
				num = 0;
			}
			for (int i = num; i < this.list.Count; i++)
			{
				Range<T> range = this.list[i];
				if (range.UpperBound >= startIndex)
				{
					if (range.LowerBound >= startIndex + count)
					{
						range.LowerBound -= count;
						range.UpperBound -= count;
					}
					else
					{
						int rangeIndex = i;
						if (range.LowerBound <= startIndex)
						{
							if (range.UpperBound >= startIndex + count)
							{
								i++;
								this.list.Insert(i, new Range<T>(startIndex, range.UpperBound - count, range.Value));
							}
							range.UpperBound = startIndex - 1;
						}
						else
						{
							range.LowerBound = startIndex;
							range.UpperBound -= count;
						}
						if (this.RemoveRangeIfInvalid(range, rangeIndex))
						{
							i--;
						}
					}
				}
			}
			if (!this.Merge(num))
			{
				this.Merge(num + 1);
			}
		}

		// Token: 0x06007AFE RID: 31486 RVA: 0x001C3D1C File Offset: 0x001C1F1C
		public void RemoveIndexesAndValues(int startIndex, int count)
		{
			this.RemoveValues(startIndex, count);
			this.RemoveIndexes(startIndex, count);
		}

		// Token: 0x06007AFF RID: 31487 RVA: 0x001C3D2E File Offset: 0x001C1F2E
		public void RemoveValue(int index)
		{
			this.RemoveValues(index, 1);
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x001C3D38 File Offset: 0x001C1F38
		public void RemoveValues(int startIndex, int count)
		{
			int num = this.FindRangeIndex(startIndex);
			if (num < 0)
			{
				num = 0;
			}
			while (num < this.list.Count && this.list[num].UpperBound < startIndex)
			{
				num++;
			}
			if (num >= this.list.Count || this.list[num].LowerBound > startIndex + count - 1)
			{
				return;
			}
			if (this.list[num].LowerBound < startIndex)
			{
				this.list.Insert(num, new Range<T>(this.list[num].LowerBound, startIndex - 1, this.list[num].Value));
				num++;
			}
			this.list[num].LowerBound = startIndex + count;
			if (!this.RemoveRangeIfInvalid(this.list[num], num))
			{
				num++;
			}
			while (num < this.list.Count && this.list[num].UpperBound < startIndex + count)
			{
				this.list.RemoveAt(num);
			}
			if (num < this.list.Count && this.list[num].UpperBound >= startIndex + count && this.list[num].LowerBound < startIndex + count)
			{
				this.list[num].LowerBound = startIndex + count;
				this.RemoveRangeIfInvalid(this.list[num], num);
			}
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x001C3EB4 File Offset: 0x001C20B4
		private void AddValuesPrivate(int startIndex, int count, T value, int? startRangeIndex)
		{
			int num = startIndex + count - 1;
			Range<T> item = new Range<T>(startIndex, num, value);
			if (this.list.Count == 0)
			{
				this.list.Add(item);
				return;
			}
			int num2 = (startRangeIndex != null) ? startRangeIndex.Value : this.FindRangeIndex(startIndex);
			Range<T> range = (num2 < 0) ? null : this.list[num2];
			if (range == null)
			{
				if (num2 < 0)
				{
					num2 = 0;
				}
				this.list.Insert(num2, item);
			}
			else
			{
				T value2 = range.Value;
				if (!value2.Equals(value) && range.UpperBound >= startIndex)
				{
					if (range.UpperBound > num)
					{
						this.list.Insert(num2 + 1, new Range<T>(num + 1, range.UpperBound, range.Value));
					}
					range.UpperBound = startIndex - 1;
					if (!this.RemoveRangeIfInvalid(range, num2))
					{
						num2++;
					}
					this.list.Insert(num2, item);
				}
				else
				{
					this.list.Insert(num2 + 1, item);
					if (!this.Merge(num2))
					{
						num2++;
					}
				}
			}
			int num3 = num2 + 1;
			while (num3 < this.list.Count && this.list[num3].UpperBound < num)
			{
				this.list.RemoveAt(num3);
			}
			if (num3 < this.list.Count)
			{
				Range<T> range2 = this.list[num3];
				if (range2.LowerBound <= num)
				{
					range2.LowerBound = num + 1;
					this.RemoveRangeIfInvalid(range2, num3);
				}
				this.Merge(num2);
			}
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x001C4044 File Offset: 0x001C2244
		private int FindRangeIndex(int index)
		{
			bool flag;
			return this.FindRangeIndex(index, out flag);
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x001C405C File Offset: 0x001C225C
		private int FindRangeIndex(int index, out bool found)
		{
			found = false;
			if (this.list.Count == 0)
			{
				return -1;
			}
			int num = 0;
			int i = this.list.Count - 1;
			Range<T> range;
			while (i > num)
			{
				int num2 = (num + i) / 2;
				range = this.list[num2];
				if (range.UpperBound < index)
				{
					num = num2 + 1;
				}
				else
				{
					if (range.LowerBound <= index)
					{
						found = true;
						return num2;
					}
					i = num2 - 1;
				}
			}
			if (num != i)
			{
				return i;
			}
			range = this.list[num];
			if (range.ContainsIndex(index))
			{
				found = true;
				return num;
			}
			if (range.UpperBound < index)
			{
				return num;
			}
			return num - 1;
		}

		// Token: 0x06007B04 RID: 31492 RVA: 0x001C40F8 File Offset: 0x001C22F8
		private bool Merge(int lowerRangeIndex)
		{
			int num = lowerRangeIndex + 1;
			if (lowerRangeIndex >= 0 && num < this.list.Count)
			{
				Range<T> range = this.list[lowerRangeIndex];
				Range<T> range2 = this.list[num];
				if (range.UpperBound + 1 >= range2.LowerBound)
				{
					T value = range.Value;
					if (value.Equals(range2.Value))
					{
						range.UpperBound = Math.Max(range.UpperBound, range2.UpperBound);
						this.list.RemoveAt(num);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007B05 RID: 31493 RVA: 0x001C418C File Offset: 0x001C238C
		private void InsertIndexesPrivate(int startIndex, int count, int lowerRangeIndex)
		{
			int num = (lowerRangeIndex >= 0) ? lowerRangeIndex : 0;
			int i = num;
			while (i < this.list.Count)
			{
				Range<T> range = this.list[i];
				if (range.LowerBound >= startIndex)
				{
					range.LowerBound += count;
					goto IL_6B;
				}
				if (range.UpperBound < startIndex)
				{
					goto IL_6B;
				}
				i++;
				this.list.Insert(i, new Range<T>(startIndex, range.UpperBound + count, range.Value));
				range.UpperBound = startIndex - 1;
				IL_82:
				i++;
				continue;
				IL_6B:
				if (range.UpperBound >= startIndex)
				{
					range.UpperBound += count;
					goto IL_82;
				}
				goto IL_82;
			}
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x001C4230 File Offset: 0x001C2430
		private bool IsCorrectRangeIndex(int rangeIndex, int index)
		{
			return -1 != rangeIndex && this.list[rangeIndex].ContainsIndex(index);
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x001C424A File Offset: 0x001C244A
		private bool RemoveRangeIfInvalid(Range<T> range, int rangeIndex)
		{
			if (range.UpperBound < range.LowerBound)
			{
				this.list.RemoveAt(rangeIndex);
				return true;
			}
			return false;
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x001C4269 File Offset: 0x001C2469
		public IEnumerator<Range<T>> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x001C427B File Offset: 0x001C247B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x001C4290 File Offset: 0x001C2490
		internal bool TryGetValue(int slot, out T group, out int lowerBound)
		{
			bool flag;
			int num = this.FindRangeIndex(slot, out flag);
			if (num == -1)
			{
				group = default(T);
				lowerBound = -1;
				return false;
			}
			Range<T> range = this.list[num];
			lowerBound = range.LowerBound;
			group = range.Value;
			return true;
		}

		// Token: 0x040021AA RID: 8618
		private List<Range<T>> list;
	}
}
