using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x02000621 RID: 1569
	[ComVisible(true)]
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		// Token: 0x06003897 RID: 14487 RVA: 0x000BEE9A File Offset: 0x000BDE9A
		public XmlSyntaxException() : base(Environment.GetResourceString("XMLSyntax_InvalidSyntax"))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000BEEB7 File Offset: 0x000BDEB7
		public XmlSyntaxException(string message) : base(message)
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000BEECB File Offset: 0x000BDECB
		public XmlSyntaxException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000BEEE0 File Offset: 0x000BDEE0
		public XmlSyntaxException(int lineNumber) : base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxError"), new object[]
		{
			lineNumber
		}))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000BEF24 File Offset: 0x000BDF24
		public XmlSyntaxException(int lineNumber, string message) : base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("XMLSyntax_SyntaxErrorEx"), new object[]
		{
			lineNumber,
			message
		}))
		{
			base.SetErrorCode(-2146233320);
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000BEF6B File Offset: 0x000BDF6B
		internal XmlSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
