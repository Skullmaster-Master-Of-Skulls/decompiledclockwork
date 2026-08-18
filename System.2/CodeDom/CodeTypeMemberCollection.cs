using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000660 RID: 1632
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeMemberCollection : CollectionBase
	{
		// Token: 0x06003B24 RID: 15140 RVA: 0x000F51D9 File Offset: 0x000F33D9
		public CodeTypeMemberCollection()
		{
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000F51E1 File Offset: 0x000F33E1
		public CodeTypeMemberCollection(CodeTypeMemberCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000F51F0 File Offset: 0x000F33F0
		public CodeTypeMemberCollection(CodeTypeMember[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000E44 RID: 3652
		public CodeTypeMember this[int index]
		{
			get
			{
				return (CodeTypeMember)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000F5221 File Offset: 0x000F3421
		public int Add(CodeTypeMember value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000F5230 File Offset: 0x000F3430
		public void AddRange(CodeTypeMember[] value)
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

		// Token: 0x06003B2B RID: 15147 RVA: 0x000F5264 File Offset: 0x000F3464
		public void AddRange(CodeTypeMemberCollection value)
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

		// Token: 0x06003B2C RID: 15148 RVA: 0x000F52A0 File Offset: 0x000F34A0
		public bool Contains(CodeTypeMember value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000F52AE File Offset: 0x000F34AE
		public void CopyTo(CodeTypeMember[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000F52BD File Offset: 0x000F34BD
		public int IndexOf(CodeTypeMember value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000F52CB File Offset: 0x000F34CB
		public void Insert(int index, CodeTypeMember value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000F52DA File Offset: 0x000F34DA
		public void Remove(CodeTypeMember value)
		{
			base.List.Remove(value);
		}
	}
}
