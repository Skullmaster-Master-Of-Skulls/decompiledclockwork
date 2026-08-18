using System;
using System.Data;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public sealed class OraclePermission : DBDataPermission
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x0003C3E8 File Offset: 0x0003B3E8
		private OraclePermission(OraclePermission permission) : base(permission)
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0003C3F1 File Offset: 0x0003B3F1
		internal OraclePermission(OraclePermissionAttribute attrib) : base(attrib)
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0003C3FA File Offset: 0x0003B3FA
		public OraclePermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0003C403 File Offset: 0x0003B403
		public sealed override IPermission Copy()
		{
			return new OraclePermission(this);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0003C40C File Offset: 0x0003B40C
		public override bool IsSubsetOf(IPermission target)
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OraclePermission::IsSubsetOf()\n"
				});
			}
			bool result;
			if (target is OraclePermission)
			{
				OraclePermission oraclePermission = target as OraclePermission;
				bool allowBlankPassword = base.AllowBlankPassword;
				bool allowBlankPassword2 = oraclePermission.AllowBlankPassword;
				base.AllowBlankPassword = false;
				oraclePermission.AllowBlankPassword = false;
				result = base.IsSubsetOf(target);
				base.AllowBlankPassword = allowBlankPassword;
				oraclePermission.AllowBlankPassword = allowBlankPassword2;
			}
			else
			{
				result = base.IsSubsetOf(target);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OraclePermission::IsSubsetOf()\n"
				});
			}
			return result;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0003C4B3 File Offset: 0x0003B4B3
		internal new void Clear()
		{
			base.Clear();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0003C4BC File Offset: 0x0003B4BC
		private string EliminatePasswordValue(string conString)
		{
			string value = "password";
			string value2 = "proxy password";
			string[] array = conString.Split(new char[]
			{
				';'
			});
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				string text2 = text.ToLower();
				if (text2.IndexOf(value) == -1 && text2.IndexOf(value2) == -1)
				{
					stringBuilder.Append(text);
				}
				else
				{
					string[] array2 = text.Split(new char[]
					{
						'='
					});
					string text3 = array2[0].Trim();
					string text4 = text3.ToLower();
					if (text4.Equals(value) || text4.Equals(value2))
					{
						stringBuilder.Append(text3);
						stringBuilder.Append("=");
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				if (i < array.Length - 1)
				{
					stringBuilder.Append(";");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0003C5BC File Offset: 0x0003B5BC
		public override void Add(string connStr, string keyRestrict, KeyRestrictionBehavior behavior)
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OraclePermission::Add()\n"
				});
			}
			string connectionString = connStr;
			if (connStr != null && connStr.Length != 0 && connStr.ToLower().IndexOf("password") != -1)
			{
				connectionString = this.EliminatePasswordValue(connStr);
			}
			base.Add(connectionString, keyRestrict, behavior);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OraclePermission::Add()\n"
				});
			}
		}
	}
}
