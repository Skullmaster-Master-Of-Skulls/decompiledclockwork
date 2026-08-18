using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Xml.Serialization;

namespace System.Diagnostics
{
	// Token: 0x020004A6 RID: 1190
	public abstract class Switch
	{
		// Token: 0x06002C13 RID: 11283 RVA: 0x000C7109 File Offset: 0x000C5309
		protected Switch(string displayName, string description) : this(displayName, description, "0")
		{
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000C7118 File Offset: 0x000C5318
		protected Switch(string displayName, string description, string defaultSwitchValue)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			this.displayName = displayName;
			this.description = description;
			List<WeakReference> obj = Switch.switches;
			lock (obj)
			{
				Switch._pruneCachedSwitches();
				Switch.switches.Add(new WeakReference(this));
			}
			this.defaultValue = defaultSwitchValue;
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000C7198 File Offset: 0x000C5398
		private static void _pruneCachedSwitches()
		{
			List<WeakReference> obj = Switch.switches;
			lock (obj)
			{
				if (Switch.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list = new List<WeakReference>(Switch.switches.Count);
					for (int i = 0; i < Switch.switches.Count; i++)
					{
						Switch @switch = (Switch)Switch.switches[i].Target;
						if (@switch != null)
						{
							list.Add(Switch.switches[i]);
						}
					}
					if (list.Count < Switch.switches.Count)
					{
						Switch.switches.Clear();
						Switch.switches.AddRange(list);
						Switch.switches.TrimExcess();
					}
					Switch.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000C7270 File Offset: 0x000C5470
		[XmlIgnore]
		public StringDictionary Attributes
		{
			get
			{
				this.Initialize();
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000C7291 File Offset: 0x000C5491
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000C7299 File Offset: 0x000C5499
		public string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000C72AF File Offset: 0x000C54AF
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x000C72D0 File Offset: 0x000C54D0
		protected int SwitchSetting
		{
			get
			{
				if (!this.initialized && this.InitializeWithStatus())
				{
					this.OnSwitchSettingChanged();
				}
				return this.switchSetting;
			}
			set
			{
				bool flag = false;
				object critSec = TraceInternal.critSec;
				lock (critSec)
				{
					this.initialized = true;
					if (this.switchSetting != value)
					{
						this.switchSetting = value;
						flag = true;
					}
				}
				if (flag)
				{
					this.OnSwitchSettingChanged();
				}
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000C7330 File Offset: 0x000C5530
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x000C7340 File Offset: 0x000C5540
		protected string Value
		{
			get
			{
				this.Initialize();
				return this.switchValueString;
			}
			set
			{
				this.Initialize();
				this.switchValueString = value;
				try
				{
					this.OnValueChanged();
				}
				catch (ArgumentException inner)
				{
					throw new ConfigurationErrorsException(SR.GetString("BadConfigSwitchValue", new object[]
					{
						this.DisplayName
					}), inner);
				}
				catch (FormatException inner2)
				{
					throw new ConfigurationErrorsException(SR.GetString("BadConfigSwitchValue", new object[]
					{
						this.DisplayName
					}), inner2);
				}
				catch (OverflowException inner3)
				{
					throw new ConfigurationErrorsException(SR.GetString("BadConfigSwitchValue", new object[]
					{
						this.DisplayName
					}), inner3);
				}
			}
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000C73F0 File Offset: 0x000C55F0
		private void Initialize()
		{
			this.InitializeWithStatus();
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000C73FC File Offset: 0x000C55FC
		private bool InitializeWithStatus()
		{
			if (!this.initialized)
			{
				object critSec = TraceInternal.critSec;
				lock (critSec)
				{
					if (this.initialized || this.initializing)
					{
						return false;
					}
					this.initializing = true;
					if (this.switchSettings == null && !this.InitializeConfigSettings())
					{
						this.initialized = true;
						this.initializing = false;
						return false;
					}
					if (this.switchSettings != null)
					{
						SwitchElement switchElement = this.switchSettings[this.displayName];
						if (switchElement != null)
						{
							string value = switchElement.Value;
							if (value != null)
							{
								this.Value = value;
							}
							else
							{
								this.Value = this.defaultValue;
							}
							try
							{
								TraceUtils.VerifyAttributes(switchElement.Attributes, this.GetSupportedAttributes(), this);
							}
							catch (ConfigurationException)
							{
								this.initialized = false;
								this.initializing = false;
								throw;
							}
							this.attributes = new StringDictionary();
							this.attributes.ReplaceHashtable(switchElement.Attributes);
						}
						else
						{
							this.switchValueString = this.defaultValue;
							this.OnValueChanged();
						}
					}
					else
					{
						this.switchValueString = this.defaultValue;
						this.OnValueChanged();
					}
					this.initialized = true;
					this.initializing = false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000C7574 File Offset: 0x000C5774
		private bool InitializeConfigSettings()
		{
			if (this.switchSettings != null)
			{
				return true;
			}
			if (!DiagnosticsConfiguration.CanInitialize())
			{
				return false;
			}
			this.switchSettings = DiagnosticsConfiguration.SwitchSettings;
			return true;
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000C7595 File Offset: 0x000C5795
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000C7598 File Offset: 0x000C5798
		protected virtual void OnSwitchSettingChanged()
		{
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000C759A File Offset: 0x000C579A
		protected virtual void OnValueChanged()
		{
			this.SwitchSetting = int.Parse(this.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000C75B4 File Offset: 0x000C57B4
		internal static void RefreshAll()
		{
			List<WeakReference> obj = Switch.switches;
			lock (obj)
			{
				Switch._pruneCachedSwitches();
				for (int i = 0; i < Switch.switches.Count; i++)
				{
					Switch @switch = (Switch)Switch.switches[i].Target;
					if (@switch != null)
					{
						@switch.Refresh();
					}
				}
			}
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000C7628 File Offset: 0x000C5828
		internal void Refresh()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.initialized = false;
				this.switchSettings = null;
				this.Initialize();
			}
		}

		// Token: 0x040026B4 RID: 9908
		private SwitchElementsCollection switchSettings;

		// Token: 0x040026B5 RID: 9909
		private readonly string description;

		// Token: 0x040026B6 RID: 9910
		private readonly string displayName;

		// Token: 0x040026B7 RID: 9911
		private int switchSetting;

		// Token: 0x040026B8 RID: 9912
		private volatile bool initialized;

		// Token: 0x040026B9 RID: 9913
		private bool initializing;

		// Token: 0x040026BA RID: 9914
		private volatile string switchValueString = string.Empty;

		// Token: 0x040026BB RID: 9915
		private StringDictionary attributes;

		// Token: 0x040026BC RID: 9916
		private string defaultValue;

		// Token: 0x040026BD RID: 9917
		private static List<WeakReference> switches = new List<WeakReference>();

		// Token: 0x040026BE RID: 9918
		private static int s_LastCollectionCount;
	}
}
