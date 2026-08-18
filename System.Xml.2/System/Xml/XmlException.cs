using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Xml
{
	// Token: 0x0200008E RID: 142
	[__DynamicallyInvokable]
	[Serializable]
	public class XmlException : SystemException
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00012E30 File Offset: 0x00011030
		protected XmlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
			this.sourceUri = string.Empty;
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (!(name == "sourceUri"))
				{
					if (name == "version")
					{
						text = (string)serializationEntry.Value;
					}
				}
				else
				{
					this.sourceUri = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XmlException.CreateMessage(this.res, this.args, this.lineNumber, this.linePosition);
				return;
			}
			this.message = null;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00012F60 File Offset: 0x00011160
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("version", "2.0");
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00012FDA File Offset: 0x000111DA
		[__DynamicallyInvokable]
		public XmlException() : this(null)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00012FE3 File Offset: 0x000111E3
		[__DynamicallyInvokable]
		public XmlException(string message) : this(message, null, 0, 0)
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00012FEF File Offset: 0x000111EF
		[__DynamicallyInvokable]
		public XmlException(string message, Exception innerException) : this(message, innerException, 0, 0)
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00012FFB File Offset: 0x000111FB
		[__DynamicallyInvokable]
		public XmlException(string message, Exception innerException, int lineNumber, int linePosition) : this(message, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001300C File Offset: 0x0001120C
		internal XmlException(string message, Exception innerException, int lineNumber, int linePosition, string sourceUri) : base(XmlException.FormatUserMessage(message, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = ((message == null) ? "Xml_DefaultException" : "Xml_UserException");
			this.args = new string[]
			{
				message
			};
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013070 File Offset: 0x00011270
		internal XmlException(string res, string[] args) : this(res, args, null, 0, 0, null)
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001307E File Offset: 0x0001127E
		internal XmlException(string res, string[] args, string sourceUri) : this(res, args, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001308C File Offset: 0x0001128C
		internal XmlException(string res, string arg) : this(res, new string[]
		{
			arg
		}, null, 0, 0, null)
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000130A3 File Offset: 0x000112A3
		internal XmlException(string res, string arg, string sourceUri) : this(res, new string[]
		{
			arg
		}, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000130BA File Offset: 0x000112BA
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo) : this(res, new string[]
		{
			arg
		}, lineInfo, null)
		{
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000130CF File Offset: 0x000112CF
		internal XmlException(string res, string arg, Exception innerException, IXmlLineInfo lineInfo) : this(res, new string[]
		{
			arg
		}, innerException, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, null)
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013100 File Offset: 0x00011300
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo, string sourceUri) : this(res, new string[]
		{
			arg
		}, lineInfo, sourceUri)
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013116 File Offset: 0x00011316
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo) : this(res, args, lineInfo, null)
		{
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013122 File Offset: 0x00011322
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo, string sourceUri) : this(res, args, null, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, sourceUri)
		{
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013147 File Offset: 0x00011347
		internal XmlException(string res, int lineNumber, int linePosition) : this(res, null, null, lineNumber, linePosition)
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00013154 File Offset: 0x00011354
		internal XmlException(string res, string arg, int lineNumber, int linePosition) : this(res, new string[]
		{
			arg
		}, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001316C File Offset: 0x0001136C
		internal XmlException(string res, string arg, int lineNumber, int linePosition, string sourceUri) : this(res, new string[]
		{
			arg
		}, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013185 File Offset: 0x00011385
		internal XmlException(string res, string[] args, int lineNumber, int linePosition) : this(res, args, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00013194 File Offset: 0x00011394
		internal XmlException(string res, string[] args, int lineNumber, int linePosition, string sourceUri) : this(res, args, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000131A4 File Offset: 0x000113A4
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition) : this(res, args, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000131B4 File Offset: 0x000113B4
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition, string sourceUri) : base(XmlException.CreateMessage(res, args, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00013204 File Offset: 0x00011404
		private static string FormatUserMessage(string message, int lineNumber, int linePosition)
		{
			if (message == null)
			{
				return XmlException.CreateMessage("Xml_DefaultException", null, lineNumber, linePosition);
			}
			if (lineNumber == 0 && linePosition == 0)
			{
				return message;
			}
			return XmlException.CreateMessage("Xml_UserException", new string[]
			{
				message
			}, lineNumber, linePosition);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00013238 File Offset: 0x00011438
		private static string CreateMessage(string res, string[] args, int lineNumber, int linePosition)
		{
			string result;
			try
			{
				string @string;
				if (lineNumber == 0)
				{
					@string = Res.GetString(res, args);
				}
				else
				{
					string text = lineNumber.ToString(CultureInfo.InvariantCulture);
					string text2 = linePosition.ToString(CultureInfo.InvariantCulture);
					@string = Res.GetString(res, args);
					string name = "Xml_MessageWithErrorPosition";
					object[] array = new string[]
					{
						@string,
						text,
						text2
					};
					@string = Res.GetString(name, array);
				}
				result = @string;
			}
			catch (MissingManifestResourceException)
			{
				result = "UNKNOWN(" + res + ")";
			}
			return result;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000132C4 File Offset: 0x000114C4
		internal static string[] BuildCharExceptionArgs(string data, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data[invCharIndex], (invCharIndex + 1 < data.Length) ? data[invCharIndex + 1] : '\0');
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000132E9 File Offset: 0x000114E9
		internal static string[] BuildCharExceptionArgs(char[] data, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data, data.Length, invCharIndex);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000132F5 File Offset: 0x000114F5
		internal static string[] BuildCharExceptionArgs(char[] data, int length, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data[invCharIndex], (invCharIndex + 1 < length) ? data[invCharIndex + 1] : '\0');
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00013310 File Offset: 0x00011510
		internal static string[] BuildCharExceptionArgs(char invChar, char nextChar)
		{
			string[] array = new string[2];
			if (XmlCharType.IsHighSurrogate((int)invChar) && nextChar != '\0')
			{
				int num = XmlCharType.CombineSurrogateChar((int)nextChar, (int)invChar);
				array[0] = new string(new char[]
				{
					invChar,
					nextChar
				});
				array[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", new object[]
				{
					num
				});
			}
			else
			{
				if (invChar == '\0')
				{
					array[0] = ".";
				}
				else
				{
					array[0] = invChar.ToString(CultureInfo.InvariantCulture);
				}
				array[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", new object[]
				{
					(int)invChar
				});
			}
			return array;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000133AE File Offset: 0x000115AE
		[__DynamicallyInvokable]
		public int LineNumber
		{
			[__DynamicallyInvokable]
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000133B6 File Offset: 0x000115B6
		[__DynamicallyInvokable]
		public int LinePosition
		{
			[__DynamicallyInvokable]
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000133BE File Offset: 0x000115BE
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000133C6 File Offset: 0x000115C6
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.message != null)
				{
					return this.message;
				}
				return base.Message;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000133DD File Offset: 0x000115DD
		internal string ResString
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000133E5 File Offset: 0x000115E5
		internal static bool IsCatchableException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is ThreadInterruptedException) && !(e is NullReferenceException) && !(e is AccessViolationException);
		}

		// Token: 0x040001FA RID: 506
		private string res;

		// Token: 0x040001FB RID: 507
		private string[] args;

		// Token: 0x040001FC RID: 508
		private int lineNumber;

		// Token: 0x040001FD RID: 509
		private int linePosition;

		// Token: 0x040001FE RID: 510
		[OptionalField]
		private string sourceUri;

		// Token: 0x040001FF RID: 511
		private string message;
	}
}
