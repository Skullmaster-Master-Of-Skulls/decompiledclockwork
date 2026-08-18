using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	// Token: 0x02000284 RID: 644
	[Serializable]
	public class XmlSchemaException : SystemException
	{
		// Token: 0x060026A2 RID: 9890 RVA: 0x000CED18 File Offset: 0x000CCF18
		protected XmlSchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			this.sourceUri = (string)info.GetValue("sourceUri", typeof(string));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "version")
				{
					text = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XmlSchemaException.CreateMessage(this.res, this.args);
				return;
			}
			this.message = null;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000CEE2C File Offset: 0x000CD02C
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("version", "2.0");
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000CEEA6 File Offset: 0x000CD0A6
		public XmlSchemaException() : this(null)
		{
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000CEEAF File Offset: 0x000CD0AF
		public XmlSchemaException(string message) : this(message, null, 0, 0)
		{
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000CEEBB File Offset: 0x000CD0BB
		public XmlSchemaException(string message, Exception innerException) : this(message, innerException, 0, 0)
		{
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000CEEC7 File Offset: 0x000CD0C7
		public XmlSchemaException(string message, Exception innerException, int lineNumber, int linePosition) : this((message == null) ? "Sch_DefaultException" : "Xml_UserException", new string[]
		{
			message
		}, innerException, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000CEEEE File Offset: 0x000CD0EE
		internal XmlSchemaException(string res, string[] args) : this(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000CEEFD File Offset: 0x000CD0FD
		internal XmlSchemaException(string res, string arg) : this(res, new string[]
		{
			arg
		}, null, null, 0, 0, null)
		{
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000CEF15 File Offset: 0x000CD115
		internal XmlSchemaException(string res, string arg, string sourceUri, int lineNumber, int linePosition) : this(res, new string[]
		{
			arg
		}, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000CEF2F File Offset: 0x000CD12F
		internal XmlSchemaException(string res, string sourceUri, int lineNumber, int linePosition) : this(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000CEF3F File Offset: 0x000CD13F
		internal XmlSchemaException(string res, string[] args, string sourceUri, int lineNumber, int linePosition) : this(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000CEF50 File Offset: 0x000CD150
		internal XmlSchemaException(string res, XmlSchemaObject source) : this(res, null, source)
		{
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000CEF5B File Offset: 0x000CD15B
		internal XmlSchemaException(string res, string arg, XmlSchemaObject source) : this(res, new string[]
		{
			arg
		}, source)
		{
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000CEF6F File Offset: 0x000CD16F
		internal XmlSchemaException(string res, string[] args, XmlSchemaObject source) : this(res, args, null, source.SourceUri, source.LineNumber, source.LinePosition, source)
		{
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000CEF90 File Offset: 0x000CD190
		internal XmlSchemaException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject source) : base(XmlSchemaException.CreateMessage(res, args), innerException)
		{
			base.HResult = -2146231999;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
			this.sourceSchemaObject = source;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000CEFE4 File Offset: 0x000CD1E4
		internal static string CreateMessage(string res, string[] args)
		{
			string result;
			try
			{
				result = Res.GetString(res, args);
			}
			catch (MissingManifestResourceException)
			{
				result = "UNKNOWN(" + res + ")";
			}
			return result;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000CF024 File Offset: 0x000CD224
		internal string GetRes
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x000CF02C File Offset: 0x000CD22C
		internal string[] Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x000CF034 File Offset: 0x000CD234
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000CF03C File Offset: 0x000CD23C
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x000CF044 File Offset: 0x000CD244
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000CF04C File Offset: 0x000CD24C
		public XmlSchemaObject SourceSchemaObject
		{
			get
			{
				return this.sourceSchemaObject;
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000CF054 File Offset: 0x000CD254
		internal void SetSource(string sourceUri, int lineNumber, int linePosition)
		{
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000CF06B File Offset: 0x000CD26B
		internal void SetSchemaObject(XmlSchemaObject source)
		{
			this.sourceSchemaObject = source;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000CF074 File Offset: 0x000CD274
		internal void SetSource(XmlSchemaObject source)
		{
			this.sourceSchemaObject = source;
			this.sourceUri = source.SourceUri;
			this.lineNumber = source.LineNumber;
			this.linePosition = source.LinePosition;
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000CF0A1 File Offset: 0x000CD2A1
		internal void SetResourceId(string resourceId)
		{
			this.res = resourceId;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x000CF0AA File Offset: 0x000CD2AA
		public override string Message
		{
			get
			{
				if (this.message != null)
				{
					return this.message;
				}
				return base.Message;
			}
		}

		// Token: 0x040010DD RID: 4317
		private string res;

		// Token: 0x040010DE RID: 4318
		private string[] args;

		// Token: 0x040010DF RID: 4319
		private string sourceUri;

		// Token: 0x040010E0 RID: 4320
		private int lineNumber;

		// Token: 0x040010E1 RID: 4321
		private int linePosition;

		// Token: 0x040010E2 RID: 4322
		[NonSerialized]
		private XmlSchemaObject sourceSchemaObject;

		// Token: 0x040010E3 RID: 4323
		private string message;
	}
}
