using System;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006F9 RID: 1785
	public sealed class HttpHandlerAction : ConfigurationElement
	{
		// Token: 0x06005626 RID: 22054 RVA: 0x0012E158 File Offset: 0x0012C358
		static HttpHandlerAction()
		{
			HttpHandlerAction._properties = new ConfigurationPropertyCollection();
			HttpHandlerAction._properties.Add(HttpHandlerAction._propPath);
			HttpHandlerAction._properties.Add(HttpHandlerAction._propVerb);
			HttpHandlerAction._properties.Add(HttpHandlerAction._propType);
			HttpHandlerAction._properties.Add(HttpHandlerAction._propValidate);
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x0012E22F File Offset: 0x0012C42F
		public HttpHandlerAction(string path, string type, string verb) : this(path, type, verb, true)
		{
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x0012E23B File Offset: 0x0012C43B
		public HttpHandlerAction(string path, string type, string verb, bool validate)
		{
			this.Path = path;
			this.Type = type;
			this.Verb = verb;
			this.Validate = validate;
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x00117E9E File Offset: 0x0011609E
		internal HttpHandlerAction()
		{
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x0600562A RID: 22058 RVA: 0x0012E260 File Offset: 0x0012C460
		internal string Key
		{
			get
			{
				return "verb=" + this.Verb + " | path=" + this.Path;
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x0600562B RID: 22059 RVA: 0x0012E27D File Offset: 0x0012C47D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlerAction._properties;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x0600562C RID: 22060 RVA: 0x0012E284 File Offset: 0x0012C484
		// (set) Token: 0x0600562D RID: 22061 RVA: 0x0012E296 File Offset: 0x0012C496
		[ConfigurationProperty("path", IsRequired = true, IsKey = true)]
		public string Path
		{
			get
			{
				return (string)base[HttpHandlerAction._propPath];
			}
			set
			{
				base[HttpHandlerAction._propPath] = value;
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x0600562E RID: 22062 RVA: 0x0012E2A4 File Offset: 0x0012C4A4
		// (set) Token: 0x0600562F RID: 22063 RVA: 0x0012E2B6 File Offset: 0x0012C4B6
		[ConfigurationProperty("verb", IsRequired = true, IsKey = true)]
		public string Verb
		{
			get
			{
				return (string)base[HttpHandlerAction._propVerb];
			}
			set
			{
				base[HttpHandlerAction._propVerb] = value;
			}
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06005630 RID: 22064 RVA: 0x0012E2C4 File Offset: 0x0012C4C4
		// (set) Token: 0x06005631 RID: 22065 RVA: 0x0012E2EA File Offset: 0x0012C4EA
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				if (this.typeCache == null)
				{
					this.typeCache = (string)base[HttpHandlerAction._propType];
				}
				return this.typeCache;
			}
			set
			{
				base[HttpHandlerAction._propType] = value;
				this.typeCache = value;
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06005632 RID: 22066 RVA: 0x0012E2FF File Offset: 0x0012C4FF
		internal Type TypeInternal
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06005633 RID: 22067 RVA: 0x0012E307 File Offset: 0x0012C507
		// (set) Token: 0x06005634 RID: 22068 RVA: 0x0012E319 File Offset: 0x0012C519
		[ConfigurationProperty("validate", DefaultValue = true)]
		public bool Validate
		{
			get
			{
				return (bool)base[HttpHandlerAction._propValidate];
			}
			set
			{
				base[HttpHandlerAction._propValidate] = value;
			}
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x0012E32C File Offset: 0x0012C52C
		[FileIOPermission(SecurityAction.Assert, AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery))]
		internal void InitValidateInternal()
		{
			string text = this.Verb;
			text = text.Replace(" ", string.Empty);
			this._requestType = new Wildcard(text, false);
			this._path = new WildcardUrl(this.Path, true);
			if (!this.Validate)
			{
				this._type = null;
				return;
			}
			this._type = ConfigUtil.GetType(this.Type, "type", this);
			if (!ConfigUtil.IsTypeHandlerOrFactory(this._type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_not_factory_or_handler", new object[]
				{
					this.Type
				}), base.ElementInformation.Source, base.ElementInformation.LineNumber);
			}
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x0012E3D9 File Offset: 0x0012C5D9
		internal bool IsMatch(string verb, VirtualPath path)
		{
			return this._path.IsSuffix(path.VirtualPathString) && this._requestType.IsMatch(verb);
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x0012E3FC File Offset: 0x0012C5FC
		internal object Create()
		{
			if (this._type == null)
			{
				Type type = ConfigUtil.GetType(this.Type, "type", this);
				if (!ConfigUtil.IsTypeHandlerOrFactory(type))
				{
					throw new ConfigurationErrorsException(SR.GetString("Type_not_factory_or_handler", new object[]
					{
						this.Type
					}), base.ElementInformation.Source, base.ElementInformation.LineNumber);
				}
				this._type = type;
			}
			return HttpRuntime.CreateNonPublicInstanceByWebObjectActivator(this._type);
		}

		// Token: 0x04002DC9 RID: 11721
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002DCA RID: 11722
		private static readonly ConfigurationProperty _propPath = new ConfigurationProperty("path", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002DCB RID: 11723
		private static readonly ConfigurationProperty _propVerb = new ConfigurationProperty("verb", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002DCC RID: 11724
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04002DCD RID: 11725
		private static readonly ConfigurationProperty _propValidate = new ConfigurationProperty("validate", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002DCE RID: 11726
		private Wildcard _requestType;

		// Token: 0x04002DCF RID: 11727
		private WildcardUrl _path;

		// Token: 0x04002DD0 RID: 11728
		private Type _type;

		// Token: 0x04002DD1 RID: 11729
		private string typeCache;
	}
}
