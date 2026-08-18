using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000645 RID: 1605
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeNamespaceCollection : CollectionBase
	{
		// Token: 0x06003A5A RID: 14938 RVA: 0x000F416E File Offset: 0x000F236E
		public CodeNamespaceCollection()
		{
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000F4176 File Offset: 0x000F2376
		public CodeNamespaceCollection(CodeNamespaceCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000F4185 File Offset: 0x000F2385
		public CodeNamespaceCollection(CodeNamespace[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000E0A RID: 3594
		public CodeNamespace this[int index]
		{
			get
			{
				return (CodeNamespace)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000F41B6 File Offset: 0x000F23B6
		public int Add(CodeNamespace value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000F41C4 File Offset: 0x000F23C4
		public void AddRange(CodeNamespace[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000F41F8 File Offset: 0x000F23F8
		public void AddRange(CodeNamespaceCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x000F4234 File Offset: 0x000F2434
		public bool Contains(CodeNamespace value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000F4242 File Offset: 0x000F2442
		public void CopyTo(CodeNamespace[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000F4251 File Offset: 0x000F2451
		public int IndexOf(CodeNamespace value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000F425F File Offset: 0x000F245F
		public void Insert(int index, CodeNamespace value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000F426E File Offset: 0x000F246E
		public void Remove(CodeNamespace value)
		{
			base.List.Remove(value);
		}
	}
}
