using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.DAO.ConnectionString;
using TechnoPro.Common.DAO.Impl.ConnectionString;
using TechnoPro.Common.ICore.ConnectionString;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ConnectionString;

namespace TechnoPro.Common.Core.ConnectionString
{
	// Token: 0x02000119 RID: 281
	public class ClockWorkConnectionStringManager : IClockWorkConnectionStringManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00053CB6 File Offset: 0x00051EB6
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x00053CBE File Offset: 0x00051EBE
		private IClockWorkConnectionStringDAO ConnectionStringDAO { get; set; }

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00053CC7 File Offset: 0x00051EC7
		public ClockWorkConnectionStringManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.ConnectionStringDAO = new ClockWorkConnectionStringDAO(opContext);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00053CE8 File Offset: 0x00051EE8
		public string CreateConnectionString(ClockWorkConnectionString ccs)
		{
			return this.ConnectionNameAlreadyExists(ccs.Name) ? null : this.ConnectionStringDAO.CreateConnectionString(ccs);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00053D17 File Offset: 0x00051F17
		public void UpdateConnectionString(ClockWorkConnectionString ccs)
		{
			this.ConnectionStringDAO.UpdateConnectionString(ccs);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00053D28 File Offset: 0x00051F28
		public void DeleteClockWorkConnectionString(string ccsName)
		{
			this.ConnectionStringDAO.DeleteClockWorkConnectionString(ccsName);
			IList<ClockWorkApplicationConnectionString> assignedConnectionStringList = this.GetAssignedConnectionStringList();
			IEnumerable<ClockWorkApplicationConnectionString> source = assignedConnectionStringList;
			Func<ClockWorkApplicationConnectionString, bool> <>9__0;
			Func<ClockWorkApplicationConnectionString, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((ClockWorkApplicationConnectionString conn) => conn.ConnectionString != null && conn.ConnectionString.Name.Equals(ccsName, StringComparison.OrdinalIgnoreCase)));
			}
			foreach (ClockWorkApplicationConnectionString clockWorkApplicationConnectionString in source.Where(predicate))
			{
				this.RemoveAssignedClockWorkConnectionString(clockWorkApplicationConnectionString.ApplicationId);
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00053DC8 File Offset: 0x00051FC8
		public bool ConnectionNameAlreadyExists(string ccsName)
		{
			return this.ConnectionStringDAO.ConnectionNameAlreadyExists(ccsName);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00053DE6 File Offset: 0x00051FE6
		public void AssignConnectionString(string appId, string ccsName)
		{
			this.ConnectionStringDAO.AssignConnectionString(appId, ccsName);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00053DF8 File Offset: 0x00051FF8
		public ClockWorkConnectionString GetConnectionString(string appId)
		{
			return this.ConnectionStringDAO.GetConnectionString(appId);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00053E18 File Offset: 0x00052018
		public void RemoveAssignedClockWorkConnectionString(string appId)
		{
			ClockWorkConnectionString cs = this.GetConnectionString(appId);
			this.ConnectionStringDAO.RemoveAssignedClockWorkConnectionString(appId);
			IList<ClockWorkApplicationConnectionString> assignedConnectionStringList = this.GetAssignedConnectionStringList();
			bool flag = !assignedConnectionStringList.Any((ClockWorkApplicationConnectionString c) => c.ConnectionString != null && c.ConnectionString.Name.Equals(cs.Name, StringComparison.InvariantCultureIgnoreCase));
			if (flag)
			{
				this.DeleteClockWorkConnectionString(cs.Name);
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00053E78 File Offset: 0x00052078
		public IList<ClockWorkConnectionString> GetConnectionStringList()
		{
			return this.ConnectionStringDAO.GetConnectionStringList();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00053E98 File Offset: 0x00052098
		public IList<ClockWorkApplicationConnectionString> GetAssignedConnectionStringList()
		{
			return this.ConnectionStringDAO.GetAssignedConnectionStringList();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00053EB8 File Offset: 0x000520B8
		public IList<ClockWorkApplicationConnectionString> GetAssignedConnectionStringList(eTechnoProProductNames productName)
		{
			return this.ConnectionStringDAO.GetAssignedConnectionStringList(productName);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00053ED8 File Offset: 0x000520D8
		public void ImportFromFile(string filename)
		{
			XElement xelement = XElement.Load(filename);
			foreach (XElement xelement2 in xelement.Elements("Connection"))
			{
				string text = (xelement2.Attribute("applicationId") != null) ? xelement2.Attribute("applicationId").Value : null;
				string value = xelement2.Value;
				bool flag = text != null && value != null;
				if (flag)
				{
					ClockWorkApplicationConnectionString clockWorkApplicationConnectionString = new ClockWorkApplicationConnectionString
					{
						ApplicationId = text,
						ConnectionString = new ClockWorkConnectionString(value)
					};
					this.CreateConnectionString(clockWorkApplicationConnectionString.ConnectionString);
					this.AssignConnectionString(text, clockWorkApplicationConnectionString.ConnectionString.Name);
				}
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00053FC0 File Offset: 0x000521C0
		public void ExportToFile(string filename)
		{
			IList<ClockWorkApplicationConnectionString> assignedConnectionStringList = this.GetAssignedConnectionStringList();
			XElement xelement = new XElement("ServerConnections", from acs in assignedConnectionStringList
			select new XElement("Connection", new object[]
			{
				new XAttribute("applicationId", acs.ApplicationId),
				new XAttribute("productName", acs.ProductName),
				acs.ConnectionString.ToString()
			}));
			xelement.Save(filename);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00054014 File Offset: 0x00052214
		public void ExportToFile(string filename, eTechnoProProductNames productName)
		{
			IList<ClockWorkApplicationConnectionString> assignedConnectionStringList = this.GetAssignedConnectionStringList(productName);
			XElement xelement = new XElement("ServerConnections", from acs in assignedConnectionStringList
			select new XElement("Connection", new object[]
			{
				new XAttribute("applicationId", acs.ApplicationId),
				new XAttribute("productName", acs.ProductName),
				acs.ConnectionString.ToString()
			}));
			xelement.Save(filename);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00054067 File Offset: 0x00052267
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0005406F File Offset: 0x0005226F
		public OperationContext OpContext { get; set; }
	}
}
