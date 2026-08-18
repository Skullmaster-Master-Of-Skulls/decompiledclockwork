using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x0200018B RID: 395
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class WebResponse : MarshalByRefObject, ISerializable, IDisposable
	{
		// Token: 0x06000F24 RID: 3876 RVA: 0x0004E8A5 File Offset: 0x0004CAA5
		[__DynamicallyInvokable]
		protected WebResponse()
		{
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0004E8AD File Offset: 0x0004CAAD
		protected WebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0004E8B5 File Offset: 0x0004CAB5
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0004E8BF File Offset: 0x0004CABF
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0004E8C1 File Offset: 0x0004CAC1
		public virtual void Close()
		{
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0004E8C3 File Offset: 0x0004CAC3
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0004E8D4 File Offset: 0x0004CAD4
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			try
			{
				this.Close();
			}
			catch
			{
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0004E904 File Offset: 0x0004CB04
		public virtual bool IsFromCache
		{
			get
			{
				return this.m_IsFromCache;
			}
		}

		// Token: 0x1700035F RID: 863
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x0004E90C File Offset: 0x0004CB0C
		internal bool InternalSetFromCache
		{
			set
			{
				this.m_IsFromCache = value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0004E915 File Offset: 0x0004CB15
		internal virtual bool IsCacheFresh
		{
			get
			{
				return this.m_IsCacheFresh;
			}
		}

		// Token: 0x17000361 RID: 865
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x0004E91D File Offset: 0x0004CB1D
		internal bool InternalSetIsCacheFresh
		{
			set
			{
				this.m_IsCacheFresh = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0004E926 File Offset: 0x0004CB26
		public virtual bool IsMutuallyAuthenticated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0004E929 File Offset: 0x0004CB29
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x0004E930 File Offset: 0x0004CB30
		[__DynamicallyInvokable]
		public virtual long ContentLength
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0004E937 File Offset: 0x0004CB37
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0004E93E File Offset: 0x0004CB3E
		[__DynamicallyInvokable]
		public virtual string ContentType
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0004E945 File Offset: 0x0004CB45
		[__DynamicallyInvokable]
		public virtual Stream GetResponseStream()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0004E94C File Offset: 0x0004CB4C
		[__DynamicallyInvokable]
		public virtual Uri ResponseUri
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0004E953 File Offset: 0x0004CB53
		[__DynamicallyInvokable]
		public virtual WebHeaderCollection Headers
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0004E95A File Offset: 0x0004CB5A
		[__DynamicallyInvokable]
		public virtual bool SupportsHeaders
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x040012A4 RID: 4772
		private bool m_IsCacheFresh;

		// Token: 0x040012A5 RID: 4773
		private bool m_IsFromCache;
	}
}
