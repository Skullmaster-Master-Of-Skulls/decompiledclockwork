using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Xsl
{
	// Token: 0x020002DD RID: 733
	[Serializable]
	public class XsltCompileException : XsltException
	{
		// Token: 0x06002BDF RID: 11231 RVA: 0x000E83A2 File Offset: 0x000E65A2
		protected XsltCompileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000E83AC File Offset: 0x000E65AC
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000E83B6 File Offset: 0x000E65B6
		public XsltCompileException()
		{
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000E83BE File Offset: 0x000E65BE
		public XsltCompileException(string message) : base(message)
		{
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000E83C7 File Offset: 0x000E65C7
		public XsltCompileException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000E83D4 File Offset: 0x000E65D4
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
