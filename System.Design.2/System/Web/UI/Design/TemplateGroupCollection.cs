using System;
using System.Collections;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design
{
	// Token: 0x02000072 RID: 114
	public sealed class TemplateGroupCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000362F File Offset: 0x0000182F
		public TemplateGroupCollection()
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00011E20 File Offset: 0x00010020
		internal TemplateGroupCollection(TemplateGroup[] verbs)
		{
			for (int i = 0; i < verbs.Length; i++)
			{
				this.Add(verbs[i]);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00011E4B File Offset: 0x0001004B
		public int Count
		{
			get
			{
				return this.InternalList.Count;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00011E58 File Offset: 0x00010058
		private ArrayList InternalList
		{
			get
			{
				if (this._list == null)
				{
					this._list = new ArrayList();
				}
				return this._list;
			}
		}

		// Token: 0x170000EA RID: 234
		public TemplateGroup this[int index]
		{
			get
			{
				return (TemplateGroup)this.InternalList[index];
			}
			set
			{
				this.InternalList[index] = value;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011E95 File Offset: 0x00010095
		public int Add(TemplateGroup group)
		{
			return this.InternalList.Add(group);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011EA3 File Offset: 0x000100A3
		public void AddRange(TemplateGroupCollection groups)
		{
			this.InternalList.AddRange(groups);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011EB1 File Offset: 0x000100B1
		public void Clear()
		{
			this.InternalList.Clear();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011EBE File Offset: 0x000100BE
		public bool Contains(TemplateGroup group)
		{
			return this.InternalList.Contains(group);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011ECC File Offset: 0x000100CC
		public void CopyTo(TemplateGroup[] array, int index)
		{
			this.InternalList.CopyTo(array, index);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011EDB File Offset: 0x000100DB
		public int IndexOf(TemplateGroup group)
		{
			return this.InternalList.IndexOf(group);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00011EE9 File Offset: 0x000100E9
		public void Insert(int index, TemplateGroup group)
		{
			this.InternalList.Insert(index, group);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011EF8 File Offset: 0x000100F8
		public void Remove(TemplateGroup group)
		{
			this.InternalList.Remove(group);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00011F06 File Offset: 0x00010106
		public void RemoveAt(int index)
		{
			this.InternalList.RemoveAt(index);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00011F14 File Offset: 0x00010114
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00011F1C File Offset: 0x0001011C
		bool IList.IsFixedSize
		{
			get
			{
				return this.InternalList.IsFixedSize;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00011F29 File Offset: 0x00010129
		bool IList.IsReadOnly
		{
			get
			{
				return this.InternalList.IsReadOnly;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00011F36 File Offset: 0x00010136
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InternalList.IsSynchronized;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00011F43 File Offset: 0x00010143
		object ICollection.SyncRoot
		{
			get
			{
				return this.InternalList.SyncRoot;
			}
		}

		// Token: 0x170000F0 RID: 240
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is TemplateGroup))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
					{
						"TemplateGroup"
					}), "value");
				}
				this[index] = (TemplateGroup)value;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00011FAC File Offset: 0x000101AC
		int IList.Add(object o)
		{
			if (!(o is TemplateGroup))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"TemplateGroup"
				}), "o");
			}
			return this.Add((TemplateGroup)o);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00011FFA File Offset: 0x000101FA
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00012004 File Offset: 0x00010204
		bool IList.Contains(object o)
		{
			if (!(o is TemplateGroup))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"TemplateGroup"
				}), "o");
			}
			return this.Contains((TemplateGroup)o);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00011ECC File Offset: 0x000100CC
		void ICollection.CopyTo(Array array, int index)
		{
			this.InternalList.CopyTo(array, index);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00012052 File Offset: 0x00010252
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.InternalList.GetEnumerator();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00012060 File Offset: 0x00010260
		int IList.IndexOf(object o)
		{
			if (!(o is TemplateGroup))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"TemplateGroup"
				}), "o");
			}
			return this.IndexOf((TemplateGroup)o);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000120B0 File Offset: 0x000102B0
		void IList.Insert(int index, object o)
		{
			if (!(o is TemplateGroup))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"TemplateGroup"
				}), "o");
			}
			this.Insert(index, (TemplateGroup)o);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00012100 File Offset: 0x00010300
		void IList.Remove(object o)
		{
			if (!(o is TemplateGroup))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"TemplateGroup"
				}), "o");
			}
			this.Remove((TemplateGroup)o);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001214E File Offset: 0x0001034E
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x04000193 RID: 403
		private ArrayList _list;
	}
}
