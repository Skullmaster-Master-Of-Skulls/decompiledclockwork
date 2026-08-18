using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200036A RID: 874
	internal sealed class IntegerFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x06001F62 RID: 8034 RVA: 0x0009595D File Offset: 0x00093B5D
		public IntegerFacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x00095967 File Offset: 0x00093B67
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00095978 File Offset: 0x00093B78
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
