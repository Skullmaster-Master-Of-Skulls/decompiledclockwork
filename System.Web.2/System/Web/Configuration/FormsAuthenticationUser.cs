using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration
{
	// Token: 0x020006DF RID: 1759
	public sealed class FormsAuthenticationUser : ConfigurationElement
	{
		// Token: 0x060054AB RID: 21675 RVA: 0x00128778 File Offset: 0x00126978
		static FormsAuthenticationUser()
		{
			FormsAuthenticationUser._properties = new ConfigurationPropertyCollection();
			FormsAuthenticationUser._properties.Add(FormsAuthenticationUser._propName);
			FormsAuthenticationUser._properties.Add(FormsAuthenticationUser._propPassword);
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x00117E9E File Offset: 0x0011609E
		internal FormsAuthenticationUser()
		{
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x001287F1 File Offset: 0x001269F1
		public FormsAuthenticationUser(string name, string password) : this()
		{
			this.Name = name.ToLower(CultureInfo.InvariantCulture);
			this.Password = password;
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x060054AE RID: 21678 RVA: 0x00128811 File Offset: 0x00126A11
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationUser._properties;
			}
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x060054AF RID: 21679 RVA: 0x00128818 File Offset: 0x00126A18
		// (set) Token: 0x060054B0 RID: 21680 RVA: 0x0012882A File Offset: 0x00126A2A
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		[TypeConverter(typeof(LowerCaseStringConverter))]
		[StringValidator]
		public string Name
		{
			get
			{
				return (string)base[FormsAuthenticationUser._propName];
			}
			set
			{
				base[FormsAuthenticationUser._propName] = value;
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x060054B1 RID: 21681 RVA: 0x00128838 File Offset: 0x00126A38
		// (set) Token: 0x060054B2 RID: 21682 RVA: 0x0012884A File Offset: 0x00126A4A
		[ConfigurationProperty("password", IsRequired = true, DefaultValue = "")]
		[StringValidator]
		public string Password
		{
			get
			{
				return (string)base[FormsAuthenticationUser._propPassword];
			}
			set
			{
				base[FormsAuthenticationUser._propPassword] = value;
			}
		}

		// Token: 0x04002C6D RID: 11373
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C6E RID: 11374
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", new LowerCaseStringConverter(), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002C6F RID: 11375
		private static readonly ConfigurationProperty _propPassword = new ConfigurationProperty("password", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
	}
}
