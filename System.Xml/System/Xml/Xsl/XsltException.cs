using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Utils;

namespace System.Xml.Xsl
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	public class XsltException : SystemException
	{
		// Token: 0x060013FB RID: 5115 RVA: 0x000560F0 File Offset: 0x000550F0
		protected XsltException(SerializationInfo info, StreamingContext context) : base(info, context)
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
				this.message = XsltException.CreateMessage(this.res, this.args, this.sourceUri, this.lineNumber, this.linePosition);
				return;
			}
			this.message = null;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00056214 File Offset: 0x00055214
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
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

		// Token: 0x060013FD RID: 5117 RVA: 0x0005628E File Offset: 0x0005528E
		public XsltException() : this(string.Empty, null)
		{
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0005629C File Offset: 0x0005529C
		public XsltException(string message) : this(message, null)
		{
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000562A8 File Offset: 0x000552A8
		public XsltException(string message, Exception innerException) : this("Xml_UserException", new string[]
		{
			message
		}, null, 0, 0, innerException)
		{
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000562D0 File Offset: 0x000552D0
		internal static XsltException Create(string res, params string[] args)
		{
			return new XsltException(res, args, null, 0, 0, null);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000562DD File Offset: 0x000552DD
		internal static XsltException Create(string res, string[] args, Exception inner)
		{
			return new XsltException(res, args, null, 0, 0, inner);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x000562EA File Offset: 0x000552EA
		internal XsltException(string res, string[] args, string sourceUri, int lineNumber, int linePosition, Exception inner) : base(XsltException.CreateMessage(res, args, sourceUri, lineNumber, linePosition), inner)
		{
			base.HResult = -2146231998;
			this.res = res;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00056329 File Offset: 0x00055329
		public virtual string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00056331 File Offset: 0x00055331
		public virtual int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00056339 File Offset: 0x00055339
		public virtual int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00056341 File Offset: 0x00055341
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

		// Token: 0x06001407 RID: 5127 RVA: 0x00056358 File Offset: 0x00055358
		private static string CreateMessage(string res, string[] args, string sourceUri, int lineNumber, int linePosition)
		{
			string result;
			try
			{
				string text = XsltException.FormatMessage(res, args);
				if (res != "Xslt_CompileError" && lineNumber != 0)
				{
					text = text + " " + XsltException.FormatMessage("Xml_ErrorFilePosition", new string[]
					{
						sourceUri,
						lineNumber.ToString(CultureInfo.InvariantCulture),
						linePosition.ToString(CultureInfo.InvariantCulture)
					});
				}
				result = text;
			}
			catch (MissingManifestResourceException)
			{
				result = "UNKNOWN(" + res + ")";
			}
			return result;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000563E8 File Offset: 0x000553E8
		private static string FormatMessage(string key, params string[] args)
		{
			string text = Res.GetString(key);
			if (text != null && args != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, text, args);
			}
			return text;
		}

		// Token: 0x04000C3E RID: 3134
		private string res;

		// Token: 0x04000C3F RID: 3135
		private string[] args;

		// Token: 0x04000C40 RID: 3136
		private string sourceUri;

		// Token: 0x04000C41 RID: 3137
		private int lineNumber;

		// Token: 0x04000C42 RID: 3138
		private int linePosition;

		// Token: 0x04000C43 RID: 3139
		private string message;
	}
}
