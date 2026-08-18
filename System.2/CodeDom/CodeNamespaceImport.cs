using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000646 RID: 1606
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeNamespaceImport : CodeObject
	{
		// Token: 0x06003A67 RID: 14951 RVA: 0x000F427C File Offset: 0x000F247C
		public CodeNamespaceImport()
		{
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000F4284 File Offset: 0x000F2484
		public CodeNamespaceImport(string nameSpace)
		{
			this.Namespace = nameSpace;
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000F4293 File Offset: 0x000F2493
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x000F429B File Offset: 0x000F249B
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

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x000F42A4 File Offset: 0x000F24A4
		// (set) Token: 0x06003A6C RID: 14956 RVA: 0x000F42BA File Offset: 0x000F24BA
		public string Namespace
		{
			get
			{
				if (this.nameSpace != null)
				{
					return this.nameSpace;
				}
				return string.Empty;
			}
			set
			{
				this.nameSpace = value;
			}
		}

		// Token: 0x04002C08 RID: 11272
		private string nameSpace;

		// Token: 0x04002C09 RID: 11273
		private CodeLinePragma linePragma;
	}
}
