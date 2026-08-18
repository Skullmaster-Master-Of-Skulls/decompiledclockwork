using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006BF RID: 1727
	public sealed class RemoveBehaviorElement : BehaviorExtensionElement
	{
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x0600430A RID: 17162 RVA: 0x000FD31C File Offset: 0x000FB51C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("name", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x0600430C RID: 17164 RVA: 0x000FD375 File Offset: 0x000FB575
		// (set) Token: 0x0600430D RID: 17165 RVA: 0x000FD387 File Offset: 0x000FB587
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000FD398 File Offset: 0x000FB598
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			RemoveBehaviorElement removeBehaviorElement = (RemoveBehaviorElement)from;
			this.Name = removeBehaviorElement.Name;
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x000FD3BF File Offset: 0x000FB5BF
		protected internal override object CreateBehavior()
		{
			return null;
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x000FD3C2 File Offset: 0x000FB5C2
		public override Type BehaviorType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002D0F RID: 11535
		private ConfigurationPropertyCollection properties;
	}
}
