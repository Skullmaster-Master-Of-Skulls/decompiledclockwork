using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000095 RID: 149
	public class ObjectLiteralField : ConstantWrapper, INameDeclaration
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x000294AD File Offset: 0x000276AD
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x000294B5 File Offset: 0x000276B5
		public bool IsIdentifier { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x000294BE File Offset: 0x000276BE
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x000294C6 File Offset: 0x000276C6
		public Context ColonContext { get; set; }

		// Token: 0x0600090F RID: 2319 RVA: 0x000294CF File Offset: 0x000276CF
		public ObjectLiteralField(object value, PrimitiveType primitiveType, Context context) : base(value, primitiveType, context)
		{
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000294DA File Offset: 0x000276DA
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x000294E6 File Offset: 0x000276E6
		public string Name
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000294EE File Offset: 0x000276EE
		public AstNode Initializer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000294F1 File Offset: 0x000276F1
		public bool IsParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000294F4 File Offset: 0x000276F4
		public bool RenameNotAllowed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000294F7 File Offset: 0x000276F7
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x000294FF File Offset: 0x000276FF
		public JSVariableField VariableField { get; set; }
	}
}
