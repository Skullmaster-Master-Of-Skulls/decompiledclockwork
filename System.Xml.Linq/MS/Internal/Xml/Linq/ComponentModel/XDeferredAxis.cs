using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x0200003D RID: 61
	internal class XDeferredAxis<T> : IEnumerable<T>, IEnumerable where T : XObject
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000C3AA File Offset: 0x0000A5AA
		public XDeferredAxis(Func<XElement, XName, IEnumerable<T>> func, XElement element, XName name)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			this.func = func;
			this.element = element;
			this.name = name;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C3E3 File Offset: 0x0000A5E3
		public IEnumerator<T> GetEnumerator()
		{
			return this.func(this.element, this.name).GetEnumerator();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C401 File Offset: 0x0000A601
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700006C RID: 108
		public IEnumerable<T> this[string expandedName]
		{
			get
			{
				if (expandedName == null)
				{
					throw new ArgumentNullException("expandedName");
				}
				if (this.name == null)
				{
					this.name = expandedName;
				}
				else if (this.name != expandedName)
				{
					return Enumerable.Empty<T>();
				}
				return this;
			}
		}

		// Token: 0x040000F2 RID: 242
		private Func<XElement, XName, IEnumerable<T>> func;

		// Token: 0x040000F3 RID: 243
		internal XElement element;

		// Token: 0x040000F4 RID: 244
		internal XName name;
	}
}
