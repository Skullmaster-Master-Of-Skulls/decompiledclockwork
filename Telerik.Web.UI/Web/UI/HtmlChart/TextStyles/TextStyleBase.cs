using System;
using System.ComponentModel;
using System.Text;

namespace Telerik.Web.UI.HtmlChart.TextStyles
{
	// Token: 0x02000518 RID: 1304
	public abstract class TextStyleBase : StateManager
	{
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x00098962 File Offset: 0x00096B62
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x00098982 File Offset: 0x00096B82
		[DefaultValue("")]
		public string Margin
		{
			get
			{
				return (string)(base.ViewState["Margin"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Margin"] = value;
			}
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x00098998 File Offset: 0x00096B98
		protected internal virtual string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.GetSerializedMargin());
			return stringBuilder.ToString();
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000989C0 File Offset: 0x00096BC0
		protected internal string GetSerializedMargin()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.SerializeSpacing(stringBuilder, this.Margin, "margin");
			return stringBuilder.ToString();
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000989EC File Offset: 0x00096BEC
		protected internal void SerializeSpacing(StringBuilder sb, string spacing, string propertyName)
		{
			if (spacing != string.Empty)
			{
				string text = spacing.Replace("px", "").Replace("pt", "").Replace("em", "");
				string[] spacingValues = text.Split(new char[]
				{
					' '
				}, 4);
				this.AppendSpacingSerialization(sb, propertyName, spacingValues);
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x00098A54 File Offset: 0x00096C54
		private void AppendSpacingSerialization(StringBuilder sb, string propertyName, string[] spacingValues)
		{
			switch (spacingValues.Length)
			{
			case 1:
				sb.AppendFormat("{0}:{1},", propertyName, spacingValues[0]);
				return;
			case 2:
				sb.Append(string.Format("{0}:{{top:{1},right:{2},bottom:{3},left:{4}}},", new object[]
				{
					propertyName,
					spacingValues[0],
					spacingValues[1],
					spacingValues[0],
					spacingValues[1]
				}));
				return;
			case 3:
				sb.Append(string.Format("{0}:{{top:{1},right:{2},bottom:{3},left:{4}}},", new object[]
				{
					propertyName,
					spacingValues[0],
					spacingValues[1],
					spacingValues[2],
					spacingValues[1]
				}));
				return;
			case 4:
				sb.Append(string.Format("{0}:{{top:{1},right:{2},bottom:{3},left:{4}}},", new object[]
				{
					propertyName,
					spacingValues[0],
					spacingValues[1],
					spacingValues[2],
					spacingValues[3]
				}));
				return;
			default:
				return;
			}
		}
	}
}
