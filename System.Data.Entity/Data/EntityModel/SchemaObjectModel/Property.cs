using System;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002FE RID: 766
	internal abstract class Property : SchemaElement
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x000A9632 File Offset: 0x000A7832
		internal Property(StructuredType parentElement) : base(parentElement)
		{
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002D72 RID: 11634
		public abstract SchemaType Type { get; }

		// Token: 0x06002D73 RID: 11635 RVA: 0x000AC2DC File Offset: 0x000AA4DC
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
					base.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					base.SkipElement(reader);
					return true;
				}
			}
			return false;
		}
	}
}
