using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000019 RID: 25
	internal class UnknownElementHandlerMap : IEnumerable<KeyValuePair<string, Action<XmlReader>>>, IEnumerable
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000044E9 File Offset: 0x000026E9
		public void Add(string elementName, Action<XmlReader> processingAction)
		{
			this.elementActionMap.Add(elementName, processingAction);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000044F8 File Offset: 0x000026F8
		public bool ProcessElement(string elementName, XmlReader reader)
		{
			Action<XmlReader> action;
			if (this.elementActionMap.TryGetValue(elementName, out action))
			{
				action(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000451F File Offset: 0x0000271F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004527 File Offset: 0x00002727
		public IEnumerator<KeyValuePair<string, Action<XmlReader>>> GetEnumerator()
		{
			return this.elementActionMap.GetEnumerator();
		}

		// Token: 0x0400002A RID: 42
		private readonly Dictionary<string, Action<XmlReader>> elementActionMap = new Dictionary<string, Action<XmlReader>>();
	}
}
