using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000353 RID: 851
	internal sealed class BooleanFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x06001E85 RID: 7813 RVA: 0x000926C0 File Offset: 0x000908C0
		public BooleanFacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x000926CA File Offset: 0x000908CA
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean);
			}
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000926D8 File Offset: 0x000908D8
		protected override void HandleDefaultAttribute(XmlReader reader)
		{
			bool flag = false;
			if (base.HandleBoolAttribute(reader, ref flag))
			{
				base.DefaultValue = flag;
			}
		}
	}
}
