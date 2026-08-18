using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000144 RID: 324
	internal sealed class XmlIgnoreNamespaceReader : XmlNodeReader
	{
		// Token: 0x06001338 RID: 4920 RVA: 0x000993E0 File Offset: 0x000987E0
		internal XmlIgnoreNamespaceReader(XmlDocument xdoc, string[] namespacesToIgnore) : base(xdoc)
		{
			this.namespacesToIgnore = new List<string>(namespacesToIgnore);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00099400 File Offset: 0x00098800
		public override bool MoveToFirstAttribute()
		{
			return base.MoveToFirstAttribute() && ((!this.namespacesToIgnore.Contains(this.NamespaceURI) && (!(this.NamespaceURI == "http://www.w3.org/XML/1998/namespace") || !(this.LocalName != "lang"))) || this.MoveToNextAttribute());
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00099458 File Offset: 0x00098858
		public override bool MoveToNextAttribute()
		{
			bool result;
			bool flag;
			do
			{
				result = false;
				flag = false;
				if (base.MoveToNextAttribute())
				{
					result = true;
					if (this.namespacesToIgnore.Contains(this.NamespaceURI) || (this.NamespaceURI == "http://www.w3.org/XML/1998/namespace" && this.LocalName != "lang"))
					{
						flag = true;
					}
				}
			}
			while (flag);
			return result;
		}

		// Token: 0x04000782 RID: 1922
		private List<string> namespacesToIgnore;
	}
}
