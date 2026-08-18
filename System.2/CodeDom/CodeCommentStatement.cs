using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000627 RID: 1575
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCommentStatement : CodeStatement
	{
		// Token: 0x06003988 RID: 14728 RVA: 0x000F2FDF File Offset: 0x000F11DF
		public CodeCommentStatement()
		{
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000F2FE7 File Offset: 0x000F11E7
		public CodeCommentStatement(CodeComment comment)
		{
			this.comment = comment;
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000F2FF6 File Offset: 0x000F11F6
		public CodeCommentStatement(string text)
		{
			this.comment = new CodeComment(text);
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000F300A File Offset: 0x000F120A
		public CodeCommentStatement(string text, bool docComment)
		{
			this.comment = new CodeComment(text, docComment);
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x000F301F File Offset: 0x000F121F
		// (set) Token: 0x0600398D RID: 14733 RVA: 0x000F3027 File Offset: 0x000F1227
		public CodeComment Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x04002BB8 RID: 11192
		private CodeComment comment;
	}
}
