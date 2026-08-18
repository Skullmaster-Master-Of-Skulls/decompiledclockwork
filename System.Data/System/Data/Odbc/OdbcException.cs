using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020001ED RID: 493
	[Serializable]
	public sealed class OdbcException : DbException
	{
		// Token: 0x06001B80 RID: 7040 RVA: 0x00263918 File Offset: 0x00262D18
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

		// Token: 0x06001B81 RID: 7041 RVA: 0x002639D8 File Offset: 0x00262DD8
		internal OdbcException(string message, OdbcErrorCollection errors) : base(message)
		{
			this.odbcErrors = errors;
			base.HResult = -2146232009;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00263A18 File Offset: 0x00262E18
		private OdbcException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
			this._retcode = (ODBC32.RETCODE)si.GetValue("odbcRetcode", typeof(ODBC32.RETCODE));
			this.odbcErrors = (OdbcErrorCollection)si.GetValue("odbcErrors", typeof(OdbcErrorCollection));
			base.HResult = -2146232009;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00263A88 File Offset: 0x00262E88
		public OdbcErrorCollection Errors
		{
			get
			{
				return this.odbcErrors;
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00263AA8 File Offset: 0x00262EA8
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

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x00263B08 File Offset: 0x00262F08
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

		// Token: 0x0400101C RID: 4124
		private OdbcErrorCollection odbcErrors = new OdbcErrorCollection();

		// Token: 0x0400101D RID: 4125
		private ODBC32.RETCODE _retcode;
	}
}
