using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200061B RID: 1563
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAttributeArgument
	{
		// Token: 0x06003928 RID: 14632 RVA: 0x000F2919 File Offset: 0x000F0B19
		public CodeAttributeArgument()
		{
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000F2921 File Offset: 0x000F0B21
		public CodeAttributeArgument(CodeExpression value)
		{
			this.Value = value;
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000F2930 File Offset: 0x000F0B30
		public CodeAttributeArgument(string name, CodeExpression value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600392B RID: 14635 RVA: 0x000F2946 File Offset: 0x000F0B46
		// (set) Token: 0x0600392C RID: 14636 RVA: 0x000F295C File Offset: 0x000F0B5C
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x000F2965 File Offset: 0x000F0B65
		// (set) Token: 0x0600392E RID: 14638 RVA: 0x000F296D File Offset: 0x000F0B6D
		public CodeExpression Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04002B94 RID: 11156
		private string name;

		// Token: 0x04002B95 RID: 11157
		private CodeExpression value;
	}
}
