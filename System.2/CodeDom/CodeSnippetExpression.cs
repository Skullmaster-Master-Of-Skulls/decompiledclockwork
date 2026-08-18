using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000653 RID: 1619
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetExpression : CodeExpression
	{
		// Token: 0x06003AC6 RID: 15046 RVA: 0x000F4944 File Offset: 0x000F2B44
		public CodeSnippetExpression()
		{
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000F494C File Offset: 0x000F2B4C
		public CodeSnippetExpression(string value)
		{
			this.Value = value;
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000F495B File Offset: 0x000F2B5B
		// (set) Token: 0x06003AC9 RID: 15049 RVA: 0x000F4971 File Offset: 0x000F2B71
		public string Value
		{
			get
			{
				if (this.value != null)
				{
					return this.value;
				}
				return string.Empty;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04002C21 RID: 11297
		private string value;
	}
}
