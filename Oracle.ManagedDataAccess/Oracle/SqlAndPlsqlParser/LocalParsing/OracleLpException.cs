using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B4 RID: 692
	[Serializable]
	public class OracleLpException : ApplicationException, ISerializable
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00109998 File Offset: 0x00107B98
		public OracleLpExceptionType Type
		{
			get
			{
				return this.m_vType;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x001099A0 File Offset: 0x00107BA0
		public OracleLpExceptionError Error
		{
			get
			{
				return this.m_vError;
			}
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x001099A8 File Offset: 0x00107BA8
		public OracleLpException(OracleLpExceptionType type, OracleLpExceptionError error)
		{
			this.m_vType = type;
			this.m_vError = error;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x001099C0 File Offset: 0x00107BC0
		public OracleLpException(OracleLpExceptionType type, OracleLpExceptionError error, string message) : base(message)
		{
			this.m_vType = type;
			this.m_vError = error;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x001099D8 File Offset: 0x00107BD8
		public OracleLpException(OracleLpExceptionType type, OracleLpExceptionError error, string message, Exception innerException) : base(message, innerException)
		{
			this.m_vType = type;
			this.m_vError = error;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x001099F4 File Offset: 0x00107BF4
		protected OracleLpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			uint @uint = info.GetUInt32("Type");
			this.m_vType = (OracleLpExceptionType)@uint;
			@uint = info.GetUInt32("Error");
			this.m_vError = (OracleLpExceptionError)@uint;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00109A30 File Offset: 0x00107C30
		public override string ToString()
		{
			return string.Format("Type: {0}\nError: {1}\nMessage: {2}", this.m_vType.ToString(), this.m_vError.ToString(), base.ToString());
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00109A64 File Offset: 0x00107C64
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Type", (uint)this.m_vType);
			info.AddValue("Error", (uint)this.m_vError);
		}

		// Token: 0x04001C52 RID: 7250
		protected OracleLpExceptionType m_vType;

		// Token: 0x04001C53 RID: 7251
		protected OracleLpExceptionError m_vError;
	}
}
