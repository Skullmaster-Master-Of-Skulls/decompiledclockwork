using System;
using System.Data;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;
using System.Text;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public sealed class OraclePermission : DBDataPermission
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00038C74 File Offset: 0x00036E74
		private OraclePermission(OraclePermission permission) : base(permission)
		{
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00038C80 File Offset: 0x00036E80
		internal OraclePermission(OraclePermissionAttribute attrib) : base(attrib)
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00038C8C File Offset: 0x00036E8C
		public OraclePermission(PermissionState state) : base(state)
		{
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00038C98 File Offset: 0x00036E98
		public sealed override IPermission Copy()
		{
			if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			IPermission result;
			try
			{
				result = new OraclePermission(this);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00038D1C File Offset: 0x00036F1C
		public override bool IsSubsetOf(IPermission target)
		{
			if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				bool flag;
				if (target is OraclePermission)
				{
					OraclePermission oraclePermission = target as OraclePermission;
					bool allowBlankPassword = base.AllowBlankPassword;
					bool allowBlankPassword2 = oraclePermission.AllowBlankPassword;
					base.AllowBlankPassword = false;
					oraclePermission.AllowBlankPassword = false;
					flag = base.IsSubsetOf(target);
					base.AllowBlankPassword = allowBlankPassword;
					oraclePermission.AllowBlankPassword = allowBlankPassword2;
				}
				else
				{
					flag = base.IsSubsetOf(target);
				}
				result = flag;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00038DEC File Offset: 0x00036FEC
		internal new void Clear()
		{
			if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				base.Clear();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00038E70 File Offset: 0x00037070
		private string EliminatePasswordValue(string conString)
		{
			if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
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
					string text2 = text.ToLowerInvariant();
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
						string text4 = text3.ToLowerInvariant();
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
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00038FF8 File Offset: 0x000371F8
		public override void Add(string connStr, string keyRestrict, KeyRestrictionBehavior behavior)
		{
			if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				string connectionString = connStr;
				if (connStr != null && connStr.Length != 0 && connStr.ToLowerInvariant().IndexOf("password") != -1)
				{
					connectionString = this.EliminatePasswordValue(connStr);
				}
				base.Add(connectionString, keyRestrict, behavior);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (OraclePermission.m_startTracing && ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x040006CD RID: 1741
		internal static bool m_startTracing;
	}
}
