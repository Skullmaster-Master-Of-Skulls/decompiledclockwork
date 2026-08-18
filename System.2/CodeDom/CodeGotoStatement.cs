using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000638 RID: 1592
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeGotoStatement : CodeStatement
	{
		// Token: 0x060039F0 RID: 14832 RVA: 0x000F3712 File Offset: 0x000F1912
		public CodeGotoStatement()
		{
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000F371A File Offset: 0x000F191A
		public CodeGotoStatement(string label)
		{
			this.Label = label;
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x000F3729 File Offset: 0x000F1929
		// (set) Token: 0x060039F3 RID: 14835 RVA: 0x000F3731 File Offset: 0x000F1931
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this.label = value;
			}
		}

		// Token: 0x04002BD0 RID: 11216
		private string label;
	}
}
