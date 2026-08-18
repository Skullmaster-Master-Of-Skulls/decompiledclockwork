using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TechnoPro.Common.Public.Entities.Settings.Adapters;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D0 RID: 464
	public class LookupSetting : BusinessBase<Setting>
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00014FF8 File Offset: 0x000131F8
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00015010 File Offset: 0x00013210
		public Setting Setting
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
				this.SettingDataAttribute = value.GetSettingAttribute();
				this.GroupDataAtt = this.SettingDataAttribute.Group.GetGroupAttribute();
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0001503F File Offset: 0x0001323F
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00015047 File Offset: 0x00013247
		public SettingDataAttribute SettingDataAttribute { get; set; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00015050 File Offset: 0x00013250
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00015058 File Offset: 0x00013258
		private GroupDataAttribute GroupDataAtt { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00015064 File Offset: 0x00013264
		public Group Group
		{
			get
			{
				return this.SettingDataAttribute.Group;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00015084 File Offset: 0x00013284
		public string Name
		{
			get
			{
				return this.SettingDataAttribute.Name;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000150A4 File Offset: 0x000132A4
		public string SubGroup
		{
			get
			{
				return this.SettingDataAttribute.SubGroup ?? string.Empty;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000150CC File Offset: 0x000132CC
		public string GroupName
		{
			get
			{
				return (this.GroupDataAtt != null) ? this.GroupDataAtt.Name : Enum.GetName(typeof(Group), this.SettingDataAttribute.Group);
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00015114 File Offset: 0x00013314
		public string Description
		{
			get
			{
				return this.SettingDataAttribute.Description;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00015134 File Offset: 0x00013334
		public Type SystemType
		{
			get
			{
				return this.SettingDataAttribute.SystemType;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00015154 File Offset: 0x00013354
		public SettingSemantic SemanticType
		{
			get
			{
				return this.SettingDataAttribute.SemanticType;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00015174 File Offset: 0x00013374
		public bool HasDefaultValue
		{
			get
			{
				return this.SettingDataAttribute.DefaultValue != null;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00015194 File Offset: 0x00013394
		public bool IsHidden
		{
			get
			{
				return this.SettingDataAttribute.IsHidden;
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000151B1 File Offset: 0x000133B1
		public LookupSetting()
		{
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000151BB File Offset: 0x000133BB
		public LookupSetting(Setting setting)
		{
			this.Setting = setting;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000151D0 File Offset: 0x000133D0
		public static bool SetAllowUnsafeHeaderParsing20()
		{
			ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true));
			return true;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00015218 File Offset: 0x00013418
		public object GetDefaultValue()
		{
			return this.SettingDataAttribute.DefaultValue;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00015238 File Offset: 0x00013438
		public T GetDefaultValue<T>()
		{
			return (this.SettingDataAttribute.DefaultValue != null && typeof(T).IsInstanceOfType(this.SettingDataAttribute.DefaultValue)) ? ((T)((object)this.SettingDataAttribute.DefaultValue)) : default(T);
		}
	}
}
