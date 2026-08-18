using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000371 RID: 881
	internal sealed class ModelFunction : Function
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x00096120 File Offset: 0x00094320
		public ModelFunction(Schema parentElement) : base(parentElement)
		{
			this._isComposable = true;
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0009613C File Offset: 0x0009433C
		public override SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x00096144 File Offset: 0x00094344
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

		// Token: 0x06001F9A RID: 8090 RVA: 0x0009617E File Offset: 0x0009437E
		internal void ValidateAndSetTypeUsage(ScalarType scalar)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(scalar, false);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0009618D File Offset: 0x0009438D
		internal void ValidateAndSetTypeUsage(EdmType edmType)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0009619C File Offset: 0x0009439C
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

		// Token: 0x06001F9D RID: 8093 RVA: 0x000961D8 File Offset: 0x000943D8
		protected override void HandleReturnTypeAttribute(XmlReader reader)
		{
			base.HandleReturnTypeAttribute(reader);
			this._isComposable = true;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000961E8 File Offset: 0x000943E8
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00096208 File Offset: 0x00094408
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

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000962B4 File Offset: 0x000944B4
		private void HandleDefiningExpressionElment(XmlReader reader)
		{
			FunctionCommandText functionCommandText = new FunctionCommandText(this);
			functionCommandText.Parse(reader);
			this._commandText = functionCommandText;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000962D6 File Offset: 0x000944D6
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
		}

		// Token: 0x04000B4E RID: 2894
		private readonly TypeUsageBuilder _typeUsageBuilder;
	}
}
