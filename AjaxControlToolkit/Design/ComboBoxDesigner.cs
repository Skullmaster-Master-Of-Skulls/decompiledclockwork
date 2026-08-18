using System;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000070 RID: 112
	public class ComboBoxDesigner : ListControlDesigner
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0000C004 File Offset: 0x0000A204
		public override string GetDesignTimeHtml()
		{
			ListControl listControl = (ListControl)base.ViewControl;
			ListItem[] array = new ListItem[listControl.Items.Count];
			listControl.Items.CopyTo(array, 0);
			string designTimeHtml = base.GetDesignTimeHtml();
			listControl.Items.Clear();
			listControl.Items.AddRange(array);
			string text = this.GetStringFromResourceStream("AjaxControlToolkit.Styles.ComboBox.css") + this.GetStringFromResourceStream("AjaxControlToolkit.Styles.Backgrounds.css");
			Regex regex = new Regex("(<%=)\\s*(WebResource\\(\")(?<resourceName>.+)\\s*(\"\\)%>)");
			text = regex.Replace(text, new MatchEvaluator(this.PerformWebResourceSubstitution));
			return "<style>" + text + "</style>" + designTimeHtml;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		private string GetStringFromResourceStream(string resourceName)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
			return new StreamReader(manifestResourceStream).ReadToEnd();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		protected virtual string PerformWebResourceSubstitution(Match match)
		{
			string text = match.ToString();
			return text.Replace(match.Value, base.ViewControl.Page.ClientScript.GetWebResourceUrl(base.GetType(), match.Groups["resourceName"].Value));
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000C11E File Offset: 0x0000A31E
		public override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000C124 File Offset: 0x0000A324
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new ComboBoxDesignerActionList(base.Component));
				return designerActionListCollection;
			}
		}

		// Token: 0x04000133 RID: 307
		private const string substitutionPattern = "(<%=)\\s*(WebResource\\(\")(?<resourceName>.+)\\s*(\"\\)%>)";
	}
}
