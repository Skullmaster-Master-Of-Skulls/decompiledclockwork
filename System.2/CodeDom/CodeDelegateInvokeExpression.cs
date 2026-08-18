using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200062E RID: 1582
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDelegateInvokeExpression : CodeExpression
	{
		// Token: 0x060039B7 RID: 14775 RVA: 0x000F336B File Offset: 0x000F156B
		public CodeDelegateInvokeExpression()
		{
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000F337E File Offset: 0x000F157E
		public CodeDelegateInvokeExpression(CodeExpression targetObject)
		{
			this.TargetObject = targetObject;
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000F3398 File Offset: 0x000F1598
		public CodeDelegateInvokeExpression(CodeExpression targetObject, params CodeExpression[] parameters)
		{
			this.TargetObject = targetObject;
			this.Parameters.AddRange(parameters);
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000F33BE File Offset: 0x000F15BE
		// (set) Token: 0x060039BB RID: 14779 RVA: 0x000F33C6 File Offset: 0x000F15C6
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

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000F33CF File Offset: 0x000F15CF
		public CodeExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002BC7 RID: 11207
		private CodeExpression targetObject;

		// Token: 0x04002BC8 RID: 11208
		private CodeExpressionCollection parameters = new CodeExpressionCollection();
	}
}
