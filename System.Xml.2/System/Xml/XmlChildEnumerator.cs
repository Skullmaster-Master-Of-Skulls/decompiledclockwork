using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000FE RID: 254
	internal sealed class XmlChildEnumerator : IEnumerator
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x00049D9D File Offset: 0x00047F9D
		internal XmlChildEnumerator(XmlNode container)
		{
			this.container = container;
			this.child = container.FirstChild;
			this.isFirst = true;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00049DBF File Offset: 0x00047FBF
		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00049DC8 File Offset: 0x00047FC8
		internal bool MoveNext()
		{
			if (this.isFirst)
			{
				this.child = this.container.FirstChild;
				this.isFirst = false;
			}
			else if (this.child != null)
			{
				this.child = this.child.NextSibling;
			}
			return this.child != null;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00049E19 File Offset: 0x00048019
		void IEnumerator.Reset()
		{
			this.isFirst = true;
			this.child = this.container.FirstChild;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x00049E33 File Offset: 0x00048033
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00049E3B File Offset: 0x0004803B
		internal XmlNode Current
		{
			get
			{
				if (this.isFirst || this.child == null)
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
				}
				return this.child;
			}
		}

		// Token: 0x040004D2 RID: 1234
		internal XmlNode container;

		// Token: 0x040004D3 RID: 1235
		internal XmlNode child;

		// Token: 0x040004D4 RID: 1236
		internal bool isFirst;
	}
}
