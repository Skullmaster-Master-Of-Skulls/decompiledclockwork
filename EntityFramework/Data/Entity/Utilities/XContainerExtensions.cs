using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020006F0 RID: 1776
	internal static class XContainerExtensions
	{
		// Token: 0x0600472B RID: 18219 RVA: 0x00150EC4 File Offset: 0x0014F0C4
		public static XElement GetOrAddElement(this XContainer container, XName name)
		{
			XElement xelement = container.Element(name);
			if (xelement == null)
			{
				xelement = new XElement(name);
				container.Add(xelement);
			}
			return xelement;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x00150EEB File Offset: 0x0014F0EB
		public static IEnumerable<XElement> Descendants(this XContainer container, IEnumerable<XName> name)
		{
			return name.SelectMany(new Func<XName, IEnumerable<XElement>>(container.Descendants));
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x00150EFF File Offset: 0x0014F0FF
		public static IEnumerable<XElement> Elements(this XContainer container, IEnumerable<XName> name)
		{
			return name.SelectMany(new Func<XName, IEnumerable<XElement>>(container.Elements));
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00150F70 File Offset: 0x0014F170
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source, IEnumerable<XName> name) where T : XContainer
		{
			return name.SelectMany((XName n) => source.SelectMany((T c) => c.Descendants(n)));
		}
	}
}
