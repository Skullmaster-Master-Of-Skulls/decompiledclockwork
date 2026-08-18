using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F9 RID: 761
	internal sealed class SchemaComplexType : StructuredType
	{
		// Token: 0x06002D4C RID: 11596 RVA: 0x000AB56E File Offset: 0x000A976E
		internal SchemaComplexType(Schema parentElement) : base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000ABA8D File Offset: 0x000A9C8D
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.BaseType != null && !(base.BaseType is SchemaComplexType))
			{
				base.AddError(ErrorCode.InvalidBaseType, EdmSchemaErrorSeverity.Error, Strings.InvalidBaseTypeForNestedType(base.BaseType.FQName, this.FQName));
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000ABAC9 File Offset: 0x000A9CC9
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "ValueAnnotation"))
			{
				base.SkipElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeAnnotation"))
			{
				base.SkipElement(reader);
				return true;
			}
			return false;
		}
	}
}
