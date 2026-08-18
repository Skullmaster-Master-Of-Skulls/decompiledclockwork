using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000666 RID: 1638
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeReferenceCollection : CollectionBase
	{
		// Token: 0x06003B65 RID: 15205 RVA: 0x000F5B95 File Offset: 0x000F3D95
		public CodeTypeReferenceCollection()
		{
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x000F5B9D File Offset: 0x000F3D9D
		public CodeTypeReferenceCollection(CodeTypeReferenceCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000F5BAC File Offset: 0x000F3DAC
		public CodeTypeReferenceCollection(CodeTypeReference[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000E52 RID: 3666
		public CodeTypeReference this[int index]
		{
			get
			{
				return (CodeTypeReference)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000F5BDD File Offset: 0x000F3DDD
		public int Add(CodeTypeReference value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000F5BEB File Offset: 0x000F3DEB
		public void Add(string value)
		{
			this.Add(new CodeTypeReference(value));
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000F5BFA File Offset: 0x000F3DFA
		public void Add(Type value)
		{
			this.Add(new CodeTypeReference(value));
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000F5C0C File Offset: 0x000F3E0C
		public void AddRange(CodeTypeReference[] value)
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

		// Token: 0x06003B6E RID: 15214 RVA: 0x000F5C40 File Offset: 0x000F3E40
		public void AddRange(CodeTypeReferenceCollection value)
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

		// Token: 0x06003B6F RID: 15215 RVA: 0x000F5C7C File Offset: 0x000F3E7C
		public bool Contains(CodeTypeReference value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000F5C8A File Offset: 0x000F3E8A
		public void CopyTo(CodeTypeReference[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000F5C99 File Offset: 0x000F3E99
		public int IndexOf(CodeTypeReference value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000F5CA7 File Offset: 0x000F3EA7
		public void Insert(int index, CodeTypeReference value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x000F5CB6 File Offset: 0x000F3EB6
		public void Remove(CodeTypeReference value)
		{
			base.List.Remove(value);
		}
	}
}
