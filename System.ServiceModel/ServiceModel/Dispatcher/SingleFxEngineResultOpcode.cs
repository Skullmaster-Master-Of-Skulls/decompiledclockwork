using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A2 RID: 1186
	internal abstract class SingleFxEngineResultOpcode : ResultOpcode
	{
		// Token: 0x06002D75 RID: 11637 RVA: 0x000B1C41 File Offset: 0x000AFE41
		internal SingleFxEngineResultOpcode(OpcodeID id) : base(id)
		{
			this.flags |= OpcodeFlags.Fx;
		}

		// Token: 0x17000AD1 RID: 2769
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x000B1C5C File Offset: 0x000AFE5C
		internal object Item
		{
			set
			{
				this.item = value;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (set) Token: 0x06002D77 RID: 11639 RVA: 0x000B1C65 File Offset: 0x000AFE65
		internal XPathExpression XPath
		{
			set
			{
				this.xpath = value;
			}
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000B1C70 File Offset: 0x000AFE70
		internal override void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			MessageFilter messageFilter = this.item as MessageFilter;
			if (messageFilter != null)
			{
				filters.Add(messageFilter);
			}
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000B1C93 File Offset: 0x000AFE93
		internal override bool Equals(Opcode op)
		{
			return false;
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000B1C98 File Offset: 0x000AFE98
		protected object Evaluate(XPathNavigator nav)
		{
			SeekableMessageNavigator seekableMessageNavigator = nav as SeekableMessageNavigator;
			if (seekableMessageNavigator != null)
			{
				seekableMessageNavigator.Atomize();
			}
			object result;
			if (XPathResultType.NodeSet == this.xpath.ReturnType)
			{
				result = nav.Select(this.xpath);
			}
			else
			{
				result = nav.Evaluate(this.xpath);
			}
			return result;
		}

		// Token: 0x040024CC RID: 9420
		protected XPathExpression xpath;

		// Token: 0x040024CD RID: 9421
		protected object item;
	}
}
