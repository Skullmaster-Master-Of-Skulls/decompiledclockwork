using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x02000644 RID: 1604
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeNamespace : CodeObject
	{
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06003A4C RID: 14924 RVA: 0x000F3EB4 File Offset: 0x000F20B4
		// (remove) Token: 0x06003A4D RID: 14925 RVA: 0x000F3EEC File Offset: 0x000F20EC
		public event EventHandler PopulateComments;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06003A4E RID: 14926 RVA: 0x000F3F24 File Offset: 0x000F2124
		// (remove) Token: 0x06003A4F RID: 14927 RVA: 0x000F3F5C File Offset: 0x000F215C
		public event EventHandler PopulateImports;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06003A50 RID: 14928 RVA: 0x000F3F94 File Offset: 0x000F2194
		// (remove) Token: 0x06003A51 RID: 14929 RVA: 0x000F3FCC File Offset: 0x000F21CC
		public event EventHandler PopulateTypes;

		// Token: 0x06003A52 RID: 14930 RVA: 0x000F4001 File Offset: 0x000F2201
		public CodeNamespace()
		{
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000F4035 File Offset: 0x000F2235
		public CodeNamespace(string name)
		{
			this.Name = name;
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000F4070 File Offset: 0x000F2270
		private CodeNamespace(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000F40A4 File Offset: 0x000F22A4
		public CodeTypeDeclarationCollection Types
		{
			get
			{
				if ((this.populated & 4) == 0)
				{
					this.populated |= 4;
					if (this.PopulateTypes != null)
					{
						this.PopulateTypes(this, EventArgs.Empty);
					}
				}
				return this.classes;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x000F40DD File Offset: 0x000F22DD
		public CodeNamespaceImportCollection Imports
		{
			get
			{
				if ((this.populated & 1) == 0)
				{
					this.populated |= 1;
					if (this.PopulateImports != null)
					{
						this.PopulateImports(this, EventArgs.Empty);
					}
				}
				return this.imports;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000F4116 File Offset: 0x000F2316
		// (set) Token: 0x06003A58 RID: 14936 RVA: 0x000F412C File Offset: 0x000F232C
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

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000F4135 File Offset: 0x000F2335
		public CodeCommentStatementCollection Comments
		{
			get
			{
				if ((this.populated & 2) == 0)
				{
					this.populated |= 2;
					if (this.PopulateComments != null)
					{
						this.PopulateComments(this, EventArgs.Empty);
					}
				}
				return this.comments;
			}
		}

		// Token: 0x04002BFC RID: 11260
		private string name;

		// Token: 0x04002BFD RID: 11261
		private CodeNamespaceImportCollection imports = new CodeNamespaceImportCollection();

		// Token: 0x04002BFE RID: 11262
		private CodeCommentStatementCollection comments = new CodeCommentStatementCollection();

		// Token: 0x04002BFF RID: 11263
		private CodeTypeDeclarationCollection classes = new CodeTypeDeclarationCollection();

		// Token: 0x04002C00 RID: 11264
		private CodeNamespaceCollection namespaces = new CodeNamespaceCollection();

		// Token: 0x04002C01 RID: 11265
		private int populated;

		// Token: 0x04002C02 RID: 11266
		private const int ImportsCollection = 1;

		// Token: 0x04002C03 RID: 11267
		private const int CommentsCollection = 2;

		// Token: 0x04002C04 RID: 11268
		private const int TypesCollection = 4;
	}
}
