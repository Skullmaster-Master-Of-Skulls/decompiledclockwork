using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	// Token: 0x02000281 RID: 641
	[Serializable]
	public class XmlSchemaValidationException : XmlSchemaException
	{
		// Token: 0x06001D69 RID: 7529 RVA: 0x00086004 File Offset: 0x00085004
		protected XmlSchemaValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0008600E File Offset: 0x0008500E
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00086018 File Offset: 0x00085018
		public XmlSchemaValidationException() : base(null)
		{
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00086021 File Offset: 0x00085021
		public XmlSchemaValidationException(string message) : base(message, null, 0, 0)
		{
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0008602D File Offset: 0x0008502D
		public XmlSchemaValidationException(string message, Exception innerException) : base(message, innerException, 0, 0)
		{
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00086039 File Offset: 0x00085039
		public XmlSchemaValidationException(string message, Exception innerException, int lineNumber, int linePosition) : base(message, innerException, lineNumber, linePosition)
		{
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00086046 File Offset: 0x00085046
		internal XmlSchemaValidationException(string res, string[] args) : base(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00086058 File Offset: 0x00085058
		internal XmlSchemaValidationException(string res, string arg) : base(res, new string[]
		{
			arg
		}, null, null, 0, 0, null)
		{
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00086080 File Offset: 0x00085080
		internal XmlSchemaValidationException(string res, string arg, string sourceUri, int lineNumber, int linePosition) : base(res, new string[]
		{
			arg
		}, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000860A7 File Offset: 0x000850A7
		internal XmlSchemaValidationException(string res, string sourceUri, int lineNumber, int linePosition) : base(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000860B7 File Offset: 0x000850B7
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition) : base(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000860C8 File Offset: 0x000850C8
		internal XmlSchemaValidationException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition) : base(res, args, innerException, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000860DA File Offset: 0x000850DA
		internal XmlSchemaValidationException(string res, string[] args, object sourceNode) : base(res, args, null, null, 0, 0, null)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x000860F0 File Offset: 0x000850F0
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, object sourceNode) : base(res, args, null, sourceUri, 0, 0, null)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00086107 File Offset: 0x00085107
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject source, object sourceNode) : base(res, args, null, sourceUri, lineNumber, linePosition, source)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00086121 File Offset: 0x00085121
		public object SourceObject
		{
			get
			{
				return this.sourceNodeObject;
			}
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00086129 File Offset: 0x00085129
		protected internal void SetSourceObject(object sourceObject)
		{
			this.sourceNodeObject = sourceObject;
		}

		// Token: 0x040011EB RID: 4587
		private object sourceNodeObject;
	}
}
