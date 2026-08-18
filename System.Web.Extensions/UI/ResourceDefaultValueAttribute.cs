using System;
using System.ComponentModel;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResourceDefaultValueAttribute : DefaultValueAttribute
	{
		// Token: 0x060003BC RID: 956 RVA: 0x00013C24 File Offset: 0x00011E24
		internal ResourceDefaultValueAttribute(Type type, string value) : base(value)
		{
			this._type = type;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00013C34 File Offset: 0x00011E34
		internal ResourceDefaultValueAttribute(string value) : base(value)
		{
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00013C3D File Offset: 0x00011E3D
		public override object TypeId
		{
			get
			{
				return typeof(DefaultValueAttribute);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00013C4C File Offset: 0x00011E4C
		public override object Value
		{
			get
			{
				if (!this._resourceLoaded)
				{
					this._resourceLoaded = true;
					string text = (string)base.Value;
					if (!string.IsNullOrEmpty(text))
					{
						object obj = AtlasWeb.ResourceManager.GetString(text, AtlasWeb.Culture);
						if (this._type != null)
						{
							try
							{
								obj = TypeDescriptor.GetConverter(this._type).ConvertFromInvariantString((string)obj);
							}
							catch (NotSupportedException)
							{
								obj = null;
							}
						}
						base.SetValue(obj);
					}
				}
				return base.Value;
			}
		}

		// Token: 0x0400016C RID: 364
		private Type _type;

		// Token: 0x0400016D RID: 365
		private bool _resourceLoaded;
	}
}
