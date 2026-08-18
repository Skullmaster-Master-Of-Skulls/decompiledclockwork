using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200063E RID: 1598
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberField : CodeTypeMember
	{
		// Token: 0x06003A15 RID: 14869 RVA: 0x000F3932 File Offset: 0x000F1B32
		public CodeMemberField()
		{
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000F393A File Offset: 0x000F1B3A
		public CodeMemberField(CodeTypeReference type, string name)
		{
			this.Type = type;
			base.Name = name;
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000F3950 File Offset: 0x000F1B50
		public CodeMemberField(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000F396B File Offset: 0x000F1B6B
		public CodeMemberField(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x000F3986 File Offset: 0x000F1B86
		// (set) Token: 0x06003A1A RID: 14874 RVA: 0x000F39A6 File Offset: 0x000F1BA6
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

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003A1B RID: 14875 RVA: 0x000F39AF File Offset: 0x000F1BAF
		// (set) Token: 0x06003A1C RID: 14876 RVA: 0x000F39B7 File Offset: 0x000F1BB7
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

		// Token: 0x04002BDE RID: 11230
		private CodeTypeReference type;

		// Token: 0x04002BDF RID: 11231
		private CodeExpression initExpression;
	}
}
