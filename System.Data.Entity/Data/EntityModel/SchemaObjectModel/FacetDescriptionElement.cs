using System;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200031D RID: 797
	internal abstract class FacetDescriptionElement : SchemaElement
	{
		// Token: 0x06002F28 RID: 12072 RVA: 0x000B2A34 File Offset: 0x000B0C34
		public FacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000A9C93 File Offset: 0x000A7E93
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			if (base.ProhibitAttribute(namespaceUri, localName))
			{
				return true;
			}
			if (namespaceUri == null)
			{
				localName == "Name";
				return false;
			}
			return false;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000B2A40 File Offset: 0x000B0C40
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Minimum"))
			{
				this.HandleMinimumAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Maximum"))
			{
				this.HandleMaximumAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "DefaultValue"))
			{
				this.HandleDefaultAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Constant"))
			{
				this.HandleConstantAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000B2AB4 File Offset: 0x000B0CB4
		protected void HandleMinimumAttribute(XmlReader reader)
		{
			int value = -1;
			if (base.HandleIntAttribute(reader, ref value))
			{
				this._minValue = new int?(value);
			}
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000B2ADC File Offset: 0x000B0CDC
		protected void HandleMaximumAttribute(XmlReader reader)
		{
			int value = -1;
			if (base.HandleIntAttribute(reader, ref value))
			{
				this._maxValue = new int?(value);
			}
		}

		// Token: 0x06002F2D RID: 12077
		protected abstract void HandleDefaultAttribute(XmlReader reader);

		// Token: 0x06002F2E RID: 12078 RVA: 0x000B2B04 File Offset: 0x000B0D04
		protected void HandleConstantAttribute(XmlReader reader)
		{
			bool isConstant = false;
			if (base.HandleBoolAttribute(reader, ref isConstant))
			{
				this._isConstant = isConstant;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002F2F RID: 12079
		public abstract EdmType FacetType { get; }

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000B2B25 File Offset: 0x000B0D25
		public int? MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000B2B2D File Offset: 0x000B0D2D
		public int? MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000B2B35 File Offset: 0x000B0D35
		// (set) Token: 0x06002F33 RID: 12083 RVA: 0x000B2B3D File Offset: 0x000B0D3D
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._defaultValue = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x000B2B46 File Offset: 0x000B0D46
		public FacetDescription FacetDescription
		{
			get
			{
				return this._facetDescription;
			}
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000B2B4E File Offset: 0x000B0D4E
		internal void CreateAndValidateFacetDescription(string declaringTypeName)
		{
			this._facetDescription = new FacetDescription(this.Name, this.FacetType, this.MinValue, this.MaxValue, this.DefaultValue, this._isConstant, declaringTypeName);
		}

		// Token: 0x04001456 RID: 5206
		private int? _minValue;

		// Token: 0x04001457 RID: 5207
		private int? _maxValue;

		// Token: 0x04001458 RID: 5208
		private object _defaultValue;

		// Token: 0x04001459 RID: 5209
		private bool _isConstant;

		// Token: 0x0400145A RID: 5210
		private FacetDescription _facetDescription;
	}
}
