using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000314 RID: 788
	internal abstract class SchemaType : SchemaElement
	{
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x000B075F File Offset: 0x000AE95F
		public string Namespace
		{
			get
			{
				return base.Schema.Namespace;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000B076C File Offset: 0x000AE96C
		public override string Identity
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000B076C File Offset: 0x000AE96C
		public override string FQName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000A9632 File Offset: 0x000A7832
		internal SchemaType(Schema parentElement) : base(parentElement)
		{
		}
	}
}
