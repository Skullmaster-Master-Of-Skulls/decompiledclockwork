using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F0 RID: 1008
	[TypeConverter(typeof(EmbeddedMailObject.EmbeddedMailObjectTypeConverter))]
	public sealed class EmbeddedMailObject
	{
		// Token: 0x060030AD RID: 12461 RVA: 0x000030B5 File Offset: 0x000012B5
		public EmbeddedMailObject()
		{
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x0009EC23 File Offset: 0x0009CE23
		public EmbeddedMailObject(string name, string path)
		{
			this.Name = name;
			this.Path = path;
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x0009EC39 File Offset: 0x0009CE39
		// (set) Token: 0x060030B0 RID: 12464 RVA: 0x0009EC4F File Offset: 0x0009CE4F
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("EmbeddedMailObject_Name")]
		[NotifyParentProperty(true)]
		public string Name
		{
			get
			{
				if (this._name == null)
				{
					return string.Empty;
				}
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x0009EC58 File Offset: 0x0009CE58
		// (set) Token: 0x060030B2 RID: 12466 RVA: 0x0009EC6E File Offset: 0x0009CE6E
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("EmbeddedMailObject_Path")]
		[Editor("System.Web.UI.Design.MailFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[UrlProperty]
		public string Path
		{
			get
			{
				if (this._path != null)
				{
					return this._path;
				}
				return string.Empty;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x04002096 RID: 8342
		private string _path;

		// Token: 0x04002097 RID: 8343
		private string _name;

		// Token: 0x020009A2 RID: 2466
		private sealed class EmbeddedMailObjectTypeConverter : TypeConverter
		{
			// Token: 0x06006B5C RID: 27484 RVA: 0x0017E820 File Offset: 0x0017CA20
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return "EmbeddedMailObject";
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
