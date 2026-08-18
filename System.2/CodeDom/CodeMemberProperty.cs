using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000640 RID: 1600
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberProperty : CodeTypeMember
	{
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x000F3C6F File Offset: 0x000F1E6F
		// (set) Token: 0x06003A2E RID: 14894 RVA: 0x000F3C77 File Offset: 0x000F1E77
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

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003A2F RID: 14895 RVA: 0x000F3C80 File Offset: 0x000F1E80
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this.implementationTypes == null)
				{
					this.implementationTypes = new CodeTypeReferenceCollection();
				}
				return this.implementationTypes;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000F3C9B File Offset: 0x000F1E9B
		// (set) Token: 0x06003A31 RID: 14897 RVA: 0x000F3CBB File Offset: 0x000F1EBB
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

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x000F3CC4 File Offset: 0x000F1EC4
		// (set) Token: 0x06003A33 RID: 14899 RVA: 0x000F3CDE File Offset: 0x000F1EDE
		public bool HasGet
		{
			get
			{
				return this.hasGet || this.getStatements.Count > 0;
			}
			set
			{
				this.hasGet = value;
				if (!value)
				{
					this.getStatements.Clear();
				}
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x000F3CF5 File Offset: 0x000F1EF5
		// (set) Token: 0x06003A35 RID: 14901 RVA: 0x000F3D0F File Offset: 0x000F1F0F
		public bool HasSet
		{
			get
			{
				return this.hasSet || this.setStatements.Count > 0;
			}
			set
			{
				this.hasSet = value;
				if (!value)
				{
					this.setStatements.Clear();
				}
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000F3D26 File Offset: 0x000F1F26
		public CodeStatementCollection GetStatements
		{
			get
			{
				return this.getStatements;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000F3D2E File Offset: 0x000F1F2E
		public CodeStatementCollection SetStatements
		{
			get
			{
				return this.setStatements;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000F3D36 File Offset: 0x000F1F36
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002BEE RID: 11246
		private CodeTypeReference type;

		// Token: 0x04002BEF RID: 11247
		private CodeParameterDeclarationExpressionCollection parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002BF0 RID: 11248
		private bool hasGet;

		// Token: 0x04002BF1 RID: 11249
		private bool hasSet;

		// Token: 0x04002BF2 RID: 11250
		private CodeStatementCollection getStatements = new CodeStatementCollection();

		// Token: 0x04002BF3 RID: 11251
		private CodeStatementCollection setStatements = new CodeStatementCollection();

		// Token: 0x04002BF4 RID: 11252
		private CodeTypeReference privateImplements;

		// Token: 0x04002BF5 RID: 11253
		private CodeTypeReferenceCollection implementationTypes;
	}
}
