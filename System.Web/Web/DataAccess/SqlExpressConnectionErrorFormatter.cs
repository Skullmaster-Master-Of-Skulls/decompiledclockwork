using System;

namespace System.Web.DataAccess
{
	// Token: 0x02000278 RID: 632
	internal sealed class SqlExpressConnectionErrorFormatter : DataConnectionErrorFormatter
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x0008EA69 File Offset: 0x0008DA69
		internal SqlExpressConnectionErrorFormatter(DataConnectionErrorEnum error)
		{
			this._UserName = (HttpRuntime.HasUnmanagedPermission() ? DataConnectionHelper.GetCurrentName() : string.Empty);
			this._Error = error;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0008EA91 File Offset: 0x0008DA91
		internal SqlExpressConnectionErrorFormatter(string userName, DataConnectionErrorEnum error)
		{
			this._UserName = userName;
			this._Error = error;
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x0008EAA8 File Offset: 0x0008DAA8
		protected override string ErrorTitle
		{
			get
			{
				string name = null;
				switch (this._Error)
				{
				case DataConnectionErrorEnum.CanNotCreateDataDir:
					name = "DataAccessError_CanNotCreateDataDir_Title";
					break;
				case DataConnectionErrorEnum.CanNotWriteToDataDir:
					name = "SqlExpressError_CanNotWriteToDataDir_Title";
					break;
				case DataConnectionErrorEnum.CanNotWriteToDBFile:
					name = "SqlExpressError_CanNotWriteToDbfFile_Title";
					break;
				}
				return SR.GetString(name);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0008EAF0 File Offset: 0x0008DAF0
		protected override string Description
		{
			get
			{
				string name = null;
				string name2 = null;
				switch (this._Error)
				{
				case DataConnectionErrorEnum.CanNotCreateDataDir:
					name = "DataAccessError_CanNotCreateDataDir_Description";
					name2 = "DataAccessError_CanNotCreateDataDir_Description_2";
					break;
				case DataConnectionErrorEnum.CanNotWriteToDataDir:
					name = "SqlExpressError_CanNotWriteToDataDir_Description";
					name2 = "SqlExpressError_CanNotWriteToDataDir_Description_2";
					break;
				case DataConnectionErrorEnum.CanNotWriteToDBFile:
					name = "SqlExpressError_CanNotWriteToDbfFile_Description";
					name2 = "SqlExpressError_CanNotWriteToDbfFile_Description_2";
					break;
				}
				string @string;
				if (!string.IsNullOrEmpty(this._UserName))
				{
					@string = SR.GetString(name, new object[]
					{
						this._UserName
					});
				}
				else
				{
					@string = SR.GetString(name2);
				}
				return @string + " " + SR.GetString("SqlExpressError_Description_1");
			}
		}
	}
}
