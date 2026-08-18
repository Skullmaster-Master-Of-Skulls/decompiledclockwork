using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000662 RID: 1634
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeParameter : CodeObject
	{
		// Token: 0x06003B37 RID: 15159 RVA: 0x000F5350 File Offset: 0x000F3550
		public CodeTypeParameter()
		{
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000F5358 File Offset: 0x000F3558
		public CodeTypeParameter(string name)
		{
			this.name = name;
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x000F5367 File Offset: 0x000F3567
		// (set) Token: 0x06003B3A RID: 15162 RVA: 0x000F537D File Offset: 0x000F357D
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

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003B3B RID: 15163 RVA: 0x000F5386 File Offset: 0x000F3586
		public CodeTypeReferenceCollection Constraints
		{
			get
			{
				if (this.constraints == null)
				{
					this.constraints = new CodeTypeReferenceCollection();
				}
				return this.constraints;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x000F53A1 File Offset: 0x000F35A1
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
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x000F53BC File Offset: 0x000F35BC
		// (set) Token: 0x06003B3E RID: 15166 RVA: 0x000F53C4 File Offset: 0x000F35C4
		public bool HasConstructorConstraint
		{
			get
			{
				return this.hasConstructorConstraint;
			}
			set
			{
				this.hasConstructorConstraint = value;
			}
		}

		// Token: 0x04002C41 RID: 11329
		private string name;

		// Token: 0x04002C42 RID: 11330
		private CodeAttributeDeclarationCollection customAttributes;

		// Token: 0x04002C43 RID: 11331
		private CodeTypeReferenceCollection constraints;

		// Token: 0x04002C44 RID: 11332
		private bool hasConstructorConstraint;
	}
}
