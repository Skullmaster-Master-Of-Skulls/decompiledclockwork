using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F7 RID: 759
	internal sealed class PropertyRefElement : SchemaElement
	{
		// Token: 0x06002D3C RID: 11580 RVA: 0x000A9632 File Offset: 0x000A7832
		public PropertyRefElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000AB7E9 File Offset: 0x000A99E9
		public StructuredProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void ResolveTopLevelNames()
		{
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000AB7F1 File Offset: 0x000A99F1
		internal bool ResolveNames(SchemaEntityType entityType)
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return true;
			}
			this._property = entityType.FindProperty(this.Name);
			return this._property != null;
		}

		// Token: 0x040013D1 RID: 5073
		private StructuredProperty _property;
	}
}
