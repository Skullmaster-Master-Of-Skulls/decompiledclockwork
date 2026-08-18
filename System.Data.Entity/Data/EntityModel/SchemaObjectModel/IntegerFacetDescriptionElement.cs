using System;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F2 RID: 754
	internal sealed class IntegerFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x06002D1C RID: 11548 RVA: 0x000AB530 File Offset: 0x000A9730
		public IntegerFacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x000AB53A File Offset: 0x000A973A
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000AB548 File Offset: 0x000A9748
		protected override void HandleDefaultAttribute(XmlReader reader)
		{
			int num = -1;
			if (base.HandleIntAttribute(reader, ref num))
			{
				base.DefaultValue = num;
			}
		}
	}
}
