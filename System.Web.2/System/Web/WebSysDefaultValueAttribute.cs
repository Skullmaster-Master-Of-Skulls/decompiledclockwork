using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x0200010D RID: 269
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class WebSysDefaultValueAttribute : DefaultValueAttribute
	{
		// Token: 0x060010A4 RID: 4260 RVA: 0x0002E3A0 File Offset: 0x0002C5A0
		internal WebSysDefaultValueAttribute(Type type, string value) : base(value)
		{
			this._type = type;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		internal WebSysDefaultValueAttribute(string value) : base(value)
		{
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0002E3B9 File Offset: 0x0002C5B9
		public override object TypeId
		{
			get
			{
				return typeof(DefaultValueAttribute);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		public override object Value
		{
			get
			{
				if (!this._localized)
				{
					this._localized = true;
					string text = (string)base.Value;
					if (!string.IsNullOrEmpty(text))
					{
						object obj = SR.GetString(text);
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

		// Token: 0x04000648 RID: 1608
		private Type _type;

		// Token: 0x04000649 RID: 1609
		private bool _localized;
	}
}
