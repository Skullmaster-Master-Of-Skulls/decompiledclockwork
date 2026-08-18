using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BC2 RID: 7106
	public static class XmlNodeExtensions
	{
		// Token: 0x0601129F RID: 70303 RVA: 0x003C90EC File Offset: 0x003C72EC
		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public static string ChildElementInnerText(this XmlNode node, string childName)
		{
			XmlElement xmlElement = node[childName];
			if (xmlElement == null)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Child element with specified name: {0} cannot be found.", new object[]
				{
					childName
				});
				Trace.WriteLine(message);
				return null;
			}
			return xmlElement.InnerText;
		}
	}
}
