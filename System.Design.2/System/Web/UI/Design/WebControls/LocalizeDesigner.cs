using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000DC RID: 220
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class LocalizeDesigner : LiteralDesigner
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x00028BAC File Offset: 0x00026DAC
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			EditableDesignerRegion editableDesignerRegion = new EditableDesignerRegion(this, "Text");
			editableDesignerRegion.Description = SR.GetString("LocalizeDesigner_RegionWatermark");
			editableDesignerRegion.Properties[typeof(Control)] = base.Component;
			regions.Add(editableDesignerRegion);
			return string.Format(CultureInfo.InvariantCulture, "<span {0}=0></span>", new object[]
			{
				DesignerRegion.DesignerRegionAttributeName
			});
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00028C18 File Offset: 0x00026E18
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Text"];
			return (string)propertyDescriptor.GetValue(base.Component);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00028C4C File Offset: 0x00026E4C
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			string text = content;
			try
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				Control[] array = ControlParser.ParseControls(designerHost, content);
				text = string.Empty;
				foreach (Control control in array)
				{
					LiteralControl literalControl = control as LiteralControl;
					if (literalControl != null)
					{
						text += literalControl.Text;
					}
				}
			}
			catch
			{
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Text"];
			propertyDescriptor.SetValue(base.Component, text);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00028CF0 File Offset: 0x00026EF0
		protected override void PostFilterProperties(IDictionary properties)
		{
			base.HideAllPropertiesUnlessExcluded(properties, this.EnabledPropertiesInGrid);
			base.PostFilterAttributes(properties);
		}

		// Token: 0x04000467 RID: 1127
		private const string DesignTimeHtml = "<span {0}=0></span>";

		// Token: 0x04000468 RID: 1128
		private readonly string[] EnabledPropertiesInGrid = new string[]
		{
			"ID",
			"Text"
		};
	}
}
