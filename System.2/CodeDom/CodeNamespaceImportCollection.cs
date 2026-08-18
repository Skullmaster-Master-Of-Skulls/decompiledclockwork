using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000647 RID: 1607
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeNamespaceImportCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000E0D RID: 3597
		public CodeNamespaceImport this[int index]
		{
			get
			{
				return (CodeNamespaceImport)this.data[index];
			}
			set
			{
				this.data[index] = value;
				this.SyncKeys();
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x000F42EB File Offset: 0x000F24EB
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x000F42F8 File Offset: 0x000F24F8
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000F42FB File Offset: 0x000F24FB
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000F42FE File Offset: 0x000F24FE
		public void Add(CodeNamespaceImport value)
		{
			if (!this.keys.ContainsKey(value.Namespace))
			{
				this.keys[value.Namespace] = value;
				this.data.Add(value);
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000F4334 File Offset: 0x000F2534
		public void AddRange(CodeNamespaceImport[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (CodeNamespaceImport value2 in value)
			{
				this.Add(value2);
			}
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000F436A File Offset: 0x000F256A
		public void Clear()
		{
			this.data.Clear();
			this.keys.Clear();
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000F4384 File Offset: 0x000F2584
		private void SyncKeys()
		{
			this.keys = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in this)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				this.keys[codeNamespaceImport.Namespace] = codeNamespaceImport;
			}
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000F43F4 File Offset: 0x000F25F4
		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x17000E11 RID: 3601
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (CodeNamespaceImport)value;
				this.SyncKeys();
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003A79 RID: 14969 RVA: 0x000F441F File Offset: 0x000F261F
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x000F4427 File Offset: 0x000F2627
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000F442A File Offset: 0x000F262A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000F442D File Offset: 0x000F262D
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000F443C File Offset: 0x000F263C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000F4444 File Offset: 0x000F2644
		int IList.Add(object value)
		{
			return this.data.Add((CodeNamespaceImport)value);
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000F4457 File Offset: 0x000F2657
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000F445F File Offset: 0x000F265F
		bool IList.Contains(object value)
		{
			return this.data.Contains(value);
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000F446D File Offset: 0x000F266D
		int IList.IndexOf(object value)
		{
			return this.data.IndexOf((CodeNamespaceImport)value);
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000F4480 File Offset: 0x000F2680
		void IList.Insert(int index, object value)
		{
			this.data.Insert(index, (CodeNamespaceImport)value);
			this.SyncKeys();
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000F449A File Offset: 0x000F269A
		void IList.Remove(object value)
		{
			this.data.Remove((CodeNamespaceImport)value);
			this.SyncKeys();
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000F44B3 File Offset: 0x000F26B3
		void IList.RemoveAt(int index)
		{
			this.data.RemoveAt(index);
			this.SyncKeys();
		}

		// Token: 0x04002C0A RID: 11274
		private ArrayList data = new ArrayList();

		// Token: 0x04002C0B RID: 11275
		private Hashtable keys = new Hashtable(StringComparer.OrdinalIgnoreCase);
	}
}
