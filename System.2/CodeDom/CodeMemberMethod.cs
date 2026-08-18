using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x0200063F RID: 1599
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberMethod : CodeTypeMember
	{
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06003A1D RID: 14877 RVA: 0x000F39C0 File Offset: 0x000F1BC0
		// (remove) Token: 0x06003A1E RID: 14878 RVA: 0x000F39F8 File Offset: 0x000F1BF8
		public event EventHandler PopulateParameters;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06003A1F RID: 14879 RVA: 0x000F3A30 File Offset: 0x000F1C30
		// (remove) Token: 0x06003A20 RID: 14880 RVA: 0x000F3A68 File Offset: 0x000F1C68
		public event EventHandler PopulateStatements;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06003A21 RID: 14881 RVA: 0x000F3AA0 File Offset: 0x000F1CA0
		// (remove) Token: 0x06003A22 RID: 14882 RVA: 0x000F3AD8 File Offset: 0x000F1CD8
		public event EventHandler PopulateImplementationTypes;

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000F3B0D File Offset: 0x000F1D0D
		// (set) Token: 0x06003A24 RID: 14884 RVA: 0x000F3B37 File Offset: 0x000F1D37
		public CodeTypeReference ReturnType
		{
			get
			{
				if (this.returnType == null)
				{
					this.returnType = new CodeTypeReference(typeof(void).FullName);
				}
				return this.returnType;
			}
			set
			{
				this.returnType = value;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000F3B40 File Offset: 0x000F1D40
		public CodeStatementCollection Statements
		{
			get
			{
				if ((this.populated & 2) == 0)
				{
					this.populated |= 2;
					if (this.PopulateStatements != null)
					{
						this.PopulateStatements(this, EventArgs.Empty);
					}
				}
				return this.statements;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000F3B79 File Offset: 0x000F1D79
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				if ((this.populated & 1) == 0)
				{
					this.populated |= 1;
					if (this.PopulateParameters != null)
					{
						this.PopulateParameters(this, EventArgs.Empty);
					}
				}
				return this.parameters;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000F3BB2 File Offset: 0x000F1DB2
		// (set) Token: 0x06003A28 RID: 14888 RVA: 0x000F3BBA File Offset: 0x000F1DBA
		public CodeTypeReference PrivateImplementationType
		{
			get
			{
				return this.privateImplements;
			}
			set
			{
				this.privateImplements = value;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000F3BC4 File Offset: 0x000F1DC4
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this.implementationTypes == null)
				{
					this.implementationTypes = new CodeTypeReferenceCollection();
				}
				if ((this.populated & 4) == 0)
				{
					this.populated |= 4;
					if (this.PopulateImplementationTypes != null)
					{
						this.PopulateImplementationTypes(this, EventArgs.Empty);
					}
				}
				return this.implementationTypes;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000F3C1B File Offset: 0x000F1E1B
		public CodeAttributeDeclarationCollection ReturnTypeCustomAttributes
		{
			get
			{
				if (this.returnAttributes == null)
				{
					this.returnAttributes = new CodeAttributeDeclarationCollection();
				}
				return this.returnAttributes;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000F3C36 File Offset: 0x000F1E36
		[ComVisible(false)]
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				if (this.typeParameters == null)
				{
					this.typeParameters = new CodeTypeParameterCollection();
				}
				return this.typeParameters;
			}
		}

		// Token: 0x04002BE0 RID: 11232
		private CodeParameterDeclarationExpressionCollection parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002BE1 RID: 11233
		private CodeStatementCollection statements = new CodeStatementCollection();

		// Token: 0x04002BE2 RID: 11234
		private CodeTypeReference returnType;

		// Token: 0x04002BE3 RID: 11235
		private CodeTypeReference privateImplements;

		// Token: 0x04002BE4 RID: 11236
		private CodeTypeReferenceCollection implementationTypes;

		// Token: 0x04002BE5 RID: 11237
		private CodeAttributeDeclarationCollection returnAttributes;

		// Token: 0x04002BE6 RID: 11238
		[OptionalField]
		private CodeTypeParameterCollection typeParameters;

		// Token: 0x04002BE7 RID: 11239
		private int populated;

		// Token: 0x04002BE8 RID: 11240
		private const int ParametersCollection = 1;

		// Token: 0x04002BE9 RID: 11241
		private const int StatementsCollection = 2;

		// Token: 0x04002BEA RID: 11242
		private const int ImplTypesCollection = 4;
	}
}
