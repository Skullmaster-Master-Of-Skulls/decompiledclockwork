using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000EC2 RID: 3778
	internal class RibbonBarTemplateItemConvertor : JavaScriptConverter
	{
		// Token: 0x06009036 RID: 36918 RVA: 0x00207528 File Offset: 0x00205728
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009037 RID: 36919 RVA: 0x00207530 File Offset: 0x00205730
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RibbonBarTemplateItem ribbonBarTemplateItem = obj as RibbonBarTemplateItem;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this.ShouldSerializeWidth(ribbonBarTemplateItem))
			{
				dictionary["width"] = this.SerializePixelWidthValue(ribbonBarTemplateItem.Width);
			}
			return dictionary;
		}

		// Token: 0x17002DAE RID: 11694
		// (get) Token: 0x06009038 RID: 36920 RVA: 0x00207638 File Offset: 0x00205838
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarTemplateItem);
				yield break;
			}
		}

		// Token: 0x06009039 RID: 36921 RVA: 0x00207655 File Offset: 0x00205855
		private string SerializePixelWidthValue(Unit width)
		{
			if (width.IsEmpty)
			{
				return "0px";
			}
			return string.Format("{0}px", Math.Round(width.Value));
		}

		// Token: 0x0600903A RID: 36922 RVA: 0x00207684 File Offset: 0x00205884
		private bool ShouldSerializeWidth(RibbonBarTemplateItem templateItem)
		{
			return !templateItem.Width.IsEmpty && templateItem.Width.Type == UnitType.Pixel;
		}
	}
}
