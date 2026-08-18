using System;
using System.Runtime;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200051C RID: 1308
	public sealed class XPathResult : IDisposable
	{
		// Token: 0x06003189 RID: 12681 RVA: 0x000BE3C7 File Offset: 0x000BC5C7
		internal XPathResult(XPathNodeIterator nodeSetResult) : this()
		{
			this.nodeSetResult = nodeSetResult;
			this.internalIterator = (nodeSetResult as SafeNodeSequenceIterator);
			this.resultType = XPathResultType.NodeSet;
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000BE3E9 File Offset: 0x000BC5E9
		internal XPathResult(string stringResult) : this()
		{
			this.stringResult = stringResult;
			this.resultType = XPathResultType.String;
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000BE3FF File Offset: 0x000BC5FF
		internal XPathResult(bool boolResult) : this()
		{
			this.boolResult = boolResult;
			this.resultType = XPathResultType.Boolean;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000BE415 File Offset: 0x000BC615
		internal XPathResult(double numberResult) : this()
		{
			this.numberResult = numberResult;
			this.resultType = XPathResultType.Number;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000BE42B File Offset: 0x000BC62B
		private XPathResult()
		{
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000BE433 File Offset: 0x000BC633
		public XPathResultType ResultType
		{
			get
			{
				return this.resultType;
			}
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000BE43B File Offset: 0x000BC63B
		public void Dispose()
		{
			if (this.internalIterator != null)
			{
				this.internalIterator.Dispose();
			}
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000BE450 File Offset: 0x000BC650
		public bool GetResultAsBoolean()
		{
			switch (this.resultType)
			{
			case XPathResultType.Number:
				return QueryValueModel.Boolean(this.numberResult);
			case XPathResultType.String:
				return QueryValueModel.Boolean(this.stringResult);
			case XPathResultType.Boolean:
				return this.boolResult;
			case XPathResultType.NodeSet:
				return QueryValueModel.Boolean(this.nodeSetResult);
			default:
				throw Fx.AssertAndThrow("Unexpected result type.");
			}
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000BE4B1 File Offset: 0x000BC6B1
		public XPathNodeIterator GetResultAsNodeset()
		{
			if (this.resultType != XPathResultType.NodeSet)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotRepresentResultAsNodeset")));
			}
			return this.nodeSetResult;
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000BE4DC File Offset: 0x000BC6DC
		public double GetResultAsNumber()
		{
			switch (this.resultType)
			{
			case XPathResultType.Number:
				return this.numberResult;
			case XPathResultType.String:
				return QueryValueModel.Double(this.stringResult);
			case XPathResultType.Boolean:
				return QueryValueModel.Double(this.boolResult);
			case XPathResultType.NodeSet:
				return QueryValueModel.Double(this.nodeSetResult);
			default:
				throw Fx.AssertAndThrow("Unexpected result type.");
			}
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000BE540 File Offset: 0x000BC740
		public string GetResultAsString()
		{
			switch (this.resultType)
			{
			case XPathResultType.Number:
				return QueryValueModel.String(this.numberResult);
			case XPathResultType.String:
				return this.stringResult;
			case XPathResultType.Boolean:
				return QueryValueModel.String(this.boolResult);
			case XPathResultType.NodeSet:
				return QueryValueModel.String(this.nodeSetResult);
			default:
				throw Fx.AssertAndThrow("Unexpected result type.");
			}
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000BE5A4 File Offset: 0x000BC7A4
		internal XPathResult Copy()
		{
			XPathResult xpathResult = new XPathResult();
			xpathResult.resultType = this.resultType;
			switch (this.resultType)
			{
			case XPathResultType.Number:
				xpathResult.numberResult = this.numberResult;
				break;
			case XPathResultType.String:
				xpathResult.stringResult = this.stringResult;
				break;
			case XPathResultType.Boolean:
				xpathResult.boolResult = this.boolResult;
				break;
			case XPathResultType.NodeSet:
				xpathResult.nodeSetResult = this.nodeSetResult.Clone();
				break;
			default:
				throw Fx.AssertAndThrow("Unexpected result type.");
			}
			return xpathResult;
		}

		// Token: 0x0400266A RID: 9834
		private bool boolResult;

		// Token: 0x0400266B RID: 9835
		private SafeNodeSequenceIterator internalIterator;

		// Token: 0x0400266C RID: 9836
		private XPathNodeIterator nodeSetResult;

		// Token: 0x0400266D RID: 9837
		private double numberResult;

		// Token: 0x0400266E RID: 9838
		private XPathResultType resultType;

		// Token: 0x0400266F RID: 9839
		private string stringResult;
	}
}
