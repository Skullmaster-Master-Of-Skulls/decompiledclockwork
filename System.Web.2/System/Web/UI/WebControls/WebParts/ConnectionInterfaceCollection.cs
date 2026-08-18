using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000531 RID: 1329
	public sealed class ConnectionInterfaceCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06004370 RID: 17264 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public ConnectionInterfaceCollection()
		{
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000DE197 File Offset: 0x000DC397
		public ConnectionInterfaceCollection(ICollection connectionInterfaces)
		{
			this.Initialize(null, connectionInterfaces);
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x000DE1A7 File Offset: 0x000DC3A7
		public ConnectionInterfaceCollection(ConnectionInterfaceCollection existingConnectionInterfaces, ICollection connectionInterfaces)
		{
			this.Initialize(existingConnectionInterfaces, connectionInterfaces);
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000DE1B8 File Offset: 0x000DC3B8
		private void Initialize(ConnectionInterfaceCollection existingConnectionInterfaces, ICollection connectionInterfaces)
		{
			if (existingConnectionInterfaces != null)
			{
				foreach (object obj in existingConnectionInterfaces)
				{
					Type value = (Type)obj;
					base.InnerList.Add(value);
				}
			}
			if (connectionInterfaces != null)
			{
				foreach (object obj2 in connectionInterfaces)
				{
					if (obj2 == null)
					{
						throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "connectionInterfaces");
					}
					if (!(obj2 is Type))
					{
						throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
						{
							"Type"
						}), "connectionInterfaces");
					}
					base.InnerList.Add(obj2);
				}
			}
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(Type value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(Type value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x170013C5 RID: 5061
		public Type this[int index]
		{
			get
			{
				return (Type)base.InnerList[index];
			}
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(Type[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x040025DF RID: 9695
		public static readonly ConnectionInterfaceCollection Empty = new ConnectionInterfaceCollection();
	}
}
