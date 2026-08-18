using System;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D91 RID: 3473
	internal class XmlaProperties
	{
		// Token: 0x0600811C RID: 33052 RVA: 0x001D7C73 File Offset: 0x001D5E73
		public static IXmlaMethodProperty AxisFormat(XmlaAxisFormats format)
		{
			return XmlaProperties.CreateXmlaProperty("AxisFormat", format.ToString());
		}

		// Token: 0x0600811D RID: 33053 RVA: 0x001D7C8A File Offset: 0x001D5E8A
		public static IXmlaMethodProperty Content(XmlaContentTypes contentType)
		{
			return XmlaProperties.CreateXmlaProperty("Content", contentType.ToString());
		}

		// Token: 0x0600811E RID: 33054 RVA: 0x001D7CA1 File Offset: 0x001D5EA1
		public static IXmlaMethodProperty Format(XmlaFormatTypes formatType)
		{
			return XmlaProperties.CreateXmlaProperty("Format", formatType.ToString());
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x001D7CB8 File Offset: 0x001D5EB8
		public static IXmlaMethodProperty Catalog(string name)
		{
			return XmlaProperties.CreateXmlaProperty("Catalog", name);
		}

		// Token: 0x06008120 RID: 33056 RVA: 0x001D7CC5 File Offset: 0x001D5EC5
		public static IXmlaMethodProperty Cube(string name)
		{
			return XmlaProperties.CreateXmlaProperty("Cube", name);
		}

		// Token: 0x06008121 RID: 33057 RVA: 0x001D7CD2 File Offset: 0x001D5ED2
		public static IXmlaMethodProperty DataSourceInfo(string info)
		{
			return XmlaProperties.CreateXmlaProperty("DataSourceInfo\t", info);
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x001D7CDF File Offset: 0x001D5EDF
		public static IXmlaMethodProperty ServerName(string name)
		{
			return XmlaProperties.CreateXmlaProperty("ServerName", name);
		}

		// Token: 0x06008123 RID: 33059 RVA: 0x001D7CEC File Offset: 0x001D5EEC
		private static IXmlaMethodProperty CreateXmlaProperty(string name, object value)
		{
			return new GenericXmlaProperty
			{
				Name = name,
				Value = value
			};
		}
	}
}
