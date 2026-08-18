using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x0200013C RID: 316
	public abstract class AnnotationCodeGenerator
	{
		// Token: 0x06000A8F RID: 2703 RVA: 0x00035FB2 File Offset: 0x000341B2
		public virtual IEnumerable<string> GetExtraNamespaces(IEnumerable<string> annotationNames)
		{
			Check.NotNull<IEnumerable<string>>(annotationNames, "annotationNames");
			return Enumerable.Empty<string>();
		}

		// Token: 0x06000A90 RID: 2704
		public abstract void Generate(string annotationName, object annotation, IndentedTextWriter writer);
	}
}
