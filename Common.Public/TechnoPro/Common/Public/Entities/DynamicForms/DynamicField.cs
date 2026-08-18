using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200036C RID: 876
	[Serializable]
	public class DynamicField : BusinessBase<int>, ICloneable<DynamicField>, ICloneable
	{
		// Token: 0x06001AFE RID: 6910 RVA: 0x0001EE79 File Offset: 0x0001D079
		public DynamicField()
		{
			this.ControlCaption = "";
			this.IsActive = true;
			this.ControlName = "";
			this.EnforceMethod = eEnforceType.Optional;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0001EEAC File Offset: 0x0001D0AC
		private static void CopyDynamicField(DynamicField itemSource, DynamicField itemDest)
		{
			bool flag = itemSource == null || itemDest == null;
			if (!flag)
			{
				itemDest.ControlId = itemSource.ControlId;
				itemDest.ControlCaption = itemSource.ControlCaption;
				itemDest.ControlCode = itemSource.ControlCode;
				itemDest.Args = itemSource.Args;
				itemDest.IsActive = itemSource.IsActive;
				itemDest.OrderNum = itemSource.OrderNum;
				itemDest.ControlName = itemSource.ControlName;
				itemDest.IsReadOnly = itemSource.IsReadOnly;
				itemDest.HideCaption = itemSource.HideCaption;
				itemDest.DontWrapToNextLine = itemSource.DontWrapToNextLine;
				itemDest.Setting1 = itemSource.Setting1;
				itemDest.Setting2 = itemSource.Setting2;
				itemDest.Setting3 = itemSource.Setting3;
				itemDest.Setting4 = itemSource.Setting4;
				itemDest.DefaultValue = itemSource.DefaultValue;
				itemDest.DefaultValueString = itemSource.DefaultValueString;
				itemDest.Setting4String = itemSource.Setting4String;
				itemDest.Mask = itemSource.Mask;
				itemDest.EnforceMethod = itemSource.EnforceMethod;
				itemDest.OriginalCaption = itemSource.OriginalCaption;
				itemDest.UniqueId = itemSource.UniqueId;
				itemDest.SpecialControlType = itemSource.SpecialControlType;
			}
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0001EFEB File Offset: 0x0001D1EB
		public DynamicField(DynamicField dynamicField)
		{
			DynamicField.CopyDynamicField(dynamicField, this);
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0001F000 File Offset: 0x0001D200
		// (set) Token: 0x06001B02 RID: 6914 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ControlId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0001F018 File Offset: 0x0001D218
		// (set) Token: 0x06001B04 RID: 6916 RVA: 0x0001F020 File Offset: 0x0001D220
		public string ControlCaption { get; set; }

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0001F029 File Offset: 0x0001D229
		// (set) Token: 0x06001B06 RID: 6918 RVA: 0x0001F031 File Offset: 0x0001D231
		public eControlCode ControlCode { get; set; }

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x0001F03A File Offset: 0x0001D23A
		// (set) Token: 0x06001B08 RID: 6920 RVA: 0x0001F042 File Offset: 0x0001D242
		public Dictionary<string, string> Args { get; set; }

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0001F04B File Offset: 0x0001D24B
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x0001F053 File Offset: 0x0001D253
		public bool IsActive { get; set; }

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0001F05C File Offset: 0x0001D25C
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x0001F064 File Offset: 0x0001D264
		public int OrderNum { get; set; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0001F06D File Offset: 0x0001D26D
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x0001F075 File Offset: 0x0001D275
		public string ControlName { get; set; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0001F07E File Offset: 0x0001D27E
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x0001F086 File Offset: 0x0001D286
		public bool IsReadOnly { get; set; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0001F08F File Offset: 0x0001D28F
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x0001F097 File Offset: 0x0001D297
		public bool HideCaption { get; set; }

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0001F0A0 File Offset: 0x0001D2A0
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
		public bool DontWrapToNextLine { get; set; }

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0001F0B1 File Offset: 0x0001D2B1
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x0001F0B9 File Offset: 0x0001D2B9
		public int Setting1 { get; set; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0001F0C2 File Offset: 0x0001D2C2
		// (set) Token: 0x06001B18 RID: 6936 RVA: 0x0001F0CA File Offset: 0x0001D2CA
		public int Setting2 { get; set; }

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0001F0D3 File Offset: 0x0001D2D3
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x0001F0DB File Offset: 0x0001D2DB
		public int Setting3 { get; set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0001F0E4 File Offset: 0x0001D2E4
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x0001F0EC File Offset: 0x0001D2EC
		public int Setting4 { get; set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0001F0F5 File Offset: 0x0001D2F5
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0001F0FD File Offset: 0x0001D2FD
		public int DefaultValue { get; set; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0001F106 File Offset: 0x0001D306
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x0001F10E File Offset: 0x0001D30E
		public string DefaultValueString { get; set; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0001F117 File Offset: 0x0001D317
		// (set) Token: 0x06001B22 RID: 6946 RVA: 0x0001F11F File Offset: 0x0001D31F
		public string Setting4String { get; set; }

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0001F128 File Offset: 0x0001D328
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x0001F130 File Offset: 0x0001D330
		public string Mask { get; set; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0001F139 File Offset: 0x0001D339
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x0001F141 File Offset: 0x0001D341
		public eEnforceType EnforceMethod { get; set; }

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0001F14A File Offset: 0x0001D34A
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x0001F152 File Offset: 0x0001D352
		public string OriginalCaption { get; set; }

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0001F15B File Offset: 0x0001D35B
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x0001F163 File Offset: 0x0001D363
		public string UniqueId { get; set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0001F16C File Offset: 0x0001D36C
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x0001F174 File Offset: 0x0001D374
		public eSpecialControlType SpecialControlType { get; set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0001F180 File Offset: 0x0001D380
		public eDynamicDataStorageType StorageType
		{
			get
			{
				eDynamicDataStorageType eDynamicDataStorageType = eDynamicDataStorageType.None;
				DynamicControlAttribute attribute = this.ControlCode.GetAttribute();
				bool flag = attribute != null;
				if (flag)
				{
					bool flag2 = attribute.EncryptedFlagEncryptionProperty > eDynamicControlPropertyEncryptionProperty.Unknown;
					if (flag2)
					{
						int num;
						switch (attribute.EncryptedFlagEncryptionProperty)
						{
						case eDynamicControlPropertyEncryptionProperty.Setting1:
							num = this.Setting1;
							break;
						case eDynamicControlPropertyEncryptionProperty.Setting2:
							num = this.Setting2;
							break;
						case eDynamicControlPropertyEncryptionProperty.Setting3:
							num = this.Setting3;
							break;
						case eDynamicControlPropertyEncryptionProperty.Setting4:
							num = this.Setting4;
							break;
						case eDynamicControlPropertyEncryptionProperty.AlwaysEncrypted:
							num = attribute.EncryptedFlagValue;
							break;
						default:
							num = 0;
							break;
						}
						bool flag3 = num == attribute.EncryptedFlagValue;
						if (flag3)
						{
							eDynamicDataStorageType |= eDynamicDataStorageType.Encrypted;
						}
					}
				}
				return eDynamicDataStorageType;
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0001F238 File Offset: 0x0001D438
		public DynamicField Clone()
		{
			return new DynamicField(this);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0001F250 File Offset: 0x0001D450
		public T Clone<T>() where T : DynamicField
		{
			T t = Activator.CreateInstance<T>();
			DynamicField.CopyDynamicField(this, t);
			return t;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0001F278 File Offset: 0x0001D478
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
