using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x02000629 RID: 1577
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCompileUnit : CodeObject
	{
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x000F3153 File Offset: 0x000F1353
		public CodeNamespaceCollection Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000F315B File Offset: 0x000F135B
		public StringCollection ReferencedAssemblies
		{
			get
			{
				if (this.assemblies == null)
				{
					this.assemblies = new StringCollection();
				}
				return this.assemblies;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000F3176 File Offset: 0x000F1376
		public CodeAttributeDeclarationCollection AssemblyCustomAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new CodeAttributeDeclarationCollection();
				}
				return this.attributes;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000F3191 File Offset: 0x000F1391
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

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000F31AC File Offset: 0x000F13AC
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

		// Token: 0x04002BB9 RID: 11193
		private CodeNamespaceCollection namespaces = new CodeNamespaceCollection();

		// Token: 0x04002BBA RID: 11194
		private StringCollection assemblies;

		// Token: 0x04002BBB RID: 11195
		private CodeAttributeDeclarationCollection attributes;

		// Token: 0x04002BBC RID: 11196
		[OptionalField]
		private CodeDirectiveCollection startDirectives;

		// Token: 0x04002BBD RID: 11197
		[OptionalField]
		private CodeDirectiveCollection endDirectives;
	}
}
