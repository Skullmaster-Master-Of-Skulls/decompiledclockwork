using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000616 RID: 1558
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeArgumentReferenceExpression : CodeExpression
	{
		// Token: 0x06003901 RID: 14593 RVA: 0x000F2623 File Offset: 0x000F0823
		public CodeArgumentReferenceExpression()
		{
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000F262B File Offset: 0x000F082B
		public CodeArgumentReferenceExpression(string parameterName)
		{
			this.parameterName = parameterName;
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000F263A File Offset: 0x000F083A
		// (set) Token: 0x06003904 RID: 14596 RVA: 0x000F2650 File Offset: 0x000F0850
		public string ParameterName
		{
			get
			{
				if (this.parameterName != null)
				{
					return this.parameterName;
				}
				return string.Empty;
			}
			set
			{
				this.parameterName = value;
			}
		}

		// Token: 0x04002B89 RID: 11145
		private string parameterName;
	}
}
