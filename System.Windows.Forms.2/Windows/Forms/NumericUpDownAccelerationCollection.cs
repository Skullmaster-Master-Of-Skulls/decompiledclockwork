using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000310 RID: 784
	[ListBindable(false)]
	public class NumericUpDownAccelerationCollection : MarshalByRefObject, ICollection<NumericUpDownAcceleration>, IEnumerable<NumericUpDownAcceleration>, IEnumerable
	{
		// Token: 0x060031FC RID: 12796 RVA: 0x000E12F0 File Offset: 0x000DF4F0
		public void Add(NumericUpDownAcceleration acceleration)
		{
			if (acceleration == null)
			{
				throw new ArgumentNullException("acceleration");
			}
			int num = 0;
			while (num < this.items.Count && acceleration.Seconds >= this.items[num].Seconds)
			{
				num++;
			}
			this.items.Insert(num, acceleration);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000E1347 File Offset: 0x000DF547
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000E1354 File Offset: 0x000DF554
		public bool Contains(NumericUpDownAcceleration acceleration)
		{
			return this.items.Contains(acceleration);
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000E1362 File Offset: 0x000DF562
		public void CopyTo(NumericUpDownAcceleration[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000E1371 File Offset: 0x000DF571
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x00011A20 File Offset: 0x0000FC20
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000E137E File Offset: 0x000DF57E
		public bool Remove(NumericUpDownAcceleration acceleration)
		{
			return this.items.Remove(acceleration);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000E138C File Offset: 0x000DF58C
		IEnumerator<NumericUpDownAcceleration> IEnumerable<NumericUpDownAcceleration>.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000E139E File Offset: 0x000DF59E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.items).GetEnumerator();
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000E13AB File Offset: 0x000DF5AB
		public NumericUpDownAccelerationCollection()
		{
			this.items = new List<NumericUpDownAcceleration>();
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000E13C0 File Offset: 0x000DF5C0
		public void AddRange(params NumericUpDownAcceleration[] accelerations)
		{
			if (accelerations == null)
			{
				throw new ArgumentNullException("accelerations");
			}
			for (int i = 0; i < accelerations.Length; i++)
			{
				if (accelerations[i] == null)
				{
					throw new ArgumentNullException(SR.GetString("NumericUpDownAccelerationCollectionAtLeastOneEntryIsNull"));
				}
			}
			foreach (NumericUpDownAcceleration acceleration in accelerations)
			{
				this.Add(acceleration);
			}
		}

		// Token: 0x17000BB7 RID: 2999
		public NumericUpDownAcceleration this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x04001E63 RID: 7779
		private List<NumericUpDownAcceleration> items;
	}
}
