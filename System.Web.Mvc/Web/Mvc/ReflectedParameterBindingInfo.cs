using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001A6 RID: 422
	internal class ReflectedParameterBindingInfo : ParameterBindingInfo
	{
		// Token: 0x06000BBF RID: 3007 RVA: 0x0001EB4F File Offset: 0x0001CD4F
		public ReflectedParameterBindingInfo(ParameterInfo parameterInfo)
		{
			this._parameterInfo = parameterInfo;
			this.ReadSettingsFromBindAttribute();
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0001EBC0 File Offset: 0x0001CDC0
		public override IModelBinder Binder
		{
			get
			{
				return ModelBinders.GetBinderFromAttributes(this._parameterInfo, delegate(ICustomAttributeProvider errorArg)
				{
					ParameterInfo parameterInfo = (ParameterInfo)errorArg;
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ReflectedParameterBindingInfo_MultipleConverterAttributes, new object[]
					{
						parameterInfo.Name,
						parameterInfo.Member
					}));
				});
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0001EBF7 File Offset: 0x0001CDF7
		public override ICollection<string> Exclude
		{
			get
			{
				return this._exclude;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0001EBFF File Offset: 0x0001CDFF
		public override ICollection<string> Include
		{
			get
			{
				return this._include;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0001EC07 File Offset: 0x0001CE07
		public override string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001EC10 File Offset: 0x0001CE10
		private void ReadSettingsFromBindAttribute()
		{
			BindAttribute bindAttribute = (BindAttribute)Attribute.GetCustomAttribute(this._parameterInfo, typeof(BindAttribute));
			if (bindAttribute == null)
			{
				return;
			}
			this._exclude = new ReadOnlyCollection<string>(AuthorizeAttribute.SplitString(bindAttribute.Exclude));
			this._include = new ReadOnlyCollection<string>(AuthorizeAttribute.SplitString(bindAttribute.Include));
			this._prefix = bindAttribute.Prefix;
		}

		// Token: 0x04000320 RID: 800
		private readonly ParameterInfo _parameterInfo;

		// Token: 0x04000321 RID: 801
		private ICollection<string> _exclude = new string[0];

		// Token: 0x04000322 RID: 802
		private ICollection<string> _include = new string[0];

		// Token: 0x04000323 RID: 803
		private string _prefix;
	}
}
