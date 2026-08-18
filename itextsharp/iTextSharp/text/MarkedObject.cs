using System;
using System.Collections.Generic;
using System.util;

namespace iTextSharp.text
{
	// Token: 0x020003BD RID: 957
	public class MarkedObject : IElement
	{
		// Token: 0x06002144 RID: 8516 RVA: 0x000C97F0 File Offset: 0x000C87F0
		protected MarkedObject()
		{
			this.element = null;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000C980A File Offset: 0x000C880A
		public MarkedObject(IElement element)
		{
			this.element = element;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x000C9824 File Offset: 0x000C8824
		public virtual List<Chunk> Chunks
		{
			get
			{
				return this.element.Chunks;
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000C9834 File Offset: 0x000C8834
		public virtual bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				result = listener.Add(this.element);
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x000C9868 File Offset: 0x000C8868
		public virtual int Type
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000C986C File Offset: 0x000C886C
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000C986F File Offset: 0x000C886F
		public bool IsNestable()
		{
			return true;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x000C9872 File Offset: 0x000C8872
		public virtual Properties MarkupAttributes
		{
			get
			{
				return this.markupAttributes;
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000C987A File Offset: 0x000C887A
		public virtual void SetMarkupAttribute(string key, string value)
		{
			this.markupAttributes.Add(key, value);
		}

		// Token: 0x040016F1 RID: 5873
		protected internal IElement element;

		// Token: 0x040016F2 RID: 5874
		protected internal Properties markupAttributes = new Properties();
	}
}
