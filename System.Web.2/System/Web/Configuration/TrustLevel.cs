using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000765 RID: 1893
	public sealed class TrustLevel : ConfigurationElement
	{
		// Token: 0x06005B3B RID: 23355 RVA: 0x0013CD54 File Offset: 0x0013AF54
		static TrustLevel()
		{
			TrustLevel._properties = new ConfigurationPropertyCollection();
			TrustLevel._properties.Add(TrustLevel._propName);
			TrustLevel._properties.Add(TrustLevel._propPolicyFile);
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x00117E9E File Offset: 0x0011609E
		internal TrustLevel()
		{
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x0013CDCD File Offset: 0x0013AFCD
		public TrustLevel(string name, string policyFile)
		{
			this.Name = name;
			this.PolicyFile = policyFile;
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x0013CDE3 File Offset: 0x0013AFE3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustLevel._properties;
			}
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06005B3F RID: 23359 RVA: 0x0013CDEA File Offset: 0x0013AFEA
		// (set) Token: 0x06005B40 RID: 23360 RVA: 0x0013CDFC File Offset: 0x0013AFFC
		[ConfigurationProperty("name", IsRequired = true, DefaultValue = "Full", IsKey = true)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[TrustLevel._propName];
			}
			set
			{
				base[TrustLevel._propName] = value;
			}
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x0013CE0A File Offset: 0x0013B00A
		// (set) Token: 0x06005B42 RID: 23362 RVA: 0x0013CE1C File Offset: 0x0013B01C
		[ConfigurationProperty("policyFile", IsRequired = true, DefaultValue = "internal")]
		public string PolicyFile
		{
			get
			{
				return (string)base[TrustLevel._propPolicyFile];
			}
			set
			{
				base[TrustLevel._propPolicyFile] = value;
			}
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x0013CE2C File Offset: 0x0013B02C
		internal string PolicyFileExpanded
		{
			get
			{
				if (this._PolicyFileExpanded == null)
				{
					string source = base.ElementInformation.Properties["policyFile"].Source;
					string str = source.Substring(0, source.LastIndexOf('\\') + 1);
					bool flag = true;
					if (this.PolicyFile.Length > 1)
					{
						char c = this.PolicyFile[1];
						char c2 = this.PolicyFile[0];
						if (c == ':')
						{
							flag = false;
						}
						else if (c2 == '\\' && c == '\\')
						{
							flag = false;
						}
					}
					if (flag)
					{
						this._PolicyFileExpanded = str + this.PolicyFile;
					}
					else
					{
						this._PolicyFileExpanded = this.PolicyFile;
					}
				}
				return this._PolicyFileExpanded;
			}
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x0013CEDC File Offset: 0x0013B0DC
		internal string LegacyPolicyFileExpanded
		{
			get
			{
				if (this._LegacyPolicyFileExpanded == null)
				{
					string source = base.ElementInformation.Properties["policyFile"].Source;
					string str = source.Substring(0, source.LastIndexOf('\\') + 1);
					bool flag = true;
					if (this.PolicyFile.Length > 1)
					{
						char c = this.PolicyFile[1];
						char c2 = this.PolicyFile[0];
						if (c == ':')
						{
							flag = false;
						}
						else if (c2 == '\\' && c == '\\')
						{
							flag = false;
						}
					}
					if (flag)
					{
						this._LegacyPolicyFileExpanded = str + "legacy." + this.PolicyFile;
					}
					else
					{
						this._LegacyPolicyFileExpanded = this.PolicyFile;
					}
				}
				return this._LegacyPolicyFileExpanded;
			}
		}

		// Token: 0x0400302B RID: 12331
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x0400302C RID: 12332
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "Full", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400302D RID: 12333
		private static readonly ConfigurationProperty _propPolicyFile = new ConfigurationProperty("policyFile", typeof(string), "internal", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400302E RID: 12334
		private string _PolicyFileExpanded;

		// Token: 0x0400302F RID: 12335
		private string _LegacyPolicyFileExpanded;
	}
}
