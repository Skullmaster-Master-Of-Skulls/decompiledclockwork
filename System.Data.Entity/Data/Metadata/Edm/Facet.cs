using System;
using System.Diagnostics;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D6 RID: 470
	[DebuggerDisplay("{Name,nq}={Value}")]
	public sealed class Facet : MetadataItem
	{
		// Token: 0x06001FD3 RID: 8147 RVA: 0x0006F518 File Offset: 0x0006D718
		private Facet(FacetDescription facetDescription, object value) : base(MetadataItem.MetadataFlags.Readonly)
		{
			EntityUtil.GenericCheckArgumentNull<FacetDescription>(facetDescription, "facetDescription");
			this._facetDescription = facetDescription;
			this._value = value;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0006F53B File Offset: 0x0006D73B
		internal static Facet Create(FacetDescription facetDescription, object value)
		{
			return Facet.Create(facetDescription, value, false);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0006F548 File Offset: 0x0006D748
		internal static Facet Create(FacetDescription facetDescription, object value, bool bypassKnownValues)
		{
			EntityUtil.CheckArgumentNull<FacetDescription>(facetDescription, "facetDescription");
			if (!bypassKnownValues)
			{
				if (value == null)
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
				Type type = value.GetType();
			}
			return facet;
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x0006F5DF File Offset: 0x0006D7DF
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.Facet;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0006F5E3 File Offset: 0x0006D7E3
		public FacetDescription Description
		{
			get
			{
				return this._facetDescription;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x0006F5EB File Offset: 0x0006D7EB
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._facetDescription.FacetName;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0006F5F8 File Offset: 0x0006D7F8
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public EdmType FacetType
		{
			get
			{
				return this._facetDescription.FacetType;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x0006F605 File Offset: 0x0006D805
		[MetadataProperty(typeof(object), false)]
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0006F5EB File Offset: 0x0006D7EB
		internal override string Identity
		{
			get
			{
				return this._facetDescription.FacetName;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x0006F60D File Offset: 0x0006D80D
		public bool IsUnbounded
		{
			get
			{
				return this.Value == EdmConstants.UnboundedValue;
			}
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0006F61C File Offset: 0x0006D81C
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000E10 RID: 3600
		private readonly FacetDescription _facetDescription;

		// Token: 0x04000E11 RID: 3601
		private readonly object _value;
	}
}
