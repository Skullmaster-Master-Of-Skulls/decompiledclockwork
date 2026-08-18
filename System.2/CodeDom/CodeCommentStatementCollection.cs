using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000628 RID: 1576
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCommentStatementCollection : CollectionBase
	{
		// Token: 0x0600398E RID: 14734 RVA: 0x000F3030 File Offset: 0x000F1230
		public CodeCommentStatementCollection()
		{
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000F3038 File Offset: 0x000F1238
		public CodeCommentStatementCollection(CodeCommentStatementCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000F3047 File Offset: 0x000F1247
		public CodeCommentStatementCollection(CodeCommentStatement[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000DC7 RID: 3527
		public CodeCommentStatement this[int index]
		{
			get
			{
				return (CodeCommentStatement)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000F3078 File Offset: 0x000F1278
		public int Add(CodeCommentStatement value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000F3088 File Offset: 0x000F1288
		public void AddRange(CodeCommentStatement[] value)
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

		// Token: 0x06003995 RID: 14741 RVA: 0x000F30BC File Offset: 0x000F12BC
		public void AddRange(CodeCommentStatementCollection value)
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

		// Token: 0x06003996 RID: 14742 RVA: 0x000F30F8 File Offset: 0x000F12F8
		public bool Contains(CodeCommentStatement value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000F3106 File Offset: 0x000F1306
		public void CopyTo(CodeCommentStatement[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000F3115 File Offset: 0x000F1315
		public int IndexOf(CodeCommentStatement value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000F3123 File Offset: 0x000F1323
		public void Insert(int index, CodeCommentStatement value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000F3132 File Offset: 0x000F1332
		public void Remove(CodeCommentStatement value)
		{
			base.List.Remove(value);
		}
	}
}
