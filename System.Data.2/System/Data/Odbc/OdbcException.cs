using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x0200029D RID: 669
	[Serializable]
	public sealed class OdbcException : DbException
	{
		// Token: 0x060028E0 RID: 10464 RVA: 0x00110B70 File Offset: 0x0010FF70
		internal static OdbcException CreateException(OdbcErrorCollection errors, ODBC32.RetCode retcode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in errors)
			{
				OdbcError odbcError = (OdbcError)obj;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(Res.GetString("Odbc_ExceptionMessage", new object[]
				{
					ODBC32.RetcodeToString(retcode),
					odbcError.SQLState,
					odbcError.Message
				}));
			}
			return new OdbcException(stringBuilder.ToString(), errors);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x00110C28 File Offset: 0x00110028
		internal OdbcException(string message, OdbcErrorCollection errors) : base(message)
		{
			this.odbcErrors = errors;
			base.HResult = -2146232009;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x00110C5C File Offset: 0x0011005C
		private OdbcException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
			this._retcode = (ODBC32.RETCODE)si.GetValue("odbcRetcode", typeof(ODBC32.RETCODE));
			this.odbcErrors = (OdbcErrorCollection)si.GetValue("odbcErrors", typeof(OdbcErrorCollection));
			base.HResult = -2146232009;
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x00110CC8 File Offset: 0x001100C8
		public OdbcErrorCollection Errors
		{
			get
			{
				return this.odbcErrors;
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x00110CDC File Offset: 0x001100DC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			si.AddValue("odbcRetcode", this._retcode, typeof(ODBC32.RETCODE));
			si.AddValue("odbcErrors", this.odbcErrors, typeof(OdbcErrorCollection));
			base.GetObjectData(si, context);
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x00110D3C File Offset: 0x0011013C
		public override string Source
		{
			get
			{
				if (0 >= this.Errors.Count)
				{
					return "";
				}
				string source = this.Errors[0].Source;
				if (!ADP.IsEmpty(source))
				{
					return source;
				}
				return "";
			}
		}

		// Token: 0x04001AA8 RID: 6824
		private OdbcErrorCollection odbcErrors = new OdbcErrorCollection();

		// Token: 0x04001AA9 RID: 6825
		private ODBC32.RETCODE _retcode;
	}
}
