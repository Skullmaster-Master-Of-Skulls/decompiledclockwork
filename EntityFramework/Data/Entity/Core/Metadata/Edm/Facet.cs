using System;
using System.Data.Entity.Utilities;
using System.Diagnostics;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E2 RID: 1250
	[DebuggerDisplay("{Name,nq}={Value}")]
	public class Facet : MetadataItem
	{
		// Token: 0x06002E70 RID: 11888 RVA: 0x000DE953 File Offset: 0x000DCB53
		internal Facet()
		{
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000DE95B File Offset: 0x000DCB5B
		private Facet(FacetDescription facetDescription, object value) : base(MetadataItem.MetadataFlags.Readonly)
		{
			Check.NotNull<FacetDescription>(facetDescription, "facetDescription");
			this._facetDescription = facetDescription;
			this._value = value;
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000DE97E File Offset: 0x000DCB7E
		internal static Facet Create(FacetDescription facetDescription, object value)
		{
			return Facet.Create(facetDescription, value, false);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000DE988 File Offset: 0x000DCB88
		internal static Facet Create(FacetDescription facetDescription, object value, bool bypassKnownValues)
		{
			if (!bypassKnownValues)
			{
				if (object.ReferenceEquals(value, null))
				{
					return facetDescription.NullValueFacet;
				}
				if (object.Equals(facetDescription.DefaultValue, value))
				{
					return facetDescription.DefaultValueFacet;
				}
				if (facetDescription.FacetType.Identity == "Edm.Boolean")
				{
					bool value2 = (bool)value;
					return facetDescription.GetBooleanFacet(value2);
				}
			}
			Facet facet = new Facet(facetDescription, value);
			if (value != null && !Helper.IsUnboundedFacetValue(facet) && !Helper.IsVariableFacetValue(facet) && facet.FacetType.ClrType != null)
			{
				value.GetType();
			}
			return facet;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000DEA19 File Offset: 0x000DCC19
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.Facet;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000DEA1D File Offset: 0x000DCC1D
		public FacetDescription Description
		{
			get
			{
				return this._facetDescription;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000DEA25 File Offset: 0x000DCC25
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._facetDescription.FacetName;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000DEA32 File Offset: 0x000DCC32
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public EdmType FacetType
		{
			get
			{
				return this._facetDescription.FacetType;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000DEA3F File Offset: 0x000DCC3F
		[MetadataProperty(typeof(object), false)]
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x000DEA47 File Offset: 0x000DCC47
		internal override string Identity
		{
			get
			{
				return this._facetDescription.FacetName;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x000DEA54 File Offset: 0x000DCC54
		public bool IsUnbounded
		{
			get
			{
				return object.ReferenceEquals(this.Value, EdmConstants.UnboundedValue);
			}
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000DEA66 File Offset: 0x000DCC66
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040011AA RID: 4522
		private readonly FacetDescription _facetDescription;

		// Token: 0x040011AB RID: 4523
		private readonly object _value;
	}
}
