using System;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020006E8 RID: 1768
	internal static class DbContextExtensions
	{
		// Token: 0x06004705 RID: 18181 RVA: 0x001504E4 File Offset: 0x0014E6E4
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public static XDocument GetModel(this DbContext context)
		{
			return DbContextExtensions.GetModel(delegate(XmlWriter w)
			{
				EdmxWriter.WriteEdmx(context, w);
			});
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x00150510 File Offset: 0x0014E710
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public static XDocument GetModel(Action<XmlWriter> writeXml)
		{
			XDocument result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
				{
					Indent = true
				}))
				{
					writeXml(xmlWriter);
				}
				memoryStream.Position = 0L;
				result = XDocument.Load(memoryStream);
			}
			return result;
		}
	}
}
