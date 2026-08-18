using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace System.ServiceModel
{
	// Token: 0x020000F2 RID: 242
	[DataContract]
	[__DynamicallyInvokable]
	public class ExceptionDetail
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x00018024 File Offset: 0x00016224
		[__DynamicallyInvokable]
		public ExceptionDetail(Exception exception)
		{
			if (exception == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exception");
			}
			this.helpLink = exception.HelpLink;
			this.message = exception.Message;
			this.stackTrace = exception.StackTrace;
			this.type = exception.GetType().ToString();
			if (exception.InnerException != null)
			{
				this.innerException = new ExceptionDetail(exception.InnerException);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00018098 File Offset: 0x00016298
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x000180A0 File Offset: 0x000162A0
		[DataMember]
		[__DynamicallyInvokable]
		public string HelpLink
		{
			[__DynamicallyInvokable]
			get
			{
				return this.helpLink;
			}
			[__DynamicallyInvokable]
			set
			{
				this.helpLink = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000180A9 File Offset: 0x000162A9
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x000180B1 File Offset: 0x000162B1
		[DataMember]
		[__DynamicallyInvokable]
		public ExceptionDetail InnerException
		{
			[__DynamicallyInvokable]
			get
			{
				return this.innerException;
			}
			[__DynamicallyInvokable]
			set
			{
				this.innerException = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000180BA File Offset: 0x000162BA
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x000180C2 File Offset: 0x000162C2
		[DataMember]
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get
			{
				return this.message;
			}
			[__DynamicallyInvokable]
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000180CB File Offset: 0x000162CB
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x000180D3 File Offset: 0x000162D3
		[DataMember]
		[__DynamicallyInvokable]
		public string StackTrace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.stackTrace;
			}
			[__DynamicallyInvokable]
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000180DC File Offset: 0x000162DC
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x000180E4 File Offset: 0x000162E4
		[DataMember]
		[__DynamicallyInvokable]
		public string Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this.type;
			}
			[__DynamicallyInvokable]
			set
			{
				this.type = value;
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000180ED File Offset: 0x000162ED
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}\n{1}", new object[]
			{
				SR.GetString("SFxExceptionDetailFormat"),
				this.ToStringHelper(false)
			});
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001811C File Offset: 0x0001631C
		private string ToStringHelper(bool isInner)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}: {1}", this.Type, this.Message);
			if (this.InnerException != null)
			{
				stringBuilder.AppendFormat(" ----> {0}", this.InnerException.ToStringHelper(true));
			}
			else
			{
				stringBuilder.Append("\n");
			}
			stringBuilder.Append(this.StackTrace);
			if (isInner)
			{
				stringBuilder.AppendFormat("\n   {0}\n", SR.GetString("SFxExceptionDetailEndOfInner"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000A2E RID: 2606
		private string helpLink;

		// Token: 0x04000A2F RID: 2607
		private ExceptionDetail innerException;

		// Token: 0x04000A30 RID: 2608
		private string message;

		// Token: 0x04000A31 RID: 2609
		private string stackTrace;

		// Token: 0x04000A32 RID: 2610
		private string type;
	}
}
