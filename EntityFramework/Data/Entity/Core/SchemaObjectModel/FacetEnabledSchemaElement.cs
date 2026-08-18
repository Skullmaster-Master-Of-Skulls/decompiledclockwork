using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000356 RID: 854
	internal abstract class FacetEnabledSchemaElement : SchemaElement
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0009273E File Offset: 0x0009093E
		internal new Function ParentElement
		{
			get
			{
				return base.ParentElement as Function;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0009274B File Offset: 0x0009094B
		internal SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00092753 File Offset: 0x00090953
		internal virtual TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x00092760 File Offset: 0x00090960
		internal TypeUsageBuilder TypeUsageBuilder
		{
			get
			{
				return this._typeUsageBuilder;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00092768 File Offset: 0x00090968
		internal bool HasUserDefinedFacets
		{
			get
			{
				return this._typeUsageBuilder.HasUserDefinedFacets;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x00092775 File Offset: 0x00090975
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x0009277D File Offset: 0x0009097D
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

		// Token: 0x06001E92 RID: 7826 RVA: 0x00092786 File Offset: 0x00090986
		internal FacetEnabledSchemaElement(Function parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00092790 File Offset: 0x00090990
		internal FacetEnabledSchemaElement(SchemaElement parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0009279C File Offset: 0x0009099C
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.Schema.ResolveTypeName(this, this.UnresolvedType, out this._type) && base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel && this._typeUsageBuilder.HasUserDefinedFacets)
			{
				bool flag = base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel;
				this._typeUsageBuilder.ValidateAndSetTypeUsage((ScalarType)this._type, !flag);
			}
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0009280D File Offset: 0x00090A0D
		internal void ValidateAndSetTypeUsage(ScalarType scalar)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(scalar, false);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0009281C File Offset: 0x00090A1C
		internal void ValidateAndSetTypeUsage(EdmType edmType)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0009282B File Offset: 0x00090A2B
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x04000A70 RID: 2672
		protected SchemaType _type;

		// Token: 0x04000A71 RID: 2673
		protected string _unresolvedType;

		// Token: 0x04000A72 RID: 2674
		protected TypeUsageBuilder _typeUsageBuilder;
	}
}
