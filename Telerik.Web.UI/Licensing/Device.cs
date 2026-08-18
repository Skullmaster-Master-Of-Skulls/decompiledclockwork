using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Management;
using System.Text;

namespace Telerik.Licensing
{
	// Token: 0x02000401 RID: 1025
	internal abstract class Device : IDisposable
	{
		// Token: 0x06002588 RID: 9608 RVA: 0x0007C8E3 File Offset: 0x0007AAE3
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		protected Device(string scope)
		{
			this._managmentClass = new ManagementClass(scope);
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x0007C8F7 File Offset: 0x0007AAF7
		protected ManagementClass ManagementClass
		{
			get
			{
				return this._managmentClass;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x0007C8FF File Offset: 0x0007AAFF
		private static IDictionary<Type, string> Types
		{
			get
			{
				return Device.types;
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0007C908 File Offset: 0x0007AB08
		public static string GetId(Type type)
		{
			if (!typeof(Device).IsAssignableFrom(type))
			{
				throw new NotSupportedException("Type must inherit from Telerik.Device");
			}
			if (!Device.Types.ContainsKey(type))
			{
				lock (Device.typesLock)
				{
					if (!Device.Types.ContainsKey(type))
					{
						Device.Types[type] = ((Device)Activator.CreateInstance(type)).ReadId();
					}
				}
			}
			return Device.Types[type];
		}

		// Token: 0x0600258C RID: 9612
		public abstract string[] GetWmiProperties();

		// Token: 0x0600258D RID: 9613 RVA: 0x0007C9A0 File Offset: 0x0007ABA0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x0007C9AC File Offset: 0x0007ABAC
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		protected virtual string ReadId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			ManagementObjectCollection instances = this.ManagementClass.GetInstances();
			try
			{
				foreach (ManagementBaseObject managementBaseObject in instances)
				{
					foreach (string propertyName in this.GetWmiProperties())
					{
						string text = managementBaseObject.GetPropertyValue(propertyName) as string;
						if (!string.IsNullOrEmpty(text))
						{
							stringBuilder.Append(text + ";");
						}
					}
				}
			}
			finally
			{
				instances.Dispose();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x0007CA6C File Offset: 0x0007AC6C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.ManagementClass != null)
			{
				this._managmentClass.Dispose();
			}
		}

		// Token: 0x04000988 RID: 2440
		private static readonly IDictionary<Type, string> types = new Dictionary<Type, string>();

		// Token: 0x04000989 RID: 2441
		private static readonly object typesLock = new object();

		// Token: 0x0400098A RID: 2442
		private readonly ManagementClass _managmentClass;
	}
}
