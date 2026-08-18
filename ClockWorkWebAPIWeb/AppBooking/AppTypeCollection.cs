using System;
using System.Collections;
using ClockWorkWebAPI;
using EncryptionClassLibrary;

namespace ClockWorkWebAPIWeb.AppBooking
{
	// Token: 0x0200001B RID: 27
	public class AppTypeCollection : CollectionBase
	{
		// Token: 0x0600014C RID: 332 RVA: 0x0001078E File Offset: 0x0000E98E
		public AppTypeCollection()
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00010798 File Offset: 0x0000E998
		public AppTypeCollection(string definition, db conn, IEncryption tripleDES)
		{
			string[] array = definition.Split(new char[]
			{
				'_'
			});
			foreach (string defn in array)
			{
				AppType appType = new AppType(defn, conn, tripleDES);
				bool active = appType.Active;
				if (active)
				{
					base.List.Add(appType);
				}
			}
		}
	}
}
