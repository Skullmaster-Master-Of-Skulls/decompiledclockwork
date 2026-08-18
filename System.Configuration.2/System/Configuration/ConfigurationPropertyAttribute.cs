using System;

namespace System.Configuration
{
	// Token: 0x02000033 RID: 51
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ConfigurationPropertyAttribute : Attribute
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0001136F File Offset: 0x0000F56F
		public ConfigurationPropertyAttribute(string name)
		{
			this._Name = name;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00011389 File Offset: 0x0000F589
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00011391 File Offset: 0x0000F591
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00011399 File Offset: 0x0000F599
		public object DefaultValue
		{
			get
			{
				return this._DefaultValue;
			}
			set
			{
				this._DefaultValue = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000113A2 File Offset: 0x0000F5A2
		// (set) Token: 0x06000274 RID: 628 RVA: 0x000113AA File Offset: 0x0000F5AA
		public ConfigurationPropertyOptions Options
		{
			get
			{
				return this._Flags;
			}
			set
			{
				this._Flags = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000113B3 File Offset: 0x0000F5B3
		// (set) Token: 0x06000276 RID: 630 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public bool IsDefaultCollection
		{
			get
			{
				return (this.Options & ConfigurationPropertyOptions.IsDefaultCollection) > ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.Options |= ConfigurationPropertyOptions.IsDefaultCollection;
					return;
				}
				this.Options &= ~ConfigurationPropertyOptions.IsDefaultCollection;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000113E3 File Offset: 0x0000F5E3
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000113F0 File Offset: 0x0000F5F0
		public bool IsRequired
		{
			get
			{
				return (this.Options & ConfigurationPropertyOptions.IsRequired) > ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.Options |= ConfigurationPropertyOptions.IsRequired;
					return;
				}
				this.Options &= ~ConfigurationPropertyOptions.IsRequired;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00011413 File Offset: 0x0000F613
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00011420 File Offset: 0x0000F620
		public bool IsKey
		{
			get
			{
				return (this.Options & ConfigurationPropertyOptions.IsKey) > ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.Options |= ConfigurationPropertyOptions.IsKey;
					return;
				}
				this.Options &= ~ConfigurationPropertyOptions.IsKey;
			}
		}

		// Token: 0x040001F4 RID: 500
		internal static readonly string DefaultCollectionPropertyName = "";

		// Token: 0x040001F5 RID: 501
		private string _Name;

		// Token: 0x040001F6 RID: 502
		private object _DefaultValue = ConfigurationElement.s_nullPropertyValue;

		// Token: 0x040001F7 RID: 503
		private ConfigurationPropertyOptions _Flags;
	}
}
