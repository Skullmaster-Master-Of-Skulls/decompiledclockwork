using System;
using System.Collections;
using System.Collections.Generic;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public abstract class RewriteRuleElementStream
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000A456 File Offset: 0x00008656
		public RewriteRuleElementStream(ITreeAdaptor adaptor, string elementDescription)
		{
			this.elementDescription = elementDescription;
			this.adaptor = adaptor;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000A46C File Offset: 0x0000866C
		public RewriteRuleElementStream(ITreeAdaptor adaptor, string elementDescription, object oneElement) : this(adaptor, elementDescription)
		{
			this.Add(oneElement);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000A47D File Offset: 0x0000867D
		public RewriteRuleElementStream(ITreeAdaptor adaptor, string elementDescription, IList elements) : this(adaptor, elementDescription)
		{
			this.singleElement = null;
			this.elements = elements;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000A495 File Offset: 0x00008695
		public virtual void Reset()
		{
			this.cursor = 0;
			this.dirty = true;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public virtual void Add(object el)
		{
			if (el == null)
			{
				return;
			}
			if (this.elements != null)
			{
				this.elements.Add(el);
				return;
			}
			if (this.singleElement == null)
			{
				this.singleElement = el;
				return;
			}
			this.elements = new List<object>(5);
			this.elements.Add(this.singleElement);
			this.singleElement = null;
			this.elements.Add(el);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000A514 File Offset: 0x00008714
		public virtual object NextTree()
		{
			int count = this.Count;
			if (this.dirty || (this.cursor >= count && count == 1))
			{
				object el = this.NextCore();
				return this.Dup(el);
			}
			return this.NextCore();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000A554 File Offset: 0x00008754
		protected virtual object NextCore()
		{
			int count = this.Count;
			if (count == 0)
			{
				throw new RewriteEmptyStreamException(this.elementDescription);
			}
			if (this.cursor >= count)
			{
				if (count == 1)
				{
					return this.ToTree(this.singleElement);
				}
				throw new RewriteCardinalityException(this.elementDescription);
			}
			else
			{
				if (this.singleElement != null)
				{
					this.cursor++;
					return this.ToTree(this.singleElement);
				}
				object result = this.ToTree(this.elements[this.cursor]);
				this.cursor++;
				return result;
			}
		}

		// Token: 0x060003D2 RID: 978
		protected abstract object Dup(object el);

		// Token: 0x060003D3 RID: 979 RVA: 0x0000A5E7 File Offset: 0x000087E7
		protected virtual object ToTree(object el)
		{
			return el;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000A5EA File Offset: 0x000087EA
		public virtual bool HasNext
		{
			get
			{
				return (this.singleElement != null && this.cursor < 1) || (this.elements != null && this.cursor < this.elements.Count);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000A61C File Offset: 0x0000881C
		public virtual int Count
		{
			get
			{
				int result = 0;
				if (this.singleElement != null)
				{
					result = 1;
				}
				if (this.elements != null)
				{
					return this.elements.Count;
				}
				return result;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000A64A File Offset: 0x0000884A
		public virtual string Description
		{
			get
			{
				return this.elementDescription;
			}
		}

		// Token: 0x040000C2 RID: 194
		protected int cursor;

		// Token: 0x040000C3 RID: 195
		protected object singleElement;

		// Token: 0x040000C4 RID: 196
		protected IList elements;

		// Token: 0x040000C5 RID: 197
		protected bool dirty;

		// Token: 0x040000C6 RID: 198
		protected string elementDescription;

		// Token: 0x040000C7 RID: 199
		protected ITreeAdaptor adaptor;
	}
}
