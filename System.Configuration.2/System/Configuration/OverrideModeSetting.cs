using System;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000074 RID: 116
	internal struct OverrideModeSetting
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x00018BA8 File Offset: 0x00016DA8
		static OverrideModeSetting()
		{
			OverrideModeSetting.SectionDefault._mode = 1;
			OverrideModeSetting.LocationDefault = default(OverrideModeSetting);
			OverrideModeSetting.LocationDefault._mode = 0;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00018BD8 File Offset: 0x00016DD8
		internal static OverrideModeSetting CreateFromXmlReadValue(bool allowOverride)
		{
			OverrideModeSetting result = default(OverrideModeSetting);
			result.SetMode(allowOverride ? OverrideMode.Inherit : OverrideMode.Deny);
			result._mode |= 64;
			return result;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00018C0C File Offset: 0x00016E0C
		internal static OverrideModeSetting CreateFromXmlReadValue(OverrideMode mode)
		{
			OverrideModeSetting result = default(OverrideModeSetting);
			result.SetMode(mode);
			result._mode |= 128;
			return result;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00018C3C File Offset: 0x00016E3C
		internal static OverrideMode ParseOverrideModeXmlValue(string value, XmlUtil xmlUtil)
		{
			if (value == "Inherit")
			{
				return OverrideMode.Inherit;
			}
			if (value == "Allow")
			{
				return OverrideMode.Allow;
			}
			if (!(value == "Deny"))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_section_override_mode_attribute_invalid"), xmlUtil);
			}
			return OverrideMode.Deny;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00018C88 File Offset: 0x00016E88
		internal static bool CanUseSameLocationTag(OverrideModeSetting x, OverrideModeSetting y)
		{
			bool flag = x.OverrideMode == y.OverrideMode;
			if (flag)
			{
				if ((x._mode & 48) != 0)
				{
					flag = OverrideModeSetting.IsMatchingApiChangedLocationTag(x, y);
				}
				else if ((y._mode & 48) != 0)
				{
					flag = OverrideModeSetting.IsMatchingApiChangedLocationTag(y, x);
				}
				else
				{
					flag = (((x._mode & 192) == 0 && (y._mode & 192) == 0) || (x._mode & 192) == (y._mode & 192));
				}
			}
			return flag;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00018D14 File Offset: 0x00016F14
		private static bool IsMatchingApiChangedLocationTag(OverrideModeSetting x, OverrideModeSetting y)
		{
			bool result = false;
			if ((y._mode & 48) != 0)
			{
				result = ((x._mode & 48) == (y._mode & 48));
			}
			else if ((y._mode & 192) != 0)
			{
				result = (((x._mode & 16) != 0 && (y._mode & 64) != 0) || ((x._mode & 32) != 0 && (y._mode & 128) > 0));
			}
			return result;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00018D8C File Offset: 0x00016F8C
		internal bool IsDefaultForSection
		{
			get
			{
				OverrideMode overrideMode = this.OverrideMode;
				return overrideMode == OverrideMode.Allow || overrideMode == OverrideMode.Inherit;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00018DAC File Offset: 0x00016FAC
		internal bool IsDefaultForLocationTag
		{
			get
			{
				OverrideModeSetting locationDefault = OverrideModeSetting.LocationDefault;
				return locationDefault.OverrideMode == this.OverrideMode && (this._mode & 48) == 0 && (this._mode & 192) == 0;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00018DEA File Offset: 0x00016FEA
		internal bool IsLocked
		{
			get
			{
				return this.OverrideMode == OverrideMode.Deny;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00018DF8 File Offset: 0x00016FF8
		internal string LocationTagXmlString
		{
			get
			{
				string result = string.Empty;
				bool flag = false;
				bool flag2 = false;
				if ((this._mode & 48) != 0)
				{
					flag2 = ((this._mode & 16) > 0);
					flag = true;
				}
				else if ((this._mode & 192) != 0)
				{
					flag2 = ((this._mode & 64) > 0);
					flag = true;
				}
				if (flag)
				{
					string text;
					string text2;
					if (flag2)
					{
						text = "allowOverride";
						text2 = (this.AllowOverride ? "true" : "false");
					}
					else
					{
						text = "overrideMode";
						text2 = this.OverrideModeXmlValue;
					}
					result = string.Format(CultureInfo.InvariantCulture, "{0}=\"{1}\"", new object[]
					{
						text,
						text2
					});
				}
				return result;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00018EA0 File Offset: 0x000170A0
		internal string OverrideModeXmlValue
		{
			get
			{
				switch (this.OverrideMode)
				{
				case OverrideMode.Inherit:
					return "Inherit";
				case OverrideMode.Allow:
					return "Allow";
				case OverrideMode.Deny:
					return "Deny";
				default:
					return null;
				}
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00018EDB File Offset: 0x000170DB
		internal void ChangeModeInternal(OverrideMode mode)
		{
			this.SetMode(mode);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00018EE4 File Offset: 0x000170E4
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x00018EEF File Offset: 0x000170EF
		internal OverrideMode OverrideMode
		{
			get
			{
				return (OverrideMode)(this._mode & 15);
			}
			set
			{
				this.VerifyConsistentChangeModel(32);
				this.SetMode(value);
				this._mode |= 32;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00018F10 File Offset: 0x00017110
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00018F37 File Offset: 0x00017137
		internal bool AllowOverride
		{
			get
			{
				bool result = true;
				OverrideMode overrideMode = this.OverrideMode;
				if (overrideMode > OverrideMode.Allow)
				{
					if (overrideMode == OverrideMode.Deny)
					{
						result = false;
					}
				}
				else
				{
					result = true;
				}
				return result;
			}
			set
			{
				this.VerifyConsistentChangeModel(16);
				this.SetMode(value ? OverrideMode.Inherit : OverrideMode.Deny);
				this._mode |= 16;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00018F5E File Offset: 0x0001715E
		private void SetMode(OverrideMode mode)
		{
			this._mode = (byte)mode;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00018F68 File Offset: 0x00017168
		private void VerifyConsistentChangeModel(byte required)
		{
			byte b = this._mode & 48;
			if (b != 0 && b != required)
			{
				throw new ConfigurationErrorsException(SR.GetString("Cannot_change_both_AllowOverride_and_OverrideMode"));
			}
		}

		// Token: 0x040002B9 RID: 697
		private const byte ApiDefinedLegacy = 16;

		// Token: 0x040002BA RID: 698
		private const byte ApiDefinedNewMode = 32;

		// Token: 0x040002BB RID: 699
		private const byte ApiDefinedAny = 48;

		// Token: 0x040002BC RID: 700
		private const byte XmlDefinedLegacy = 64;

		// Token: 0x040002BD RID: 701
		private const byte XmlDefinedNewMode = 128;

		// Token: 0x040002BE RID: 702
		private const byte XmlDefinedAny = 192;

		// Token: 0x040002BF RID: 703
		private const byte ModeMask = 15;

		// Token: 0x040002C0 RID: 704
		private byte _mode;

		// Token: 0x040002C1 RID: 705
		internal static OverrideModeSetting SectionDefault = default(OverrideModeSetting);

		// Token: 0x040002C2 RID: 706
		internal static OverrideModeSetting LocationDefault;
	}
}
