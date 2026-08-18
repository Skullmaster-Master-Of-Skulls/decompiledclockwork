using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C4 RID: 452
	public class IndexAttributeConvention : AttributeToColumnAnnotationConvention<IndexAttribute, IndexAnnotation>
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00040EC0 File Offset: 0x0003F0C0
		public IndexAttributeConvention() : base("Index", (PropertyInfo p, IList<IndexAttribute> a) => new IndexAnnotation(p, from i in a
		orderby i.ToString()
		select i))
		{
		}
	}
}
