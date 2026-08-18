using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	public sealed class ServerManagerException : Exception, ISerializable
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00007E98 File Offset: 0x00006E98
		public ServerManagerException() : this(string.Empty, 0)
		{
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00007EA6 File Offset: 0x00006EA6
		public ServerManagerException(string errorMessage) : this(errorMessage, 0)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00007EB0 File Offset: 0x00006EB0
		public ServerManagerException(string errorMessage, int errorCode) : this(errorMessage, null, errorCode)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00007EBB File Offset: 0x00006EBB
		public ServerManagerException(string errorMessage, Exception exception) : base(errorMessage, exception)
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00007EC5 File Offset: 0x00006EC5
		public ServerManagerException(string errorMessage, Exception exception, int errorCode) : base(errorMessage, exception)
		{
			this._errorCode = errorCode;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00007ED6 File Offset: 0x00006ED6
		private ServerManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._errorCode = info.GetInt32("ErrorCodeKeyName");
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00007EF1 File Offset: 0x00006EF1
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00007EF9 File Offset: 0x00006EF9
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00007F01 File Offset: 0x00006F01
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ErrorCodeKeyName", this._errorCode);
		}

		// Token: 0x04000116 RID: 278
		private const string ErrorCodeKeyName = "ErrorCodeKeyName";

		// Token: 0x04000117 RID: 279
		private int _errorCode;
	}
}
