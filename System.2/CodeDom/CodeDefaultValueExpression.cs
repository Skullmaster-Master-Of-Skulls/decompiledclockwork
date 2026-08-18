using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200062C RID: 1580
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDefaultValueExpression : CodeExpression
	{
		// Token: 0x060039AB RID: 14763 RVA: 0x000F32AD File Offset: 0x000F14AD
		public CodeDefaultValueExpression()
		{
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000F32B5 File Offset: 0x000F14B5
		public CodeDefaultValueExpression(CodeTypeReference type)
		{
			this.type = type;
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000F32C4 File Offset: 0x000F14C4
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000F32E4 File Offset: 0x000F14E4
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

		// Token: 0x04002BC3 RID: 11203
		private CodeTypeReference type;
	}
}
