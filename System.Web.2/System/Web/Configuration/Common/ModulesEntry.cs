using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Web.Configuration.Common
{
	// Token: 0x02000780 RID: 1920
	internal class ModulesEntry
	{
		// Token: 0x06005C3A RID: 23610 RVA: 0x0013F468 File Offset: 0x0013D668
		internal ModulesEntry(string name, string typeName, string propertyName, ConfigurationElement configElement)
		{
			this._name = ((name != null) ? name : string.Empty);
			this._type = this.SecureGetType(typeName, propertyName, configElement);
			if (typeof(IHttpModule).IsAssignableFrom(this._type))
			{
				return;
			}
			if (configElement == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_not_module", new object[]
				{
					typeName
				}));
			}
			throw new ConfigurationErrorsException(SR.GetString("Type_not_module", new object[]
			{
				typeName
			}), configElement.ElementInformation.Properties["type"].Source, configElement.ElementInformation.Properties["type"].LineNumber);
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x0013F521 File Offset: 0x0013D721
		internal static bool IsTypeMatch(Type type, string typeName)
		{
			return type.Name.Equals(typeName) || type.FullName.Equals(typeName);
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x06005C3C RID: 23612 RVA: 0x0013F53F File Offset: 0x0013D73F
		internal string ModuleName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x0013F547 File Offset: 0x0013D747
		internal IHttpModule Create()
		{
			return (IHttpModule)HttpRuntime.CreateNonPublicInstanceByWebObjectActivator(this._type);
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x0013F559 File Offset: 0x0013D759
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private Type SecureGetType(string typeName, string propertyName, ConfigurationElement configElement)
		{
			return ConfigUtil.GetType(typeName, propertyName, configElement, false);
		}

		// Token: 0x0400308A RID: 12426
		private string _name;

		// Token: 0x0400308B RID: 12427
		private Type _type;
	}
}
