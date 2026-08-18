using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E7 RID: 743
	internal sealed class EntityContainerEntitySetDefiningQuery : SchemaElement
	{
		// Token: 0x06002C98 RID: 11416 RVA: 0x000A9632 File Offset: 0x000A7832
		public EntityContainerEntitySetDefiningQuery(EntityContainerEntitySet parentElement) : base(parentElement)
		{
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000A994A File Offset: 0x000A7B4A
		public string Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000A9952 File Offset: 0x000A7B52
		protected override bool HandleText(XmlReader reader)
		{
			this._query = reader.Value;
			return true;
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000A9961 File Offset: 0x000A7B61
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this._query))
			{
				base.AddError(ErrorCode.EmptyDefiningQuery, EdmSchemaErrorSeverity.Error, Strings.EmptyDefiningQuery);
			}
		}

		// Token: 0x04001312 RID: 4882
		private string _query;
	}
}
