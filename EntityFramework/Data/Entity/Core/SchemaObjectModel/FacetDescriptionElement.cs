using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000352 RID: 850
	internal abstract class FacetDescriptionElement : SchemaElement
	{
		// Token: 0x06001E77 RID: 7799 RVA: 0x00092554 File Offset: 0x00090754
		public FacetDescriptionElement(TypeElement type, string name) : base(type, name, null)
		{
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0009255F File Offset: 0x0009075F
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return base.ProhibitAttribute(namespaceUri, localName) || (namespaceUri == null && localName == "Name" && false);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00092580 File Offset: 0x00090780
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

		// Token: 0x06001E7A RID: 7802 RVA: 0x000925F4 File Offset: 0x000907F4
		protected void HandleMinimumAttribute(XmlReader reader)
		{
			int value = -1;
			if (base.HandleIntAttribute(reader, ref value))
			{
				this._minValue = new int?(value);
			}
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0009261C File Offset: 0x0009081C
		protected void HandleMaximumAttribute(XmlReader reader)
		{
			int value = -1;
			if (base.HandleIntAttribute(reader, ref value))
			{
				this._maxValue = new int?(value);
			}
		}

		// Token: 0x06001E7C RID: 7804
		protected abstract void HandleDefaultAttribute(XmlReader reader);

		// Token: 0x06001E7D RID: 7805 RVA: 0x00092644 File Offset: 0x00090844
		protected void HandleConstantAttribute(XmlReader reader)
		{
			bool isConstant = false;
			if (base.HandleBoolAttribute(reader, ref isConstant))
			{
				this._isConstant = isConstant;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001E7E RID: 7806
		public abstract EdmType FacetType { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00092665 File Offset: 0x00090865
		public int? MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0009266D File Offset: 0x0009086D
		public int? MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00092675 File Offset: 0x00090875
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x0009267D File Offset: 0x0009087D
		public object DefaultValue { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x00092686 File Offset: 0x00090886
		public FacetDescription FacetDescription
		{
			get
			{
				return this._facetDescription;
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0009268E File Offset: 0x0009088E
		internal void CreateAndValidateFacetDescription(string declaringTypeName)
		{
			this._facetDescription = new FacetDescription(this.Name, this.FacetType, this.MinValue, this.MaxValue, this.DefaultValue, this._isConstant, declaringTypeName);
		}

		// Token: 0x04000A67 RID: 2663
		private int? _minValue;

		// Token: 0x04000A68 RID: 2664
		private int? _maxValue;

		// Token: 0x04000A69 RID: 2665
		private bool _isConstant;

		// Token: 0x04000A6A RID: 2666
		private FacetDescription _facetDescription;
	}
}
