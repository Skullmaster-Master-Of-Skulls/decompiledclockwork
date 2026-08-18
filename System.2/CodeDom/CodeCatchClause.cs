using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000623 RID: 1571
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCatchClause
	{
		// Token: 0x06003963 RID: 14691 RVA: 0x000F2D56 File Offset: 0x000F0F56
		public CodeCatchClause()
		{
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000F2D5E File Offset: 0x000F0F5E
		public CodeCatchClause(string localName)
		{
			this.localName = localName;
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000F2D6D File Offset: 0x000F0F6D
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType)
		{
			this.localName = localName;
			this.catchExceptionType = catchExceptionType;
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000F2D83 File Offset: 0x000F0F83
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType, params CodeStatement[] statements)
		{
			this.localName = localName;
			this.catchExceptionType = catchExceptionType;
			this.Statements.AddRange(statements);
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x000F2DA5 File Offset: 0x000F0FA5
		// (set) Token: 0x06003968 RID: 14696 RVA: 0x000F2DBB File Offset: 0x000F0FBB
		public string LocalName
		{
			get
			{
				if (this.localName != null)
				{
					return this.localName;
				}
				return string.Empty;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x000F2DC4 File Offset: 0x000F0FC4
		// (set) Token: 0x0600396A RID: 14698 RVA: 0x000F2DE9 File Offset: 0x000F0FE9
		public CodeTypeReference CatchExceptionType
		{
			get
			{
				if (this.catchExceptionType == null)
				{
					this.catchExceptionType = new CodeTypeReference(typeof(Exception));
				}
				return this.catchExceptionType;
			}
			set
			{
				this.catchExceptionType = value;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600396B RID: 14699 RVA: 0x000F2DF2 File Offset: 0x000F0FF2
		public CodeStatementCollection Statements
		{
			get
			{
				if (this.statements == null)
				{
					this.statements = new CodeStatementCollection();
				}
				return this.statements;
			}
		}

		// Token: 0x04002BB0 RID: 11184
		private CodeStatementCollection statements;

		// Token: 0x04002BB1 RID: 11185
		private CodeTypeReference catchExceptionType;

		// Token: 0x04002BB2 RID: 11186
		private string localName;
	}
}
