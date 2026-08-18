using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000315 RID: 789
	internal sealed class SQLMessage
	{
		// Token: 0x060029A2 RID: 10658 RVA: 0x002B5208 File Offset: 0x002B4608
		private SQLMessage()
		{
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x002B5228 File Offset: 0x002B4628
		internal static string CultureIdError()
		{
			return Res.GetString("SQL_CultureIdError");
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x002B5248 File Offset: 0x002B4648
		internal static string EncryptionNotSupportedByClient()
		{
			return Res.GetString("SQL_EncryptionNotSupportedByClient");
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x002B5268 File Offset: 0x002B4668
		internal static string EncryptionNotSupportedByServer()
		{
			return Res.GetString("SQL_EncryptionNotSupportedByServer");
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x002B5288 File Offset: 0x002B4688
		internal static string OperationCancelled()
		{
			return Res.GetString("SQL_OperationCancelled");
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x002B52A8 File Offset: 0x002B46A8
		internal static string SevereError()
		{
			return Res.GetString("SQL_SevereError");
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x002B52C8 File Offset: 0x002B46C8
		internal static string SSPIInitializeError()
		{
			return Res.GetString("SQL_SSPIInitializeError");
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x002B52E8 File Offset: 0x002B46E8
		internal static string SSPIGenerateError()
		{
			return Res.GetString("SQL_SSPIGenerateError");
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x002B5308 File Offset: 0x002B4708
		internal static string Timeout()
		{
			return Res.GetString("SQL_Timeout");
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x002B5328 File Offset: 0x002B4728
		internal static string UserInstanceFailure()
		{
			return Res.GetString("SQL_UserInstanceFailure");
		}
	}
}
