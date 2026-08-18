using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200063B RID: 1595
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeLabeledStatement : CodeStatement
	{
		// Token: 0x06003A02 RID: 14850 RVA: 0x000F382A File Offset: 0x000F1A2A
		public CodeLabeledStatement()
		{
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000F3832 File Offset: 0x000F1A32
		public CodeLabeledStatement(string label)
		{
			this.label = label;
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000F3841 File Offset: 0x000F1A41
		public CodeLabeledStatement(string label, CodeStatement statement)
		{
			this.label = label;
			this.statement = statement;
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000F3857 File Offset: 0x000F1A57
		// (set) Token: 0x06003A06 RID: 14854 RVA: 0x000F386D File Offset: 0x000F1A6D
		public string Label
		{
			get
			{
				if (this.label != null)
				{
					return this.label;
				}
				return string.Empty;
			}
			set
			{
				this.label = value;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000F3876 File Offset: 0x000F1A76
		// (set) Token: 0x06003A08 RID: 14856 RVA: 0x000F387E File Offset: 0x000F1A7E
		public CodeStatement Statement
		{
			get
			{
				return this.statement;
			}
			set
			{
				this.statement = value;
			}
		}

		// Token: 0x04002BD7 RID: 11223
		private string label;

		// Token: 0x04002BD8 RID: 11224
		private CodeStatement statement;
	}
}
