using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B82 RID: 7042
	internal class Utility
	{
		// Token: 0x060110FC RID: 69884 RVA: 0x003C347C File Offset: 0x003C167C
		internal static void CopyBaseAttributesToInnerControl(WebControl control, WebControl child)
		{
			short tabIndex = control.TabIndex;
			string accessKey = control.AccessKey;
			Unit width = control.Width;
			Unit height = control.Height;
			try
			{
				control.AccessKey = string.Empty;
				control.TabIndex = 0;
				control.Width = Unit.Empty;
				control.Height = Unit.Empty;
				child.CopyBaseAttributes(control);
			}
			finally
			{
				control.TabIndex = tabIndex;
				control.AccessKey = accessKey;
				control.Width = width;
				control.Height = height;
			}
		}

		// Token: 0x060110FD RID: 69885 RVA: 0x003C3504 File Offset: 0x003C1704
		internal static string GetUploadUniqueIdentifier(HttpContext context)
		{
			return context.Request.QueryString[Utility.UNIQUE_REQUEST_QUERY_IDENTIFIER];
		}

		// Token: 0x060110FE RID: 69886 RVA: 0x003C351B File Offset: 0x003C171B
		internal static string GetLocalizationScriptBlockIdentifier(string Language)
		{
			return string.Format(Utility.LANGUAGE_SCRIPT_BLOCK_IDENTIFIER, Language);
		}

		// Token: 0x060110FF RID: 69887 RVA: 0x003C3528 File Offset: 0x003C1728
		internal static string GetUniquePageIdentifier()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x06011100 RID: 69888 RVA: 0x003C3548 File Offset: 0x003C1748
		internal static bool IsInteger(string s)
		{
			if (s.Length == 0 || s.Length > 10)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsNumber(s, i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06011101 RID: 69889 RVA: 0x003C3588 File Offset: 0x003C1788
		internal static string GetJsArray(string[] arrayToConvert)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (string arg in arrayToConvert)
			{
				stringBuilder.AppendFormat("\"{0}\",", arg);
			}
			if (arrayToConvert.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06011102 RID: 69890 RVA: 0x003C35E9 File Offset: 0x003C17E9
		private static object GetValueFromViewStateBase(StateBag viewState, string key, object defaultValue)
		{
			if (viewState[key] == null)
			{
				return defaultValue;
			}
			return viewState[key];
		}

		// Token: 0x06011103 RID: 69891 RVA: 0x003C35FD File Offset: 0x003C17FD
		public static string GetValueFromViewState(StateBag viewState, string key, string defaultValue)
		{
			return (string)Utility.GetValueFromViewStateBase(viewState, key, defaultValue);
		}

		// Token: 0x06011104 RID: 69892 RVA: 0x003C360C File Offset: 0x003C180C
		public static string[] GetValueFromViewState(StateBag viewState, string key, string[] defaultValue)
		{
			return (string[])Utility.GetValueFromViewStateBase(viewState, key, defaultValue);
		}

		// Token: 0x06011105 RID: 69893 RVA: 0x003C361B File Offset: 0x003C181B
		public static int GetValueFromViewState(StateBag viewState, string key, int defaultValue)
		{
			return (int)Utility.GetValueFromViewStateBase(viewState, key, defaultValue);
		}

		// Token: 0x06011106 RID: 69894 RVA: 0x003C362F File Offset: 0x003C182F
		public static bool GetValueFromViewState(StateBag viewState, string key, bool defaultValue)
		{
			return (bool)Utility.GetValueFromViewStateBase(viewState, key, defaultValue);
		}

		// Token: 0x06011107 RID: 69895 RVA: 0x003C3643 File Offset: 0x003C1843
		public static Unit GetValueFromViewState(StateBag viewState, string key, Unit defaultValue)
		{
			return (Unit)Utility.GetValueFromViewStateBase(viewState, key, defaultValue);
		}

		// Token: 0x04004C67 RID: 19559
		internal static readonly string UPLOAD_IDS_QUERY_IDENTIFIER = "rU_Ids";

		// Token: 0x04004C68 RID: 19560
		internal static readonly char UPLOAD_IDS_QUERY_SEPARATOR = ',';

		// Token: 0x04004C69 RID: 19561
		internal static readonly string UNIQUE_REQUEST_QUERY_IDENTIFIER = "RadUrid";

		// Token: 0x04004C6A RID: 19562
		internal static readonly string CONTROL_CLIENT_SCRIPT_BLOCK_IDENTIFIER = "radUploadScripts";

		// Token: 0x04004C6B RID: 19563
		internal static readonly string LANGUAGE_SCRIPT_BLOCK_IDENTIFIER = "radUploadLocalization_{0}";
	}
}
