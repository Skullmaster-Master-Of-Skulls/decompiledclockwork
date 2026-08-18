using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000618 RID: 1560
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeArrayIndexerExpression : CodeExpression
	{
		// Token: 0x06003916 RID: 14614 RVA: 0x000F2815 File Offset: 0x000F0A15
		public CodeArrayIndexerExpression()
		{
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000F281D File Offset: 0x000F0A1D
		public CodeArrayIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.targetObject = targetObject;
			this.indices = new CodeExpressionCollection();
			this.indices.AddRange(indices);
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x000F2843 File Offset: 0x000F0A43
		// (set) Token: 0x06003919 RID: 14617 RVA: 0x000F284B File Offset: 0x000F0A4B
		public CodeExpression TargetObject
		{
			get
			{
				return this.targetObject;
			}
			set
			{
				this.targetObject = value;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600391A RID: 14618 RVA: 0x000F2854 File Offset: 0x000F0A54
		public CodeExpressionCollection Indices
		{
			get
			{
				if (this.indices == null)
				{
					this.indices = new CodeExpressionCollection();
				}
				return this.indices;
			}
		}

		// Token: 0x04002B8E RID: 11150
		private CodeExpression targetObject;

		// Token: 0x04002B8F RID: 11151
		private CodeExpressionCollection indices;
	}
}
