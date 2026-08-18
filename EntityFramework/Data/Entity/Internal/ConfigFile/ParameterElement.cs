using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020006B9 RID: 1721
	internal class ParameterElement : ConfigurationElement
	{
		// Token: 0x06004482 RID: 17538 RVA: 0x0014437E File Offset: 0x0014257E
		public ParameterElement(int key)
		{
			this.Key = key;
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x0014438D File Offset: 0x0014258D
		// (set) Token: 0x06004484 RID: 17540 RVA: 0x00144395 File Offset: 0x00142595
		internal int Key { get; private set; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06004485 RID: 17541 RVA: 0x0014439E File Offset: 0x0014259E
		// (set) Token: 0x06004486 RID: 17542 RVA: 0x001443B0 File Offset: 0x001425B0
		[ConfigurationProperty("value", IsRequired = true)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public string ValueString
		{
			get
			{
				return (string)base["value"];
			}
			set
			{
				base["value"] = value;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x001443BE File Offset: 0x001425BE
		// (set) Token: 0x06004488 RID: 17544 RVA: 0x001443D0 File Offset: 0x001425D0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[ConfigurationProperty("type", DefaultValue = "System.String")]
		public string TypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x001443E0 File Offset: 0x001425E0
		public object GetTypedParameterValue()
		{
			Type type = Type.GetType(this.TypeName, true);
			return Convert.ChangeType(this.ValueString, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x0400193C RID: 6460
		private const string ValueKey = "value";

		// Token: 0x0400193D RID: 6461
		private const string TypeKey = "type";
	}
}
