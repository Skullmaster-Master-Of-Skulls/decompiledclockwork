using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x0200049E RID: 1182
	[Serializable]
	public class WebException : InvalidOperationException, ISerializable
	{
		// Token: 0x06002415 RID: 9237 RVA: 0x0008D3A0 File Offset: 0x0008C3A0
		public WebException()
		{
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x0008D3B0 File Offset: 0x0008C3B0
		public WebException(string message) : this(message, null)
		{
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x0008D3BA File Offset: 0x0008C3BA
		public WebException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x0008D3CC File Offset: 0x0008C3CC
		public WebException(string message, WebExceptionStatus status) : this(message, null, status, null)
		{
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x0008D3D8 File Offset: 0x0008C3D8
		internal WebException(string message, WebExceptionStatus status, WebExceptionInternalStatus internalStatus, Exception innerException) : this(message, innerException, status, null, internalStatus)
		{
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x0008D3E6 File Offset: 0x0008C3E6
		public WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response) : this(message, null, innerException, status, response)
		{
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x0008D3F4 File Offset: 0x0008C3F4
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response) : base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x0008D440 File Offset: 0x0008C440
		internal WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus) : this(message, null, innerException, status, response, internalStatus)
		{
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x0008D450 File Offset: 0x0008C450
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus) : base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
			this.m_InternalStatus = internalStatus;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x0008D4A4 File Offset: 0x0008C4A4
		protected WebException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0008D4B6 File Offset: 0x0008C4B6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x0008D4C0 File Offset: 0x0008C4C0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x0008D4CA File Offset: 0x0008C4CA
		public WebExceptionStatus Status
		{
			get
			{
				return this.m_Status;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x0008D4D2 File Offset: 0x0008C4D2
		public WebResponse Response
		{
			get
			{
				return this.m_Response;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x0008D4DA File Offset: 0x0008C4DA
		internal WebExceptionInternalStatus InternalStatus
		{
			get
			{
				return this.m_InternalStatus;
			}
		}

		// Token: 0x04002473 RID: 9331
		private WebExceptionStatus m_Status = WebExceptionStatus.UnknownError;

		// Token: 0x04002474 RID: 9332
		private WebResponse m_Response;

		// Token: 0x04002475 RID: 9333
		[NonSerialized]
		private WebExceptionInternalStatus m_InternalStatus;
	}
}
