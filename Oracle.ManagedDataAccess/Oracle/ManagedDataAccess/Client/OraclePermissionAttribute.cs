using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200007A RID: 122
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OraclePermissionAttribute : DBDataPermissionAttribute
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x000390AC File Offset: 0x000372AC
		public OraclePermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000390B8 File Offset: 0x000372B8
		public override IPermission CreatePermission()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
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
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}
	}
}
