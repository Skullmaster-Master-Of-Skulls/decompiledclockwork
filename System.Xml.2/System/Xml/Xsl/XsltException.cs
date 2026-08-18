using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Utils;

namespace System.Xml.Xsl
{
	// Token: 0x020002DC RID: 732
	[Serializable]
	public class XsltException : SystemException
	{
		// Token: 0x06002BD1 RID: 11217 RVA: 0x000E8090 File Offset: 0x000E6290
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

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000E81B4 File Offset: 0x000E63B4
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

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000E822E File Offset: 0x000E642E
		public XsltException() : this(string.Empty, null)
		{
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000E823C File Offset: 0x000E643C
		public XsltException(string message) : this(message, null)
		{
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000E8246 File Offset: 0x000E6446
		public XsltException(string message, Exception innerException) : this("Xml_UserException", new string[]
		{
			message
		}, null, 0, 0, innerException)
		{
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000E8261 File Offset: 0x000E6461
		internal static XsltException Create(string res, params string[] args)
		{
			return new XsltException(res, args, null, 0, 0, null);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000E826E File Offset: 0x000E646E
		internal static XsltException Create(string res, string[] args, Exception inner)
		{
			return new XsltException(res, args, null, 0, 0, inner);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000E827B File Offset: 0x000E647B
		internal XsltException(string res, string[] args, string sourceUri, int lineNumber, int linePosition, Exception inner) : base(XsltException.CreateMessage(res, args, sourceUri, lineNumber, linePosition), inner)
		{
			base.HResult = -2146231998;
			this.res = res;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x000E82BA File Offset: 0x000E64BA
		public virtual string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000E82C2 File Offset: 0x000E64C2
		public virtual int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x000E82CA File Offset: 0x000E64CA
		public virtual int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x000E82D2 File Offset: 0x000E64D2
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

		// Token: 0x06002BDD RID: 11229 RVA: 0x000E82EC File Offset: 0x000E64EC
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

		// Token: 0x06002BDE RID: 11230 RVA: 0x000E8378 File Offset: 0x000E6578
		private static string FormatMessage(string key, params string[] args)
		{
			string text = Res.GetString(key);
			if (text != null && args != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, text, args);
			}
			return text;
		}

		// Token: 0x0400132F RID: 4911
		private string res;

		// Token: 0x04001330 RID: 4912
		private string[] args;

		// Token: 0x04001331 RID: 4913
		private string sourceUri;

		// Token: 0x04001332 RID: 4914
		private int lineNumber;

		// Token: 0x04001333 RID: 4915
		private int linePosition;

		// Token: 0x04001334 RID: 4916
		private string message;
	}
}
