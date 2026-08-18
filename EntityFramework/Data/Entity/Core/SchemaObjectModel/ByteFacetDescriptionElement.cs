using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000354 RID: 852
	internal sealed class ByteFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x06001E88 RID: 7816 RVA: 0x000926FE File Offset: 0x000908FE
		public ByteFacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00092708 File Offset: 0x00090908
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte);
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00092718 File Offset: 0x00090918
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
