using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200063C RID: 1596
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeLinePragma
	{
		// Token: 0x06003A09 RID: 14857 RVA: 0x000F3887 File Offset: 0x000F1A87
		public CodeLinePragma()
		{
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000F388F File Offset: 0x000F1A8F
		public CodeLinePragma(string fileName, int lineNumber)
		{
			this.FileName = fileName;
			this.LineNumber = lineNumber;
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000F38A5 File Offset: 0x000F1AA5
		// (set) Token: 0x06003A0C RID: 14860 RVA: 0x000F38BB File Offset: 0x000F1ABB
		public string FileName
		{
			get
			{
				if (this.fileName != null)
				{
					return this.fileName;
				}
				return string.Empty;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000F38C4 File Offset: 0x000F1AC4
		// (set) Token: 0x06003A0E RID: 14862 RVA: 0x000F38CC File Offset: 0x000F1ACC
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x04002BD9 RID: 11225
		private string fileName;

		// Token: 0x04002BDA RID: 11226
		private int lineNumber;
	}
}
