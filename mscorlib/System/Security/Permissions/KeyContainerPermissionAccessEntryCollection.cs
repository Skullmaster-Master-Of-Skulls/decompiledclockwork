using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200065F RID: 1631
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x06003AD6 RID: 15062 RVA: 0x000C6ACD File Offset: 0x000C5ACD
		private KeyContainerPermissionAccessEntryCollection()
		{
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000C6AD5 File Offset: 0x000C5AD5
		internal KeyContainerPermissionAccessEntryCollection(KeyContainerPermissionFlags globalFlags)
		{
			this.m_list = new ArrayList();
			this.m_globalFlags = globalFlags;
		}

		// Token: 0x170009E9 RID: 2537
		public KeyContainerPermissionAccessEntry this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
				}
				if (index >= this.m_list.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return (KeyContainerPermissionAccessEntry)this.m_list[index];
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x000C6B45 File Offset: 0x000C5B45
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000C6B54 File Offset: 0x000C5B54
		public int Add(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			int num = this.m_list.IndexOf(accessEntry);
			if (num != -1)
			{
				((KeyContainerPermissionAccessEntry)this.m_list[num]).Flags &= accessEntry.Flags;
				return num;
			}
			if (accessEntry.Flags != this.m_globalFlags)
			{
				return this.m_list.Add(accessEntry);
			}
			return -1;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000C6BC1 File Offset: 0x000C5BC1
		public void Clear()
		{
			this.m_list.Clear();
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000C6BCE File Offset: 0x000C5BCE
		public int IndexOf(KeyContainerPermissionAccessEntry accessEntry)
		{
			return this.m_list.IndexOf(accessEntry);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000C6BDC File Offset: 0x000C5BDC
		public void Remove(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			this.m_list.Remove(accessEntry);
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000C6BF8 File Offset: 0x000C5BF8
		public KeyContainerPermissionAccessEntryEnumerator GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this);
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000C6C00 File Offset: 0x000C5C00
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this);
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000C6C08 File Offset: 0x000C5C08
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000C6CA2 File Offset: 0x000C5CA2
		public void CopyTo(KeyContainerPermissionAccessEntry[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000C6CAC File Offset: 0x000C5CAC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000C6CAF File Offset: 0x000C5CAF
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04001E80 RID: 7808
		private ArrayList m_list;

		// Token: 0x04001E81 RID: 7809
		private KeyContainerPermissionFlags m_globalFlags;
	}
}
