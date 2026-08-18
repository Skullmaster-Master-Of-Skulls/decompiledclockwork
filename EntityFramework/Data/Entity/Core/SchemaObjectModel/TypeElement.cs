using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000396 RID: 918
	internal class TypeElement : SchemaType
	{
		// Token: 0x0600211F RID: 8479 RVA: 0x0009B974 File Offset: 0x00099B74
		public TypeElement(Schema parent) : base(parent)
		{
			this._primitiveType.NamespaceName = base.Schema.Namespace;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0009B9AC File Offset: 0x00099BAC
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "FacetDescriptions"))
			{
				this.SkipThroughElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Precision"))
			{
				this.HandlePrecisionElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Scale"))
			{
				this.HandleScaleElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "MaxLength"))
			{
				this.HandleMaxLengthElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Unicode"))
			{
				this.HandleUnicodeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "FixedLength"))
			{
				this.HandleFixedLengthElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "SRID"))
			{
				this.HandleSridElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "IsStrict"))
			{
				this.HandleIsStrictElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0009BA7D File Offset: 0x00099C7D
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "PrimitiveTypeKind"))
			{
				this.HandlePrimitiveTypeKindAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0009BAA4 File Offset: 0x00099CA4
		private void HandlePrecisionElement(XmlReader reader)
		{
			ByteFacetDescriptionElement byteFacetDescriptionElement = new ByteFacetDescriptionElement(this, "Precision");
			byteFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(byteFacetDescriptionElement);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0009BAD0 File Offset: 0x00099CD0
		private void HandleScaleElement(XmlReader reader)
		{
			ByteFacetDescriptionElement byteFacetDescriptionElement = new ByteFacetDescriptionElement(this, "Scale");
			byteFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(byteFacetDescriptionElement);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0009BAFC File Offset: 0x00099CFC
		private void HandleMaxLengthElement(XmlReader reader)
		{
			IntegerFacetDescriptionElement integerFacetDescriptionElement = new IntegerFacetDescriptionElement(this, "MaxLength");
			integerFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(integerFacetDescriptionElement);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0009BB28 File Offset: 0x00099D28
		private void HandleUnicodeElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "Unicode");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0009BB54 File Offset: 0x00099D54
		private void HandleFixedLengthElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "FixedLength");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0009BB80 File Offset: 0x00099D80
		private void HandleSridElement(XmlReader reader)
		{
			SridFacetDescriptionElement sridFacetDescriptionElement = new SridFacetDescriptionElement(this, "SRID");
			sridFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(sridFacetDescriptionElement);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0009BBAC File Offset: 0x00099DAC
		private void HandleIsStrictElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "IsStrict");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0009BBD8 File Offset: 0x00099DD8
		private void HandlePrimitiveTypeKindAttribute(XmlReader reader)
		{
			string value = reader.Value;
			try
			{
				this._primitiveType.PrimitiveTypeKind = (PrimitiveTypeKind)Enum.Parse(typeof(PrimitiveTypeKind), value);
				this._primitiveType.BaseType = MetadataItem.EdmProviderManifest.GetPrimitiveType(this._primitiveType.PrimitiveTypeKind);
			}
			catch (ArgumentException)
			{
				base.AddError(ErrorCode.InvalidPrimitiveTypeKind, EdmSchemaErrorSeverity.Error, Strings.InvalidPrimitiveTypeKind(value));
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x0009BC50 File Offset: 0x00099E50
		// (set) Token: 0x0600212B RID: 8491 RVA: 0x0009BC5D File Offset: 0x00099E5D
		public override string Name
		{
			get
			{
				return this._primitiveType.Name;
			}
			set
			{
				this._primitiveType.Name = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x0009BC6B File Offset: 0x00099E6B
		public PrimitiveType PrimitiveType
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x0009BE00 File Offset: 0x0009A000
		public IEnumerable<FacetDescription> FacetDescriptions
		{
			get
			{
				foreach (FacetDescriptionElement element in this._facetDescriptions)
				{
					yield return element.FacetDescription;
				}
				yield break;
			}
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0009BE20 File Offset: 0x0009A020
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			foreach (FacetDescriptionElement facetDescriptionElement in this._facetDescriptions)
			{
				try
				{
					facetDescriptionElement.CreateAndValidateFacetDescription(this.Name);
				}
				catch (ArgumentException ex)
				{
					base.AddError(ErrorCode.InvalidFacetInProviderManifest, EdmSchemaErrorSeverity.Error, ex.Message);
				}
			}
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0009BEA0 File Offset: 0x0009A0A0
		internal override void Validate()
		{
			base.Validate();
			if (!this.ValidateSufficientFacets())
			{
				return;
			}
			this.ValidateInterFacetConsistency();
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0009BEB8 File Offset: 0x0009A0B8
		private bool ValidateInterFacetConsistency()
		{
			if (this.PrimitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Decimal)
			{
				FacetDescription facet = Helper.GetFacet(this.FacetDescriptions, "Precision");
				FacetDescription facet2 = Helper.GetFacet(this.FacetDescriptions, "Scale");
				if (facet.MaxValue.Value < facet2.MaxValue.Value)
				{
					base.AddError(ErrorCode.BadPrecisionAndScale, EdmSchemaErrorSeverity.Error, Strings.BadPrecisionAndScale(facet.MaxValue.Value, facet2.MaxValue.Value));
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0009BF4C File Offset: 0x0009A14C
		private bool ValidateSufficientFacets()
		{
			PrimitiveType primitiveType = this._primitiveType.BaseType as PrimitiveType;
			if (primitiveType == null)
			{
				return false;
			}
			bool flag = false;
			foreach (FacetDescription facetDescription in primitiveType.FacetDescriptions)
			{
				if (Helper.GetFacet(this.FacetDescriptions, facetDescription.FacetName) == null)
				{
					base.AddError(ErrorCode.RequiredFacetMissing, EdmSchemaErrorSeverity.Error, Strings.MissingFacetDescription(this.PrimitiveType.Name, this.PrimitiveType.PrimitiveTypeKind, facetDescription.FacetName));
					flag = true;
				}
			}
			return !flag;
		}

		// Token: 0x04000BBF RID: 3007
		private readonly PrimitiveType _primitiveType = new PrimitiveType();

		// Token: 0x04000BC0 RID: 3008
		private readonly List<FacetDescriptionElement> _facetDescriptions = new List<FacetDescriptionElement>();
	}
}
