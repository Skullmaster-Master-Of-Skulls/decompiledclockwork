using System;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x0200003E RID: 62
	internal class XDeferredSingleton<T> where T : XObject
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000C45D File Offset: 0x0000A65D
		public XDeferredSingleton(Func<XElement, XName, T> func, XElement element, XName name)
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

		// Token: 0x1700006D RID: 109
		public T this[string expandedName]
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
					return default(T);
				}
				return this.func(this.element, this.name);
			}
		}

		// Token: 0x040000F5 RID: 245
		private Func<XElement, XName, T> func;

		// Token: 0x040000F6 RID: 246
		internal XElement element;

		// Token: 0x040000F7 RID: 247
		internal XName name;
	}
}
