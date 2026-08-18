using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000657 RID: 1623
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeStatementCollection : CollectionBase
	{
		// Token: 0x06003AD7 RID: 15063 RVA: 0x000F4A35 File Offset: 0x000F2C35
		public CodeStatementCollection()
		{
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x000F4A3D File Offset: 0x000F2C3D
		public CodeStatementCollection(CodeStatementCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000F4A4C File Offset: 0x000F2C4C
		public CodeStatementCollection(CodeStatement[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000E2C RID: 3628
		public CodeStatement this[int index]
		{
			get
			{
				return (CodeStatement)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000F4A7D File Offset: 0x000F2C7D
		public int Add(CodeStatement value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000F4A8B File Offset: 0x000F2C8B
		public int Add(CodeExpression value)
		{
			return this.Add(new CodeExpressionStatement(value));
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000F4A9C File Offset: 0x000F2C9C
		public void AddRange(CodeStatement[] value)
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

		// Token: 0x06003ADF RID: 15071 RVA: 0x000F4AD0 File Offset: 0x000F2CD0
		public void AddRange(CodeStatementCollection value)
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

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000F4B0C File Offset: 0x000F2D0C
		public bool Contains(CodeStatement value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000F4B1A File Offset: 0x000F2D1A
		public void CopyTo(CodeStatement[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000F4B29 File Offset: 0x000F2D29
		public int IndexOf(CodeStatement value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000F4B37 File Offset: 0x000F2D37
		public void Insert(int index, CodeStatement value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000F4B46 File Offset: 0x000F2D46
		public void Remove(CodeStatement value)
		{
			base.List.Remove(value);
		}
	}
}
