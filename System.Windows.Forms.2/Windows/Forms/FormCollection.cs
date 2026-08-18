using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000264 RID: 612
	public class FormCollection : ReadOnlyCollectionBase
	{
		// Token: 0x17000923 RID: 2339
		public virtual Form this[string name]
		{
			get
			{
				if (name != null)
				{
					object collectionSyncRoot = FormCollection.CollectionSyncRoot;
					lock (collectionSyncRoot)
					{
						foreach (object obj in base.InnerList)
						{
							Form form = (Form)obj;
							if (string.Equals(form.Name, name, StringComparison.OrdinalIgnoreCase))
							{
								return form;
							}
						}
					}
				}
				return null;
			}
		}

		// Token: 0x17000924 RID: 2340
		public virtual Form this[int index]
		{
			get
			{
				Form result = null;
				object collectionSyncRoot = FormCollection.CollectionSyncRoot;
				lock (collectionSyncRoot)
				{
					result = (Form)base.InnerList[index];
				}
				return result;
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000B8E74 File Offset: 0x000B7074
		internal void Add(Form form)
		{
			object collectionSyncRoot = FormCollection.CollectionSyncRoot;
			lock (collectionSyncRoot)
			{
				base.InnerList.Add(form);
			}
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000B8EBC File Offset: 0x000B70BC
		internal bool Contains(Form form)
		{
			bool result = false;
			object collectionSyncRoot = FormCollection.CollectionSyncRoot;
			lock (collectionSyncRoot)
			{
				result = base.InnerList.Contains(form);
			}
			return result;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000B8F08 File Offset: 0x000B7108
		internal void Remove(Form form)
		{
			object collectionSyncRoot = FormCollection.CollectionSyncRoot;
			lock (collectionSyncRoot)
			{
				base.InnerList.Remove(form);
			}
		}

		// Token: 0x04001049 RID: 4169
		internal static object CollectionSyncRoot = new object();
	}
}
