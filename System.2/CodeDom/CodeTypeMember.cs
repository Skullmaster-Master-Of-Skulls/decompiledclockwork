using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x0200065F RID: 1631
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeMember : CodeObject
	{
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003B18 RID: 15128 RVA: 0x000F5118 File Offset: 0x000F3318
		// (set) Token: 0x06003B19 RID: 15129 RVA: 0x000F512E File Offset: 0x000F332E
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

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003B1A RID: 15130 RVA: 0x000F5137 File Offset: 0x000F3337
		// (set) Token: 0x06003B1B RID: 15131 RVA: 0x000F513F File Offset: 0x000F333F
		public MemberAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003B1C RID: 15132 RVA: 0x000F5148 File Offset: 0x000F3348
		// (set) Token: 0x06003B1D RID: 15133 RVA: 0x000F5163 File Offset: 0x000F3363
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.customAttributes = new CodeAttributeDeclarationCollection();
				}
				return this.customAttributes;
			}
			set
			{
				this.customAttributes = value;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000F516C File Offset: 0x000F336C
		// (set) Token: 0x06003B1F RID: 15135 RVA: 0x000F5174 File Offset: 0x000F3374
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
			set
			{
				this.linePragma = value;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x000F517D File Offset: 0x000F337D
		public CodeCommentStatementCollection Comments
		{
			get
			{
				return this.comments;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000F5185 File Offset: 0x000F3385
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				if (this.startDirectives == null)
				{
					this.startDirectives = new CodeDirectiveCollection();
				}
				return this.startDirectives;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000F51A0 File Offset: 0x000F33A0
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				if (this.endDirectives == null)
				{
					this.endDirectives = new CodeDirectiveCollection();
				}
				return this.endDirectives;
			}
		}

		// Token: 0x04002C39 RID: 11321
		private MemberAttributes attributes = (MemberAttributes)20482;

		// Token: 0x04002C3A RID: 11322
		private string name;

		// Token: 0x04002C3B RID: 11323
		private CodeCommentStatementCollection comments = new CodeCommentStatementCollection();

		// Token: 0x04002C3C RID: 11324
		private CodeAttributeDeclarationCollection customAttributes;

		// Token: 0x04002C3D RID: 11325
		private CodeLinePragma linePragma;

		// Token: 0x04002C3E RID: 11326
		[OptionalField]
		private CodeDirectiveCollection startDirectives;

		// Token: 0x04002C3F RID: 11327
		[OptionalField]
		private CodeDirectiveCollection endDirectives;
	}
}
