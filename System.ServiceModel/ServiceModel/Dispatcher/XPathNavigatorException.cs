using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000478 RID: 1144
	[KnownType(typeof(string[]))]
	[Serializable]
	public class XPathNavigatorException : XPathException
	{
		// Token: 0x06002C8B RID: 11403 RVA: 0x000AE0C8 File Offset: 0x000AC2C8
		protected XPathNavigatorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000AE0D2 File Offset: 0x000AC2D2
		public XPathNavigatorException()
		{
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000AE0DA File Offset: 0x000AC2DA
		public XPathNavigatorException(string message) : this(message, null)
		{
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000AE0E4 File Offset: 0x000AC2E4
		public XPathNavigatorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000AE0F0 File Offset: 0x000AC2F0
		internal MessageFilterException Process(Opcode op)
		{
			Collection<MessageFilter> filters = new Collection<MessageFilter>();
			op.CollectXPathFilters(filters);
			return new MessageFilterException(this.Message, base.InnerException, filters);
		}
	}
}
