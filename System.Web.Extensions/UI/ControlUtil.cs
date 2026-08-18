using System;

namespace System.Web.UI
{
	// Token: 0x0200004A RID: 74
	internal class ControlUtil
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00011A38 File Offset: 0x0000FC38
		internal static Control FindTargetControl(string controlID, Control control, bool searchNamingContainers)
		{
			Control control3;
			if (searchNamingContainers)
			{
				Control control2;
				if (control is INamingContainer)
				{
					control2 = control;
				}
				else
				{
					control2 = control.NamingContainer;
				}
				do
				{
					control3 = control2.FindControl(controlID);
					control2 = control2.NamingContainer;
					if (control3 != null)
					{
						break;
					}
				}
				while (control2 != null);
			}
			else
			{
				control3 = control.FindControl(controlID);
			}
			return control3;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00011A80 File Offset: 0x0000FC80
		internal static bool IsBuiltInHiddenField(string hiddenFieldName)
		{
			return hiddenFieldName.Length > 2 && hiddenFieldName[0] == '_' && hiddenFieldName[1] == '_' && (hiddenFieldName.StartsWith("__VIEWSTATE", StringComparison.Ordinal) || string.Equals(hiddenFieldName, "__EVENTVALIDATION", StringComparison.Ordinal) || string.Equals(hiddenFieldName, "__LASTFOCUS", StringComparison.Ordinal) || string.Equals(hiddenFieldName, "__SCROLLPOSITIONX", StringComparison.Ordinal) || string.Equals(hiddenFieldName, "__SCROLLPOSITIONY", StringComparison.Ordinal) || string.Equals(hiddenFieldName, "__EVENTTARGET", StringComparison.Ordinal) || string.Equals(hiddenFieldName, "__EVENTARGUMENT", StringComparison.Ordinal) || string.Equals(hiddenFieldName, "__PREVIOUSPAGE", StringComparison.Ordinal));
		}
	}
}
