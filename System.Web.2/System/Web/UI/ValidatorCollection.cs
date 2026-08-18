using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000325 RID: 805
	public sealed class ValidatorCollection : ICollection, IEnumerable
	{
		// Token: 0x060025B2 RID: 9650 RVA: 0x0007C81F File Offset: 0x0007AA1F
		public ValidatorCollection()
		{
			this.data = new ArrayList();
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x0007C832 File Offset: 0x0007AA32
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x17000A6B RID: 2667
		public IValidator this[int index]
		{
			get
			{
				return (IValidator)this.data[index];
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x0007C852 File Offset: 0x0007AA52
		public void Add(IValidator validator)
		{
			this.data.Add(validator);
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0007C861 File Offset: 0x0007AA61
		public bool Contains(IValidator validator)
		{
			return this.data.Contains(validator);
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0007C86F File Offset: 0x0007AA6F
		public void Remove(IValidator validator)
		{
			this.data.Remove(validator);
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0007C87D File Offset: 0x0007AA7D
		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0007C88C File Offset: 0x0007AA8C
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001D7C RID: 7548
		private ArrayList data;
	}
}
