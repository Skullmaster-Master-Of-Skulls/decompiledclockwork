using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.XPath
{
	// Token: 0x020002E2 RID: 738
	[Serializable]
	public class XPathException : SystemException
	{
		// Token: 0x06002C34 RID: 11316 RVA: 0x000E8F14 File Offset: 0x000E7114
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

		// Token: 0x06002C35 RID: 11317 RVA: 0x000E8FC5 File Offset: 0x000E71C5
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("version", "2.0");
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000E9001 File Offset: 0x000E7201
		public XPathException() : this(string.Empty, null)
		{
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000E900F File Offset: 0x000E720F
		public XPathException(string message) : this(message, null)
		{
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000E9019 File Offset: 0x000E7219
		public XPathException(string message, Exception innerException) : this("Xml_UserException", new string[]
		{
			message
		}, innerException)
		{
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000E9031 File Offset: 0x000E7231
		internal static XPathException Create(string res)
		{
			return new XPathException(res, null);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000E903A File Offset: 0x000E723A
		internal static XPathException Create(string res, string arg)
		{
			return new XPathException(res, new string[]
			{
				arg
			});
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000E904C File Offset: 0x000E724C
		internal static XPathException Create(string res, string arg, string arg2)
		{
			return new XPathException(res, new string[]
			{
				arg,
				arg2
			});
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000E9062 File Offset: 0x000E7262
		internal static XPathException Create(string res, string arg, Exception innerException)
		{
			return new XPathException(res, new string[]
			{
				arg
			}, innerException);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000E9075 File Offset: 0x000E7275
		private XPathException(string res, string[] args) : this(res, args, null)
		{
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000E9080 File Offset: 0x000E7280
		private XPathException(string res, string[] args, Exception inner) : base(XPathException.CreateMessage(res, args), inner)
		{
			base.HResult = -2146231997;
			this.res = res;
			this.args = args;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000E90AC File Offset: 0x000E72AC
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

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000E9100 File Offset: 0x000E7300
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

		// Token: 0x0400134C RID: 4940
		private string res;

		// Token: 0x0400134D RID: 4941
		private string[] args;

		// Token: 0x0400134E RID: 4942
		private string message;
	}
}
