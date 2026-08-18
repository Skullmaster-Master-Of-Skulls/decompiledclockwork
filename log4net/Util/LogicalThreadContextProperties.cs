using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace log4net.Util
{
	// Token: 0x020000FD RID: 253
	public sealed class LogicalThreadContextProperties : ContextPropertiesBase
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x000171E7 File Offset: 0x000153E7
		internal LogicalThreadContextProperties()
		{
		}

		// Token: 0x1700017E RID: 382
		public override object this[string key]
		{
			get
			{
				PropertiesDictionary properties = this.GetProperties(false);
				if (properties != null)
				{
					return properties[key];
				}
				return null;
			}
			set
			{
				PropertiesDictionary properties = this.GetProperties(true);
				PropertiesDictionary propertiesDictionary = new PropertiesDictionary(properties);
				propertiesDictionary[key] = value;
				LogicalThreadContextProperties.SetLogicalProperties(propertiesDictionary);
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00017240 File Offset: 0x00015440
		public void Remove(string key)
		{
			PropertiesDictionary properties = this.GetProperties(false);
			if (properties != null)
			{
				PropertiesDictionary propertiesDictionary = new PropertiesDictionary(properties);
				propertiesDictionary.Remove(key);
				LogicalThreadContextProperties.SetLogicalProperties(propertiesDictionary);
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001726C File Offset: 0x0001546C
		public void Clear()
		{
			PropertiesDictionary properties = this.GetProperties(false);
			if (properties != null)
			{
				PropertiesDictionary logicalProperties = new PropertiesDictionary();
				LogicalThreadContextProperties.SetLogicalProperties(logicalProperties);
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00017290 File Offset: 0x00015490
		internal PropertiesDictionary GetProperties(bool create)
		{
			if (!this.m_disabled)
			{
				try
				{
					PropertiesDictionary propertiesDictionary = LogicalThreadContextProperties.GetLogicalProperties();
					if (propertiesDictionary == null && create)
					{
						propertiesDictionary = new PropertiesDictionary();
						LogicalThreadContextProperties.SetLogicalProperties(propertiesDictionary);
					}
					return propertiesDictionary;
				}
				catch (SecurityException exception)
				{
					this.m_disabled = true;
					LogLog.Warn(LogicalThreadContextProperties.declaringType, "SecurityException while accessing CallContext. Disabling LogicalThreadContextProperties", exception);
				}
			}
			if (create)
			{
				return new PropertiesDictionary();
			}
			return null;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000172F8 File Offset: 0x000154F8
		[SecuritySafeCritical]
		private static PropertiesDictionary GetLogicalProperties()
		{
			return CallContext.LogicalGetData("log4net.Util.LogicalThreadContextProperties") as PropertiesDictionary;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00017309 File Offset: 0x00015509
		[SecuritySafeCritical]
		private static void SetLogicalProperties(PropertiesDictionary properties)
		{
			CallContext.LogicalSetData("log4net.Util.LogicalThreadContextProperties", properties);
		}

		// Token: 0x040002B5 RID: 693
		private const string c_SlotName = "log4net.Util.LogicalThreadContextProperties";

		// Token: 0x040002B6 RID: 694
		private bool m_disabled;

		// Token: 0x040002B7 RID: 695
		private static readonly Type declaringType = typeof(LogicalThreadContextProperties);
	}
}
