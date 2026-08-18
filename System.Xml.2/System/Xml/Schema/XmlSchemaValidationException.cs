using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	// Token: 0x020002B7 RID: 695
	[Serializable]
	public class XmlSchemaValidationException : XmlSchemaException
	{
		// Token: 0x06002826 RID: 10278 RVA: 0x000D23CB File Offset: 0x000D05CB
		protected XmlSchemaValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000D23D5 File Offset: 0x000D05D5
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000D23DF File Offset: 0x000D05DF
		public XmlSchemaValidationException() : base(null)
		{
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000D23E8 File Offset: 0x000D05E8
		public XmlSchemaValidationException(string message) : base(message, null, 0, 0)
		{
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000D23F4 File Offset: 0x000D05F4
		public XmlSchemaValidationException(string message, Exception innerException) : base(message, innerException, 0, 0)
		{
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000D2400 File Offset: 0x000D0600
		public XmlSchemaValidationException(string message, Exception innerException, int lineNumber, int linePosition) : base(message, innerException, lineNumber, linePosition)
		{
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000D240D File Offset: 0x000D060D
		internal XmlSchemaValidationException(string res, string[] args) : base(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000D241C File Offset: 0x000D061C
		internal XmlSchemaValidationException(string res, string arg) : base(res, new string[]
		{
			arg
		}, null, null, 0, 0, null)
		{
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000D2434 File Offset: 0x000D0634
		internal XmlSchemaValidationException(string res, string arg, string sourceUri, int lineNumber, int linePosition) : base(res, new string[]
		{
			arg
		}, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000D244E File Offset: 0x000D064E
		internal XmlSchemaValidationException(string res, string sourceUri, int lineNumber, int linePosition) : base(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000D245E File Offset: 0x000D065E
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition) : base(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000D246F File Offset: 0x000D066F
		internal XmlSchemaValidationException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition) : base(res, args, innerException, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000D2481 File Offset: 0x000D0681
		internal XmlSchemaValidationException(string res, string[] args, object sourceNode) : base(res, args, null, null, 0, 0, null)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000D2497 File Offset: 0x000D0697
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, object sourceNode) : base(res, args, null, sourceUri, 0, 0, null)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000D24AE File Offset: 0x000D06AE
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject source, object sourceNode) : base(res, args, null, sourceUri, lineNumber, linePosition, source)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x000D24C8 File Offset: 0x000D06C8
		public object SourceObject
		{
			get
			{
				return this.sourceNodeObject;
			}
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000D24D0 File Offset: 0x000D06D0
		protected internal void SetSourceObject(object sourceObject)
		{
			this.sourceNodeObject = sourceObject;
		}

		// Token: 0x0400116B RID: 4459
		private object sourceNodeObject;
	}
}
