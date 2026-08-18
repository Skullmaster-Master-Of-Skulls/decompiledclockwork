using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200026A RID: 618
	[ComVisible(true)]
	[Serializable]
	public class ParserException : ApplicationException, ISerializable
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x001042A4 File Offset: 0x001024A4
		public ParserExceptionType Type
		{
			get
			{
				return this.m_vType;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x001042AC File Offset: 0x001024AC
		public ParserExceptionError Error
		{
			get
			{
				return this.m_vError;
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x001042B4 File Offset: 0x001024B4
		public ParserException(ParserExceptionType type, ParserExceptionError error)
		{
			this.m_vType = type;
			this.m_vError = error;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x001042CC File Offset: 0x001024CC
		public ParserException(ParserExceptionType type, ParserExceptionError error, string message) : base(message)
		{
			this.m_vType = type;
			this.m_vError = error;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x001042E4 File Offset: 0x001024E4
		protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			uint @uint = info.GetUInt32("Type");
			this.m_vType = (ParserExceptionType)@uint;
			@uint = info.GetUInt32("Error");
			this.m_vError = (ParserExceptionError)@uint;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00104320 File Offset: 0x00102520
		public ParserException(ParserExceptionType type, ParserExceptionError error, string message, Exception innerException) : base(message, innerException)
		{
			this.m_vType = type;
			this.m_vError = error;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0010433C File Offset: 0x0010253C
		public override string ToString()
		{
			return string.Format("Type: {0}\nError: {1}\nMessage: {2}", this.m_vType.ToString(), this.m_vError.ToString(), base.ToString());
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00104370 File Offset: 0x00102570
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Type", (uint)this.m_vType);
			info.AddValue("Error", (uint)this.m_vError);
		}

		// Token: 0x04001B18 RID: 6936
		protected ParserExceptionType m_vType;

		// Token: 0x04001B19 RID: 6937
		protected ParserExceptionError m_vError;
	}
}
