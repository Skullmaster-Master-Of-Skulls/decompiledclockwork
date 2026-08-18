using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000649 RID: 1609
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeObjectCreateExpression : CodeExpression
	{
		// Token: 0x06003A88 RID: 14984 RVA: 0x000F450D File Offset: 0x000F270D
		public CodeObjectCreateExpression()
		{
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000F4520 File Offset: 0x000F2720
		public CodeObjectCreateExpression(CodeTypeReference createType, params CodeExpression[] parameters)
		{
			this.CreateType = createType;
			this.Parameters.AddRange(parameters);
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000F4546 File Offset: 0x000F2746
		public CodeObjectCreateExpression(string createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x000F4571 File Offset: 0x000F2771
		public CodeObjectCreateExpression(Type createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000F459C File Offset: 0x000F279C
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000F45BC File Offset: 0x000F27BC
		public CodeTypeReference CreateType
		{
			get
			{
				if (this.createType == null)
				{
					this.createType = new CodeTypeReference("");
				}
				return this.createType;
			}
			set
			{
				this.createType = value;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000F45C5 File Offset: 0x000F27C5
		public CodeExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002C0D RID: 11277
		private CodeTypeReference createType;

		// Token: 0x04002C0E RID: 11278
		private CodeExpressionCollection parameters = new CodeExpressionCollection();
	}
}
