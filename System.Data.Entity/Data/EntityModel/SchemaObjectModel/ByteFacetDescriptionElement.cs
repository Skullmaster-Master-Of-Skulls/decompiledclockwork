using System;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200031F RID: 799
	internal sealed class ByteFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x06002F39 RID: 12089 RVA: 0x000AB530 File Offset: 0x000A9730
		public ByteFacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000B2BB6 File Offset: 0x000B0DB6
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte);
			}
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000B2BC4 File Offset: 0x000B0DC4
		protected override void HandleDefaultAttribute(XmlReader reader)
		{
			byte b = 0;
			if (base.HandleByteAttribute(reader, ref b))
			{
				base.DefaultValue = b;
			}
		}
	}
}
