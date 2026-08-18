using System;
using System.Globalization;
using System.Text;
using Microsoft.Win32;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200006F RID: 111
	public sealed class SiteCollection : ConfigurationElementCollectionBase<Site>
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000841C File Offset: 0x0000741C
		internal SiteCollection(ServerManager owner)
		{
			this._owner = owner;
		}

		// Token: 0x17000183 RID: 387
		public Site this[string name]
		{
			get
			{
				return base.FindElementWithCollectionKey("name", name);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00008439 File Offset: 0x00007439
		public Site Add(string name, string physicalPath, int port)
		{
			return this.Add(name, "http", "*:" + port.ToString(CultureInfo.InvariantCulture) + ':', physicalPath, null);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008466 File Offset: 0x00007466
		public Site Add(string name, string bindingProtocol, string bindingInformation, string physicalPath)
		{
			return this.Add(name, bindingProtocol, bindingInformation, physicalPath, null);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00008474 File Offset: 0x00007474
		public Site Add(string name, string bindingInformation, string physicalPath, byte[] certificateHash)
		{
			if (certificateHash == null)
			{
				throw new InvalidOperationException(Resources.CertificateNotSpecified);
			}
			return this.Add(name, "https", bindingInformation, physicalPath, certificateHash);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008498 File Offset: 0x00007498
		private Site Add(string name, string bindingProtocol, string bindingInformation, string physicalPath, byte[] certificateHash)
		{
			SiteCollection.ValidateName(name);
			Site site = base.CreateElement();
			site["name"] = name;
			site["id"] = (long)this.GenerateNewSiteID(name);
			base.Add(site);
			if (certificateHash != null)
			{
				site.Bindings.Add(bindingInformation, certificateHash, "MY");
			}
			else
			{
				site.Bindings.Add(bindingInformation, bindingProtocol);
			}
			site.Applications.Add("/", physicalPath);
			return site;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000851A File Offset: 0x0000751A
		protected override Site CreateNewElement(string elementTagName)
		{
			return new Site(this._owner);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00008528 File Offset: 0x00007528
		private bool ExistsSiteId(int siteID)
		{
			foreach (Site site in this)
			{
				if ((long)siteID == site.Id)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000857C File Offset: 0x0000757C
		private int GenerateNewSiteID(string siteName)
		{
			if (this.IsIncrementalSiteIDCreationSet())
			{
				return this.GenerateNewSiteIDIncremental();
			}
			return this.GenerateNewSiteIDFromName(siteName);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00008594 File Offset: 0x00007594
		private int GenerateNewSiteIDFromName(string siteName)
		{
			int num = siteName.GetHashCode();
			num = Math.Abs(num);
			while (this.ExistsSiteId(num))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000085C0 File Offset: 0x000075C0
		private int GenerateNewSiteIDIncremental()
		{
			int num = base.Count + 1;
			long[] array = new long[num];
			int length = 1;
			for (int i = 1; i < num; i++)
			{
				long id = base[i - 1].Id;
				if (id != 0L)
				{
					array[length++] = id;
				}
			}
			Array.Sort<long>(array, 0, length);
			for (int j = 1; j < num; j++)
			{
				if (array[j] != (long)j)
				{
					return j;
				}
			}
			return num;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000862F File Offset: 0x0000762F
		public static char[] InvalidSiteNameCharacters()
		{
			return SharedGlobals.GetInvalidSiteNameCharacters();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008638 File Offset: 0x00007638
		private bool IsIncrementalSiteIDCreationSet()
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\InetMgr\\Parameters", false);
				if (registryKey == null)
				{
					return false;
				}
				int num = (int)registryKey.GetValue("IncrementalSiteIDCreation", 0);
				if (num == 1)
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
					registryKey = null;
				}
			}
			return false;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000086B4 File Offset: 0x000076B4
		public new void Remove(Site element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Configuration != base.Configuration)
			{
				throw new InvalidOperationException(Resources.InvalidElementConfigurationObject);
			}
			SiteCollection.RemoveBindings(element);
			base.Remove(element);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000086EC File Offset: 0x000076EC
		private static void RemoveBindings(Site element)
		{
			if (element != null && element.Bindings != null)
			{
				for (int i = element.Bindings.Count - 1; i >= 0; i--)
				{
					element.Bindings.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008728 File Offset: 0x00007728
		public new void RemoveAt(int index)
		{
			Site element = base[index];
			SiteCollection.RemoveBindings(element);
			base.RemoveAt(index);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000874C File Offset: 0x0000774C
		private static void ValidateName(string name)
		{
			string text = string.Empty;
			if (string.IsNullOrEmpty(name) || name.Trim().Length < 1)
			{
				text = Resources.SiteNameLengthValidation;
			}
			else
			{
				char[] array = SiteCollection.InvalidSiteNameCharacters();
				if (name.IndexOfAny(array) != -1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(array[i]);
						if (i < array.Length - 1)
						{
							stringBuilder.Append(", ");
						}
					}
					text = string.Format(CultureInfo.InvariantCulture, Resources.SiteNameCannotContainChars, new object[]
					{
						stringBuilder.ToString()
					});
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				throw new FormatException(text);
			}
		}

		// Token: 0x04000124 RID: 292
		private const string InetmgrRegistryKeyPath = "SOFTWARE\\Microsoft\\InetMgr\\Parameters";

		// Token: 0x04000125 RID: 293
		private const string IncrementalSiteIDCreationKey = "IncrementalSiteIDCreation";

		// Token: 0x04000126 RID: 294
		private ServerManager _owner;
	}
}
