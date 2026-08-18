using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	// Token: 0x020002D4 RID: 724
	[Serializable]
	public class XmlSchemaInferenceException : XmlSchemaException
	{
		// Token: 0x06002B79 RID: 11129 RVA: 0x000E740F File Offset: 0x000E560F
		protected XmlSchemaInferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000E7419 File Offset: 0x000E5619
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000E7423 File Offset: 0x000E5623
		public XmlSchemaInferenceException() : base(null)
		{
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000E742C File Offset: 0x000E562C
		public XmlSchemaInferenceException(string message) : base(message, null, 0, 0)
		{
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000E7438 File Offset: 0x000E5638
		public XmlSchemaInferenceException(string message, Exception innerException) : base(message, innerException, 0, 0)
		{
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000E7444 File Offset: 0x000E5644
		public XmlSchemaInferenceException(string message, Exception innerException, int lineNumber, int linePosition) : base(message, innerException, lineNumber, linePosition)
		{
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000E7451 File Offset: 0x000E5651
		internal XmlSchemaInferenceException(string res, string[] args) : base(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000E7460 File Offset: 0x000E5660
		internal XmlSchemaInferenceException(string res, string arg) : base(res, new string[]
		{
			arg
		}, null, null, 0, 0, null)
		{
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000E7478 File Offset: 0x000E5678
		internal XmlSchemaInferenceException(string res, string arg, string sourceUri, int lineNumber, int linePosition) : base(res, new string[]
		{
			arg
		}, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000E7492 File Offset: 0x000E5692
		internal XmlSchemaInferenceException(string res, string sourceUri, int lineNumber, int linePosition) : base(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000E74A2 File Offset: 0x000E56A2
		internal XmlSchemaInferenceException(string res, string[] args, string sourceUri, int lineNumber, int linePosition) : base(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000E74B3 File Offset: 0x000E56B3
		internal XmlSchemaInferenceException(string res, int lineNumber, int linePosition) : base(res, null, null, null, lineNumber, linePosition, null)
		{
		}
	}
}
