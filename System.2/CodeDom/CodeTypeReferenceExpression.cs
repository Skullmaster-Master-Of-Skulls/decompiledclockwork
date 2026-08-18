using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000667 RID: 1639
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeReferenceExpression : CodeExpression
	{
		// Token: 0x06003B74 RID: 15220 RVA: 0x000F5CC4 File Offset: 0x000F3EC4
		public CodeTypeReferenceExpression()
		{
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000F5CCC File Offset: 0x000F3ECC
		public CodeTypeReferenceExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000F5CDB File Offset: 0x000F3EDB
		public CodeTypeReferenceExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000F5CEF File Offset: 0x000F3EEF
		public CodeTypeReferenceExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000F5D03 File Offset: 0x000F3F03
		// (set) Token: 0x06003B79 RID: 15225 RVA: 0x000F5D23 File Offset: 0x000F3F23
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

		// Token: 0x04002C4F RID: 11343
		private CodeTypeReference type;
	}
}
