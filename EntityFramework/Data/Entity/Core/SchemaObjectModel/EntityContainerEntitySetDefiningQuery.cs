using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000362 RID: 866
	internal sealed class EntityContainerEntitySetDefiningQuery : SchemaElement
	{
		// Token: 0x06001F0A RID: 7946 RVA: 0x0009447E File Offset: 0x0009267E
		public EntityContainerEntitySetDefiningQuery(EntityContainerEntitySet parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00094488 File Offset: 0x00092688
		public string Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00094490 File Offset: 0x00092690
		protected override bool HandleText(XmlReader reader)
		{
			this._query = reader.Value;
			return true;
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0009449F File Offset: 0x0009269F
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this._query))
			{
				base.AddError(ErrorCode.EmptyDefiningQuery, EdmSchemaErrorSeverity.Error, Strings.EmptyDefiningQuery);
			}
		}

		// Token: 0x04000A8E RID: 2702
		private string _query;
	}
}
