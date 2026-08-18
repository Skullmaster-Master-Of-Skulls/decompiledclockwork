using System;
using System.Collections;

namespace Telerik.Charting
{
	// Token: 0x020016F9 RID: 5881
	public class DoubleCollection : CollectionBase
	{
		// Token: 0x170045B9 RID: 17849
		public double this[int index]
		{
			get
			{
				return (double)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600E471 RID: 58481 RVA: 0x0032B2D7 File Offset: 0x003294D7
		public int Add(double value)
		{
			return base.List.Add(value);
		}

		// Token: 0x0600E472 RID: 58482 RVA: 0x0032B2EA File Offset: 0x003294EA
		public int IndexOf(double value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600E473 RID: 58483 RVA: 0x0032B2FD File Offset: 0x003294FD
		public void Insert(int index, double value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600E474 RID: 58484 RVA: 0x0032B311 File Offset: 0x00329511
		public void Remove(double value)
		{
			base.List.Remove(value);
		}

		// Token: 0x0600E475 RID: 58485 RVA: 0x0032B324 File Offset: 0x00329524
		public bool Contains(double value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600E476 RID: 58486 RVA: 0x0032B337 File Offset: 0x00329537
		protected override void OnInsert(int index, object value)
		{
		}

		// Token: 0x0600E477 RID: 58487 RVA: 0x0032B339 File Offset: 0x00329539
		protected override void OnRemove(int index, object value)
		{
		}

		// Token: 0x0600E478 RID: 58488 RVA: 0x0032B33B File Offset: 0x0032953B
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x0600E479 RID: 58489 RVA: 0x0032B33D File Offset: 0x0032953D
		protected override void OnValidate(object value)
		{
			if (value.GetType() != typeof(double))
			{
				throw new ArgumentException("value must be of type Double.", "value");
			}
		}

		// Token: 0x0600E47A RID: 58490 RVA: 0x0032B368 File Offset: 0x00329568
		public void InitData()
		{
			this.Add(11.3);
			this.Add(3.8);
			this.Add(8.1);
			this.Add(5.0);
			this.Add(3.2);
		}
	}
}
