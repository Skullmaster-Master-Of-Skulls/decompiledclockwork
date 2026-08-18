using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Web.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x02000625 RID: 1573
	internal sealed class DisplayAttributeAdapter
	{
		// Token: 0x06004EC8 RID: 20168 RVA: 0x00112333 File Offset: 0x00110533
		public DisplayAttributeAdapter(DisplayAttribute displayAttribute)
		{
			if (displayAttribute == null)
			{
				throw new ArgumentNullException("displayAttribute");
			}
			this._displayAttribute = displayAttribute;
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x00112350 File Offset: 0x00110550
		public string GetDescription()
		{
			string text = this.GetLocalizedString(this._displayAttribute.Description);
			if (text == null)
			{
				text = this._displayAttribute.GetDescription();
			}
			return text;
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x00112380 File Offset: 0x00110580
		public string GetShortName()
		{
			string text = this.GetLocalizedString(this._displayAttribute.ShortName);
			if (text == null)
			{
				text = this._displayAttribute.GetShortName();
			}
			return text;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x001123B0 File Offset: 0x001105B0
		public string GetPrompt()
		{
			string text = this.GetLocalizedString(this._displayAttribute.Prompt);
			if (text == null)
			{
				text = this._displayAttribute.GetPrompt();
			}
			return text;
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x001123E0 File Offset: 0x001105E0
		public string GetName()
		{
			string text = this.GetLocalizedString(this._displayAttribute.Name);
			if (text == null)
			{
				text = this._displayAttribute.GetName();
			}
			return text;
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x0011240F File Offset: 0x0011060F
		public int? GetOrder()
		{
			return this._displayAttribute.GetOrder();
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x0011241C File Offset: 0x0011061C
		private string GetLocalizedString(string name)
		{
			if (this._displayAttribute.ResourceType != null)
			{
				return null;
			}
			return StringLocalizerProviders.DataAnnotationStringLocalizerProvider.GetLocalizedString(Thread.CurrentThread.CurrentUICulture, name, new object[0]);
		}

		// Token: 0x04002A51 RID: 10833
		private DisplayAttribute _displayAttribute;
	}
}
