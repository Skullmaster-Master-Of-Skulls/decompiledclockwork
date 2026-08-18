using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.XPath
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public class XPathException : SystemException
	{
		// Token: 0x060010B3 RID: 4275 RVA: 0x0004BF64 File Offset: 0x0004AF64
		protected XPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
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
				this.message = XPathException.CreateMessage(this.res, this.args);
				return;
			}
			this.message = null;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0004C015 File Offset: 0x0004B015
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("version", "2.0");
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0004C051 File Offset: 0x0004B051
		public XPathException() : this(string.Empty, null)
		{
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004C05F File Offset: 0x0004B05F
		public XPathException(string message) : this(message, null)
		{
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004C06C File Offset: 0x0004B06C
		public XPathException(string message, Exception innerException) : this("Xml_UserException", new string[]
		{
			message
		}, innerException)
		{
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004C091 File Offset: 0x0004B091
		internal static XPathException Create(string res)
		{
			return new XPathException(res, null);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0004C09C File Offset: 0x0004B09C
		internal static XPathException Create(string res, string arg)
		{
			return new XPathException(res, new string[]
			{
				arg
			});
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0004C0BC File Offset: 0x0004B0BC
		internal static XPathException Create(string res, string arg, string arg2)
		{
			return new XPathException(res, new string[]
			{
				arg,
				arg2
			});
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004C0E0 File Offset: 0x0004B0E0
		internal static XPathException Create(string res, string arg, Exception innerException)
		{
			return new XPathException(res, new string[]
			{
				arg
			}, innerException);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004C100 File Offset: 0x0004B100
		private XPathException(string res, string[] args) : this(res, args, null)
		{
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0004C10B File Offset: 0x0004B10B
		private XPathException(string res, string[] args, Exception inner) : base(XPathException.CreateMessage(res, args), inner)
		{
			base.HResult = -2146231997;
			this.res = res;
			this.args = args;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0004C134 File Offset: 0x0004B134
		private static string CreateMessage(string res, string[] args)
		{
			string result;
			try
			{
				string text = Res.GetString(res, args);
				if (text == null)
				{
					text = "UNKNOWN(" + res + ")";
				}
				result = text;
			}
			catch (MissingManifestResourceException)
			{
				result = "UNKNOWN(" + res + ")";
			}
			return result;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0004C188 File Offset: 0x0004B188
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

		// Token: 0x04000AD7 RID: 2775
		private string res;

		// Token: 0x04000AD8 RID: 2776
		private string[] args;

		// Token: 0x04000AD9 RID: 2777
		private string message;
	}
}
