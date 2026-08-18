using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x02001B6A RID: 7018
	internal class TreeNodeContextMenuIDConverter : TypeConverter
	{
		// Token: 0x06010FFE RID: 69630 RVA: 0x003C200C File Offset: 0x003C020C
		private string[] GetContextMenuIDs(RadTreeNode node)
		{
			RadTreeViewContextMenuCollection contextMenus = node.TreeView.ContextMenus;
			string[] array = new string[contextMenus.Count];
			for (int i = 0; i < contextMenus.Count; i++)
			{
				array[i] = contextMenus[i].ID;
			}
			return array;
		}

		// Token: 0x06010FFF RID: 69631 RVA: 0x003C2054 File Offset: 0x003C0254
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null)
			{
				return null;
			}
			RadTreeNode node = (RadTreeNode)context.Instance;
			string[] contextMenuIDs = this.GetContextMenuIDs(node);
			if (contextMenuIDs == null || contextMenuIDs.Length == 0)
			{
				return null;
			}
			return new TypeConverter.StandardValuesCollection(contextMenuIDs);
		}

		// Token: 0x06011000 RID: 69632 RVA: 0x003C208A File Offset: 0x003C028A
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06011001 RID: 69633 RVA: 0x003C208D File Offset: 0x003C028D
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null;
		}

		// Token: 0x06011002 RID: 69634 RVA: 0x003C2096 File Offset: 0x003C0296
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06011003 RID: 69635 RVA: 0x003C20A8 File Offset: 0x003C02A8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return value;
		}
	}
}
