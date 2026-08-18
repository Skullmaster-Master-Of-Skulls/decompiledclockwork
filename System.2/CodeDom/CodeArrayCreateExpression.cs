using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000617 RID: 1559
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeArrayCreateExpression : CodeExpression
	{
		// Token: 0x06003905 RID: 14597 RVA: 0x000F2659 File Offset: 0x000F0859
		public CodeArrayCreateExpression()
		{
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000F266C File Offset: 0x000F086C
		public CodeArrayCreateExpression(CodeTypeReference createType, params CodeExpression[] initializers)
		{
			this.createType = createType;
			this.initializers.AddRange(initializers);
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000F2692 File Offset: 0x000F0892
		public CodeArrayCreateExpression(string createType, params CodeExpression[] initializers)
		{
			this.createType = new CodeTypeReference(createType);
			this.initializers.AddRange(initializers);
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000F26BD File Offset: 0x000F08BD
		public CodeArrayCreateExpression(Type createType, params CodeExpression[] initializers)
		{
			this.createType = new CodeTypeReference(createType);
			this.initializers.AddRange(initializers);
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000F26E8 File Offset: 0x000F08E8
		public CodeArrayCreateExpression(CodeTypeReference createType, int size)
		{
			this.createType = createType;
			this.size = size;
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000F2709 File Offset: 0x000F0909
		public CodeArrayCreateExpression(string createType, int size)
		{
			this.createType = new CodeTypeReference(createType);
			this.size = size;
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000F272F File Offset: 0x000F092F
		public CodeArrayCreateExpression(Type createType, int size)
		{
			this.createType = new CodeTypeReference(createType);
			this.size = size;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000F2755 File Offset: 0x000F0955
		public CodeArrayCreateExpression(CodeTypeReference createType, CodeExpression size)
		{
			this.createType = createType;
			this.sizeExpression = size;
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000F2776 File Offset: 0x000F0976
		public CodeArrayCreateExpression(string createType, CodeExpression size)
		{
			this.createType = new CodeTypeReference(createType);
			this.sizeExpression = size;
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000F279C File Offset: 0x000F099C
		public CodeArrayCreateExpression(Type createType, CodeExpression size)
		{
			this.createType = new CodeTypeReference(createType);
			this.sizeExpression = size;
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000F27C2 File Offset: 0x000F09C2
		// (set) Token: 0x06003910 RID: 14608 RVA: 0x000F27E2 File Offset: 0x000F09E2
		public CodeTypeReference CreateType
		{
			get
			{
				if (this.createType == null)
				{
					this.createType = new CodeTypeReference("");
				}
				return this.createType;
			}
			set
			{
				this.createType = value;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x000F27EB File Offset: 0x000F09EB
		public CodeExpressionCollection Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x000F27F3 File Offset: 0x000F09F3
		// (set) Token: 0x06003913 RID: 14611 RVA: 0x000F27FB File Offset: 0x000F09FB
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x000F2804 File Offset: 0x000F0A04
		// (set) Token: 0x06003915 RID: 14613 RVA: 0x000F280C File Offset: 0x000F0A0C
		public CodeExpression SizeExpression
		{
			get
			{
				return this.sizeExpression;
			}
			set
			{
				this.sizeExpression = value;
			}
		}

		// Token: 0x04002B8A RID: 11146
		private CodeTypeReference createType;

		// Token: 0x04002B8B RID: 11147
		private CodeExpressionCollection initializers = new CodeExpressionCollection();

		// Token: 0x04002B8C RID: 11148
		private CodeExpression sizeExpression;

		// Token: 0x04002B8D RID: 11149
		private int size;
	}
}
