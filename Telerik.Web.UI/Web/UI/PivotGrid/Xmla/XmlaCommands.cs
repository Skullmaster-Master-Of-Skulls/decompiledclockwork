using System;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D8A RID: 3466
	internal class XmlaCommands
	{
		// Token: 0x06008108 RID: 33032 RVA: 0x001D7A68 File Offset: 0x001D5C68
		public static IXmlaCommand Statement(string value)
		{
			return XmlaCommands.CreateTextBodyCommand("Statement", value);
		}

		// Token: 0x06008109 RID: 33033 RVA: 0x001D7A75 File Offset: 0x001D5C75
		private static XmlaTextBodyCommand CreateTextBodyCommand(string name, string body)
		{
			return new XmlaTextBodyCommand(name, body);
		}
	}
}
