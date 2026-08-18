using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02001960 RID: 6496
	public class RadDataPagerFieldItem : Control, INamingContainer
	{
		// Token: 0x17004C00 RID: 19456
		// (get) Token: 0x0600FB84 RID: 64388 RVA: 0x0038A580 File Offset: 0x00388780
		public RadDataPagerField Field
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x17004C01 RID: 19457
		// (get) Token: 0x0600FB85 RID: 64389 RVA: 0x0038A588 File Offset: 0x00388788
		public RadDataPager Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x0600FB86 RID: 64390 RVA: 0x0038A63C File Offset: 0x0038883C
		public RadDataPagerFieldItem(RadDataPager owner, RadDataPagerField field)
		{
			this._field = field2;
			this._field.FieldChanged += delegate(object sender, EventArgs args)
			{
				this.Visible = ((RadDataPagerField)sender).Visible;
			};
			this._owner = owner;
		}

		// Token: 0x0600FB87 RID: 64391 RVA: 0x0038A6A0 File Offset: 0x003888A0
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			bool result = false;
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null)
			{
				result = true;
				RadDataPagerCommandEventArgs args2 = new RadDataPagerCommandEventArgs(this.Owner, this, source, commandEventArgs);
				base.RaiseBubbleEvent(this, args2);
			}
			return result;
		}

		// Token: 0x0600FB88 RID: 64392 RVA: 0x0038B1A0 File Offset: 0x003893A0
		internal static string ResolveAdaptiveClassNames(RadDataPagerField field)
		{
			string arg = "rdp";
			RadDataPagerButtonField radDataPagerButtonField = field as RadDataPagerButtonField;
			string format = " {0}{1}";
			string format2 = " {0}{1}{2}";
			StringBuilder stringBuilder = new StringBuilder();
			if (field.HiddenXs)
			{
				stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
				{
					field.HiddenXs
				}));
			}
			if (field.HiddenSm)
			{
				stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
				{
					field.HiddenSm
				}));
			}
			if (field.HiddenMd)
			{
				stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
				{
					field.HiddenMd
				}));
			}
			if (field.HiddenLg)
			{
				stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
				{
					field.HiddenLg
				}));
			}
			if (field.HiddenXl)
			{
				stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
				{
					field.HiddenXl
				}));
			}
			if (field.HorizontalPositionXs != PagerFieldAdaptiveHorizontalPosition.NotSet)
			{
				stringBuilder.AppendFormat(format2, arg, field.HorizontalPositionXs.ToString(), RadDataPagerFieldItem.GetNameSuffix(new
				{
					field.HorizontalPositionXs
				}));
			}
			if (field.HorizontalPositionSm != PagerFieldAdaptiveHorizontalPosition.NotSet)
			{
				stringBuilder.AppendFormat(format2, arg, field.HorizontalPositionSm.ToString(), RadDataPagerFieldItem.GetNameSuffix(new
				{
					field.HorizontalPositionSm
				}));
			}
			if (field.HorizontalPositionMd != PagerFieldAdaptiveHorizontalPosition.NotSet)
			{
				stringBuilder.AppendFormat(format2, arg, field.HorizontalPositionMd.ToString(), RadDataPagerFieldItem.GetNameSuffix(new
				{
					field.HorizontalPositionMd
				}));
			}
			if (field.HorizontalPositionLg != PagerFieldAdaptiveHorizontalPosition.NotSet)
			{
				stringBuilder.AppendFormat(format2, arg, field.HorizontalPositionLg.ToString(), RadDataPagerFieldItem.GetNameSuffix(new
				{
					field.HorizontalPositionLg
				}));
			}
			if (field.HorizontalPositionXl != PagerFieldAdaptiveHorizontalPosition.NotSet)
			{
				stringBuilder.AppendFormat(format2, arg, field.HorizontalPositionXl.ToString(), RadDataPagerFieldItem.GetNameSuffix(new
				{
					field.HorizontalPositionXl
				}));
			}
			if (radDataPagerButtonField != null && radDataPagerButtonField.FieldType == PagerButtonFieldType.Numeric)
			{
				if (radDataPagerButtonField.TrimXs)
				{
					stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
					{
						radDataPagerButtonField.TrimXs
					}));
				}
				if (radDataPagerButtonField.TrimSm)
				{
					stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
					{
						radDataPagerButtonField.TrimSm
					}));
				}
				if (radDataPagerButtonField.TrimMd)
				{
					stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
					{
						radDataPagerButtonField.TrimMd
					}));
				}
				if (radDataPagerButtonField.TrimLg)
				{
					stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
					{
						radDataPagerButtonField.TrimLg
					}));
				}
				if (radDataPagerButtonField.TrimXl)
				{
					stringBuilder.AppendFormat(format, arg, RadDataPagerFieldItem.GetName(new
					{
						radDataPagerButtonField.TrimXl
					}));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600FB89 RID: 64393 RVA: 0x0038B434 File Offset: 0x00389634
		private static string GetName<T>(T item) where T : class
		{
			return typeof(T).GetProperties()[0].Name;
		}

		// Token: 0x0600FB8A RID: 64394 RVA: 0x0038B44C File Offset: 0x0038964C
		private static string GetNameSuffix<T>(T item) where T : class
		{
			string name = typeof(T).GetProperties()[0].Name;
			return name.Substring(name.Length - 2);
		}

		// Token: 0x0600FB8B RID: 64395 RVA: 0x0038B47E File Offset: 0x0038967E
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.PrepareItemClassName(this.Field));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.Render(writer);
			writer.RenderEndTag();
		}

		// Token: 0x04004786 RID: 18310
		protected const string FieldItemClassName = "rdpWrap";

		// Token: 0x04004787 RID: 18311
		protected const string FieldItemNumPartClassName = "rdpNumPart";

		// Token: 0x04004788 RID: 18312
		protected const string FieldHorizontalPositionRightSuffix = "Right";

		// Token: 0x04004789 RID: 18313
		protected const string FieldHorizontalPositionNoneSuffix = "None";

		// Token: 0x0400478A RID: 18314
		private RadDataPagerField _field;

		// Token: 0x0400478B RID: 18315
		private RadDataPager _owner;

		// Token: 0x0400478C RID: 18316
		internal TFunc<RadDataPagerField, string> PrepareItemClassName = delegate(RadDataPagerField field)
		{
			string arg = "";
			string arg2 = "";
			StringBuilder stringBuilder = new StringBuilder();
			RadDataPagerButtonField radDataPagerButtonField = field as RadDataPagerButtonField;
			if (radDataPagerButtonField != null)
			{
				arg = ((radDataPagerButtonField.FieldType == PagerButtonFieldType.Numeric) ? string.Format(" {0}", "rdpNumPart") : "");
			}
			switch (field.HorizontalPosition)
			{
			case PagerFieldHorizontalPosition.RightFloat:
				arg2 = "Right";
				break;
			case PagerFieldHorizontalPosition.NoFloat:
				arg2 = "None";
				break;
			}
			stringBuilder.AppendFormat("{0}{1}{2}", "rdpWrap", arg2, arg);
			stringBuilder.Append(RadDataPagerFieldItem.ResolveAdaptiveClassNames(field));
			return stringBuilder.ToString();
		};
	}
}
