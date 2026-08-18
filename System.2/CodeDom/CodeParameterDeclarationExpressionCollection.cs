using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200064B RID: 1611
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeParameterDeclarationExpressionCollection : CollectionBase
	{
		// Token: 0x06003A9B RID: 15003 RVA: 0x000F469E File Offset: 0x000F289E
		public CodeParameterDeclarationExpressionCollection()
		{
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000F46A6 File Offset: 0x000F28A6
		public CodeParameterDeclarationExpressionCollection(CodeParameterDeclarationExpressionCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000F46B5 File Offset: 0x000F28B5
		public CodeParameterDeclarationExpressionCollection(CodeParameterDeclarationExpression[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000E1C RID: 3612
		public CodeParameterDeclarationExpression this[int index]
		{
			get
			{
				return (CodeParameterDeclarationExpression)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000F46E6 File Offset: 0x000F28E6
		public int Add(CodeParameterDeclarationExpression value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000F46F4 File Offset: 0x000F28F4
		public void AddRange(CodeParameterDeclarationExpression[] value)
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

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000F4728 File Offset: 0x000F2928
		public void AddRange(CodeParameterDeclarationExpressionCollection value)
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

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000F4764 File Offset: 0x000F2964
		public bool Contains(CodeParameterDeclarationExpression value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000F4772 File Offset: 0x000F2972
		public void CopyTo(CodeParameterDeclarationExpression[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000F4781 File Offset: 0x000F2981
		public int IndexOf(CodeParameterDeclarationExpression value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000F478F File Offset: 0x000F298F
		public void Insert(int index, CodeParameterDeclarationExpression value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000F479E File Offset: 0x000F299E
		public void Remove(CodeParameterDeclarationExpression value)
		{
			base.List.Remove(value);
		}
	}
}
