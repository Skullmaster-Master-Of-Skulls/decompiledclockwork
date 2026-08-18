using System;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200031E RID: 798
	internal sealed class BooleanFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x06002F36 RID: 12086 RVA: 0x000AB530 File Offset: 0x000A9730
		public BooleanFacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x000B2B80 File Offset: 0x000B0D80
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean);
			}
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000B2B90 File Offset: 0x000B0D90
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
