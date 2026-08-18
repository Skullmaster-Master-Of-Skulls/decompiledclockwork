using System;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200001B RID: 27
	[DebuggerDisplay("Name = {Name}")]
	public class ConfigurationAttribute
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00005720 File Offset: 0x00004720
		internal ConfigurationAttribute(IAppHostProperty property, ConfigurationElement parentElement)
		{
			this._property = property;
			this._parentElement = parentElement;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005736 File Offset: 0x00004736
		public bool IsInheritedFromDefaultValue
		{
			get
			{
				return (bool)this._property.GetMetadata("isDefaultValue");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000574D File Offset: 0x0000474D
		public bool IsProtected
		{
			get
			{
				return (bool)this._property.GetMetadata("isEncrypted");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005764 File Offset: 0x00004764
		public string Name
		{
			get
			{
				return this._property.Name;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005774 File Offset: 0x00004774
		public ConfigurationAttributeSchema Schema
		{
			get
			{
				if (this._schema == null)
				{
					IAppHostPropertySchema schema = this._property.Schema;
					if (schema != null)
					{
						this._schema = new ConfigurationAttributeSchema(this._property.Schema);
					}
				}
				return this._schema;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000057B4 File Offset: 0x000047B4
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000057C1 File Offset: 0x000047C1
		public object Value
		{
			get
			{
				return ConfigurationElement.GetPropertyValue(this._property);
			}
			set
			{
				this._parentElement.SetAttributeValue(this.Name, value);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000057D5 File Offset: 0x000047D5
		public void Delete()
		{
			this._property.Clear();
			this._parentElement.SetDirty();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000057ED File Offset: 0x000047ED
		public object GetMetadata(string metadataType)
		{
			return this._property.GetMetadata(metadataType);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000057FB File Offset: 0x000047FB
		public void SetMetadata(string metadataType, object value)
		{
			this._property.SetMetadata(metadataType, value);
			this._parentElement.SetDirty();
		}

		// Token: 0x04000056 RID: 86
		private ConfigurationElement _parentElement;

		// Token: 0x04000057 RID: 87
		private IAppHostProperty _property;

		// Token: 0x04000058 RID: 88
		private ConfigurationAttributeSchema _schema;
	}
}
