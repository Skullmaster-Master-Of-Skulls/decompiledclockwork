using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200064D RID: 1613
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodePropertyReferenceExpression : CodeExpression
	{
		// Token: 0x06003AAC RID: 15020 RVA: 0x000F47D4 File Offset: 0x000F29D4
		public CodePropertyReferenceExpression()
		{
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000F47E7 File Offset: 0x000F29E7
		public CodePropertyReferenceExpression(CodeExpression targetObject, string propertyName)
		{
			this.TargetObject = targetObject;
			this.PropertyName = propertyName;
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x000F4808 File Offset: 0x000F2A08
		// (set) Token: 0x06003AAF RID: 15023 RVA: 0x000F4810 File Offset: 0x000F2A10
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

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000F4819 File Offset: 0x000F2A19
		// (set) Token: 0x06003AB1 RID: 15025 RVA: 0x000F482F File Offset: 0x000F2A2F
		public string PropertyName
		{
			get
			{
				if (this.propertyName != null)
				{
					return this.propertyName;
				}
				return string.Empty;
			}
			set
			{
				this.propertyName = value;
			}
		}

		// Token: 0x04002C14 RID: 11284
		private CodeExpression targetObject;

		// Token: 0x04002C15 RID: 11285
		private string propertyName;

		// Token: 0x04002C16 RID: 11286
		private CodeExpressionCollection parameters = new CodeExpressionCollection();
	}
}
