using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000635 RID: 1589
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeExpressionCollection : CollectionBase
	{
		// Token: 0x060039D9 RID: 14809 RVA: 0x000F358E File Offset: 0x000F178E
		public CodeExpressionCollection()
		{
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000F3596 File Offset: 0x000F1796
		public CodeExpressionCollection(CodeExpressionCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000F35A5 File Offset: 0x000F17A5
		public CodeExpressionCollection(CodeExpression[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000DDD RID: 3549
		public CodeExpression this[int index]
		{
			get
			{
				return (CodeExpression)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000F35D6 File Offset: 0x000F17D6
		public int Add(CodeExpression value)
		{
			return base.List.Add(value);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000F35E4 File Offset: 0x000F17E4
		public void AddRange(CodeExpression[] value)
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

		// Token: 0x060039E0 RID: 14816 RVA: 0x000F3618 File Offset: 0x000F1818
		public void AddRange(CodeExpressionCollection value)
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

		// Token: 0x060039E1 RID: 14817 RVA: 0x000F3654 File Offset: 0x000F1854
		public bool Contains(CodeExpression value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000F3662 File Offset: 0x000F1862
		public void CopyTo(CodeExpression[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000F3671 File Offset: 0x000F1871
		public int IndexOf(CodeExpression value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000F367F File Offset: 0x000F187F
		public void Insert(int index, CodeExpression value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000F368E File Offset: 0x000F188E
		public void Remove(CodeExpression value)
		{
			base.List.Remove(value);
		}
	}
}
