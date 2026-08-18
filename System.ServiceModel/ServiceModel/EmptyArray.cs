using System;
using System.Collections.Generic;

namespace System.ServiceModel
{
	// Token: 0x0200011A RID: 282
	internal class EmptyArray<T>
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x0001E57E File Offset: 0x0001C77E
		private EmptyArray()
		{
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001E586 File Offset: 0x0001C786
		internal static T[] Instance
		{
			get
			{
				if (EmptyArray<T>.instance == null)
				{
					EmptyArray<T>.instance = new T[0];
				}
				return EmptyArray<T>.instance;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001E59F File Offset: 0x0001C79F
		internal static T[] Allocate(int n)
		{
			if (n == 0)
			{
				return EmptyArray<T>.Instance;
			}
			return new T[n];
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
		internal static T[] ToArray(IList<T> collection)
		{
			if (collection.Count == 0)
			{
				return EmptyArray<T>.Instance;
			}
			T[] array = new T[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001E5E0 File Offset: 0x0001C7E0
		internal static T[] ToArray(SynchronizedCollection<T> collection)
		{
			object syncRoot = collection.SyncRoot;
			T[] result;
			lock (syncRoot)
			{
				result = EmptyArray<T>.ToArray(collection);
			}
			return result;
		}

		// Token: 0x04000ABB RID: 2747
		private static T[] instance;
	}
}
