using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000319 RID: 793
	internal class TypeElement : SchemaType
	{
		// Token: 0x06002EDD RID: 11997 RVA: 0x000B0FCC File Offset: 0x000AF1CC
		public TypeElement(Schema parent) : base(parent)
		{
			this._primitiveType.NamespaceName = base.Schema.Namespace;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000B1004 File Offset: 0x000AF204
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

		// Token: 0x06002EDF RID: 11999 RVA: 0x000B10D5 File Offset: 0x000AF2D5
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

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000B10FC File Offset: 0x000AF2FC
		private void HandlePrecisionElement(XmlReader reader)
		{
			ByteFacetDescriptionElement byteFacetDescriptionElement = new ByteFacetDescriptionElement(this, "Precision");
			byteFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(byteFacetDescriptionElement);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000B1128 File Offset: 0x000AF328
		private void HandleScaleElement(XmlReader reader)
		{
			ByteFacetDescriptionElement byteFacetDescriptionElement = new ByteFacetDescriptionElement(this, "Scale");
			byteFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(byteFacetDescriptionElement);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000B1154 File Offset: 0x000AF354
		private void HandleMaxLengthElement(XmlReader reader)
		{
			IntegerFacetDescriptionElement integerFacetDescriptionElement = new IntegerFacetDescriptionElement(this, "MaxLength");
			integerFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(integerFacetDescriptionElement);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000B1180 File Offset: 0x000AF380
		private void HandleUnicodeElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "Unicode");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000B11AC File Offset: 0x000AF3AC
		private void HandleFixedLengthElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "FixedLength");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000B11D8 File Offset: 0x000AF3D8
		private void HandleSridElement(XmlReader reader)
		{
			SridFacetDescriptionElement sridFacetDescriptionElement = new SridFacetDescriptionElement(this, "SRID");
			sridFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(sridFacetDescriptionElement);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000B1204 File Offset: 0x000AF404
		private void HandleIsStrictElement(XmlReader reader)
		{
			BooleanFacetDescriptionElement booleanFacetDescriptionElement = new BooleanFacetDescriptionElement(this, "IsStrict");
			booleanFacetDescriptionElement.Parse(reader);
			this._facetDescriptions.Add(booleanFacetDescriptionElement);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000B1230 File Offset: 0x000AF430
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

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x000B12A8 File Offset: 0x000AF4A8
		// (set) Token: 0x06002EE9 RID: 12009 RVA: 0x000B12B5 File Offset: 0x000AF4B5
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

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000B12C3 File Offset: 0x000AF4C3
		public PrimitiveType PrimitiveType
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x000B12CC File Offset: 0x000AF4CC
		public IEnumerable<FacetDescription> FacetDescriptions
		{
			get
			{
				foreach (FacetDescriptionElement facetDescriptionElement in this._facetDescriptions)
				{
					yield return facetDescriptionElement.FacetDescription;
				}
				List<FacetDescriptionElement>.Enumerator enumerator = default(List<FacetDescriptionElement>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000B12EC File Offset: 0x000AF4EC
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

		// Token: 0x06002EED RID: 12013 RVA: 0x000B136C File Offset: 0x000AF56C
		internal override void Validate()
		{
			base.Validate();
			if (!this.ValidateSufficientFacets())
			{
				return;
			}
			this.ValidateInterFacetConsistency();
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000B1384 File Offset: 0x000AF584
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

		// Token: 0x06002EEF RID: 12015 RVA: 0x000B1418 File Offset: 0x000AF618
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

		// Token: 0x04001444 RID: 5188
		private PrimitiveType _primitiveType = new PrimitiveType();

		// Token: 0x04001445 RID: 5189
		private List<FacetDescriptionElement> _facetDescriptions = new List<FacetDescriptionElement>();
	}
}
