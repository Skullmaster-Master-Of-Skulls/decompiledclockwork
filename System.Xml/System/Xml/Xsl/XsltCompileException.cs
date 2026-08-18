using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Xsl
{
	// Token: 0x02000179 RID: 377
	[Serializable]
	public class XsltCompileException : XsltException
	{
		// Token: 0x06001409 RID: 5129 RVA: 0x00056410 File Offset: 0x00055410
		protected XsltCompileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0005641A File Offset: 0x0005541A
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00056424 File Offset: 0x00055424
		public XsltCompileException()
		{
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0005642C File Offset: 0x0005542C
		public XsltCompileException(string message) : base(message)
		{
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00056435 File Offset: 0x00055435
		public XsltCompileException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00056440 File Offset: 0x00055440
		public XsltCompileException(Exception inner, string sourceUri, int lineNumber, int linePosition) : base((lineNumber != 0) ? "Xslt_CompileError" : "Xslt_CompileError2", new string[]
		{
			sourceUri,
			lineNumber.ToString(CultureInfo.InvariantCulture),
			linePosition.ToString(CultureInfo.InvariantCulture)
		}, sourceUri, lineNumber, linePosition, inner)
		{
		}
	}
}
