using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	public class CounterCreationDataCollection : CollectionBase
	{
		// Token: 0x06002D79 RID: 11641 RVA: 0x000CCB03 File Offset: 0x000CAD03
		public CounterCreationDataCollection()
		{
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000CCB0B File Offset: 0x000CAD0B
		public CounterCreationDataCollection(CounterCreationDataCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000CCB1A File Offset: 0x000CAD1A
		public CounterCreationDataCollection(CounterCreationData[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000AFD RID: 2813
		public CounterCreationData this[int index]
		{
			get
			{
				return (CounterCreationData)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000CCB4B File Offset: 0x000CAD4B
		public int Add(CounterCreationData value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000CCB5C File Offset: 0x000CAD5C
		public void AddRange(CounterCreationData[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000CCB90 File Offset: 0x000CAD90
		public void AddRange(CounterCreationDataCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000CCBCC File Offset: 0x000CADCC
		public bool Contains(CounterCreationData value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000CCBDA File Offset: 0x000CADDA
		public void CopyTo(CounterCreationData[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000CCBE9 File Offset: 0x000CADE9
		public int IndexOf(CounterCreationData value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000CCBF7 File Offset: 0x000CADF7
		public void Insert(int index, CounterCreationData value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000CCC06 File Offset: 0x000CAE06
		public virtual void Remove(CounterCreationData value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000CCC14 File Offset: 0x000CAE14
		protected override void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is CounterCreationData))
			{
				throw new ArgumentException(SR.GetString("MustAddCounterCreationData"));
			}
		}
	}
}
