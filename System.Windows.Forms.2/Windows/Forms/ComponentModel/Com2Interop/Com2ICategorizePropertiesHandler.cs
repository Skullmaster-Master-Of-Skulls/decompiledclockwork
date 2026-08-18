using System;
using System.ComponentModel;
using System.Globalization;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049B RID: 1179
	[SuppressUnmanagedCodeSecurity]
	internal class Com2ICategorizePropertiesHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x001436C3 File Offset: 0x001418C3
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.ICategorizeProperties);
			}
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x001436D0 File Offset: 0x001418D0
		private string GetCategoryFromObject(object obj, int dispid)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is NativeMethods.ICategorizeProperties)
			{
				NativeMethods.ICategorizeProperties categorizeProperties = (NativeMethods.ICategorizeProperties)obj;
				try
				{
					int propcat = 0;
					if (categorizeProperties.MapPropertyToCategory(dispid, ref propcat) == 0)
					{
						string result = null;
						switch (propcat)
						{
						case -11:
							return SR.GetString("PropertyCategoryDDE");
						case -10:
							return SR.GetString("PropertyCategoryScale");
						case -9:
							return SR.GetString("PropertyCategoryText");
						case -8:
							return SR.GetString("PropertyCategoryList");
						case -7:
							return SR.GetString("PropertyCategoryData");
						case -6:
							return SR.GetString("PropertyCategoryBehavior");
						case -5:
							return SR.GetString("PropertyCategoryAppearance");
						case -4:
							return SR.GetString("PropertyCategoryPosition");
						case -3:
							return SR.GetString("PropertyCategoryFont");
						case -2:
							return SR.GetString("PropertyCategoryMisc");
						case -1:
							return "";
						default:
							if (categorizeProperties.GetCategoryName(propcat, CultureInfo.CurrentCulture.LCID, out result) == 0)
							{
								return result;
							}
							break;
						}
					}
				}
				catch
				{
				}
			}
			return null;
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x00143804 File Offset: 0x00141A04
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetBaseAttributes += this.OnGetAttributes;
			}
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x00143838 File Offset: 0x00141A38
		private void OnGetAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			string categoryFromObject = this.GetCategoryFromObject(sender.TargetObject, sender.DISPID);
			if (categoryFromObject != null && categoryFromObject.Length > 0)
			{
				attrEvent.Add(new CategoryAttribute(categoryFromObject));
			}
		}
	}
}
