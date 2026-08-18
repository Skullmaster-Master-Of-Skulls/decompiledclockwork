using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000387 RID: 903
	internal sealed class SchemaComplexType : StructuredType
	{
		// Token: 0x060020BC RID: 8380 RVA: 0x0009A5C9 File Offset: 0x000987C9
		internal SchemaComplexType(Schema parentElement) : base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0009A5F5 File Offset: 0x000987F5
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.BaseType != null && !(base.BaseType is SchemaComplexType))
			{
				base.AddError(ErrorCode.InvalidBaseType, EdmSchemaErrorSeverity.Error, Strings.InvalidBaseTypeForNestedType(base.BaseType.FQName, this.FQName));
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0009A631 File Offset: 0x00098831
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "ValueAnnotation"))
			{
				this.SkipElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeAnnotation"))
			{
				this.SkipElement(reader);
				return true;
			}
			return false;
		}
	}
}
