using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200064C RID: 1612
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodePrimitiveExpression : CodeExpression
	{
		// Token: 0x06003AA8 RID: 15016 RVA: 0x000F47AC File Offset: 0x000F29AC
		public CodePrimitiveExpression()
		{
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000F47B4 File Offset: 0x000F29B4
		public CodePrimitiveExpression(object value)
		{
			this.Value = value;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000F47C3 File Offset: 0x000F29C3
		// (set) Token: 0x06003AAB RID: 15019 RVA: 0x000F47CB File Offset: 0x000F29CB
		public object Value
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

		// Token: 0x04002C13 RID: 11283
		private object value;
	}
}
