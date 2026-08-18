using System;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F1 RID: 753
	internal sealed class ModelFunction : Function
	{
		// Token: 0x06002D11 RID: 11537 RVA: 0x000AB34B File Offset: 0x000A954B
		public ModelFunction(Schema parentElement) : base(parentElement)
		{
			this._isComposable = true;
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x000AB367 File Offset: 0x000A9567
		public override SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000AB36F File Offset: 0x000A956F
		internal TypeUsage TypeUsage
		{
			get
			{
				if (this._typeUsageBuilder.TypeUsage == null)
				{
					return null;
				}
				if (base.CollectionKind != CollectionKind.None)
				{
					return TypeUsage.Create(new CollectionType(this._typeUsageBuilder.TypeUsage));
				}
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000AB3A9 File Offset: 0x000A95A9
		internal void ValidateAndSetTypeUsage(ScalarType scalar)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(scalar, false);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000AB3B8 File Offset: 0x000A95B8
		internal void ValidateAndSetTypeUsage(EdmType edmType)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000AB3C7 File Offset: 0x000A95C7
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "DefiningExpression"))
			{
				this.HandleDefiningExpressionElment(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Parameter"))
			{
				base.HandleParameterElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000AB403 File Offset: 0x000A9603
		protected override void HandleReturnTypeAttribute(XmlReader reader)
		{
			base.HandleReturnTypeAttribute(reader);
			this._isComposable = true;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000AB413 File Offset: 0x000A9613
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000AB434 File Offset: 0x000A9634
		internal override void ResolveTopLevelNames()
		{
			if (base.UnresolvedReturnType != null && base.Schema.ResolveTypeName(this, base.UnresolvedReturnType, out this._type) && this._type is ScalarType)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(this._type as ScalarType, false);
			}
			foreach (Parameter parameter in base.Parameters)
			{
				parameter.ResolveTopLevelNames();
			}
			if (base.ReturnTypeList != null)
			{
				base.ReturnTypeList[0].ResolveTopLevelNames();
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000AB4E0 File Offset: 0x000A96E0
		private void HandleDefiningExpressionElment(XmlReader reader)
		{
			FunctionCommandText functionCommandText = new FunctionCommandText(this);
			functionCommandText.Parse(reader);
			this._commandText = functionCommandText;
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000AB502 File Offset: 0x000A9702
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
		}

		// Token: 0x040013CC RID: 5068
		private TypeUsageBuilder _typeUsageBuilder;
	}
}
