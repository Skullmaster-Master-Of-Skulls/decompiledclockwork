using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.IO.Compression
{
	// Token: 0x02000425 RID: 1061
	[Serializable]
	internal class ZLibException : IOException, ISerializable
	{
		// Token: 0x060027B3 RID: 10163 RVA: 0x000B6B65 File Offset: 0x000B4D65
		public ZLibException(string message, string zlibErrorContext, int zlibErrorCode, string zlibErrorMessage) : base(message)
		{
			this.Init(zlibErrorContext, (ZLibNative.ErrorCode)zlibErrorCode, zlibErrorMessage);
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000B6B78 File Offset: 0x000B4D78
		public ZLibException()
		{
			this.Init();
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000B6B86 File Offset: 0x000B4D86
		public ZLibException(string message) : base(message)
		{
			this.Init();
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000B6B95 File Offset: 0x000B4D95
		public ZLibException(string message, Exception inner) : base(message, inner)
		{
			this.Init();
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000B6BA8 File Offset: 0x000B4DA8
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected ZLibException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			string @string = info.GetString("zlibErrorContext");
			ZLibNative.ErrorCode @int = (ZLibNative.ErrorCode)info.GetInt32("zlibErrorCode");
			string string2 = info.GetString("zlibErrorMessage");
			this.Init(@string, @int, string2);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000B6BEA File Offset: 0x000B4DEA
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
			si.AddValue("zlibErrorContext", this.zlibErrorContext);
			si.AddValue("zlibErrorCode", (int)this.zlibErrorCode);
			si.AddValue("zlibErrorMessage", this.zlibErrorMessage);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000B6C27 File Offset: 0x000B4E27
		private void Init()
		{
			this.Init("", ZLibNative.ErrorCode.Ok, "");
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000B6C3A File Offset: 0x000B4E3A
		private void Init(string zlibErrorContext, ZLibNative.ErrorCode zlibErrorCode, string zlibErrorMessage)
		{
			this.zlibErrorContext = zlibErrorContext;
			this.zlibErrorCode = zlibErrorCode;
			this.zlibErrorMessage = zlibErrorMessage;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x000B6C51 File Offset: 0x000B4E51
		public string ZLibContext
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.zlibErrorContext;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x000B6C59 File Offset: 0x000B4E59
		public int ZLibErrorCode
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return (int)this.zlibErrorCode;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x000B6C61 File Offset: 0x000B4E61
		public string ZLibErrorMessage
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.zlibErrorMessage;
			}
		}

		// Token: 0x04002192 RID: 8594
		private string zlibErrorContext;

		// Token: 0x04002193 RID: 8595
		private string zlibErrorMessage;

		// Token: 0x04002194 RID: 8596
		private ZLibNative.ErrorCode zlibErrorCode;
	}
}
