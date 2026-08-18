using System;
using System.Data.Entity.Infrastructure;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020002D0 RID: 720
	internal static class DbModelExtensions
	{
		// Token: 0x06001959 RID: 6489 RVA: 0x0007E7A0 File Offset: 0x0007C9A0
		public static XDocument GetModel(this DbModel model)
		{
			return DbContextExtensions.GetModel(delegate(XmlWriter w)
			{
				EdmxWriter.WriteEdmx(model, w);
			});
		}
	}
}
