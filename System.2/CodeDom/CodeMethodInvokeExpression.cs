using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000641 RID: 1601
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMethodInvokeExpression : CodeExpression
	{
		// Token: 0x06003A3A RID: 14906 RVA: 0x000F3D67 File Offset: 0x000F1F67
		public CodeMethodInvokeExpression()
		{
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000F3D7A File Offset: 0x000F1F7A
		public CodeMethodInvokeExpression(CodeMethodReferenceExpression method, params CodeExpression[] parameters)
		{
			this.method = method;
			this.Parameters.AddRange(parameters);
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000F3DA0 File Offset: 0x000F1FA0
		public CodeMethodInvokeExpression(CodeExpression targetObject, string methodName, params CodeExpression[] parameters)
		{
			this.method = new CodeMethodReferenceExpression(targetObject, methodName);
			this.Parameters.AddRange(parameters);
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x000F3DCC File Offset: 0x000F1FCC
		// (set) Token: 0x06003A3E RID: 14910 RVA: 0x000F3DE7 File Offset: 0x000F1FE7
		public CodeMethodReferenceExpression Method
		{
			get
			{
				if (this.method == null)
				{
					this.method = new CodeMethodReferenceExpression();
				}
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x000F3DF0 File Offset: 0x000F1FF0
		public CodeExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002BF6 RID: 11254
		private CodeMethodReferenceExpression method;

		// Token: 0x04002BF7 RID: 11255
		private CodeExpressionCollection parameters = new CodeExpressionCollection();
	}
}
