using System;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000315 RID: 789
	internal sealed class SridFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x06002EAE RID: 11950 RVA: 0x000AB530 File Offset: 0x000A9730
		public SridFacetDescriptionElement(TypeElement type, string name) : base(type, name)
		{
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000AB53A File Offset: 0x000A973A
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000B0784 File Offset: 0x000AE984
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
