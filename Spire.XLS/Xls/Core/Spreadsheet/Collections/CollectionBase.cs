using System;
using System.Collections;
using System.Collections.Generic;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001DA RID: 474
	public class CollectionBase<T> : IList<T>
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x000EE338 File Offset: 0x000ED338
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x000EE380 File Offset: 0x000ED380
		public int Capacity
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Capacity;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.Capacity = value;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x000EE3C8 File Offset: 0x000ED3C8
		public int Count
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.Count;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000EE410 File Offset: 0x000ED410
		protected internal List<T> InnerList
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x000EE454 File Offset: 0x000ED454
		protected IList<T> List
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x170009CD RID: 2509
		public T this[int i]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ[i];
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				T oldValue = this.ᜀ[i];
				this.OnSet(i, oldValue, value);
				this.ᜀ[i] = value;
				this.OnSetComplete(i, oldValue, value);
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000EE548 File Offset: 0x000ED548
		public CollectionBase()
		{
			this.ᜀ = new List<T>();
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000EE568 File Offset: 0x000ED568
		public CollectionBase(int capacity)
		{
			this.ᜀ = new List<T>(capacity);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000EE588 File Offset: 0x000ED588
		public void Clear()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OnClear();
			this.ᜀ.Clear();
			this.OnClearComplete();
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000EE5DC File Offset: 0x000ED5DC
		public void Insert(int index, T item)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OnInsert(index, item);
			this.ᜀ.Insert(index, item);
			this.OnInsertComplete(index, item);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000EE634 File Offset: 0x000ED634
		public IEnumerator<T> GetEnumerator()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ᜀ.GetEnumerator();
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000EE680 File Offset: 0x000ED680
		protected virtual void OnClear()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000EE6BC File Offset: 0x000ED6BC
		protected virtual void OnClearComplete()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000EE6F8 File Offset: 0x000ED6F8
		protected virtual void OnInsert(int index, T value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000EE734 File Offset: 0x000ED734
		protected virtual void OnInsertComplete(int index, T value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000EE770 File Offset: 0x000ED770
		protected virtual void OnRemove(int index, T value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000EE7AC File Offset: 0x000ED7AC
		protected virtual void OnRemoveComplete(int index, T value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000EE7E8 File Offset: 0x000ED7E8
		protected virtual void OnSet(int index, T oldValue, T newValue)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000EE824 File Offset: 0x000ED824
		protected virtual void OnSetComplete(int index, T oldValue, T newValue)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000EE860 File Offset: 0x000ED860
		public void RemoveAt(int index)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			T value = this[index];
			this.OnRemove(index, value);
			this.ᜀ.RemoveAt(index);
			this.OnRemoveComplete(index, value);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000EE8C0 File Offset: 0x000ED8C0
		public int IndexOf(T item)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ᜀ.IndexOf(item);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000EE908 File Offset: 0x000ED908
		public virtual void Add(T item)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			int count = this.Count;
			this.OnInsert(count, item);
			this.ᜀ.Add(item);
			this.OnInsertComplete(count, item);
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000EE968 File Offset: 0x000ED968
		public bool Contains(T item)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ᜀ.Contains(item);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000EE9B0 File Offset: 0x000ED9B0
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ.CopyTo(array, arrayIndex);
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x000EE9F8 File Offset: 0x000ED9F8
		public bool IsReadOnly
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return false;
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000EEA34 File Offset: 0x000EDA34
		public bool Remove(T item)
		{
			bool result;
			for (;;)
			{
				int num = this.IndexOf(item);
				result = false;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_58;
					case 1:
						goto IL_6B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_58;
						default:
							if (false)
							{
							}
							if (num >= 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_6D;
						}
						break;
					}
					break;
					IL_58:
					this.RemoveAt(num);
					result = true;
					num2 = 1;
				}
			}
			IL_6B:
			IL_6D:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000EEAB8 File Offset: 0x000EDAB8
		IEnumerator IEnumerable.GetEnumerator()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return ((IEnumerable)this.ᜀ).GetEnumerator();
		}

		// Token: 0x04001044 RID: 4164
		private bool \u25D9\u007F\u009F\u008F;

		// Token: 0x04001045 RID: 4165
		private int[] \u2609\u0095\u0081\u00AE;

		// Token: 0x04001046 RID: 4166
		private List<T> ᜀ;
	}
}
