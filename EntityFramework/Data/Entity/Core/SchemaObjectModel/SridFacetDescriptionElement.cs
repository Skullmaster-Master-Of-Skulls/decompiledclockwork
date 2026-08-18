using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000392 RID: 914
	internal sealed class SridFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x06002107 RID: 8455 RVA: 0x0009B5DD File Offset: 0x000997DD
		public SridFacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x0009B5E7 File Offset: 0x000997E7
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0009B5F8 File Offset: 0x000997F8
		protected override void HandleDefaultAttribute(XmlReader reader)
		{
			string value = reader.Value;
			if (value.Trim() == "Variable")
			{
				base.DefaultValue = EdmConstants.VariableValue;
				return;
			}
			int num = -1;
			if (base.HandleIntAttribute(reader, ref num))
			{
				base.DefaultValue = num;
			}
		}
	}
}
