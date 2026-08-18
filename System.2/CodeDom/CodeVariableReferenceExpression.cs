using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000669 RID: 1641
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeVariableReferenceExpression : CodeExpression
	{
		// Token: 0x06003B87 RID: 15239 RVA: 0x000F5E3A File Offset: 0x000F403A
		public CodeVariableReferenceExpression()
		{
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000F5E42 File Offset: 0x000F4042
		public CodeVariableReferenceExpression(string variableName)
		{
			this.variableName = variableName;
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x000F5E51 File Offset: 0x000F4051
		// (set) Token: 0x06003B8A RID: 15242 RVA: 0x000F5E67 File Offset: 0x000F4067
		public string VariableName
		{
			get
			{
				if (this.variableName != null)
				{
					return this.variableName;
				}
				return string.Empty;
			}
			set
			{
				this.variableName = value;
			}
		}

		// Token: 0x04002C53 RID: 11347
		private string variableName;
	}
}
