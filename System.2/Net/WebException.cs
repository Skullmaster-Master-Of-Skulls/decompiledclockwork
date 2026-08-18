using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x0200017D RID: 381
	[__DynamicallyInvokable]
	[Serializable]
	public class WebException : InvalidOperationException, ISerializable
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x00049B2A File Offset: 0x00047D2A
		[__DynamicallyInvokable]
		public WebException()
		{
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00049B3A File Offset: 0x00047D3A
		[__DynamicallyInvokable]
		public WebException(string message) : this(message, null)
		{
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00049B44 File Offset: 0x00047D44
		[__DynamicallyInvokable]
		public WebException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00049B56 File Offset: 0x00047D56
		[__DynamicallyInvokable]
		public WebException(string message, WebExceptionStatus status) : this(message, null, status, null)
		{
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00049B62 File Offset: 0x00047D62
		internal WebException(string message, WebExceptionStatus status, WebExceptionInternalStatus internalStatus, Exception innerException) : this(message, innerException, status, null, internalStatus)
		{
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00049B70 File Offset: 0x00047D70
		[__DynamicallyInvokable]
		public WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response) : this(message, null, innerException, status, response)
		{
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00049B80 File Offset: 0x00047D80
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response) : base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00049BCC File Offset: 0x00047DCC
		internal WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus) : this(message, null, innerException, status, response, internalStatus)
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00049BDC File Offset: 0x00047DDC
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus) : base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
			this.m_InternalStatus = internalStatus;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00049C30 File Offset: 0x00047E30
		protected WebException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00049C42 File Offset: 0x00047E42
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00049C4C File Offset: 0x00047E4C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00049C56 File Offset: 0x00047E56
		[__DynamicallyInvokable]
		public WebExceptionStatus Status
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Status;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00049C5E File Offset: 0x00047E5E
		[__DynamicallyInvokable]
		public WebResponse Response
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Response;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00049C66 File Offset: 0x00047E66
		internal WebExceptionInternalStatus InternalStatus
		{
			get
			{
				return this.m_InternalStatus;
			}
		}

		// Token: 0x0400122C RID: 4652
		private WebExceptionStatus m_Status = WebExceptionStatus.UnknownError;

		// Token: 0x0400122D RID: 4653
		private WebResponse m_Response;

		// Token: 0x0400122E RID: 4654
		[NonSerialized]
		private WebExceptionInternalStatus m_InternalStatus;
	}
}
