using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000D0 RID: 208
	internal sealed class XmlChildEnumerator : IEnumerator
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x00037F58 File Offset: 0x00036F58
		internal XmlChildEnumerator(XmlNode container)
		{
			this.container = container;
			this.child = container.FirstChild;
			this.isFirst = true;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00037F7A File Offset: 0x00036F7A
		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00037F84 File Offset: 0x00036F84
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

		// Token: 0x06000C5E RID: 3166 RVA: 0x00037FD8 File Offset: 0x00036FD8
		void IEnumerator.Reset()
		{
			this.isFirst = true;
			this.child = this.container.FirstChild;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00037FF2 File Offset: 0x00036FF2
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00037FFA File Offset: 0x00036FFA
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

		// Token: 0x040008F3 RID: 2291
		internal XmlNode container;

		// Token: 0x040008F4 RID: 2292
		internal XmlNode child;

		// Token: 0x040008F5 RID: 2293
		internal bool isFirst;
	}
}
