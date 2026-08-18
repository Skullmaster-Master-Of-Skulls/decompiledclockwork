using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000652 RID: 1618
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetCompileUnit : CodeCompileUnit
	{
		// Token: 0x06003AC0 RID: 15040 RVA: 0x000F48FD File Offset: 0x000F2AFD
		public CodeSnippetCompileUnit()
		{
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000F4905 File Offset: 0x000F2B05
		public CodeSnippetCompileUnit(string value)
		{
			this.Value = value;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000F4914 File Offset: 0x000F2B14
		// (set) Token: 0x06003AC3 RID: 15043 RVA: 0x000F492A File Offset: 0x000F2B2A
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

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000F4933 File Offset: 0x000F2B33
		// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x000F493B File Offset: 0x000F2B3B
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
			set
			{
				this.linePragma = value;
			}
		}

		// Token: 0x04002C1F RID: 11295
		private string value;

		// Token: 0x04002C20 RID: 11296
		private CodeLinePragma linePragma;
	}
}
