using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Xsl;

namespace System.ServiceModel
{
	// Token: 0x0200009F RID: 159
	[ContentProperty("Expression")]
	public class XPathMessageQuery : MessageQuery
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		public XPathMessageQuery() : this(string.Empty, new XPathMessageContext())
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000FFDA File Offset: 0x0000E1DA
		public XPathMessageQuery(string expression) : this(expression, new XPathMessageContext())
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public XPathMessageQuery(string expression, XsltContext context) : this(expression, context)
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000FFF2 File Offset: 0x0000E1F2
		public XPathMessageQuery(string expression, XmlNamespaceManager namespaces)
		{
			if (expression == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("expression");
			}
			this.expression = expression;
			this.namespaces = namespaces;
			this.needCompile = true;
			this.thisLock = new object();
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0001002D File Offset: 0x0000E22D
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00010035 File Offset: 0x0000E235
		[DefaultValue("")]
		public string Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.expression = value;
				this.needCompile = true;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00010058 File Offset: 0x0000E258
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00010060 File Offset: 0x0000E260
		[DefaultValue(null)]
		public XmlNamespaceManager Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
				this.needCompile = true;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00010070 File Offset: 0x0000E270
		public override MessageQueryCollection CreateMessageQueryCollection()
		{
			return new XPathMessageQueryCollection();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00010078 File Offset: 0x0000E278
		public override TResult Evaluate<TResult>(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (typeof(TResult) == typeof(XPathResult) || typeof(TResult) == typeof(string) || typeof(TResult) == typeof(bool) || typeof(TResult) == typeof(object))
			{
				this.EnsureCompile();
				QueryResult<TResult> queryResult = this.matcher.Evaluate<TResult>(message, false);
				return queryResult.GetSingleResult();
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TResult", SR.GetString("UnsupportedMessageQueryResultType", new object[]
			{
				typeof(TResult)
			}));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001014C File Offset: 0x0000E34C
		public override TResult Evaluate<TResult>(MessageBuffer buffer)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			this.EnsureCompile();
			if (typeof(TResult) == typeof(XPathResult) || typeof(TResult) == typeof(string) || typeof(TResult) == typeof(bool) || typeof(TResult) == typeof(object))
			{
				this.EnsureCompile();
				QueryResult<TResult> queryResult = this.matcher.Evaluate<TResult>(buffer);
				return queryResult.GetSingleResult();
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TResult", SR.GetString("UnsupportedMessageQueryResultType", new object[]
			{
				typeof(TResult)
			}));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00010224 File Offset: 0x0000E424
		private void EnsureCompile()
		{
			if (this.needCompile)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (this.needCompile)
					{
						this.matcher = new XPathQueryMatcher(false);
						this.matcher.Compile(this.expression, this.namespaces);
						this.needCompile = false;
					}
				}
			}
		}

		// Token: 0x04000917 RID: 2327
		private string expression;

		// Token: 0x04000918 RID: 2328
		private XPathQueryMatcher matcher;

		// Token: 0x04000919 RID: 2329
		private XmlNamespaceManager namespaces;

		// Token: 0x0400091A RID: 2330
		private bool needCompile;

		// Token: 0x0400091B RID: 2331
		private object thisLock;
	}
}
