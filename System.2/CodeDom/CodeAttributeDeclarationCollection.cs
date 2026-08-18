using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200061E RID: 1566
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAttributeDeclarationCollection : CollectionBase
	{
		// Token: 0x06003945 RID: 14661 RVA: 0x000F2B59 File Offset: 0x000F0D59
		public CodeAttributeDeclarationCollection()
		{
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000F2B61 File Offset: 0x000F0D61
		public CodeAttributeDeclarationCollection(CodeAttributeDeclarationCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000F2B70 File Offset: 0x000F0D70
		public CodeAttributeDeclarationCollection(CodeAttributeDeclaration[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000DB7 RID: 3511
		public CodeAttributeDeclaration this[int index]
		{
			get
			{
				return (CodeAttributeDeclaration)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000F2BA1 File Offset: 0x000F0DA1
		public int Add(CodeAttributeDeclaration value)
		{
			return base.List.Add(value);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000F2BB0 File Offset: 0x000F0DB0
		public void AddRange(CodeAttributeDeclaration[] value)
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

		// Token: 0x0600394C RID: 14668 RVA: 0x000F2BE4 File Offset: 0x000F0DE4
		public void AddRange(CodeAttributeDeclarationCollection value)
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

		// Token: 0x0600394D RID: 14669 RVA: 0x000F2C20 File Offset: 0x000F0E20
		public bool Contains(CodeAttributeDeclaration value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000F2C2E File Offset: 0x000F0E2E
		public void CopyTo(CodeAttributeDeclaration[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000F2C3D File Offset: 0x000F0E3D
		public int IndexOf(CodeAttributeDeclaration value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000F2C4B File Offset: 0x000F0E4B
		public void Insert(int index, CodeAttributeDeclaration value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000F2C5A File Offset: 0x000F0E5A
		public void Remove(CodeAttributeDeclaration value)
		{
			base.List.Remove(value);
		}
	}
}
