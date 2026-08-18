using System;
using System.Collections;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000741 RID: 1857
	internal class CommunicationObjectManager<ItemType> : LifetimeManager where ItemType : class, ICommunicationObject
	{
		// Token: 0x060046D8 RID: 18136 RVA: 0x00108532 File Offset: 0x00106732
		public CommunicationObjectManager(object mutex) : base(mutex)
		{
			this.table = new Hashtable();
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x00108548 File Offset: 0x00106748
		public void Add(ItemType item)
		{
			bool flag = false;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State == LifetimeState.Opened && !this.inputClosed)
				{
					if (this.table.ContainsKey(item))
					{
						return;
					}
					this.table.Add(item, item);
					base.IncrementBusyCountWithoutLock();
					item.Closed += this.OnItemClosed;
					flag = true;
				}
			}
			if (!flag)
			{
				item.Abort();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
			}
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x00108608 File Offset: 0x00106808
		public void CloseInput()
		{
			this.inputClosed = true;
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00108611 File Offset: 0x00106811
		public void DecrementActivityCount()
		{
			base.DecrementBusyCount();
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x00108619 File Offset: 0x00106819
		public void IncrementActivityCount()
		{
			this.IncrementBusyCount();
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x00108621 File Offset: 0x00106821
		private void OnItemClosed(object sender, EventArgs args)
		{
			this.Remove((ItemType)((object)sender));
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x00108630 File Offset: 0x00106830
		public void Remove(ItemType item)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (!this.table.ContainsKey(item))
				{
					return;
				}
				this.table.Remove(item);
			}
			item.Closed -= this.OnItemClosed;
			base.DecrementBusyCount();
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x001086B0 File Offset: 0x001068B0
		public ItemType[] ToArray()
		{
			object thisLock = base.ThisLock;
			ItemType[] result;
			lock (thisLock)
			{
				int num = 0;
				ItemType[] array = new ItemType[this.table.Keys.Count];
				foreach (object obj in this.table.Keys)
				{
					ItemType itemType = (ItemType)((object)obj);
					array[num++] = itemType;
				}
				result = array;
			}
			return result;
		}

		// Token: 0x04002DA3 RID: 11683
		private bool inputClosed;

		// Token: 0x04002DA4 RID: 11684
		private Hashtable table;
	}
}
