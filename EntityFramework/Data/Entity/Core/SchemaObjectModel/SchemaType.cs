using System;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200035B RID: 859
	internal abstract class SchemaType : SchemaElement
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x00092C90 File Offset: 0x00090E90
		public string Namespace
		{
			get
			{
				return base.Schema.Namespace;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x00092C9D File Offset: 0x00090E9D
		public override string Identity
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x00092CB5 File Offset: 0x00090EB5
		public override string FQName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00092CCD File Offset: 0x00090ECD
		internal SchemaType(Schema parentElement) : base(parentElement, null)
		{
		}
	}
}
