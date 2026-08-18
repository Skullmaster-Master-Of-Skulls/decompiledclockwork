using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x0200061D RID: 1565
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAttributeDeclaration
	{
		// Token: 0x0600393C RID: 14652 RVA: 0x000F2A84 File Offset: 0x000F0C84
		public CodeAttributeDeclaration()
		{
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000F2A97 File Offset: 0x000F0C97
		public CodeAttributeDeclaration(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000F2AB1 File Offset: 0x000F0CB1
		public CodeAttributeDeclaration(string name, params CodeAttributeArgument[] arguments)
		{
			this.Name = name;
			this.Arguments.AddRange(arguments);
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000F2AD7 File Offset: 0x000F0CD7
		public CodeAttributeDeclaration(CodeTypeReference attributeType) : this(attributeType, null)
		{
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000F2AE1 File Offset: 0x000F0CE1
		public CodeAttributeDeclaration(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
		{
			this.attributeType = attributeType;
			if (attributeType != null)
			{
				this.name = attributeType.BaseType;
			}
			if (arguments != null)
			{
				this.Arguments.AddRange(arguments);
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000F2B19 File Offset: 0x000F0D19
		// (set) Token: 0x06003942 RID: 14658 RVA: 0x000F2B2F File Offset: 0x000F0D2F
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
				this.attributeType = new CodeTypeReference(this.name);
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06003943 RID: 14659 RVA: 0x000F2B49 File Offset: 0x000F0D49
		public CodeAttributeArgumentCollection Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000F2B51 File Offset: 0x000F0D51
		public CodeTypeReference AttributeType
		{
			get
			{
				return this.attributeType;
			}
		}

		// Token: 0x04002B96 RID: 11158
		private string name;

		// Token: 0x04002B97 RID: 11159
		private CodeAttributeArgumentCollection arguments = new CodeAttributeArgumentCollection();

		// Token: 0x04002B98 RID: 11160
		[OptionalField]
		private CodeTypeReference attributeType;
	}
}
