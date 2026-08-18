using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000654 RID: 1620
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetStatement : CodeStatement
	{
		// Token: 0x06003ACA RID: 15050 RVA: 0x000F497A File Offset: 0x000F2B7A
		public CodeSnippetStatement()
		{
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x000F4982 File Offset: 0x000F2B82
		public CodeSnippetStatement(string value)
		{
			this.Value = value;
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x000F4991 File Offset: 0x000F2B91
		// (set) Token: 0x06003ACD RID: 15053 RVA: 0x000F49A7 File Offset: 0x000F2BA7
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

		// Token: 0x04002C22 RID: 11298
		private string value;
	}
}
