using System;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000526 RID: 1318
	internal class XPathMessageFunctionCallOpcode : Opcode
	{
		// Token: 0x06003231 RID: 12849 RVA: 0x000C146F File Offset: 0x000BF66F
		internal XPathMessageFunctionCallOpcode(XPathMessageFunction fun, int argCount) : base(OpcodeID.XsltInternalFunction)
		{
			this.function = fun;
			this.argCount = argCount;
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000C1486 File Offset: 0x000BF686
		internal XPathResultType ReturnType
		{
			get
			{
				return this.function.ReturnType;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000C1493 File Offset: 0x000BF693
		internal int ArgCount
		{
			get
			{
				return this.argCount;
			}
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000C149C File Offset: 0x000BF69C
		internal override bool Equals(Opcode op)
		{
			if (base.Equals(op))
			{
				XPathMessageFunctionCallOpcode xpathMessageFunctionCallOpcode = op as XPathMessageFunctionCallOpcode;
				if (xpathMessageFunctionCallOpcode != null)
				{
					return this.function == xpathMessageFunctionCallOpcode.function;
				}
			}
			return false;
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000C14CC File Offset: 0x000BF6CC
		internal override Opcode Eval(ProcessingContext context)
		{
			this.function.InvokeInternal(context, this.argCount);
			return this.next;
		}

		// Token: 0x04002713 RID: 10003
		private XPathMessageFunction function;

		// Token: 0x04002714 RID: 10004
		private int argCount;
	}
}
