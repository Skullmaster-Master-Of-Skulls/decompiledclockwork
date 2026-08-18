using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200065D RID: 1629
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeDeclarationCollection : CollectionBase
	{
		// Token: 0x06003B06 RID: 15110 RVA: 0x000F4F6D File Offset: 0x000F316D
		public CodeTypeDeclarationCollection()
		{
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000F4F75 File Offset: 0x000F3175
		public CodeTypeDeclarationCollection(CodeTypeDeclarationCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000F4F84 File Offset: 0x000F3184
		public CodeTypeDeclarationCollection(CodeTypeDeclaration[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000E3A RID: 3642
		public CodeTypeDeclaration this[int index]
		{
			get
			{
				return (CodeTypeDeclaration)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x000F4FB5 File Offset: 0x000F31B5
		public int Add(CodeTypeDeclaration value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x000F4FC4 File Offset: 0x000F31C4
		public void AddRange(CodeTypeDeclaration[] value)
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

		// Token: 0x06003B0D RID: 15117 RVA: 0x000F4FF8 File Offset: 0x000F31F8
		public void AddRange(CodeTypeDeclarationCollection value)
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

		// Token: 0x06003B0E RID: 15118 RVA: 0x000F5034 File Offset: 0x000F3234
		public bool Contains(CodeTypeDeclaration value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000F5042 File Offset: 0x000F3242
		public void CopyTo(CodeTypeDeclaration[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000F5051 File Offset: 0x000F3251
		public int IndexOf(CodeTypeDeclaration value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x000F505F File Offset: 0x000F325F
		public void Insert(int index, CodeTypeDeclaration value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x000F506E File Offset: 0x000F326E
		public void Remove(CodeTypeDeclaration value)
		{
			base.List.Remove(value);
		}
	}
}
