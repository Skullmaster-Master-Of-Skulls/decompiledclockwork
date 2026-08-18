using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x02000744 RID: 1860
	[Serializable]
	public class CounterCreationDataCollection : CollectionBase
	{
		// Token: 0x060038C2 RID: 14530 RVA: 0x000EF823 File Offset: 0x000EE823
		public CounterCreationDataCollection()
		{
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000EF82B File Offset: 0x000EE82B
		public CounterCreationDataCollection(CounterCreationDataCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000EF83A File Offset: 0x000EE83A
		public CounterCreationDataCollection(CounterCreationData[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000D2A RID: 3370
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

		// Token: 0x060038C7 RID: 14535 RVA: 0x000EF86B File Offset: 0x000EE86B
		public int Add(CounterCreationData value)
		{
			return base.List.Add(value);
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000EF87C File Offset: 0x000EE87C
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

		// Token: 0x060038C9 RID: 14537 RVA: 0x000EF8B0 File Offset: 0x000EE8B0
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

		// Token: 0x060038CA RID: 14538 RVA: 0x000EF8EC File Offset: 0x000EE8EC
		public bool Contains(CounterCreationData value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000EF8FA File Offset: 0x000EE8FA
		public void CopyTo(CounterCreationData[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000EF909 File Offset: 0x000EE909
		public int IndexOf(CounterCreationData value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000EF917 File Offset: 0x000EE917
		public void Insert(int index, CounterCreationData value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000EF926 File Offset: 0x000EE926
		public virtual void Remove(CounterCreationData value)
		{
			base.List.Remove(value);
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000EF934 File Offset: 0x000EE934
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
