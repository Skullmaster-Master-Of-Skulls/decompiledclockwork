using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000622 RID: 1570
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCastExpression : CodeExpression
	{
		// Token: 0x0600395B RID: 14683 RVA: 0x000F2CC8 File Offset: 0x000F0EC8
		public CodeCastExpression()
		{
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000F2CD0 File Offset: 0x000F0ED0
		public CodeCastExpression(CodeTypeReference targetType, CodeExpression expression)
		{
			this.TargetType = targetType;
			this.Expression = expression;
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000F2CE6 File Offset: 0x000F0EE6
		public CodeCastExpression(string targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000F2D01 File Offset: 0x000F0F01
		public CodeCastExpression(Type targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000F2D1C File Offset: 0x000F0F1C
		// (set) Token: 0x06003960 RID: 14688 RVA: 0x000F2D3C File Offset: 0x000F0F3C
		public CodeTypeReference TargetType
		{
			get
			{
				if (this.targetType == null)
				{
					this.targetType = new CodeTypeReference("");
				}
				return this.targetType;
			}
			set
			{
				this.targetType = value;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x000F2D45 File Offset: 0x000F0F45
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x000F2D4D File Offset: 0x000F0F4D
		public CodeExpression Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		// Token: 0x04002BAE RID: 11182
		private CodeTypeReference targetType;

		// Token: 0x04002BAF RID: 11183
		private CodeExpression expression;
	}
}
