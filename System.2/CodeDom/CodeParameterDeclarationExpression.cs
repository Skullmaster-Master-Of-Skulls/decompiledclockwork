using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200064A RID: 1610
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeParameterDeclarationExpression : CodeExpression
	{
		// Token: 0x06003A8F RID: 14991 RVA: 0x000F45CD File Offset: 0x000F27CD
		public CodeParameterDeclarationExpression()
		{
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000F45D5 File Offset: 0x000F27D5
		public CodeParameterDeclarationExpression(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000F45EB File Offset: 0x000F27EB
		public CodeParameterDeclarationExpression(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000F4606 File Offset: 0x000F2806
		public CodeParameterDeclarationExpression(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003A93 RID: 14995 RVA: 0x000F4621 File Offset: 0x000F2821
		// (set) Token: 0x06003A94 RID: 14996 RVA: 0x000F463C File Offset: 0x000F283C
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

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x000F4645 File Offset: 0x000F2845
		// (set) Token: 0x06003A96 RID: 14998 RVA: 0x000F464D File Offset: 0x000F284D
		public FieldDirection Direction
		{
			get
			{
				return this.dir;
			}
			set
			{
				this.dir = value;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003A97 RID: 14999 RVA: 0x000F4656 File Offset: 0x000F2856
		// (set) Token: 0x06003A98 RID: 15000 RVA: 0x000F4676 File Offset: 0x000F2876
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

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x000F467F File Offset: 0x000F287F
		// (set) Token: 0x06003A9A RID: 15002 RVA: 0x000F4695 File Offset: 0x000F2895
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

		// Token: 0x04002C0F RID: 11279
		private CodeTypeReference type;

		// Token: 0x04002C10 RID: 11280
		private string name;

		// Token: 0x04002C11 RID: 11281
		private CodeAttributeDeclarationCollection customAttributes;

		// Token: 0x04002C12 RID: 11282
		private FieldDirection dir;
	}
}
