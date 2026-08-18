using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Management;
using System.Text;

namespace Telerik.Licensing
{
	// Token: 0x02000403 RID: 1027
	internal class Hdd : Device
	{
		// Token: 0x06002594 RID: 9620 RVA: 0x0007CAF4 File Offset: 0x0007ACF4
		public Hdd() : base("Win32_DiskDrive")
		{
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x0007CB4D File Offset: 0x0007AD4D
		protected List<string> InterfaceBlacklist
		{
			get
			{
				return this._forbiddenInterafces;
			}
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0007CB55 File Offset: 0x0007AD55
		public override string[] GetWmiProperties()
		{
			return this._wmiProperties;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0007CB60 File Offset: 0x0007AD60
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		protected override string ReadId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			ManagementObjectCollection instances = base.ManagementClass.GetInstances();
			try
			{
				foreach (ManagementBaseObject obj in instances)
				{
					IDictionary<string, string> dictionary = this.PopulateProperties(obj);
					if (dictionary.ContainsKey("InterfaceType") && this.IsInterfaceValid(dictionary["InterfaceType"]))
					{
						stringBuilder.Append(this.StringifyProperties(dictionary.Values));
					}
				}
			}
			finally
			{
				instances.Dispose();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0007CC10 File Offset: 0x0007AE10
		private bool IsInterfaceValid(string interfaceName)
		{
			return !this.InterfaceBlacklist.Contains(interfaceName);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0007CC24 File Offset: 0x0007AE24
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		private IDictionary<string, string> PopulateProperties(ManagementBaseObject obj)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in this.GetWmiProperties())
			{
				string value = obj.GetPropertyValue(text) as string;
				if (!string.IsNullOrEmpty(value))
				{
					dictionary[text] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0007CC74 File Offset: 0x0007AE74
		private string StringifyProperties(ICollection<string> valuesCollection)
		{
			string[] array = new string[valuesCollection.Count];
			valuesCollection.CopyTo(array, 0);
			return string.Join(";", array) + ";";
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0007CCAA File Offset: 0x0007AEAA
		public static string GetId()
		{
			return Device.GetId(typeof(Hdd));
		}

		// Token: 0x0400098D RID: 2445
		private const string WmiClass = "Win32_DiskDrive";

		// Token: 0x0400098E RID: 2446
		private const string InterfaceKey = "InterfaceType";

		// Token: 0x0400098F RID: 2447
		private readonly string[] _wmiProperties = new string[]
		{
			"SerialNumber",
			"Model",
			"InterfaceType"
		};

		// Token: 0x04000990 RID: 2448
		private readonly List<string> _forbiddenInterafces = new List<string>(new string[]
		{
			"USB"
		});
	}
}
