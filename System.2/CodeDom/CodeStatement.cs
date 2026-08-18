using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x02000656 RID: 1622
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeStatement : CodeObject
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000F49E6 File Offset: 0x000F2BE6
		// (set) Token: 0x06003AD3 RID: 15059 RVA: 0x000F49EE File Offset: 0x000F2BEE
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

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000F49F7 File Offset: 0x000F2BF7
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

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000F4A12 File Offset: 0x000F2C12
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

		// Token: 0x04002C24 RID: 11300
		private CodeLinePragma linePragma;

		// Token: 0x04002C25 RID: 11301
		[OptionalField]
		private CodeDirectiveCollection startDirectives;

		// Token: 0x04002C26 RID: 11302
		[OptionalField]
		private CodeDirectiveCollection endDirectives;
	}
}
