using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000661 RID: 1633
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeOfExpression : CodeExpression
	{
		// Token: 0x06003B31 RID: 15153 RVA: 0x000F52E8 File Offset: 0x000F34E8
		public CodeTypeOfExpression()
		{
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000F52F0 File Offset: 0x000F34F0
		public CodeTypeOfExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000F52FF File Offset: 0x000F34FF
		public CodeTypeOfExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000F5313 File Offset: 0x000F3513
		public CodeTypeOfExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003B35 RID: 15157 RVA: 0x000F5327 File Offset: 0x000F3527
		// (set) Token: 0x06003B36 RID: 15158 RVA: 0x000F5347 File Offset: 0x000F3547
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = new CodeTypeReference("");
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04002C40 RID: 11328
		private CodeTypeReference type;
	}
}
