using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D1 RID: 465
	public class GroupDataAttribute : Attribute
	{
		// Token: 0x06000D6D RID: 3437 RVA: 0x0001528E File Offset: 0x0001348E
		public GroupDataAttribute(string name)
		{
			this.name = name;
			this.isActive = true;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000152A6 File Offset: 0x000134A6
		public GroupDataAttribute(string name, bool isActive)
		{
			this.name = name;
			this.isActive = isActive;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000152BE File Offset: 0x000134BE
		public GroupDataAttribute(string name, bool isActive, Setting defaultSignatureSetting)
		{
			this.name = name;
			this.isActive = isActive;
			this.defaultSignatureSetting = (int)defaultSignatureSetting;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000152E0 File Offset: 0x000134E0
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x000152F8 File Offset: 0x000134F8
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				this.isActive = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00015304 File Offset: 0x00013504
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x0001531C File Offset: 0x0001351C
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00015328 File Offset: 0x00013528
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00015340 File Offset: 0x00013540
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x00015358 File Offset: 0x00013558
		public string IconName
		{
			get
			{
				return this.iconName;
			}
			set
			{
				this.iconName = value;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00015364 File Offset: 0x00013564
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x0001537C File Offset: 0x0001357C
		public int DefaultFromSetting
		{
			get
			{
				return this.defaultFromSetting;
			}
			set
			{
				this.defaultFromSetting = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00015388 File Offset: 0x00013588
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x000153A0 File Offset: 0x000135A0
		public int DefaultSignatureSetting
		{
			get
			{
				return this.defaultSignatureSetting;
			}
			set
			{
				this.defaultSignatureSetting = value;
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000153AC File Offset: 0x000135AC
		public static GroupDataAttribute GetAttribute(Group group)
		{
			return GroupDataAttribute.GetAttribute<GroupDataAttribute>(group);
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000153C9 File Offset: 0x000135C9
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x000153D1 File Offset: 0x000135D1
		public string LicensingProductName { get; set; }

		// Token: 0x06000D7E RID: 3454 RVA: 0x000153DC File Offset: 0x000135DC
		public static T GetAttribute<T>(Enum enumeration) where T : Attribute
		{
			T t = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault<T>();
			bool flag = t == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				result = t;
			}
			return result;
		}

		// Token: 0x04000935 RID: 2357
		protected string name;

		// Token: 0x04000936 RID: 2358
		protected string description;

		// Token: 0x04000937 RID: 2359
		protected string iconName;

		// Token: 0x04000938 RID: 2360
		protected bool isActive;

		// Token: 0x04000939 RID: 2361
		protected int defaultSignatureSetting;

		// Token: 0x0400093A RID: 2362
		protected int defaultFromSetting;
	}
}
