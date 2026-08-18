using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x020019AE RID: 6574
	internal class OrderByEnumerable<TElement, TKey> : IOrderedEnumerable<TElement>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600FE3A RID: 65082 RVA: 0x00391A98 File Offset: 0x0038FC98
		public OrderByEnumerable(IEnumerable source, TFunc<object, TKey> keySelector, IComparer<TKey> comparer, bool descending, bool stableSort)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			this.Source = source;
			this.KeySelector = keySelector;
			this.Comparer = (comparer ?? Comparer<TKey>.Default);
			this.Descending = descending;
			this.StableSort = stableSort;
		}

		// Token: 0x0600FE3B RID: 65083 RVA: 0x00391AF5 File Offset: 0x0038FCF5
		public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TNewKey>(TFunc<object, TNewKey> keySelector, IComparer<TNewKey> comparer, bool descending, bool stableSort)
		{
			return new ThenByEnumerable<TElement, TNewKey, TKey>(this, keySelector, comparer, descending, stableSort);
		}

		// Token: 0x0600FE3C RID: 65084 RVA: 0x00391B04 File Offset: 0x0038FD04
		internal virtual int CompareElements(object e1, object e2)
		{
			int num = this.Comparer.Compare(this.KeySelector(e1), this.KeySelector(e2));
			if (!this.Descending)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x0600FE3D RID: 65085 RVA: 0x00391B41 File Offset: 0x0038FD41
		internal virtual IEnumerable GetElementsToSort()
		{
			return this.Source;
		}

		// Token: 0x0600FE3E RID: 65086 RVA: 0x00391B49 File Offset: 0x0038FD49
		public IEnumerator<TElement> GetEnumerator()
		{
			if (this.StableSort)
			{
				return this.GetEnumeratorStableSort();
			}
			return this.GetEnumeratorDefaultSort();
		}

		// Token: 0x0600FE3F RID: 65087 RVA: 0x00391D54 File Offset: 0x0038FF54
		public IEnumerator<TElement> GetEnumeratorDefaultSort()
		{
			ArrayList array = new ArrayList();
			foreach (object value in this.GetElementsToSort())
			{
				array.Add(value);
			}
			OrderByEnumerable<TElement, TKey>.ElementComparer comparer = new OrderByEnumerable<TElement, TKey>.ElementComparer();
			comparer.SetComparer(new TFunc<object, object, int>(this.CompareElements));
			array.Sort(comparer);
			foreach (object obj in array)
			{
				TElement element = (TElement)((object)obj);
				yield return element;
			}
			yield break;
		}

		// Token: 0x0600FE40 RID: 65088 RVA: 0x00391F9C File Offset: 0x0039019C
		public IEnumerator<TElement> GetEnumeratorStableSort()
		{
			ArrayList array = new ArrayList();
			int index = 0;
			foreach (object value in this.GetElementsToSort())
			{
				KeyValuePair<int, object> keyValuePair = new KeyValuePair<int, object>(index, value);
				array.Add(keyValuePair);
				index++;
			}
			OrderByEnumerable<TElement, TKey>.ElementComparer comparer = new OrderByEnumerable<TElement, TKey>.ElementComparer();
			comparer.SetComparer(new TFunc<object, object, int>(this.CompareElements));
			array.Sort(comparer);
			foreach (object obj in array)
			{
				KeyValuePair<int, object> pair = (KeyValuePair<int, object>)obj;
				KeyValuePair<int, object> keyValuePair2 = pair;
				yield return (TElement)((object)keyValuePair2.Value);
			}
			yield break;
		}

		// Token: 0x0600FE41 RID: 65089 RVA: 0x00391FB8 File Offset: 0x003901B8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400481F RID: 18463
		public readonly IEnumerable Source;

		// Token: 0x04004820 RID: 18464
		public readonly TFunc<object, TKey> KeySelector;

		// Token: 0x04004821 RID: 18465
		public readonly IComparer<TKey> Comparer;

		// Token: 0x04004822 RID: 18466
		public readonly bool Descending;

		// Token: 0x04004823 RID: 18467
		public readonly bool StableSort;

		// Token: 0x020019AF RID: 6575
		private class ElementComparer : IComparer
		{
			// Token: 0x0600FE42 RID: 65090 RVA: 0x00391FC0 File Offset: 0x003901C0
			public int Compare(object x, object y)
			{
				if (x is KeyValuePair<int, object>)
				{
					KeyValuePair<int, object> keyValuePair = (KeyValuePair<int, object>)x;
					KeyValuePair<int, object> keyValuePair2 = (KeyValuePair<int, object>)y;
					int num = this._comparison(keyValuePair.Value, keyValuePair2.Value);
					if (num == 0)
					{
						num = keyValuePair.Key.CompareTo(keyValuePair2.Key);
					}
					return num;
				}
				return this._comparison(x, y);
			}

			// Token: 0x0600FE43 RID: 65091 RVA: 0x00392026 File Offset: 0x00390226
			public void SetComparer(TFunc<object, object, int> comparison)
			{
				this._comparison = comparison;
			}

			// Token: 0x04004824 RID: 18468
			private TFunc<object, object, int> _comparison;
		}
	}
}
