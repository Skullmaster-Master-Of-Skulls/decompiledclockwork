using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200065E RID: 1630
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeDelegate : CodeTypeDeclaration
	{
		// Token: 0x06003B13 RID: 15123 RVA: 0x000F507C File Offset: 0x000F327C
		public CodeTypeDelegate()
		{
			base.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
			base.TypeAttributes |= TypeAttributes.NotPublic;
			base.BaseTypes.Clear();
			base.BaseTypes.Add(new CodeTypeReference("System.Delegate"));
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x000F50D8 File Offset: 0x000F32D8
		public CodeTypeDelegate(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000F50E7 File Offset: 0x000F32E7
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x000F5107 File Offset: 0x000F3307
		public CodeTypeReference ReturnType
		{
			get
			{
				if (this.returnType == null)
				{
					this.returnType = new CodeTypeReference("");
				}
				return this.returnType;
			}
			set
			{
				this.returnType = value;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000F5110 File Offset: 0x000F3310
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002C37 RID: 11319
		private CodeParameterDeclarationExpressionCollection parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002C38 RID: 11320
		private CodeTypeReference returnType;
	}
}
