using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000FD RID: 253
	internal sealed class XmlIgnoreNamespaceReader : XmlNodeReader
	{
		// Token: 0x06000ECE RID: 3790 RVA: 0x00226DC8 File Offset: 0x002261C8
		internal XmlIgnoreNamespaceReader(XmlDocument xdoc, string[] namespacesToIgnore) : base(xdoc)
		{
			this.namespacesToIgnore = new List<string>(namespacesToIgnore);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00226DE8 File Offset: 0x002261E8
		public override bool MoveToFirstAttribute()
		{
			return base.MoveToFirstAttribute() && ((!this.namespacesToIgnore.Contains(this.NamespaceURI) && (!(this.NamespaceURI == "http://www.w3.org/XML/1998/namespace") || !(this.LocalName != "lang"))) || this.MoveToNextAttribute());
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00226E48 File Offset: 0x00226248
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

		// Token: 0x04000AA9 RID: 2729
		private List<string> namespacesToIgnore;
	}
}
