using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000018 RID: 24
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not a collection, only implements IEnumerable to get initialization syntax.")]
	public class UnknownElementHandlerMap<TContainingElement> : IEnumerable<KeyValuePair<string, Action<TContainingElement, XmlReader>>>, IEnumerable where TContainingElement : ConfigurationElement
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004482 File Offset: 0x00002682
		public void Add(string elementName, Action<TContainingElement, XmlReader> processingAction)
		{
			this.elementActionMap.Add(elementName, processingAction);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004494 File Offset: 0x00002694
		public bool ProcessElement(TContainingElement parentElement, string elementName, XmlReader reader)
		{
			Action<TContainingElement, XmlReader> action;
			if (this.elementActionMap.TryGetValue(elementName, out action))
			{
				action(parentElement, reader);
				return true;
			}
			return false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000044BC File Offset: 0x000026BC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000044C4 File Offset: 0x000026C4
		public IEnumerator<KeyValuePair<string, Action<TContainingElement, XmlReader>>> GetEnumerator()
		{
			return this.elementActionMap.GetEnumerator();
		}

		// Token: 0x04000029 RID: 41
		private readonly Dictionary<string, Action<TContainingElement, XmlReader>> elementActionMap = new Dictionary<string, Action<TContainingElement, XmlReader>>();
	}
}
