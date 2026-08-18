using System;
using System.Collections;
using System.Text;

namespace Telerik.Web.UI.GridExcelBuilder.Abstract
{
	// Token: 0x02000B78 RID: 2936
	public abstract class ElementBase : IElement
	{
		// Token: 0x1700245E RID: 9310
		// (get) Token: 0x06006EDE RID: 28382
		protected abstract string StartTag { get; }

		// Token: 0x1700245F RID: 9311
		// (get) Token: 0x06006EDF RID: 28383
		protected abstract string EndTag { get; }

		// Token: 0x06006EE0 RID: 28384 RVA: 0x0019B7F8 File Offset: 0x001999F8
		protected ElementBase() : this(new ElementsCollection(), new AttributesCollection())
		{
		}

		// Token: 0x06006EE1 RID: 28385 RVA: 0x0019B80A File Offset: 0x00199A0A
		public ElementBase(IElementsCollection elementsCollection, IAttributesCollection attributesCollection)
		{
			this._attributes = attributesCollection;
			this._elements = elementsCollection;
		}

		// Token: 0x17002460 RID: 9312
		// (get) Token: 0x06006EE2 RID: 28386 RVA: 0x0019B820 File Offset: 0x00199A20
		public virtual IElementsCollection InnerElements
		{
			get
			{
				return this._elements;
			}
		}

		// Token: 0x17002461 RID: 9313
		// (get) Token: 0x06006EE3 RID: 28387 RVA: 0x0019B828 File Offset: 0x00199A28
		public virtual IAttributesCollection Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x06006EE4 RID: 28388 RVA: 0x0019B830 File Offset: 0x00199A30
		public virtual void Render(StringBuilder sb)
		{
			this.AppendAttributes(sb);
			this.RenderChildElements(sb);
			sb.Append(this.EndTag);
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x0019B850 File Offset: 0x00199A50
		protected virtual void RenderChildElements(StringBuilder sb)
		{
			foreach (object obj in this.InnerElements)
			{
				IElement element = (IElement)obj;
				element.Render(sb);
			}
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x0019B8AC File Offset: 0x00199AAC
		protected virtual void AppendAttributes(StringBuilder sb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this._attributes)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (dictionaryEntry.Key != null && ((string)dictionaryEntry.Key).Trim().Length > 0)
				{
					stringBuilder.AppendFormat(" {0}=\"{1}\"", dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			sb.AppendFormat(this.StartTag, stringBuilder);
		}

		// Token: 0x17002462 RID: 9314
		// (get) Token: 0x06006EE7 RID: 28391 RVA: 0x0019B950 File Offset: 0x00199B50
		IElementsCollection IElement.InnerElements
		{
			get
			{
				return this.InnerElements;
			}
		}

		// Token: 0x17002463 RID: 9315
		// (get) Token: 0x06006EE8 RID: 28392 RVA: 0x0019B958 File Offset: 0x00199B58
		IAttributesCollection IElement.Attributes
		{
			get
			{
				return this.Attributes;
			}
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x0019B960 File Offset: 0x00199B60
		void IElement.Render(StringBuilder sb)
		{
			this.Render(sb);
		}

		// Token: 0x04001DEA RID: 7658
		private IElementsCollection _elements;

		// Token: 0x04001DEB RID: 7659
		private IAttributesCollection _attributes;
	}
}
