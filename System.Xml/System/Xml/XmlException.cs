using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Xml
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class XmlException : SystemException
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00007FC8 File Offset: 0x00006FC8
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
				string name;
				if ((name = serializationEntry.Name) != null)
				{
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
			}
			if (text == null)
			{
				this.message = XmlException.CreateMessage(this.res, this.args, this.lineNumber, this.linePosition);
				return;
			}
			this.message = null;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000080F8 File Offset: 0x000070F8
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
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

		// Token: 0x060001B2 RID: 434 RVA: 0x00008172 File Offset: 0x00007172
		public XmlException() : this(null)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000817B File Offset: 0x0000717B
		public XmlException(string message) : this(message, null, 0, 0)
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008187 File Offset: 0x00007187
		public XmlException(string message, Exception innerException) : this(message, innerException, 0, 0)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008193 File Offset: 0x00007193
		public XmlException(string message, Exception innerException, int lineNumber, int linePosition) : this(message, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000081A4 File Offset: 0x000071A4
		internal XmlException(string message, Exception innerException, int lineNumber, int linePosition, string sourceUri) : this((message == null) ? "Xml_DefaultException" : "Xml_UserException", new string[]
		{
			message
		}, innerException, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000081D8 File Offset: 0x000071D8
		internal XmlException(string res, string[] args) : this(res, args, null, 0, 0, null)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000081E6 File Offset: 0x000071E6
		internal XmlException(string res, string[] args, string sourceUri) : this(res, args, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000081F4 File Offset: 0x000071F4
		internal XmlException(string res, string arg) : this(res, new string[]
		{
			arg
		}, null, 0, 0, null)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008218 File Offset: 0x00007218
		internal XmlException(string res, string arg, string sourceUri) : this(res, new string[]
		{
			arg
		}, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000823C File Offset: 0x0000723C
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo) : this(res, new string[]
		{
			arg
		}, lineInfo, null)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008260 File Offset: 0x00007260
		internal XmlException(string res, string arg, Exception innerException, IXmlLineInfo lineInfo) : this(res, new string[]
		{
			arg
		}, innerException, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, null)
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000082A0 File Offset: 0x000072A0
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo, string sourceUri) : this(res, new string[]
		{
			arg
		}, lineInfo, sourceUri)
		{
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000082C3 File Offset: 0x000072C3
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo) : this(res, args, lineInfo, null)
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000082CF File Offset: 0x000072CF
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo, string sourceUri) : this(res, args, null, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, sourceUri)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000082F4 File Offset: 0x000072F4
		internal XmlException(string res, int lineNumber, int linePosition) : this(res, null, null, lineNumber, linePosition)
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008304 File Offset: 0x00007304
		internal XmlException(string res, string arg, int lineNumber, int linePosition) : this(res, new string[]
		{
			arg
		}, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000832C File Offset: 0x0000732C
		internal XmlException(string res, string arg, int lineNumber, int linePosition, string sourceUri) : this(res, new string[]
		{
			arg
		}, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008352 File Offset: 0x00007352
		internal XmlException(string res, string[] args, int lineNumber, int linePosition) : this(res, args, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008361 File Offset: 0x00007361
		internal XmlException(string res, string[] args, int lineNumber, int linePosition, string sourceUri) : this(res, args, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008371 File Offset: 0x00007371
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition) : this(res, args, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008384 File Offset: 0x00007384
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition, string sourceUri) : base(XmlException.CreateMessage(res, args, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000083D4 File Offset: 0x000073D4
		private static string CreateMessage(string res, string[] args, int lineNumber, int linePosition)
		{
			string result;
			try
			{
				string text = Res.GetString(res, args);
				if (lineNumber != 0)
				{
					text = text + " " + Res.GetString("Xml_ErrorPosition", new string[]
					{
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

		// Token: 0x060001C8 RID: 456 RVA: 0x00008454 File Offset: 0x00007454
		internal static string[] BuildCharExceptionStr(char ch)
		{
			string[] array = new string[2];
			if (ch == '\0')
			{
				array[0] = ".";
			}
			else
			{
				array[0] = ch.ToString(CultureInfo.InvariantCulture);
			}
			string[] array2 = array;
			int num = 1;
			string str = "0x";
			int num2 = (int)ch;
			array2[num] = str + num2.ToString("X2", CultureInfo.InvariantCulture);
			return array;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000084A5 File Offset: 0x000074A5
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000084AD File Offset: 0x000074AD
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000084B5 File Offset: 0x000074B5
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000084BD File Offset: 0x000074BD
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000084D4 File Offset: 0x000074D4
		internal string ResString
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000084DC File Offset: 0x000074DC
		internal static bool IsCatchableException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is ThreadInterruptedException) && !(e is NullReferenceException) && !(e is AccessViolationException);
		}

		// Token: 0x040004C1 RID: 1217
		private string res;

		// Token: 0x040004C2 RID: 1218
		private string[] args;

		// Token: 0x040004C3 RID: 1219
		private int lineNumber;

		// Token: 0x040004C4 RID: 1220
		private int linePosition;

		// Token: 0x040004C5 RID: 1221
		[OptionalField]
		private string sourceUri;

		// Token: 0x040004C6 RID: 1222
		private string message;
	}
}
