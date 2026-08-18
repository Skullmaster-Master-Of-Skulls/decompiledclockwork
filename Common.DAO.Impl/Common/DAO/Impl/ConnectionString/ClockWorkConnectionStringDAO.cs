using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DAO.ConnectionString;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ConnectionString;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.DAO.Impl.ConnectionString
{
	// Token: 0x02000108 RID: 264
	public class ClockWorkConnectionStringDAO : IClockWorkConnectionStringDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0004DD46 File Offset: 0x0004BF46
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0004DD4E File Offset: 0x0004BF4E
		private RegistryHelper RegHelper { get; set; }

		// Token: 0x06000792 RID: 1938 RVA: 0x0004DD57 File Offset: 0x0004BF57
		public ClockWorkConnectionStringDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.RegHelper = new RegistryHelper();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0004DD78 File Offset: 0x0004BF78
		public string CreateConnectionString(ClockWorkConnectionString ccs)
		{
			string text = ccs.ToString();
			this.RegHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, text, new string[]
			{
				"Connections",
				"ConnectionStrings",
				ccs.Name
			});
			return text;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0004DDC4 File Offset: 0x0004BFC4
		public void UpdateConnectionString(ClockWorkConnectionString ccs)
		{
			string value = ccs.ToString();
			this.RegHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, value, new string[]
			{
				"Connections",
				"ConnectionStrings",
				ccs.Name
			});
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0004DE0A File Offset: 0x0004C00A
		public void DeleteClockWorkConnectionString(string ccsName)
		{
			this.RegHelper.DeleteLocalMachineRegistry(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"Connections",
				"ConnectionStrings",
				ccsName
			});
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0004DE38 File Offset: 0x0004C038
		public bool ConnectionNameAlreadyExists(string ccsName)
		{
			return this.RegHelper.GetLocalMachineSubKeyValueNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"Connections",
				"ConnectionStrings"
			}).Any((string s) => s.Equals(ccsName, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0004DE8E File Offset: 0x0004C08E
		public void AssignConnectionString(string appId, string ccsName)
		{
			this.RegHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, ccsName, new string[]
			{
				"Connections",
				"ApplicationConnections",
				appId
			});
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0004DEC0 File Offset: 0x0004C0C0
		public ClockWorkConnectionString GetConnectionString(string appId)
		{
			string text = this.RegHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"Connections",
				"ApplicationConnections",
				appId
			});
			bool flag = string.IsNullOrEmpty(text);
			ClockWorkConnectionString result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text2 = this.RegHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"Connections",
					"ConnectionStrings",
					text
				});
				result = (string.IsNullOrEmpty(text2) ? null : new ClockWorkConnectionString(text2));
			}
			return result;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0004DF47 File Offset: 0x0004C147
		public void RemoveAssignedClockWorkConnectionString(string appId)
		{
			this.RegHelper.DeleteLocalMachineRegistry(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"Connections",
				"ApplicationConnections",
				appId
			});
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0004DF78 File Offset: 0x0004C178
		public IList<ClockWorkConnectionString> GetConnectionStringList()
		{
			return (from csName in this.RegHelper.GetLocalMachineSubKeyValueNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"Connections",
				"ConnectionStrings"
			})
			select this.RegHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"Connections",
				"ConnectionStrings",
				csName
			}) into cs
			where !string.IsNullOrEmpty(cs)
			select new ClockWorkConnectionString(cs)).ToList<ClockWorkConnectionString>();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0004E010 File Offset: 0x0004C210
		public IList<ClockWorkApplicationConnectionString> GetAssignedConnectionStringList()
		{
			string[] localMachineSubKeyValueNames = this.RegHelper.GetLocalMachineSubKeyValueNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"Connections",
				"ApplicationConnections"
			});
			return (from appId in localMachineSubKeyValueNames
			let conn = this.GetConnectionString(appId)
			where conn != null
			select new ClockWorkApplicationConnectionString
			{
				ApplicationId = appId,
				ConnectionString = conn
			}).ToList<ClockWorkApplicationConnectionString>();
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0004E0A8 File Offset: 0x0004C2A8
		public IList<ClockWorkApplicationConnectionString> GetAssignedConnectionStringList(eTechnoProProductNames productName)
		{
			string[] localMachineSubKeyValueNames = this.RegHelper.GetLocalMachineSubKeyValueNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"Connections",
				"ApplicationConnections"
			});
			return (from s in localMachineSubKeyValueNames
			where s.StartsWith(string.Format("{0}.", productName.ToString()))
			select s into appId
			let conn = this.GetConnectionString(appId)
			where conn != null
			select new ClockWorkApplicationConnectionString
			{
				ApplicationId = appId,
				ConnectionString = conn
			}).ToList<ClockWorkApplicationConnectionString>();
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0004E165 File Offset: 0x0004C365
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0004E16D File Offset: 0x0004C36D
		public OperationContext OpContext { get; set; }
	}
}
