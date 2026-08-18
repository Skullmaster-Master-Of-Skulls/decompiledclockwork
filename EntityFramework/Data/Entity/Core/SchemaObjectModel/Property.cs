using System;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000372 RID: 882
	internal abstract class Property : SchemaElement
	{
		// Token: 0x06001FA2 RID: 8098 RVA: 0x00096304 File Offset: 0x00094504
		internal Property(StructuredType parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001FA3 RID: 8099
		public abstract SchemaType Type { get; }

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00096310 File Offset: 0x00094510
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
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
			}
			return false;
		}
	}
}
