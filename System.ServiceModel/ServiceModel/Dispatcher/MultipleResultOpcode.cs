using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004ED RID: 1261
	internal abstract class MultipleResultOpcode : ResultOpcode
	{
		// Token: 0x06002FB3 RID: 12211 RVA: 0x000B74F3 File Offset: 0x000B56F3
		internal MultipleResultOpcode(OpcodeID id) : base(id)
		{
			this.flags |= OpcodeFlags.Multiple;
			this.results = new QueryBuffer<object>(1);
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000B7518 File Offset: 0x000B5718
		internal override void Add(Opcode op)
		{
			MultipleResultOpcode multipleResultOpcode = op as MultipleResultOpcode;
			if (multipleResultOpcode != null)
			{
				this.results.Add(ref multipleResultOpcode.results);
				this.results.TrimToCount();
				return;
			}
			base.Add(op);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000B7553 File Offset: 0x000B5753
		public void AddItem(object item)
		{
			this.results.Add(item);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000B7564 File Offset: 0x000B5764
		internal override void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			for (int i = 0; i < this.results.Count; i++)
			{
				XPathMessageFilter xpathMessageFilter = this.results[i] as XPathMessageFilter;
				if (xpathMessageFilter != null)
				{
					filters.Add(xpathMessageFilter);
				}
			}
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000B75A3 File Offset: 0x000B57A3
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this == op;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000B75B4 File Offset: 0x000B57B4
		public void RemoveItem(object item)
		{
			this.results.Remove(item);
			this.Remove();
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000B75C8 File Offset: 0x000B57C8
		internal override void Remove()
		{
			if (this.results.Count == 0)
			{
				base.Remove();
			}
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000B75DD File Offset: 0x000B57DD
		internal override void Trim()
		{
			this.results.TrimToCount();
		}

		// Token: 0x040025E7 RID: 9703
		protected QueryBuffer<object> results;
	}
}
