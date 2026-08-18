using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020000C2 RID: 194
	[ToolboxItem(false)]
	public class KeyboardNavigationSettings : StateManager
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		public KeyboardNavigationSettings() : this(new JavaScriptConverter[]
		{
			new KeyboardNavigationSettingsConverter()
		})
		{
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001C803 File Offset: 0x0001AA03
		public KeyboardNavigationSettings(JavaScriptConverter[] converters)
		{
			this._serializer = new JavaScriptSerializer();
			this._serializer.RegisterConverters(converters);
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001C822 File Offset: 0x0001AA22
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0001C843 File Offset: 0x0001AA43
		[DefaultValue(typeof(KeyboardNavigationModifier), "Alt")]
		[Description("This property sets the key that is used to focus RadTabStrip. It is always used in combination with FocusKey.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual KeyboardNavigationModifier CommandKey
		{
			get
			{
				return (KeyboardNavigationModifier)(base.ViewState["CommandKey"] ?? KeyboardNavigationModifier.Alt);
			}
			set
			{
				base.ViewState["CommandKey"] = (int)value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001C85B File Offset: 0x0001AA5B
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001C87D File Offset: 0x0001AA7D
		[Category("Client")]
		[Description("This property sets the key that is used to focus RadTabStrip. It is always used in combination with CommandKey.")]
		[DefaultValue(typeof(KeyboardNavigationKey), "T")]
		[NotifyParentProperty(true)]
		public virtual KeyboardNavigationKey FocusKey
		{
			get
			{
				return (KeyboardNavigationKey)(base.ViewState["FocusKey"] ?? KeyboardNavigationKey.T);
			}
			set
			{
				base.ViewState["FocusKey"] = (int)value;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001C895 File Offset: 0x0001AA95
		internal void Describe(IScriptDescriptor descriptor)
		{
			descriptor.AddProperty("_navigationSettings", this._serializer.Serialize(this));
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001C8AE File Offset: 0x0001AAAE
		internal void Describe(IScriptDescriptor descriptor, string clientProperty)
		{
			descriptor.AddProperty(clientProperty, this._serializer.Serialize(this));
		}

		// Token: 0x040001B6 RID: 438
		private JavaScriptSerializer _serializer;
	}
}
