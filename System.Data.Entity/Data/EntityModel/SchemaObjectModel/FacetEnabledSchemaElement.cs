using System;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002EC RID: 748
	internal abstract class FacetEnabledSchemaElement : SchemaElement
	{
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000AA104 File Offset: 0x000A8304
		internal new Function ParentElement
		{
			get
			{
				return base.ParentElement as Function;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x000AA111 File Offset: 0x000A8311
		internal SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000AA119 File Offset: 0x000A8319
		internal virtual TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000AA126 File Offset: 0x000A8326
		internal TypeUsageBuilder TypeUsageBuilder
		{
			get
			{
				return this._typeUsageBuilder;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000AA12E File Offset: 0x000A832E
		internal bool HasUserDefinedFacets
		{
			get
			{
				return this._typeUsageBuilder.HasUserDefinedFacets;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000AA13B File Offset: 0x000A833B
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x000AA143 File Offset: 0x000A8343
		internal string UnresolvedType
		{
			get
			{
				return this._unresolvedType;
			}
			set
			{
				this._unresolvedType = value;
			}
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000A9632 File Offset: 0x000A7832
		internal FacetEnabledSchemaElement(Function parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000A9632 File Offset: 0x000A7832
		internal FacetEnabledSchemaElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000AA14C File Offset: 0x000A834C
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.Schema.ResolveTypeName(this, this.UnresolvedType, out this._type) && base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel && this._typeUsageBuilder.HasUserDefinedFacets)
			{
				bool flag = base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel;
				this._typeUsageBuilder.ValidateAndSetTypeUsage((ScalarType)this._type, !flag);
			}
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000AA1BD File Offset: 0x000A83BD
		internal void ValidateAndSetTypeUsage(ScalarType scalar)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(scalar, false);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000AA1CC File Offset: 0x000A83CC
		internal void ValidateAndSetTypeUsage(EdmType edmType)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000AA1DB File Offset: 0x000A83DB
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x040013B2 RID: 5042
		protected SchemaType _type;

		// Token: 0x040013B3 RID: 5043
		protected string _unresolvedType;

		// Token: 0x040013B4 RID: 5044
		protected TypeUsageBuilder _typeUsageBuilder;
	}
}
