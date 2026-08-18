using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000626 RID: 1574
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeComment : CodeObject
	{
		// Token: 0x06003981 RID: 14721 RVA: 0x000F2F82 File Offset: 0x000F1182
		public CodeComment()
		{
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000F2F8A File Offset: 0x000F118A
		public CodeComment(string text)
		{
			this.Text = text;
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000F2F99 File Offset: 0x000F1199
		public CodeComment(string text, bool docComment)
		{
			this.Text = text;
			this.docComment = docComment;
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x000F2FAF File Offset: 0x000F11AF
		// (set) Token: 0x06003985 RID: 14725 RVA: 0x000F2FB7 File Offset: 0x000F11B7
		public bool DocComment
		{
			get
			{
				return this.docComment;
			}
			set
			{
				this.docComment = value;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x000F2FC0 File Offset: 0x000F11C0
		// (set) Token: 0x06003987 RID: 14727 RVA: 0x000F2FD6 File Offset: 0x000F11D6
		public string Text
		{
			get
			{
				if (this.text != null)
				{
					return this.text;
				}
				return string.Empty;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x04002BB6 RID: 11190
		private string text;

		// Token: 0x04002BB7 RID: 11191
		private bool docComment;
	}
}
