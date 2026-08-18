using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000639 RID: 1593
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeIndexerExpression : CodeExpression
	{
		// Token: 0x060039F4 RID: 14836 RVA: 0x000F374D File Offset: 0x000F194D
		public CodeIndexerExpression()
		{
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000F3755 File Offset: 0x000F1955
		public CodeIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.targetObject = targetObject;
			this.indices = new CodeExpressionCollection();
			this.indices.AddRange(indices);
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060039F6 RID: 14838 RVA: 0x000F377B File Offset: 0x000F197B
		// (set) Token: 0x060039F7 RID: 14839 RVA: 0x000F3783 File Offset: 0x000F1983
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

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x000F378C File Offset: 0x000F198C
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

		// Token: 0x04002BD1 RID: 11217
		private CodeExpression targetObject;

		// Token: 0x04002BD2 RID: 11218
		private CodeExpressionCollection indices;
	}
}
