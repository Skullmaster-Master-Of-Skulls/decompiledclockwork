using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000655 RID: 1621
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetTypeMember : CodeTypeMember
	{
		// Token: 0x06003ACE RID: 15054 RVA: 0x000F49B0 File Offset: 0x000F2BB0
		public CodeSnippetTypeMember()
		{
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000F49B8 File Offset: 0x000F2BB8
		public CodeSnippetTypeMember(string text)
		{
			this.Text = text;
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x000F49C7 File Offset: 0x000F2BC7
		// (set) Token: 0x06003AD1 RID: 15057 RVA: 0x000F49DD File Offset: 0x000F2BDD
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

		// Token: 0x04002C23 RID: 11299
		private string text;
	}
}
