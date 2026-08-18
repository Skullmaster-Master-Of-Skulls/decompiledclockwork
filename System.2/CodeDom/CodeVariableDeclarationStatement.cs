using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000668 RID: 1640
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeVariableDeclarationStatement : CodeStatement
	{
		// Token: 0x06003B7A RID: 15226 RVA: 0x000F5D2C File Offset: 0x000F3F2C
		public CodeVariableDeclarationStatement()
		{
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000F5D34 File Offset: 0x000F3F34
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000F5D4A File Offset: 0x000F3F4A
		public CodeVariableDeclarationStatement(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000F5D65 File Offset: 0x000F3F65
		public CodeVariableDeclarationStatement(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000F5D80 File Offset: 0x000F3F80
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name, CodeExpression initExpression)
		{
			this.Type = type;
			this.Name = name;
			this.InitExpression = initExpression;
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000F5D9D File Offset: 0x000F3F9D
		public CodeVariableDeclarationStatement(string type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000F5DBF File Offset: 0x000F3FBF
		public CodeVariableDeclarationStatement(Type type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000F5DE1 File Offset: 0x000F3FE1
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x000F5DE9 File Offset: 0x000F3FE9
		public CodeExpression InitExpression
		{
			get
			{
				return this.initExpression;
			}
			set
			{
				this.initExpression = value;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x000F5DF2 File Offset: 0x000F3FF2
		// (set) Token: 0x06003B84 RID: 15236 RVA: 0x000F5E08 File Offset: 0x000F4008
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

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x000F5E11 File Offset: 0x000F4011
		// (set) Token: 0x06003B86 RID: 15238 RVA: 0x000F5E31 File Offset: 0x000F4031
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = new CodeTypeReference("");
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04002C50 RID: 11344
		private CodeTypeReference type;

		// Token: 0x04002C51 RID: 11345
		private string name;

		// Token: 0x04002C52 RID: 11346
		private CodeExpression initExpression;
	}
}
