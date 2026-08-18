using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x0200065C RID: 1628
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeDeclaration : CodeTypeMember
	{
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06003AF1 RID: 15089 RVA: 0x000F4C80 File Offset: 0x000F2E80
		// (remove) Token: 0x06003AF2 RID: 15090 RVA: 0x000F4CB8 File Offset: 0x000F2EB8
		public event EventHandler PopulateBaseTypes;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06003AF3 RID: 15091 RVA: 0x000F4CF0 File Offset: 0x000F2EF0
		// (remove) Token: 0x06003AF4 RID: 15092 RVA: 0x000F4D28 File Offset: 0x000F2F28
		public event EventHandler PopulateMembers;

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000F4D5D File Offset: 0x000F2F5D
		public CodeTypeDeclaration()
		{
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000F4D82 File Offset: 0x000F2F82
		public CodeTypeDeclaration(string name)
		{
			base.Name = name;
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000F4DAE File Offset: 0x000F2FAE
		// (set) Token: 0x06003AF8 RID: 15096 RVA: 0x000F4DB6 File Offset: 0x000F2FB6
		public TypeAttributes TypeAttributes
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

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000F4DBF File Offset: 0x000F2FBF
		public CodeTypeReferenceCollection BaseTypes
		{
			get
			{
				if ((this.populated & 1) == 0)
				{
					this.populated |= 1;
					if (this.PopulateBaseTypes != null)
					{
						this.PopulateBaseTypes(this, EventArgs.Empty);
					}
				}
				return this.baseTypes;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x000F4DF8 File Offset: 0x000F2FF8
		// (set) Token: 0x06003AFB RID: 15099 RVA: 0x000F4E18 File Offset: 0x000F3018
		public bool IsClass
		{
			get
			{
				return (this.attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.isEnum && !this.isStruct;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.attributes |= TypeAttributes.NotPublic;
					this.isStruct = false;
					this.isEnum = false;
				}
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000F4E48 File Offset: 0x000F3048
		// (set) Token: 0x06003AFD RID: 15101 RVA: 0x000F4E50 File Offset: 0x000F3050
		public bool IsStruct
		{
			get
			{
				return this.isStruct;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.isStruct = true;
					this.isEnum = false;
					return;
				}
				this.isStruct = false;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x000F4E7A File Offset: 0x000F307A
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x000F4E82 File Offset: 0x000F3082
		public bool IsEnum
		{
			get
			{
				return this.isEnum;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.isStruct = false;
					this.isEnum = true;
					return;
				}
				this.isEnum = false;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000F4EAC File Offset: 0x000F30AC
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x000F4EBC File Offset: 0x000F30BC
		public bool IsInterface
		{
			get
			{
				return (this.attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.attributes |= TypeAttributes.ClassSemanticsMask;
					this.isStruct = false;
					this.isEnum = false;
					return;
				}
				this.attributes &= ~TypeAttributes.ClassSemanticsMask;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000F4F08 File Offset: 0x000F3108
		// (set) Token: 0x06003B03 RID: 15107 RVA: 0x000F4F10 File Offset: 0x000F3110
		public bool IsPartial
		{
			get
			{
				return this.isPartial;
			}
			set
			{
				this.isPartial = value;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x000F4F19 File Offset: 0x000F3119
		public CodeTypeMemberCollection Members
		{
			get
			{
				if ((this.populated & 2) == 0)
				{
					this.populated |= 2;
					if (this.PopulateMembers != null)
					{
						this.PopulateMembers(this, EventArgs.Empty);
					}
				}
				return this.members;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000F4F52 File Offset: 0x000F3152
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

		// Token: 0x04002C2B RID: 11307
		private TypeAttributes attributes = TypeAttributes.Public;

		// Token: 0x04002C2C RID: 11308
		private CodeTypeReferenceCollection baseTypes = new CodeTypeReferenceCollection();

		// Token: 0x04002C2D RID: 11309
		private CodeTypeMemberCollection members = new CodeTypeMemberCollection();

		// Token: 0x04002C2E RID: 11310
		private bool isEnum;

		// Token: 0x04002C2F RID: 11311
		private bool isStruct;

		// Token: 0x04002C30 RID: 11312
		private int populated;

		// Token: 0x04002C31 RID: 11313
		private const int BaseTypesCollection = 1;

		// Token: 0x04002C32 RID: 11314
		private const int MembersCollection = 2;

		// Token: 0x04002C33 RID: 11315
		[OptionalField]
		private CodeTypeParameterCollection typeParameters;

		// Token: 0x04002C34 RID: 11316
		[OptionalField]
		private bool isPartial;
	}
}
