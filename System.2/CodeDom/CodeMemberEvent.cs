using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200063D RID: 1597
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberEvent : CodeTypeMember
	{
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003A10 RID: 14864 RVA: 0x000F38DD File Offset: 0x000F1ADD
		// (set) Token: 0x06003A11 RID: 14865 RVA: 0x000F38FD File Offset: 0x000F1AFD
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

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x000F3906 File Offset: 0x000F1B06
		// (set) Token: 0x06003A13 RID: 14867 RVA: 0x000F390E File Offset: 0x000F1B0E
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

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000F3917 File Offset: 0x000F1B17
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

		// Token: 0x04002BDB RID: 11227
		private CodeTypeReference type;

		// Token: 0x04002BDC RID: 11228
		private CodeTypeReference privateImplements;

		// Token: 0x04002BDD RID: 11229
		private CodeTypeReferenceCollection implementationTypes;
	}
}
