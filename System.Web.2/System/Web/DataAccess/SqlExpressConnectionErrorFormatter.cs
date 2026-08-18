using System;

namespace System.Web.DataAccess
{
	// Token: 0x020001AD RID: 429
	internal sealed class SqlExpressConnectionErrorFormatter : DataConnectionErrorFormatter
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x00046A2D File Offset: 0x00044C2D
		internal SqlExpressConnectionErrorFormatter(DataConnectionErrorEnum error)
		{
			this._UserName = (HttpRuntime.HasUnmanagedPermission() ? DataConnectionHelper.GetCurrentName() : string.Empty);
			this._Error = error;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00046A55 File Offset: 0x00044C55
		internal SqlExpressConnectionErrorFormatter(string userName, DataConnectionErrorEnum error)
		{
			this._UserName = userName;
			this._Error = error;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00046A6C File Offset: 0x00044C6C
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

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00046AB4 File Offset: 0x00044CB4
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
