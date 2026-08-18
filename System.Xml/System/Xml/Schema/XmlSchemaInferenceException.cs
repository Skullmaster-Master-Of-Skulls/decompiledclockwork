using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	// Token: 0x020002AC RID: 684
	[Serializable]
	public class XmlSchemaInferenceException : XmlSchemaException
	{
		// Token: 0x060020D7 RID: 8407 RVA: 0x0009B4ED File Offset: 0x0009A4ED
		protected XmlSchemaInferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0009B4F7 File Offset: 0x0009A4F7
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0009B501 File Offset: 0x0009A501
		public XmlSchemaInferenceException() : base(null)
		{
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0009B50A File Offset: 0x0009A50A
		public XmlSchemaInferenceException(string message) : base(message, null, 0, 0)
		{
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0009B516 File Offset: 0x0009A516
		public XmlSchemaInferenceException(string message, Exception innerException) : base(message, innerException, 0, 0)
		{
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0009B522 File Offset: 0x0009A522
		public XmlSchemaInferenceException(string message, Exception innerException, int lineNumber, int linePosition) : base(message, innerException, lineNumber, linePosition)
		{
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0009B52F File Offset: 0x0009A52F
		internal XmlSchemaInferenceException(string res, string[] args) : base(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x0009B540 File Offset: 0x0009A540
		internal XmlSchemaInferenceException(string res, string arg) : base(res, new string[]
		{
			arg
		}, null, null, 0, 0, null)
		{
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x0009B568 File Offset: 0x0009A568
		internal XmlSchemaInferenceException(string res, string arg, string sourceUri, int lineNumber, int linePosition) : base(res, new string[]
		{
			arg
		}, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x0009B58F File Offset: 0x0009A58F
		internal XmlSchemaInferenceException(string res, string sourceUri, int lineNumber, int linePosition) : base(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0009B59F File Offset: 0x0009A59F
		internal XmlSchemaInferenceException(string res, string[] args, string sourceUri, int lineNumber, int linePosition) : base(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0009B5B0 File Offset: 0x0009A5B0
		internal XmlSchemaInferenceException(string res, int lineNumber, int linePosition) : base(res, null, null, null, lineNumber, linePosition, null)
		{
		}
	}
}
